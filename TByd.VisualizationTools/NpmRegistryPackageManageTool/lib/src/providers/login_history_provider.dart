/// NPM Registry Manager - 登录历史管理
///
/// 该文件负责管理用户的登录历史记录，包括：
/// - 登录信息的保存和读取
/// - 历史记录的增删改查
/// - 最近登录记录的追踪
/// - 多服务器登录历史管理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/login_history_service.dart';
import '../models/login_history_model.dart';
import 'locale_provider.dart';

/// 登录历史服务提供者
///
/// 提供 [LoginHistoryService] 实例，用于管理登录历史数据
/// 依赖于 [SharedPreferences] 进行数据持久化
final loginHistoryServiceProvider = Provider<LoginHistoryService>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  return LoginHistoryService(prefs);
});

/// 登录历史状态管理器
///
/// 负责管理登录历史的状态和操作，包括：
/// - 历史记录的加载
/// - 新登录信息的保存
/// - 历史记录的删除
/// - 最近使用记录的更新
class LoginHistoryNotifier extends StateNotifier<LoginHistory> {
  /// 登录历史服务实例
  final LoginHistoryService _service;

  /// 构造函数
  ///
  /// 初始化状态并立即加载历史记录
  /// [_service] - 登录历史服务实例
  LoginHistoryNotifier(this._service) : super(const LoginHistory()) {
    _loadHistory();
  }

  /// 加载登录历史记录
  ///
  /// 从持久化存储中读取历史记录并更新状态
  Future<void> _loadHistory() async {
    state = await _service.getHistory();
  }

  /// 保存登录信息
  ///
  /// 将新的登录信息保存到历史记录中
  /// [serverUrl] - 服务器地址
  /// [username] - 用户名
  /// [password] - 密码（可选）
  /// [rememberPassword] - 是否记住密码
  Future<void> saveLoginInfo(
    String serverUrl,
    String username,
    String? password,
    bool rememberPassword,
  ) async {
    await _service.saveLoginInfo(
      serverUrl,
      username,
      password,
      rememberPassword,
    );
    await _loadHistory();
  }

  /// 移除服务器记录
  ///
  /// 删除指定服务器的所有登录历史
  /// [serverUrl] - 要删除的服务器地址
  Future<void> removeServer(String serverUrl) async {
    await _service.removeServer(serverUrl);
    await _loadHistory();
  }

  /// 移除用户记录
  ///
  /// 删除指定服务器上特定用户的登录历史
  /// [serverUrl] - 服务器地址
  /// [username] - 要删除的用户名
  Future<void> removeUser(String serverUrl, String username) async {
    await _service.removeUser(serverUrl, username);
    await _loadHistory();
  }

  /// 更新最近使用时间
  ///
  /// 更新指定用户的最后登录时间
  /// [serverUrl] - 服务器地址
  /// [username] - 用户名
  Future<void> updateLastUsed(String serverUrl, String username) async {
    await _service.updateLastUsed(serverUrl, username);
    await _loadHistory();
  }
}

/// 登录历史状态提供者
///
/// 提供对登录历史状态的全局访问
/// 使用 [StateNotifierProvider] 支持状态的响应式更新
final loginHistoryProvider = StateNotifierProvider<LoginHistoryNotifier, LoginHistory>((ref) {
  final service = ref.watch(loginHistoryServiceProvider);
  return LoginHistoryNotifier(service);
});
