/// 许可证类型枚举
enum LicenseType {
  mit('MIT License'),
  apache2('Apache License 2.0'),
  gplV3('GNU General Public License (GPL) v3'),
  mpl2('Mozilla Public License 2.0 (MPL-2.0)'),
  unityPackage('Unity Package Distribution License (v2.1)'),
  custom('Custom License');

  final String displayName;
  const LicenseType(this.displayName);

  /// 从许可证名称获取许可证类型
  static LicenseType? fromLicenseName(String name) {
    return LicenseType.values.firstWhere(
      (type) => type.name.toLowerCase() == name.toLowerCase() || type.displayName.toLowerCase() == name.toLowerCase(),
      orElse: () => LicenseType.custom,
    );
  }

  /// 获取许可证的标准名称
  String get standardName {
    switch (this) {
      case LicenseType.mit:
        return 'MIT';
      case LicenseType.apache2:
        return 'Apache-2.0';
      case LicenseType.gplV3:
        return 'GPL-3.0';
      case LicenseType.mpl2:
        return 'MPL-2.0';
      case LicenseType.unityPackage:
        return 'Unity-Package-2.1';
      case LicenseType.custom:
        return 'Custom';
    }
  }
}
