import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/login_history_model.dart';
import '../providers/auth_provider.dart';
import '../providers/login_history_provider.dart';
import '../pages/login_page.dart';
import 'dart:developer' as developer;

class LoginHistorySelector extends ConsumerWidget {
  const LoginHistorySelector({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
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
                ...server.users.map((user) => _buildUserTile(context, ref, server, user)),
              ],
            );
          },
        ),
      ),
      actions: [
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
        TextButton(
          onPressed: () => Navigator.of(context).pop(),
          child: const Text('取消'),
        ),
      ],
    );
  }

  Widget _buildUserTile(BuildContext context, WidgetRef ref, ServerHistory server, UserHistory user) {
    developer.log('Building user tile for ${user.username} on ${server.serverUrl}');
    return ListTile(
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
          TextButton.icon(
            icon: const Icon(Icons.login),
            label: const Text('使用此账号登录'),
            onPressed: () async {
              Navigator.of(context).pop();
              if (user.rememberPassword && user.savedPassword != null) {
                // 如果有保存的密码，直接登录
                try {
                  await ref.read(authProvider.notifier).login(
                        server.serverUrl,
                        user.username,
                        user.savedPassword!,
                        rememberMe: true,
                      );
                } catch (e) {
                  if (context.mounted) {
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
                // 否则返回登录界面并填充信息
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
