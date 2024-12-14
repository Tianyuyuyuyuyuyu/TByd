import 'dart:convert';
import 'dart:async';
import 'package:http/http.dart' as http;
import '../models/package_model.dart';
import '../utils/constants.dart';

class PackageService {
  final String serverUrl;
  final String? token;
  http.Client? _client;

  PackageService({
    required this.serverUrl,
    this.token,
    http.Client? client,
  }) : _client = client ?? http.Client();

  http.Client get client {
    _client ??= http.Client();
    return _client!;
  }

  Future<List<PackageSearchResult>> searchPackages(String query) async {
    if (serverUrl.isEmpty) {
      throw Exception('Server URL is not set');
    }

    final uri = Uri.parse('$serverUrl/-/verdaccio/data/packages');

    try {
      print('发送请求到: ${uri.toString()}');
      final response = await client.get(uri, headers: _getHeaders()).timeout(const Duration(seconds: 30));
      print('响应状态码: ${response.statusCode}');

      if (response.statusCode == 200) {
        final data = json.decode(response.body);
        print('响应数据类型: ${data.runtimeType}');
        final results = <PackageSearchResult>[];

        if (data is List) {
          print('包总数: ${data.length}');
          for (final pkg in data) {
            try {
              final result = _createSearchResult(pkg);
              results.add(result);
            } catch (e) {
              print('解析包数据时出错: $e');
              continue;
            }
          }
        }

        print('成功解析包数量: ${results.length}');
        return results;
      } else {
        print('请求失败: ${response.body}');
        throw Exception('获取包列表失败: ${response.statusCode}');
      }
    } catch (e) {
      print('请求出错: $e');
      if (e is TimeoutException) {
        throw Exception('请求超时，请稍后重试');
      }
      throw Exception('获取包列表失败: $e');
    }
  }

  void dispose() {
    _client?.close();
    _client = null;
  }

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
            print('解析包数据时出错: $e');
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
      print('解析搜索结果时出错: $e');
      throw Exception('解析搜索结果失败: $e');
    }
  }

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
      DateTime lastModified = DateTime.now();

      try {
        // 解析包名
        name = data['name']?.toString() ?? '';

        // 解析显示名称 - 从包的最新版本中获
        if (data['versions'] is Map) {
          final versions = data['versions'] as Map;
          final latestVersion = data['dist-tags']?['latest']?.toString();
          if (latestVersion != null && versions[latestVersion] is Map) {
            final latestVersionData = versions[latestVersion] as Map;
            displayName = latestVersionData['displayName']?.toString() ?? name;
          } else {
            displayName = name;
          }
        } else {
          displayName = name;
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
                print('解析时间戳出错: $e');
              }
            }
          } else if (timeData is String) {
            try {
              lastModified = DateTime.parse(timeData);
            } catch (e) {
              print('解析时间戳出错: $e');
            }
          }
        }
      } catch (e) {
        print('解析字段时出错: $e');
      }

      return PackageSearchResult(
        name: name,
        displayName: displayName,
        version: version,
        description: description,
        author: author,
        lastModified: lastModified,
      );
    }

    throw Exception('不支持的数据格式');
  }

  Future<List<PackageSearchResult>> _searchPackagesAlternative(String query) async {
    print('使用备用搜索方法');
    final searchText = query.trim().toLowerCase();

    // 使用 v1 search API 并带上搜索文本
    final uri = Uri.parse('$serverUrl/-/v1/search').replace(
      queryParameters: {'text': searchText},
    );

    try {
      print('发送备用搜索请求到: ${uri.toString()}');
      final response = await client
          .get(
        uri,
        headers: _getHeaders(),
      )
          .timeout(
        const Duration(seconds: 30),
        onTimeout: () {
          throw Exception('搜索请求超时，请稍后重试');
        },
      );

      print('备用搜索响应状态码: ${response.statusCode}');

      if (response.statusCode == 200) {
        final data = json.decode(response.body);
        final results = <PackageSearchResult>[];

        if (data is List) {
          for (final pkg in data) {
            try {
              final result = _createSearchResult(pkg);
              if (_matchesPackageName(result.name, searchText)) {
                results.add(result);
              }
            } catch (e) {
              print('解析包数据时出错: $e');
              continue;
            }
          }
        } else if (data is Map) {
          for (final entry in data.entries) {
            if (entry.key.startsWith('_')) continue;
            try {
              final result = _createSearchResult(entry.value);
              if (_matchesPackageName(result.name, searchText)) {
                results.add(result);
              }
            } catch (e) {
              print('解析包数据时出错: $e');
              continue;
            }
          }
        }

        return _sortByRelevance(results, searchText);
      } else {
        throw Exception('搜索包失败: ${response.statusCode}');
      }
    } catch (e) {
      print('备用搜索请求出错: $e');
      if (e.toString().contains('timeout')) {
        throw Exception('搜索请求超时，请稍后重试');
      }
      throw Exception('搜索包失败: $e');
    }
  }

  bool _matchesPackageName(String packageName, String searchText) {
    if (searchText.isEmpty) {
      return true;
    }

    final name = packageName.toLowerCase();
    final search = searchText.toLowerCase();

    // 简单的包含匹配
    return name.contains(search);
  }

  List<PackageSearchResult> _sortByRelevance(List<PackageSearchResult> results, String searchText) {
    results.sort((a, b) {
      final aName = a.name.toLowerCase(); // 使用 name 而不是 displayName 进行排序
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

  Future<Package> getPackageDetails(String packageName) async {
    if (serverUrl.isEmpty) {
      throw Exception('Server URL is not set');
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
        throw Exception('Failed to get package details: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Failed to get package details: $e');
    }
  }

  Future<List<PackageVersion>> getPackageVersions(String packageName) async {
    if (serverUrl.isEmpty) {
      throw Exception('Server URL is not set');
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
        throw Exception('Failed to get package versions: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Failed to get package versions: $e');
    }
  }

  Future<void> unpublishPackage(String packageName, String version) async {
    if (serverUrl.isEmpty) {
      throw Exception('Server URL is not set');
    }

    if (token == null) {
      throw Exception('Authentication token is required');
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
        throw Exception('Failed to unpublish package: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Failed to unpublish package: $e');
    }
  }

  Future<void> deprecatePackage(String packageName, String version, String message) async {
    if (serverUrl.isEmpty) {
      throw Exception('Server URL is not set');
    }

    if (token == null) {
      throw Exception('Authentication token is required');
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
        throw Exception('Failed to deprecate package: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Failed to deprecate package: $e');
    }
  }

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

  Future<String> getRawManifest(String packageName) async {
    if (serverUrl.isEmpty) {
      throw Exception('Server URL is not set');
    }

    final uri = Uri.parse('$serverUrl/$packageName');

    try {
      final response = await client.get(uri, headers: _getHeaders());

      if (response.statusCode == 200) {
        // 解析 JSON 数据
        final dynamic jsonData = json.decode(response.body);

        // 移除顶层的 readme 字段
        if (jsonData is Map) {
          jsonData.remove('readme');

          // 移除版本中的 readme 字段
          final versions = jsonData['versions'];
          if (versions is Map) {
            for (final version in versions.values) {
              if (version is Map) {
                version.remove('readme');

                // 将 Installation 转换为大写
                if (version['installation'] != null) {
                  version['INSTALLATION'] = version.remove('installation');
                }
              }
            }
          }

          // 处理顶层的 Installation
          if (jsonData['installation'] != null) {
            jsonData['INSTALLATION'] = jsonData.remove('installation');
          }
        }

        // 格式化 JSON 以便于阅读
        return const JsonEncoder.withIndent('  ').convert(jsonData);
      } else {
        throw Exception('Failed to get raw manifest: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Failed to get raw manifest: $e');
    }
  }
}
