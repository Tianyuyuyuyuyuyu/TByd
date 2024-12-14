/// NPM Registry Manager - 存储服务管理
///
/// 该文件负责提供应用程序的数据存储服务，包括：
/// - 本地数据持久化
/// - 配置信息存储
/// - 缓存数据管理
/// - 安全数据存储
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/storage_service.dart';
import 'locale_provider.dart';

/// 存储服务提供者
///
/// 提供 [StorageService] 实例，用于处理应用程序的数据存储需求
/// 依赖于 [SharedPreferences] 进行数据的持久化存储
///
/// 主要功能：
/// - 配置数据的读写
/// - 用户偏好设置的存储
/// - 缓存数据的管理
/// - 敏感数据的安全存储
/// - 临时数据的管理
final storageServiceProvider = Provider<StorageService>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  return StorageService(prefs);
});
