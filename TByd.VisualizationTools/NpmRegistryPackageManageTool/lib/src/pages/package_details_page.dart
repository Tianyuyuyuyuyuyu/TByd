import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:intl/intl.dart';
import '../models/package_model.dart';
import '../providers/package_provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

class PackageDetailsPage extends ConsumerWidget {
  final String packageName;
  final DateFormat _dateFormat = DateFormat('yyyy-MM-dd HH:mm');

  PackageDetailsPage({
    super.key,
    required this.packageName,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final l10n = AppLocalizations.of(context);
    final theme = Theme.of(context);

    return Scaffold(
      appBar: AppBar(
        title: Text(packageName),
      ),
      body: Consumer(
        builder: (context, ref, child) {
          final packageDetails = ref.watch(packageDetailsProvider(packageName));

          if (packageDetails.error != null) {
            return Center(
              child: Text(
                packageDetails.error!,
                style: TextStyle(color: theme.colorScheme.error),
              ),
            );
          }

          if (packageDetails.isLoading) {
            return const Center(
              child: CircularProgressIndicator(),
            );
          }

          if (packageDetails.package == null) {
            return const Center(
              child: Text('Package not found'),
            );
          }

          final package = packageDetails.package!;

          return SingleChildScrollView(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                _buildSection(
                  title: l10n.description,
                  content: package.description,
                  theme: theme,
                ),
                const SizedBox(height: 16.0),
                _buildInfoRow(
                  icon: Icons.person_outline,
                  label: l10n.author,
                  value: package.author,
                  theme: theme,
                ),
                _buildInfoRow(
                  icon: Icons.access_time,
                  label: l10n.lastModified,
                  value: _dateFormat.format(package.publishedAt),
                  theme: theme,
                ),
                _buildInfoRow(
                  icon: Icons.verified_user_outlined,
                  label: l10n.license,
                  value: package.license,
                  theme: theme,
                ),
                const SizedBox(height: 16.0),
                _buildSection(
                  title: l10n.keywords,
                  content: package.keywords.isEmpty ? l10n.noKeywords : package.keywords.join(', '),
                  theme: theme,
                ),
                const SizedBox(height: 16.0),
                _buildVersionsList(
                  versions: packageDetails.versions,
                  theme: theme,
                  l10n: l10n,
                  ref: ref,
                  packageName: packageName,
                ),
                const SizedBox(height: 16.0),
                _buildDependenciesList(
                  dependencies: package.dependencies,
                  theme: theme,
                  l10n: l10n,
                ),
              ],
            ),
          );
        },
      ),
    );
  }

  Widget _buildSection({
    required String title,
    required String content,
    required ThemeData theme,
  }) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          title,
          style: theme.textTheme.titleMedium,
        ),
        const SizedBox(height: 8.0),
        Text(
          content,
          style: theme.textTheme.bodyMedium,
        ),
      ],
    );
  }

  Widget _buildInfoRow({
    required IconData icon,
    required String label,
    required String value,
    required ThemeData theme,
  }) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 4.0),
      child: Row(
        children: [
          Icon(icon, size: 20.0),
          const SizedBox(width: 8.0),
          Text(
            '$label: ',
            style: theme.textTheme.bodyMedium?.copyWith(
              fontWeight: FontWeight.bold,
            ),
          ),
          Expanded(
            child: Text(
              value,
              style: theme.textTheme.bodyMedium,
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildVersionsList({
    required List<PackageVersion> versions,
    required ThemeData theme,
    required AppLocalizations l10n,
    required WidgetRef ref,
    required String packageName,
  }) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          l10n.version,
          style: theme.textTheme.titleMedium,
        ),
        const SizedBox(height: 8.0),
        ListView.builder(
          shrinkWrap: true,
          physics: const NeverScrollableScrollPhysics(),
          itemCount: versions.length,
          itemBuilder: (context, index) {
            final version = versions[index];
            return Card(
              child: ListTile(
                title: Text('v${version.version}'),
                subtitle: Text(
                  _dateFormat.format(version.publishedAt),
                ),
                trailing: PopupMenuButton<String>(
                  itemBuilder: (context) => [
                    PopupMenuItem(
                      value: 'unpublish',
                      child: Text(l10n.unpublish),
                    ),
                    PopupMenuItem(
                      value: 'deprecate',
                      child: Text(l10n.deprecate),
                    ),
                  ],
                  onSelected: (value) async {
                    if (value == 'unpublish') {
                      final confirmed = await showDialog<bool>(
                        context: context,
                        builder: (context) => AlertDialog(
                          title: Text(l10n.confirmUnpublish),
                          content: Text(
                            l10n.confirmUnpublishMessage(
                              packageName,
                              version.version,
                            ),
                          ),
                          actions: [
                            TextButton(
                              onPressed: () => Navigator.pop(context, false),
                              child: Text(l10n.cancel),
                            ),
                            TextButton(
                              onPressed: () => Navigator.pop(context, true),
                              child: Text(l10n.confirm),
                            ),
                          ],
                        ),
                      );

                      if (confirmed == true) {
                        await ref.read(packageDetailsProvider(packageName).notifier).unpublishVersion(version.version);
                      }
                    } else if (value == 'deprecate') {
                      final message = await showDialog<String>(
                        context: context,
                        builder: (context) => AlertDialog(
                          title: Text(l10n.deprecate),
                          content: TextField(
                            decoration: InputDecoration(
                              hintText: l10n.deprecateMessagePlaceholder,
                              labelText: l10n.deprecateMessage,
                            ),
                            maxLines: 3,
                          ),
                          actions: [
                            TextButton(
                              onPressed: () => Navigator.pop(context),
                              child: Text(l10n.cancel),
                            ),
                            TextButton(
                              onPressed: () => Navigator.pop(
                                context,
                                (context as Element).findAncestorStateOfType<FormFieldState<String>>()?.value,
                              ),
                              child: Text(l10n.confirm),
                            ),
                          ],
                        ),
                      );

                      if (message != null && message.isNotEmpty) {
                        await ref
                            .read(packageDetailsProvider(packageName).notifier)
                            .deprecateVersion(version.version, message);
                      }
                    }
                  },
                ),
              ),
            );
          },
        ),
      ],
    );
  }

  Widget _buildDependenciesList({
    required Map<String, String> dependencies,
    required ThemeData theme,
    required AppLocalizations l10n,
  }) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          l10n.dependencies,
          style: theme.textTheme.titleMedium,
        ),
        const SizedBox(height: 8.0),
        if (dependencies.isEmpty)
          Text(
            l10n.noDependencies,
            style: theme.textTheme.bodyMedium,
          )
        else
          ListView.builder(
            shrinkWrap: true,
            physics: const NeverScrollableScrollPhysics(),
            itemCount: dependencies.length,
            itemBuilder: (context, index) {
              final entry = dependencies.entries.elementAt(index);
              return Card(
                child: ListTile(
                  title: Text(entry.key),
                  subtitle: Text(entry.value),
                ),
              );
            },
          ),
      ],
    );
  }
}
