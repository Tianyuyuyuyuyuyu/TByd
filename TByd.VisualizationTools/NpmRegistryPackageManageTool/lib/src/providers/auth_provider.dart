import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/auth_model.dart';
import '../services/auth_service.dart';
import '../services/login_history_service.dart';
import 'locale_provider.dart';
import 'encryption_provider.dart';

final authServiceProvider = Provider<AuthService>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  final encryptionService = ref.watch(encryptionServiceProvider);
  return AuthService(
    encryptionService: encryptionService,
    prefs: prefs,
  );
});

final loginHistoryServiceProvider = Provider<LoginHistoryService>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  return LoginHistoryService(prefs);
});

class AuthState {
  final bool isAuthenticated;
  final bool isLoading;
  final String? error;
  final AuthModel? auth;

  const AuthState({
    this.isAuthenticated = false,
    this.isLoading = false,
    this.error,
    this.auth,
  });

  AuthState copyWith({
    bool? isAuthenticated,
    bool? isLoading,
    String? error,
    AuthModel? auth,
  }) {
    return AuthState(
      isAuthenticated: isAuthenticated ?? this.isAuthenticated,
      isLoading: isLoading ?? this.isLoading,
      error: error,
      auth: auth ?? this.auth,
    );
  }
}

class AuthNotifier extends StateNotifier<AuthState> {
  final AuthService _authService;
  final LoginHistoryService _historyService;

  AuthNotifier(this._authService, this._historyService) : super(const AuthState());

  Future<void> setAuthModel(AuthModel authModel) async {
    state = state.copyWith(
      isAuthenticated: true,
      isLoading: false,
      error: null,
      auth: authModel,
    );
  }

  Future<void> login(
    String serverUrl,
    String username,
    String password, {
    bool rememberMe = false,
  }) async {
    if (state.isLoading) return;

    state = state.copyWith(isLoading: true, error: null);

    try {
      final auth = await _authService.login(
        serverUrl,
        username,
        password,
      );

      if (rememberMe) {
        await _historyService.saveLoginInfo(
          serverUrl,
          username,
          password,
          rememberMe,
        );
      }

      state = state.copyWith(
        isAuthenticated: true,
        isLoading: false,
        auth: auth,
      );
    } catch (e) {
      state = state.copyWith(
        isLoading: false,
        error: e.toString(),
        isAuthenticated: false,
      );
    }
  }

  Future<void> logout() async {
    if (!state.isAuthenticated || state.auth == null) {
      state = const AuthState();
      return;
    }

    try {
      state = state.copyWith(isLoading: true, error: null);

      await _authService.logoutUser(
        state.auth!.serverUrl,
        state.auth!.token,
      );

      await _historyService.clearUserLoginInfo(state.auth!.serverUrl);

      state = const AuthState();
    } catch (e) {
      if (e is AuthenticationException && (e.code == 'AUTH001' || e.code == 'AUTH002')) {
        await _historyService.clearUserLoginInfo(state.auth!.serverUrl);
        state = const AuthState();
      } else {
        state = state.copyWith(
          isLoading: false,
          error: e.toString(),
        );
        rethrow;
      }
    }
  }

  Future<void> checkAuth() async {
    if (!state.isAuthenticated || state.auth == null) return;

    final isValid = await _authService.validateToken(
      state.auth!.serverUrl,
      state.auth!.token,
    );

    if (!isValid) {
      state = const AuthState();
    }
  }

  @override
  void dispose() {
    _authService.dispose();
    super.dispose();
  }
}

final authProvider = StateNotifierProvider<AuthNotifier, AuthState>((ref) {
  final authService = ref.watch(authServiceProvider);
  final historyService = ref.watch(loginHistoryServiceProvider);
  return AuthNotifier(authService, historyService);
});
