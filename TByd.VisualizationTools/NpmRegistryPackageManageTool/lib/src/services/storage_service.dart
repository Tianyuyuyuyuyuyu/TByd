import 'dart:convert';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:crypto/crypto.dart';
import 'dart:math';

class StorageService {
  final SharedPreferences _prefs;
  static const String _prefix = 'secure_';
  static const String _saltKey = 'storage_salt';
  late final List<int> _salt;

  StorageService(this._prefs) {
    _initializeSalt();
  }

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

  Future<void> delete({required String key}) async {
    await _prefs.remove(_prefix + key);
  }

  Future<void> deleteAll() async {
    final keys = _prefs.getKeys().where((key) => key.startsWith(_prefix));
    for (final key in keys) {
      await _prefs.remove(key);
    }
  }

  String _encrypt(String value) {
    final key = _deriveKey();
    final bytes = utf8.encode(value);
    final hmac = Hmac(sha256, key);
    final digest = hmac.convert(bytes);
    final encrypted = base64.encode(bytes);
    return '$encrypted.${digest.toString()}';
  }

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

  List<int> _deriveKey() {
    final deviceInfo = _getDeviceInfo();
    final input = [..._salt, ...utf8.encode(deviceInfo)];
    return sha256.convert(input).bytes;
  }

  String _getDeviceInfo() {
    // TODO: 在实际应用中，应该使用设备特定信息
    // 例如设备ID、安装ID等
    return 'device_specific_info';
  }
}
