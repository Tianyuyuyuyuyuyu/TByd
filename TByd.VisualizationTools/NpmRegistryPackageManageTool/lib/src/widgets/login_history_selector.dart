/// NPM Registry Manager - 登录历史选择器组件
///
/// 该文件实现了登录历史选择器的UI组件，包括：
/// - 历史登录服务器列表
/// - 用户账号切换
/// - 快速登录功能
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/login_history_model.dart';
import '../providers/auth_provider.dart';
import '../providers/login_history_provider.dart';
import '../pages/login_page.dart';
import 'dart:developer' as developer;

/// 登录历史选择器组件
///
/// 显示用户的登录历史记录，允许用户：
/// - 查看历史登录的服务器
/// - 切换不同的用户账号
/// - 使用保存的凭据快速登录
class LoginHistorySelector extends ConsumerWidget {
  /// 构造函数
  const LoginHistorySelector({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // 监听登录历史状态
    final history = ref.watch(loginHistoryProvider);

    return AlertDialog(
      title: const Text('切换用户'),
      content: SizedBox(
        width: double.maxFinite,
        child: ListView.builder(
          shrinkWrap: true,
          itemCount: history.servers.length,
          itemBuilder: (context, index) {
            final server = history.servers[index];
            return Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                if (index > 0) const Divider(),
                // 服务器信息头部
                Padding(
                  padding: const EdgeInsets.symmetric(vertical: 8.0),
                  child: Row(
                    children: [
                      const Icon(Icons.dns, size: 16),
                      const SizedBox(width: 8),
                      Text(
                        server.serverName,
                        style: Theme.of(context).textTheme.titleSmall,
                      ),
                    ],
                  ),
                ),
                // 服务器下的用户列表
                ...server.users.map((user) => _buildUserTile(context, ref, server, user)),
              ],
            );
          },
        ),
      ),
      actions: [
        // 使用其他账号登录按钮
        TextButton(
          onPressed: () async {
            Navigator.of(context).pop();
            await ref.read(authProvider.notifier).logout();
            if (context.mounted) {
              Navigator.of(context).pushReplacement(
                MaterialPageRoute(
                  builder: (context) => const LoginPage(),
                ),
              );
            }
          },
          child: const Text('使用其他账号登录'),
        ),
        // 取消按钮
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: const Text('取消'),
        ),
      ],
    );
  }

  /// 构建用户列表项
  ///
  /// 为每个用户创建一个列表项，显示：
  /// - 用户头像
  /// - 用户名
  /// - 快速登录按钮
  ///
  /// 参数：
  /// - [context] 构建上下文
  /// - [ref] Provider引用
  /// - [server] 服务器历史记录
  /// - [user] 用户历史记录
  Widget _buildUserTile(BuildContext context, WidgetRef ref, ServerHistory server, UserHistory user) {
    developer.log('Building user tile for ${user.username} on ${server.serverUrl}');
    return ListTile(
      // 用户头像
      leading: CircleAvatar(
        backgroundColor: Theme.of(context).colorScheme.primaryContainer,
        child: Text(
          user.username[0].toUpperCase(),
          style: TextStyle(color: Theme.of(context).colorScheme.onPrimaryContainer),
        ),
      ),
      title: Text(user.username),
      trailing: Row(
        mainAxisSize: MainAxisSize.min,
        children: [
          // 快速登录按钮
          TextButton.icon(
            icon: const Icon(Icons.login),
            label: const Text('使用此账号登录'),
            onPressed: () async {
              Navigator.of(context).pop();
              if (user.rememberPassword && user.savedPassword != null) {
                // 使用保存的密码进行快速登录
                try {
                  await ref.read(authProvider.notifier).login(
                        server.serverUrl,
                        user.username,
                        user.savedPassword!,
                        rememberMe: true,
                      );
                } catch (e) {
                  if (context.mounted) {
                    // 显示登录失败提示
                    ScaffoldMessenger.of(context).showSnackBar(
                      SnackBar(
                        content: Text('登录失败：${e.toString()}'),
                        backgroundColor: Theme.of(context).colorScheme.error,
                      ),
                    );
                    // 登录失败时返回登录界面
                    Navigator.of(context).pushReplacement(
                      MaterialPageRoute(
                        builder: (context) => const LoginPage(),
                      ),
                    );
                  }
                }
              } else {
                // 如果没有保存密码，返回登录界面并填充用户信息
                await ref.read(authProvider.notifier).logout();
                if (context.mounted) {
                  Navigator.of(context).pushReplacement(
                    MaterialPageRoute(
                      builder: (context) => const LoginPage(),
                    ),
                  );
                }
              }
            },
          ),
        ],
      ),
    );
  }
}
