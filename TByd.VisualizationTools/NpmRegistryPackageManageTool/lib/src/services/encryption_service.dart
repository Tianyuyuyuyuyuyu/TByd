import 'dart:convert';
import 'dart:typed_data';
import 'package:crypto/crypto.dart';
import 'package:encrypt/encrypt.dart';
import '../services/storage_service.dart';

class EncryptionService {
  static const String _keyKey = 'encryption_key';
  static const String _ivKey = 'encryption_iv';
  final StorageService _storage;

  late final Key _key;
  late final IV _iv;
  bool _isInitialized = false;

  EncryptionService(this._storage);

  Future<void> init() async {
    if (_isInitialized) return;
    await _initialize();
  }

  Future<void> _initialize() async {
    if (_isInitialized) return;

    // 尝试从存储中获取密钥，使用1天的过期时间
    String? storedKey = await _storage.read(key: _keyKey);
    String? storedIv = await _storage.read(key: _ivKey);

    if (storedKey == null || storedIv == null) {
      // 生成新的密钥
      await _generateNewKeys();
    } else {
      // 使用存储的密钥
      _key = Key(base64.decode(storedKey));
      _iv = IV(base64.decode(storedIv));
    }

    _isInitialized = true;
  }

  Future<void> _generateNewKeys() async {
    // 生成随机密钥
    final key = Key.fromSecureRandom(32);
    final iv = IV.fromSecureRandom(16);

    // 保存到存储，设置1天过期时间
    await _storage.write(
      key: _keyKey,
      value: base64.encode(key.bytes),
      expiry: const Duration(days: 1),
    );
    await _storage.write(
      key: _ivKey,
      value: base64.encode(iv.bytes),
      expiry: const Duration(days: 1),
    );

    _key = key;
    _iv = iv;
  }

  Future<String> encrypt(String data) async {
    await _initialize();
    final encrypter = Encrypter(AES(_key));
    final encrypted = encrypter.encrypt(data, iv: _iv);
    return encrypted.base64;
  }

  Future<String> decrypt(String encryptedData) async {
    await _initialize();
    try {
      final encrypter = Encrypter(AES(_key));
      final encrypted = Encrypted.fromBase64(encryptedData);
      return encrypter.decrypt(encrypted, iv: _iv);
    } catch (e) {
      // 如果解密失败，可能是密钥过期，尝试重新生成密钥
      await _generateNewKeys();
      final encrypter = Encrypter(AES(_key));
      final encrypted = Encrypted.fromBase64(encryptedData);
      return encrypter.decrypt(encrypted, iv: _iv);
    }
  }

  String hashPassword(String password) {
    final bytes = utf8.encode(password);
    final digest = sha256.convert(bytes);
    return digest.toString();
  }

  Future<void> clearKeys() async {
    await _storage.delete(key: _keyKey);
    await _storage.delete(key: _ivKey);
    _isInitialized = false;
  }
}
