import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/unity_version.dart';

/// Unity 版本服务
///
/// 用于从 Unity Hub API 获取版本信息
class UnityVersionService {
  /// Unity Hub API 的基础 URL
  static const String _baseUrl = 'https://public-cdn.cloud.unity3d.com/hub/prod';

  /// 获取所有 Unity 版本信息
  ///
  /// 从 Unity Hub API 获取所有可用的 Unity 版本信息
  /// 返回 UnityVersion 对象列表
  Future<List<UnityVersion>> fetchVersions() async {
    try {
      final response = await http.get(Uri.parse('$_baseUrl/releases.json'));

      if (response.statusCode == 200) {
        final List<dynamic> jsonList = json.decode(response.body);
        return jsonList.map((json) => UnityVersion.fromJson(json)).toList();
      } else {
        throw Exception('Failed to load Unity versions: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Failed to load Unity versions: $e');
    }
  }
}
