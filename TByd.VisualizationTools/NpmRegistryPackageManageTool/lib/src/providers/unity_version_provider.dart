import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/unity_version.dart';
import '../services/unity_version_service.dart';

/// Unity 版本状态
class UnityVersionState {
  /// 版本列表
  final List<UnityVersion>? versions;

  /// 是否正在加载
  final bool isLoading;

  /// 错误信息
  final String? error;

  /// 构造函数
  const UnityVersionState({
    this.versions,
    this.isLoading = false,
    this.error,
  });

  /// 创建加载中状态
  UnityVersionState copyWithLoading() {
    return const UnityVersionState(isLoading: true);
  }

  /// 创建加载成功状态
  UnityVersionState copyWithData(List<UnityVersion> versions) {
    return UnityVersionState(versions: versions, isLoading: false);
  }

  /// 创建错误状态
  UnityVersionState copyWithError(String error) {
    return UnityVersionState(error: error, isLoading: false);
  }
}

/// Unity 版本状态通知器
class UnityVersionNotifier extends StateNotifier<UnityVersionState> {
  final UnityVersionService _service;

  /// 构造函数
  UnityVersionNotifier(this._service) : super(const UnityVersionState()) {
    // 初始化时自动加载数据
    loadVersions();
  }

  /// 加载版本信息
  Future<void> loadVersions() async {
    state = state.copyWithLoading();

    try {
      final versions = await _service.fetchVersions();
      state = state.copyWithData(versions);
    } catch (e) {
      state = state.copyWithError(e.toString());
    }
  }

  @override
  void dispose() {
    _service.dispose();
    super.dispose();
  }
}

/// Unity 版本服务提供者
final unityVersionServiceProvider = Provider((ref) => UnityVersionService());

/// Unity 版本状态提供者
final unityVersionProvider = StateNotifierProvider<UnityVersionNotifier, UnityVersionState>((ref) {
  final service = ref.watch(unityVersionServiceProvider);
  return UnityVersionNotifier(service);
});
