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
    return UnityVersionState(versions: versions, isLoading: true);
  }

  /// 创建加载成功状态
  UnityVersionState copyWithData(List<UnityVersion> versions) {
    return UnityVersionState(versions: versions, isLoading: false);
  }

  /// 创建错误状态
  UnityVersionState copyWithError(String error) {
    return UnityVersionState(error: error, isLoading: false, versions: versions);
  }
}

/// Unity 版本状态通知器
class UnityVersionNotifier extends StateNotifier<UnityVersionState> {
  final UnityVersionService _service;

  /// 构造函数
  UnityVersionNotifier(this._service) : super(const UnityVersionState());

  /// 加载版本信息
  Future<void> loadVersions() async {
    // 如果正在加载或已有数据，则不重复加载
    if (state.isLoading || state.versions != null) return;

    state = state.copyWithLoading();

    try {
      final versions = await _service.fetchVersions();
      if (!mounted) return;
      state = state.copyWithData(versions);
    } catch (e) {
      if (!mounted) return;
      state = state.copyWithError(e.toString());
    }
  }

  /// 刷新版本信息
  Future<void> refreshVersions() async {
    state = state.copyWithLoading();
    _service.clearCache();

    try {
      final versions = await _service.fetchVersions();
      if (!mounted) return;
      state = state.copyWithData(versions);
    } catch (e) {
      if (!mounted) return;
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
  final notifier = UnityVersionNotifier(service);
  // 自动加载版本信息
  Future.microtask(() => notifier.loadVersions());
  return notifier;
});
