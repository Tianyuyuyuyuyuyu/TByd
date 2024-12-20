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
class KeywordsSelector extends ConsumerStatefulWidget {
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

  @override
  ConsumerState<KeywordsSelector> createState() => _KeywordsSelectorState();
}

class _KeywordsSelectorState extends ConsumerState<KeywordsSelector> {
  /// 新关键字输入控制器
  late final TextEditingController _newKeywordController;

  @override
  void initState() {
    super.initState();
    _newKeywordController = TextEditingController();
  }

  @override
  void dispose() {
    _newKeywordController.dispose();
    super.dispose();
  }

  /// 添加关键词
  void _addKeyword(String keyword) {
    if (keyword.isEmpty || widget.selectedKeywords.contains(keyword)) return;
    final updatedKeywords = List<String>.from(widget.selectedKeywords)..add(keyword);
    widget.onKeywordsChanged(updatedKeywords);
    _newKeywordController.clear();
  }

  /// 移除关键词
  void _removeKeyword(String keyword) {
    final updatedKeywords = List<String>.from(widget.selectedKeywords)..remove(keyword);
    widget.onKeywordsChanged(updatedKeywords);
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final keywordsState = ref.watch(keywordsProvider);

    // 处理错误状态
    if (keywordsState.error != null) {
      return Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // 已选择的 keywords 显示区域
          if (widget.selectedKeywords.isNotEmpty)
            Wrap(
              spacing: 8,
              runSpacing: 8,
              children: widget.selectedKeywords.map((keyword) {
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
          if (widget.selectedKeywords.isNotEmpty)
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
                    '已选择的关键字',
                    style: theme.textTheme.titleSmall?.copyWith(
                      color: theme.colorScheme.onSurfaceVariant,
                    ),
                  ),
                  const SizedBox(height: 8),
                  Wrap(
                    spacing: 8,
                    runSpacing: 8,
                    children: widget.selectedKeywords.map((keyword) {
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

          // 新增关键字区域
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
                    controller: _newKeywordController,
                    decoration: InputDecoration(
                      labelText: '新增关键字',
                      hintText: '输入新的关键字，按回车或点击添加按钮',
                      isDense: true,
                      contentPadding: const EdgeInsets.symmetric(horizontal: 12, vertical: 12),
                      border: OutlineInputBorder(
                        borderRadius: BorderRadius.circular(4),
                      ),
                    ),
                    onFieldSubmitted: (value) {
                      if (value.trim().isNotEmpty) {
                        _addKeyword(value.trim());
                      }
                    },
                  ),
                ),
                const SizedBox(width: 8),
                ElevatedButton.icon(
                  onPressed: () {
                    if (_newKeywordController.text.trim().isNotEmpty) {
                      _addKeyword(_newKeywordController.text.trim());
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
                          keywordsState.allKeywords.where((k) => !widget.selectedKeywords.contains(k)).toList()..sort();
                      return sortedKeywords
                          .map((keyword) => ActionChip(
                                label: Text(keyword),
                                onPressed: () => _addKeyword(keyword),
                                backgroundColor: theme.colorScheme.surfaceContainerHighest,
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
