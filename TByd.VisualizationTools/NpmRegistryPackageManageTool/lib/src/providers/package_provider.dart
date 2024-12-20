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

/// 全局提供者

/// 包列表状态提供者
///
/// 提供包列表的状态管理
final packageProvider = StateNotifierProvider<PackageListNotifier, AsyncValue<List<PackageSearchResult>>>(
  (ref) {
    final packageService = ref.watch(packageServiceProvider);
    final keywordsNotifier = ref.watch(keywordsProvider.notifier);
    return PackageListNotifier(packageService, keywordsNotifier, ref);
  },
);

/// 包列表状态管理器
///
/// 负责管理包列表功能，包括：
/// - 加载所有包列表
/// - 搜索过滤
/// - 结果刷新
class PackageListNotifier extends StateNotifier<AsyncValue<List<PackageSearchResult>>> {
  final PackageService _packageService;
  final KeywordsNotifier _keywordsNotifier;
  final Ref _ref;
  List<PackageSearchResult> _allPackages = [];
  bool _isDisposed = false;
  bool _isLoading = false;

  PackageListNotifier(this._packageService, this._keywordsNotifier, this._ref) : super(const AsyncValue.loading()) {
    // 只在初始化时检查一次认证状态并加载
    final authState = _ref.read(authProvider);
    if (authState.isAuthenticated) {
      _loadAllPackages();
    }

    // 监听认证状态变化
    _ref.listen(authProvider, (previous, next) {
      if (!next.isAuthenticated && previous?.isAuthenticated == true) {
        // 用户登出，清空列表
        reset();
      }
    });
  }

  /// 重置搜索状态
  void reset() {
    state = const AsyncValue.data([]);
  }

  /// 加载所有包
  ///
  /// 从服务器获取完整的包列表
  Future<void> _loadAllPackages() async {
    if (_isDisposed || _isLoading) return; // 防止重复加载
    _isLoading = true;

    try {
      print('开始加载所有包');
      state = const AsyncValue.loading();

      final packages = await _packageService.searchPackages('');
      if (_isDisposed) return;

      if (packages.isEmpty) {
        print('未找到任何包');
        state = const AsyncValue.data([]);
        return;
      }

      _allPackages = packages;
      state = AsyncValue.data(_allPackages);

      // 收集所有关键字
      final allKeywords = <String>{};
      for (final package in packages) {
        allKeywords.addAll(package.keywords);
      }
      _keywordsNotifier.addKeywords(allKeywords);

      print('成功加载 ${packages.length} 个包');
    } catch (error, stackTrace) {
      print('加载包列表失败: $error');
      if (_isDisposed) return;
      state = AsyncValue.error(error, stackTrace);
    } finally {
      _isLoading = false; // 重置加载状态
    }
  }

  /// 搜索包
  ///
  /// 根据查询文本在本地包列表中搜索
  /// [query] - 搜索关键词
  Future<void> search(String query) async {
    if (_isDisposed) return;

    final searchText = query.trim().toLowerCase();
    print('搜索包: "$searchText"');

    try {
      if (searchText.isEmpty) {
        print('搜索文本为空，显示所有包');
        state = AsyncValue.data(_allPackages);
        return;
      }

      // 在本地列表中筛选匹配的包
      final results = _allPackages.where((package) {
        final name = package.name.toLowerCase();
        return name.contains(searchText);
      }).toList();

      // 按相关性排序
      results.sort((a, b) {
        final aName = a.name.toLowerCase();
        final bName = b.name.toLowerCase();

        // 完全匹配的排在最前面
        if (aName == searchText && bName != searchText) return -1;
        if (bName == searchText && aName != searchText) return 1;

        // 前缀匹配的排在其次
        if (aName.startsWith(searchText) && !bName.startsWith(searchText)) return -1;
        if (bName.startsWith(searchText) && !aName.startsWith(searchText)) return 1;

        // 其他按字母顺序排序
        return aName.compareTo(bName);
      });

      print('找到 ${results.length} 个匹配的包');
      state = AsyncValue.data(results);
    } catch (error, stackTrace) {
      print('搜索失败: $error');
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

/// 包详情状态提供者
///
/// 提供特定包的详细信息管理
final packageDetailsProvider = StateNotifierProvider.family<PackageDetailsNotifier, PackageDetailsState, String>(
  (ref, packageName) {
    final packageService = ref.watch(packageServiceProvider);
    return PackageDetailsNotifier(packageService, packageName);
  },
);
