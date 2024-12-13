import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '../services/app_config_service.dart';

final sharedPreferencesProvider = Provider<SharedPreferences>((ref) {
  throw UnimplementedError('需要在应用启动时初始化 SharedPreferences');
});

final appConfigServiceProvider = Provider<AppConfigService>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  return AppConfigService(prefs);
});

/// 服务器地址提供者
final serverUrlProvider = Provider<String?>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.serverUrl;
});

/// 认证令牌提供者
final authTokenProvider = Provider<String?>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.authToken;
});

/// 用户名提供者
final usernameProvider = Provider<String?>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.username;
});

/// 登录状态提供者
final isLoggedInProvider = Provider<bool>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.isLoggedIn;
});

/// 服务器配置提供者
final serverConfigProvider = Provider<Map<String, String?>>((ref) {
  final configService = ref.watch(appConfigServiceProvider);
  return configService.serverConfig;
});
