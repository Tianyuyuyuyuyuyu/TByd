/// NPM Registry Manager - Category 管理
///
/// 该文件负责管理所有包的 category，包括：
/// - 收集和存储所有包的 category
/// - 提供 category 的搜索和过滤功能
/// - 管理 category 的使用频率统计
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/package_service.dart';
import '../providers/auth_provider.dart';

/// Category 状态类
///
/// 存储和管理所有 category 相关的状态信息
class CategoryState {
  /// 所有已知的 category
  final Set<String> allCategories;

  /// 按使用频率排序的 category
  final Map<String, int> categoryFrequency;

  /// 是否正在加载
  final bool isLoading;

  /// 错误信息
  final String? error;

  const CategoryState({
    this.allCategories = const {},
    this.categoryFrequency = const {},
    this.isLoading = false,
    this.error,
  });

  /// 创建状态副本
  CategoryState copyWith({
    Set<String>? allCategories,
    Map<String, int>? categoryFrequency,
    bool? isLoading,
    String? error,
  }) {
    return CategoryState(
      allCategories: allCategories ?? this.allCategories,
      categoryFrequency: categoryFrequency ?? this.categoryFrequency,
      isLoading: isLoading ?? this.isLoading,
      error: error,
    );
  }
}

/// Category 状态管理器
class CategoryNotifier extends StateNotifier<CategoryState> {
  final PackageService _packageService;
  bool _mounted = true;

  CategoryNotifier(this._packageService) : super(const CategoryState()) {
    loadCategories();
  }

  /// 加载所有包的 category
  Future<void> loadCategories() async {
    if (!_mounted) return;
    state = state.copyWith(isLoading: true);

    try {
      final packages = await _packageService.searchPackages('');
      if (!_mounted) return;

      final allCategories = <String>{};
      final frequency = <String, int>{};

      // 收集所有包的 category
      for (final package in packages) {
        if (!_mounted) return;

        try {
          final packageDetails = await _packageService.getPackageDetails(package.name);
          if (!_mounted) return;

          if (packageDetails.category != null && packageDetails.category!.isNotEmpty) {
            allCategories.add(packageDetails.category!);
            frequency[packageDetails.category!] = (frequency[packageDetails.category!] ?? 0) + 1;
          }
        } catch (e) {
          // 忽略单个包的详情获取错误
          print('Error fetching details for ${package.name}: $e');
        }
      }

      if (!_mounted) return;
      state = state.copyWith(
        allCategories: allCategories,
        categoryFrequency: frequency,
        isLoading: false,
      );
    } catch (e) {
      if (!_mounted) return;
      state = state.copyWith(
        isLoading: false,
        error: e.toString(),
      );
    }
  }

  /// 搜索分类
  ///
  /// 根据搜索文本过滤分类列表
  /// [searchText] - 搜索文本
  List<String> searchCategories(String searchText) {
    if (searchText.isEmpty) {
      return state.allCategories.toList()..sort();
    }
    return state.allCategories.where((category) => category.toLowerCase().contains(searchText)).toList()..sort();
  }

  /// 更新分类列表
  ///
  /// 直接添加新的分类到列表中
  /// [category] - 要添加的分类
  void addCategory(String category) {
    if (!_mounted || category.isEmpty) return;

    final newCategories = Set<String>.from(state.allCategories)..add(category);
    final newFrequency = Map<String, int>.from(state.categoryFrequency);
    newFrequency[category] = (newFrequency[category] ?? 0) + 1;

    state = CategoryState(
      allCategories: newCategories,
      categoryFrequency: newFrequency,
    );
  }

  @override
  void dispose() {
    _mounted = false;
    super.dispose();
  }
}

/// Category 状态提供者
final categoryProvider = StateNotifierProvider<CategoryNotifier, CategoryState>((ref) {
  final authState = ref.watch(authProvider);
  final packageService = PackageService(
    serverUrl: authState.auth?.serverUrl ?? '',
    token: authState.auth?.token,
  );
  return CategoryNotifier(packageService);
});
