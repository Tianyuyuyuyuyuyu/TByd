import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'login_page.dart';
import 'package_list_page.dart';

class HomePage extends ConsumerStatefulWidget {
  const HomePage({super.key});

  @override
  ConsumerState<HomePage> createState() => _HomePageState();
}

class _HomePageState extends ConsumerState<HomePage> {
  int _selectedIndex = 0;

  Future<void> _handleLogout(BuildContext context) async {
    try {
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

      await ref.read(authProvider.notifier).logout();

      if (!mounted) return;
      Navigator.pop(context);

      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (context) => const LoginPage()),
        (route) => false,
      );
    } catch (e) {
      if (!mounted) return;
      Navigator.pop(context);

      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('退出登录失败：${e.toString()}'),
          backgroundColor: Theme.of(context).colorScheme.error,
          action: SnackBarAction(
            label: '重试',
            textColor: Colors.white,
            onPressed: () => _handleLogout(context),
          ),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final l10n = AppLocalizations.of(context);
    final theme = Theme.of(context);
    final authState = ref.watch(authProvider);

    return Scaffold(
      body: Row(
        children: [
          // 左侧导航栏
          Container(
            width: 72,
            decoration: BoxDecoration(
              color: theme.colorScheme.surface,
              border: Border(
                right: BorderSide(
                  color: theme.colorScheme.outlineVariant,
                  width: 1,
                ),
              ),
            ),
            child: Column(
              children: [
                // 头像和用户名
                Padding(
                  padding: const EdgeInsets.fromLTRB(8, 16, 8, 8),
                  child: Column(
                    children: [
                      CircleAvatar(
                        radius: 24,
                        backgroundColor: theme.colorScheme.primary,
                        child: Text(
                          authState.auth?.username.substring(0, 1).toUpperCase() ?? 'U',
                          style: TextStyle(
                            color: theme.colorScheme.onPrimary,
                            fontWeight: FontWeight.bold,
                            fontSize: 20,
                          ),
                        ),
                      ),
                      const SizedBox(height: 8),
                      Text(
                        authState.auth?.username ?? '',
                        style: theme.textTheme.bodySmall?.copyWith(
                          fontWeight: FontWeight.w500,
                        ),
                        overflow: TextOverflow.ellipsis,
                        textAlign: TextAlign.center,
                      ),
                    ],
                  ),
                ),
                const Divider(),
                // 导航按钮
                Expanded(
                  child: ListView(
                    padding: EdgeInsets.zero,
                    children: [
                      _buildNavButton(
                        context: context,
                        icon: Icons.inventory_2_outlined,
                        selectedIcon: Icons.inventory_2,
                        label: l10n.packages,
                        index: 0,
                      ),
                      _buildNavButton(
                        context: context,
                        icon: Icons.settings_outlined,
                        selectedIcon: Icons.settings,
                        label: l10n.settings,
                        index: 1,
                      ),
                    ],
                  ),
                ),
                // 底部登出按钮
                Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: IconButton(
                    icon: const Icon(Icons.logout),
                    onPressed: () => _handleLogout(context),
                    tooltip: l10n.logout,
                    style: IconButton.styleFrom(
                      foregroundColor: theme.colorScheme.error,
                    ),
                  ),
                ),
              ],
            ),
          ),
          // 右侧内容区域
          Expanded(
            child: Container(
              color: theme.colorScheme.surface,
              child: _buildContent(),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildNavButton({
    required BuildContext context,
    required IconData icon,
    required IconData selectedIcon,
    required String label,
    required int index,
  }) {
    final theme = Theme.of(context);
    final isSelected = _selectedIndex == index;

    return Tooltip(
      message: label,
      preferBelow: false,
      child: InkWell(
        onTap: () => setState(() => _selectedIndex = index),
        child: Container(
          height: 56,
          decoration: BoxDecoration(
            color: isSelected ? theme.colorScheme.primaryContainer : null,
            borderRadius: BorderRadius.circular(8),
          ),
          margin: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
          child: Icon(
            isSelected ? selectedIcon : icon,
            color: isSelected ? theme.colorScheme.primary : theme.colorScheme.onSurfaceVariant,
          ),
        ),
      ),
    );
  }

  Widget _buildContent() {
    switch (_selectedIndex) {
      case 0:
        return const PackageListPage();
      case 1:
        return const Center(
          child: Text('设置页面'),
        );
      default:
        return const Center(
          child: Text('未知页面'),
        );
    }
  }
}
