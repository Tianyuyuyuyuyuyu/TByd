import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/package_model.dart';
import '../services/package_service.dart';

final packageServiceProvider = Provider<PackageService>((ref) {
  return PackageService();
});

// 搜索状态
class SearchState {
  final bool isLoading;
  final String? error;
  final List<PackageSearchResult> results;
  final String query;

  const SearchState({
    this.isLoading = false,
    this.error,
    this.results = const [],
    this.query = '',
  });

  SearchState copyWith({
    bool? isLoading,
    String? error,
    List<PackageSearchResult>? results,
    String? query,
  }) {
    return SearchState(
      isLoading: isLoading ?? this.isLoading,
      error: error,
      results: results ?? this.results,
      query: query ?? this.query,
    );
  }
}

class SearchNotifier extends StateNotifier<SearchState> {
  final PackageService _packageService;

  SearchNotifier(this._packageService) : super(const SearchState());

  Future<void> search(String query) async {
    if (query.isEmpty) {
      state = const SearchState();
      return;
    }

    state = SearchState(isLoading: true, query: query);

    try {
      final results = await _packageService.searchPackages(query);
      state = SearchState(results: results, query: query);
    } catch (e) {
      state = SearchState(error: e.toString(), query: query);
    }
  }
}

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
final searchProvider = StateNotifierProvider<SearchNotifier, SearchState>((ref) {
  final packageService = ref.watch(packageServiceProvider);
  return SearchNotifier(packageService);
});

final packageDetailsProvider =
    StateNotifierProvider.family<PackageDetailsNotifier, PackageDetailsState, String>((ref, packageName) {
  final packageService = ref.watch(packageServiceProvider);
  return PackageDetailsNotifier(packageService, packageName);
});
