import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:intl/intl.dart';
import '../providers/unity_version_provider.dart';
import '../models/unity_version.dart';

/// 测试页面
class TestPage extends ConsumerWidget {
  /// 构造函数
  const TestPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final state = ref.watch(unityVersionProvider);
    final theme = Theme.of(context);

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
      body: _buildBody(state, theme),
    );
  }

  Widget _buildBody(UnityVersionState state, ThemeData theme) {
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
              color: theme.colorScheme.error,
              size: 60,
            ),
            const SizedBox(height: 16),
            Text(
              '加载失败',
              style: TextStyle(
                fontSize: 20,
                color: theme.colorScheme.error,
              ),
            ),
            const SizedBox(height: 8),
            Text(
              state.error!,
              style: TextStyle(
                color: theme.colorScheme.onSurfaceVariant,
              ),
            ),
            const SizedBox(height: 16),
            FilledButton.icon(
              onPressed: () {
                // TODO: 实现重试功能
              },
              icon: const Icon(Icons.refresh),
              label: const Text('重试'),
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
        return _buildVersionCard(version, theme);
      },
    );
  }

  Widget _buildVersionCard(UnityVersion version, ThemeData theme) {
    return Card(
      margin: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      child: ExpansionTile(
        title: Row(
          children: [
            _buildVersionBadge(version, theme),
            const SizedBox(width: 12),
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    version.version,
                    style: theme.textTheme.titleMedium?.copyWith(
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  if (version.releaseDate != null)
                    Text(
                      '发布于 ${DateFormat('yyyy-MM-dd').format(version.releaseDate!)}',
                      style: theme.textTheme.bodySmall?.copyWith(
                        color: theme.colorScheme.onSurfaceVariant,
                      ),
                    ),
                ],
              ),
            ),
          ],
        ),
        children: [
          Padding(
            padding: const EdgeInsets.all(16),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                _buildDownloadSection(version, theme),
                if (version.modules != null && version.modules!.isNotEmpty) ...[
                  const SizedBox(height: 16),
                  _buildModulesSection(version.modules!, theme),
                ],
              ],
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildVersionBadge(UnityVersion version, ThemeData theme) {
    Color backgroundColor;
    Color textColor;
    String text;

    switch (version.versionType.toLowerCase()) {
      case 'official':
        backgroundColor = theme.colorScheme.primary;
        textColor = theme.colorScheme.onPrimary;
        text = '正式版';
        break;
      case 'lts':
        backgroundColor = theme.colorScheme.tertiary;
        textColor = theme.colorScheme.onTertiary;
        text = 'LTS';
        break;
      case 'beta':
        backgroundColor = theme.colorScheme.secondary;
        textColor = theme.colorScheme.onSecondary;
        text = 'Beta';
        break;
      default:
        backgroundColor = theme.colorScheme.surfaceContainerHighest;
        textColor = theme.colorScheme.onSurfaceVariant;
        text = version.versionType;
    }

    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
      decoration: BoxDecoration(
        color: backgroundColor,
        borderRadius: BorderRadius.circular(4),
      ),
      child: Text(
        text,
        style: theme.textTheme.labelSmall?.copyWith(
          color: textColor,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
  }

  Widget _buildDownloadSection(UnityVersion version, ThemeData theme) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          '下载信息',
          style: theme.textTheme.titleSmall?.copyWith(
            fontWeight: FontWeight.bold,
          ),
        ),
        const SizedBox(height: 8),
        ...version.downloadUrl.entries.map((entry) {
          final platform = entry.key;
          final url = entry.value;
          final size = version.size?[platform];
          final checksum = version.checksum?[platform];

          return Padding(
            padding: const EdgeInsets.only(bottom: 8),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Row(
                  children: [
                    Icon(
                      _getPlatformIcon(platform),
                      size: 16,
                      color: theme.colorScheme.primary,
                    ),
                    const SizedBox(width: 8),
                    Text(
                      _getPlatformName(platform),
                      style: const TextStyle(
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ],
                ),
                const SizedBox(height: 4),
                Text(
                  url,
                  style: TextStyle(
                    color: theme.colorScheme.primary,
                    fontSize: 12,
                  ),
                ),
                if (size != null || checksum != null)
                  Padding(
                    padding: const EdgeInsets.only(top: 4),
                    child: Row(
                      children: [
                        if (size != null)
                          Text(
                            '大小: ${_formatFileSize(size)}',
                            style: theme.textTheme.bodySmall,
                          ),
                        if (size != null && checksum != null)
                          Text(
                            ' | ',
                            style: theme.textTheme.bodySmall,
                          ),
                        if (checksum != null)
                          Text(
                            'MD5: ${checksum.substring(0, 8)}...',
                            style: theme.textTheme.bodySmall,
                          ),
                      ],
                    ),
                  ),
              ],
            ),
          );
        }).toList(),
      ],
    );
  }

  Widget _buildModulesSection(List<UnityModule> modules, ThemeData theme) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          '可选模块',
          style: theme.textTheme.titleSmall?.copyWith(
            fontWeight: FontWeight.bold,
          ),
        ),
        const SizedBox(height: 8),
        ...modules.map((module) => _buildModuleItem(module, theme)).toList(),
      ],
    );
  }

  Widget _buildModuleItem(UnityModule module, ThemeData theme) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 8),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              Icon(
                _getModuleIcon(module.category),
                size: 16,
                color: theme.colorScheme.primary,
              ),
              const SizedBox(width: 8),
              Expanded(
                child: Text(
                  module.name,
                  style: const TextStyle(
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
              if (module.downloadSize != null)
                Text(
                  _formatFileSize(module.downloadSize!),
                  style: theme.textTheme.bodySmall,
                ),
            ],
          ),
          if (module.description.isNotEmpty)
            Padding(
              padding: const EdgeInsets.only(top: 4),
              child: Text(
                module.description,
                style: theme.textTheme.bodySmall?.copyWith(
                  color: theme.colorScheme.onSurfaceVariant,
                ),
              ),
            ),
        ],
      ),
    );
  }

  IconData _getPlatformIcon(String platform) {
    switch (platform.toLowerCase()) {
      case 'win32':
      case 'windows':
        return Icons.window;
      case 'darwin':
      case 'mac':
        return Icons.apple;
      case 'linux':
        return Icons.computer;
      case 'unityhub':
        return Icons.hub;
      default:
        return Icons.devices;
    }
  }

  String _getPlatformName(String platform) {
    switch (platform.toLowerCase()) {
      case 'win32':
      case 'windows':
        return 'Windows';
      case 'darwin':
      case 'mac':
        return 'macOS';
      case 'linux':
        return 'Linux';
      case 'unityhub':
        return 'Unity Hub';
      default:
        return platform;
    }
  }

  IconData _getModuleIcon(String category) {
    switch (category.toLowerCase()) {
      case 'platforms':
        return Icons.devices;
      case 'documentation':
        return Icons.description;
      case 'language':
        return Icons.language;
      case 'debug':
        return Icons.bug_report;
      case 'editor':
        return Icons.edit;
      default:
        return Icons.extension;
    }
  }

  String _formatFileSize(int bytes) {
    const suffixes = ['B', 'KB', 'MB', 'GB', 'TB'];
    var i = 0;
    double size = bytes.toDouble();

    while (size >= 1024 && i < suffixes.length - 1) {
      size /= 1024;
      i++;
    }

    return '${size.toStringAsFixed(2)} ${suffixes[i]}';
  }
}
