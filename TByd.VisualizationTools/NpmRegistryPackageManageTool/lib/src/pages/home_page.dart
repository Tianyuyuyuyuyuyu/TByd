import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import '../providers/package_provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package_search_page.dart';
import 'login_page.dart';

class HomePage extends ConsumerWidget {
  const HomePage({super.key});

  Future<void> _handleLogout(BuildContext context, WidgetRef ref) async {
    try {
      // 显示加载指示器
      showDialog(
        context: context,
        barrierDismissible: false,
        builder: (context) => WillPopScope(
          onWillPop: () async => false,
          child: const Center(
            child: CircularProgressIndicator(),
          ),
        ),
      );

      // 先清除所有状态
      ref.invalidate(searchProvider);
      ref.invalidate(packageServiceProvider);

      // 执行登出操作
      await ref.read(authProvider.notifier).logout();

      if (!context.mounted) return;

      // 关闭加载指示器
      Navigator.pop(context);

      // 使用 pushAndRemoveUntil 确保清除导航栈
      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (context) => const LoginPage()),
        (route) => false,
      );
    } catch (e) {
      if (!context.mounted) return;

      // 关闭加载指示器
      Navigator.pop(context);

      // 显示错误消息
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('退出登录失败：${e.toString()}'),
          backgroundColor: Theme.of(context).colorScheme.error,
          action: SnackBarAction(
            label: '重试',
            textColor: Colors.white,
            onPressed: () => _handleLogout(context, ref),
          ),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final l10n = AppLocalizations.of(context);
    final authState = ref.watch(authProvider);
    final theme = Theme.of(context);

    return DefaultTabController(
      length: 2,
      child: Scaffold(
        appBar: AppBar(
          title: Text(l10n.appTitle),
          actions: [
            IconButton(
              icon: const Icon(Icons.language),
              onPressed: () {
                Navigator.pushNamed(context, '/settings/language');
              },
              tooltip: l10n.language,
            ),
            IconButton(
              icon: const Icon(Icons.logout),
              onPressed: () => _handleLogout(context, ref),
              tooltip: l10n.logout,
            ),
          ],
          bottom: TabBar(
            tabs: [
              Tab(
                icon: const Icon(Icons.search),
                text: l10n.searchPackages,
              ),
              Tab(
                icon: const Icon(Icons.settings),
                text: l10n.settings,
              ),
            ],
          ),
        ),
        body: TabBarView(
          children: [
            const PackageSearchPage(),
            Center(
              child: Text(
                l10n.settingsPageUnderConstruction,
                style: theme.textTheme.bodyLarge,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
