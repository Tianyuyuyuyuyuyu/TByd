import 'package:equatable/equatable.dart';

class ServerHistory extends Equatable {
  final String serverUrl;
  final String serverName;
  final List<UserHistory> users;
  final DateTime lastUsed;

  const ServerHistory({
    required this.serverUrl,
    required this.serverName,
    required this.users,
    required this.lastUsed,
  });

  @override
  List<Object?> get props => [serverUrl, serverName, users, lastUsed];

  factory ServerHistory.fromJson(Map<String, dynamic> json) {
    return ServerHistory(
      serverUrl: json['serverUrl'] as String,
      serverName: json['serverName'] as String,
      users: (json['users'] as List).map((u) => UserHistory.fromJson(u as Map<String, dynamic>)).toList(),
      lastUsed: DateTime.parse(json['lastUsed'] as String),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'serverUrl': serverUrl,
      'serverName': serverName,
      'users': users.map((u) => u.toJson()).toList(),
      'lastUsed': lastUsed.toIso8601String(),
    };
  }
}

class UserHistory extends Equatable {
  final String username;
  final bool rememberPassword;
  final String? savedPassword;
  final DateTime lastLogin;

  const UserHistory({
    required this.username,
    this.rememberPassword = false,
    this.savedPassword,
    required this.lastLogin,
  });

  @override
  List<Object?> get props => [username, rememberPassword, savedPassword, lastLogin];

  factory UserHistory.fromJson(Map<String, dynamic> json) {
    return UserHistory(
      username: json['username'] as String,
      rememberPassword: json['rememberPassword'] as bool? ?? false,
      savedPassword: json['savedPassword'] as String?,
      lastLogin: DateTime.parse(json['lastLogin'] as String),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'username': username,
      'rememberPassword': rememberPassword,
      'savedPassword': savedPassword,
      'lastLogin': lastLogin.toIso8601String(),
    };
  }

  UserHistory copyWith({
    String? username,
    bool? rememberPassword,
    String? savedPassword,
    DateTime? lastLogin,
  }) {
    return UserHistory(
      username: username ?? this.username,
      rememberPassword: rememberPassword ?? this.rememberPassword,
      savedPassword: savedPassword ?? this.savedPassword,
      lastLogin: lastLogin ?? this.lastLogin,
    );
  }
}

class LoginHistory extends Equatable {
  final List<ServerHistory> servers;
  final String? lastServerUrl;
  final String? lastUsername;

  const LoginHistory({
    this.servers = const [],
    this.lastServerUrl,
    this.lastUsername,
  });

  @override
  List<Object?> get props => [servers, lastServerUrl, lastUsername];

  factory LoginHistory.fromJson(Map<String, dynamic> json) {
    return LoginHistory(
      servers: (json['servers'] as List?)?.map((s) => ServerHistory.fromJson(s as Map<String, dynamic>)).toList() ?? [],
      lastServerUrl: json['lastServerUrl'] as String?,
      lastUsername: json['lastUsername'] as String?,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'servers': servers.map((s) => s.toJson()).toList(),
      'lastServerUrl': lastServerUrl,
      'lastUsername': lastUsername,
    };
  }
}
