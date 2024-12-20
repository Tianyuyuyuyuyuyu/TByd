import 'dart:io';
import 'dart:convert';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';

/// 包上传服务提供者
final packageUploadServiceProvider = Provider<PackageUploadService>((ref) {
  final authState = ref.watch(authProvider);
  return PackageUploadService(
    serverUrl: authState.user?.serverUrl ?? '',
    username: authState.user?.username ?? '',
    token: authState.auth?.token ?? '',
  );
});

/// 包上传服务
class PackageUploadService {
  final String serverUrl;
  final String username;
  final String token;

  PackageUploadService({
    required this.serverUrl,
    required this.username,
    required this.token,
  });

  /// 执行 npm set registry 命令
  Future<Process> executeSetRegistry(String projectPath) async {
    final command = Platform.isWindows ? 'powershell' : 'bash';
    final arguments = Platform.isWindows
        ? ['-NoProfile', '-Command', 'npm set registry $serverUrl']
        : ['-c', 'npm set registry $serverUrl'];

    return Process.start(
      command,
      arguments,
      workingDirectory: projectPath,
      runInShell: true,
    );
  }

  /// 执行 npm login 命令（非交互式）
  Future<Process> executeLogin(String projectPath) async {
    // 创建 .npmrc 文件
    final npmrcPath = Platform.isWindows ? '$projectPath\\.npmrc' : '$projectPath/.npmrc';

    final npmrcFile = File(npmrcPath);
    await npmrcFile.writeAsString('''
registry=$serverUrl
//$serverUrl:_auth=${base64.encode(utf8.encode('$username:$token'))}
''');

    final command = Platform.isWindows ? 'powershell' : 'bash';
    final arguments = Platform.isWindows
        ? ['-NoProfile', '-Command', 'npm whoami --registry $serverUrl']
        : ['-c', 'npm whoami --registry $serverUrl'];

    return Process.start(
      command,
      arguments,
      workingDirectory: projectPath,
      runInShell: true,
    );
  }

  /// 执行 npm publish 命令
  Future<Process> executePublish(String projectPath) async {
    final command = Platform.isWindows ? 'powershell' : 'bash';
    final arguments = Platform.isWindows
        ? ['-NoProfile', '-Command', 'npm publish --registry $serverUrl']
        : ['-c', 'npm publish --registry $serverUrl'];

    return Process.start(
      command,
      arguments,
      workingDirectory: projectPath,
      runInShell: true,
    );
  }
}
