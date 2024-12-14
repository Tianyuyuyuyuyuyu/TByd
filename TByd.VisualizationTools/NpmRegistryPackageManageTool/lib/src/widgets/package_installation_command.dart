/// NPM Registry Manager - 包安装命令组件
///
/// 该文件实现了包安装命令的展示组件，包括：
/// - 命令文本显示
/// - 复制到剪贴板功能
/// - 包管理器图标
/// - 设置按钮
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package_settings_button.dart';

/// 包安装命令组件
///
/// 展示特定包管理器的安装命令，提供：
/// - 命令文本显示
/// - 一键复制功能
/// - 设置选项
class PackageInstallationCommand extends StatelessWidget {
  /// 安装命令
  final String command;

  /// 包管理器名称
  final String packageManager;

  /// 包管理器图标
  final Widget icon;

  /// 设置按钮点击回调
  final VoidCallback onSettingsPressed;

  /// 构造函数
  ///
  /// [command] 完整的安装命令
  /// [packageManager] 包管理器名称（npm/yarn/pnpm）
  /// [icon] 包管理器图标
  /// [onSettingsPressed] 设置按钮点击处理函数
  /// [key] Widget的键
  const PackageInstallationCommand({
    Key? key,
    required this.command,
    required this.packageManager,
    required this.icon,
    required this.onSettingsPressed,
  }) : super(key: key);

  /// 复制命令到剪贴板
  ///
  /// 将安装命令复制到系统剪贴板，并显示提示信息
  /// [context] 构建上下文，用于显示提示信息
  void _copyToClipboard(BuildContext context) {
    Clipboard.setData(ClipboardData(text: command));
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text('$packageManager command copied to clipboard'),
        duration: const Duration(seconds: 2),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(vertical: 8.0, horizontal: 16.0),
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.surface,
        borderRadius: BorderRadius.circular(4.0),
      ),
      child: Row(
        children: [
          // 包管理器图标
          icon,
          const SizedBox(width: 12.0),
          // 命令文本
          Expanded(
            child: Text(
              command,
              style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                    fontFamily: 'Monospace',
                  ),
            ),
          ),
          // 复制按钮
          IconButton(
            icon: const Icon(Icons.copy),
            onPressed: () => _copyToClipboard(context),
            tooltip: 'Copy to clipboard',
            constraints: const BoxConstraints(),
            padding: const EdgeInsets.all(8.0),
            iconSize: 20.0,
          ),
          // 设置按钮
          PackageSettingsButton(
            onPressed: onSettingsPressed,
          ),
        ],
      ),
    );
  }
}
