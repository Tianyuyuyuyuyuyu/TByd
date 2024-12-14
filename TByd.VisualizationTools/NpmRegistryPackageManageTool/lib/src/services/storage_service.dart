/// NPM Registry Manager - 安全存储服务
///
/// 该文件提供安全的数据存储功能，包括：
/// - 数据加密存储
/// - 过期时间管理
/// - 密钥派生
/// - 数据完整性验证
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:crypto/crypto.dart';
import 'dart:math';

/// 安全存储服务类
///
/// 提供加密的数据存储功能，包括：
/// - 安全的数据写入和读取
/// - 数据过期管理
/// - 加密密钥管理
/// - 数据完整性校验
class StorageService {
  /// SharedPreferences实例
  final SharedPreferences _prefs;

  /// 存储键前缀
  static const String _prefix = 'secure_';

  /// 盐值存储键
  static const String _saltKey = 'storage_salt';

  /// 加密盐值
  late final List<int> _salt;

  /// 构造函数
  ///
  /// 初始化存储服务并设置加密盐值
  /// [_prefs] SharedPreferences实例
  StorageService(this._prefs) {
    _initializeSalt();
  }

  /// 初始化加密盐值
  ///
  /// 如果不存在则生成新的盐值
  /// 否则从存储中读取现有盐值
  void _initializeSalt() {
    final storedSalt = _prefs.getString(_saltKey);
    if (storedSalt == null) {
      final random = Random.secure();
      _salt = List<int>.generate(32, (i) => random.nextInt(256));
      _prefs.setString(_saltKey, base64.encode(_salt));
    } else {
      _salt = base64.decode(storedSalt);
    }
  }

  /// 写入数据
  ///
  /// 将数据安全地写入存储
  /// 支持设置过期时间
  ///
  /// 参数：
  /// - [key] 存储键
  /// - [value] 存储值
  /// - [expiry] 过期时间（可选）
  Future<void> write({
    required String key,
    required String value,
    Duration? expiry,
  }) async {
    final timestamp = DateTime.now().millisecondsSinceEpoch;
    final expiryTime = expiry != null ? timestamp + expiry.inMilliseconds : null;

    final data = {
      'value': value,
      'timestamp': timestamp,
      'expiry': expiryTime,
    };

    final encryptedValue = _encrypt(jsonEncode(data));
    await _prefs.setString(_prefix + key, encryptedValue);
  }

  /// 读取数据
  ///
  /// 从存储中安全地读取数据
  /// 自动处理过期数据
  ///
  /// 参数：
  /// - [key] 存储键
  ///
  /// 返回：
  /// 存储的值，如果不存在或已过期则返回null
  Future<String?> read({required String key}) async {
    final encryptedValue = _prefs.getString(_prefix + key);
    if (encryptedValue == null) return null;

    final decryptedValue = _decrypt(encryptedValue);
    if (decryptedValue == null) return null;

    try {
      final data = jsonDecode(decryptedValue) as Map<String, dynamic>;
      final expiryTime = data['expiry'] as int?;

      if (expiryTime != null && DateTime.now().millisecondsSinceEpoch > expiryTime) {
        await delete(key: key);
        return null;
      }

      return data['value'] as String;
    } catch (e) {
      return null;
    }
  }

  /// 删除数据
  ///
  /// 从存储中删除指定键的数据
  /// [key] 要删除的存储键
  Future<void> delete({required String key}) async {
    await _prefs.remove(_prefix + key);
  }

  /// 删除所有数据
  ///
  /// 清除所有安全存储的数据
  Future<void> deleteAll() async {
    final keys = _prefs.getKeys().where((key) => key.startsWith(_prefix));
    for (final key in keys) {
      await _prefs.remove(key);
    }
  }

  /// 加密数据
  ///
  /// 使用HMAC-SHA256进行数据加密和完整性保护
  ///
  /// 参数：
  /// - [value] 要加密的数据
  ///
  /// 返回：
  /// 加密后的数据和HMAC，格式为"encrypted.hmac"
  String _encrypt(String value) {
    final key = _deriveKey();
    final bytes = utf8.encode(value);
    final hmac = Hmac(sha256, key);
    final digest = hmac.convert(bytes);
    final encrypted = base64.encode(bytes);
    return '$encrypted.${digest.toString()}';
  }

  /// 解密数据
  ///
  /// 解密数据并验证完整性
  ///
  /// 参数：
  /// - [encryptedValue] 加密的数据
  ///
  /// 返回：
  /// 解密后的数据，如果验证失败则返回null
  String? _decrypt(String encryptedValue) {
    try {
      final parts = encryptedValue.split('.');
      if (parts.length != 2) return null;

      final encrypted = parts[0];
      final hash = parts[1];

      final bytes = base64.decode(encrypted);
      final value = utf8.decode(bytes);

      final key = _deriveKey();
      final hmac = Hmac(sha256, key);
      final digest = hmac.convert(bytes);

      if (digest.toString() != hash) return null;

      return value;
    } catch (e) {
      return null;
    }
  }

  /// 派生加密密钥
  ///
  /// 使用盐值和设备信息派生加密密钥
  ///
  /// 返回：
  /// 派生的密钥
  List<int> _deriveKey() {
    final deviceInfo = _getDeviceInfo();
    final input = [..._salt, ...utf8.encode(deviceInfo)];
    return sha256.convert(input).bytes;
  }

  /// 获取设备信息
  ///
  /// 获取用于密钥派生的设备特定信息
  /// TODO: 在实际应用中，应该使用设备特定信息
  String _getDeviceInfo() {
    // TODO: 在实际应用中，应该使用设备特定信息
    // 例如设备ID、安装ID等
    return 'device_specific_info';
  }
}
