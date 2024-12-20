/// NPM Registry Manager - NPM包数据模型
///
/// 该文件定义了NPM包相关的数据结构，包括：
/// - 包的基本信息
/// - 版本信息
/// - 搜索结果
/// - 依赖关系
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:equatable/equatable.dart';

/// NPM包类
///
/// 存储和管理NPM包的完整信息，包括：
/// - 基本信息（名称、版本等）
/// - 发布信息
/// - 依赖关系
/// - 分发信息
class Package extends Equatable {
  /// 包名
  final String name;

  /// 显示名称
  final String displayName;

  /// 版本号
  final String version;

  /// 包描述
  final String description;

  /// 作者
  final String author;

  /// 发布时间
  final DateTime publishedAt;

  /// 分发信息（如tarball URL等）
  final Map<String, String> dist;

  /// 依赖包列表
  final Map<String, String> dependencies;

  /// 关键词列表
  final List<String> keywords;

  /// 许可证
  final String license;

  /// 代码仓库地址
  final String? repository;

  /// 主页地址
  final String? homepage;

  /// Bug追踪地址
  final String? bugsUrl;

  /// 分类
  final String? category;

  /// 构造函数
  ///
  /// 创建一个新的包实例
  ///
  /// 参数：
  /// - [name] 包名
  /// - [displayName] 显示名称（可选）
  /// - [version] 版本号
  /// - [description] 描述
  /// - [author] 作者
  /// - [publishedAt] 发布时间
  /// - [dist] 分发信息
  /// - [dependencies] 依赖关系
  /// - [keywords] 关键词
  /// - [license] 许可证
  /// - [repository] 仓库地址
  /// - [homepage] 主页
  /// - [bugsUrl] Bug追踪
  /// - [category] 分类
  const Package({
    required this.name,
    String? displayName,
    required this.version,
    required this.description,
    required this.author,
    required this.publishedAt,
    required this.dist,
    required this.dependencies,
    required this.keywords,
    required this.license,
    this.repository,
    this.homepage,
    this.bugsUrl,
    this.category,
  }) : displayName = displayName ?? name;

  /// 从JSON创建实例
  ///
  /// 工厂构造函数，用于从NPM仓库返回的JSON数据创建Package实例
  /// 处理各种可能的数据格式和缺失字段
  factory Package.fromJson(Map<String, dynamic> json) {
    try {
      final timeData = json['time'] as Map<String, dynamic>?;
      final version = json['version']?.toString() ?? json['dist-tags']?['latest']?.toString() ?? '0.0.0';
      final versions = json['versions'] as Map<String, dynamic>?;
      final versionData = versions?[version] as Map<String, dynamic>?;

      // 解析仓库地址
      String? repositoryUrl;
      final repository = versionData?['repository'] ?? json['repository'];
      if (repository != null) {
        if (repository is String) {
          repositoryUrl = repository;
        } else if (repository is Map) {
          repositoryUrl = repository['url']?.toString();
        }
      }

      // 清理仓库地址
      if (repositoryUrl != null) {
        repositoryUrl = repositoryUrl
            .replaceAll(RegExp(r'^git\+'), '')
            .replaceAll(RegExp(r'\.git$'), '')
            .replaceAll('git://', 'https://')
            .replaceAll('ssh://git@', 'https://');
      }

      // 获取 homepage 地址
      String? homepageUrl = versionData?['homepage']?.toString() ?? json['homepage']?.toString();

      // 解析 bugs URL
      String? bugsUrl;
      final bugs = versionData?['bugs'] ?? json['bugs'];
      if (bugs != null) {
        if (bugs is String) {
          bugsUrl = bugs;
        } else if (bugs is Map) {
          bugsUrl = bugs['url']?.toString();
        }
      }

      // 获取分类
      String? category = versionData?['category']?.toString() ?? json['category']?.toString();

      return Package(
        name: json['name']?.toString() ?? '',
        displayName: versionData?['displayName']?.toString() ?? json['displayName']?.toString(),
        version: version,
        description: versionData?['description']?.toString() ?? json['description']?.toString() ?? '',
        author: _extractAuthor(versionData?['author'] ?? json['author']),
        publishedAt: _parseDateTime(timeData?[version] ?? timeData?['modified']),
        dist: _extractDist(versionData?['dist'] ?? json['dist']),
        dependencies: _extractDependencies(versionData?['dependencies'] ?? json['dependencies']),
        keywords: _extractKeywords(versionData?['keywords'] ?? json['keywords']),
        license: versionData?['license']?.toString() ?? json['license']?.toString() ?? 'Unknown',
        repository: repositoryUrl,
        homepage: homepageUrl,
        bugsUrl: bugsUrl,
        category: category,
      );
    } catch (e) {
      print('解析包详情时出错: $e');
      rethrow;
    }
  }

  /// 提取作者信息
  ///
  /// 从不同格式的作者数据中提取作者名称
  static String _extractAuthor(dynamic author) {
    if (author == null) return 'Unknown';
    if (author is String) return author;
    if (author is Map) return author['name']?.toString() ?? 'Unknown';
    return 'Unknown';
  }

  /// 解析日期时间
  ///
  /// 将字符串格式的日期时间转换为DateTime对象
  static DateTime _parseDateTime(dynamic dateStr) {
    if (dateStr == null) return DateTime.now();
    try {
      return DateTime.parse(dateStr.toString());
    } catch (e) {
      return DateTime.now();
    }
  }

  /// 提取分发信息
  ///
  /// 从JSON数据中提取包的分发信息
  static Map<String, String> _extractDist(dynamic dist) {
    if (dist == null) return {};
    if (dist is Map) {
      return Map<String, String>.from(
        dist.map((key, value) => MapEntry(key.toString(), value.toString())),
      );
    }
    return {};
  }

  /// 提取依赖关系
  ///
  /// 从JSON数据中提取包的依赖关系
  static Map<String, String> _extractDependencies(dynamic deps) {
    if (deps == null) return {};
    if (deps is Map) {
      return Map<String, String>.from(
        deps.map((key, value) => MapEntry(key.toString(), value.toString())),
      );
    }
    return {};
  }

  /// 提取关键词
  ///
  /// 从JSON数据中提取包的关键词列表
  static List<String> _extractKeywords(dynamic keywords) {
    if (keywords == null) return [];
    if (keywords is List) {
      return keywords.map((k) => k.toString()).toList();
    }
    return [];
  }

  /// 转换为JSON
  ///
  /// 将包信息转换为可序列化的Map结构
  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'version': version,
      'description': description,
      'author': author,
      'time': {version: publishedAt.toIso8601String()},
      'dist': dist,
      'dependencies': dependencies,
      'keywords': keywords,
      'license': license,
      'repository': repository,
      'homepage': homepage,
      'category': category,
    };
  }

  /// 获取用于相等性比较的属性列表
  @override
  List<Object?> get props => [name, version, repository];
}

/// 包版本类
///
/// 存储特定版本的包信息，包括：
/// - 版本号
/// - 发布时间
/// - 分发信息
/// - 依赖关系
class PackageVersion extends Equatable {
  /// 版本号
  final String version;

  /// 发布时间
  final DateTime publishedAt;

  /// 分发信息
  final Map<String, String> dist;

  /// 依赖关系
  final Map<String, String> dependencies;

  /// 构造函数
  const PackageVersion({
    required this.version,
    required this.publishedAt,
    required this.dist,
    required this.dependencies,
  });

  /// 从JSON创建实例
  factory PackageVersion.fromJson(Map<String, dynamic> json, String version) {
    final timeData = json['time'] as Map<String, dynamic>?;
    final versionsData = json['versions'] as Map<String, dynamic>?;
    final versionData = versionsData?[version] as Map<String, dynamic>?;

    return PackageVersion(
      version: version,
      publishedAt: DateTime.parse(timeData?[version] ?? DateTime.now().toIso8601String()),
      dist: Map<String, String>.from(versionData?['dist'] as Map? ?? {}),
      dependencies: Map<String, String>.from(versionData?['dependencies'] as Map? ?? {}),
    );
  }

  /// 获取用于相等性比较的属性列表
  @override
  List<Object?> get props => [version];
}

/// 包搜索结果类
///
/// 存储包搜索结果的简要信息，包括：
/// - 基本信息
/// - 最新版本
/// - 最后修改时间
class PackageSearchResult extends Equatable {
  /// 包名
  final String name;

  /// 显示名称
  final String displayName;

  /// 版本号
  final String version;

  /// 描述
  final String description;

  /// 作者
  final String author;

  /// 许可证
  final String? license;

  /// 最后修改时间
  final DateTime lastModified;

  /// 关键词列表
  final List<String> keywords;

  /// 构造函数
  const PackageSearchResult({
    required this.name,
    String? displayName,
    required this.version,
    required this.description,
    required this.author,
    this.license,
    required this.lastModified,
    this.keywords = const [],
  }) : displayName = displayName ?? name;

  /// 从JSON创建实例
  factory PackageSearchResult.fromJson(Map<String, dynamic> json) {
    final time = json['time'] as Map<String, dynamic>?;
    final latest = json['dist-tags']?['latest'] as String?;
    final versions = json['versions'] as Map<String, dynamic>?;
    final latestVersion = versions?[latest ?? ''] as Map<String, dynamic>?;

    // 解析关键词
    List<String> keywords = [];
    if (latestVersion != null && latestVersion['keywords'] is List) {
      keywords = List<String>.from(latestVersion['keywords'] as List);
    } else if (json['keywords'] is List) {
      keywords = List<String>.from(json['keywords'] as List);
    }

    // 解析显示名称
    String? displayName;
    if (latestVersion != null) {
      displayName = latestVersion['displayName'] as String?;
    }
    if (displayName == null) {
      displayName = json['displayName'] as String?;
    }
    if (displayName == null) {
      displayName = latestVersion?['name'] as String?;
    }

    return PackageSearchResult(
      name: json['name'] as String,
      displayName: displayName,
      version: latest ?? '0.0.0',
      description: latestVersion?['description'] as String? ?? json['description'] as String? ?? '',
      author: _extractAuthor(latestVersion?['author'] ?? json['author']),
      lastModified: DateTime.parse(time?['modified'] as String? ?? DateTime.now().toIso8601String()),
      keywords: keywords,
      license: latestVersion?['license'] as String? ?? json['license'] as String? ?? 'Unknown',
    );
  }

  /// 提取作者信息
  static String _extractAuthor(dynamic author) {
    if (author == null) return 'Unknown';
    if (author is String) return author;
    if (author is Map) return author['name'] as String? ?? 'Unknown';
    return 'Unknown';
  }

  /// 转换为JSON
  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'version': version,
      'description': description,
      'author': author,
      'lastModified': lastModified.toIso8601String(),
      'keywords': keywords,
      'license': license,
    };
  }

  /// 获取用于相等性比较的属性列表
  @override
  List<Object?> get props => [name, version];
}
