/// NPM Registry Manager - NPM包服务管理
///
/// 该文件负责提供NPM包相关的服务和状态管理，包括：
/// - NPM包信息获取
/// - 包文档访问
/// - 包依赖管理
/// - 版本控制
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/npm_package_service.dart';
import '../providers/auth_provider.dart';

/// NPM包服务提供者
///
/// 提供 [NpmPackageService] 实例，用于处理NPM包相关的操作
/// 依赖于认证状态获取服务器URL和认证令牌
///
/// 主要功能：
/// - 包信息查询
/// - 包版本管理
/// - 依赖分析
/// - 包发布和更新
final npmPackageServiceProvider = Provider<NpmPackageService>((ref) {
  final authState = ref.watch(authProvider);
  final serverUrl = authState.auth?.serverUrl;
  final authToken = authState.auth?.token;

  if (serverUrl == null || serverUrl.isEmpty) {
    throw Exception('Server URL is not configured');
  }

  final service = NpmPackageService(
    baseUrl: serverUrl,
    token: authToken,
  );

  // 确保在提供者销毁时释放资源
  ref.onDispose(() {
    service.dispose();
  });

  return service;
});

/// README文档提供者
///
/// 基于包名获取对应的README文档内容
/// 使用 [FutureProvider.family] 支持参数化的异步数据获取
///
/// 参数：
/// - [packageName] 包名，用于获取特定包的README文档
///
/// 返回：
/// - 包的README文档内容
/// - 如果包名为空，返回空字符串
/// - 如果获取失败，抛出异常
final readmeProvider = FutureProvider.family<String, String>((ref, packageName) async {
  if (packageName.isEmpty) {
    return '';
  }
  final service = ref.watch(npmPackageServiceProvider);
  return service.fetchReadme(packageName);
});
