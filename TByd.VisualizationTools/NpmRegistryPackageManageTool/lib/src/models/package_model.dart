import 'package:equatable/equatable.dart';

class Package extends Equatable {
  final String name;
  final String displayName;
  final String version;
  final String description;
  final String author;
  final DateTime publishedAt;
  final Map<String, String> dist;
  final Map<String, String> dependencies;
  final List<String> keywords;
  final String license;

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
  }) : displayName = displayName ?? name;

  factory Package.fromJson(Map<String, dynamic> json) {
    try {
      final timeData = json['time'] as Map<String, dynamic>?;
      final version = json['version']?.toString() ?? json['dist-tags']?['latest']?.toString() ?? '0.0.0';

      return Package(
        name: json['name']?.toString() ?? '',
        displayName: json['displayName']?.toString(),
        version: version,
        description: json['description']?.toString() ?? '',
        author: _extractAuthor(json['author']),
        publishedAt: _parseDateTime(timeData?[version] ?? timeData?['modified']),
        dist: _extractDist(json['dist']),
        dependencies: _extractDependencies(json['dependencies']),
        keywords: _extractKeywords(json['keywords']),
        license: json['license']?.toString() ?? 'Unknown',
      );
    } catch (e) {
      print('解析包详情时出错: $e');
      rethrow;
    }
  }

  static String _extractAuthor(dynamic author) {
    if (author == null) return 'Unknown';
    if (author is String) return author;
    if (author is Map) return author['name']?.toString() ?? 'Unknown';
    return 'Unknown';
  }

  static DateTime _parseDateTime(dynamic dateStr) {
    if (dateStr == null) return DateTime.now();
    try {
      return DateTime.parse(dateStr.toString());
    } catch (e) {
      return DateTime.now();
    }
  }

  static Map<String, String> _extractDist(dynamic dist) {
    if (dist == null) return {};
    if (dist is Map) {
      return Map<String, String>.from(
        dist.map((key, value) => MapEntry(key.toString(), value.toString())),
      );
    }
    return {};
  }

  static Map<String, String> _extractDependencies(dynamic deps) {
    if (deps == null) return {};
    if (deps is Map) {
      return Map<String, String>.from(
        deps.map((key, value) => MapEntry(key.toString(), value.toString())),
      );
    }
    return {};
  }

  static List<String> _extractKeywords(dynamic keywords) {
    if (keywords == null) return [];
    if (keywords is List) {
      return keywords.map((k) => k.toString()).toList();
    }
    return [];
  }

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
    };
  }

  @override
  List<Object?> get props => [name, version];
}

class PackageVersion extends Equatable {
  final String version;
  final DateTime publishedAt;
  final Map<String, String> dist;
  final Map<String, String> dependencies;

  const PackageVersion({
    required this.version,
    required this.publishedAt,
    required this.dist,
    required this.dependencies,
  });

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

  @override
  List<Object?> get props => [version];
}

class PackageSearchResult extends Equatable {
  final String name;
  final String displayName;
  final String version;
  final String description;
  final String author;
  final DateTime lastModified;

  const PackageSearchResult({
    required this.name,
    String? displayName,
    required this.version,
    required this.description,
    required this.author,
    required this.lastModified,
  }) : displayName = displayName ?? name;

  factory PackageSearchResult.fromJson(Map<String, dynamic> json) {
    final time = json['time'] as Map<String, dynamic>?;
    final latest = json['dist-tags']?['latest'] as String?;
    final versions = json['versions'] as Map<String, dynamic>?;
    final latestVersion = versions?[latest ?? ''] as Map<String, dynamic>?;

    return PackageSearchResult(
      name: json['name'] as String,
      version: latest ?? '0.0.0',
      description: latestVersion?['description'] as String? ?? json['description'] as String? ?? '',
      author: _extractAuthor(latestVersion?['author'] ?? json['author']),
      lastModified: DateTime.parse(time?['modified'] as String? ?? DateTime.now().toIso8601String()),
    );
  }

  static String _extractAuthor(dynamic author) {
    if (author == null) return 'Unknown';
    if (author is String) return author;
    if (author is Map) return author['name'] as String? ?? 'Unknown';
    return 'Unknown';
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'version': version,
      'description': description,
      'author': author,
      'lastModified': lastModified.toIso8601String(),
    };
  }

  @override
  List<Object?> get props => [name, version];
}
