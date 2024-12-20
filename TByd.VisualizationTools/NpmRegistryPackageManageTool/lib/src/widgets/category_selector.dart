/// NPM Registry Manager - Category 选择器
///
/// 该文件实现了 category 选择和管理的 UI 组件，包括：
/// - 显示当前选择的 category
/// - 显示所有可用的 category
/// - 支持添加和删除 category
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/category_provider.dart';

/// Category 选择器组件
class CategorySelector extends ConsumerStatefulWidget {
  /// 当前选中的 category
  final String? selectedCategory;

  /// category 变化时的回调函数
  final ValueChanged<String?> onCategoryChanged;

  /// 构造函数
  const CategorySelector({
    super.key,
    this.selectedCategory,
    required this.onCategoryChanged,
  });

  @override
  ConsumerState<CategorySelector> createState() => _CategorySelectorState();
}

class _CategorySelectorState extends ConsumerState<CategorySelector> {
  /// 新分类输入控制器
  late final TextEditingController _newCategoryController;

  @override
  void initState() {
    super.initState();
    _newCategoryController = TextEditingController();
  }

  @override
  void dispose() {
    _newCategoryController.dispose();
    super.dispose();
  }

  /// 添加分类
  void _addCategory(String category) {
    if (category.isEmpty) return;
    widget.onCategoryChanged(category);
    _newCategoryController.clear();
    ref.read(categoryProvider.notifier).addCategory(category);
  }

  /// 移除分类
  void _removeCategory() {
    widget.onCategoryChanged(null);
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final categoryState = ref.watch(categoryProvider);

    // 处理错误状态
    if (categoryState.error != null) {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // 已选择的 category 显示区域
          if (widget.selectedCategory != null)
            Chip(
              label: Text(widget.selectedCategory!),
              deleteIcon: const Icon(Icons.close, size: 18),
              onDeleted: _removeCategory,
              backgroundColor: theme.colorScheme.primaryContainer,
              labelStyle: TextStyle(color: theme.colorScheme.onPrimaryContainer),
            ),
          const SizedBox(height: 8),
          Text(
            '加载分类列表失败: ${categoryState.error}',
            style: TextStyle(color: theme.colorScheme.error),
          ),
        ],
      );
    }

    return Container(
      decoration: BoxDecoration(
        border: Border.all(color: theme.dividerColor),
        borderRadius: BorderRadius.circular(4),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // 已选择的 category 显示区域
          if (widget.selectedCategory?.isNotEmpty == true)
            Container(
              padding: const EdgeInsets.all(16),
              decoration: BoxDecoration(
                color: theme.colorScheme.surfaceContainerHighest.withOpacity(0.5),
                border: Border(
                  bottom: BorderSide(color: theme.dividerColor),
                ),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    '当前分类',
                    style: theme.textTheme.titleSmall?.copyWith(
                      color: theme.colorScheme.onSurfaceVariant,
                    ),
                  ),
                  const SizedBox(height: 8),
                  Chip(
                    label: Text(widget.selectedCategory!),
                    deleteIcon: const Icon(Icons.close, size: 18),
                    onDeleted: _removeCategory,
                    backgroundColor: theme.colorScheme.primary.withOpacity(0.1),
                    labelStyle: TextStyle(color: theme.colorScheme.primary),
                    deleteIconColor: theme.colorScheme.primary,
                    side: BorderSide(color: theme.colorScheme.primary.withOpacity(0.2)),
                  ),
                ],
              ),
            ),

          // 新增分类区域
          Container(
            padding: const EdgeInsets.all(16),
            decoration: BoxDecoration(
              border: Border(
                bottom: BorderSide(color: theme.dividerColor),
              ),
            ),
            child: Row(
              children: [
                Expanded(
                  child: TextFormField(
                    controller: _newCategoryController,
                    decoration: InputDecoration(
                      labelText: '新增分类',
                      hintText: '输入新的分类，按回车或点击添加按钮',
                      isDense: true,
                      contentPadding: const EdgeInsets.symmetric(horizontal: 12, vertical: 12),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(4),
                      ),
                    ),
                    onFieldSubmitted: (value) {
                      if (value.trim().isNotEmpty) {
                        _addCategory(value.trim());
                      }
                    },
                  ),
                ),
                const SizedBox(width: 8),
                ElevatedButton.icon(
                  onPressed: () {
                    if (_newCategoryController.text.trim().isNotEmpty) {
                      _addCategory(_newCategoryController.text.trim());
                    }
                  },
                  icon: const Icon(Icons.add, size: 18),
                  label: const Text('添加'),
                  style: ElevatedButton.styleFrom(
                    padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
                  ),
                ),
              ],
            ),
          ),

          // 可用的 category 显示区域
          Container(
            padding: const EdgeInsets.all(16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  '可用的分类',
                  style: theme.textTheme.titleSmall,
                ),
                const SizedBox(height: 8),
                if (categoryState.allCategories.isEmpty && !categoryState.isLoading)
                  Center(
                    child: Padding(
                      padding: const EdgeInsets.all(16.0),
                      child: Text(
                        '没有可用的分类',
                        style: theme.textTheme.bodyMedium?.copyWith(
                          color: theme.colorScheme.onSurfaceVariant.withOpacity(0.7),
                        ),
                      ),
                    ),
                  )
                else if (!categoryState.isLoading)
                  Wrap(
                    spacing: 8,
                    runSpacing: 8,
                    children: (() {
                      final sortedCategories = categoryState.allCategories.toList()..sort();
                      return sortedCategories
                          .map((category) => ActionChip(
                                label: Text(category),
                                onPressed: () => _addCategory(category),
                                backgroundColor: category == widget.selectedCategory
                                    ? theme.colorScheme.primary.withOpacity(0.1)
                                    : theme.colorScheme.surfaceContainerHighest,
                                labelStyle: TextStyle(
                                  color: category == widget.selectedCategory
                                      ? theme.colorScheme.primary
                                      : theme.colorScheme.onSurfaceVariant,
                                ),
                                side: category == widget.selectedCategory
                                    ? BorderSide(color: theme.colorScheme.primary.withOpacity(0.2))
                                    : null,
                              ))
                          .toList();
                    })(),
                  ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
