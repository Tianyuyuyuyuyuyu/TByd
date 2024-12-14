/// NPM Registry Manager - 加密服务管理
///
/// 该文件负责提供应用程序的加密服务，包括：
/// - 敏感数据加密
/// - 密码加密存储
/// - 安全令牌管理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/encryption_service.dart';
import 'storage_provider.dart';

/// 加密服务提供者
///
/// 提供 [EncryptionService] 实例，用于处理应用程序的加密需求
/// 依赖于 [StorageService] 进行加密密钥和数据的存储
///
/// 主要功能：
/// - 数据加密和解密
/// - 密码哈希处理
/// - 安全令牌加密存储
/// - 加密密钥管理
final encryptionServiceProvider = Provider<EncryptionService>((ref) {
  final storage = ref.watch(storageServiceProvider);
  return EncryptionService(storage);
});
