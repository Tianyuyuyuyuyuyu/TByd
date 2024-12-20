/// NPM Registry Manager - TByd Team
///
/// 这是一个用于管理NPM包注册表的Flutter应用程序的入口文件。
/// 该应用支持多语言本地化、主题切换和用户认证功能。
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

// 核心Flutter框架
import 'package:flutter/material.dart';

// 状态管理
import 'package:flutter_riverpod/flutter_riverpod.dart';

// 国际化支持
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

// 本地存储
import 'package:shared_preferences/shared_preferences.dart';

// 自定义providers
import 'src/providers/locale_provider.dart';
import 'src/providers/auth_provider.dart';
import 'src/providers/unity_version_provider.dart';

// 页面路由
import 'src/pages/login_page.dart';
import 'src/pages/home_page.dart';
import 'src/pages/settings/language_settings_page.dart';

/// 应用程序入口点
///
/// 初始化必要的服务和配置，包括：
/// 1. Flutter绑定初始化
/// 2. SharedPreferences实例获取
/// 3. 使用ProviderScope包装整个应用以支持状态管理
Future<void> main() async {
  // 确保Flutter绑定初始化
  WidgetsFlutterBinding.ensureInitialized();

  // 获取SharedPreferences实例用于本地数据存储
  final prefs = await SharedPreferences.getInstance();

  // 运行应用程序
  runApp(
    ProviderScope(
      overrides: [
        // 注入SharedPreferences实例
        sharedPreferencesProvider.overrideWithValue(prefs),
      ],
      child: const MyApp(),
    ),
  );
}

/// 应用程序根组件
///
/// 负责配置应用程序的基础设置，包括：
/// - 主题配置（支持亮色和暗色主题）
/// - 国际化设置
/// - 路由配置
/// - 认证状态管理
class MyApp extends ConsumerWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // 监听当前语言环境
    final locale = ref.watch(localeProvider);
    // 监听认证状态
    final authState = ref.watch(authProvider);
    // 监听 Unity 版本加载状态
    final unityVersionState = ref.watch(unityVersionProvider);

    return MaterialApp(
      title: 'NPM Registry Manager',
      debugShowCheckedModeBanner: false,
      // 配置亮色主题
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(
          seedColor: Colors.blue,
          brightness: Brightness.light,
        ),
        useMaterial3: true,
      ),
      // 配置暗色主题
      darkTheme: ThemeData(
        colorScheme: ColorScheme.fromSeed(
          seedColor: Colors.blue,
          brightness: Brightness.dark,
        ),
        useMaterial3: true,
      ),
      // 使用系统主题模式
      themeMode: ThemeMode.system,
      // 设置当前语言环境
      locale: locale,
      // 配置国际化代理
      localizationsDelegates: const [
        AppLocalizations.delegate,
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate,
      ],
      // 支持的语言列表
      supportedLocales: LocaleNotifier.supportedLocales,
      // 根据 Unity 版本加载状态和认证状态决定显示的页面
      home: Builder(
        builder: (context) {
          if (unityVersionState.error != null) {
            // Unity 版本获取失败，显示错误页面
            return Scaffold(
              body: Center(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    const Icon(
                      Icons.error_outline,
                      size: 64,
                      color: Colors.red,
                    ),
                    const SizedBox(height: 16),
                    Text(
                      'Unity 版本数据获取失败',
                      style: Theme.of(context).textTheme.titleLarge,
                    ),
                    const SizedBox(height: 8),
                    Text(
                      unityVersionState.error!,
                      style: Theme.of(context).textTheme.bodyMedium,
                      textAlign: TextAlign.center,
                    ),
                    const SizedBox(height: 24),
                    FilledButton(
                      onPressed: () {
                        ref.read(unityVersionProvider.notifier).refreshVersions();
                      },
                      child: const Text('重试'),
                    ),
                  ],
                ),
              ),
            );
          }

          if (unityVersionState.isLoading || unityVersionState.versions == null) {
            // Unity 版本正在加载，显示加载页面
            return Scaffold(
              body: Center(
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    const CircularProgressIndicator(),
                    const SizedBox(height: 16),
                    Text(
                      '正在爬取Unity中国官网所有 Unity 版本数据...',
                      style: Theme.of(context).textTheme.titleMedium,
                    ),
                    const SizedBox(height: 8),
                    Text(
                      'Made By 田雨 - TByd',
                      style: Theme.of(context).textTheme.titleMedium?.copyWith(
                            color: Theme.of(context).colorScheme.onSurface.withOpacity(0.6),
                            fontWeight: FontWeight.bold,
                          ),
                    ),
                  ],
                ),
              ),
            );
          }

          // Unity 版本加载完成，根据认证状态显示对应页面
          return authState.isAuthenticated ? const HomePage() : const LoginPage();
        },
      ),
      // 注册路由表
      routes: {
        '/login': (context) => const LoginPage(),
        '/home': (context) => const HomePage(),
        '/settings/language': (context) => const LanguageSettingsPage(),
      },
      // 处理未知路由
      onUnknownRoute: (settings) {
        return MaterialPageRoute(
          builder: (context) => const LoginPage(),
        );
      },
    );
  }
}
