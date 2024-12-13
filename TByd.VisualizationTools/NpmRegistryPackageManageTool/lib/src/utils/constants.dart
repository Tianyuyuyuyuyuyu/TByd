class ApiConstants {
  static const String baseUrl = 'http://localhost:4873';

  // Auth endpoints
  static const String login = '/-/v1/login';
  static const String userProfile = '/-/user/org.couchdb.user:';
  static const String logout = '/-/user/token';
  static const String whoami = '/-/whoami';

  // Package management endpoints
  static const String search = '/-/v1/search';
  static const String packageInfo = '/-/package';
  static const String publish = '/-/v1/publish';
  static const String unpublish = '/-/v1/unpublish';
  static const String deprecate = '/-/v1/deprecate';
}

class StorageKeys {
  static const String authToken = 'auth_token';
  static const String serverUrl = 'server_url';
  static const String currentSession = 'current_session';
  static const int sessionExpirationHours = 24;
  static const int maxLoginAttempts = 5;
  static const int loginLockoutMinutes = 30;
}

class AppConstants {
  static const String appName = 'NPM Registry Manager';
  static const String version = '1.0.0';
}
