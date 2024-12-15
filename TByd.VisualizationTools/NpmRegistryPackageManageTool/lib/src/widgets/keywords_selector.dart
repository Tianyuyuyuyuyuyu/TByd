/// NPM Registry Manager - Keywords 选择器
///
/// 该文件实现了 keywords 选择和管理的 UI 组件，包括：
/// - 显示已选择的 keywords
/// - 显示所有可用的 keywords
/// - 支持添加和删除 keywords
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/keywords_provider.dart';

/// Keywords 选择器组件
class KeywordsSelector extends ConsumerWidget {
  /// 当前选中的 keywords
  final List<String> selectedKeywords;

  /// keywords 变化时的回调函数
  final ValueChanged<List<String>> onKeywordsChanged;

  /// 构造函数
  const KeywordsSelector({
    super.key,
    required this.selectedKeywords,
    required this.onKeywordsChanged,
  });

  /// 添加关键词
  void _addKeyword(String keyword) {
    if (keyword.isEmpty || selectedKeywords.contains(keyword)) return;
    final updatedKeywords = List<String>.from(selectedKeywords)..add(keyword);
    onKeywordsChanged(updatedKeywords);
  }

  /// 移除关键词
  void _removeKeyword(String keyword) {
    final updatedKeywords = List<String>.from(selectedKeywords)..remove(keyword);
    onKeywordsChanged(updatedKeywords);
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final theme = Theme.of(context);
    final keywordsState = ref.watch(keywordsProvider);

    // 处理错误状态
    if (keywordsState.error != null) {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // 已选择的 keywords 显示区域
          if (selectedKeywords.isNotEmpty)
            Wrap(
              spacing: 8,
              runSpacing: 8,
              children: selectedKeywords.map((keyword) {
                return Chip(
                  label: Text(keyword),
                  deleteIcon: const Icon(Icons.close, size: 18),
                  onDeleted: () => _removeKeyword(keyword),
                  backgroundColor: theme.colorScheme.primaryContainer,
                  labelStyle: TextStyle(color: theme.colorScheme.onPrimaryContainer),
                );
              }).toList(),
            ),
          const SizedBox(height: 8),
          Text(
            '加载关键词列表失败: ${keywordsState.error}',
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
          // 已选择的 keywords 显示区域
          if (selectedKeywords.isNotEmpty)
            Container(
              padding: const EdgeInsets.all(16),
              decoration: BoxDecoration(
                color: theme.colorScheme.surfaceVariant.withOpacity(0.5),
                border: Border(
                  bottom: BorderSide(color: theme.dividerColor),
                ),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    '已选择的关键字',
                    style: theme.textTheme.titleSmall?.copyWith(
                      color: theme.colorScheme.onSurfaceVariant,
                    ),
                  ),
                  const SizedBox(height: 8),
                  Wrap(
                    spacing: 8,
                    runSpacing: 8,
                    children: selectedKeywords.map((keyword) {
                      return Chip(
                        label: Text(keyword),
                        deleteIcon: const Icon(Icons.close, size: 18),
                        onDeleted: () => _removeKeyword(keyword),
                        backgroundColor: theme.colorScheme.primary.withOpacity(0.1),
                        labelStyle: TextStyle(color: theme.colorScheme.primary),
                        deleteIconColor: theme.colorScheme.primary,
                        side: BorderSide(color: theme.colorScheme.primary.withOpacity(0.2)),
                      );
                    }).toList(),
                  ),
                ],
              ),
            ),

          // 可用的 keywords 显示区域
          Container(
            padding: const EdgeInsets.all(16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  '可用的关键字',
                  style: theme.textTheme.titleSmall,
                ),
                const SizedBox(height: 8),
                if (keywordsState.isLoading)
                  const Center(child: CircularProgressIndicator())
                else
                  Wrap(
                    spacing: 8,
                    runSpacing: 8,
                    children: (() {
                      final sortedKeywords =
                          keywordsState.allKeywords.where((k) => !selectedKeywords.contains(k)).toList()..sort();
                      return sortedKeywords
                          .map((keyword) => ActionChip(
                                label: Text(keyword),
                                onPressed: () => _addKeyword(keyword),
                                backgroundColor: theme.colorScheme.surfaceVariant,
                                labelStyle: TextStyle(color: theme.colorScheme.onSurfaceVariant),
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
