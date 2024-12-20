import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/package_upload_service.dart';

class PackageUploadTerminal extends ConsumerStatefulWidget {
  final String projectPath;
  final VoidCallback? onUpload;
  final bool isUploading;

  const PackageUploadTerminal({
    super.key,
    required this.projectPath,
    this.onUpload,
    this.isUploading = false,
  });

  @override
  ConsumerState<PackageUploadTerminal> createState() => _PackageUploadTerminalState();
}

class _PackageUploadTerminalState extends ConsumerState<PackageUploadTerminal> {
  final ScrollController _scrollController = ScrollController();
  final List<String> _outputLines = [];

  @override
  void initState() {
    super.initState();
    _initializeTerminal();
  }

  @override
  void dispose() {
    _scrollController.dispose();
    super.dispose();
  }

  void _initializeTerminal() {
    setState(() {
      _outputLines.addAll([
        'Windows PowerShell',
        '版权所有 (C) Microsoft Corporation。保留所有权利。',
        '',
        'PS ${widget.projectPath}> ',
      ]);
    });
    _scrollToBottom();
  }

  void _scrollToBottom() {
    if (!_scrollController.hasClients) return;

    WidgetsBinding.instance.addPostFrameCallback((_) {
      _scrollController.jumpTo(_scrollController.position.maxScrollExtent);
    });
  }

  void _appendOutput(String line) {
    if (!mounted) return;

    setState(() {
      if (_outputLines.isNotEmpty && _outputLines.last.startsWith('PS ')) {
        // 如果最后一行是提示符，在它之前插入输出
        _outputLines.insert(_outputLines.length - 1, line);
      } else {
        _outputLines.add(line);
      }
    });
    _scrollToBottom();
  }

  void _appendCommand(String command) {
    if (!mounted) return;

    setState(() {
      // 移除旧的提示符
      if (_outputLines.isNotEmpty && _outputLines.last.startsWith('PS ')) {
        _outputLines.removeLast();
      }
      // 添加带命令的提示符
      _outputLines.add('PS ${widget.projectPath}> $command');
      // 添加新的提示符
      _outputLines.add('PS ${widget.projectPath}> ');
    });
    _scrollToBottom();
  }

  Future<bool> _handleProcessOutput(Process process, String command, String commandName) async {
    final utf8Decoder = utf8.decoder;
    final stdoutCompleter = Completer<void>();
    final stderrCompleter = Completer<void>();

    // 显示正在执行的命令
    _appendCommand(command);

    process.stdout.transform(utf8Decoder).listen(
      (output) {
        final lines = output.split('\n');
        for (final line in lines) {
          if (line.trim().isNotEmpty) {
            _appendOutput(line.trimRight());
          }
        }
      },
      onDone: () => stdoutCompleter.complete(),
    );

    process.stderr.transform(utf8Decoder).listen(
      (error) {
        final lines = error.split('\n');
        for (final line in lines) {
          if (line.trim().isNotEmpty) {
            _appendOutput(line.trimRight());
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

    return exitCode == 0;
  }

  Future<void> _startUpload() async {
    if (widget.isUploading) return;

    // 通知父组件开始上传
    widget.onUpload?.call();

    setState(() {
      _outputLines.clear();
    });
    _initializeTerminal();

    final uploadService = ref.read(packageUploadServiceProvider);
    bool success = true;

    try {
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
        _appendOutput('\n包发布成功！');
      } else {
        _appendOutput('\n包发布失败，请检查上面的错误信息。');
      }
    } catch (e) {
      _appendOutput('\n发生错误：${e.toString()}');
      success = false;
    }
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Expanded(
          child: Container(
            decoration: BoxDecoration(
              color: const Color(0xFF1E293B),
              borderRadius: BorderRadius.circular(8),
            ),
            margin: const EdgeInsets.all(16),
            padding: const EdgeInsets.all(16),
            child: RawScrollbar(
              controller: _scrollController,
              thumbVisibility: false,
              thickness: 8,
              thumbColor: Colors.white24,
              radius: const Radius.circular(4),
              child: ListView.builder(
                controller: _scrollController,
                itemCount: _outputLines.length,
                itemBuilder: (context, index) {
                  return SelectableText(
                    _outputLines[index],
                    style: const TextStyle(
                      color: Colors.white,
                      fontFamily: 'Consolas',
                      fontSize: 14,
                      height: 1.2,
                    ),
                  );
                },
              ),
            ),
          ),
        ),
        Padding(
          padding: const EdgeInsets.all(16),
          child: ElevatedButton(
            onPressed: widget.isUploading ? null : _startUpload,
            child: Text(widget.isUploading ? 'Uploading...' : 'Start Upload'),
          ),
        ),
      ],
    );
  }
}
