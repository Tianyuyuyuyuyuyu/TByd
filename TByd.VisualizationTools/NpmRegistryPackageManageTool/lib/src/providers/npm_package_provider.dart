import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/npm_package_service.dart';
import '../providers/app_config_provider.dart';
import '../providers/auth_provider.dart';

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

  ref.onDispose(() {
    service.dispose();
  });

  return service;
});

final readmeProvider = FutureProvider.family<String, String>((ref, packageName) async {
  if (packageName.isEmpty) {
    return '';
  }
  final service = ref.watch(npmPackageServiceProvider);
  return service.fetchReadme(packageName);
});
