import 'package:flutter/material.dart';

class UnityVersionSelector extends StatefulWidget {
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
  State<UnityVersionSelector> createState() => _UnityVersionSelectorState();
}

class _UnityVersionSelectorState extends State<UnityVersionSelector> {
  // Unity主要版本列表
  static const List<String> _unityVersions = [
    '2022.3',
    '2022.2',
    '2022.1',
    '2021.3',
    '2021.2',
    '2021.1',
    '2020.3',
    '2020.2',
    '2020.1',
  ];

  // Unity Release映射表
  static const Map<String, List<String>> _unityReleases = {
    '2022.3': ['0f1', '1f1', '2f1', '3f1', '4f1', '5f1'],
    '2022.2': ['1f1', '2f1', '3f1', '4f1', '5f1', '6f1'],
    '2022.1': ['1f1', '2f1', '3f1', '4f1', '5f1', '6f1'],
    '2021.3': ['1f1', '2f1', '3f1', '4f1', '5f1', '6f1', '7f1', '8f1', '9f1', '10f1', '11f1', '12f1'],
    '2021.2': ['1f1', '2f1', '3f1', '4f1', '5f1', '6f1'],
    '2021.1': ['1f1', '2f1', '3f1', '4f1', '5f1', '6f1'],
    '2020.3': ['1f1', '2f1', '3f1', '4f1', '5f1', '6f1', '7f1', '8f1', '9f1', '10f1'],
    '2020.2': ['1f1', '2f1', '3f1', '4f1', '5f1', '6f1'],
    '2020.1': ['1f1', '2f1', '3f1', '4f1', '5f1', '6f1'],
  };

  late String _selectedVersion;
  String? _selectedRelease;

  @override
  void initState() {
    super.initState();
    _selectedVersion = _unityVersions.contains(widget.initialVersion) ? widget.initialVersion : _unityVersions[0];
    _selectedRelease = _validateRelease(widget.initialRelease);
  }

  String? _validateRelease(String? release) {
    if (release == null) return null;
    final releases = _unityReleases[_selectedVersion] ?? [];
    return releases.contains(release) ? release : null;
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final releases = _unityReleases[_selectedVersion] ?? [];

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
                Expanded(
                  flex: 2,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Unity Version',
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
                        items: _unityVersions.map((version) {
                          return DropdownMenuItem(
                            value: version,
                            child: Text(version),
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
                        validator: (value) {
                          if (value == null || value.isEmpty) {
                            return '请选择Unity版本';
                          }
                          return null;
                        },
                      ),
                    ],
                  ),
                ),
                const SizedBox(width: 16),
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Unity Release',
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
