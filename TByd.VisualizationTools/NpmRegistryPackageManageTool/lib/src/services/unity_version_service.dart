import 'dart:convert';
import 'dart:io';
import 'dart:async';
import 'package:http/http.dart' as http;
import '../models/unity_version.dart';

/// Unity 版本服务
class UnityVersionService {
  /// API 地址
  static const String _apiUrl = 'https://public-cdn.cloud.unity3d.com/hub/prod/releases.json';

  /// 最大重试次数
  static const int _maxRetries = 3;

  /// 超时时间
  static const Duration _timeout = Duration(seconds: 30);

  /// HTTP 客户端
  final http.Client _client = http.Client();

  /// 获取版本列表
  Future<List<UnityVersion>> fetchVersions() async {
    int retryCount = 0;
    Exception? lastException;

    while (retryCount < _maxRetries) {
      try {
        final request = http.Request('GET', Uri.parse(_apiUrl))
          ..headers.addAll({
            'Accept': 'application/json',
            'User-Agent':
                'Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36',
          });

        final response = await _client.send(request).timeout(_timeout);
        final responseBody = await response.stream.bytesToString();

        if (response.statusCode == 200) {
          final Map<String, dynamic> data = json.decode(responseBody);

          final List<dynamic> officialVersions = data['official'] ?? [];
          final List<dynamic> betaVersions = data['beta'] ?? [];

          final List<UnityVersion> allVersions = [
            ...officialVersions.map((v) => UnityVersion.fromJson(v)),
            ...betaVersions.map((v) => UnityVersion.fromJson(v)),
          ];

          return allVersions;
        } else {
          throw HttpException('HTTP ${response.statusCode}');
        }
      } on TimeoutException {
        lastException = Exception('请求超时，请检查网络连接');
      } on SocketException {
        lastException = Exception('网络连接失败，请检查网络设置');
      } on HttpException catch (e) {
        lastException = Exception('HTTP 错误: ${e.message}');
      } on FormatException {
        lastException = Exception('数据格式错误');
      } catch (e) {
        lastException = Exception('未知错误: $e');
      }

      retryCount++;
      if (retryCount < _maxRetries) {
        // 等待一段时间后重试
        await Future.delayed(Duration(seconds: retryCount * 2));
      }
    }

    throw lastException ?? Exception('请求失败');
  }

  /// 释放资源
  void dispose() {
    _client.close();
  }
}
