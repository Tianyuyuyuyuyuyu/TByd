import 'dart:io';
import 'package:flutter/material.dart';
import 'package:flutter_markdown/flutter_markdown.dart';
import 'package:url_launcher/url_launcher.dart' as url_launcher;

/// Markdown 文件查看器组件
class MarkdownFileViewer extends StatefulWidget {
  /// 项目根目录路径
  final String projectPath;

  /// 文件名
  final String fileName;

  const MarkdownFileViewer({
    super.key,
    required this.projectPath,
    required this.fileName,
  });

  @override
  State<MarkdownFileViewer> createState() => _MarkdownFileViewerState();
}

class _MarkdownFileViewerState extends State<MarkdownFileViewer> {
  String? _content;
  String? _error;

  @override
  void initState() {
    super.initState();
    _loadContent();
  }

  @override
  void didUpdateWidget(MarkdownFileViewer oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.projectPath != widget.projectPath || oldWidget.fileName != widget.fileName) {
      _loadContent();
    }
  }

  Future<void> _loadContent() async {
    try {
      final file = File('${widget.projectPath}/${widget.fileName}');
      if (await file.exists()) {
        final content = await file.readAsString();
        if (mounted) {
          setState(() {
            _content = content;
            _error = null;
          });
        }
      } else {
        if (mounted) {
          setState(() {
            _content = null;
            _error = '文件不存在';
          });
        }
      }
    } catch (e) {
      if (mounted) {
        setState(() {
          _content = null;
          _error = '读取文件失败: $e';
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);

    if (_error != null) {
      return Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              Icons.error_outline,
              size: 48,
              color: theme.colorScheme.error,
            ),
            const SizedBox(height: 16),
            Text(
              _error!,
              style: theme.textTheme.bodyLarge?.copyWith(
                color: theme.colorScheme.error,
              ),
            ),
            const SizedBox(height: 24),
            FilledButton.icon(
              onPressed: _loadContent,
              icon: const Icon(Icons.refresh),
              label: const Text('重试'),
            ),
          ],
        ),
      );
    }

    if (_content == null) {
      return const Center(
        child: CircularProgressIndicator(),
      );
    }

    return Card(
      margin: EdgeInsets.zero,
      shape: const RoundedRectangleBorder(
        borderRadius: BorderRadius.zero,
      ),
      child: Stack(
        children: [
          Markdown(
            data: _content!,
            selectable: true,
            onTapLink: (text, href, title) {
              if (href != null) {
                url_launcher.launchUrl(Uri.parse(href));
              }
            },
            styleSheet: MarkdownStyleSheet(
              h1: theme.textTheme.headlineMedium,
              h2: theme.textTheme.headlineSmall,
              h3: theme.textTheme.titleLarge,
              h4: theme.textTheme.titleMedium,
              h5: theme.textTheme.titleSmall,
              h6: theme.textTheme.bodyLarge,
              p: theme.textTheme.bodyMedium,
              code: theme.textTheme.bodyMedium?.copyWith(
                fontFamily: 'monospace',
                backgroundColor: theme.colorScheme.surfaceVariant,
              ),
              codeblockDecoration: BoxDecoration(
                color: theme.colorScheme.surfaceVariant,
                borderRadius: BorderRadius.circular(4),
              ),
            ),
          ),
          Positioned(
            top: 8,
            right: 8,
            child: IconButton(
              icon: const Icon(Icons.edit),
              onPressed: () async {
                final file = File('${widget.projectPath}/${widget.fileName}');
                final uri = Uri.file(file.path);
                if (await url_launcher.canLaunchUrl(uri)) {
                  await url_launcher.launchUrl(uri);
                }
              },
              tooltip: '编辑文件',
            ),
          ),
        ],
      ),
    );
  }
}
