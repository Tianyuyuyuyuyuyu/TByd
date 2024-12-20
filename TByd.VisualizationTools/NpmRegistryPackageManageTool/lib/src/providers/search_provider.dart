import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

/// 搜索状态类
class SearchState {
  /// 搜索文本
  final String searchText;

  /// 搜索框控制器
  final TextEditingController controller;

  /// 是否正在搜索
  final bool isSearching;

  const SearchState({
    required this.searchText,
    required this.controller,
    this.isSearching = false,
  });

  SearchState copyWith({
    String? searchText,
    bool? isSearching,
  }) {
    if (searchText != null && searchText != controller.text) {
      controller.text = searchText;
    }
    return SearchState(
      searchText: searchText ?? this.searchText,
      controller: controller,
      isSearching: isSearching ?? this.isSearching,
    );
  }
}

/// 搜索状态管理器
class SearchNotifier extends StateNotifier<SearchState> {
  SearchNotifier()
      : super(SearchState(
          searchText: '',
          controller: TextEditingController(),
          isSearching: false,
        ));

  @override
  void dispose() {
    state.controller.dispose();
    super.dispose();
  }

  void updateSearchText(String text) {
    state = state.copyWith(
      searchText: text,
      isSearching: text.isNotEmpty,
    );
  }

  void clear() {
    state.controller.clear();
    updateSearchText('');
  }

  void setSearching(bool isSearching) {
    state = state.copyWith(isSearching: isSearching);
  }
}

/// 搜索状态提供者
final searchProvider = StateProvider<String>((ref) => '');
