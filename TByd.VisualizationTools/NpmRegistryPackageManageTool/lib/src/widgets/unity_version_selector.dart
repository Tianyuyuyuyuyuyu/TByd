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
    _initializeVersion();
  }

  void _initializeVersion() {
    if (widget.initialVersion.isEmpty) {
      setState(() {
        _selectedYear = '';
        _selectedVersion = '';
        _selectedRelease = null;
      });
    } else {
      // 解析版本号，格式为 "2021.3"
      final versionParts = widget.initialVersion.split('.');
      if (versionParts.length >= 2) {
        setState(() {
          _selectedYear = versionParts[0]; // "2021"
          _selectedVersion = widget.initialVersion; // "2021.3"
          _selectedRelease = widget.initialRelease; // "8f1"
        });
      } else {
        setState(() {
          _selectedYear = versionParts[0];
          _selectedVersion = widget.initialVersion;
          _selectedRelease = widget.initialRelease;
        });
      }
    }
  }

  @override
  void didUpdateWidget(UnityVersionSelector oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.initialVersion != widget.initialVersion || oldWidget.initialRelease != widget.initialRelease) {
      _initializeVersion();
    }
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
      if (version.version.startsWith('$year.')) {
        final parts = version.version.split('.');
        if (parts.length >= 2) {
          minorVersions.add('$year.${parts[1]}');
        }
      }
    }
    return minorVersions.toList()..sort((a, b) => b.compareTo(a));
  }

  /// 获取指定版本的所有Release
  List<String> _getReleasesForVersion(List<UnityVersion> versions, String version) {
    final releases = <String>{};
    for (var ver in versions) {
      if (ver.version.startsWith(version) && ver.version.split('.').length > 2) {
        releases.add(ver.version.split('.').sublist(2).join('.'));
      }
    }
    return releases.toList()..sort((a, b) => b.compareTo(a));
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final versionState = ref.watch(unityVersionProvider);

    final versions = versionState.versions ?? [];
    final years = _extractYears(versions);
    final minorVersions = _selectedYear.isNotEmpty ? _extractMinorVersions(versions, _selectedYear) : [];
    final releases = _selectedVersion.isNotEmpty ? _getReleasesForVersion(versions, _selectedVersion) : [];

    // 当版本列表加载完成后，验证当前选择的值是否有效
    if (!versionState.isLoading && versions.isNotEmpty) {
      // 证年份
      if (!years.contains(_selectedYear)) {
        WidgetsBinding.instance.addPostFrameCallback((_) {
          setState(() {
            _selectedYear = '';
            _selectedVersion = '';
            _selectedRelease = null;
          });
          widget.onVersionChanged('');
          widget.onReleaseChanged(null);
        });
      }
      // 验证版本号
      else if (!minorVersions.contains(_selectedVersion)) {
        WidgetsBinding.instance.addPostFrameCallback((_) {
          setState(() {
            _selectedVersion = '';
            _selectedRelease = null;
          });
          widget.onVersionChanged('');
          widget.onReleaseChanged(null);
        });
      }
      // 验证Release
      else if (_selectedRelease != null && !releases.contains(_selectedRelease)) {
        WidgetsBinding.instance.addPostFrameCallback((_) {
          setState(() {
            _selectedRelease = null;
          });
          widget.onReleaseChanged(null);
        });
      }
    }

    // 创建通用的下拉框装饰
    const dropdownDecoration = InputDecoration(
      isDense: true,
      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
      border: OutlineInputBorder(),
    );

    // 创建标题行组件
    Widget buildTitleRow(String title) {
      return Text(
        title,
        style: theme.textTheme.labelMedium,
      );
    }

    // 创建加载提示
    void showLoadingSnackBar() {
      if (!versionState.isLoading) return;
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Row(
            children: [
              SizedBox(
                height: 16,
                width: 16,
                child: CircularProgressIndicator(
                  strokeWidth: 2,
                  color: theme.colorScheme.onPrimary,
                ),
              ),
              const SizedBox(width: 8),
              const Text('正在加载Unity版本信息，请稍候...'),
            ],
          ),
          backgroundColor: theme.colorScheme.primary,
          behavior: SnackBarBehavior.floating,
          duration: const Duration(seconds: 2),
        ),
      );
    }

    return Row(
      children: [
        // 年份选择
        Expanded(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            mainAxisSize: MainAxisSize.min,
            children: [
              buildTitleRow('Year'),
              const SizedBox(height: 8),
              DropdownButtonFormField<String>(
                value: _selectedYear,
                decoration: dropdownDecoration,
                items: [
                  const DropdownMenuItem<String>(
                    value: '',
                    child: Text(''),
                  ),
                  if (_selectedYear.isNotEmpty && !years.contains(_selectedYear))
                    DropdownMenuItem<String>(
                      value: _selectedYear,
                      child: Text(_selectedYear),
                    ),
                  ...years.map((year) {
                    return DropdownMenuItem<String>(
                      value: year,
                      child: Text(year),
                    );
                  }),
                ],
                onTap: versionState.isLoading ? () => showLoadingSnackBar() : null,
                onChanged: versionState.isLoading
                    ? null
                    : (value) {
                        if (value != null) {
                          setState(() {
                            _selectedYear = value;
                            final newMinorVersions = _extractMinorVersions(versions, value);
                            _selectedVersion = newMinorVersions.isNotEmpty ? newMinorVersions.first : '';
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
            mainAxisSize: MainAxisSize.min,
            children: [
              buildTitleRow('Version'),
              const SizedBox(height: 8),
              DropdownButtonFormField<String>(
                value: _selectedVersion,
                decoration: dropdownDecoration,
                items: [
                  const DropdownMenuItem<String>(
                    value: '',
                    child: Text(''),
                  ),
                  if (_selectedVersion.isNotEmpty && !minorVersions.contains(_selectedVersion))
                    DropdownMenuItem<String>(
                      value: _selectedVersion,
                      child: Text(_selectedVersion.split('.')[1]),
                    ),
                  ...minorVersions.map((version) {
                    final minorVersion = version.split('.')[1];
                    return DropdownMenuItem<String>(
                      value: version,
                      child: Text(minorVersion),
                    );
                  }),
                ],
                onTap: versionState.isLoading ? () => showLoadingSnackBar() : null,
                onChanged: versionState.isLoading
                    ? null
                    : (value) {
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
            mainAxisSize: MainAxisSize.min,
            children: [
              buildTitleRow('Release'),
              const SizedBox(height: 8),
              DropdownButtonFormField<String?>(
                value: _selectedRelease,
                decoration: dropdownDecoration,
                items: [
                  const DropdownMenuItem<String?>(
                    value: null,
                    child: Text(''),
                  ),
                  if (_selectedRelease != null && !releases.contains(_selectedRelease))
                    DropdownMenuItem<String?>(
                      value: _selectedRelease,
                      child: Text(_selectedRelease!),
                    ),
                  ...releases.map((release) {
                    return DropdownMenuItem<String?>(
                      value: release,
                      child: Text(release),
                    );
                  }),
                ],
                onTap: versionState.isLoading ? () => showLoadingSnackBar() : null,
                onChanged: versionState.isLoading
                    ? null
                    : (value) {
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
    );
  }
}
