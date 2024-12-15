import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/package_provider.dart';
import '../models/package_model.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package_details_page.dart';
import '../widgets/package_search_box.dart';

class PackageListPage extends ConsumerStatefulWidget {
  const PackageListPage({super.key});

  @override
  ConsumerState<PackageListPage> createState() => _PackageListPageState();
}

class _PackageListPageState extends ConsumerState<PackageListPage> {
  PackageSearchResult? _selectedPackage;
  final ScrollController _scrollController = ScrollController();

  @override
  void initState() {
    super.initState();
    // 不需要手动加载包，SearchNotifier 会在初始化时自动加载
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  Future<void> _refreshPackages() async {
    await ref.read(searchProvider.notifier).refresh();
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final l10n = AppLocalizations.of(context);
    final searchState = ref.watch(searchProvider);

    return Row(
      children: [
        // 左侧包列表
        Container(
          width: 300,
          decoration: BoxDecoration(
            border: Border(
              right: BorderSide(
                color: theme.colorScheme.outlineVariant,
                width: 1,
              ),
            ),
          ),
          child: Column(
            children: [
              // 搜索框
              Padding(
                padding: const EdgeInsets.all(16.0),
                child: PackageSearchBox(
                  onSearch: (value) {
                    ref.read(searchProvider.notifier).search(value);
                  },
                  hintText: l10n.searchPlaceholder,
                ),
              ),
              // 包列表
              Expanded(
                child: searchState.when(
                  loading: () => const Center(
                    child: CircularProgressIndicator(),
                  ),
                  error: (error, _) => Center(
                    child: Text(
                      error.toString(),
                      style: TextStyle(color: theme.colorScheme.error),
                    ),
                  ),
                  data: (packages) => packages.isEmpty
                      ? Center(
                          child: Text(
                            l10n.noPackagesFound,
                            style: theme.textTheme.bodyLarge,
                          ),
                        )
                      : Scrollbar(
                          controller: _scrollController,
                          thumbVisibility: true,
                          child: ListView.builder(
                            controller: _scrollController,
                            itemCount: packages.length,
                            physics: const AlwaysScrollableScrollPhysics(),
                            itemBuilder: (context, index) {
                              final package = packages[index];
                              final isSelected = package == _selectedPackage;

                              return ListTile(
                                contentPadding: const EdgeInsets.symmetric(
                                  horizontal: 16.0,
                                  vertical: 8.0,
                                ),
                                title: Text(
                                  package.displayName,
                                  maxLines: 1,
                                  overflow: TextOverflow.ellipsis,
                                ),
                                subtitle: Text(
                                  package.description ?? '',
                                  maxLines: 2,
                                  overflow: TextOverflow.ellipsis,
                                ),
                                selected: isSelected,
                                selectedTileColor: theme.colorScheme.primaryContainer,
                                onTap: () {
                                  setState(() {
                                    _selectedPackage = package;
                                  });
                                },
                              );
                            },
                          ),
                        ),
                ),
              ),
            ],
          ),
        ),
        // 右侧详情页
        Expanded(
          child: _selectedPackage == null
              ? Center(
                  child: Text(
                    l10n.noPackagesFound,
                    style: theme.textTheme.bodyLarge,
                  ),
                )
              : PackageDetailsPage(packageName: _selectedPackage!.name),
        ),
      ],
    );
  }
}
