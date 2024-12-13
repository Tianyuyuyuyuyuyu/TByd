import 'package:equatable/equatable.dart';

class Package extends Equatable {
  final String name;
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
    required this.version,
    required this.description,
    required this.author,
    required this.publishedAt,
    required this.dist,
    required this.dependencies,
    required this.keywords,
    required this.license,
  });

  factory Package.fromJson(Map<String, dynamic> json) {
    final timeData = json['time'] as Map<String, dynamic>?;
    final version = json['version'] as String;

    return Package(
      name: json['name'] as String,
      version: version,
      description: json['description'] as String? ?? '',
      author: json['author'] is String ? json['author'] as String : json['author']?['name'] as String? ?? 'Unknown',
      publishedAt: DateTime.parse(timeData?[version] ?? timeData?['modified'] ?? DateTime.now().toIso8601String()),
      dist: Map<String, String>.from(json['dist'] as Map? ?? {}),
      dependencies: Map<String, String>.from(json['dependencies'] as Map? ?? {}),
      keywords: List<String>.from(json['keywords'] as List? ?? []),
      license: json['license'] as String? ?? 'Unknown',
    );
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
  final String version;
  final String description;
  final String author;
  final DateTime lastModified;

  const PackageSearchResult({
    required this.name,
    required this.version,
    required this.description,
    required this.author,
    required this.lastModified,
  });

  factory PackageSearchResult.fromJson(Map<String, dynamic> json) {
    return PackageSearchResult(
      name: json['name'] as String,
      version: json['version'] as String,
      description: json['description'] as String? ?? '',
      author: json['author'] as String? ?? 'Unknown',
      lastModified: DateTime.parse(json['lastModified'] as String),
    );
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
