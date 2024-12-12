import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/package_model.dart';
import '../utils/constants.dart';
import 'package:shared_preferences/shared_preferences.dart';

class PackageException implements Exception {
  final String message;
  PackageException(this.message);

  @override
  String toString() => message;
}

class PackageService {
  final http.Client _client;

  PackageService({http.Client? client}) : _client = client ?? http.Client();

  Future<String?> _getAuthToken() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString(StorageKeys.authToken);
  }

  Map<String, String> _getHeaders(String? token) {
    final headers = {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
    };
    if (token != null) {
      headers['Authorization'] = 'Basic $token';
    }
    return headers;
  }

  Future<List<PackageSearchResult>> searchPackages(String query) async {
    try {
      final token = await _getAuthToken();
      final lowercaseQuery = query.toLowerCase();

      // 使用标准的 npm 搜索接口获取所有包
      final response = await _client.get(
        Uri.parse('${ApiConstants.baseUrl}/-/v1/search?size=250'),
        headers: _getHeaders(token),
      );

      print('Search response: ${response.statusCode} - ${response.body}');

      if (response.statusCode == 200) {
        final data = json.decode(response.body);
        final objects = data['objects'] as List<dynamic>? ?? [];
        final List<PackageSearchResult> results = [];

        for (final obj in objects) {
          try {
            final package = obj['package'] as Map<String, dynamic>? ?? {};
            final packageName = package['name'] as String? ?? '';
            final author = package['author'] as Map<String, dynamic>?;
            final maintainers = package['maintainers'] as List<dynamic>? ?? [];

            // 检查包是否属于你的仓库（通过作者和维护者邮箱）
            final isYourPackage = (author?['email'] == 'tianyulovecars@gmail.com') ||
                maintainers.any((m) => m is Map<String, dynamic> && m['email'] == 'tianyulovecars@gmail.com');

            // 检查包名是否包含搜索词（不区分大小写）
            if (isYourPackage && packageName.toLowerCase().contains(lowercaseQuery)) {
              final distTags = package['dist-tags'] as Map<String, dynamic>? ?? {};
              final latestVersion = distTags['latest'] as String? ?? '0.0.0';

              results.add(PackageSearchResult(
                name: packageName,
                version: latestVersion,
                description: package['description'] as String? ?? '',
                author: _parseAuthor(author),
                lastModified: _parseDate(package['time']?['modified']),
                downloadCount: 0,
              ));
            }
          } catch (e) {
            print('Error parsing package data: $e');
          }
        }

        // 按相关性排序：完全匹配的排在前面，然后按字母顺序排序
        results.sort((a, b) {
          // 首先按包名长度排序（较短的包名在前）
          final lengthCompare = a.name.length.compareTo(b.name.length);
          if (lengthCompare != 0) {
            return lengthCompare;
          }
          // 长度相同时按字母顺序排序
          return a.name.compareTo(b.name);
        });

        return results;
      }

      throw PackageException('搜索包失败: ${response.body}');
    } catch (e) {
      print('Search error: $e');
      if (e is PackageException) rethrow;
      throw PackageException('网络错误: $e');
    }
  }

  String _parseAuthor(dynamic author) {
    if (author == null) return 'Unknown';
    if (author is String) return author;
    if (author is Map<String, dynamic>) {
      final name = author['name'] as String?;
      final email = author['email'] as String?;
      if (name != null && email != null) {
        return '$name <$email>';
      }
      return name ?? email ?? 'Unknown';
    }
    return 'Unknown';
  }

  DateTime _parseDate(dynamic date) {
    if (date == null) return DateTime.now();
    try {
      return DateTime.parse(date as String);
    } catch (e) {
      return DateTime.now();
    }
  }

  Future<Package> getPackageDetails(String packageName) async {
    try {
      final token = await _getAuthToken();
      final response = await _client.get(
        Uri.parse('${ApiConstants.baseUrl}/${packageName}'),
        headers: _getHeaders(token),
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body);
        return Package.fromJson(data);
      }

      throw PackageException('获取详情失败: ${response.body}');
    } catch (e) {
      if (e is PackageException) rethrow;
      throw PackageException('网络错误: $e');
    }
  }

  Future<List<PackageVersion>> getPackageVersions(String packageName) async {
    try {
      final token = await _getAuthToken();
      final response = await _client.get(
        Uri.parse('${ApiConstants.baseUrl}/${packageName}'),
        headers: _getHeaders(token),
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body);
        final versions = data['versions'] as Map<String, dynamic>;
        return versions.keys.map((version) => PackageVersion.fromJson(data, version)).toList()
          ..sort((a, b) => b.publishedAt.compareTo(a.publishedAt));
      }

      throw PackageException('获取包版本失败: ${response.body}');
    } catch (e) {
      if (e is PackageException) rethrow;
      throw PackageException('网络错误: $e');
    }
  }

  Future<void> unpublishPackage(String packageName, String version) async {
    try {
      final token = await _getAuthToken();
      if (token == null) {
        throw PackageException('需要登录');
      }

      final response = await _client.delete(
        Uri.parse('${ApiConstants.baseUrl}/${packageName}/-/${packageName}-${version}.tgz/-rev/whatever'),
        headers: _getHeaders(token),
      );

      if (response.statusCode != 200 && response.statusCode != 204) {
        throw PackageException('取消发布失败: ${response.body}');
      }
    } catch (e) {
      if (e is PackageException) rethrow;
      throw PackageException('网络错误: $e');
    }
  }

  Future<void> deprecatePackage(String packageName, String version, String message) async {
    try {
      final token = await _getAuthToken();
      if (token == null) {
        throw PackageException('需要登录');
      }

      final response = await _client.put(
        Uri.parse('${ApiConstants.baseUrl}/${packageName}'),
        headers: _getHeaders(token),
        body: json.encode({
          'versions': {
            version: {
              'deprecated': message,
            },
          },
        }),
      );

      if (response.statusCode != 200 && response.statusCode != 201) {
        throw PackageException('标记废弃失败: ${response.body}');
      }
    } catch (e) {
      if (e is PackageException) rethrow;
      throw PackageException('网络错误: $e');
    }
  }
}
