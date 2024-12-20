import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import '../utils/avatar.dart';

/// 用户头像组件
class UserAvatar extends ConsumerWidget {
  /// 头像大小
  final double size;

  /// 构造函数
  const UserAvatar({
    super.key,
    this.size = 40,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final authState = ref.watch(authProvider);
    final user = authState.user;

    if (user == null) {
      return CircleAvatar(
        radius: size / 2,
        backgroundColor: Theme.of(context).colorScheme.primary,
        child: Icon(
          Icons.person,
          size: size * 0.6,
          color: Theme.of(context).colorScheme.onPrimary,
        ),
      );
    }

    return CustomUserAvatar(
      username: user.username,
      size: size,
    );
  }
}
