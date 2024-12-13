import 'package:shared_preferences/shared_preferences.dart';
import 'dart:convert';

class AppConfigService {
  static const String _serverUrlKey = 'server_url';
  static const String _authTokenKey = 'auth_token';
  static const String _usernameKey = 'username';

  final SharedPreferences _prefs;

  AppConfigService(this._prefs);

  /// 获取当前登录的服务器地址
  String? get serverUrl => _prefs.getString(_serverUrlKey);

  /// 获取认证令牌
  String? get authToken => _prefs.getString(_authTokenKey);

  /// 获取当前登录用户名
  String? get username => _prefs.getString(_usernameKey);

  /// 检查是否已登录
  bool get isLoggedIn => serverUrl != null && authToken != null;

  /// 设置服务器地址
  Future<void> setServerUrl(String url) async {
    await _prefs.setString(_serverUrlKey, url);
  }

  /// 设置认证信息
  Future<void> setAuthInfo({
    required String username,
    required String authToken,
  }) async {
    await Future.wait([
      _prefs.setString(_authTokenKey, authToken),
      _prefs.setString(_usernameKey, username),
    ]);
  }

  /// 保存登录信息
  Future<void> saveLoginInfo({
    required String serverUrl,
    required String username,
    required String authToken,
  }) async {
    await Future.wait([
      setServerUrl(serverUrl),
      setAuthInfo(username: username, authToken: authToken),
    ]);
  }

  /// 清除所有认证信息
  Future<void> clearAuthInfo() async {
    await Future.wait([
      _prefs.remove(_serverUrlKey),
      _prefs.remove(_authTokenKey),
      _prefs.remove(_usernameKey),
    ]);
  }

  /// 获取完整的服务器配置
  Map<String, String?> get serverConfig => {
        'serverUrl': serverUrl,
        'username': username,
        'authToken': authToken,
      };
}
