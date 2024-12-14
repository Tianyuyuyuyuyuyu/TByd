/// NPM Registry Manager - 加密服务
///
/// 该文件提供应用程序的数据加密功能，包括：
/// - AES加密解密
/// - 密钥管理
/// - 密码哈希
/// - 安全存储
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'dart:convert';
import 'package:crypto/crypto.dart';
import 'package:encrypt/encrypt.dart';
import '../services/storage_service.dart';

/// 加密服务类
///
/// 提供应用程序的数据加密和解密功能，包括：
/// - 密钥生成和管理
/// - 数据加密解密
/// - 密码哈希处理
class EncryptionService {
  /// 密钥存储键名
  static const String _keyKey = 'encryption_key';

  /// 初始化向量存储键名
  static const String _ivKey = 'encryption_iv';

  /// 存储服务实例
  final StorageService _storage;

  /// AES加密密钥
  late final Key _key;

  /// AES初始化向量
  late final IV _iv;

  /// 初始化状态标志
  bool _isInitialized = false;

  /// 构造函数
  ///
  /// [_storage] 存储服务实例，用于密钥的持久化存储
  EncryptionService(this._storage);

  /// 初始化服务
  ///
  /// 确保加密服务正确初始化，包括：
  /// - 加载或生成密钥
  /// - 设置初始化向量
  Future<void> init() async {
    if (_isInitialized) return;
    await _initialize();
  }

  /// 内部初始化方法
  ///
  /// 执行实际的初始化操作：
  /// - 从存储中读取现有密钥
  /// - 必要时生成新密钥
  /// - 设置服务状态
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

  /// 生成新密钥
  ///
  /// 生成新的加密密钥和初始化向量：
  /// - 使用安全随机数生成器
  /// - 将密钥保存到存储
  /// - 设置过期时间
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

  /// 加密数据
  ///
  /// 使用AES算法加密字符串数据
  ///
  /// 参数：
  /// - [data] 要加密的字符串
  ///
  /// 返回：
  /// Base64编码的加密数据
  Future<String> encrypt(String data) async {
    await _initialize();
    final encrypter = Encrypter(AES(_key));
    final encrypted = encrypter.encrypt(data, iv: _iv);
    return encrypted.base64;
  }

  /// 解密数据
  ///
  /// 解密之前加密的数据
  /// 如果解密失败会尝试重新生成密钥
  ///
  /// 参数：
  /// - [encryptedData] Base64编码的加密数据
  ///
  /// 返回：
  /// 解密后的原始字符串
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

  /// 密码哈希
  ///
  /// 使用SHA-256算法对密码进行单向哈希
  ///
  /// 参数：
  /// - [password] 原始密码
  ///
  /// 返回：
  /// 密码的哈希值
  String hashPassword(String password) {
    final bytes = utf8.encode(password);
    final digest = sha256.convert(bytes);
    return digest.toString();
  }

  /// 清除密钥
  ///
  /// 从存储中删除所有加密密钥
  /// 重置服务状态
  Future<void> clearKeys() async {
    await _storage.delete(key: _keyKey);
    await _storage.delete(key: _ivKey);
    _isInitialized = false;
  }
}
