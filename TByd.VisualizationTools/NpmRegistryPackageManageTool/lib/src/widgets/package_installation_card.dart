/// NPM Registry Manager - 包安装卡片组件
///
/// 该文件实现了包安装命令的展示卡片，包括：
/// - NPM安装命令
/// - Yarn安装命令
/// - PNPM安装命令
/// - 安装设置管理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package_settings_dialog.dart';
import 'package_installation_command.dart';

/// 包安装卡片组件
///
/// 展示不同包管理器的安装命令，包括：
/// - 包名和版本信息
/// - 多种包管理器的安装命令
/// - 安装设置选项
class PackageInstallationCard extends StatelessWidget {
  /// 包名
  final String packageName;

  /// 版本号
  final String version;

  /// 构造函数
  ///
  /// [packageName] 要安装的包名
  /// [version] 要安装的版本号
  /// [key] Widget的键
  const PackageInstallationCard({
    Key? key,
    required this.packageName,
    required this.version,
  }) : super(key: key);

  /// 显示设置对话框
  ///
  /// 打开包安装设置对话框，允许用户配置：
  /// - 全局/本地安装
  /// - 包管理器特定选项
  void _showSettingsDialog(BuildContext context) {
    showDialog(
      context: context,
      builder: (context) => PackageSettingsDialog(
        packageName: packageName,
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    // 构建完整的包名（包含版本号）
    final String fullPackageName = '$packageName@$version';

    return Card(
      margin: const EdgeInsets.all(8.0),
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // NPM安装命令
            PackageInstallationCommand(
              packageManager: 'npm',
              command: 'npm install $fullPackageName',
              icon: Image.asset(
                'assets/images/npm-logo.png',
                width: 24,
                height: 24,
              ),
              onSettingsPressed: () => _showSettingsDialog(context),
            ),
            const SizedBox(height: 8.0),
            // Yarn安装命令
            PackageInstallationCommand(
              packageManager: 'yarn',
              command: 'yarn add $fullPackageName',
              icon: Image.asset(
                'assets/images/yarn-logo.png',
                width: 24,
                height: 24,
              ),
              onSettingsPressed: () => _showSettingsDialog(context),
            ),
            const SizedBox(height: 8.0),
            // PNPM安装命令
            PackageInstallationCommand(
              packageManager: 'pnpm',
              command: 'pnpm install $fullPackageName',
              icon: Image.asset(
                'assets/images/pnpm-logo.png',
                width: 24,
                height: 24,
              ),
              onSettingsPressed: () => _showSettingsDialog(context),
            ),
          ],
        ),
      ),
    );
  }
}
