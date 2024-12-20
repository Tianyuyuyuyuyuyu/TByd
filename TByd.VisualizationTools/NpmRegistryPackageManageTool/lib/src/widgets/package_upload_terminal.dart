import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/package_upload_service.dart';
import '../providers/terminal_output_provider.dart';

class PackageUploadTerminal extends ConsumerStatefulWidget {
  final String projectPath;
  final VoidCallback? onUpload;
  final VoidCallback? onUploadSuccess;
  final bool isUploading;

  const PackageUploadTerminal({
    super.key,
    required this.projectPath,
    this.onUpload,
    this.onUploadSuccess,
    this.isUploading = false,
  });

  @override
  ConsumerState<PackageUploadTerminal> createState() => _PackageUploadTerminalState();
}

class _PackageUploadTerminalState extends ConsumerState<PackageUploadTerminal> {
  final ScrollController _scrollController = ScrollController();
  bool _isLocalUploading = false;

  @override
  void initState() {
    super.initState();
    if (ref.read(terminalOutputProvider).isEmpty) {
      Future(() => _initializeTerminal());
    }
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  void _initializeTerminal() {
    if (!mounted) return;

    final notifier = ref.read(terminalOutputProvider.notifier);
    notifier.initialize();
    notifier.addLine('PS ${widget.projectPath}> ');
    _scrollToBottom();
  }

  void _scrollToBottom() {
    if (!_scrollController.hasClients) return;

    WidgetsBinding.instance.addPostFrameCallback((_) {
      _scrollController.jumpTo(_scrollController.position.maxScrollExtent);
    });
  }

  void _appendOutput(String line, {bool showPrompt = false, Color? color}) {
    if (!mounted) return;

    final notifier = ref.read(terminalOutputProvider.notifier);
    final lines = ref.read(terminalOutputProvider);

    if (lines.isNotEmpty && lines.last.text.startsWith('PS ')) {
      // 如果最后一行是提示符，在它之前插入输出
      notifier.insertLine(lines.length - 1, line, color: color);
    } else {
      notifier.addLine(line, color: color);
    }
    _scrollToBottom();
  }

  void _appendCommand(String command) {
    if (!mounted) return;

    final notifier = ref.read(terminalOutputProvider.notifier);
    final lines = ref.read(terminalOutputProvider);

    // 移除旧的提示符
    if (lines.isNotEmpty && lines.last.text.startsWith('PS ')) {
      notifier.removeLast();
    }
    // 添加带命令的提示符
    notifier.addLine('PS ${widget.projectPath}> $command');
    _scrollToBottom();
  }

  Future<bool> _handleProcessOutput(Process process, String command, String commandName) async {
    final utf8Decoder = utf8.decoder;
    final stdoutCompleter = Completer<void>();
    final stderrCompleter = Completer<void>();

    // 显示正在执行的命令
    _appendCommand(command);

    // 处理标准输出
    process.stdout.transform(utf8Decoder).listen(
      (output) {
        final lines = output.split('\n');
        for (final line in lines) {
          final trimmed = line.trimRight();
          if (trimmed.isNotEmpty) {
            _appendOutput(trimmed);
          }
        }
      },
      onDone: () => stdoutCompleter.complete(),
    );

    // 处理标准错误
    process.stderr.transform(utf8Decoder).listen(
      (error) {
        final lines = error.split('\n');
        for (final line in lines) {
          final trimmed = line.trimRight();
          if (trimmed.isNotEmpty) {
            _appendOutput(trimmed);
          }
        }
      },
      onDone: () => stderrCompleter.complete(),
    );

    // 等待两个流都完成
    await Future.wait([stdoutCompleter.future, stderrCompleter.future]);
    final exitCode = await process.exitCode;

    if (exitCode != 0) {
      _appendOutput('\n命令执行失败，退出码：$exitCode');
    }

    // 添加新的命令提示符
    final notifier = ref.read(terminalOutputProvider.notifier);
    notifier.addLine('PS ${widget.projectPath}> ');

    return exitCode == 0;
  }

  Future<void> _startUpload() async {
    if (_isLocalUploading || widget.isUploading) return;

    setState(() {
      _isLocalUploading = true;
    });

    try {
      // 通知父组件开始上传
      widget.onUpload?.call();

      // 清空终端并重新初始化
      ref.read(terminalOutputProvider.notifier).clear();
      _initializeTerminal();

      final uploadService = ref.read(packageUploadServiceProvider);
      bool success = true;

      // 1. 设置 registry
      final setRegistryProcess = await uploadService.executeSetRegistry(widget.projectPath);
      success = await _handleProcessOutput(
        setRegistryProcess,
        'npm set registry ${uploadService.serverUrl}',
        'SET_REGISTRY',
      );

      if (success) {
        // 2. 执行登录
        final loginProcess = await uploadService.executeLogin(widget.projectPath);
        success = await _handleProcessOutput(
          loginProcess,
          'npm whoami --registry ${uploadService.serverUrl}',
          'LOGIN',
        );

        if (success) {
          // 3. 执行发布
          final publishProcess = await uploadService.executePublish(widget.projectPath);
          success = await _handleProcessOutput(
            publishProcess,
            'npm publish --registry ${uploadService.serverUrl}',
            'PUBLISH',
          );
        }
      }

      if (success) {
        _appendOutput('\n包发布成功！', color: Colors.green);
        // 通知父组件上传成功
        widget.onUploadSuccess?.call();
      } else {
        _appendOutput('\n包发布失败，请检查上面的错误信息。', color: Colors.red);
      }
    } catch (e) {
      _appendOutput('\n发生错误：${e.toString()}', color: Colors.red);
    } finally {
      // 移除最后一个提示符
      final notifier = ref.read(terminalOutputProvider.notifier);
      final lines = ref.read(terminalOutputProvider);
      if (lines.isNotEmpty && lines.last.text.startsWith('PS ')) {
        notifier.removeLast();
      }

      if (mounted) {
        setState(() {
          _isLocalUploading = false;
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final outputLines = ref.watch(terminalOutputProvider);
    final isUploading = _isLocalUploading || widget.isUploading;

    return Stack(
      children: [
        Container(
          decoration: BoxDecoration(
            color: const Color(0xFF1E293B),
            borderRadius: BorderRadius.circular(8),
          ),
          margin: const EdgeInsets.all(8),
          padding: const EdgeInsets.all(16),
          child: RawScrollbar(
            controller: _scrollController,
            thumbVisibility: false,
            thickness: 8,
            thumbColor: Colors.white24,
            radius: const Radius.circular(4),
            child: ListView.builder(
              controller: _scrollController,
              itemCount: outputLines.length,
              itemBuilder: (context, index) {
                final line = outputLines[index];
                return SelectableText(
                  line.text,
                  style: TextStyle(
                    color: line.color ?? Colors.white,
                    fontFamily: 'Consolas',
                    fontSize: 14,
                    height: 1.2,
                  ),
                );
              },
            ),
          ),
        ),
        Positioned(
          right: 24,
          bottom: 24,
          child: FilledButton.icon(
            onPressed: isUploading ? null : _startUpload,
            icon: isUploading
                ? const SizedBox(
                    width: 16,
                    height: 16,
                    child: CircularProgressIndicator(
                      strokeWidth: 2,
                      valueColor: AlwaysStoppedAnimation<Color>(Colors.white),
                    ),
                  )
                : const Icon(Icons.upload),
            label: Text(isUploading ? '上传中...' : '一键上传'),
          ),
        ),
      ],
    );
  }
}
