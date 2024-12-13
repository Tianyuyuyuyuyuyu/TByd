import 'dart:convert';
import 'dart:developer' as developer;
import 'package:http/http.dart' as http;

class NpmPackageService {
  final String baseUrl;
  final String? token;
  final http.Client _client;
  final Map<String, String> _readmeCache = {};

  NpmPackageService({
    required this.baseUrl,
    this.token,
    http.Client? client,
  }) : _client = client ?? http.Client();

  Map<String, String> get _headers => {
        'Accept': 'application/json',
        if (token != null) 'Authorization': 'Bearer $token',
      };

  /// 获取包的 README 内容
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

  bool _isValidReadme(String? readme) {
    return readme != null && readme.trim().isNotEmpty;
  }

  String _cacheAndReturn(String packageName, String readme) {
    _readmeCache[packageName] = readme;
    developer.log('Cached README for $packageName (length: ${readme.length})');
    return readme;
  }

  String _extractAuthor(dynamic author) {
    if (author == null) return '未知';
    if (author is String) return author;
    if (author is Map) return author['name']?.toString() ?? '未知';
    return '未知';
  }

  /// 清除指定包的 README 缓存
  void clearCache(String packageName) {
    _readmeCache.remove(packageName);
    developer.log('Cleared cache for package: $packageName');
  }

  /// 清除所有缓存
  void clearAllCache() {
    _readmeCache.clear();
    developer.log('Cleared all cache');
  }

  void dispose() {
    _readmeCache.clear();
    _client.close();
    developer.log('Disposed NpmPackageService');
  }
}
