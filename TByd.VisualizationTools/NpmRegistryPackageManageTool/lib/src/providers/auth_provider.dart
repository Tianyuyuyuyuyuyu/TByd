import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/auth_model.dart';
import '../services/auth_service.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'dart:convert';
import '../utils/constants.dart';

final authServiceProvider = Provider<AuthService>((ref) {
  return AuthService();
});

enum AuthState {
  initial,
  authenticated,
  unauthenticated,
  loading,
  error,
}

class AuthNotifier extends StateNotifier<AuthState> {
  final AuthService _authService;
  User? _user;

  AuthNotifier(this._authService) : super(AuthState.initial) {
    _checkAuthStatus();
  }

  User? get user => _user;

  Future<void> _checkAuthStatus() async {
    state = AuthState.loading;
    try {
      final isAuthenticated = await _authService.isAuthenticated();
      if (isAuthenticated) {
        final prefs = await SharedPreferences.getInstance();
        final token = prefs.getString(StorageKeys.authToken);
        final email = prefs.getString(StorageKeys.userEmail);
        if (token != null) {
          final username = String.fromCharCodes(base64Decode(token).takeWhile((char) => char != 58)); // 58 is ':'
          _user = User(
            username: username,
            email: email ?? '${username}@npm.registry',
            token: token,
          );
          state = AuthState.authenticated;
          return;
        }
      }
      state = AuthState.unauthenticated;
    } catch (e) {
      state = AuthState.unauthenticated;
    }
  }

  Future<void> login(String username, String password, String email) async {
    try {
      state = AuthState.loading;
      final credentials = AuthCredentials(
        username: username,
        password: password,
        email: email,
      );
      _user = await _authService.login(credentials);
      state = AuthState.authenticated;
    } on AuthenticationException {
      state = AuthState.error;
      rethrow;
    }
  }

  Future<void> logout() async {
    try {
      state = AuthState.loading;
      await _authService.logout();
      _user = null;
      state = AuthState.unauthenticated;
    } catch (e) {
      state = AuthState.error;
      rethrow;
    }
  }
}

final authProvider = StateNotifierProvider<AuthNotifier, AuthState>((ref) {
  final authService = ref.watch(authServiceProvider);
  return AuthNotifier(authService);
});
