/// NPM Registry Manager - 包搜索页面
///
/// 该文件实现了包搜索的界面，包括：
/// - 搜索输入框
/// - 搜索结果列表
/// - 包详情展示
/// - 防抖处理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/package_provider.dart';
import '../models/package_model.dart';
import 'package:intl/intl.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package_details_page.dart';

/// 包搜索页面组件
///
/// 提供包搜索功能，包括：
/// - 实时搜索
/// - 结果展示
/// - 包详情导航
class PackageSearchPage extends ConsumerStatefulWidget {
  const PackageSearchPage({super.key});

  @override
  ConsumerState<PackageSearchPage> createState() => _PackageSearchPageState();
}

/// 包搜索页面状态类
///
/// 管理搜索页面的状态和行为，包括：
/// - 搜索输入处理
/// - 结果展示
/// - 防抖控制
class _PackageSearchPageState extends ConsumerState<PackageSearchPage> {
  /// 搜索输���控制器
  final _searchController = TextEditingController();

  /// 防抖控制器
  final _debouncer = Debouncer(milliseconds: 300);

  /// 当前搜索关键词
  String _currentQuery = '';

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

  /// 处理搜索文本变化
  ///
  /// 使用防抖处理避免频繁搜索
  /// [query] 搜索关键词
  void _onSearchChanged(String query) {
    if (!mounted) return;
    _currentQuery = query;
    _debouncer.run(() {
      if (!mounted) return;
      final notifier = ref.read(searchProvider.notifier);
      notifier.search(query);
    });
  }

  @override
  Widget build(BuildContext context) {
    final searchState = ref.watch(searchProvider);
    final l10n = AppLocalizations.of(context);
    final theme = Theme.of(context);

    return Column(
      children: [
        // 搜索输入框
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
        // 搜索结果展示
        Expanded(
          child: searchState.when(
            data: (packages) {
              if (packages.isEmpty && _currentQuery.isNotEmpty) {
                return Center(
                  child: Text(l10n.noPackagesFound),
                );
              }
              return ListView.builder(
                itemCount: packages.length,
                itemBuilder: (context, index) {
                  final package = packages[index];
                  return PackageListItem(package: package);
                },
              );
            },
            loading: () => const Center(
              child: CircularProgressIndicator(),
            ),
            error: (error, stackTrace) => Center(
              child: Text(
                error.toString(),
                style: TextStyle(color: theme.colorScheme.error),
              ),
            ),
          ),
        ),
      ],
    );
  }
}

/// 包列表项组件
///
/// 展示单个包的基本信息，包括：
/// - 包名和版本
/// - 描述信息
/// - 作者和更新时间
class PackageListItem extends StatelessWidget {
  /// 包信息
  final PackageSearchResult package;

  /// 构造函数
  ///
  /// [package] 要显示的包信息
  /// [key] Widget的键
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
              // 包名和版本
              Row(
                children: [
                  Expanded(
                    child: Text(
                      package.displayName ?? package.name,
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
              // 包描述
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
              // 作者和更新时间
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

/// 防抖控制器类
///
/// 用于控制搜索输入的防抖处理，避免频繁请求
class Debouncer {
  /// 防抖延迟时间（毫秒）
  final int milliseconds;

  /// 定时器
  Timer? _timer;

  /// 构造函数
  ///
  /// [milliseconds] 防抖延迟时间
  Debouncer({required this.milliseconds});

  /// 执���防抖操作
  ///
  /// [action] 要执行的操作
  void run(VoidCallback action) {
    _timer?.cancel();
    _timer = Timer(Duration(milliseconds: milliseconds), action);
  }
}
