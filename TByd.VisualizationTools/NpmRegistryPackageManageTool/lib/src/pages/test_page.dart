import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/unity_version_provider.dart';
import '../models/unity_version.dart';

/// 测试页面
class TestPage extends ConsumerWidget {
  /// 构造函数
  const TestPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final state = ref.watch(unityVersionProvider);

    return Scaffold(
      appBar: AppBar(
        title: const Text('Unity 版本列表'),
        actions: [
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () {
              ref.read(unityVersionProvider.notifier).loadVersions();
            },
          ),
        ],
      ),
      body: _buildBody(state),
    );
  }

  Widget _buildBody(UnityVersionState state) {
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
            const Icon(
              Icons.error_outline,
              color: Colors.red,
              size: 60,
            ),
            const SizedBox(height: 16),
            Text(
              '加载失败',
              style: TextStyle(
                fontSize: 20,
                color: Colors.red[700],
              ),
            ),
            const SizedBox(height: 8),
            Text(
              state.error!,
              style: const TextStyle(
                color: Colors.grey,
              ),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () {
                // 重试按钮
              },
              child: const Text('重试'),
            ),
          ],
        ),
      );
    }

    if (state.versions == null || state.versions!.isEmpty) {
      return const Center(
        child: Text('没有找到版本信息'),
      );
    }

    return ListView.builder(
      itemCount: state.versions!.length,
      itemBuilder: (context, index) {
        final version = state.versions![index];
        return Card(
          margin: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
          child: ExpansionTile(
            title: Text(
              version.version,
              style: const TextStyle(
                fontWeight: FontWeight.bold,
              ),
            ),
            subtitle: Text(
              version.lts ? 'LTS 版本' : '标准版本',
              style: TextStyle(
                color: version.lts ? Colors.green : Colors.grey,
              ),
            ),
            children: [
              Padding(
                padding: const EdgeInsets.all(16.0),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    _buildDownloadSection(version),
                  ],
                ),
              ),
            ],
          ),
        );
      },
    );
  }

  Widget _buildDownloadSection(UnityVersion version) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Text(
          '下载链接:',
          style: TextStyle(
            fontWeight: FontWeight.bold,
            fontSize: 16,
          ),
        ),
        const SizedBox(height: 8),
        ...version.downloadUrl.entries.map((entry) {
          return Padding(
            padding: const EdgeInsets.only(bottom: 8.0),
            child: Row(
              children: [
                Text(
                  '${entry.key}:',
                  style: const TextStyle(
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(width: 8),
                Expanded(
                  child: Text(
                    entry.value,
                    style: const TextStyle(
                      color: Colors.blue,
                    ),
                    overflow: TextOverflow.ellipsis,
                  ),
                ),
              ],
            ),
          );
        }).toList(),
      ],
    );
  }
}
