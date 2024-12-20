/// NPM Registry Manager - 包管理服务
///
/// 该文件负责管理NPM包的核心功能，包括：
/// - 包信息的获取和管理
/// - 包版本控制
/// - 包搜索功能
/// - 包详情查看
/// - 包发布和废弃管理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/package_model.dart';
import '../services/package_service.dart';
import '../providers/auth_provider.dart';
import '../providers/keywords_provider.dart';

/// 包服务提供者
///
/// 提供 [PackageService] 实例，用于处理包管理相关的操作
/// 依赖于认证状态获取服务器URL和认证令牌
final packageServiceProvider = Provider<PackageService>((ref) {
  final authState = ref.watch(authProvider);
  return PackageService(
    serverUrl: authState.auth?.serverUrl ?? '',
    token: authState.auth?.token,
  );
});

/// 包详情状态类
///
/// 存储和管理包详情相关的所有状态信息，括：
/// - 加载状态
/// - 错误信息
/// - 包信息
/// - 版本列表
/// - 原始清单数据
class PackageDetailsState {
  /// 是否正在加载
  final bool isLoading;

  /// 错误信息
  final String? error;

  /// 包信息
  final Package? package;

  /// 版本列表
  final List<PackageVersion> versions;

  /// 原始清单数据
  final String? rawManifest;

  const PackageDetailsState({
    this.isLoading = false,
    this.error,
    this.package,
    this.versions = const [],
    this.rawManifest,
  });

  /// 创建状态副本
  ///
  /// 用于更新状态时保持不变的值不变
  PackageDetailsState copyWith({
    bool? isLoading,
    String? error,
    Package? package,
    List<PackageVersion>? versions,
    String? rawManifest,
  }) {
    return PackageDetailsState(
      isLoading: isLoading ?? this.isLoading,
      error: error,
      package: package ?? this.package,
      versions: versions ?? this.versions,
      rawManifest: rawManifest ?? this.rawManifest,
    );
  }
}

/// 包详情状态管理器
///
/// 负责管理包详情的状态和操作，包括：
/// - 加载包详情
/// - 版本管理
/// - 包废弃处理
class PackageDetailsNotifier extends StateNotifier<PackageDetailsState> {
  final PackageService _packageService;
  final String packageName;
  bool _isDisposed = false;

  /// 构造函数
  ///
  /// 初始化状态并立即加载包详情
  PackageDetailsNotifier(this._packageService, this.packageName) : super(const PackageDetailsState()) {
    loadPackageDetails();
  }

  /// 加载包详情
  ///
  /// 获取包的完整信息，包括：
  /// - 基本信息
  /// - 版本列表
  /// - 原始清单
  Future<void> loadPackageDetails() async {
    if (_isDisposed) return;
    state = state.copyWith(isLoading: true);

    try {
      final package = await _packageService.getPackageDetails(packageName);
      if (_isDisposed) return;
      final versions = await _packageService.getPackageVersions(packageName);
      if (_isDisposed) return;
      final rawManifest = await _packageService.getRawManifest(packageName);
      if (_isDisposed) return;
      state = PackageDetailsState(
        package: package,
        versions: versions,
        rawManifest: rawManifest,
      );
    } catch (e) {
      if (_isDisposed) return;
      state = PackageDetailsState(error: e.toString());
    }
  }

  /// 取消发布版本
  ///
  /// 从仓库中移除指定版本的包
  /// [version] - 要取消发布的版本号
  Future<void> unpublishVersion(String version) async {
    if (_isDisposed) return;
    state = state.copyWith(isLoading: true);

    try {
      await _packageService.unpublishPackage(packageName, version);
      if (_isDisposed) return;
      await loadPackageDetails();
    } catch (e) {
      if (_isDisposed) return;
      state = state.copyWith(
        isLoading: false,
        error: e.toString(),
      );
    }
  }

  /// 废弃版本
  ///
  /// 将指定版本标记为废弃状态
  /// [version] - 要废弃的版本号
  /// [message] - 废弃说明信息
  Future<void> deprecateVersion(String version, String message) async {
    if (_isDisposed) return;
    state = state.copyWith(isLoading: true);

    try {
      await _packageService.deprecatePackage(packageName, version, message);
      if (_isDisposed) return;
      await loadPackageDetails();
    } catch (e) {
      if (_isDisposed) return;
      state = state.copyWith(
        isLoading: false,
        error: e.toString(),
      );
    }
  }

  /// 资源释放
  @override
  void dispose() {
    _isDisposed = true;
    super.dispose();
  }
}

/// 包管理状态
class PackageState {
  /// 包列表
  final List<PackageSearchResult> packages;

  /// 当前选中的包名
  final String? selectedPackageName;

  /// 是否正在加载
  final bool isLoading;

  /// 错误信息
  final String? error;

  /// 构造函数
  const PackageState({
    this.packages = const [],
    this.selectedPackageName,
    this.isLoading = false,
    this.error,
  });

  /// 创建新实例
  PackageState copyWith({
    List<PackageSearchResult>? packages,
    String? selectedPackageName,
    bool? isLoading,
    String? error,
  }) {
    return PackageState(
      packages: packages ?? this.packages,
      selectedPackageName: selectedPackageName ?? this.selectedPackageName,
      isLoading: isLoading ?? this.isLoading,
      error: error ?? this.error,
    );
  }
}

/// 包管理状态提供者
class PackageNotifier extends StateNotifier<PackageState> {
  final PackageService _packageService;

  PackageNotifier(this._packageService) : super(const PackageState());

  /// 设置选中的包
  void setSelectedPackage(String? packageName) {
    state = state.copyWith(selectedPackageName: packageName);
  }

  /// 刷新包列表
  Future<void> refreshPackages() async {
    state = state.copyWith(isLoading: true, error: null);

    try {
      final packages = await _packageService.searchPackages('');

      // 如果当前选中的包不在新的列表中，选择第一个包
      final selectedExists = packages.any((p) => p.name == state.selectedPackageName);
      final newSelectedPackage = selectedExists
          ? state.selectedPackageName
          : packages.isNotEmpty
              ? packages.first.name
              : null;

      state = state.copyWith(
        packages: packages,
        selectedPackageName: newSelectedPackage,
        isLoading: false,
      );
    } catch (e) {
      state = state.copyWith(
        error: e.toString(),
        isLoading: false,
      );
    }
  }
}

/// 包管理状态提供者
final packageProvider = StateNotifierProvider<PackageNotifier, PackageState>((ref) {
  final packageService = ref.watch(packageServiceProvider);
  return PackageNotifier(packageService);
});

/// 包详情状态提供者
///
/// 提供特定包的详细信息管理
final packageDetailsProvider = StateNotifierProvider.family<PackageDetailsNotifier, PackageDetailsState, String>(
  (ref, packageName) {
    final packageService = ref.watch(packageServiceProvider);
    return PackageDetailsNotifier(packageService, packageName);
  },
);
