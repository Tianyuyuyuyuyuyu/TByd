import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import '../models/login_history_model.dart';

class LoginHistoryService {
  static const String _historyKey = 'login_history';
  final SharedPreferences _prefs;

  LoginHistoryService(this._prefs);

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

  Future<void> clearLoginHistory() async {
    await _prefs.remove(_historyKey);
  }

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

extension LoginHistoryJson on LoginHistory {
  Map<String, dynamic> toJson() {
    return {
      'servers': servers.map((s) => s.toJson()).toList(),
      'lastServerUrl': lastServerUrl,
      'lastUsername': lastUsername,
    };
  }

  static LoginHistory fromJson(Map<String, dynamic> json) {
    return LoginHistory(
      servers: (json['servers'] as List?)?.map((s) => ServerHistory.fromJson(s as Map<String, dynamic>)).toList() ?? [],
      lastServerUrl: json['lastServerUrl'] as String?,
      lastUsername: json['lastUsername'] as String?,
    );
  }
}

extension ServerHistoryJson on ServerHistory {
  Map<String, dynamic> toJson() {
    return {
      'serverUrl': serverUrl,
      'serverName': serverName,
      'users': users.map((u) => u.toJson()).toList(),
      'lastUsed': lastUsed.toIso8601String(),
    };
  }

  static ServerHistory fromJson(Map<String, dynamic> json) {
    return ServerHistory(
      serverUrl: json['serverUrl'] as String,
      serverName: json['serverName'] as String,
      users: (json['users'] as List).map((u) => UserHistory.fromJson(u as Map<String, dynamic>)).toList(),
      lastUsed: DateTime.parse(json['lastUsed'] as String),
    );
  }
}

extension UserHistoryJson on UserHistory {
  Map<String, dynamic> toJson() {
    return {
      'username': username,
      'rememberPassword': rememberPassword,
      'savedPassword': savedPassword,
      'lastLogin': lastLogin.toIso8601String(),
    };
  }

  static UserHistory fromJson(Map<String, dynamic> json) {
    return UserHistory(
      username: json['username'] as String,
      rememberPassword: json['rememberPassword'] as bool? ?? false,
      savedPassword: json['savedPassword'] as String?,
      lastLogin: DateTime.parse(json['lastLogin'] as String),
    );
  }
}
