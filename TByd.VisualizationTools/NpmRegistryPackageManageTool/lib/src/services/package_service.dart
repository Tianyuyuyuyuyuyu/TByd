import 'dart:convert';
import 'package:http/http.dart' as http;
import '../models/package_model.dart';
import '../utils/constants.dart';

class PackageService {
  final String serverUrl;
  final String? token;
  final http.Client _client;

  PackageService({
    required this.serverUrl,
    this.token,
    http.Client? client,
  }) : _client = client ?? http.Client();

  Future<List<PackageSearchResult>> searchPackages(String query) async {
    if (serverUrl.isEmpty) {
      throw Exception('Server URL is not set');
    }

    final uri = Uri.parse('$serverUrl${ApiConstants.search}').replace(
      queryParameters: {
        'text': query,
      },
    );

    try {
      final response = await _client.get(
        uri,
        headers: _getHeaders(),
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body) as Map<String, dynamic>;
        final objects = data['objects'] as List<dynamic>;

        return objects.map((obj) => PackageSearchResult.fromJson(obj['package'] as Map<String, dynamic>)).toList();
      } else {
        throw Exception('Failed to search packages: ${response.statusCode}');
      }
    } catch (e) {
      throw Exception('Failed to search packages: $e');
    }
  }

  Future<Package> getPackageDetails(String packageName) async {
    if (serverUrl.isEmpty) {
      throw Exception('Server URL is not set');
    }

    final uri = Uri.parse('$serverUrl/$packageName');

    try {
      final response = await _client.get(
        uri,
        headers: _getHeaders(),
      );

      if (response.statusCode == 200) {
        final data = json.decode(response.body) as Map<String, dynamic>;
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
      final response = await _client.get(
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
      final response = await _client.delete(
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
      final response = await _client.put(
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
    return {
      'Content-Type': 'application/json',
      if (token != null) 'Authorization': 'Bearer $token',
    };
  }

  void dispose() {
    _client.close();
  }
}
