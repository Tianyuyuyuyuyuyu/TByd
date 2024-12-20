import 'dart:convert';
import 'dart:async';
import 'package:http/http.dart' as http;
import '../models/package_model.dart';
import '../utils/constants.dart';

/// NPM Registry Manager - 包管理服务类
///
/// 该文件负责与 Verdaccio NPM 仓库进行交互，提供包的搜索、详情获取、版本管理等核心功能
class PackageService {
  /// Verdaccio 服务器的基础 URL
  final String serverUrl;

  /// 用于身份验证的 JWT token
  final String? token;

  /// HTTP 客户端实例，用于发送网络请求
  /// 可以在测试时注入模拟客户端
  http.Client? _client;

  /// 构造函数
  /// [serverUrl] - Verdaccio 服务器地址
  /// [token] - 可选的身份验证令牌
  /// [client] - 可选的 HTTP 客户端实例，用于依赖注入和测试
  PackageService({
    required this.serverUrl,
    this.token,
    http.Client? client,
  }) : _client = client ?? http.Client();

  /// 获取或创建 HTTP 客户端实例
  /// 确保客户端实例的懒加载初始化
  http.Client get client {
    _client ??= http.Client();
    return _client!;
  }

  /// 搜索包
  /// [query] - 搜索关键词
  /// 返回匹配的包列表
  /// 抛出异常：当服务器无响应或请求失败时
  Future<List<PackageSearchResult>> searchPackages(String query) async {
    if (serverUrl.isEmpty) {
      print('错误：服务器URL未设置');
      return [];
    }

    final uri = Uri.parse('$serverUrl/-/verdaccio/data/packages');
    print('正在请求：$uri');

    try {
      final response = await client.get(uri, headers: _getHeaders()).timeout(const Duration(seconds: 30));
      print('服务器响应状态码：${response.statusCode}');

      if (response.statusCode == 200) {
        final data = json.decode(response.body);
        final results = <PackageSearchResult>[];

        if (data is List) {
          for (final pkg in data) {
            try {
              final result = _createSearchResult(pkg);
              results.add(result);
            } catch (e) {
              print('解析包数据失败：$e');
              continue;
            }
          }
        }

        print('成功获取到 ${results.length} 个包');
        return results;
      } else {
        print('服务器返回错误：${response.statusCode}');
        print('响应内容：${response.body}');
        return [];
      }
    } catch (e) {
      if (e is TimeoutException) {
        print('请求超时：$e');
      } else {
        print('请求失败：$e');
      }
      return [];
    }
  }

  /// 释放资源
  /// 关闭 HTTP 客户端连接
  void dispose() {
    _client?.close();
    _client = null;
  }

  /// 解析搜索结果
  /// [body] - API 响应体
  /// [searchText] - 搜索关键词
  /// 返回解析后的包搜索结果列表
  Future<List<PackageSearchResult>> _parseSearchResults(String body, String searchText) async {
    final data = json.decode(body);
    final results = <PackageSearchResult>[];

    try {
      if (data is Map<String, dynamic>) {
        // 处理直接的包列表格式
        for (final entry in data.entries) {
          if (entry.key.startsWith('_')) continue;
          try {
            final result = _createSearchResult({
              'name': entry.key,
              ...entry.value as Map<String, dynamic>,
            });
            if (_matchesPackageName(result.name, searchText)) {
              results.add(result);
            }
          } catch (e) {
            continue;
          }
        }
      }

      // 如果没有找到结果，尝试备用搜索
      if (results.isEmpty && searchText.isNotEmpty) {
        return _searchPackagesAlternative(searchText);
      }

      return _sortByRelevance(results, searchText);
    } catch (e) {
      throw Exception('解析搜索结果失败: $e');
    }
  }

  /// 从 API 响应创建搜索结果对象
  /// [data] - API 返回的原始数据
  /// 返回格式化的 PackageSearchResult 对象
  /// 抛出异常：当数据格式不符合预期时
  PackageSearchResult _createSearchResult(dynamic data) {
    if (data is String) {
      return PackageSearchResult(
        name: data,
        displayName: data,
        version: '0.0.0',
        description: '',
        author: 'Unknown',
        lastModified: DateTime.now(),
      );
    }

    if (data is Map<String, dynamic>) {
      String name = '';
      String displayName = '';
      String version = '0.0.0';
      String description = '';
      String author = 'Unknown';
      String? license;
      DateTime lastModified = DateTime.now();

      try {
        // 解析包名
        name = data['name']?.toString() ?? '';

        // 解析显示名称 - 从包的最新版本中获取
        if (data['versions'] is Map) {
          final versions = data['versions'] as Map;
          final latestVersion = data['dist-tags']?['latest']?.toString();
          if (latestVersion != null && versions[latestVersion] is Map) {
            final latestVersionData = versions[latestVersion] as Map;
            displayName = latestVersionData['displayName']?.toString() ??
                data['displayName']?.toString() ??
                latestVersionData['name']?.toString() ??
                name;
            // 从最新版本获取许可证信息
            license = latestVersionData['license']?.toString();
          } else {
            displayName = data['displayName']?.toString() ?? name;
          }
        } else {
          displayName = data['displayName']?.toString() ?? name;
        }

        // 如果最新版本没有许可证信息，尝试从包的根级别获取
        if (license == null || license.isEmpty) {
          license = data['license']?.toString();
        }

        // 解析版本号
        if (data['dist-tags'] is Map) {
          version = (data['dist-tags'] as Map)['latest']?.toString() ?? '0.0.0';
        } else if (data['version'] != null) {
          version = data['version'].toString();
        }

        // 解析描述
        description = data['description']?.toString() ?? '';

        // 解析作者信息
        final authorData = data['author'];
        if (authorData != null) {
          if (authorData is String) {
            author = authorData;
          } else if (authorData is Map) {
            author = authorData['name']?.toString() ?? 'Unknown';
          }
        }

        // 解析时间信息
        final timeData = data['time'];
        if (timeData != null) {
          if (timeData is Map) {
            final modified = timeData['modified'];
            if (modified != null) {
              try {
                lastModified = DateTime.parse(modified.toString());
              } catch (e) {
                // 忽略解析错误
              }
            }
          } else if (timeData is String) {
            try {
              lastModified = DateTime.parse(timeData);
            } catch (e) {
              // 忽略解析错误
            }
          }
        }
      } catch (e) {
        // 忽略字段解析错误
      }

      return PackageSearchResult(
        name: name,
        displayName: displayName,
        version: version,
        description: description,
        author: author,
        license: license,
        lastModified: lastModified,
      );
    }

    throw Exception('不支持的数据格式');
  }

  /// 备用搜索方法
  /// 当主搜索方法未返回结果时使用
  /// [query] - 搜索关键词
  Future<List<PackageSearchResult>> _searchPackagesAlternative(String query) async {
    final searchText = query.trim().toLowerCase();

    // 使用 v1 search API 并带上搜索文本
    final uri = Uri.parse('$serverUrl/-/v1/search').replace(
      queryParameters: {'text': searchText},
    );

    try {
      final response = await client
          .get(
            uri,
            headers: _getHeaders(),
          )
          .timeout(const Duration(seconds: 30));

      if (response.statusCode == 200) {
        final data = json.decode(response.body);
        final results = <PackageSearchResult>[];

        if (data is Map<String, dynamic> && data.containsKey('objects')) {
          final objects = data['objects'] as List;
          for (final obj in objects) {
            try {
              final result = _createSearchResult(obj['package']);
              results.add(result);
            } catch (e) {
              continue;
            }
          }
        }

        return _sortByRelevance(results, searchText);
      } else {
        throw Exception('备用搜索失败: ${response.statusCode}');
      }
    } catch (e) {
      if (e is TimeoutException) {
        throw Exception('备用搜索请求超时，请稍后重试');
      }
      throw Exception('备用搜索失败: $e');
    }
  }

  /// 检查包名是否匹配搜索文本
  /// [packageName] - 包名
  /// [searchText] - 搜索关键词
  bool _matchesPackageName(String packageName, String searchText) {
    if (searchText.isEmpty) {
      return true;
    }

    final name = packageName.toLowerCase();
    final search = searchText.toLowerCase();

    // 简单的包含匹配
    return name.contains(search);
  }

  /// 按相关性对搜索结果排序
  /// [results] - 待排序的结果列表
  /// [searchText] - 搜索关键词
  /// 返回排序后的结果（最多50个）
  List<PackageSearchResult> _sortByRelevance(List<PackageSearchResult> results, String searchText) {
    results.sort((a, b) {
      final aName = a.name.toLowerCase();
      final bName = b.name.toLowerCase();

      // 完全匹配优先
      if (aName == searchText && bName != searchText) return -1;
      if (bName == searchText && aName != searchText) return 1;

      // 前缀匹配次之
      if (aName.startsWith(searchText) && !bName.startsWith(searchText)) return -1;
      if (bName.startsWith(searchText) && !aName.startsWith(searchText)) return 1;

      // 最后按字母顺序排序
      return aName.compareTo(bName);
    });

    return results.take(50).toList();
  }

  /// 获取包的详细信息
  /// [packageName] - 包名
  /// 返回包的完整信息
  /// 抛出异常：当获取失败时
  Future<Package> getPackageDetails(String packageName) async {
    if (serverUrl.isEmpty) {
      throw Exception('服务器URL未设置');
    }

    final uri = Uri.parse('$serverUrl/$packageName');

    try {
      final response = await client.get(
        uri,
        headers: _getHeaders(),
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body) as Map<String, dynamic>;

        // 从最新版本中获取 displayName
        String? displayName;
        if (data['versions'] is Map) {
          final versions = data['versions'] as Map;
          final latestVersion = data['dist-tags']?['latest']?.toString();
          if (latestVersion != null && versions[latestVersion] is Map) {
            final latestVersionData = versions[latestVersion] as Map;
            displayName = latestVersionData['displayName']?.toString();
          }
        }

        // 将 displayName 添加到数据中
        if (displayName != null) {
          data['displayName'] = displayName;
        }

        return Package.fromJson(data);
      } else {
        throw Exception('获取包详情失败: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('获取包详情失败: $e');
    }
  }

  /// 获取包的版本历史
  /// [packageName] - 包名
  /// 返回按时间倒序排列的版本列表
  Future<List<PackageVersion>> getPackageVersions(String packageName) async {
    if (serverUrl.isEmpty) {
      throw Exception('服务器URL未设置');
    }

    final uri = Uri.parse('$serverUrl/$packageName');

    try {
      final response = await client.get(
        uri,
        headers: _getHeaders(),
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body) as Map<String, dynamic>;
        final versions = data['versions'] as Map<String, dynamic>;
        return versions.keys.map((version) => PackageVersion.fromJson(data, version)).toList()
          ..sort((a, b) => b.publishedAt.compareTo(a.publishedAt));
      } else {
        throw Exception('获取包版本失败: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('获取包版本失败: $e');
    }
  }

  /// 下架指定版本的包
  /// [packageName] - 包名
  /// [version] - 版本号
  /// 抛出异常：当操作失败或未授权时
  Future<void> unpublishPackage(String packageName, String version) async {
    if (serverUrl.isEmpty) {
      throw Exception('服务器URL未设置');
    }

    if (token == null) {
      throw Exception('需要认证令牌');
    }

    final uri = Uri.parse('$serverUrl${ApiConstants.unpublish}').replace(
      queryParameters: {
        'package': packageName,
        'version': version,
      },
    );

    try {
      final response = await client.delete(
        uri,
        headers: _getHeaders(),
      );

      if (response.statusCode != 200 && response.statusCode != 204) {
        throw Exception('下架包失败: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('下架包失败: $e');
    }
  }

  /// 将包标记为已弃用
  /// [packageName] - 包名
  /// [version] - 版本号
  /// [message] - 弃用说明
  Future<void> deprecatePackage(String packageName, String version, String message) async {
    if (serverUrl.isEmpty) {
      throw Exception('服务器URL未设置');
    }

    if (token == null) {
      throw Exception('需要认证令牌');
    }

    final uri = Uri.parse('$serverUrl${ApiConstants.deprecate}');

    try {
      final response = await client.put(
        uri,
        headers: _getHeaders(),
        body: json.encode({
          'package': packageName,
          'version': version,
          'message': message,
        }),
      );

      if (response.statusCode != 200 && response.statusCode != 201) {
        throw Exception('标记包为已弃用失败: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('标记包为已弃用失败: $e');
    }
  }

  /// 构造请求头
  /// 添加认证信息和内容类型
  /// 返回包含必要头信息的 Map
  Map<String, String> _getHeaders() {
    final headers = <String, String>{
      'Accept': 'application/json',
      'Content-Type': 'application/json',
    };

    if (token != null && token!.isNotEmpty) {
      headers['Authorization'] = 'Bearer $token';
    }

    return headers;
  }

  /// 获取包的原始清单数据
  /// [packageName] - 包名
  /// 返回格式化的 JSON 字符串
  Future<String> getRawManifest(String packageName) async {
    if (serverUrl.isEmpty) {
      throw Exception('服务器URL未设置');
    }

    final uri = Uri.parse('$serverUrl/$packageName');

    try {
      final response = await client.get(uri, headers: _getHeaders());

      if (response.statusCode == 200) {
        final dynamic jsonData = json.decode(response.body);

        if (jsonData is Map) {
          jsonData.remove('readme');

          final versions = jsonData['versions'];
          if (versions is Map) {
            for (final version in versions.values) {
              if (version is Map) {
                version.remove('readme');

                if (version['installation'] != null) {
                  version['INSTALLATION'] = version.remove('installation');
                }
              }
            }
          }

          if (jsonData['installation'] != null) {
            jsonData['INSTALLATION'] = jsonData.remove('installation');
          }
        }

        return const JsonEncoder.withIndent('  ').convert(jsonData);
      } else {
        throw Exception('获取原始清单失败: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('获取原始清单失败: $e');
    }
  }
}
