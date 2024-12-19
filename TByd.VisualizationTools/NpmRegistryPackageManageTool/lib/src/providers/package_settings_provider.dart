import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/unity_package_config.dart';
import '../services/npm_package_service.dart';

/// 包设置状态
class PackageSettingsState {
  /// 项目路径到配置的映射
  final Map<String, UnityPackageConfig> projectConfigs;

  /// 当前选中的项目路径
  final String? currentProjectPath;

  /// 仓库中现有包的版本号
  final Map<String, String> existingPackageVersions;

  /// 是否正在加载包信息
  final bool isLoadingPackageInfo;

  const PackageSettingsState({
    this.projectConfigs = const {},
    this.currentProjectPath,
    this.existingPackageVersions = const {},
    this.isLoadingPackageInfo = false,
  });

  PackageSettingsState copyWith({
    Map<String, UnityPackageConfig>? projectConfigs,
    String? currentProjectPath,
    Map<String, String>? existingPackageVersions,
    bool? isLoadingPackageInfo,
    bool clearCurrentPath = false,
  }) {
    return PackageSettingsState(
      projectConfigs: projectConfigs ?? this.projectConfigs,
      currentProjectPath: clearCurrentPath ? null : (currentProjectPath ?? this.currentProjectPath),
      existingPackageVersions: existingPackageVersions ?? this.existingPackageVersions,
      isLoadingPackageInfo: isLoadingPackageInfo ?? this.isLoadingPackageInfo,
    );
  }

  /// 获取当前配置
  UnityPackageConfig? get config => currentProjectPath != null ? projectConfigs[currentProjectPath] : null;

  /// 获取指定包名的现有版本号
  String? getExistingVersion(String packageName) => existingPackageVersions[packageName];
}

/// 包设置状态管理器
class PackageSettingsNotifier extends StateNotifier<PackageSettingsState> {
  final NpmPackageService _npmService;

  PackageSettingsNotifier(this._npmService) : super(const PackageSettingsState());

  /// 更新指定项目的配置
  void updateConfig(UnityPackageConfig config, {String? projectPath}) {
    final path = projectPath ?? state.currentProjectPath;
    if (path == null) return;

    final newConfigs = Map<String, UnityPackageConfig>.from(state.projectConfigs);
    newConfigs[path] = config;

    state = state.copyWith(
      projectConfigs: newConfigs,
    );

    // 当配置更新时，检查包版本
    _fetchPackageVersion(config.name);
  }

  /// 设置当前项目路径
  void setCurrentProject(String path, {UnityPackageConfig? initialConfig}) {
    if (initialConfig != null && !state.projectConfigs.containsKey(path)) {
      final newConfigs = Map<String, UnityPackageConfig>.from(state.projectConfigs);
      newConfigs[path] = initialConfig;

      state = state.copyWith(
        projectConfigs: newConfigs,
        currentProjectPath: path,
      );

      // 当设置新项目时，检查包版本
      _fetchPackageVersion(initialConfig.name);
    } else {
      state = state.copyWith(currentProjectPath: path);
      final config = state.projectConfigs[path];
      if (config != null) {
        _fetchPackageVersion(config.name);
      }
    }
  }

  /// 获取包的版本信息
  Future<void> _fetchPackageVersion(String packageName) async {
    if (packageName.isEmpty) return;

    state = state.copyWith(isLoadingPackageInfo: true);

    try {
      final response = await _npmService.fetchPackageInfo(packageName);
      if (response != null && response['dist-tags'] != null) {
        final latestVersion = response['dist-tags']['latest'] as String?;
        if (latestVersion != null) {
          final newVersions = Map<String, String>.from(state.existingPackageVersions);
          newVersions[packageName] = latestVersion;
          state = state.copyWith(
            existingPackageVersions: newVersions,
            isLoadingPackageInfo: false,
          );
          return;
        }
      }
      // 如果包不存在，从版本映射中移除
      final newVersions = Map<String, String>.from(state.existingPackageVersions);
      newVersions.remove(packageName);
      state = state.copyWith(
        existingPackageVersions: newVersions,
        isLoadingPackageInfo: false,
      );
    } catch (e) {
      // 发生错误时，从版本映射中移除
      final newVersions = Map<String, String>.from(state.existingPackageVersions);
      newVersions.remove(packageName);
      state = state.copyWith(
        existingPackageVersions: newVersions,
        isLoadingPackageInfo: false,
      );
    }
  }

  /// 清除当前项目
  void clearCurrentProject() {
    state = state.copyWith(clearCurrentPath: true);
  }

  /// 重置状态
  void reset() {
    state = const PackageSettingsState();
  }
}

/// 全局包设置状态提供者
final packageSettingsProvider = StateNotifierProvider<PackageSettingsNotifier, PackageSettingsState>((ref) {
  final npmService = NpmPackageService(baseUrl: 'https://registry.npmjs.org');
  return PackageSettingsNotifier(npmService);
});
