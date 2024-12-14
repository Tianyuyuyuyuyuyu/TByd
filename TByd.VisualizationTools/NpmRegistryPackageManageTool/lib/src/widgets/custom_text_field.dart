/// NPM Registry Manager - 自定义文本输入框组件
///
/// 该文件实现了一个统一风格的文本输入框组件，包括：
/// - Material Design风格
/// - 自定义外观和行为
/// - 输入验证功能
/// - 状态管理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';

/// 自定义文本输入框组件
///
/// 提供统一的文本输入界面，支持：
/// - 标签文本
/// - 图标装饰
/// - 输入验证
/// - 状态反馈
class CustomTextField extends StatelessWidget {
  /// 文本控制器
  final TextEditingController controller;

  /// 标签文本
  final String label;

  /// 前缀图标
  final IconData? prefixIcon;

  /// 后缀图标
  final Widget? suffixIcon;

  /// 是否隐藏输入内容
  final bool obscureText;

  /// 输入验证函数
  final String? Function(String?)? validator;

  /// 键盘类型
  final TextInputType? keyboardType;

  /// 是否启用
  final bool enabled;

  /// 点击回调
  final VoidCallback? onTap;

  /// 内容变化回调
  final void Function(String)? onChanged;

  /// 构造函数
  ///
  /// [controller] 文本编辑控制器
  /// [label] 输入框标签
  /// [prefixIcon] 前缀图标
  /// [suffixIcon] 后缀图标
  /// [obscureText] 是否隐藏输入内容（用于密码输入）
  /// [validator] 输入验证函数
  /// [keyboardType] 键盘类型
  /// [enabled] 是否启用输入
  /// [onTap] 点击回调函数
  /// [onChanged] 内容变化回调函数
  /// [key] Widget的键
  const CustomTextField({
    Key? key,
    required this.controller,
    required this.label,
    this.prefixIcon,
    this.suffixIcon,
    this.obscureText = false,
    this.validator,
    this.keyboardType,
    this.enabled = true,
    this.onTap,
    this.onChanged,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return TextFormField(
      controller: controller,
      obscureText: obscureText,
      validator: validator,
      keyboardType: keyboardType,
      enabled: enabled,
      onTap: onTap,
      onChanged: onChanged,
      style: theme.textTheme.bodyLarge,
      decoration: InputDecoration(
        labelText: label,
        prefixIcon: prefixIcon != null ? Icon(prefixIcon) : null,
        suffixIcon: suffixIcon,
        // 默认边框样式
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8),
        ),
        // 启用状态边框样式
        enabledBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8),
          borderSide: BorderSide(
            color: theme.colorScheme.outline,
          ),
        ),
        // 聚焦状态边框样式
        focusedBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8),
          borderSide: BorderSide(
            color: theme.colorScheme.primary,
            width: 2,
          ),
        ),
        // 错误状态边框样式
        errorBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8),
          borderSide: BorderSide(
            color: theme.colorScheme.error,
          ),
        ),
        // 聚焦错误状态边框样式
        focusedErrorBorder: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8),
          borderSide: BorderSide(
            color: theme.colorScheme.error,
            width: 2,
          ),
        ),
        // 填充样式
        filled: true,
        fillColor: enabled ? theme.colorScheme.surface : theme.colorScheme.surfaceContainerHighest,
      ),
    );
  }
}
