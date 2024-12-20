import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:file_picker/file_picker.dart';
import '../widgets/package_settings_form.dart';
import '../models/unity_package_config.dart';
import '../providers/package_settings_provider.dart';
import 'dart:io';
import 'dart:convert';
import 'package:path/path.dart' as path;
import 'package:url_launcher/url_launcher.dart' as url_launcher;
import '../widgets/markdown_file_viewer.dart';
import '../widgets/package_upload_terminal.dart';
import '../services/package_upload_service.dart';

/// 包操作页面
///
/// 提供Unity Custom Package的相关操作功能，包括：
/// - 打开项目文件夹
/// - 发布设置
/// - 查看README
/// - 查看CHANGELOG
class PackageOperationsPage extends ConsumerStatefulWidget {
  const PackageOperationsPage({super.key});

  @override
  ConsumerState<PackageOperationsPage> createState() => _PackageOperationsPageState();
}

class _PackageOperationsPageState extends ConsumerState<PackageOperationsPage> with SingleTickerProviderStateMixin {
  String? _selectedFolderPath;
  late TabController _tabController;
  bool _isUploading = false;

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 4, vsync: this);

    // 恢复上次的项目路径
    Future.microtask(() {
      final savedState = ref.read(packageSettingsProvider);
      if (savedState.currentProjectPath != null) {
        setState(() {
          _selectedFolderPath = savedState.currentProjectPath;
        });
        _loadPackageConfig(savedState.currentProjectPath!);
      }
    });
  }

  @override
  void dispose() {
    _tabController.dispose();
    super.dispose();
  }

  Future<void> _selectFolder() async {
    String? folderPath = await FilePicker.platform.getDirectoryPath(
      dialogTitle: '选择Unity Custom Package项目文件夹',
    );

    if (folderPath != null) {
      setState(() {
        _selectedFolderPath = folderPath;
      });

      // 尝试读取package.json
      await _loadPackageConfig(folderPath);
    }
  }

  Future<void> _loadPackageConfig(String folderPath) async {
    final packageJsonFile = File(path.join(folderPath, 'package.json'));
    if (await packageJsonFile.exists()) {
      try {
        final jsonContent = await packageJsonFile.readAsString();
        final jsonData = json.decode(jsonContent);
        final config = UnityPackageConfig.fromJson(jsonData);

        // 设置provider的配置和当前项目
        ref.read(packageSettingsProvider.notifier).setCurrentProject(
              folderPath,
              initialConfig: config,
            );
      } catch (e) {
        if (mounted) {
          ScaffoldMessenger.of(context).showSnackBar(
            SnackBar(
              content: Text('读取package.json失败: ${e.toString()}'),
              backgroundColor: Theme.of(context).colorScheme.error,
            ),
          );
        }
      }
    }
  }

  Future<void> _savePackageConfig(UnityPackageConfig config) async {
    if (_selectedFolderPath == null) return;

    final packageJsonFile = File(path.join(_selectedFolderPath!, 'package.json'));
    try {
      final jsonContent = const JsonEncoder.withIndent('  ').convert(config.toJson());
      await packageJsonFile.writeAsString(jsonContent);

      // 更新provider的配置
      ref.read(packageSettingsProvider.notifier).updateConfig(
            config,
            projectPath: _selectedFolderPath,
          );

      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: const Text('配置已保存'),
            backgroundColor: Theme.of(context).colorScheme.primary,
            behavior: SnackBarBehavior.floating,
          ),
        );
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('保存配置失败: ${e.toString()}'),
            backgroundColor: Theme.of(context).colorScheme.error,
          ),
        );
      }
    }
  }

  Future<void> _handleUpload() async {
    if (_selectedFolderPath == null) {
      return;
    }

    setState(() {
      _isUploading = true;
    });

    try {
      await Future.delayed(const Duration(seconds: 2));
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('上传失败：${e.toString()}'),
            backgroundColor: Colors.red,
          ),
        );
      }
    } finally {
      if (mounted) {
        setState(() {
          _isUploading = false;
        });
      }
    }
  }

  Future<void> _handleUploadSuccess() async {
    // 显示成功提示
    if (mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('包发布成功！'),
          backgroundColor: Colors.green,
        ),
      );

      // 重新加载配置
      if (_selectedFolderPath != null) {
        await _loadPackageConfig(_selectedFolderPath!);
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final savedState = ref.watch(packageSettingsProvider);
    final currentConfig = savedState.config;

    return Padding(
      padding: const EdgeInsets.all(16.0),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // 顶部标题和操作区
          Card(
            child: Padding(
              padding: const EdgeInsets.all(16.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text(
                    'Unity Custom Package 项目',
                    style: theme.textTheme.titleLarge?.copyWith(
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                  const SizedBox(height: 16),
                  Row(
                    children: [
                      ElevatedButton.icon(
                        icon: const Icon(Icons.folder_open),
                        label: const Text('选择项目文件夹'),
                        onPressed: _selectFolder,
                      ),
                      if (_selectedFolderPath != null) ...[
                        const SizedBox(width: 8),
                        IconButton(
                          icon: const Icon(Icons.folder),
                          onPressed: () async {
                            if (_selectedFolderPath != null) {
                              final uri = Uri.file(_selectedFolderPath!);
                              if (await url_launcher.canLaunchUrl(uri)) {
                                await url_launcher.launchUrl(uri);
                              }
                            }
                          },
                          tooltip: '打开项目文件夹',
                        ),
                        const SizedBox(width: 8),
                        Expanded(
                          child: Text(
                            _selectedFolderPath!,
                            style: theme.textTheme.bodyMedium,
                            overflow: TextOverflow.ellipsis,
                          ),
                        ),
                      ],
                    ],
                  ),
                ],
              ),
            ),
          ),
          const SizedBox(height: 8),
          // 页签区域
          if (_selectedFolderPath != null) ...[
            Container(
              decoration: BoxDecoration(
                border: Border(
                  bottom: BorderSide(
                    color: theme.colorScheme.outlineVariant,
                    width: 1,
                  ),
                ),
              ),
              child: TabBar(
                controller: _tabController,
                labelColor: theme.colorScheme.primary,
                unselectedLabelColor: theme.colorScheme.onSurfaceVariant,
                indicatorColor: theme.colorScheme.primary,
                indicatorWeight: 3,
                labelStyle: theme.textTheme.titleSmall?.copyWith(
                  fontWeight: FontWeight.bold,
                ),
                unselectedLabelStyle: theme.textTheme.titleSmall,
                tabs: const [
                  Tab(text: 'package.json'),
                  Tab(text: 'README'),
                  Tab(text: 'CHANGELOG'),
                  Tab(text: '上传'),
                ],
              ),
            ),
            const SizedBox(height: 8),
            Expanded(
              child: TabBarView(
                controller: _tabController,
                children: [
                  // 发布设置页内容
                  PackageSettingsForm(
                    initialConfig: currentConfig,
                    onConfigChanged: _savePackageConfig,
                  ),
                  // README 页签内容
                  MarkdownFileViewer(
                    projectPath: _selectedFolderPath!,
                    fileName: 'README.md',
                  ),
                  // CHANGELOG 页签内容
                  MarkdownFileViewer(
                    projectPath: _selectedFolderPath!,
                    fileName: 'CHANGELOG.md',
                  ),
                  // 上传页签内容
                  PackageUploadTerminal(
                    projectPath: _selectedFolderPath!,
                    onUpload: _handleUpload,
                    onUploadSuccess: _handleUploadSuccess,
                    isUploading: _isUploading,
                  ),
                ],
              ),
            ),
          ],
        ],
      ),
    );
  }
}
