/// NPM Registry Manager - 认证数据模型
///
/// 该文件定义了用户认证相关的数据结构，包括：
/// - 认证令牌
/// - 用户信息
/// - 服务器配置
/// - 过期时间管理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:equatable/equatable.dart';

/// 认证模型类
///
/// 存储和管理用户认证会话的所有必要信息
/// 继承自 [Equatable] 以支持值相等性比较
class AuthModel extends Equatable {
  /// 认证令牌
  ///
  /// 用于验证用户身份的JWT令牌或其他形式的访问令牌
  final String token;

  /// 用户名
  ///
  /// 当前登录用户的用户名
  final String username;

  /// 服务器地址
  ///
  /// NPM仓库服务器的URL地址
  final String serverUrl;

  /// 过期时间
  ///
  /// 认证会话的过期时间戳
  final DateTime expiresAt;

  /// 构造函数
  ///
  /// 创建一个新的认证模型实例
  ///
  /// 参数：
  /// - [token] 认证令牌
  /// - [username] 用户名
  /// - [serverUrl] 服务器地址
  /// - [expiresAt] 过期时间
  const AuthModel({
    required this.token,
    required this.username,
    required this.serverUrl,
    required this.expiresAt,
  });

  /// 检查认证是否过期
  ///
  /// 通过比较当前时间和过期时间来判断认证是否已过期
  /// 返回 true 表示已过期，false 表示未过期
  bool get isExpired => DateTime.now().isAfter(expiresAt);

  /// 获取用于相等性比较的属性列表
  ///
  /// 重写 [Equatable] 的 props 属性
  /// 用于确定两个 AuthModel 实例是否相等
  @override
  List<Object?> get props => [token, username, serverUrl, expiresAt];

  /// 转换为JSON
  ///
  /// 将认证模型转换为可序列化的Map结构
  /// 用于数据持久化和网络传输
  Map<String, dynamic> toJson() {
    return {
      'token': token,
      'username': username,
      'serverUrl': serverUrl,
      'expiresAt': expiresAt.toIso8601String(),
    };
  }

  /// 从JSON创建实例
  ///
  /// 工厂构造函数，用于从JSON数据创建AuthModel实例
  ///
  /// 参数：
  /// - [json] 包含认证数据的Map结构
  ///
  /// 返回：
  /// 一个新的AuthModel实例
  factory AuthModel.fromJson(Map<String, dynamic> json) {
    return AuthModel(
      token: json['token'] as String,
      username: json['username'] as String,
      serverUrl: json['serverUrl'] as String,
      expiresAt: DateTime.parse(json['expiresAt'] as String),
    );
  }
}
