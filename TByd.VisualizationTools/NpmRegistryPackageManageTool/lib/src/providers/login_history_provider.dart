import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/login_history_service.dart';
import '../models/login_history_model.dart';
import 'locale_provider.dart';

final loginHistoryServiceProvider = Provider<LoginHistoryService>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  return LoginHistoryService(prefs);
});

class LoginHistoryNotifier extends StateNotifier<LoginHistory> {
  final LoginHistoryService _service;

  LoginHistoryNotifier(this._service) : super(const LoginHistory()) {
    _loadHistory();
  }

  Future<void> _loadHistory() async {
    state = await _service.getHistory();
  }

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

  Future<void> removeServer(String serverUrl) async {
    await _service.removeServer(serverUrl);
    await _loadHistory();
  }

  Future<void> removeUser(String serverUrl, String username) async {
    await _service.removeUser(serverUrl, username);
    await _loadHistory();
  }

  Future<void> updateLastUsed(String serverUrl, String username) async {
    await _service.updateLastUsed(serverUrl, username);
    await _loadHistory();
  }
}

final loginHistoryProvider = StateNotifierProvider<LoginHistoryNotifier, LoginHistory>((ref) {
  final service = ref.watch(loginHistoryServiceProvider);
  return LoginHistoryNotifier(service);
});
