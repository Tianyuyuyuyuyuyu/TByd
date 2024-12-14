/// NPM Registry Manager - 包设置按钮组件
///
/// 该文件实现了包安装设置的按钮组件，包括：
/// - 设置图标按钮
/// - 点击事件处理
/// - 工具提示
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';

/// 包设置按钮组件
///
/// 提供一个统一的设置按钮，用于：
/// - 打开包安装设置
/// - 配置安装选项
/// - 提供视觉反馈
class PackageSettingsButton extends StatelessWidget {
  /// 按钮点击回调函数
  final VoidCallback onPressed;

  /// 构造函数
  ///
  /// [onPressed] 按钮点击处理函数
  /// [key] Widget的键
  const PackageSettingsButton({
    Key? key,
    required this.onPressed,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return IconButton(
      icon: const Icon(Icons.settings),
      onPressed: onPressed,
      tooltip: 'Settings',
      constraints: const BoxConstraints(),
      padding: const EdgeInsets.all(8.0),
      iconSize: 20.0,
    );
  }
}
