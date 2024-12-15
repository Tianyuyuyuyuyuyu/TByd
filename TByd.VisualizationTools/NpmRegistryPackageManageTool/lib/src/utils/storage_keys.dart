/// 存储键常量
///
/// 定义应用程序中使用的所有存储键常量
class StorageKeys {
  /// 当前会话ID
  static const String currentSession = 'current_session';

  /// 认证令牌
  static const String authToken = 'auth_token';

  /// 服务器URL
  static const String serverUrl = 'server_url';

  /// 登录历史记录
  static const String loginHistory = 'login_history';

  /// 会话过期时间（小时）
  static const int sessionExpirationHours = 24;

  /// 最大登录尝试次数
  static const int maxLoginAttempts = 5;

  /// 登录锁定时间（分钟）
  static const int loginLockoutMinutes = 30;

  /// 私有构造函数，防止实例化
  StorageKeys._();
}
