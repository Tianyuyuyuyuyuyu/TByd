/// NPM Registry Manager - Contributors 选择器
///
/// 该文件实现了 contributors 选择和管理的 UI 组件，包括：
/// - 显示已选择的 contributors
/// - 支持添加和删除 contributors
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/unity_package_config.dart';

/// Contributors 选择器组件
class ContributorsSelector extends ConsumerStatefulWidget {
  /// 当前选中的 contributors
  final List<Contributor> selectedContributors;

  /// contributors 变化时的回调函数
  final ValueChanged<List<Contributor>> onContributorsChanged;

  /// 构造函数
  const ContributorsSelector({
    super.key,
    required this.selectedContributors,
    required this.onContributorsChanged,
  });

  @override
  ConsumerState<ContributorsSelector> createState() => _ContributorsSelectorState();
}

class _ContributorsSelectorState extends ConsumerState<ContributorsSelector> {
  /// 新贡献者名称输入控制器
  late final TextEditingController _nameController;

  /// 新贡献者邮箱输入控制器
  late final TextEditingController _emailController;

  @override
  void initState() {
    super.initState();
    _nameController = TextEditingController();
    _emailController = TextEditingController();
  }

  @override
  void dispose() {
    _nameController.dispose();
    _emailController.dispose();
    super.dispose();
  }

  /// 添加新的贡献者
  void _addContributor() {
    final name = _nameController.text.trim();
    final email = _emailController.text.trim();

    if (name.isNotEmpty) {
      final newContributor = Contributor(
        name: name,
        email: email,
      );

      if (!widget.selectedContributors.any((c) => c.name == name)) {
        final updatedContributors = [...widget.selectedContributors, newContributor];
        widget.onContributorsChanged(updatedContributors);

        // 清空输入框
        _nameController.clear();
        _emailController.clear();
      }
    }
  }

  /// 删除贡献者
  void _removeContributor(Contributor contributor) {
    final updatedContributors = widget.selectedContributors.where((c) => c.name != contributor.name).toList();
    widget.onContributorsChanged(updatedContributors);
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Container(
      decoration: BoxDecoration(
        border: Border.all(color: theme.dividerColor),
        borderRadius: BorderRadius.circular(4),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // 已选择的 contributors 显示区域
          if (widget.selectedContributors.isNotEmpty)
            Container(
              padding: const EdgeInsets.all(16),
              decoration: BoxDecoration(
                color: theme.colorScheme.surfaceContainerHighest.withOpacity(0.5),
                border: Border(
                  bottom: BorderSide(color: theme.dividerColor),
                ),
              ),
              child: Wrap(
                spacing: 8,
                runSpacing: 8,
                children: widget.selectedContributors.map((contributor) {
                  return Chip(
                    label: Text(
                      contributor.email.isEmpty ? contributor.name : '${contributor.name} <${contributor.email}>',
                    ),
                    deleteIcon: const Icon(Icons.close, size: 18),
                    onDeleted: () => _removeContributor(contributor),
                    backgroundColor: theme.colorScheme.primaryContainer,
                    labelStyle: TextStyle(color: theme.colorScheme.onPrimaryContainer),
                  );
                }).toList(),
              ),
            ),

          // 添加新贡献者的输入区域
          Container(
            padding: const EdgeInsets.all(16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  '添加贡献者',
                  style: theme.textTheme.titleSmall,
                ),
                const SizedBox(height: 8),
                Row(
                  children: [
                    Expanded(
                      flex: 2,
                      child: TextField(
                        controller: _nameController,
                        decoration: const InputDecoration(
                          hintText: '贡献者名称',
                          isDense: true,
                          contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 12),
                        ),
                        onSubmitted: (_) => _addContributor(),
                      ),
                    ),
                    const SizedBox(width: 8),
                    Expanded(
                      flex: 2,
                      child: TextField(
                        controller: _emailController,
                        decoration: const InputDecoration(
                          hintText: '电子邮件（可选）',
                          isDense: true,
                          contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 12),
                        ),
                        onSubmitted: (_) => _addContributor(),
                      ),
                    ),
                    const SizedBox(width: 8),
                    ElevatedButton.icon(
                      onPressed: _addContributor,
                      icon: const Icon(Icons.add, size: 18),
                      label: const Text('添加'),
                      style: ElevatedButton.styleFrom(
                        padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 12),
                      ),
                    ),
                  ],
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
