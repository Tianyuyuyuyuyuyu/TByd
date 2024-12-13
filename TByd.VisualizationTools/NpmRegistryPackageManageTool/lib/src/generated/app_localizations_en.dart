import 'app_localizations.dart';

// ignore_for_file: type=lint

/// The translations for English (`en`).
class AppLocalizationsEn extends AppLocalizations {
  AppLocalizationsEn([String locale = 'en']) : super(locale);

  @override
  String get appTitle => 'NPM Registry Manager';

  @override
  String get welcome => 'Welcome to NPM Registry Manager';

  @override
  String get loginTitle => 'Login';

  @override
  String get serverAddress => 'Server Address';

  @override
  String get username => 'Username';

  @override
  String get email => 'Email';

  @override
  String get password => 'Password';

  @override
  String get rememberMe => 'Remember Me';

  @override
  String get login => 'Login';

  @override
  String get loginLoading => 'Logging in...';

  @override
  String get loginFailed => 'Login Failed';

  @override
  String get loginSuccess => 'Login Successful';

  @override
  String required(String field) {
    return 'Please enter $field';
  }

  @override
  String get invalidServerUrl => 'Invalid server address';

  @override
  String get invalidEmail => 'Invalid email address';

  @override
  String get invalidCredentials => 'Invalid username or password';

  @override
  String get networkError => 'Network connection error';

  @override
  String get serverError => 'Server error';

  @override
  String get ok => 'OK';

  @override
  String get cancel => 'Cancel';

  @override
  String get retry => 'Retry';

  @override
  String get loading => 'Loading...';

  @override
  String get error => 'Error';

  @override
  String get success => 'Success';

  @override
  String get warning => 'Warning';

  @override
  String get info => 'Info';

  @override
  String get settings => 'Settings';

  @override
  String get language => 'Language';

  @override
  String get theme => 'Theme';

  @override
  String get darkMode => 'Dark Mode';

  @override
  String get lightMode => 'Light Mode';

  @override
  String get systemMode => 'System Mode';
}
