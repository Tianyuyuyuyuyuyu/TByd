/// NPM Registry Manager - 主页
///
/// 该文件实现了应用程序的主页界面，包括：
/// - 左侧导航栏
/// - 用户信息展示
/// - 页面导航
/// - 登出功能
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'login_page.dart';
import 'package_list_page.dart';
import 'package_operations_page.dart';
import 'test_page.dart';

/// 主页组件
///
/// 提供应用程序的主界面，包括：
/// - 导航功能
/// - 用户管理
/// - 内容展示
class HomePage extends ConsumerStatefulWidget {
  const HomePage({super.key});

  @override
  ConsumerState<HomePage> createState() => _HomePageState();
}

/// 主页状态类
///
/// 管理主页的状态和行为，包括：
/// - 页面切换
/// - 用户登出
/// - 导航状态
class _HomePageState extends ConsumerState<HomePage> {
  /// 当前选中的导航项索引
  int _selectedIndex = 0;

  /// 处理用户登出
  ///
  /// 显示加载指示器，执行登出操作，
  /// 成功后跳转到登录页面，失败则显示错误信息
  Future<void> _handleLogout(BuildContext context) async {
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

      // 执行登出操作
      await ref.read(authProvider.notifier).logout();

      if (!mounted) return;
      Navigator.pop(context); // 关闭加载指示器

      // 跳转到登录页面
      Navigator.pushAndRemoveUntil(
        context,
        MaterialPageRoute(builder: (context) => const LoginPage()),
        (route) => false,
      );
    } catch (e) {
      if (!mounted) return;
      Navigator.pop(context); // 关闭加载指示器

      // 显示错误信息
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
                // 用户头像和名称
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
                // 导航按钮列表
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
                        icon: Icons.build_outlined,
                        selectedIcon: Icons.build,
                        label: '包操作',
                        index: 1,
                      ),
                      _buildNavButton(
                        context: context,
                        icon: Icons.settings_outlined,
                        selectedIcon: Icons.settings,
                        label: l10n.settings,
                        index: 2,
                      ),
                      _buildNavButton(
                        context: context,
                        icon: Icons.science_outlined,
                        selectedIcon: Icons.science,
                        label: '测试',
                        index: 3,
                      ),
                    ],
                  ),
                ),
                // 登出按钮
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
          // 主内容区域
          Expanded(
            child: _buildContent(),
          ),
        ],
      ),
    );
  }

  /// 构建导航按钮
  ///
  /// [context] 构建上下文
  /// [icon] 未选中时的图标
  /// [selectedIcon] 选中时的图标
  /// [label] 按钮文本
  /// [index] 导航索引
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

  /// 构建主内容区域
  ///
  /// 根据当前选中的导航索引显示相应的页面内容
  Widget _buildContent() {
    switch (_selectedIndex) {
      case 0:
        return const PackageListPage();
      case 1:
        return const PackageOperationsPage();
      case 2:
        return const Center(
          child: Text('设置页面'),
        );
      case 3:
        return const TestPage();
      default:
        return const Center(
          child: Text('未知页面'),
        );
    }
  }
}
