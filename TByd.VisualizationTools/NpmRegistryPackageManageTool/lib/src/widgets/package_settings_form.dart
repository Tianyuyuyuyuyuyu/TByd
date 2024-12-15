import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/unity_package_config.dart';
import '../providers/package_settings_provider.dart';

/// 包设置表单组件
class PackageSettingsForm extends ConsumerStatefulWidget {
  /// 当前配置
  final UnityPackageConfig? initialConfig;

  /// 配置变更回调
  final ValueChanged<UnityPackageConfig>? onConfigChanged;

  const PackageSettingsForm({
    super.key,
    this.initialConfig,
    this.onConfigChanged,
  });

  @override
  ConsumerState<PackageSettingsForm> createState() => _PackageSettingsFormState();
}

class _PackageSettingsFormState extends ConsumerState<PackageSettingsForm> {
  final _formKey = GlobalKey<FormState>();
  Timer? _debounceTimer;
  UnityPackageConfig? _lastConfig;

  late final TextEditingController _nameController;
  late final TextEditingController _versionController;
  late final TextEditingController _displayNameController;
  late final TextEditingController _descriptionController;
  late final TextEditingController _unityVersionController;
  late final TextEditingController _unityReleaseController;
  late final TextEditingController _licenseController;
  late final TextEditingController _authorNameController;
  late final TextEditingController _authorEmailController;
  late final TextEditingController _categoryController;
  late final TextEditingController _homepageController;
  late final TextEditingController _repositoryTypeController;
  late final TextEditingController _repositoryUrlController;
  late final TextEditingController _bugsUrlController;
  late List<String> _keywords;
  late Map<String, String> _dependencies;
  late List<Contributor> _contributors;
  late List<Sample> _samples;
  late Map<String, String> _relatedPackages;
  bool _isInitialized = false;

  @override
  void initState() {
    super.initState();
    _initializeControllers();

    // 如果有初始配置，设置到provider中
    if (widget.initialConfig != null) {
      final savedState = ref.read(packageSettingsProvider);
      if (savedState.currentProjectPath != null &&
          !savedState.projectConfigs.containsKey(savedState.currentProjectPath)) {
        Future.microtask(() {
          if (mounted) {
            ref.read(packageSettingsProvider.notifier).updateConfig(
                  widget.initialConfig!,
                  projectPath: savedState.currentProjectPath,
                );
          }
        });
      }
    }
  }

  void _initializeControllers() {
    _nameController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _versionController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _displayNameController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _descriptionController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _unityVersionController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _unityReleaseController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _licenseController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _authorNameController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _authorEmailController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _categoryController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _homepageController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _repositoryTypeController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _repositoryUrlController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _bugsUrlController = TextEditingController()..addListener(_debouncedUpdateConfig);
    _keywords = [];
    _dependencies = {};
    _contributors = [];
    _samples = [];
    _relatedPackages = {};
  }

  @override
  void dispose() {
    _nameController.dispose();
    _versionController.dispose();
    _displayNameController.dispose();
    _descriptionController.dispose();
    _unityVersionController.dispose();
    _unityReleaseController.dispose();
    _licenseController.dispose();
    _authorNameController.dispose();
    _authorEmailController.dispose();
    _categoryController.dispose();
    _homepageController.dispose();
    _repositoryTypeController.dispose();
    _repositoryUrlController.dispose();
    _bugsUrlController.dispose();
    _debounceTimer?.cancel();
    super.dispose();
  }

  void _updateFormFromConfig(UnityPackageConfig config) {
    // 暂时移除监听器
    _nameController.removeListener(_debouncedUpdateConfig);
    _versionController.removeListener(_debouncedUpdateConfig);
    _displayNameController.removeListener(_debouncedUpdateConfig);
    _descriptionController.removeListener(_debouncedUpdateConfig);
    _unityVersionController.removeListener(_debouncedUpdateConfig);
    _unityReleaseController.removeListener(_debouncedUpdateConfig);
    _licenseController.removeListener(_debouncedUpdateConfig);
    _authorNameController.removeListener(_debouncedUpdateConfig);
    _authorEmailController.removeListener(_debouncedUpdateConfig);
    _categoryController.removeListener(_debouncedUpdateConfig);
    _homepageController.removeListener(_debouncedUpdateConfig);
    _repositoryTypeController.removeListener(_debouncedUpdateConfig);
    _repositoryUrlController.removeListener(_debouncedUpdateConfig);
    _bugsUrlController.removeListener(_debouncedUpdateConfig);

    // 更新控制器值
    _nameController.text = config.name;
    _versionController.text = config.version;
    _displayNameController.text = config.displayName;
    _descriptionController.text = config.description;
    _unityVersionController.text = config.unityVersion;
    _unityReleaseController.text = config.unityRelease ?? '';
    _licenseController.text = config.license;
    _authorNameController.text = config.author.name ?? '';
    _authorEmailController.text = config.author.email ?? '';
    _categoryController.text = config.category ?? '';
    _homepageController.text = config.homepage ?? '';
    _repositoryTypeController.text = config.repository.type ?? '';
    _repositoryUrlController.text = config.repository.url ?? '';
    _bugsUrlController.text = config.bugs.url ?? '';

    // 重新添加监听器
    _nameController.addListener(_debouncedUpdateConfig);
    _versionController.addListener(_debouncedUpdateConfig);
    _displayNameController.addListener(_debouncedUpdateConfig);
    _descriptionController.addListener(_debouncedUpdateConfig);
    _unityVersionController.addListener(_debouncedUpdateConfig);
    _unityReleaseController.addListener(_debouncedUpdateConfig);
    _licenseController.addListener(_debouncedUpdateConfig);
    _authorNameController.addListener(_debouncedUpdateConfig);
    _authorEmailController.addListener(_debouncedUpdateConfig);
    _categoryController.addListener(_debouncedUpdateConfig);
    _homepageController.addListener(_debouncedUpdateConfig);
    _repositoryTypeController.addListener(_debouncedUpdateConfig);
    _repositoryUrlController.addListener(_debouncedUpdateConfig);
    _bugsUrlController.addListener(_debouncedUpdateConfig);

    setState(() {
      _keywords = List.from(config.keywords ?? []);
      _dependencies = Map.from(config.dependencies ?? {});
      _contributors = List.from(config.contributors ?? []);
      _samples = List.from(config.samples ?? []);
      _relatedPackages = Map.from(config.relatedPackages ?? {});
    });

    _lastConfig = config;
    _isInitialized = true;
  }

  void _debouncedUpdateConfig() {
    if (!_isInitialized) return;
    _debounceTimer?.cancel();
    _debounceTimer = Timer(const Duration(milliseconds: 500), _updateConfig);
  }

  void _updateConfig() {
    if (_formKey.currentState?.validate() ?? false) {
      final config = UnityPackageConfig(
        name: _nameController.text,
        version: _versionController.text,
        displayName: _displayNameController.text,
        description: _descriptionController.text,
        unityVersion: _unityVersionController.text,
        unityRelease: _unityReleaseController.text,
        license: _licenseController.text,
        keywords: _keywords,
        author: PackageAuthor(
          name: _authorNameController.text,
          email: _authorEmailController.text,
        ),
        category: _categoryController.text,
        homepage: _homepageController.text,
        repository: Repository(
          type: _repositoryTypeController.text,
          url: _repositoryUrlController.text,
        ),
        bugs: BugTracker(
          url: _bugsUrlController.text,
        ),
        dependencies: _dependencies,
        contributors: _contributors,
        samples: _samples,
        relatedPackages: _relatedPackages,
      );

      Future.microtask(() {
        if (mounted) {
          ref.read(packageSettingsProvider.notifier).updateConfig(config);
          widget.onConfigChanged?.call(config);
        }
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final savedState = ref.watch(packageSettingsProvider);
    final config = savedState.config;

    // 检查配置是否发生变化
    if (config != null && config != _lastConfig) {
      Future.microtask(() {
        if (mounted) {
          _updateFormFromConfig(config);
        }
      });
    }

    return Form(
      key: _formKey,
      child: ListView(
        padding: EdgeInsets.zero,
        children: [
          Card(
            margin: EdgeInsets.zero,
            shape: const RoundedRectangleBorder(
              borderRadius: BorderRadius.zero,
            ),
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Column(
                children: [
                  // 第一行：name, displayName, version
                  Row(
                    children: [
                      Expanded(
                        flex: 2,
                        child: TextFormField(
                          controller: _nameController,
                          decoration: const InputDecoration(
                            labelText: 'name',
                            hintText: 'com.company.package-name',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return '请输入包名称';
                            }
                            return null;
                          },
                        ),
                      ),
                      const SizedBox(width: 16),
                      Expanded(
                        flex: 2,
                        child: TextFormField(
                          controller: _displayNameController,
                          decoration: const InputDecoration(
                            labelText: 'displayName',
                            hintText: '显示名称',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return '请输入显示名称';
                            }
                            return null;
                          },
                        ),
                      ),
                      const SizedBox(width: 16),
                      Expanded(
                        child: TextFormField(
                          controller: _versionController,
                          decoration: const InputDecoration(
                            labelText: 'version',
                            hintText: '1.0.0',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return '请输入版本号';
                            }
                            if (!RegExp(r'^\d+\.\d+\.\d+$').hasMatch(value)) {
                              return '请输入有效的版本号';
                            }
                            return null;
                          },
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 16),
                  // 第二行：unity, unityRelease, license
                  Row(
                    children: [
                      Expanded(
                        flex: 2,
                        child: TextFormField(
                          controller: _unityVersionController,
                          decoration: const InputDecoration(
                            labelText: 'unity',
                            hintText: '2021.3',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return '请输入Unity版本';
                            }
                            return null;
                          },
                        ),
                      ),
                      const SizedBox(width: 16),
                      Expanded(
                        child: TextFormField(
                          controller: _unityReleaseController,
                          decoration: const InputDecoration(
                            labelText: 'unityRelease',
                            hintText: '8f1',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                        ),
                      ),
                      const SizedBox(width: 16),
                      Expanded(
                        child: TextFormField(
                          controller: _licenseController,
                          decoration: const InputDecoration(
                            labelText: 'license',
                            hintText: 'MIT',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return '请输入许可证';
                            }
                            return null;
                          },
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 16),
                  // 描述
                  TextFormField(
                    controller: _descriptionController,
                    decoration: const InputDecoration(
                      labelText: 'description',
                      hintText: '的简要描述',
                      isDense: true,
                      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                    ),
                    maxLines: null,
                  ),
                  const SizedBox(height: 16),
                  // 关键字
                  TextFormField(
                    controller: TextEditingController(text: _keywords.join(', ')),
                    decoration: const InputDecoration(
                      labelText: 'keywords',
                      hintText: '逗号分隔的关键字列表',
                      isDense: true,
                      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                    ),
                    onChanged: (value) {
                      setState(() {
                        _keywords = value.split(',').map((e) => e.trim()).where((e) => e.isNotEmpty).toList();
                      });
                    },
                  ),
                  const SizedBox(height: 16),
                  // ���者信息
                  Row(
                    children: [
                      Expanded(
                        child: TextFormField(
                          controller: _authorNameController,
                          decoration: const InputDecoration(
                            labelText: 'author.name',
                            hintText: '作者名称',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                          validator: (value) {
                            if (value == null || value.isEmpty) {
                              return '请输入作者名称';
                            }
                            return null;
                          },
                        ),
                      ),
                      const SizedBox(width: 16),
                      Expanded(
                        child: TextFormField(
                          controller: _authorEmailController,
                          decoration: const InputDecoration(
                            labelText: 'author.email',
                            hintText: '电子邮件',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 16),
                  // 贡献者
                  TextFormField(
                    controller: TextEditingController(
                        text: _contributors
                            .map((c) => '${c.name}${c.twitter.isNotEmpty ? ' <${c.twitter}>' : ''}')
                            .join(', ')),
                    decoration: const InputDecoration(
                      labelText: 'contributors',
                      hintText: '贡献者列表，格式：name <twitter>, name2 <twitter2>',
                      isDense: true,
                      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                    ),
                    onChanged: (value) {
                      setState(() {
                        _contributors = value
                            .split(',')
                            .map((e) {
                              final match = RegExp(r'^(.*?)(?:\s+<(.+)>)?$').firstMatch(e.trim());
                              if (match != null) {
                                return Contributor(
                                  name: match.group(1)?.trim() ?? '',
                                  twitter: match.group(2)?.trim() ?? '',
                                );
                              }
                              return Contributor(name: e.trim());
                            })
                            .where((c) => c.name.isNotEmpty)
                            .toList();
                      });
                    },
                  ),
                  const SizedBox(height: 16),
                  // 分类
                  TextFormField(
                    controller: _categoryController,
                    decoration: const InputDecoration(
                      labelText: 'category',
                      hintText: '例如：GUI',
                      isDense: true,
                      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                    ),
                  ),
                  const SizedBox(height: 16),
                  // ��页
                  TextFormField(
                    controller: _homepageController,
                    decoration: const InputDecoration(
                      labelText: 'homepage',
                      isDense: true,
                      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                    ),
                  ),
                  const SizedBox(height: 16),
                  // 仓库信息
                  Row(
                    children: [
                      Expanded(
                        child: TextFormField(
                          controller: _repositoryTypeController,
                          decoration: const InputDecoration(
                            labelText: 'repository.type',
                            hintText: '仓库类型，例如：git',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                        ),
                      ),
                      const SizedBox(width: 16),
                      Expanded(
                        flex: 2,
                        child: TextFormField(
                          controller: _repositoryUrlController,
                          decoration: const InputDecoration(
                            labelText: 'repository.url',
                            hintText: '仓库地址',
                            isDense: true,
                            contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                          ),
                        ),
                      ),
                    ],
                  ),
                  const SizedBox(height: 16),
                  // 错误报告
                  TextFormField(
                    controller: _bugsUrlController,
                    decoration: const InputDecoration(
                      labelText: 'bugs.url',
                      hintText: '问题追踪地址',
                      isDense: true,
                      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                    ),
                  ),
                  const SizedBox(height: 16),
                  // 相关包
                  TextFormField(
                    controller: TextEditingController(
                      text: _relatedPackages.entries.map((e) => '${e.key}:${e.value}').join(', '),
                    ),
                    decoration: const InputDecoration(
                      labelText: 'relatedPackages',
                      hintText: '相关包列表，格式：package:version, package2:version2',
                      isDense: true,
                      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                    ),
                    onChanged: (value) {
                      setState(() {
                        _relatedPackages = Map.fromEntries(
                          value
                              .split(',')
                              .map((e) {
                                final parts = e.trim().split(':');
                                if (parts.length == 2) {
                                  return MapEntry(parts[0].trim(), parts[1].trim());
                                }
                                return null;
                              })
                              .where((e) => e != null)
                              .cast<MapEntry<String, String>>(),
                        );
                      });
                    },
                  ),
                  const SizedBox(height: 16),
                  // 依赖
                  TextFormField(
                    controller: TextEditingController(
                      text: _dependencies.entries.map((e) => '${e.key}@${e.value}').join(', '),
                    ),
                    decoration: const InputDecoration(
                      labelText: 'dependencies',
                      hintText: '依赖项列表，格式：package@version, package2@version2',
                      isDense: true,
                      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                    ),
                    onChanged: (value) {
                      setState(() {
                        _dependencies = Map.fromEntries(
                          value
                              .split(',')
                              .map((e) {
                                final parts = e.trim().split('@');
                                if (parts.length == 2) {
                                  return MapEntry(parts[0].trim(), parts[1].trim());
                                }
                                return null;
                              })
                              .where((e) => e != null)
                              .cast<MapEntry<String, String>>(),
                        );
                      });
                    },
                  ),
                  const SizedBox(height: 16),
                  // 示例
                  TextFormField(
                    controller: TextEditingController(
                      text: _samples.map((s) => '${s.displayName}:${s.description}:${s.path}').join(', '),
                    ),
                    decoration: const InputDecoration(
                      labelText: 'samples',
                      hintText: '示例列表，格式：displayName:description:path, displayName2:description2:path2',
                      isDense: true,
                      contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                    ),
                    onChanged: (value) {
                      setState(() {
                        _samples = value
                            .split(',')
                            .map((e) {
                              final parts = e.trim().split(':');
                              if (parts.length == 3) {
                                return Sample(
                                  displayName: parts[0].trim(),
                                  description: parts[1].trim(),
                                  path: parts[2].trim(),
                                );
                              }
                              return null;
                            })
                            .where((s) => s != null)
                            .cast<Sample>()
                            .toList();
                      });
                    },
                  ),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}