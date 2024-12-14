/// NPM Registry Manager - 应用配置管理
///
/// 该文件负责管理应用程序的全局配置状态，包括：
/// - 服务器连接配置
/// - 用户认证信息
/// - 登录状态
/// - 全局配置项
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../services/app_config_service.dart';

/// 全局 SharedPreferences 提供者
///
/// 需要在应用程序启动时通过 ProviderScope 提供实例
/// 用于持久化存储应用配置
final sharedPreferencesProvider = Provider<SharedPreferences>((ref) {
  throw UnimplementedError('需要在应用启动时初始化 SharedPreferences');
});

/// 应用配置服务提供者
///
/// 提供 [AppConfigService] 实例，用于管理应用程序配置
/// 依赖于 [SharedPreferences] 进行配置的持久化存储
final appConfigServiceProvider = Provider<AppConfigService>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  return AppConfigService(prefs);
});

/// 服务器地址提供者
///
/// 提供当前配置的 NPM 仓库服务器地址
/// 如果未配置则返回 null
final serverUrlProvider = Provider<String?>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.serverUrl;
});

/// 认证令牌提供者
///
/// 提供当前用户的认证令牌
/// 用于进行身份验证和授权
/// 如果用户未登录则返回 null
final authTokenProvider = Provider<String?>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.authToken;
});

/// 用户名提供者
///
/// 提供当前登录用户的用户名
/// 如果用户未登录则返回 null
final usernameProvider = Provider<String?>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.username;
});

/// 登录状态提供者
///
/// 提供当前用户的登录状态
/// true 表示已登录，false 表示未登录
final isLoggedInProvider = Provider<bool>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.isLoggedIn;
});

/// 服务器配置提供者
///
/// 提供服务器相关的配置信息
/// 返回一个包含服务器配置键值对的 Map
/// 包括：
/// - 服务器地址
/// - API 版本
/// - 连接超时设置
/// - 其他服务器特定配置
final serverConfigProvider = Provider<Map<String, String?>>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.serverConfig;
});
