/// NPM Registry Manager - 应用配置服务
///
/// 该文件提供应用程序配置的管理功能，包括：
/// - 服务器配置管理
/// - 认证信息存储
/// - 用户会话管理
/// - 配置持久化
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:shared_preferences/shared_preferences.dart';

/// 应用配置服务类
///
/// 提供应用程序配置的管理功能，包括：
/// - 服务器配置的存储和读取
/// - 认证信息的管理
/// - 用户会话的维护
class AppConfigService {
  /// 服务器URL存储键
  static const String _serverUrlKey = 'server_url';

  /// 认证令牌存储键
  static const String _authTokenKey = 'auth_token';

  /// 用户名存储键
  static const String _usernameKey = 'username';

  /// SharedPreferences实例
  final SharedPreferences _prefs;

  /// 构造函数
  ///
  /// [_prefs] SharedPreferences实例，用于配置的持久化存储
  AppConfigService(this._prefs);

  /// 获取当前登录的服务器地址
  ///
  /// 返回已保存的NPM仓库服务器URL
  /// 如果未设置则返回null
  String? get serverUrl => _prefs.getString(_serverUrlKey);

  /// 获取认证令牌
  ///
  /// 返回当前用户的认证令牌
  /// 如果未登录则返回null
  String? get authToken => _prefs.getString(_authTokenKey);

  /// 获取当前登录用户名
  ///
  /// 返回当前登录的用户名
  /// 如果未登录则返回null
  String? get username => _prefs.getString(_usernameKey);

  /// 检查是否已登录
  ///
  /// 通过验证服务器地址和认证令牌是否存在
  /// 来判断用户是否已登录
  bool get isLoggedIn => serverUrl != null && authToken != null;

  /// 设置服务器地址
  ///
  /// 保存NPM仓库服务器的URL地址
  /// [url] 服务器URL
  Future<void> setServerUrl(String url) async {
    await _prefs.setString(_serverUrlKey, url);
  }

  /// 设置认证信息
  ///
  /// 保存用户的认证信息
  ///
  /// 参数：
  /// - [username] 用户名
  /// - [authToken] 认证令牌
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
  ///
  /// 保存完整的登录会话信息
  ///
  /// 参数：
  /// - [serverUrl] 服务器URL
  /// - [username] 用户名
  /// - [authToken] 认证令牌
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

  /// 清除认证信息
  ///
  /// 清除所有存储的认证相关信息
  /// 用于用户登出时清理会话数据
  Future<void> clearAuthInfo() async {
    await Future.wait([
      _prefs.remove(_serverUrlKey),
      _prefs.remove(_authTokenKey),
      _prefs.remove(_usernameKey),
    ]);
  }

  /// 获取服务器配置
  ///
  /// 返回包含所有服务器配置信息的Map
  /// 包括服务器URL、用户名和认证令牌
  Map<String, String?> get serverConfig => {
        'serverUrl': serverUrl,
        'username': username,
        'authToken': authToken,
      };
}
