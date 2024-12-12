import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/package_provider.dart';
import '../models/package_model.dart';
import 'package:intl/intl.dart';

class PackageSearchScreen extends ConsumerStatefulWidget {
  const PackageSearchScreen({super.key});

  @override
  ConsumerState<PackageSearchScreen> createState() => _PackageSearchScreenState();
}

class _PackageSearchScreenState extends ConsumerState<PackageSearchScreen> {
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
    super.dispose();
  }

  void _onSearchChanged(String query) {
    _debouncer.run(() {
      ref.read(searchProvider.notifier).search(query);
    });
  }

  @override
  Widget build(BuildContext context) {
    final searchState = ref.watch(searchProvider);

    return Column(
      children: [
        Padding(
          padding: const EdgeInsets.all(16.0),
          child: TextField(
            controller: _searchController,
            decoration: InputDecoration(
              hintText: '搜索包名...',
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
              fillColor: Theme.of(context).colorScheme.surface,
            ),
            onChanged: _onSearchChanged,
          ),
        ),
        if (searchState.error != null)
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Text(
              searchState.error!,
              style: TextStyle(color: Theme.of(context).colorScheme.error),
            ),
          )
        else if (searchState.isLoading && searchState.results.isEmpty)
          const Expanded(
            child: Center(
              child: CircularProgressIndicator(),
            ),
          )
        else if (searchState.results.isEmpty && searchState.query.isNotEmpty)
          const Expanded(
            child: Center(
              child: Text('未找到相关包'),
            ),
          )
        else
          Expanded(
            child: Stack(
              children: [
                ListView.builder(
                  itemCount: searchState.results.length,
                  itemBuilder: (context, index) {
                    final package = searchState.results[index];
                    return PackageListItem(package: package);
                  },
                ),
                if (searchState.isLoading)
                  const Positioned(
                    top: 8,
                    right: 16,
                    child: CircularProgressIndicator(),
                  ),
              ],
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
          // TODO: Navigate to package details
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
