import 'app_localizations.dart';

// ignore_for_file: type=lint

/// The translations for Chinese (`zh`).
class AppLocalizationsZh extends AppLocalizations {
  AppLocalizationsZh([String locale = 'zh']) : super(locale);

  @override
  String get appTitle => 'NPM 仓库管理工具';

  @override
  String get welcome => '欢迎使用 NPM 仓库管理器';

  @override
  String get loginTitle => '登录';

  @override
  String get serverAddress => '服务器地址';

  @override
  String get username => '用户名';

  @override
  String get email => '邮箱';

  @override
  String get password => '密码';

  @override
  String get rememberMe => '记住登录信息';

  @override
  String get login => '登录';

  @override
  String get loginLoading => '正在登录...';

  @override
  String get loginFailed => '登录失败';

  @override
  String get loginSuccess => '登录成功';

  @override
  String required(String field) {
    return '请输入$field';
  }

  @override
  String get invalidServerUrl => '无效的服务器地址';

  @override
  String get invalidEmail => '无效的邮箱地址';

  @override
  String get invalidCredentials => '用户名或密码错误';

  @override
  String get networkError => '网络连接错误';

  @override
  String get serverError => '服务器错误';

  @override
  String get ok => '确定';

  @override
  String get cancel => '取消';

  @override
  String get retry => '重试';

  @override
  String get loading => '加载中...';

  @override
  String get error => '错误';

  @override
  String get success => '成功';

  @override
  String get warning => '警告';

  @override
  String get info => '提示';

  @override
  String get settings => '设置';

  @override
  String get language => '语言';

  @override
  String get theme => '主题';

  @override
  String get darkMode => '深色模式';

  @override
  String get lightMode => '浅色模式';

  @override
  String get systemMode => '跟随系统';
}
