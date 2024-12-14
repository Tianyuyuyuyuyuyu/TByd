import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:file_picker/file_picker.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

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

  @override
  void initState() {
    super.initState();
    _tabController = TabController(length: 3, vsync: this);
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
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('已选择文件夹: $folderPath'),
            backgroundColor: Theme.of(context).colorScheme.primary,
            behavior: SnackBarBehavior.floating,
          ),
        );
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

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
                        const SizedBox(width: 16),
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
                  Tab(text: '发布设置'),
                  Tab(text: 'README'),
                  Tab(text: 'CHANGELOG'),
                ],
              ),
            ),
            const SizedBox(height: 8),
            Expanded(
              child: TabBarView(
                controller: _tabController,
                children: [
                  // 发布设置页签内容
                  Card(
                    child: Center(
                      child: Text(
                        '发布设置内容区域',
                        style: theme.textTheme.bodyLarge,
                      ),
                    ),
                  ),
                  // README 页签内容
                  Card(
                    child: Center(
                      child: Text(
                        'README 内容区域',
                        style: theme.textTheme.bodyLarge,
                      ),
                    ),
                  ),
                  // CHANGELOG 页签内容
                  Card(
                    child: Center(
                      child: Text(
                        'CHANGELOG 内容区域',
                        style: theme.textTheme.bodyLarge,
                      ),
                    ),
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
