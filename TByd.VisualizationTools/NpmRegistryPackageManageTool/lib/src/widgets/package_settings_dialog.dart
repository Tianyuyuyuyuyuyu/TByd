/// NPM Registry Manager - 包设置对话框
///
/// 该文件实现了包安装设置的对话框组件，包括：
/// - 全局安装选项
/// - Yarn现代语法选项
/// - 设置状态管理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

/// 全局包安装状态提供者
///
/// 控制是否将包安装为全局包
/// true 表示全局安装，false 表示本地安装
final globalPackageProvider = StateProvider<bool>((ref) => false);

/// Yarn现代语法状态提供者
///
/// 控制是否使用Yarn的现代语法
/// true 表示使用现代语法，false 表示使用传统语法
final yarnModernSyntaxProvider = StateProvider<bool>((ref) => false);

/// 包设置对话框组件
///
/// 提供包安装的相关设置选项，包括：
/// - 全局/本地安装选择
/// - Yarn语法版本选择
class PackageSettingsDialog extends ConsumerWidget {
  /// 包名
  final String packageName;

  /// 构造函数
  ///
  /// [packageName] 要设置的包名
  /// [key] Widget的键
  const PackageSettingsDialog({
    Key? key,
    required this.packageName,
  }) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // 监听全局包安装状态
    final isGlobalPackage = ref.watch(globalPackageProvider);
    // 监听Yarn语法选择状态
    final isYarnModernSyntax = ref.watch(yarnModernSyntaxProvider);

    return Column(
      mainAxisSize: MainAxisSize.min,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        // 全局包安装选项
        CheckboxListTile(
          title: const Text('global package'),
          value: isGlobalPackage,
          onChanged: (value) {
            ref.read(globalPackageProvider.notifier).state = value ?? false;
          },
          dense: true,
          controlAffinity: ListTileControlAffinity.leading,
          visualDensity: VisualDensity.compact,
        ),
        // Yarn现代语法选项
        CheckboxListTile(
          title: const Text('yarn modern syntax'),
          value: isYarnModernSyntax,
          onChanged: (value) {
            ref.read(yarnModernSyntaxProvider.notifier).state = value ?? false;
          },
          dense: true,
          controlAffinity: ListTileControlAffinity.leading,
          visualDensity: VisualDensity.compact,
        ),
      ],
    );
  }
}
