/// NPM Registry Manager - 认证服务
///
/// 该文件提供NPM仓库的认证相关功能，包括：
/// - 用户登录认证
/// - 用户注册
/// - 会话管理
/// - 令牌验证
/// - 安全限制
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'dart:convert';
import 'dart:math';
import 'package:http/http.dart' as http;
import '../models/auth_model.dart';
import '../utils/constants.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'login_history_service.dart';
import 'encryption_service.dart';
import 'storage_service.dart';
import 'dart:developer' as developer;
import 'package:flutter/foundation.dart';

/// 认证异常类
///
/// 用于处理认证过程中的各种错误情况
/// 包含错误消息和错误代码
class AuthenticationException implements Exception {
  /// 错误消息
  final String message;

  /// 错误代码
  final String code;

  /// 构造函数
  AuthenticationException(this.message, this.code);

  @override
  String toString() => message;
}

/// 认证服务类
///
/// 提供完整的NPM仓库认证功能，包括：
/// - 用户认证
/// - 会话管理
/// - 安全限制
/// - 令牌管理
class AuthService {
  /// 会话前缀
  static const _sessionPrefix = 'session_';

  /// 令牌前缀
  static const _tokenPrefix = 'token_';

  /// 安全随机数生成器
  static final _random = Random.secure();

  /// HTTP客户端
  final http.Client _client;

  /// 登录历史服务
  final LoginHistoryService _historyService;

  /// 加密服务
  final EncryptionService _encryptionService;

  /// 登录尝试次数记录
  final Map<String, int> _loginAttempts = {};

  /// 账户锁定时间记录
  final Map<String, DateTime> _lockoutTimes = {};

  /// 构造函数
  ///
  /// 初始化认证服务的必要组件
  ///
  /// 参数：
  /// - [client] HTTP客户端，可选
  /// - [encryptionService] 加密服务，可选
  /// - [prefs] SharedPreferences实例，必需
  AuthService({
    http.Client? client,
    EncryptionService? encryptionService,
    required SharedPreferences prefs,
  })  : _client = client ?? http.Client(),
        _historyService = LoginHistoryService(prefs),
        _encryptionService = encryptionService ?? EncryptionService(StorageService(prefs)) {
    _init();
  }

  /// 初始化服务
  ///
  /// 执行必要的初始化操作：
  /// - 初始化加密服务
  /// - 清理过期会话
  Future<void> _init() async {
    await _encryptionService.init();
    await _cleanupExpiredSessions();
  }

  /// 检查登录尝试次数
  ///
  /// 实现登录限制功能：
  /// - 检查是否超过最大尝试次数
  /// - 检查是否在锁定期内
  /// - 管理锁定状态
  ///
  /// [username] 要检查的用户名
  /// 返回是否允许继续尝试登录
  bool _checkLoginAttempts(String username) {
    final attempts = _loginAttempts[username] ?? 0;
    final lockoutTime = _lockoutTimes[username];

    if (lockoutTime != null) {
      final now = DateTime.now();
      if (now.difference(lockoutTime).inMinutes < StorageKeys.loginLockoutMinutes) {
        return false;
      }
      // 锁定时间已过，重置计数
      _loginAttempts.remove(username);
      _lockoutTimes.remove(username);
      return true;
    }

    if (attempts >= StorageKeys.maxLoginAttempts) {
      _lockoutTimes[username] = DateTime.now();
      return false;
    }

    return true;
  }

  /// 记录登录失败
  ///
  /// 增加指定用户的登录失败计数
  /// [username] 失败登录的用户名
  void _recordLoginFailure(String username) {
    final attempts = _loginAttempts[username] ?? 0;
    _loginAttempts[username] = attempts + 1;
  }

  /// 重置登录尝试
  ///
  /// 清除指定用户的登录尝试记录和锁定状态
  /// [username] 要重置的用户名
  void _resetLoginAttempts(String username) {
    _loginAttempts.remove(username);
    _lockoutTimes.remove(username);
  }

  /// 清理过期会话
  ///
  /// 定期清理系统中的过期会话数据：
  /// - 检查所有会话的过期时间
  /// - 删除过期的会话数据
  /// - 清理相关的令牌信息
  Future<void> _cleanupExpiredSessions() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final keys = prefs.getKeys();
      final now = DateTime.now();

      for (final key in keys) {
        if (key.startsWith(_sessionPrefix)) {
          final sessionId = key.substring(_sessionPrefix.length);
          final sessionTime = prefs.getString('${_sessionPrefix}time_$sessionId');

          if (sessionTime != null) {
            final sessionDateTime = DateTime.parse(sessionTime);
            if (now.difference(sessionDateTime).inHours >= StorageKeys.sessionExpirationHours) {
              // 清除过期会话
              await prefs.remove(key);
              await prefs.remove('$_tokenPrefix$sessionId');
              await prefs.remove('${_sessionPrefix}time_$sessionId');
            }
          }
        }
      }
    } catch (e) {
      developer.log('Failed to cleanup expired sessions', error: e);
    }
  }

  /// 用户登录
  ///
  /// 处理用户登录请求：
  /// - 验证用户凭据
  /// - 处理服务器响应
  /// - 管理认证状态
  /// - 处理错误情况
  ///
  /// 参数：
  /// - [serverUrl] NPM仓库服务器地址
  /// - [username] 用户名
  /// - [password] 密码
  ///
  /// 返回：
  /// 成功时返回 [AuthModel] 实例
  /// 失败时抛出 [AuthenticationException]
  Future<AuthModel> login(
    String serverUrl,
    String username,
    String password,
  ) async {
    try {
      // 处理服务器地址
      String processedUrl = serverUrl.trim();
      if (!processedUrl.startsWith('http://') && !processedUrl.startsWith('https://')) {
        processedUrl = 'http://$processedUrl';
      }

      // 确保URL末尾没有斜杠
      processedUrl = processedUrl.endsWith('/') ? processedUrl.substring(0, processedUrl.length - 1) : processedUrl;

      // 创建 Basic Auth 凭据
      final basicAuth = base64Encode(utf8.encode('$username:$password'));

      // 尝试直接登录
      final loginResponse = await _client.get(
        Uri.parse('$processedUrl/-/user/org.couchdb.user:$username'),
        headers: {
          'Authorization': 'Basic $basicAuth',
          'Accept': 'application/json',
        },
      );

      if (loginResponse.statusCode == 200 || loginResponse.statusCode == 201) {
        // 如果登录成功，直接使用 Basic Auth 作为 token
        return AuthModel(
          token: basicAuth,
          username: username,
          serverUrl: processedUrl,
          expiresAt: DateTime.now().add(
            const Duration(hours: 24),
          ),
        );
      }

      // 如果登录失败，尝试注册
      if (loginResponse.statusCode == 404) {
        final registerResponse = await _client.put(
          Uri.parse('$processedUrl/-/user/org.couchdb.user:$username'),
          headers: {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
          },
          body: json.encode({
            'name': username,
            'password': password,
            '_id': 'org.couchdb.user:$username',
            'type': 'user',
            'roles': [],
            'date': DateTime.now().toIso8601String(),
          }),
        );

        if (registerResponse.statusCode == 201 || registerResponse.statusCode == 200) {
          // 注册成功后，使用 Basic Auth 作为 token
          return AuthModel(
            token: basicAuth,
            username: username,
            serverUrl: processedUrl,
            expiresAt: DateTime.now().add(
              const Duration(hours: 24),
            ),
          );
        }

        // 处理注册失败
        Map<String, dynamic> errorData;
        try {
          errorData = json.decode(registerResponse.body);
        } catch (e) {
          errorData = {'message': '未知错误'};
        }

        switch (registerResponse.statusCode) {
          case 409:
            throw AuthenticationException(
              '用户名已存在',
              'AUTH001',
            );
          case 400:
            throw AuthenticationException(
              errorData['message'] ?? '请求参数错误',
              'AUTH004',
            );
          default:
            throw AuthenticationException(
              errorData['message'] ?? '注册失败：${registerResponse.statusCode}',
              'AUTH999',
            );
        }
      }

      // 处理登录失败
      Map<String, dynamic> errorData;
      try {
        errorData = json.decode(loginResponse.body);
      } catch (e) {
        errorData = {'message': '未知错误'};
      }

      switch (loginResponse.statusCode) {
        case 401:
          throw AuthenticationException(
            '用户名或密码错误',
            'AUTH001',
          );
        case 404:
          throw AuthenticationException(
            '服务器地址无效',
            'AUTH002',
          );
        case 429:
          throw AuthenticationException(
            '登录尝试次数过多，请稍后再试',
            'AUTH003',
          );
        case 400:
          throw AuthenticationException(
            errorData['message'] ?? '请求参数错误',
            'AUTH004',
          );
        default:
          throw AuthenticationException(
            errorData['message'] ?? '登录失败：${loginResponse.statusCode}',
            'AUTH999',
          );
      }
    } catch (e) {
      if (e is AuthenticationException) rethrow;

      if (e.toString().contains('SocketException')) {
        throw AuthenticationException('无法连接到服务器', 'NET001');
      }

      throw AuthenticationException(
        '登录过程中发生错误：${e.toString()}',
        'SYS001',
      );
    }
  }

  Future<void> logoutUser(String serverUrl, String? token) async {
    if (token == null || token.isEmpty) {
      throw AuthenticationException('无效的认证信息', 'AUTH001');
    }

    try {
      // 先验证当前认证状态
      final isValid = await validateToken(serverUrl, token);
      if (!isValid) {
        // 如果认证已失效，清除本地状态
        await logout();
        return;
      }

      // 验证用户名（从 Basic Auth token 中获取）
      final credentials = String.fromCharCodes(base64.decode(token));
      final username = credentials.split(':')[0];

      // 检查用户配置文件
      final response = await _client.get(
        Uri.parse('$serverUrl/-/user/org.couchdb.user:$username'),
        headers: {
          'Authorization': 'Basic $token',
          'Accept': 'application/json',
        },
      );

      if (response.statusCode != 200) {
        throw AuthenticationException('验证用户信息失败', 'AUTH002');
      }

      // 清除本地会话
      await logout();
    } catch (e) {
      if (e is AuthenticationException) rethrow;
      throw AuthenticationException('注销时发生错误：${e.toString()}', 'SYS002');
    }
  }

  Future<bool> validateToken(String serverUrl, String? token) async {
    if (token == null || token.isEmpty) return false;

    try {
      // 从 Basic Auth token 中获取用户名
      final credentials = String.fromCharCodes(base64.decode(token));
      final username = credentials.split(':')[0];

      // 验证用户配置文件
      final response = await _client.get(
        Uri.parse('$serverUrl/-/user/org.couchdb.user:$username'),
        headers: {
          'Authorization': 'Basic $token',
          'Accept': 'application/json',
        },
      );

      return response.statusCode == 200;
    } catch (e) {
      developer.log('Token validation failed', error: e);
      return false;
    }
  }

  Future<String?> getDecryptedPassword(String? encryptedPassword) async {
    if (encryptedPassword == null || encryptedPassword.isEmpty) return null;
    try {
      return await _encryptionService.decrypt(encryptedPassword);
    } catch (e) {
      debugPrint('Failed to decrypt password: $e');
      return null;
    }
  }

  Future<void> logout() async {
    developer.log('Starting logout process');
    try {
      final prefs = await SharedPreferences.getInstance();

      // 获取当前会话
      final sessionId = prefs.getString(StorageKeys.currentSession);
      if (sessionId != null) {
        // 清除会话相关信息
        await prefs.remove('$_sessionPrefix$sessionId');
        await prefs.remove('$_tokenPrefix$sessionId');
        await prefs.remove('${_sessionPrefix}time_$sessionId');
        await prefs.remove(StorageKeys.currentSession);
      }

      // 清除其他敏感信息
      await prefs.remove(StorageKeys.authToken);
      await prefs.remove(StorageKeys.serverUrl);

      developer.log('Logout completed successfully');
    } catch (e) {
      developer.log('Logout error: $e', error: e);
    }
  }

  Future<bool> isAuthenticated() async {
    developer.log('Checking authentication status');
    try {
      final prefs = await SharedPreferences.getInstance();
      final sessionId = prefs.getString(StorageKeys.currentSession);
      if (sessionId == null) {
        developer.log('No session found');
        return false;
      }

      final token = prefs.getString('$_tokenPrefix$sessionId');
      final serverUrl = prefs.getString(StorageKeys.serverUrl);

      if (token == null || serverUrl == null) {
        developer.log('No token or server URL found');
        return false;
      }

      // 检查话是否过期
      final sessionTime = prefs.getString('${_sessionPrefix}time_$sessionId');
      if (sessionTime != null) {
        final sessionDateTime = DateTime.parse(sessionTime);
        if (DateTime.now().difference(sessionDateTime).inHours >= StorageKeys.sessionExpirationHours) {
          developer.log('Session expired');
          // 清除过期会话
          await prefs.remove('$_sessionPrefix$sessionId');
          await prefs.remove('$_tokenPrefix$sessionId');
          await prefs.remove('${_sessionPrefix}time_$sessionId');
          await prefs.remove(StorageKeys.currentSession);
          return false;
        }
      }

      final response = await _client.get(
        Uri.parse('$serverUrl${ApiConstants.userProfile}${token.split(':')[0]}'),
        headers: {
          'Authorization': 'Basic $token',
          'Accept': 'application/json',
          'X-Requested-With': 'XMLHttpRequest',
        },
      );

      final isAuth = response.statusCode == 200;
      developer.log('Authentication check result: $isAuth');

      if (!isAuth) {
        // 如果认证失败，清除会话
        await prefs.remove('$_sessionPrefix$sessionId');
        await prefs.remove('$_tokenPrefix$sessionId');
        await prefs.remove('${_sessionPrefix}time_$sessionId');
        await prefs.remove(StorageKeys.currentSession);
      }

      return isAuth;
    } catch (e) {
      developer.log('Authentication check failed', error: e);
      return false;
    }
  }

  /// 清理所有会话数据
  Future<void> clearAllSessions() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      final keys = prefs.getKeys();

      // 清除所有会话和令牌
      for (final key in keys) {
        if (key.startsWith(_sessionPrefix) || key.startsWith(_tokenPrefix)) {
          await prefs.remove(key);
        }
      }

      await prefs.remove(StorageKeys.currentSession);
      developer.log('All sessions cleared successfully');
    } catch (e) {
      developer.log('Failed to clear sessions', error: e);
    }
  }

  void dispose() {
    _client.close();
  }

  /// 加密密码
  Future<String?> encryptPassword(String password) async {
    if (password.isEmpty) return null;
    try {
      return await _encryptionService.encrypt(password);
    } catch (e) {
      debugPrint('Failed to encrypt password: $e');
      return null;
    }
  }
}
