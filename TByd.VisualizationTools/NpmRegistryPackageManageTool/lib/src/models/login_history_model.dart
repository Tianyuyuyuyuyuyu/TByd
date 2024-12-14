/// NPM Registry Manager - 登录历史数据模型
///
/// 该文件定义了用户登录历史相关的数据结构，包括：
/// - 服务器历史记录
/// - 用户历史记录
/// - 登录历史管理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:equatable/equatable.dart';

/// 服务器历史记录类
///
/// 存储和管理特定NPM仓库服务器的历史信息
/// 包含该服务器上的所有用户登录记录
class ServerHistory extends Equatable {
  /// 服务器URL地址
  final String serverUrl;

  /// 服务器名称
  final String serverName;

  /// 用户历史记录列表
  final List<UserHistory> users;

  /// 最后使用时间
  final DateTime lastUsed;

  /// 构造函数
  ///
  /// 创建一个新的服务器历史记录实例
  ///
  /// 参数：
  /// - [serverUrl] 服务器URL地址
  /// - [serverName] 服务器名称
  /// - [users] 用户历史记录列表
  /// - [lastUsed] 最后使用时间
  const ServerHistory({
    required this.serverUrl,
    required this.serverName,
    required this.users,
    required this.lastUsed,
  });

  /// 获取用于相等性比较的属性列表
  @override
  List<Object?> get props => [serverUrl, serverName, users, lastUsed];

  /// 从JSON创建实例
  ///
  /// 工厂构造函数，用于从JSON数据创建ServerHistory实例
  factory ServerHistory.fromJson(Map<String, dynamic> json) {
    return ServerHistory(
      serverUrl: json['serverUrl'] as String,
      serverName: json['serverName'] as String,
      users: (json['users'] as List).map((u) => UserHistory.fromJson(u as Map<String, dynamic>)).toList(),
      lastUsed: DateTime.parse(json['lastUsed'] as String),
    );
  }

  /// 转换为JSON
  ///
  /// 将服务器历史记录转换为可序列化的Map结构
  Map<String, dynamic> toJson() {
    return {
      'serverUrl': serverUrl,
      'serverName': serverName,
      'users': users.map((u) => u.toJson()).toList(),
      'lastUsed': lastUsed.toIso8601String(),
    };
  }
}

/// 用户历史记录类
///
/// 存储和管理特定用户的登录历史信息
/// 包含用户的登录凭据和最近登录时间
class UserHistory extends Equatable {
  /// 用户名
  final String username;

  /// 是否记住密码
  final bool rememberPassword;

  /// 已保存的密码（如果选择记住密码）
  final String? savedPassword;

  /// 最后登录时间
  final DateTime lastLogin;

  /// 构造函数
  ///
  /// 创建一个新的用户历史记录实例
  ///
  /// 参数：
  /// - [username] 用户名
  /// - [rememberPassword] 是否记住密码
  /// - [savedPassword] 已保存的密码
  /// - [lastLogin] 最后登录时间
  const UserHistory({
    required this.username,
    this.rememberPassword = false,
    this.savedPassword,
    required this.lastLogin,
  });

  /// 获取用于相等性比较的属性列表
  @override
  List<Object?> get props => [username, rememberPassword, savedPassword, lastLogin];

  /// 从JSON创建实例
  ///
  /// 工厂构造函数，用于从JSON数据创建UserHistory实例
  factory UserHistory.fromJson(Map<String, dynamic> json) {
    return UserHistory(
      username: json['username'] as String,
      rememberPassword: json['rememberPassword'] as bool? ?? false,
      savedPassword: json['savedPassword'] as String?,
      lastLogin: DateTime.parse(json['lastLogin'] as String),
    );
  }

  /// 转换为JSON
  ///
  /// 将用户历史记录转换为可序列化的Map结构
  Map<String, dynamic> toJson() {
    return {
      'username': username,
      'rememberPassword': rememberPassword,
      'savedPassword': savedPassword,
      'lastLogin': lastLogin.toIso8601String(),
    };
  }

  /// 创建副本
  ///
  /// 创建当前实例的副本，可选择性地更新某些属性
  ///
  /// 参数：
  /// - [username] 新的用户名
  /// - [rememberPassword] 新的记住密码设置
  /// - [savedPassword] 新的保存密码
  /// - [lastLogin] 新的最后登录时间
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

/// 登录历史管理类
///
/// 管理所有服务器和用户的登录历史记录
/// 包含最近登录的服务器和用户信息
class LoginHistory extends Equatable {
  /// 服务器历史记录列表
  final List<ServerHistory> servers;

  /// 最后使用的服务器URL
  final String? lastServerUrl;

  /// 最后登录的用户名
  final String? lastUsername;

  /// 构造函数
  ///
  /// 创建一个新的登录历史管理实例
  ///
  /// 参数：
  /// - [servers] 服务器历史记录列表
  /// - [lastServerUrl] 最后使用的服务器URL
  /// - [lastUsername] 最后登录的用户名
  const LoginHistory({
    this.servers = const [],
    this.lastServerUrl,
    this.lastUsername,
  });

  /// 获取用于相等性比较的属性列表
  @override
  List<Object?> get props => [servers, lastServerUrl, lastUsername];

  /// 从JSON创建实例
  ///
  /// 工厂构造函数，用于从JSON数据创建LoginHistory实例
  factory LoginHistory.fromJson(Map<String, dynamic> json) {
    return LoginHistory(
      servers: (json['servers'] as List?)?.map((s) => ServerHistory.fromJson(s as Map<String, dynamic>)).toList() ?? [],
      lastServerUrl: json['lastServerUrl'] as String?,
      lastUsername: json['lastUsername'] as String?,
    );
  }

  /// 转换为JSON
  ///
  /// 将登录历史管理数据转换为可序列化的Map结构
  Map<String, dynamic> toJson() {
    return {
      'servers': servers.map((s) => s.toJson()).toList(),
      'lastServerUrl': lastServerUrl,
      'lastUsername': lastUsername,
    };
  }
}
