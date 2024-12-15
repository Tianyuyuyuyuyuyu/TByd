import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/unity_version_provider.dart';
import '../models/unity_version.dart';

class UnityVersionSelector extends ConsumerStatefulWidget {
  final String initialVersion;
  final String? initialRelease;
  final ValueChanged<String> onVersionChanged;
  final ValueChanged<String?> onReleaseChanged;

  const UnityVersionSelector({
    super.key,
    required this.initialVersion,
    this.initialRelease,
    required this.onVersionChanged,
    required this.onReleaseChanged,
  });

  @override
  ConsumerState<UnityVersionSelector> createState() => _UnityVersionSelectorState();
}

class _UnityVersionSelectorState extends ConsumerState<UnityVersionSelector> {
  late String _selectedYear;
  late String _selectedVersion;
  String? _selectedRelease;

  @override
  void initState() {
    super.initState();
    // 从初始版本中提取年份和版本
    final parts = widget.initialVersion.split('.');
    _selectedYear = parts[0];
    _selectedVersion = widget.initialVersion;
    _selectedRelease = widget.initialRelease;
  }

  /// 从版本列表中提取年份
  List<String> _extractYears(List<UnityVersion> versions) {
    final Set<String> years = {};
    for (var version in versions) {
      final year = version.version.split('.')[0];
      years.add(year);
    }
    return years.toList()..sort((a, b) => b.compareTo(a));
  }

  /// 从版本列表中提取指定年份的小版本
  List<String> _extractMinorVersions(List<UnityVersion> versions, String year) {
    final Set<String> minorVersions = {};
    for (var version in versions) {
      if (version.version.startsWith(year)) {
        final parts = version.version.split('.');
        if (parts.length >= 2) {
          minorVersions.add('${parts[0]}.${parts[1]}');
        }
      }
    }
    return minorVersions.toList()..sort((a, b) => b.compareTo(a));
  }

  /// 获取指定版本的所有Release
  List<String> _getReleasesForVersion(List<UnityVersion> versions, String version) {
    final releases = <String>{};
    for (var ver in versions) {
      if (ver.version.startsWith(version)) {
        final parts = ver.version.split('.');
        if (parts.length > 2) {
          releases.add(parts.sublist(2).join('.'));
        }
      }
    }
    return releases.toList()..sort((a, b) => b.compareTo(a));
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final versionState = ref.watch(unityVersionProvider);

    // 如果正在加载，显示加载指示器
    if (versionState.isLoading) {
      return Card(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Unity Version Configuration',
                style: theme.textTheme.titleMedium,
              ),
              const SizedBox(height: 16),
              const Center(
                child: CircularProgressIndicator(),
              ),
              const SizedBox(height: 8),
              const Center(
                child: Text('正在加载Unity版本信息...'),
              ),
            ],
          ),
        ),
      );
    }

    // 如果加载出错，显示错误信息
    if (versionState.error != null) {
      return Card(
        child: Padding(
          padding: const EdgeInsets.all(16.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                'Unity Version Configuration',
                style: theme.textTheme.titleMedium,
              ),
              const SizedBox(height: 16),
              Center(
                child: Text(
                  '加载失败: ${versionState.error}',
                  style: TextStyle(color: theme.colorScheme.error),
                ),
              ),
            ],
          ),
        ),
      );
    }

    final versions = versionState.versions ?? [];
    final years = _extractYears(versions);
    final minorVersions = _extractMinorVersions(versions, _selectedYear);
    final releases = _getReleasesForVersion(versions, _selectedVersion);

    // 确保选中的年份在列表中
    if (!years.contains(_selectedYear)) {
      _selectedYear = years.isNotEmpty ? years.first : '2021';
      final newMinorVersions = _extractMinorVersions(versions, _selectedYear);
      _selectedVersion = newMinorVersions.isNotEmpty ? newMinorVersions.first : '$_selectedYear.3';
      widget.onVersionChanged(_selectedVersion);
    }

    // 确保选中的版本在列表中
    if (!minorVersions.contains(_selectedVersion)) {
      _selectedVersion = minorVersions.isNotEmpty ? minorVersions.first : '$_selectedYear.3';
      widget.onVersionChanged(_selectedVersion);
    }

    // 确保选中的Release在列表中
    if (_selectedRelease != null && !releases.contains(_selectedRelease)) {
      _selectedRelease = null;
      widget.onReleaseChanged(null);
    }

    return Card(
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'Unity Version Configuration',
              style: theme.textTheme.titleMedium,
            ),
            const SizedBox(height: 16),
            Row(
              children: [
                // 年份选择
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Year',
                        style: theme.textTheme.labelMedium,
                      ),
                      const SizedBox(height: 8),
                      DropdownButtonFormField<String>(
                        value: _selectedYear,
                        decoration: const InputDecoration(
                          isDense: true,
                          contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          border: OutlineInputBorder(),
                        ),
                        items: years.map((year) {
                          return DropdownMenuItem(
                            value: year,
                            child: Text(year),
                          );
                        }).toList(),
                        onChanged: (value) {
                          if (value != null) {
                            setState(() {
                              _selectedYear = value;
                              // 更新小版本选择
                              final newMinorVersions = _extractMinorVersions(versions, value);
                              _selectedVersion = newMinorVersions.isNotEmpty ? newMinorVersions.first : '$value.3';
                              _selectedRelease = null;
                            });
                            widget.onVersionChanged(_selectedVersion);
                            widget.onReleaseChanged(null);
                          }
                        },
                      ),
                    ],
                  ),
                ),
                const SizedBox(width: 16),
                // 小版本选择
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Version',
                        style: theme.textTheme.labelMedium,
                      ),
                      const SizedBox(height: 8),
                      DropdownButtonFormField<String>(
                        value: _selectedVersion,
                        decoration: const InputDecoration(
                          isDense: true,
                          contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          border: OutlineInputBorder(),
                        ),
                        items: minorVersions.map((version) {
                          return DropdownMenuItem(
                            value: version,
                            child: Text(version.substring(_selectedYear.length + 1)),
                          );
                        }).toList(),
                        onChanged: (value) {
                          if (value != null) {
                            setState(() {
                              _selectedVersion = value;
                              _selectedRelease = null;
                            });
                            widget.onVersionChanged(value);
                            widget.onReleaseChanged(null);
                          }
                        },
                      ),
                    ],
                  ),
                ),
                const SizedBox(width: 16),
                // Release选择
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Release',
                        style: theme.textTheme.labelMedium,
                      ),
                      const SizedBox(height: 8),
                      DropdownButtonFormField<String?>(
                        value: _selectedRelease,
                        decoration: const InputDecoration(
                          isDense: true,
                          contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          border: OutlineInputBorder(),
                        ),
                        items: [
                          const DropdownMenuItem<String?>(
                            value: null,
                            child: Text(''),
                          ),
                          ...releases.map((release) {
                            return DropdownMenuItem<String?>(
                              value: release,
                              child: Text(release),
                            );
                          }),
                        ],
                        onChanged: (value) {
                          setState(() {
                            _selectedRelease = value;
                          });
                          widget.onReleaseChanged(value);
                        },
                      ),
                    ],
                  ),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
