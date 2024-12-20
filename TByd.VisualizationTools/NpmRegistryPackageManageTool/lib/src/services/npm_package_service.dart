/// NPM Registry Manager - NPM包服务
///
/// 该文件提供NPM包相关的服务功能，包括：
/// - 包信息获取
/// - README文档管理
/// - 缓存控制
/// - 错误处理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'dart:convert';
import 'dart:developer' as developer;
import 'package:http/http.dart' as http;

/// NPM包服务类
///
/// 提供NPM包相关的操作功能，包括：
/// - 获取包信息
/// - 管理README文档
/// - 处理认证
/// - 缓存控制
class NpmPackageService {
  /// NPM仓库基础URL
  final String baseUrl;

  /// 认证令牌
  final String? token;

  /// HTTP客户端
  final http.Client _client;

  /// README缓存
  final Map<String, String> _readmeCache = {};

  /// 构造函数
  ///
  /// 初始化NPM包服务
  ///
  /// 参数：
  /// - [baseUrl] NPM仓库的基础URL
  /// - [token] 认证令牌（可选）
  /// - [client] HTTP客户端（可选）
  NpmPackageService({
    required this.baseUrl,
    this.token,
    http.Client? client,
  }) : _client = client ?? http.Client();

  /// 获取HTTP请求头
  ///
  /// 根据是否有认证令牌返回适当的请求头
  Map<String, String> get _headers => {
        'Accept': 'application/json',
        if (token != null) 'Authorization': 'Bearer $token',
      };

  /// 获取包的README内容
  ///
  /// 从NPM仓库获取指定包的README文档
  /// 实现了多级缓存和回退机制
  ///
  /// 参数：
  /// - [packageName] 包名
  ///
  /// 返回：
  /// 包的README内容
  ///
  /// 异常：
  /// - ArgumentError 如果包名为空
  /// - Exception 如果获取失败
  Future<String> fetchReadme(String packageName) async {
    if (packageName.isEmpty) {
      throw ArgumentError('Package name cannot be empty');
    }

    try {
      // 检查缓存
      if (_readmeCache.containsKey(packageName)) {
        developer.log('Returning cached README for $packageName');
        return _readmeCache[packageName]!;
      }

      developer.log('Fetching README for package: $packageName');
      developer.log('Using base URL: $baseUrl');

      // 获取包的 Raw Manifest
      final response = await _client.get(
        Uri.parse('$baseUrl/$packageName'),
        headers: _headers,
      );

      developer.log('API Response Status: ${response.statusCode}');
      developer.log('API URL: ${response.request?.url}');

      if (response.statusCode == 401 || response.statusCode == 403) {
        throw Exception('未授权访问。请检查您的认证信息。');
      }

      if (response.statusCode == 404) {
        throw Exception('找不到包 "$packageName"。');
      }

      if (response.statusCode != 200) {
        throw Exception('服务器返回错误: ${response.statusCode}');
      }

      Map<String, dynamic> data;
      try {
        data = json.decode(response.body) as Map<String, dynamic>;
      } catch (e) {
        throw Exception('无法解析服务器响应: $e');
      }

      developer.log('Response Data Keys: ${data.keys.toList()}');

      // 1. 首先尝试从 manifest 的根级别获取 readme
      String? readme = data['readme'] as String?;
      if (_isValidReadme(readme)) {
        developer.log('Found readme in manifest root');
        return _cacheAndReturn(packageName, readme!);
      }

      // 2. 尝试从最新版本获取
      final latestVersion = data['dist-tags']?['latest'] as String?;
      if (latestVersion != null) {
        final versions = data['versions'] as Map<String, dynamic>?;
        final latestData = versions?[latestVersion] as Map<String, dynamic>?;

        readme = latestData?['readme'] as String?;
        if (_isValidReadme(readme)) {
          developer.log('Found readme in latest version');
          return _cacheAndReturn(packageName, readme!);
        }
      }

      // 3. 尝试从 raw 数据获取
      final raw = data['raw'] as Map<String, dynamic>?;
      readme = raw?['readme'] as String?;
      if (_isValidReadme(readme)) {
        developer.log('Found readme in raw data');
        return _cacheAndReturn(packageName, readme!);
      }

      // 4. 如果都没有找到，返回默认消息
      developer.log('No README found in package');
      final defaultReadme = '''
# $packageName

该包暂无 README 文件。

## 基本信息

- **版本**: ${latestVersion ?? '未知'}
- **描述**: ${data['description'] ?? '暂无描述'}
- **作者**: ${_extractAuthor(data['author'])}
- **许可证**: ${data['license'] ?? '未指定'}
''';
      return _cacheAndReturn(packageName, defaultReadme);
    } catch (e, stackTrace) {
      developer.log('Error fetching README', error: e, stackTrace: stackTrace);
      rethrow;
    }
  }

  /// 验证README内容
  ///
  /// 检查README内容是否有效
  /// [readme] README内容
  bool _isValidReadme(String? readme) {
    return readme != null && readme.trim().isNotEmpty;
  }

  /// 缓存并返回README
  ///
  /// 将README内容保存到缓存并返回
  ///
  /// 参数：
  /// - [packageName] 包名
  /// - [readme] README内容
  String _cacheAndReturn(String packageName, String readme) {
    _readmeCache[packageName] = readme;
    developer.log('Cached README for $packageName (length: ${readme.length})');
    return readme;
  }

  /// 提取作者信息
  ///
  /// 从不同格式的作者数据中提取作者名称
  ///
  /// 参数：
  /// - [author] 作者数据
  String _extractAuthor(dynamic author) {
    if (author == null) return '未知';
    if (author is String) return author;
    if (author is Map) return author['name']?.toString() ?? '未知';
    return '未知';
  }

  /// 清除指定包的缓存
  ///
  /// 从缓存中移除指定包的README
  /// [packageName] 要清除缓存的包名
  void clearCache(String packageName) {
    _readmeCache.remove(packageName);
    developer.log('Cleared cache for package: $packageName');
  }

  /// 清除所有缓存
  ///
  /// 清空README缓存
  void clearAllCache() {
    _readmeCache.clear();
    developer.log('Cleared all cache');
  }

  /// 释放资源
  ///
  /// 清理缓存并关闭HTTP客户端
  void dispose() {
    _readmeCache.clear();
    _client.close();
    developer.log('Disposed NpmPackageService');
  }

  /// 获取包信息
  ///
  /// 从NPM仓库获取指定包的信息
  ///
  /// 参数：
  /// - [packageName] 包名
  ///
  /// 返回：
  /// 包含包信息的Map，如果获取失���则返回null
  Future<Map<String, dynamic>?> fetchPackageInfo(String packageName) async {
    if (packageName.isEmpty) {
      throw ArgumentError('Package name cannot be empty');
    }

    try {
      developer.log('Fetching package info for: $packageName');
      developer.log('Using base URL: $baseUrl');

      final response = await _client.get(
        Uri.parse('$baseUrl/$packageName'),
        headers: _headers,
      );

      developer.log('API Response Status: ${response.statusCode}');

      if (response.statusCode == 401 || response.statusCode == 403) {
        throw Exception('未授权访问。请检查您的认证信息。');
      }

      if (response.statusCode == 404) {
        return null;
      }

      if (response.statusCode != 200) {
        throw Exception('服务器返回错误: ${response.statusCode}');
      }

      try {
        return json.decode(response.body) as Map<String, dynamic>;
      } catch (e) {
        throw Exception('无法解析服务器响应: $e');
      }
    } catch (e, stackTrace) {
      developer.log('Error fetching package info', error: e, stackTrace: stackTrace);
      rethrow;
    }
  }
}
