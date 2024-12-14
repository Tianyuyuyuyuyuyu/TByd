/// NPM Registry Manager - 常量定义
///
/// 该文件定义了应用程序使用的所有常量，包括：
/// - API端点
/// - 存储键名
/// - 应用程序配置
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

/// API常量类
///
/// 定义所有API相关的常量，包括：
/// - 基础URL
/// - 认证端点
/// - 包管理端点
class ApiConstants {
  /// API基础URL
  static const String baseUrl = 'http://localhost:4873';

  /// 认证相关端点
  static const String login = '/-/v1/login';
  static const String userProfile = '/-/user/org.couchdb.user:';
  static const String logout = '/-/user/token';
  static const String whoami = '/-/whoami';

  /// 包管理相关端点
  static const String search = '/-/v1/search';
  static const String packageInfo = '/-/package';
  static const String publish = '/-/v1/publish';
  static const String unpublish = '/-/v1/unpublish';
  static const String deprecate = '/-/v1/deprecate';
}

/// 存储键名常量类
///
/// 定义所有本地存储相关的常量，包括：
/// - 存储键名
/// - 会话配置
/// - 安全限制
class StorageKeys {
  /// 认证令牌存储键
  static const String authToken = 'auth_token';

  /// 服务器URL存储键
  static const String serverUrl = 'server_url';

  /// 当前会话标识存储键
  static const String currentSession = 'current_session';

  /// 会话过期时间（小时）
  static const int sessionExpirationHours = 24;

  /// 最大登录尝试次数
  static const int maxLoginAttempts = 5;

  /// 登录锁定时间（分钟）
  static const int loginLockoutMinutes = 30;
}

/// 应用程序常量类
///
/// 定义应用程序的基本信息，包括：
/// - 应用名称
/// - 版本号
class AppConstants {
  /// 应用程序名称
  static const String appName = 'NPM Registry Manager';

  /// 应用程序版本号
  static const String version = '1.0.0';
}
