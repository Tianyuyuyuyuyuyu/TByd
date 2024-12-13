import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/package_provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package:flutter_svg/flutter_svg.dart';
import '../widgets/package_settings_dialog.dart';
import 'package:url_launcher/url_launcher.dart';

class PackageDetailsPage extends ConsumerWidget {
  final String packageName;

  const PackageDetailsPage({
    super.key,
    required this.packageName,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final theme = Theme.of(context);
    final l10n = AppLocalizations.of(context);
    final packageDetails = ref.watch(packageDetailsProvider(packageName));

    return DefaultTabController(
      length: 5,
      child: Column(
        mainAxisSize: MainAxisSize.min,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // 包标题和版本信息
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                if (packageDetails.package != null) ...[
                  // 显示包名
                  Text(
                    packageName,
                    style: theme.textTheme.titleMedium?.copyWith(
                      color: theme.colorScheme.onSurfaceVariant,
                    ),
                  ),
                  const SizedBox(height: 4),
                  // 显示 displayName
                  Text(
                    packageDetails.package!.displayName,
                    style: theme.textTheme.headlineMedium,
                  ),
                  const SizedBox(height: 8),
                  Text(
                    packageDetails.package!.description,
                    style: theme.textTheme.bodyLarge,
                  ),
                  const SizedBox(height: 8),
                  Text(
                    'Latest v${packageDetails.package!.version} · Published ${_formatDate(packageDetails.package!.publishedAt)}',
                    style: theme.textTheme.bodyMedium?.copyWith(
                      color: theme.colorScheme.onSurfaceVariant,
                    ),
                  ),
                  const SizedBox(height: 8),
                  // 添加仓库链接区域
                  if (packageDetails.package?.repository != null)
                    Container(
                      padding: const EdgeInsets.symmetric(vertical: 8),
                      decoration: BoxDecoration(
                        border: Border(
                          bottom: BorderSide(
                            color: theme.dividerColor.withOpacity(0.1),
                          ),
                        ),
                      ),
                      child: Row(
                        crossAxisAlignment: CrossAxisAlignment.center,
                        children: [
                          SizedBox(
                            width: 20,
                            height: 20,
                            child: FutureBuilder<String>(
                              future: rootBundle.loadString('assets/images/git.svg'),
                              builder: (context, snapshot) {
                                if (snapshot.hasError) {
                                  debugPrint('Git SVG 加载失败: ${snapshot.error}');
                                  return const Icon(Icons.code, size: 16);
                                }
                                if (!snapshot.hasData) {
                                  return const SizedBox(
                                    width: 16,
                                    height: 16,
                                    child: CircularProgressIndicator(
                                      strokeWidth: 2,
                                    ),
                                  );
                                }
                                return SvgPicture.string(
                                  snapshot.data!,
                                  width: 16,
                                  height: 16,
                                  fit: BoxFit.contain,
                                );
                              },
                            ),
                          ),
                          const SizedBox(width: 8),
                          Expanded(
                            child: InkWell(
                              onTap: () async {
                                final url = Uri.parse(packageDetails.package!.repository!);
                                if (await canLaunchUrl(url)) {
                                  await launchUrl(url);
                                }
                              },
                              child: Text(
                                packageDetails.package!.repository!,
                                style: theme.textTheme.bodyMedium?.copyWith(
                                  color: theme.colorScheme.primary,
                                  height: 1.2,
                                ),
                                maxLines: 1,
                                overflow: TextOverflow.ellipsis,
                              ),
                            ),
                          ),
                          const SizedBox(width: 4),
                          SizedBox(
                            width: 32,
                            height: 32,
                            child: IconButton(
                              icon: const Icon(Icons.copy, size: 16),
                              onPressed: () {
                                Clipboard.setData(
                                  ClipboardData(
                                    text: packageDetails.package!.repository!,
                                  ),
                                );
                                ScaffoldMessenger.of(context).showSnackBar(
                                  SnackBar(
                                    content: Text(l10n.repositoryUrlCopied),
                                    duration: const Duration(seconds: 2),
                                  ),
                                );
                              },
                              padding: EdgeInsets.zero,
                              constraints: const BoxConstraints(),
                              splashRadius: 16,
                              tooltip: l10n.copyRepositoryUrl,
                            ),
                          ),
                        ],
                      ),
                    ),
                ],
              ],
            ),
          ),
          // 操作按钮
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16.0),
            child: Row(
              children: [
                if (packageDetails.package?.homepage != null)
                  _ActionButton(
                    icon: Icons.home,
                    label: 'Homepage',
                    onPressed: () async {
                      final url = Uri.parse(packageDetails.package!.homepage!);
                      if (await canLaunchUrl(url)) {
                        await launchUrl(url);
                      }
                    },
                  )
                else
                  _ActionButton(
                    icon: Icons.home,
                    label: 'Homepage',
                    onPressed: null,
                  ),
                const SizedBox(width: 8),
                if (packageDetails.package?.bugsUrl != null)
                  _ActionButton(
                    icon: Icons.bug_report,
                    label: 'Issues',
                    onPressed: () async {
                      final url = Uri.parse(packageDetails.package!.bugsUrl!);
                      if (await canLaunchUrl(url)) {
                        await launchUrl(url);
                      }
                    },
                  )
                else
                  _ActionButton(
                    icon: Icons.bug_report,
                    label: 'Issues',
                    onPressed: null,
                  ),
                const SizedBox(width: 8),
                if (packageDetails.package?.dist['tarball'] != null)
                  _ActionButton(
                    icon: Icons.cloud_download,
                    label: 'Download',
                    onPressed: () async {
                      final url = Uri.parse(packageDetails.package!.dist['tarball']!);
                      if (await canLaunchUrl(url)) {
                        await launchUrl(
                          url,
                          mode: LaunchMode.externalApplication,
                        );
                      }
                    },
                  )
                else
                  _ActionButton(
                    icon: Icons.cloud_download,
                    label: 'Download',
                    onPressed: null,
                  ),
                const SizedBox(width: 8),
                _ActionButton(
                  icon: Icons.code,
                  label: 'Repository',
                  onPressed: () {},
                ),
              ],
            ),
          ),
          const SizedBox(height: 8),
          // 标签栏
          TabBar(
            tabs: [
              Tab(text: l10n.readmeTab),
              Tab(text: l10n.dependenciesTab),
              Tab(text: l10n.versionsTab),
              Tab(text: l10n.uplinksTab),
              Tab(text: l10n.packageInstallation),
            ],
          ),
          Expanded(
            child: TabBarView(
              children: [
                _ReadmeTab(packageDetails: packageDetails),
                _DependenciesTab(packageDetails: packageDetails),
                _VersionsTab(packageDetails: packageDetails),
                const _UplinksTab(),
                if (packageDetails.package != null)
                  InstallationSection(
                    packageName: packageDetails.package!.name,
                    version: packageDetails.package!.version,
                  )
                else
                  const Center(child: CircularProgressIndicator()),
              ],
            ),
          ),
        ],
      ),
    );
  }

  String _formatDate(DateTime date) {
    final now = DateTime.now();
    final difference = now.difference(date);

    if (difference.inDays < 1) {
      return 'today';
    }

    if (difference.inDays < 30) {
      return '${difference.inDays} days ago';
    }

    if (difference.inDays < 365) {
      final months = (difference.inDays / 30).floor();
      return '$months months ago';
    }

    final years = (difference.inDays / 365).floor();
    return '$years years ago';
  }
}

class _ActionButton extends StatelessWidget {
  final IconData icon;
  final String label;
  final VoidCallback? onPressed;

  const _ActionButton({
    required this.icon,
    required this.label,
    required this.onPressed,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return ElevatedButton.icon(
      icon: Icon(icon, size: 16),
      label: Text(label),
      onPressed: onPressed,
      style: ElevatedButton.styleFrom(
        padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 4),
        minimumSize: const Size(0, 32),
        tapTargetSize: MaterialTapTargetSize.shrinkWrap,
        disabledBackgroundColor: theme.colorScheme.surfaceVariant.withOpacity(0.5),
        disabledForegroundColor: theme.colorScheme.onSurfaceVariant.withOpacity(0.5),
      ),
    );
  }
}

// 标签页内容组件
class _ReadmeTab extends StatelessWidget {
  final PackageDetailsState packageDetails;

  const _ReadmeTab({required this.packageDetails});

  @override
  Widget build(BuildContext context) {
    if (packageDetails.isLoading) {
      return const Center(child: CircularProgressIndicator());
    }

    if (packageDetails.error != null) {
      return Center(child: Text(packageDetails.error!));
    }

    return SingleChildScrollView(
      padding: const EdgeInsets.all(16),
      child: Text(packageDetails.package?.description ?? ''),
    );
  }
}

class _DependenciesTab extends StatelessWidget {
  final PackageDetailsState packageDetails;

  const _DependenciesTab({required this.packageDetails});

  @override
  Widget build(BuildContext context) {
    if (packageDetails.package == null) {
      return const Center(child: Text('No dependencies information available'));
    }

    return ListView.builder(
      padding: const EdgeInsets.all(16),
      itemCount: packageDetails.package!.dependencies.length,
      itemBuilder: (context, index) {
        final entry = packageDetails.package!.dependencies.entries.elementAt(index);
        return ListTile(
          title: Text(entry.key),
          subtitle: Text(entry.value),
        );
      },
    );
  }
}

class _VersionsTab extends StatelessWidget {
  final PackageDetailsState packageDetails;

  const _VersionsTab({required this.packageDetails});

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      padding: const EdgeInsets.all(16),
      itemCount: packageDetails.versions.length,
      itemBuilder: (context, index) {
        final version = packageDetails.versions[index];
        return ListTile(
          title: Text('v${version.version}'),
          subtitle: Text(_formatDate(version.publishedAt)),
        );
      },
    );
  }

  String _formatDate(DateTime date) {
    return '${date.year}-${date.month.toString().padLeft(2, '0')}-${date.day.toString().padLeft(2, '0')}';
  }
}

class _UplinksTab extends StatelessWidget {
  const _UplinksTab();

  @override
  Widget build(BuildContext context) {
    return const Center(child: Text('No uplinks information available'));
  }
}

class _InstallCommand extends ConsumerWidget {
  final String icon;
  final String label;
  final String packageName;
  final String version;

  const _InstallCommand({
    required this.icon,
    required this.label,
    required this.packageName,
    required this.version,
  });

  String _getCommand(bool isGlobalPackage, bool isYarnModernSyntax) {
    final fullPackageName = '$packageName@$version';

    switch (label.toLowerCase()) {
      case 'npm':
        return 'npm install ${isGlobalPackage ? '-g ' : ''}$fullPackageName';
      case 'yarn':
        if (isGlobalPackage) {
          return isYarnModernSyntax ? 'yarn global add $fullPackageName' : 'yarn add $fullPackageName';
        }
        return 'yarn add $fullPackageName';
      case 'pnpm':
        return 'pnpm install ${isGlobalPackage ? '-g ' : ''}$fullPackageName';
      default:
        return '';
    }
  }

  Widget _buildIcon() {
    return SizedBox(
      width: 24,
      height: 24,
      child: FutureBuilder<String>(
        future: rootBundle.loadString('assets/images/$icon.svg'),
        builder: (context, snapshot) {
          if (snapshot.hasError) {
            debugPrint('SVG 加载失败: assets/images/$icon.svg - ${snapshot.error}');
            return Icon(
              icon == 'npm'
                  ? Icons.archive_outlined
                  : icon == 'yarn'
                      ? Icons.all_inclusive
                      : Icons.extension,
              size: 20,
              color: Colors.grey[600],
            );
          }

          if (!snapshot.hasData) {
            return SizedBox(
              width: 20,
              height: 20,
              child: CircularProgressIndicator(
                strokeWidth: 2,
                color: Colors.grey[600],
              ),
            );
          }

          return SvgPicture.string(
            snapshot.data!,
            width: 20,
            height: 20,
          );
        },
      ),
    );
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final theme = Theme.of(context);
    final isGlobalPackage = ref.watch(globalPackageProvider);
    final isYarnModernSyntax = ref.watch(yarnModernSyntaxProvider);

    final command = _getCommand(isGlobalPackage, isYarnModernSyntax);

    return Container(
      padding: const EdgeInsets.symmetric(vertical: 12, horizontal: 16),
      child: Row(
        children: [
          _buildIcon(),
          const SizedBox(width: 16),
          Expanded(
            child: Text(
              command,
              style: theme.textTheme.bodyMedium?.copyWith(
                fontFamily: 'Monospace',
              ),
            ),
          ),
          IconButton(
            icon: const Icon(Icons.copy, size: 20),
            onPressed: () {
              Clipboard.setData(ClipboardData(text: command));
              ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(
                  content: Text('$label command copied to clipboard'),
                  duration: const Duration(seconds: 2),
                ),
              );
            },
            tooltip: 'Copy to clipboard',
            padding: const EdgeInsets.all(8),
            constraints: const BoxConstraints(),
          ),
        ],
      ),
    );
  }
}

class InstallationSection extends ConsumerWidget {
  final String packageName;
  final String version;

  const InstallationSection({
    super.key,
    required this.packageName,
    required this.version,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final l10n = AppLocalizations.of(context);

    return SingleChildScrollView(
      child: Container(
        width: double.infinity,
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              mainAxisAlignment: MainAxisAlignment.end,
              children: [
                PopupMenuButton<void>(
                  icon: const Icon(Icons.settings, size: 20),
                  tooltip: l10n.settings,
                  position: PopupMenuPosition.under,
                  offset: const Offset(0, 4),
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(4),
                  ),
                  elevation: 8,
                  surfaceTintColor: Colors.transparent,
                  itemBuilder: (context) => [
                    PopupMenuItem<void>(
                      padding: EdgeInsets.zero,
                      child: PackageSettingsDialog(
                        packageName: packageName,
                      ),
                    ),
                  ],
                ),
              ],
            ),
            const SizedBox(height: 16),
            _InstallCommand(
              icon: 'npm',
              label: 'npm',
              packageName: packageName,
              version: version,
            ),
            const Divider(height: 1),
            _InstallCommand(
              icon: 'yarn',
              label: 'yarn',
              packageName: packageName,
              version: version,
            ),
            const Divider(height: 1),
            _InstallCommand(
              icon: 'pnpm',
              label: 'pnpm',
              packageName: packageName,
              version: version,
            ),
          ],
        ),
      ),
    );
  }
}
