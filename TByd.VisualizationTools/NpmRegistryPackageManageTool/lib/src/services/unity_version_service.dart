import 'dart:convert';
import 'dart:io';
import 'dart:async';
import 'package:http/http.dart' as http;
import 'dart:math' as math;
import '../models/unity_version.dart';

/// Unity 版本服务
class UnityVersionService {
  /// Unity 中国版本页面基础 URL
  static const String _baseUrl = 'https://unity.cn/releases';

  /// 最大重试次数
  static const int _maxRetries = 3;

  /// 超时时间
  static const Duration _timeout = Duration(seconds: 30);

  /// 调试输出目录
  static const String _debugOutputDir = 'debug_output';

  /// HTTP 客户端
  final http.Client _client = http.Client();

  /// 版本信息缓存
  List<UnityVersion>? _cachedVersions;

  /// 是否正在加载
  bool _isLoading = false;

  /// 确保调试输出目录存在
  Future<void> _ensureDebugOutputDir() async {
    final directory = Directory(_debugOutputDir);
    if (!await directory.exists()) {
      await directory.create(recursive: true);
    }
  }

  /// 保存调试文件
  Future<void> _saveDebugFile(String fileName, String content) async {
    if (const bool.fromEnvironment('dart.vm.product')) {
      return; // 在生产环境中不保存调试文件
    }
    await _ensureDebugOutputDir();
    final file = File('$_debugOutputDir/$fileName');
    await file.writeAsString(content);
  }

  /// 获取版本列表
  Future<List<UnityVersion>> fetchVersions() async {
    // 如果正在加载，等待加载完成
    if (_isLoading) {
      while (_isLoading) {
        await Future.delayed(const Duration(milliseconds: 100));
      }
      return _cachedVersions ?? [];
    }

    // 如果已有缓存，直接返回
    if (_cachedVersions != null) {
      return _cachedVersions!;
    }

    _isLoading = true;
    try {
      final List<UnityVersion> allVersions = [];
      final versionSeries = await _fetchVersionSeries();

      for (final series in versionSeries) {
        try {
          final versions = await _fetchVersionsFromPage(series);
          allVersions.addAll(versions);
        } catch (e) {
          continue;
        }
      }

      allVersions.sort((a, b) => _compareVersions(b.version, a.version));

      // 保存到缓存
      _cachedVersions = allVersions;
      return allVersions;
    } catch (e) {
      throw Exception('获取Unity版本信息失败: $e');
    } finally {
      _isLoading = false;
    }
  }

  /// 清除缓存
  void clearCache() {
    _cachedVersions = null;
  }

  /// 获取可用的版本系列
  Future<List<String>> _fetchVersionSeries() async {
    try {
      final response = await _client.get(
        Uri.parse(_baseUrl),
        headers: {
          'Accept': 'text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8',
          'Accept-Language': 'zh-CN,zh;q=0.9,en;q=0.8',
          'Cache-Control': 'no-cache',
          'Connection': 'keep-alive',
          'Pragma': 'no-cache',
          'User-Agent':
              'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36',
          'Sec-Fetch-Dest': 'document',
          'Sec-Fetch-Mode': 'navigate',
          'Sec-Fetch-Site': 'none',
          'Sec-Fetch-User': '?1',
          'Upgrade-Insecure-Requests': '1',
        },
      ).timeout(_timeout);

      if (response.statusCode != 200) {
        throw HttpException('HTTP ${response.statusCode}');
      }

      final content = response.body;
      await _saveDebugFile('unity_versions_page.html', content);

      final scriptPattern = RegExp(r'window\.__props__\s*=\s*({[\s\S]*?});[\s\n]*</script>', multiLine: true);
      final match = scriptPattern.firstMatch(content);

      if (match == null) {
        throw Exception('未找到版本数据');
      }

      final jsonStr = match.group(1)!;
      final Map<String, dynamic> data = json.decode(jsonStr);

      if (!data.containsKey('data')) {
        throw Exception('未找到 data 字段');
      }

      final mainData = data['data'] as Map<String, dynamic>;
      if (!mainData.containsKey('majors')) {
        throw Exception('未找到 majors 字段');
      }

      final majors = mainData['majors'] as List<dynamic>;
      final versions = majors.map((m) => m.toString()).where((version) {
        final versionNumber = int.tryParse(version);
        if (versionNumber != null) {
          return versionNumber > 5;
        }
        return true;
      }).toList();

      return versions;
    } catch (e) {
      return [
        '6000',
        '2023',
        '2022',
        '2021',
        '2020',
        '2019',
        '2018',
        '2017',
      ];
    }
  }

  /// 从页面获取版本信息
  Future<List<UnityVersion>> _fetchVersionsFromPage(String major) async {
    int retryCount = 0;
    Exception? lastException;

    while (retryCount < _maxRetries) {
      try {
        final url = '$_baseUrl/lts/$major';
        final response = await _client.get(
          Uri.parse(url),
          headers: {
            'Accept':
                'text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8',
            'Accept-Language': 'zh-CN,zh;q=0.9,en;q=0.8',
            'Cache-Control': 'no-cache',
            'Connection': 'keep-alive',
            'Pragma': 'no-cache',
            'User-Agent':
                'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36',
            'Sec-Fetch-Dest': 'document',
            'Sec-Fetch-Mode': 'navigate',
            'Sec-Fetch-Site': 'none',
            'Sec-Fetch-User': '?1',
            'Upgrade-Insecure-Requests': '1',
          },
        ).timeout(_timeout);

        if (response.statusCode == 404) {
          return [];
        }

        if (response.statusCode != 200) {
          throw HttpException('HTTP ${response.statusCode}');
        }

        final content = response.body;
        final debugFileName = 'unity_${major}_page.html';
        await _saveDebugFile(debugFileName, content);

        String? jsonStr;
        final scriptPattern = RegExp(r'window\.__props__\s*=\s*({[\s\S]*?});[\s\n]*</script>', multiLine: true);
        final match = scriptPattern.firstMatch(content);

        if (match != null) {
          jsonStr = match.group(1);
        } else {
          final allScriptsPattern = RegExp(r'<script[^>]*>([\s\S]*?)</script>', multiLine: true);
          final scripts = allScriptsPattern.allMatches(content);

          for (var scriptMatch in scripts) {
            final scriptContent = scriptMatch.group(1) ?? '';
            if (scriptContent.contains('window.__props__')) {
              final propsMatch = RegExp(r'window\.__props__\s*=\s*({[\s\S]*?});').firstMatch(scriptContent);
              if (propsMatch != null) {
                jsonStr = propsMatch.group(1);
                break;
              }
            }
          }
        }

        if (jsonStr == null) {
          throw Exception('未找到版本数据');
        }

        final Map<String, dynamic> data = json.decode(jsonStr);

        if (data.containsKey('data')) {
          final mainData = data['data'] as Map<String, dynamic>;

          if (mainData.containsKey('releases')) {
            final releases = mainData['releases'];

            if (releases is List) {
              final List<UnityVersion> versions = [];
              for (var release in releases) {
                try {
                  if (release is Map<String, dynamic>) {
                    final version = release['title'] as String?;
                    final releaseType = release['releaseType'] as String?;
                    final isLts = releaseType?.toLowerCase() == 'lts';
                    final downloadWin = release['downloadWin'] as Map<String, dynamic>?;
                    final downloadUrl = downloadWin?['unityEditor64'] as String?;

                    if (version != null && version.isNotEmpty && downloadUrl != null) {
                      versions.add(UnityVersion(
                        version: version,
                        lts: isLts,
                        versionType: releaseType?.toLowerCase() ?? 'official',
                        downloadUrl: {'unityhub': downloadUrl},
                      ));
                    }
                  }
                } catch (e) {
                  continue;
                }
              }

              if (versions.isNotEmpty) {
                return versions;
              }
            }
          }
        }

        throw Exception('未找到任何Unity $major 版本信息');
      } catch (e) {
        lastException = _handleException(e);
        retryCount++;
        if (retryCount < _maxRetries) {
          await Future.delayed(Duration(seconds: retryCount * 2));
        }
      }
    }

    throw lastException ?? Exception('获取版本信息失败');
  }

  /// 比较两个版本号
  int _compareVersions(String version1, String version2) {
    final v1Parts = version1.split('.');
    final v2Parts = version2.split('.');

    final v1Major = _extractNumber(v1Parts[0]);
    final v2Major = _extractNumber(v2Parts[0]);
    if (v1Major != v2Major) {
      return v1Major.compareTo(v2Major);
    }

    for (var i = 1; i < math.min(v1Parts.length, v2Parts.length); i++) {
      final v1Part = _extractNumber(v1Parts[i]);
      final v2Part = _extractNumber(v2Parts[i]);

      if (v1Part != v2Part) {
        return v1Part.compareTo(v2Part);
      }
    }

    return v1Parts.length.compareTo(v2Parts.length);
  }

  /// 从版本号部分提取数字
  int _extractNumber(String part) {
    final RegExp numberRegex = RegExp(r'(\d+)');
    final match = numberRegex.firstMatch(part);
    return match != null ? int.parse(match.group(1)!) : 0;
  }

  /// 处理异常
  Exception _handleException(dynamic e) {
    if (e is TimeoutException) {
      return Exception('请求超时，请检查网络连接');
    } else if (e is SocketException) {
      return Exception('网络连接失败，请检查网络设置');
    } else if (e is HttpException) {
      return Exception('HTTP 错误: ${e.message}');
    } else if (e is FormatException) {
      return Exception('数据格式错误');
    } else {
      return Exception('未知错误: $e');
    }
  }

  /// 释放资源
  void dispose() {
    _cachedVersions = null;
    _client.close();
  }
}
