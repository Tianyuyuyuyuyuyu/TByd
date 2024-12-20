/// NPM Registry Manager - 认证状态管理
///
/// 该文件负责处理应用程序的认证状态管理，包括：
/// - 用户登录/登出流程
/// - 认证状态维护
/// - 令牌验证
/// - 登录历史记录
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/auth_model.dart';
import '../services/auth_service.dart';
import '../services/login_history_service.dart';
import 'unity_version_provider.dart';
import 'locale_provider.dart';
import 'encryption_provider.dart';
import 'package_settings_provider.dart';
import '../models/user.dart';
import '../utils/avatar.dart';

/// 认证服务提供者
///
/// 负责创建和提供 [AuthService] 实例
/// 依赖于 [SharedPreferences] 和 [EncryptionService] 进行数据持久化和加密
final authServiceProvider = Provider<AuthService>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  final encryptionService = ref.watch(encryptionServiceProvider);
  return AuthService(
    encryptionService: encryptionService,
    prefs: prefs,
  );
});

/// 登录历史服务提供者
///
/// 负责创建和提供 [LoginHistoryService] 实例
/// 用于管理用户的登录历史记录
final loginHistoryServiceProvider = Provider<LoginHistoryService>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  return LoginHistoryService(prefs);
});

/// 认证状态类
///
/// 存储和管理认证相关的所有状态信息，包括：
/// - 认证状态
/// - 加载状态
/// - 错误信息
/// - 认证模型数据
class AuthState {
  /// 是否已认证
  final bool isAuthenticated;

  /// 是否正在加载
  final bool isLoading;

  /// 错误信息，如果有的话
  final String? error;

  /// 认证模型，包含用户认证相关的详细信息
  final AuthModel? auth;

  /// 当前登录用户
  final User? user;

  const AuthState({
    this.isAuthenticated = false,
    this.isLoading = false,
    this.error,
    this.auth,
    this.user,
  });

  /// 创建状态副本
  ///
  /// 用于更新状态时保持不变的值不变
  /// [isAuthenticated] - 新的认证状态
  /// [isLoading] - 新的加载状态
  /// [error] - 新的错误信息
  /// [auth] - 新的认证模型
  AuthState copyWith({
    bool? isAuthenticated,
    bool? isLoading,
    String? error,
    AuthModel? auth,
    User? user,
  }) {
    return AuthState(
      isAuthenticated: isAuthenticated ?? this.isAuthenticated,
      isLoading: isLoading ?? this.isLoading,
      error: error,
      auth: auth ?? this.auth,
      user: user ?? this.user,
    );
  }
}

/// 认证状态管理器
///
/// 负责处理所有认证相关的业务逻辑，括：
/// - 用户登录
/// - 用户登出
/// - 认证状态检查
/// - 令牌验证
class AuthNotifier extends StateNotifier<AuthState> {
  final AuthService _authService;
  final LoginHistoryService _historyService;
  final Ref _ref;

  AuthNotifier(this._authService, this._historyService, this._ref) : super(const AuthState());

  /// 设置认证模型
  ///
  /// 用于直接更新认证状态和模型
  /// [authModel] - 新的认证模型
  Future<void> setAuthModel(AuthModel authModel) async {
    state = state.copyWith(
      isAuthenticated: true,
      isLoading: false,
      error: null,
      auth: authModel,
    );
  }

  /// 保存登录历史
  Future<void> _saveLoginHistory(User user) async {
    await _historyService.saveLoginInfo(
      user.serverUrl,
      user.username,
      user.savedPassword ?? '',
      user.rememberPassword,
    );
  }

  /// 用户登录
  ///
  /// 处理用户登录流程，包括：
  /// - 验证用户凭据
  /// - 保存登录历史（如果选择记住登录）
  /// - 更新认证状态
  ///
  /// [serverUrl] - NPM 仓库服务器地址
  /// [username] - 用户名
  /// [password] - 密码
  /// [rememberMe] - 是否记住登录信息
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

      // 创建用户对象
      final user = User(
        username: username,
        serverUrl: serverUrl,
        rememberPassword: rememberMe,
        savedPassword: rememberMe ? password : null,
      );

      // 更新状态
      state = state.copyWith(
        isLoading: false,
        isAuthenticated: true,
        auth: auth,
        user: user,
      );

      // 保存登录历史
      if (rememberMe) {
        await _saveLoginHistory(user);
      }

      // 登录成功后立即获取 Unity 版本信息
      _ref.read(unityVersionProvider.notifier).loadVersions();
    } catch (e) {
      state = state.copyWith(
        isLoading: false,
        error: e.toString(),
        isAuthenticated: false,
      );
      rethrow;
    }
  }

  /// 用户登出
  ///
  /// 处理用户登出流程，包括：
  /// - 清除服务器端会话
  /// - 清除本地登录历史
  /// - 重置认证状态
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

      // 重置包设置状态
      _ref.read(packageSettingsProvider.notifier).reset();

      state = const AuthState();
    } catch (e) {
      if (e is AuthenticationException && (e.code == 'AUTH001' || e.code == 'AUTH002')) {
        await _historyService.clearUserLoginInfo(state.auth!.serverUrl);

        // 重置包设置状态
        _ref.read(packageSettingsProvider.notifier).reset();

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

  /// 检查认证状态
  ///
  /// 验证当前的认证令牌是否有效
  /// 如果令牌无效，则重置认证状态
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

  /// 资源释放
  ///
  /// 确保在状态管理器被销毁时正确释放资源
  @override
  void dispose() {
    _authService.dispose();
    super.dispose();
  }
}

/// 全局认证状态提供者
///
/// 提供对认证状态的全局访问
/// 整合认证服务和登录历史服务
final authProvider = StateNotifierProvider<AuthNotifier, AuthState>((ref) {
  final authService = ref.watch(authServiceProvider);
  final historyService = ref.watch(loginHistoryServiceProvider);
  return AuthNotifier(authService, historyService, ref);
});
