/// Unity 版本信息模型
///
/// 用于存储从 Unity Hub API 获取的版本信息
class UnityVersion {
  /// 版本号
  final String version;

  /// 发布说明
  final String releaseNotes;

  /// 长版本号（可选）
  final String? longVersion;

  /// 发布日期（可选）
  final String? releaseDate;

  /// 更新日期（可选）
  final String? updateDate;

  /// 是否是 LTS 版本（可选）
  final bool? isLts;

  /// 是否支持 Apple Silicon（可选）
  final bool? supportAppleSilicon;

  /// 构造函数
  const UnityVersion({
    required this.version,
    required this.releaseNotes,
    this.longVersion,
    this.releaseDate,
    this.updateDate,
    this.isLts,
    this.supportAppleSilicon,
  });

  /// 从 JSON 创建 UnityVersion 实例
  factory UnityVersion.fromJson(Map<String, dynamic> json) {
    return UnityVersion(
      version: json['version'] as String,
      releaseNotes: json['releaseNotes'] as String,
      longVersion: json['longVersion'] as String?,
      releaseDate: json['releaseDate'] as String?,
      updateDate: json['updateDate'] as String?,
      isLts: json['lts'] as bool?,
      supportAppleSilicon: json['supportAppleSilicon'] as bool?,
    );
  }

  /// 转换为 JSON
  Map<String, dynamic> toJson() {
    return {
      'version': version,
      'releaseNotes': releaseNotes,
      'longVersion': longVersion,
      'releaseDate': releaseDate,
      'updateDate': updateDate,
      'lts': isLts,
      'supportAppleSilicon': supportAppleSilicon,
    };
  }
}
