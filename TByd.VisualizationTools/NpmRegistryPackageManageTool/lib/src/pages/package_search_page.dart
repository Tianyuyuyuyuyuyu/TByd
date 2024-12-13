import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/package_provider.dart';
import '../models/package_model.dart';
import 'package:intl/intl.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package_details_page.dart';

class PackageSearchPage extends ConsumerStatefulWidget {
  const PackageSearchPage({super.key});

  @override
  ConsumerState<PackageSearchPage> createState() => _PackageSearchPageState();
}

class _PackageSearchPageState extends ConsumerState<PackageSearchPage> {
  final _searchController = TextEditingController();
  final _debouncer = Debouncer(milliseconds: 300);

  @override
  void initState() {
    super.initState();
    _onSearchChanged('');
  }

  @override
  void dispose() {
    _searchController.dispose();
    _debouncer._timer?.cancel();
    super.dispose();
  }

  void _onSearchChanged(String query) {
    if (!mounted) return;
    _debouncer.run(() {
      if (!mounted) return;
      final notifier = ref.read(searchProvider.notifier);
      if (notifier.mounted) {
        notifier.search(query);
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    final searchState = ref.watch(searchProvider);
    final l10n = AppLocalizations.of(context);
    final theme = Theme.of(context);

    return Column(
      children: [
        Padding(
          padding: const EdgeInsets.all(16.0),
          child: TextField(
            controller: _searchController,
            decoration: InputDecoration(
              hintText: l10n.searchPlaceholder,
              prefixIcon: const Icon(Icons.search),
              suffixIcon: _searchController.text.isNotEmpty
                  ? IconButton(
                      icon: const Icon(Icons.clear),
                      onPressed: () {
                        _searchController.clear();
                        _onSearchChanged('');
                      },
                    )
                  : null,
              border: const OutlineInputBorder(),
              filled: true,
              fillColor: theme.colorScheme.surface,
            ),
            onChanged: _onSearchChanged,
          ),
        ),
        if (searchState.error != null)
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Text(
              searchState.error!,
              style: TextStyle(color: theme.colorScheme.error),
            ),
          )
        else if (searchState.isLoading && searchState.results.isEmpty)
          const Expanded(
            child: Center(
              child: CircularProgressIndicator(),
            ),
          )
        else if (searchState.results.isEmpty && searchState.query.isNotEmpty)
          Expanded(
            child: Center(
              child: Text(l10n.noPackagesFound),
            ),
          )
        else
          Expanded(
            child: ListView.builder(
              itemCount: searchState.results.length,
              itemBuilder: (context, index) {
                final package = searchState.results[index];
                return ListTile(
                  title: Text(package.name),
                  subtitle: Text(package.description),
                  trailing: Text(package.version),
                  onTap: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => PackageDetailsPage(
                          packageName: package.name,
                        ),
                      ),
                    );
                  },
                );
              },
            ),
          ),
      ],
    );
  }
}

class PackageListItem extends StatelessWidget {
  final PackageSearchResult package;

  const PackageListItem({
    super.key,
    required this.package,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final dateFormat = DateFormat('yyyy-MM-dd HH:mm');

    return Card(
      margin: const EdgeInsets.symmetric(horizontal: 16.0, vertical: 8.0),
      child: InkWell(
        onTap: () {
          Navigator.push(
            context,
            MaterialPageRoute(
              builder: (context) => PackageDetailsPage(
                packageName: package.name,
              ),
            ),
          );
        },
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Row(
                children: [
                  Expanded(
                    child: Text(
                      package.name,
                      style: theme.textTheme.titleLarge?.copyWith(
                        color: theme.colorScheme.primary,
                      ),
                    ),
                  ),
                  Container(
                    padding: const EdgeInsets.symmetric(
                      horizontal: 8.0,
                      vertical: 4.0,
                    ),
                    decoration: BoxDecoration(
                      color: theme.colorScheme.primaryContainer,
                      borderRadius: BorderRadius.circular(4.0),
                    ),
                    child: Text(
                      'v${package.version}',
                      style: TextStyle(
                        color: theme.colorScheme.onPrimaryContainer,
                        fontSize: 12.0,
                      ),
                    ),
                  ),
                ],
              ),
              if (package.description.isNotEmpty) ...[
                const SizedBox(height: 8.0),
                Text(
                  package.description,
                  style: theme.textTheme.bodyMedium,
                  maxLines: 2,
                  overflow: TextOverflow.ellipsis,
                ),
              ],
              const SizedBox(height: 8.0),
              DefaultTextStyle(
                style: theme.textTheme.bodySmall ?? const TextStyle(),
                child: Row(
                  children: [
                    Icon(
                      Icons.person_outline,
                      size: 16.0,
                      color: theme.textTheme.bodySmall?.color,
                    ),
                    const SizedBox(width: 4.0),
                    Text(package.author),
                    const SizedBox(width: 16.0),
                    Icon(
                      Icons.access_time,
                      size: 16.0,
                      color: theme.textTheme.bodySmall?.color,
                    ),
                    const SizedBox(width: 4.0),
                    Text(dateFormat.format(package.lastModified)),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class Debouncer {
  final int milliseconds;
  Timer? _timer;

  Debouncer({required this.milliseconds});

  void run(VoidCallback action) {
    _timer?.cancel();
    _timer = Timer(Duration(milliseconds: milliseconds), action);
  }
}
