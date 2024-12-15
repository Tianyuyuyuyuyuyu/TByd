import 'package:equatable/equatable.dart';

/// Unity 版本信息模型
class UnityVersion extends Equatable {
  /// 版本号
  final String version;

  /// 是否是长期支持版本
  final bool lts;

  /// 下载链接
  final Map<String, String> downloadUrl;

  /// 构造函数
  const UnityVersion({
    required this.version,
    required this.downloadUrl,
    this.lts = false,
  });

  /// 从 JSON 创建实例
  factory UnityVersion.fromJson(Map<String, dynamic> json) {
    return UnityVersion(
      version: json['version'] as String,
      lts: json['lts'] as bool? ?? false,
      downloadUrl: Map<String, String>.from(json['downloadUrl'] as Map),
    );
  }

  /// 转换为 JSON
  Map<String, dynamic> toJson() {
    return {
      'version': version,
      'lts': lts,
      'downloadUrl': downloadUrl,
    };
  }

  @override
  List<Object?> get props => [version, lts, downloadUrl];

  @override
  String toString() => 'UnityVersion(version: $version, lts: $lts)';
}
