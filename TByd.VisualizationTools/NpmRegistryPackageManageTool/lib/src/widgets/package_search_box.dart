/// NPM Registry Manager - 包搜索框组件
///
/// 该文件实现了包搜索的输入框组件，包括：
/// - 搜索输入框
/// - 搜索图标
/// - 清除按钮
/// - 搜索回调
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';

/// 包搜索框组件
///
/// 提供包搜索的用户界面，包括：
/// - 搜索文本输入
/// - 实时搜索回调
/// - 一键清除功能
class PackageSearchBox extends StatefulWidget {
  /// 搜索回调函数
  final void Function(String) onSearch;

  /// 提示文本
  final String? hintText;

  /// 构造函数
  ///
  /// [onSearch] 搜索文本变化时的回调函数
  /// [hintText] 搜索框的提示文本
  /// [key] Widget的键
  const PackageSearchBox({
    super.key,
    required this.onSearch,
    this.hintText,
  });

  @override
  State<PackageSearchBox> createState() => _PackageSearchBoxState();
}

/// 包搜索框状态类
///
/// 管理搜索框的状态，包括：
/// - 输入控制器
/// - 清除功能
/// - UI更新
class _PackageSearchBoxState extends State<PackageSearchBox> {
  /// 文本输入控制器
  final TextEditingController _controller = TextEditingController();

  @override
  void dispose() {
    // 释放控制器资源
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 36,
      decoration: BoxDecoration(
        color: Colors.grey[200],
        borderRadius: BorderRadius.circular(4),
      ),
      child: TextField(
        controller: _controller,
        onChanged: widget.onSearch,
        decoration: InputDecoration(
          hintText: widget.hintText ?? '搜索',
          border: InputBorder.none,
          contentPadding: const EdgeInsets.symmetric(
            horizontal: 12,
            vertical: 8,
          ),
          // 搜索图标
          prefixIcon: const Icon(
            Icons.search,
            size: 20,
            color: Colors.grey,
          ),
          // 清除按钮（仅在有输入时显示）
          suffixIcon: _controller.text.isNotEmpty
              ? IconButton(
                  icon: const Icon(
                    Icons.clear,
                    size: 20,
                    color: Colors.grey,
                  ),
                  onPressed: () {
                    // 清除输入并触发搜索回调
                    _controller.clear();
                    widget.onSearch('');
                  },
                )
              : null,
        ),
      ),
    );
  }
}
