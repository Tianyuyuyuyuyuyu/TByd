/// NPM Registry Manager - 加载遮罩组件
///
/// 该文件实现了加载状态的遮罩层组件，包括：
/// - 半透明背景
/// - 加载指示器
/// - 加载提示信息
/// - 子组件显示
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';

/// 加载遮罩组件
///
/// 在加载状态下显示遮罩层和加载指示器，包括：
/// - 可配置的加载状态
/// - 自定义加载提示
/// - 内容组件显示
class LoadingOverlay extends StatelessWidget {
  /// 是否显示加载状态
  final bool isLoading;

  /// 内容组件
  final Widget child;

  /// 加载提示信息
  final String? message;

  /// 构造函数
  ///
  /// [isLoading] 是否处于加载状态
  /// [child] 被遮罩的内容组件
  /// [message] 可选的加载提示文本
  /// [key] Widget的键
  const LoadingOverlay({
    Key? key,
    required this.isLoading,
    required this.child,
    this.message,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    return Stack(
      children: [
        // 内容组件
        child,
        // 加载遮罩层（仅在加载状态显示）
        if (isLoading)
          Container(
            // 半透明黑色背景
            color: Colors.black.withOpacity(0.3),
            child: Center(
              child: Card(
                margin: const EdgeInsets.all(16),
                child: Padding(
                  padding: const EdgeInsets.all(24),
                  child: Column(
                    mainAxisSize: MainAxisSize.min,
                    children: [
                      // 加载进度指示器
                      CircularProgressIndicator(
                        valueColor: AlwaysStoppedAnimation<Color>(
                          theme.colorScheme.primary,
                        ),
                      ),
                      // 加载提示文本（如果有）
                      if (message != null) ...[
                        const SizedBox(height: 16),
                        Text(
                          message!,
                          style: theme.textTheme.bodyMedium,
                          textAlign: TextAlign.center,
                        ),
                      ],
                    ],
                  ),
                ),
              ),
            ),
          ),
      ],
    );
  }
}
