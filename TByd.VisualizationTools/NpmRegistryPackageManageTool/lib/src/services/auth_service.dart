import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/auth_model.dart';
import '../utils/constants.dart';
import 'package:shared_preferences/shared_preferences.dart';

class AuthenticationException implements Exception {
  final String message;
  AuthenticationException(this.message);

  @override
  String toString() => message;
}

class AuthService {
  final http.Client _client;

  AuthService({http.Client? client}) : _client = client ?? http.Client();

  Future<User> login(AuthCredentials credentials) async {
    try {
      // 尝试登录
      final loginResponse = await _client.get(
        Uri.parse('${ApiConstants.baseUrl}/-/user/org.couchdb.user:${credentials.username}'),
        headers: {
          'Authorization': 'Basic ${base64Encode(utf8.encode('${credentials.username}:${credentials.password}'))}',
          'Accept': 'application/json',
        },
      );

      print('Login response: ${loginResponse.statusCode} - ${loginResponse.body}');

      if (loginResponse.statusCode == 200 || loginResponse.statusCode == 201) {
        final loginData = json.decode(loginResponse.body);

        // 获取token
        final token = base64Encode(utf8.encode('${credentials.username}:${credentials.password}'));

        // Save token and email to SharedPreferences
        final prefs = await SharedPreferences.getInstance();
        await prefs.setString(StorageKeys.authToken, token);
        await prefs.setString(StorageKeys.userEmail, credentials.email);

        return User(
          username: credentials.username,
          email: credentials.email,
          token: token,
        );
      }

      // 如果登录失败，抛出异常
      throw AuthenticationException('登录失败：用户名或密码错误');
    } catch (e) {
      if (e is AuthenticationException) rethrow;
      throw AuthenticationException('网络错误: ${e.toString()}');
    }
  }

  Future<void> logout() async {
    try {
      final prefs = await SharedPreferences.getInstance();
      await prefs.remove(StorageKeys.authToken);
      await prefs.remove(StorageKeys.userEmail);
    } catch (e) {
      print('Logout error: $e');
    }
  }

  Future<bool> isAuthenticated() async {
    final prefs = await SharedPreferences.getInstance();
    final token = prefs.getString(StorageKeys.authToken);
    if (token == null) return false;

    try {
      final response = await _client.get(
        Uri.parse('${ApiConstants.baseUrl}/-/user/org.couchdb.user:${token.split(':')[0]}'),
        headers: {
          'Authorization': 'Basic $token',
          'Accept': 'application/json',
        },
      );
      return response.statusCode == 200;
    } catch (e) {
      return false;
    }
  }
}
