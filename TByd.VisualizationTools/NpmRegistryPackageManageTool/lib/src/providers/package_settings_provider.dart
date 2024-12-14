import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/unity_package_config.dart';

/// 包设置状态
class PackageSettingsState {
  /// 项目路径到配置的映射
  final Map<String, UnityPackageConfig> projectConfigs;

  /// 当前选中的项目路径
  final String? currentProjectPath;

  const PackageSettingsState({
    this.projectConfigs = const {},
    this.currentProjectPath,
  });

  PackageSettingsState copyWith({
    Map<String, UnityPackageConfig>? projectConfigs,
    String? currentProjectPath,
    bool clearCurrentPath = false,
  }) {
    return PackageSettingsState(
      projectConfigs: projectConfigs ?? this.projectConfigs,
      currentProjectPath: clearCurrentPath ? null : (currentProjectPath ?? this.currentProjectPath),
    );
  }

  /// 获取当前配置
  UnityPackageConfig? get config => currentProjectPath != null ? projectConfigs[currentProjectPath] : null;
}

/// 包设置状态管理器
class PackageSettingsNotifier extends StateNotifier<PackageSettingsState> {
  PackageSettingsNotifier() : super(const PackageSettingsState());

  /// 更新指定项目的配置
  void updateConfig(UnityPackageConfig config, {String? projectPath}) {
    final path = projectPath ?? state.currentProjectPath;
    if (path == null) return;

    final newConfigs = Map<String, UnityPackageConfig>.from(state.projectConfigs);
    newConfigs[path] = config;

    state = state.copyWith(
      projectConfigs: newConfigs,
    );
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
    } else {
      state = state.copyWith(currentProjectPath: path);
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
  return PackageSettingsNotifier();
});
