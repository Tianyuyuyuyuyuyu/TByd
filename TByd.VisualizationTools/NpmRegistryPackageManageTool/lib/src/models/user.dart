/// 用户模型
class User {
  /// 用户名
  final String username;

  /// 服务器地址
  final String serverUrl;

  /// 是否记住密码
  final bool rememberPassword;

  /// 保存的密码（加密）
  final String? savedPassword;

  /// 头像URL
  final String? avatarUrl;

  /// 构造函数
  const User({
    required this.username,
    required this.serverUrl,
    this.rememberPassword = false,
    this.savedPassword,
    this.avatarUrl,
  });

  /// 从JSON创建用户
  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      username: json['username'] as String,
      serverUrl: json['serverUrl'] as String,
      rememberPassword: json['rememberPassword'] as bool? ?? false,
      savedPassword: json['savedPassword'] as String?,
      avatarUrl: json['avatarUrl'] as String?,
    );
  }

  /// 转换为JSON
  Map<String, dynamic> toJson() {
    return {
      'username': username,
      'serverUrl': serverUrl,
      'rememberPassword': rememberPassword,
      'savedPassword': savedPassword,
      'avatarUrl': avatarUrl,
    };
  }

  /// 创建一个新的用户实例，但只更新部分属性
  User copyWith({
    String? username,
    String? serverUrl,
    bool? rememberPassword,
    String? savedPassword,
    String? avatarUrl,
  }) {
    return User(
      username: username ?? this.username,
      serverUrl: serverUrl ?? this.serverUrl,
      rememberPassword: rememberPassword ?? this.rememberPassword,
      savedPassword: savedPassword ?? this.savedPassword,
      avatarUrl: avatarUrl ?? this.avatarUrl,
    );
  }
}
