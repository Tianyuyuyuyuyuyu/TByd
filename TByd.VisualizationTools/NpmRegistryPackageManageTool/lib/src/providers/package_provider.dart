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

/// 包服务提供者
///
/// 提供 [PackageService] 实例，用于处理包管理相关的操作
/// 依赖于认证状态获取服务器URL和认证令牌
final packageServiceProvider = Provider<PackageService>((ref) {
  final authState = ref.watch(authProvider);
  print('PackageService created with:');
  print('serverUrl: ${authState.auth?.serverUrl}');
  print('token: ${authState.auth?.token != null}');

  return PackageService(
    serverUrl: authState.auth?.serverUrl ?? '',
    token: authState.auth?.token,
  );
});

/// 包详情状态类
///
/// 存储和管理包详情相关的所有状态信息，包括：
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

/// 全局提供者

/// 搜索状态提供者
///
/// 提供包搜索功能和结果管理
final searchProvider = StateNotifierProvider<SearchNotifier, AsyncValue<List<PackageSearchResult>>>((ref) {
  final packageService = ref.watch(packageServiceProvider);
  return SearchNotifier(packageService);
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

/// 搜索状态管理器
///
/// 负责管理包搜索功能，包括：
/// - 加载所有包列表
/// - 搜索过滤
/// - 结果刷新
class SearchNotifier extends StateNotifier<AsyncValue<List<PackageSearchResult>>> {
  final PackageService _packageService;
  List<PackageSearchResult> _allPackages = [];
  bool _isDisposed = false;

  /// 构造函数
  ///
  /// 初始化状态并加载所有包列表
  SearchNotifier(this._packageService) : super(const AsyncValue.loading()) {
    _loadAllPackages();
  }

  /// 加载所有包
  ///
  /// 从服务器获取完整的包列表
  Future<void> _loadAllPackages() async {
    if (_isDisposed) return;

    try {
      print('Loading all packages');
      final packages = await _packageService.searchPackages('');
      if (_isDisposed) return;

      _allPackages = packages;
      state = AsyncValue.data(_allPackages);
    } catch (error, stackTrace) {
      print('Error loading packages: $error');
      if (_isDisposed) return;
      state = AsyncValue.error(error, stackTrace);
    }
  }

  /// 搜索包
  ///
  /// 根据查询文本在本地包列表中搜索
  /// [query] - 搜索关键词
  void search(String query) {
    if (_isDisposed) return;

    final searchText = query.trim().toLowerCase();

    try {
      if (searchText.isEmpty) {
        // 如果搜索文本为空，显示所有包
        state = AsyncValue.data(_allPackages);
        return;
      }

      // 在本地列表中筛选匹配的包
      final results = _allPackages.where((package) => package.name.toLowerCase().contains(searchText)).toList();

      state = AsyncValue.data(results);
    } catch (error, stackTrace) {
      state = AsyncValue.error(error, stackTrace);
    }
  }

  /// 刷新包列表
  ///
  /// 重新从服务器加载所有包的信息
  Future<void> refresh() async {
    await _loadAllPackages();
  }

  /// 资源释放
  @override
  void dispose() {
    _isDisposed = true;
    super.dispose();
  }
}
