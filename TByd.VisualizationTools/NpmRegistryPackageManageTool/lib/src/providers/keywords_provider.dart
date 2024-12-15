/// NPM Registry Manager - Keywords 管理
///
/// 该文件负责管理所有包的 keywords，包括：
/// - 收集和存储所有包的 keywords
/// - 提供 keywords 的搜索和过滤功能
/// - 管理 keywords 的使用频率统计
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/package_service.dart';
import '../providers/auth_provider.dart';

/// Keywords 状态类
///
/// 存储和管理所有 keywords 相关的状态信息
class KeywordsState {
  /// 所有已知的 keywords
  final Set<String> allKeywords;

  /// 按使用频率排序的 keywords
  final Map<String, int> keywordFrequency;

  /// 是否正在加载
  final bool isLoading;

  /// 错误信息
  final String? error;

  const KeywordsState({
    this.allKeywords = const {},
    this.keywordFrequency = const {},
    this.isLoading = false,
    this.error,
  });

  /// 创建状态副本
  KeywordsState copyWith({
    Set<String>? allKeywords,
    Map<String, int>? keywordFrequency,
    bool? isLoading,
    String? error,
  }) {
    return KeywordsState(
      allKeywords: allKeywords ?? this.allKeywords,
      keywordFrequency: keywordFrequency ?? this.keywordFrequency,
      isLoading: isLoading ?? this.isLoading,
      error: error,
    );
  }
}

/// Keywords 状态管理器
class KeywordsNotifier extends StateNotifier<KeywordsState> {
  final PackageService _packageService;
  bool _mounted = true;

  KeywordsNotifier(this._packageService) : super(const KeywordsState()) {
    loadKeywords();
  }

  /// 加载所有包的 keywords
  Future<void> loadKeywords() async {
    if (!_mounted) return;
    state = state.copyWith(isLoading: true);

    try {
      final packages = await _packageService.searchPackages('');
      if (!_mounted) return;

      final allKeywords = <String>{};
      final frequency = <String, int>{};

      // 收集所有包的 keywords
      for (final package in packages) {
        if (!_mounted) return;

        // 从搜索结果中收集 keywords
        for (final keyword in package.keywords) {
          allKeywords.add(keyword);
          frequency[keyword] = (frequency[keyword] ?? 0) + 1;
        }

        // 从包详情中收集额外的 keywords
        try {
          final packageDetails = await _packageService.getPackageDetails(package.name);
          if (!_mounted) return;

          for (final keyword in packageDetails.keywords) {
            allKeywords.add(keyword);
            frequency[keyword] = (frequency[keyword] ?? 0) + 1;
          }
        } catch (e) {
          print('获取包详情失败: ${package.name}, 错误: $e');
          // 继续处理下一个包
          continue;
        }
      }

      if (!_mounted) return;
      state = KeywordsState(
        allKeywords: allKeywords,
        keywordFrequency: frequency,
      );
    } catch (e) {
      if (!_mounted) return;
      state = KeywordsState(error: e.toString());
    }
  }

  /// 获取推荐的 keywords
  ///
  /// 基于使用频率返回推荐的 keywords
  /// [limit] - 返回的关键词数量限制
  List<String> getRecommendedKeywords([int limit = 10]) {
    if (!_mounted) return [];
    final sortedKeywords = state.keywordFrequency.entries.toList()..sort((a, b) => b.value.compareTo(a.value));

    return sortedKeywords.take(limit).map((e) => e.key).toList();
  }

  /// 搜索 keywords
  ///
  /// 根据输入的文本搜索匹配的 keywords
  /// [query] - 搜索文本
  List<String> searchKeywords(String query) {
    if (!_mounted) return [];
    final searchText = query.toLowerCase();
    return state.allKeywords.where((keyword) => keyword.toLowerCase().contains(searchText)).toList()..sort();
  }

  /// 更新关键字列表
  ///
  /// 直接��加新的关键字到列表中
  /// [keywords] - 要添加的关键字列表
  void addKeywords(Iterable<String> keywords) {
    if (!_mounted) return;

    final newKeywords = Set<String>.from(state.allKeywords)..addAll(keywords);
    final newFrequency = Map<String, int>.from(state.keywordFrequency);

    for (final keyword in keywords) {
      newFrequency[keyword] = (newFrequency[keyword] ?? 0) + 1;
    }

    state = KeywordsState(
      allKeywords: newKeywords,
      keywordFrequency: newFrequency,
    );
  }

  @override
  void dispose() {
    _mounted = false;
    super.dispose();
  }
}

/// Keywords 状态提供者
final keywordsProvider = StateNotifierProvider<KeywordsNotifier, KeywordsState>((ref) {
  final authState = ref.watch(authProvider);
  final packageService = PackageService(
    serverUrl: authState.auth?.serverUrl ?? '',
    token: authState.auth?.token,
  );
  return KeywordsNotifier(packageService);
});
