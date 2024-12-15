import 'package:equatable/equatable.dart';

/// Unity Package 配置模型
class UnityPackageConfig extends Equatable {
  final String name;
  final String displayName;
  final String version;
  final String unityVersion;
  final String unityRelease;
  final String description;
  final String license;
  final List<String> keywords;
  final PackageAuthor author;
  final List<Contributor> contributors;
  final String category;
  final String homepage;
  final Repository repository;
  final BugTracker bugs;
  final Map<String, String> relatedPackages;
  final Map<String, String> dependencies;
  final List<Sample> samples;

  const UnityPackageConfig({
    required this.name,
    required this.displayName,
    required this.version,
    required this.unityVersion,
    this.unityRelease = '',
    required this.description,
    required this.license,
    this.keywords = const [],
    required this.author,
    this.contributors = const [],
    this.category = '',
    this.homepage = '',
    this.repository = const Repository(),
    this.bugs = const BugTracker(),
    this.relatedPackages = const {},
    this.dependencies = const {},
    this.samples = const [],
  });

  factory UnityPackageConfig.fromJson(Map<String, dynamic> json) {
    return UnityPackageConfig(
      name: json['name'] as String? ?? '',
      displayName: json['displayName'] as String? ?? '',
      version: json['version'] as String? ?? '1.0.0',
      unityVersion: json['unity'] as String? ?? '2021.3',
      unityRelease: json['unityRelease'] as String? ?? '',
      description: json['description'] as String? ?? '',
      license: json['license'] as String? ?? 'MIT',
      keywords: List<String>.from(json['keywords'] as List? ?? []),
      author: json['author'] != null ? PackageAuthor.fromJson(json['author']) : const PackageAuthor(name: ''),
      contributors:
          (json['contributors'] as List?)?.map((e) => Contributor.fromJson(e as Map<String, dynamic>)).toList() ??
              const [],
      category: json['category'] as String? ?? '',
      homepage: json['homepage'] as String? ?? '',
      repository: json['repository'] != null
          ? Repository.fromJson(json['repository'] as Map<String, dynamic>)
          : const Repository(),
      bugs: json['bugs'] != null ? BugTracker.fromJson(json['bugs'] as Map<String, dynamic>) : const BugTracker(),
      relatedPackages: Map<String, String>.from(json['relatedPackages'] as Map? ?? {}),
      dependencies: Map<String, String>.from(json['dependencies'] as Map? ?? {}),
      samples: (json['samples'] as List?)?.map((e) => Sample.fromJson(e as Map<String, dynamic>)).toList() ?? const [],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'displayName': displayName,
      'version': version,
      'unity': unityVersion,
      'unityRelease': unityRelease,
      'description': description,
      'license': license,
      'keywords': keywords,
      'author': author.toJson(),
      'contributors': contributors.map((e) => e.toJson()).toList(),
      'category': category,
      'homepage': homepage,
      'repository': repository.toJson(),
      'bugs': bugs.toJson(),
      'relatedPackages': relatedPackages,
      'dependencies': dependencies,
      'samples': samples.map((e) => e.toJson()).toList(),
    };
  }

  @override
  List<Object?> get props => [
        name,
        displayName,
        version,
        unityVersion,
        unityRelease,
        description,
        license,
        keywords,
        author,
        contributors,
        category,
        homepage,
        repository,
        bugs,
        relatedPackages,
        dependencies,
        samples,
      ];
}

class PackageAuthor extends Equatable {
  final String name;
  final String email;

  const PackageAuthor({
    required this.name,
    this.email = '',
  });

  factory PackageAuthor.fromJson(dynamic json) {
    if (json is String) {
      return PackageAuthor(name: json);
    }
    if (json is Map<String, dynamic>) {
      return PackageAuthor(
        name: json['name'] as String? ?? '',
        email: json['email'] as String? ?? '',
      );
    }
    return const PackageAuthor(name: '');
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'email': email,
    };
  }

  @override
  List<Object?> get props => [name, email];
}

class Contributor extends Equatable {
  final String name;
  final String email;

  const Contributor({
    required this.name,
    this.email = '',
  });

  factory Contributor.fromJson(Map<String, dynamic> json) {
    return Contributor(
      name: json['name'] as String? ?? '',
      email: json['email'] as String? ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'email': email,
    };
  }

  @override
  List<Object?> get props => [name, email];
}

class Repository extends Equatable {
  final String type;
  final String url;

  const Repository({
    this.type = '',
    this.url = '',
  });

  factory Repository.fromJson(Map<String, dynamic> json) {
    return Repository(
      type: json['type'] as String? ?? '',
      url: json['url'] as String? ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'type': type,
      'url': url,
    };
  }

  @override
  List<Object?> get props => [type, url];
}

class BugTracker extends Equatable {
  final String url;

  const BugTracker({
    this.url = '',
  });

  factory BugTracker.fromJson(Map<String, dynamic> json) {
    return BugTracker(
      url: json['url'] as String? ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'url': url,
    };
  }

  @override
  List<Object?> get props => [url];
}

class Sample extends Equatable {
  final String displayName;
  final String description;
  final String path;

  const Sample({
    required this.displayName,
    required this.description,
    required this.path,
  });

  factory Sample.fromJson(Map<String, dynamic> json) {
    return Sample(
      displayName: json['displayName'] as String? ?? '',
      description: json['description'] as String? ?? '',
      path: json['path'] as String? ?? '',
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'displayName': displayName,
      'description': description,
      'path': path,
    };
  }

  @override
  List<Object?> get props => [displayName, description, path];
}
