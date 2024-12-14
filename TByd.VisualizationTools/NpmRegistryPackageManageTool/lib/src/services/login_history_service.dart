/// NPM Registry Manager - 登录历史服务
///
/// 该文件提供用户登录历史的管理功能，包括：
/// - 登录历史记录的存储和读取
/// - 多服务器登录信息管理
/// - 用户登录信息维护
/// - 历史记录清理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import '../models/login_history_model.dart';

/// 登录历史服务类
///
/// 提供完整的登录历史管理功能，包括：
/// - 历史记录的存储和检索
/// - 服务器和用户信息的管理
/// - 登录信息的更新和维护
class LoginHistoryService {
  /// 历史记录存储键名
  static const String _historyKey = 'login_history';

  /// SharedPreferences实例
  final SharedPreferences _prefs;

  /// 构造函数
  ///
  /// [_prefs] SharedPreferences实例，用于数据持久化
  LoginHistoryService(this._prefs);

  /// 获取登录历史
  ///
  /// 从本地存储读取完整的登录历史记录
  /// 如果读取失败或没有历史记录，返回空的历史记录
  Future<LoginHistory> getHistory() async {
    final jsonStr = _prefs.getString(_historyKey);
    if (jsonStr == null) return const LoginHistory();

    try {
      final json = jsonDecode(jsonStr) as Map<String, dynamic>;
      return LoginHistory.fromJson(json);
    } catch (e) {
      return const LoginHistory();
    }
  }

  /// 保存登录信息
  ///
  /// 将新的登录信息添加到历史记录中
  /// 如果服务器或用户已存在，则更新相应信息
  ///
  /// 参数：
  /// - [serverUrl] 服务器地址
  /// - [username] 用户名
  /// - [password] 密码（可选）
  /// - [rememberPassword] 是否记住密码
  Future<void> saveLoginInfo(
    String serverUrl,
    String username,
    String? password,
    bool rememberPassword,
  ) async {
    final history = await getHistory();
    final now = DateTime.now();

    // 查找或创建服务器记录
    final serverIndex = history.servers.indexWhere((s) => s.serverUrl == serverUrl);
    final List<ServerHistory> updatedServers = List.from(history.servers);

    if (serverIndex >= 0) {
      final server = history.servers[serverIndex];
      final userIndex = server.users.indexWhere((u) => u.username == username);
      final List<UserHistory> updatedUsers = List.from(server.users);

      if (userIndex >= 0) {
        // 更新现有用户
        updatedUsers[userIndex] = UserHistory(
          username: username,
          rememberPassword: rememberPassword,
          savedPassword: rememberPassword ? password : null,
          lastLogin: now,
        );
      } else {
        // 添加新用户
        updatedUsers.add(UserHistory(
          username: username,
          rememberPassword: rememberPassword,
          savedPassword: rememberPassword ? password : null,
          lastLogin: now,
        ));
      }

      updatedServers[serverIndex] = ServerHistory(
        serverUrl: serverUrl,
        serverName: Uri.parse(serverUrl).host,
        users: updatedUsers,
        lastUsed: now,
      );
    } else {
      // 添加新服务器
      updatedServers.add(ServerHistory(
        serverUrl: serverUrl,
        serverName: Uri.parse(serverUrl).host,
        users: [
          UserHistory(
            username: username,
            rememberPassword: rememberPassword,
            savedPassword: rememberPassword ? password : null,
            lastLogin: now,
          ),
        ],
        lastUsed: now,
      ));
    }

    // 按最后使用时间排序
    updatedServers.sort((a, b) => b.lastUsed.compareTo(a.lastUsed));

    final updatedHistory = LoginHistory(
      servers: updatedServers,
      lastServerUrl: serverUrl,
      lastUsername: username,
    );

    await _prefs.setString(_historyKey, jsonEncode(updatedHistory.toJson()));
  }

  /// 移除服务器记录
  ///
  /// 从历史记录中删除指定服务器的所有信息
  ///
  /// 参数：
  /// - [serverUrl] 要删除的服务器地址
  Future<void> removeServer(String serverUrl) async {
    final history = await getHistory();
    final updatedServers = history.servers.where((s) => s.serverUrl != serverUrl).toList();

    final updatedHistory = LoginHistory(
      servers: updatedServers,
      lastServerUrl: history.lastServerUrl == serverUrl ? null : history.lastServerUrl,
      lastUsername: history.lastServerUrl == serverUrl ? null : history.lastUsername,
    );

    await _prefs.setString(_historyKey, jsonEncode(updatedHistory.toJson()));
  }

  /// 移除用户记录
  ///
  /// 从指定服务器中删除特定用户的登录记录
  /// 如果服务器没有其他用户，则同时删除服务器记录
  ///
  /// 参数：
  /// - [serverUrl] 服务器地址
  /// - [username] 要删除的用户名
  Future<void> removeUser(String serverUrl, String username) async {
    final history = await getHistory();
    final updatedServers = List<ServerHistory>.from(history.servers);

    final serverIndex = updatedServers.indexWhere((s) => s.serverUrl == serverUrl);
    if (serverIndex >= 0) {
      final server = updatedServers[serverIndex];
      final updatedUsers = server.users.where((u) => u.username != username).toList();

      if (updatedUsers.isEmpty) {
        updatedServers.removeAt(serverIndex);
      } else {
        updatedServers[serverIndex] = ServerHistory(
          serverUrl: server.serverUrl,
          serverName: server.serverName,
          users: updatedUsers,
          lastUsed: server.lastUsed,
        );
      }
    }

    final updatedHistory = LoginHistory(
      servers: updatedServers,
      lastServerUrl: history.lastServerUrl,
      lastUsername: history.lastUsername == username ? null : history.lastUsername,
    );

    await _prefs.setString(_historyKey, jsonEncode(updatedHistory.toJson()));
  }

  /// 更新最后使用时间
  ///
  /// 更新指定服务器和用户的最后使用时间
  /// 同时更新最后使用的服务器和用户记录
  ///
  /// 参数：
  /// - [serverUrl] 服务器地址
  /// - [username] 用户名
  Future<void> updateLastUsed(String serverUrl, String username) async {
    final history = await getHistory();
    final now = DateTime.now();
    final updatedServers = List<ServerHistory>.from(history.servers);

    final serverIndex = updatedServers.indexWhere((s) => s.serverUrl == serverUrl);
    if (serverIndex >= 0) {
      final server = updatedServers[serverIndex];
      final userIndex = server.users.indexWhere((u) => u.username == username);

      if (userIndex >= 0) {
        final updatedUsers = List<UserHistory>.from(server.users);
        updatedUsers[userIndex] = updatedUsers[userIndex].copyWith(lastLogin: now);

        updatedServers[serverIndex] = ServerHistory(
          serverUrl: server.serverUrl,
          serverName: server.serverName,
          users: updatedUsers,
          lastUsed: now,
        );
      }
    }

    final updatedHistory = LoginHistory(
      servers: updatedServers,
      lastServerUrl: serverUrl,
      lastUsername: username,
    );

    await _prefs.setString(_historyKey, jsonEncode(updatedHistory.toJson()));
  }

  /// 清除所有登录历史
  ///
  /// 删除所有存储的登录历史记录
  Future<void> clearLoginHistory() async {
    await _prefs.remove(_historyKey);
  }

  /// 清除用户登录信息
  ///
  /// 清除指定服务器上所有用户的敏感信息
  /// 保留基本的用户记录但删除密码等敏感数据
  ///
  /// 参数：
  /// - [serverUrl] 服务器地址
  Future<void> clearUserLoginInfo(String serverUrl) async {
    final history = await getHistory();
    final updatedServers = List<ServerHistory>.from(history.servers);

    final serverIndex = updatedServers.indexWhere((s) => s.serverUrl == serverUrl);
    if (serverIndex >= 0) {
      final server = updatedServers[serverIndex];
      // 清除所有用户的密码信息，但保留用户记录
      final updatedUsers = server.users
          .map((user) => UserHistory(
                username: user.username,
                rememberPassword: false,
                savedPassword: null,
                lastLogin: user.lastLogin,
              ))
          .toList();

      updatedServers[serverIndex] = ServerHistory(
        serverUrl: server.serverUrl,
        serverName: server.serverName,
        users: updatedUsers,
        lastUsed: server.lastUsed,
      );
    }

    final updatedHistory = LoginHistory(
      servers: updatedServers,
      lastServerUrl: history.lastServerUrl,
      lastUsername: null, // 清除最后登录的用户名
    );

    await _prefs.setString(_historyKey, jsonEncode(updatedHistory.toJson()));
  }
}

/// 登录历史JSON扩展
///
/// 为LoginHistory类提供JSON序列化和反序列化功能
extension LoginHistoryJson on LoginHistory {
  /// 转换为JSON
  Map<String, dynamic> toJson() {
    return {
      'servers': servers.map((s) => s.toJson()).toList(),
      'lastServerUrl': lastServerUrl,
      'lastUsername': lastUsername,
    };
  }

  /// 从JSON创建实例
  static LoginHistory fromJson(Map<String, dynamic> json) {
    return LoginHistory(
      servers: (json['servers'] as List?)?.map((s) => ServerHistory.fromJson(s as Map<String, dynamic>)).toList() ?? [],
      lastServerUrl: json['lastServerUrl'] as String?,
      lastUsername: json['lastUsername'] as String?,
    );
  }
}

/// 服务器历史JSON扩展
///
/// 为ServerHistory类提供JSON序列化和反序列化功能
extension ServerHistoryJson on ServerHistory {
  /// 转换为JSON
  Map<String, dynamic> toJson() {
    return {
      'serverUrl': serverUrl,
      'serverName': serverName,
      'users': users.map((u) => u.toJson()).toList(),
      'lastUsed': lastUsed.toIso8601String(),
    };
  }

  /// 从JSON创建实例
  static ServerHistory fromJson(Map<String, dynamic> json) {
    return ServerHistory(
      serverUrl: json['serverUrl'] as String,
      serverName: json['serverName'] as String,
      users: (json['users'] as List).map((u) => UserHistory.fromJson(u as Map<String, dynamic>)).toList(),
      lastUsed: DateTime.parse(json['lastUsed'] as String),
    );
  }
}

/// 用户历史JSON扩展
///
/// 为UserHistory类提供JSON序列化和反序列化功能
extension UserHistoryJson on UserHistory {
  /// 转换为JSON
  Map<String, dynamic> toJson() {
    return {
      'username': username,
      'rememberPassword': rememberPassword,
      'savedPassword': savedPassword,
      'lastLogin': lastLogin.toIso8601String(),
    };
  }

  /// 从JSON创建实例
  static UserHistory fromJson(Map<String, dynamic> json) {
    return UserHistory(
      username: json['username'] as String,
      rememberPassword: json['rememberPassword'] as bool? ?? false,
      savedPassword: json['savedPassword'] as String?,
      lastLogin: DateTime.parse(json['lastLogin'] as String),
    );
  }
}
