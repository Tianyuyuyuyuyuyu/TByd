class ApiConstants {
  static const String baseUrl = 'http://120.26.201.54:1998';

  // Auth endpoints
  static const String login = '/-/v1/login';
  static const String userProfile = '/-/user/org.couchdb.user:';
  static const String logout = '/-/user/token';
  static const String whoami = '/-/whoami';

  // Package management endpoints
  static const String packages = '/-/v1/search';
  static const String packageInfo = '/-/package';
}

class StorageKeys {
  static const String authToken = 'auth_token';
  static const String user = 'user';
  static const String userEmail = 'user_email';
  static const String rememberMe = 'remember_me';
}

class AppConstants {
  static const String appName = 'NPM Registry Manager';
  static const String version = '1.0.0';
}
