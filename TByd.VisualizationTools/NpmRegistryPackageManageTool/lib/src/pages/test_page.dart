import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/unity_version_provider.dart';

/// 测试页面
///
/// 用于测试 Unity 版本 API 和展示版本信息
class TestPage extends ConsumerStatefulWidget {
  /// 构造函数
  const TestPage({super.key});

  @override
  ConsumerState<TestPage> createState() => _TestPageState();
}

class _TestPageState extends ConsumerState<TestPage> {
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final state = ref.watch(unityVersionProvider);

    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // 标题和刷新按钮
            Row(
              children: [
                Text(
                  'Unity 版本列表',
                  style: theme.textTheme.headlineMedium,
                ),
                const Spacer(),
                IconButton(
                  icon: const Icon(Icons.refresh),
                  onPressed: () {
                    ref.read(unityVersionProvider.notifier).loadVersions();
                  },
                  tooltip: '刷新版本列表',
                ),
              ],
            ),
            const SizedBox(height: 16),

            // 内容区域
            Expanded(
              child: _buildContent(state, theme),
            ),
          ],
        ),
      ),
    );
  }

  /// 构建内容区域
  Widget _buildContent(UnityVersionState state, ThemeData theme) {
    if (state.isLoading) {
      return const Center(
        child: CircularProgressIndicator(),
      );
    }

    if (state.error != null) {
      return Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              Icons.error_outline,
              size: 48,
              color: theme.colorScheme.error,
            ),
            const SizedBox(height: 16),
            Text(
              '加载失败',
              style: theme.textTheme.titleLarge?.copyWith(
                color: theme.colorScheme.error,
              ),
            ),
            const SizedBox(height: 8),
            Text(
              state.error!,
              style: theme.textTheme.bodyMedium?.copyWith(
                color: theme.colorScheme.error,
              ),
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 16),
            FilledButton.icon(
              icon: const Icon(Icons.refresh),
              label: const Text('重试'),
              onPressed: () {
                ref.read(unityVersionProvider.notifier).loadVersions();
              },
            ),
          ],
        ),
      );
    }

    if (state.versions == null || state.versions!.isEmpty) {
      return Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              Icons.info_outline,
              size: 48,
              color: theme.colorScheme.primary,
            ),
            const SizedBox(height: 16),
            Text(
              '点击刷新按钮获取版本信息',
              style: theme.textTheme.titleLarge,
            ),
          ],
        ),
      );
    }

    return ListView.builder(
      itemCount: state.versions!.length,
      itemBuilder: (context, index) {
        final version = state.versions![index];
        return Card(
          margin: const EdgeInsets.only(bottom: 8),
          child: ListTile(
            title: Text(
              version.version,
              style: theme.textTheme.titleMedium?.copyWith(
                fontWeight: FontWeight.bold,
              ),
            ),
            subtitle: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                if (version.longVersion != null) ...[
                  const SizedBox(height: 4),
                  Text('完整版本: ${version.longVersion}'),
                ],
                if (version.releaseDate != null) ...[
                  const SizedBox(height: 4),
                  Text('发布日期: ${version.releaseDate}'),
                ],
                if (version.isLts == true)
                  Chip(
                    label: const Text('LTS'),
                    backgroundColor: theme.colorScheme.primaryContainer,
                    labelStyle: TextStyle(
                      color: theme.colorScheme.onPrimaryContainer,
                    ),
                  ),
              ],
            ),
            trailing: IconButton(
              icon: const Icon(Icons.info_outline),
              onPressed: () {
                showDialog(
                  context: context,
                  builder: (context) => AlertDialog(
                    title: Text('版本 ${version.version} 详情'),
                    content: SingleChildScrollView(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          Text('发布说明:', style: theme.textTheme.titleSmall),
                          const SizedBox(height: 8),
                          Text(version.releaseNotes),
                        ],
                      ),
                    ),
                    actions: [
                      TextButton(
                        onPressed: () => Navigator.of(context).pop(),
                        child: const Text('关闭'),
                      ),
                    ],
                  ),
                );
              },
              tooltip: '查看详情',
            ),
          ),
        );
      },
    );
  }
}
