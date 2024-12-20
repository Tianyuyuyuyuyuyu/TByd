import 'dart:async';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../models/unity_package_config.dart';
import '../providers/package_settings_provider.dart';
import '../providers/unity_version_provider.dart';
import '../providers/category_provider.dart';
import 'unity_version_selector.dart';
import '../models/license_type.dart';
import '../widgets/keywords_selector.dart';
import '../widgets/category_selector.dart';
import 'contributors_selector.dart';
import 'package:url_launcher/url_launcher.dart' as url_launcher;
import 'dart:io';
import 'package:path/path.dart' as path;

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
  UnityPackageConfig? _lastConfig;
  bool _isDirty = false;

  late final TextEditingController _nameController;
  late final TextEditingController _versionController;
  late final TextEditingController _displayNameController;
  late final TextEditingController _descriptionController;
  late final TextEditingController _unityVersionController;
  late final TextEditingController _unityReleaseController;
  late final TextEditingController _customLicenseController;
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
  LicenseType _selectedLicenseType = LicenseType.mit;

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
            // 确保立即更新表单
            _updateFormFromConfig(widget.initialConfig!);
          }
        });
      } else {
        // 如果配置已存在，直接更新表单
        _updateFormFromConfig(widget.initialConfig!);
      }
    }
  }

  void _initializeControllers() {
    _nameController = TextEditingController()..addListener(_markAsDirty);
    _versionController = TextEditingController()..addListener(_markAsDirty);
    _displayNameController = TextEditingController()..addListener(_markAsDirty);
    _descriptionController = TextEditingController()..addListener(_markAsDirty);
    _unityVersionController = TextEditingController()..addListener(_markAsDirty);
    _unityReleaseController = TextEditingController()..addListener(_markAsDirty);
    _customLicenseController = TextEditingController()..addListener(_markAsDirty);
    _authorNameController = TextEditingController()..addListener(_markAsDirty);
    _authorEmailController = TextEditingController()..addListener(_markAsDirty);
    _categoryController = TextEditingController()..addListener(_markAsDirty);
    _homepageController = TextEditingController()..addListener(_markAsDirty);
    _repositoryTypeController = TextEditingController()..addListener(_markAsDirty);
    _repositoryUrlController = TextEditingController()..addListener(_markAsDirty);
    _bugsUrlController = TextEditingController()..addListener(_markAsDirty);
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
    _customLicenseController.dispose();
    _authorNameController.dispose();
    _authorEmailController.dispose();
    _categoryController.dispose();
    _homepageController.dispose();
    _repositoryTypeController.dispose();
    _repositoryUrlController.dispose();
    _bugsUrlController.dispose();
    super.dispose();
  }

  void _updateFormFromConfig(UnityPackageConfig config) {
    // 更新控制器值
    _nameController.text = config.name;
    _versionController.text = config.version;
    _displayNameController.text = config.displayName;
    _descriptionController.text = config.description;
    _unityVersionController.text = config.unityVersion;
    _unityReleaseController.text = config.unityRelease ?? '';
    _customLicenseController.text = config.license;
    _authorNameController.text = config.author.name ?? '';
    _authorEmailController.text = config.author.email ?? '';
    _categoryController.text = config.category ?? '';
    _homepageController.text = config.homepage ?? '';
    _repositoryTypeController.text = config.repository.type ?? '';
    _repositoryUrlController.text = config.repository.url ?? '';
    _bugsUrlController.text = config.bugs.url ?? '';

    setState(() {
      _keywords = List.from(config.keywords ?? []);
      _dependencies = Map.from(config.dependencies ?? {});
      _contributors = List.from(config.contributors ?? []);
      _samples = List.from(config.samples ?? []);
      _relatedPackages = Map.from(config.relatedPackages ?? {});
      _selectedLicenseType = LicenseType.fromLicenseName(config.license) ?? LicenseType.custom;
      if (_selectedLicenseType == LicenseType.custom) {
        _customLicenseController.text = config.license;
      }
      _isDirty = false;
    });

    _lastConfig = config;
    _isInitialized = true;
  }

  void _markAsDirty() {
    if (_isInitialized && !_isDirty && mounted) {
      WidgetsBinding.instance.addPostFrameCallback((_) {
        if (mounted) {
          setState(() {
            _isDirty = true;
          });
        }
      });
    }
  }

  void _handleValueChanged() {
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _markAsDirty();
    });
  }

  Future<void> _saveConfig() async {
    if (_formKey.currentState?.validate() ?? false) {
      final config = UnityPackageConfig(
        name: _nameController.text,
        version: _versionController.text,
        displayName: _displayNameController.text,
        description: _descriptionController.text,
        unityVersion: _unityVersionController.text,
        unityRelease: _unityReleaseController.text,
        license: _selectedLicenseType == LicenseType.custom
            ? _customLicenseController.text
            : _selectedLicenseType.standardName,
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

      if (mounted) {
        ref.read(packageSettingsProvider.notifier).updateConfig(config);
        widget.onConfigChanged?.call(config);
        setState(() {
          _isDirty = false;
        });

        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: const Text('配置已保存'),
            backgroundColor: Theme.of(context).colorScheme.primary,
            behavior: SnackBarBehavior.floating,
          ),
        );
      }
    }
  }

  Widget _buildVersionBadge(String version) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
      decoration: BoxDecoration(
        color: Colors.blue,
        borderRadius: BorderRadius.circular(4),
      ),
      child: Text(
        'v$version',
        style: const TextStyle(
          color: Colors.white,
          fontSize: 12,
          fontWeight: FontWeight.bold,
        ),
      ),
    );
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

    // 获取当前包名的现有版本
    final currentPackageName = _nameController.text;
    final existingVersion = savedState.getExistingVersion(currentPackageName);
    final isLoadingVersion = savedState.isLoadingPackageInfo;

    return Stack(
      children: [
        Form(
          key: _formKey,
          onChanged: _handleValueChanged,
          child: ListView(
            padding: const EdgeInsets.only(bottom: 80),
            children: [
              Card(
                margin: EdgeInsets.zero,
                shape: const RoundedRectangleBorder(
                  borderRadius: BorderRadius.zero,
                ),
                child: Padding(
                  padding: const EdgeInsets.all(16),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      // 第一行name, displayName, version
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
                                  return '请输入包名';
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
                                hintText: 'Package Display Name',
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
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Row(
                                  children: [
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
                                          return null;
                                        },
                                      ),
                                    ),
                                  ],
                                ),
                              ],
                            ),
                          ),
                        ],
                      ),
                      const SizedBox(height: 16),
                      // Unity版本选择器
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Row(
                            children: [
                              Text(
                                'Unity Version Configuration',
                                style: theme.textTheme.labelLarge,
                              ),
                              const SizedBox(width: 4),
                              SizedBox(
                                height: 12,
                                width: 12,
                                child: ref.watch(unityVersionProvider).isLoading
                                    ? CircularProgressIndicator(
                                        strokeWidth: 2,
                                        color: theme.colorScheme.primary,
                                      )
                                    : null,
                              ),
                            ],
                          ),
                          const SizedBox(height: 8),
                          Container(
                            decoration: BoxDecoration(
                              border: Border.all(
                                color: theme.colorScheme.outline.withOpacity(0.5),
                              ),
                              borderRadius: BorderRadius.circular(4),
                            ),
                            padding: const EdgeInsets.all(12),
                            child: UnityVersionSelector(
                              initialVersion: _unityVersionController.text,
                              initialRelease: _unityReleaseController.text,
                              onVersionChanged: (version) {
                                _unityVersionController.text = version;
                                _handleValueChanged();
                              },
                              onReleaseChanged: (release) {
                                _unityReleaseController.text = release ?? '';
                                _handleValueChanged();
                              },
                            ),
                          ),
                        ],
                      ),
                      const SizedBox(height: 16),
                      // 许可证选择
                      Row(
                        children: [
                          Expanded(
                            child: DropdownButtonFormField<LicenseType>(
                              value: _selectedLicenseType,
                              decoration: const InputDecoration(
                                labelText: 'license',
                                isDense: true,
                                contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                              ),
                              items: LicenseType.values.map((type) {
                                return DropdownMenuItem(
                                  value: type,
                                  child: Text(type.displayName),
                                );
                              }).toList(),
                              onChanged: (value) {
                                if (value != null) {
                                  setState(() {
                                    _selectedLicenseType = value;
                                    if (value != LicenseType.custom) {
                                      _customLicenseController.text = value.standardName;
                                    }
                                    _handleValueChanged();
                                  });
                                }
                              },
                              validator: (value) {
                                if (value == LicenseType.custom && _customLicenseController.text.isEmpty) {
                                  return '请输入自定义许可证';
                                }
                                return null;
                              },
                            ),
                          ),
                          if (_selectedLicenseType == LicenseType.custom) ...[
                            const SizedBox(width: 16),
                            Expanded(
                              child: TextFormField(
                                controller: _customLicenseController,
                                decoration: const InputDecoration(
                                  labelText: 'custom license',
                                  hintText: '输入自定义许可证名称',
                                  isDense: true,
                                  contentPadding: EdgeInsets.symmetric(horizontal: 8, vertical: 8),
                                ),
                                validator: (value) {
                                  if (value == null || value.isEmpty) {
                                    return '请输入自定义许可证';
                                  }
                                  return null;
                                },
                              ),
                            ),
                          ],
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
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            'keywords',
                            style: theme.textTheme.labelLarge,
                          ),
                          const SizedBox(height: 8),
                          KeywordsSelector(
                            selectedKeywords: _keywords,
                            onKeywordsChanged: (keywords) {
                              setState(() {
                                _keywords = keywords;
                              });
                              _handleValueChanged();
                            },
                          ),
                        ],
                      ),
                      const SizedBox(height: 16),
                      // 作者信息
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
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            'contributors',
                            style: theme.textTheme.labelLarge,
                          ),
                          const SizedBox(height: 8),
                          ContributorsSelector(
                            selectedContributors: _contributors,
                            onContributorsChanged: (contributors) {
                              setState(() {
                                _contributors = contributors;
                              });
                              _handleValueChanged();
                            },
                          ),
                        ],
                      ),
                      const SizedBox(height: 16),
                      // 分类
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Row(
                            children: [
                              Text(
                                'category',
                                style: theme.textTheme.labelLarge,
                              ),
                              const SizedBox(width: 8),
                              SizedBox(
                                height: 12,
                                width: 12,
                                child: ref.watch(categoryProvider).isLoading
                                    ? CircularProgressIndicator(
                                        strokeWidth: 2,
                                        color: theme.colorScheme.primary,
                                      )
                                    : null,
                              ),
                            ],
                          ),
                          const SizedBox(height: 8),
                          CategorySelector(
                            selectedCategory: _categoryController.text,
                            onCategoryChanged: (category) {
                              setState(() {
                                _categoryController.text = category ?? '';
                              });
                              _handleValueChanged();
                            },
                          ),
                        ],
                      ),
                      const SizedBox(height: 16),
                      // 主页
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
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            'relatedPackages',
                            style: theme.textTheme.titleSmall,
                          ),
                          const SizedBox(height: 8),
                          if (_relatedPackages.isEmpty)
                            Text(
                              '无相关包',
                              style: theme.textTheme.bodyMedium?.copyWith(
                                color: theme.colorScheme.onSurfaceVariant.withOpacity(0.7),
                              ),
                            )
                          else
                            Wrap(
                              spacing: 8,
                              runSpacing: 8,
                              children: _relatedPackages.entries.map((entry) {
                                return Chip(
                                  label: Text('${entry.key}:${entry.value}'),
                                  backgroundColor: theme.colorScheme.surfaceContainerHighest,
                                  labelStyle: TextStyle(color: theme.colorScheme.onSurfaceVariant),
                                );
                              }).toList(),
                            ),
                        ],
                      ),
                      const SizedBox(height: 16),
                      // 依赖
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            'dependencies',
                            style: theme.textTheme.titleSmall,
                          ),
                          const SizedBox(height: 8),
                          if (_dependencies.isEmpty)
                            Text(
                              '无依赖项',
                              style: theme.textTheme.bodyMedium?.copyWith(
                                color: theme.colorScheme.onSurfaceVariant.withOpacity(0.7),
                              ),
                            )
                          else
                            Wrap(
                              spacing: 8,
                              runSpacing: 8,
                              children: _dependencies.entries.map((entry) {
                                return Chip(
                                  label: Text('${entry.key}@${entry.value}'),
                                  backgroundColor: theme.colorScheme.surfaceContainerHighest,
                                  labelStyle: TextStyle(color: theme.colorScheme.onSurfaceVariant),
                                );
                              }).toList(),
                            ),
                        ],
                      ),
                      const SizedBox(height: 16),
                      // 示例
                      Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            'samples',
                            style: theme.textTheme.titleSmall,
                          ),
                          const SizedBox(height: 8),
                          if (_samples.isEmpty)
                            Text(
                              '无示例',
                              style: theme.textTheme.bodyMedium?.copyWith(
                                color: theme.colorScheme.onSurfaceVariant.withOpacity(0.7),
                              ),
                            )
                          else
                            ListView.separated(
                              shrinkWrap: true,
                              physics: const NeverScrollableScrollPhysics(),
                              itemCount: _samples.length,
                              separatorBuilder: (context, index) => const SizedBox(height: 8),
                              itemBuilder: (context, index) {
                                final sample = _samples[index];
                                return Container(
                                  padding: const EdgeInsets.all(12),
                                  decoration: BoxDecoration(
                                    color: theme.colorScheme.surfaceContainerHighest,
                                    borderRadius: BorderRadius.circular(4),
                                  ),
                                  child: Column(
                                    crossAxisAlignment: CrossAxisAlignment.start,
                                    children: [
                                      Text(
                                        sample.displayName,
                                        style: theme.textTheme.titleSmall,
                                      ),
                                      if (sample.description.isNotEmpty) ...[
                                        const SizedBox(height: 4),
                                        Text(
                                          sample.description,
                                          style: theme.textTheme.bodyMedium,
                                        ),
                                      ],
                                      const SizedBox(height: 4),
                                      Text(
                                        sample.path,
                                        style: theme.textTheme.bodySmall?.copyWith(
                                          color: theme.colorScheme.onSurfaceVariant.withOpacity(0.7),
                                        ),
                                      ),
                                    ],
                                  ),
                                );
                              },
                            ),
                        ],
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
        Positioned(
          left: 0,
          right: 0,
          bottom: 0,
          child: Card(
            margin: EdgeInsets.zero,
            shape: const RoundedRectangleBorder(
              borderRadius: BorderRadius.zero,
            ),
            elevation: 8,
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.end,
                children: [
                  FilledButton.icon(
                    onPressed: () async {
                      final savedState = ref.read(packageSettingsProvider);
                      if (savedState.currentProjectPath != null) {
                        final packageJsonFile = File(path.join(savedState.currentProjectPath!, 'package.json'));
                        final uri = Uri.file(packageJsonFile.path);
                        if (await url_launcher.canLaunchUrl(uri)) {
                          await url_launcher.launchUrl(uri);
                        }
                      }
                    },
                    icon: const Icon(Icons.code),
                    label: const Text('打开源文件'),
                  ),
                  const SizedBox(width: 8),
                  FilledButton.icon(
                    onPressed: _saveConfig,
                    icon: const Icon(Icons.save),
                    label: const Text('保存'),
                  ),
                ],
              ),
            ),
          ),
        ),
      ],
    );
  }
}
