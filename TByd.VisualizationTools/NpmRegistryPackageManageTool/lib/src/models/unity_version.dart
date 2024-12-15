import 'package:equatable/equatable.dart';

/// Unity 版本信息模型
class UnityVersion extends Equatable {
  /// 版本号
  final String version;

  /// 是否是长期支持版本
  final bool lts;

  /// 下载链接
  final Map<String, String> downloadUrl;

  /// 发布日期
  final DateTime? releaseDate;

  /// 版本类型 (official, beta, alpha)
  final String versionType;

  /// 版本大小（字节）
  final Map<String, int>? size;

  /// 版本校验和
  final Map<String, String>? checksum;

  /// 支持的模块
  final List<UnityModule>? modules;

  /// 构造函数
  const UnityVersion({
    required this.version,
    required this.downloadUrl,
    this.lts = false,
    this.releaseDate,
    this.versionType = 'official',
    this.size,
    this.checksum,
    this.modules,
  });

  /// 从 JSON 创建实例
  factory UnityVersion.fromJson(Map<String, dynamic> json) {
    return UnityVersion(
      version: json['version'] as String,
      lts: json['lts'] as bool? ?? false,
      downloadUrl: Map<String, String>.from(json['downloadUrl'] as Map),
      releaseDate: json['releaseDate'] != null ? DateTime.parse(json['releaseDate'] as String) : null,
      versionType: json['versionType'] as String? ?? 'official',
      size: json['size'] != null ? Map<String, int>.from(json['size'] as Map) : null,
      checksum: json['checksum'] != null ? Map<String, String>.from(json['checksum'] as Map) : null,
      modules: json['modules'] != null
          ? (json['modules'] as List).map((e) => UnityModule.fromJson(e as Map<String, dynamic>)).toList()
          : null,
    );
  }

  /// 转换为 JSON
  Map<String, dynamic> toJson() {
    return {
      'version': version,
      'lts': lts,
      'downloadUrl': downloadUrl,
      'releaseDate': releaseDate?.toIso8601String(),
      'versionType': versionType,
      'size': size,
      'checksum': checksum,
      'modules': modules?.map((e) => e.toJson()).toList(),
    };
  }

  @override
  List<Object?> get props => [
        version,
        lts,
        downloadUrl,
        releaseDate,
        versionType,
        size,
        checksum,
        modules,
      ];

  @override
  String toString() => 'UnityVersion(version: $version, type: $versionType, lts: $lts)';
}

/// Unity 模块信息
class UnityModule extends Equatable {
  /// 模块ID
  final String id;

  /// 模块名称
  final String name;

  /// 模块描述
  final String description;

  /// 下载链接
  final String downloadUrl;

  /// 目标路径
  final String destination;

  /// 模块类别
  final String category;

  /// 安装大小（字节）
  final int? installedSize;

  /// 下载大小（字节）
  final int? downloadSize;

  /// 是否可见
  final bool visible;

  /// 是否已选择
  final bool selected;

  /// 校验和
  final String? checksum;

  /// 构造函数
  const UnityModule({
    required this.id,
    required this.name,
    required this.description,
    required this.downloadUrl,
    required this.destination,
    required this.category,
    this.installedSize,
    this.downloadSize,
    this.visible = true,
    this.selected = false,
    this.checksum,
  });

  /// 从 JSON 创建实例
  factory UnityModule.fromJson(Map<String, dynamic> json) {
    return UnityModule(
      id: json['id'] as String,
      name: json['name'] as String,
      description: json['description'] as String,
      downloadUrl: json['downloadUrl'] as String,
      destination: json['destination'] as String,
      category: json['category'] as String,
      installedSize: json['installedSize'] != null ? int.tryParse(json['installedSize'].toString()) : null,
      downloadSize: json['downloadSize'] != null ? int.tryParse(json['downloadSize'].toString()) : null,
      visible: json['visible'] as bool? ?? true,
      selected: json['selected'] as bool? ?? false,
      checksum: json['checksum'] as String?,
    );
  }

  /// 转换为 JSON
  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
      'description': description,
      'downloadUrl': downloadUrl,
      'destination': destination,
      'category': category,
      'installedSize': installedSize,
      'downloadSize': downloadSize,
      'visible': visible,
      'selected': selected,
      'checksum': checksum,
    };
  }

  @override
  List<Object?> get props => [
        id,
        name,
        description,
        downloadUrl,
        destination,
        category,
        installedSize,
        downloadSize,
        visible,
        selected,
        checksum,
      ];

  @override
  String toString() => 'UnityModule(id: $id, name: $name)';
}
