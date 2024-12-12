import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import 'package_search_screen.dart';

class HomeScreen extends ConsumerWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final user = ref.watch(authProvider.notifier).user;

    return Scaffold(
      appBar: AppBar(
        title: const Text('NPM Registry Manager'),
        actions: [
          PopupMenuButton<String>(
            onSelected: (value) async {
              if (value == 'logout') {
                await ref.read(authProvider.notifier).logout();
              }
            },
            itemBuilder: (BuildContext context) => [
              PopupMenuItem(
                value: 'profile',
                child: ListTile(
                  leading: const Icon(Icons.person),
                  title: Text(user?.username ?? ''),
                  subtitle: Text(user?.email ?? ''),
                ),
              ),
              const PopupMenuItem(
                value: 'logout',
                child: ListTile(
                  leading: Icon(Icons.logout),
                  title: Text('退出登录'),
                ),
              ),
            ],
          ),
        ],
      ),
      body: DefaultTabController(
        length: 2,
        child: Column(
          children: [
            Container(
              color: Theme.of(context).primaryColor,
              child: const TabBar(
                tabs: [
                  Tab(
                    icon: Icon(Icons.search),
                    text: '搜索包',
                  ),
                  Tab(
                    icon: Icon(Icons.settings),
                    text: '设置',
                  ),
                ],
              ),
            ),
            const Expanded(
              child: TabBarView(
                children: [
                  PackageSearchScreen(),
                  Center(child: Text('设置页面开发中...')),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }
}
