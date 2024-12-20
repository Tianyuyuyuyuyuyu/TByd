import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/package_provider.dart';
import '../providers/search_provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package_details_page.dart';
import '../widgets/search_box.dart';

class PackageListPage extends ConsumerStatefulWidget {
  const PackageListPage({super.key});

  @override
  ConsumerState<PackageListPage> createState() => _PackageListPageState();
}

class _PackageListPageState extends ConsumerState<PackageListPage> {
  @override
  void initState() {
    super.initState();
    // 初始化时刷新包列表
    Future.microtask(() {
      ref.read(packageProvider.notifier).refreshPackages();
    });
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final packageState = ref.watch(packageProvider);
    final searchText = ref.watch(searchProvider);

    // 过滤包列表
    final filteredPackages = packageState.packages.where((package) {
      if (searchText.isEmpty) return true;
      return package.name.toLowerCase().contains(searchText.toLowerCase());
    }).toList();

    return Column(
      children: [
        // 搜索框
        Padding(
          padding: const EdgeInsets.all(16.0),
          child: SearchBox(
            hintText: '搜索包',
          ),
        ),
        // 包列表
        Expanded(
          child: Builder(
            builder: (context) {
              if (packageState.isLoading) {
                return const Center(child: CircularProgressIndicator());
              }

              if (packageState.error != null) {
                return Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        '加载失败',
                        style: theme.textTheme.titleLarge,
                      ),
                      const SizedBox(height: 8),
                      Text(packageState.error!),
                      const SizedBox(height: 16),
                      FilledButton.icon(
                        onPressed: () {
                          ref.read(packageProvider.notifier).refreshPackages();
                        },
                        icon: const Icon(Icons.refresh),
                        label: const Text('重试'),
                      ),
                    ],
                  ),
                );
              }

              if (filteredPackages.isEmpty) {
                return Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        searchText.isEmpty ? '暂无包' : '未找到匹配的包',
                        style: theme.textTheme.titleLarge,
                      ),
                      const SizedBox(height: 16),
                      if (searchText.isNotEmpty)
                        FilledButton.icon(
                          onPressed: () {
                            ref.read(searchProvider.notifier).state = '';
                          },
                          icon: const Icon(Icons.clear),
                          label: const Text('清除搜索'),
                        ),
                    ],
                  ),
                );
              }

              return ListView.builder(
                itemCount: filteredPackages.length,
                itemBuilder: (context, index) {
                  final package = filteredPackages[index];
                  final isSelected = package.name == packageState.selectedPackageName;

                  return Card(
                    margin: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                    child: ListTile(
                      selected: isSelected,
                      selectedTileColor: theme.colorScheme.primaryContainer.withOpacity(0.1),
                      title: Text(
                        package.displayName,
                        style: theme.textTheme.titleMedium?.copyWith(
                          color: theme.colorScheme.primary,
                        ),
                      ),
                      subtitle: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          if (package.description.isNotEmpty) ...[
                            const SizedBox(height: 4),
                            Text(package.description),
                          ],
                          const SizedBox(height: 4),
                          Row(
                            children: [
                              Container(
                                padding: const EdgeInsets.symmetric(
                                  horizontal: 8,
                                  vertical: 2,
                                ),
                                decoration: BoxDecoration(
                                  color: theme.colorScheme.primaryContainer,
                                  borderRadius: BorderRadius.circular(12),
                                ),
                                child: Text(
                                  'v${package.version}',
                                  style: theme.textTheme.labelSmall?.copyWith(
                                    color: theme.colorScheme.onPrimaryContainer,
                                  ),
                                ),
                              ),
                              const SizedBox(width: 8),
                              Text(
                                package.author,
                                style: theme.textTheme.bodySmall?.copyWith(
                                  color: theme.colorScheme.onSurfaceVariant,
                                ),
                              ),
                              if (package.license != null && package.license!.isNotEmpty) ...[
                                const SizedBox(width: 8),
                                Container(
                                  padding: const EdgeInsets.symmetric(
                                    horizontal: 8,
                                    vertical: 2,
                                  ),
                                  decoration: BoxDecoration(
                                    color: theme.colorScheme.tertiaryContainer,
                                    borderRadius: BorderRadius.circular(12),
                                  ),
                                  child: Text(
                                    package.license!,
                                    style: theme.textTheme.labelSmall?.copyWith(
                                      color: theme.colorScheme.onTertiaryContainer,
                                    ),
                                  ),
                                ),
                              ],
                            ],
                          ),
                        ],
                      ),
                      onTap: () {
                        ref.read(packageProvider.notifier).setSelectedPackage(package.name);
                      },
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
