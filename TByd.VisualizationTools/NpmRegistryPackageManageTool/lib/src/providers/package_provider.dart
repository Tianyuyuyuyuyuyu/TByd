import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/package_model.dart';
import '../services/package_service.dart';
import '../providers/auth_provider.dart';

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

// 包详情状态
class PackageDetailsState {
  final bool isLoading;
  final String? error;
  final Package? package;
  final List<PackageVersion> versions;

  const PackageDetailsState({
    this.isLoading = false,
    this.error,
    this.package,
    this.versions = const [],
  });

  PackageDetailsState copyWith({
    bool? isLoading,
    String? error,
    Package? package,
    List<PackageVersion>? versions,
  }) {
    return PackageDetailsState(
      isLoading: isLoading ?? this.isLoading,
      error: error,
      package: package ?? this.package,
      versions: versions ?? this.versions,
    );
  }
}

class PackageDetailsNotifier extends StateNotifier<PackageDetailsState> {
  final PackageService _packageService;
  final String packageName;

  PackageDetailsNotifier(this._packageService, this.packageName) : super(const PackageDetailsState()) {
    loadPackageDetails();
  }

  Future<void> loadPackageDetails() async {
    state = state.copyWith(isLoading: true);

    try {
      final package = await _packageService.getPackageDetails(packageName);
      final versions = await _packageService.getPackageVersions(packageName);
      state = PackageDetailsState(package: package, versions: versions);
    } catch (e) {
      state = PackageDetailsState(error: e.toString());
    }
  }

  Future<void> unpublishVersion(String version) async {
    state = state.copyWith(isLoading: true);

    try {
      await _packageService.unpublishPackage(packageName, version);
      await loadPackageDetails(); // 重新加载包详情
    } catch (e) {
      state = state.copyWith(
        isLoading: false,
        error: e.toString(),
      );
    }
  }

  Future<void> deprecateVersion(String version, String message) async {
    state = state.copyWith(isLoading: true);

    try {
      await _packageService.deprecatePackage(packageName, version, message);
      await loadPackageDetails(); // 重新加载包详情
    } catch (e) {
      state = state.copyWith(
        isLoading: false,
        error: e.toString(),
      );
    }
  }
}

// Providers
final searchProvider = StateNotifierProvider<SearchNotifier, AsyncValue<List<PackageSearchResult>>>((ref) {
  final packageService = ref.watch(packageServiceProvider);
  return SearchNotifier(packageService);
});

final packageDetailsProvider = StateNotifierProvider.family<PackageDetailsNotifier, PackageDetailsState, String>(
  (ref, packageName) {
    final packageService = ref.watch(packageServiceProvider);
    return PackageDetailsNotifier(packageService, packageName);
  },
);

class SearchNotifier extends StateNotifier<AsyncValue<List<PackageSearchResult>>> {
  final PackageService _packageService;
  List<PackageSearchResult> _allPackages = [];
  bool _isDisposed = false;

  SearchNotifier(this._packageService) : super(const AsyncValue.loading()) {
    _loadAllPackages();
  }

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

  Future<void> refresh() async {
    await _loadAllPackages();
  }

  @override
  void dispose() {
    _isDisposed = true;
    super.dispose();
  }
}
