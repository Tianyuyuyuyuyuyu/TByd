/// NPM Registry Manager - README 页面
///
/// 该文件实现了包的 README 文档展示界面，包括：
/// - Markdown 渲染
/// - 样式定制
/// - 链接处理
/// - 刷新功能
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_markdown/flutter_markdown.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:google_fonts/google_fonts.dart';
import 'package:url_launcher/url_launcher.dart';
import '../providers/npm_package_provider.dart';

/// README 页面组件
///
/// 展示 NPM 包的 README 文档，支持：
/// - Markdown 格式渲染
/// - 代码高亮
/// - 链接跳转
/// - 手动刷新
class ReadmePage extends ConsumerWidget {
  /// 包名
  final String packageName;

  /// 构造函数
  ///
  /// [packageName] 要显示 README 的包名
  /// [key] Widget的键
  const ReadmePage({
    super.key,
    required this.packageName,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final readmeAsync = ref.watch(readmeProvider(packageName));
    final theme = Theme.of(context);

    return Card(
      margin: const EdgeInsets.all(16.0),
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            // 标题栏
            Row(
              children: [
                Text(
                  'README',
                  style: theme.textTheme.headlineMedium?.copyWith(
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(width: 8),
                IconButton(
                  icon: const Icon(Icons.refresh),
                  onPressed: () {
                    // 清除缓存并刷新
                    ref.read(npmPackageServiceProvider).clearCache(packageName);
                    ref.refresh(readmeProvider(packageName));
                  },
                  tooltip: '刷新 README',
                ),
              ],
            ),
            const SizedBox(height: 16),
            // README 内容
            Expanded(
              child: readmeAsync.when(
                data: (content) => SingleChildScrollView(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                    children: [
                      if (content.isEmpty)
                        Center(
                          child: Text(
                            '暂无 README 内容',
                            style: theme.textTheme.bodyLarge?.copyWith(
                              color: theme.colorScheme.onSurfaceVariant,
                            ),
                          ),
                        )
                      else
                        MarkdownBody(
                          data: content,
                          selectable: true,
                          styleSheet: MarkdownStyleSheet(
                            // 标题样式
                            h1: theme.textTheme.headlineLarge?.copyWith(
                              fontWeight: FontWeight.bold,
                              height: 1.5,
                            ),
                            h2: theme.textTheme.headlineMedium?.copyWith(
                              fontWeight: FontWeight.bold,
                              height: 1.5,
                            ),
                            h3: theme.textTheme.headlineSmall?.copyWith(
                              fontWeight: FontWeight.bold,
                              height: 1.5,
                            ),
                            h4: theme.textTheme.titleLarge?.copyWith(
                              fontWeight: FontWeight.bold,
                              height: 1.5,
                            ),
                            h5: theme.textTheme.titleMedium?.copyWith(
                              fontWeight: FontWeight.bold,
                              height: 1.5,
                            ),
                            h6: theme.textTheme.titleSmall?.copyWith(
                              fontWeight: FontWeight.bold,
                              height: 1.5,
                            ),
                            // 段落样式
                            p: theme.textTheme.bodyLarge?.copyWith(
                              height: 1.7,
                            ),
                            // 代码样式
                            code: GoogleFonts.firaCode(
                              textStyle: theme.textTheme.bodyMedium?.copyWith(
                                backgroundColor: theme.colorScheme.surfaceContainerHighest,
                                height: 1.5,
                              ),
                            ),
                            codeblockPadding: const EdgeInsets.all(16),
                            codeblockDecoration: BoxDecoration(
                              color: theme.colorScheme.surfaceContainerHighest,
                              borderRadius: BorderRadius.circular(8),
                            ),
                            // 引用样式
                            blockquote: theme.textTheme.bodyLarge?.copyWith(
                              color: theme.colorScheme.onSurfaceVariant,
                              fontStyle: FontStyle.italic,
                              height: 1.7,
                            ),
                            blockquoteDecoration: BoxDecoration(
                              border: Border(
                                left: BorderSide(
                                  color: theme.colorScheme.primary,
                                  width: 4,
                                ),
                              ),
                            ),
                            blockquotePadding: const EdgeInsets.only(
                              left: 16,
                              top: 8,
                              bottom: 8,
                            ),
                            // 列表样式
                            listBullet: theme.textTheme.bodyLarge?.copyWith(
                              height: 1.7,
                            ),
                            listIndent: 24,
                            listBulletPadding: const EdgeInsets.only(right: 8),
                            // 表格样式
                            tableHead: theme.textTheme.titleSmall?.copyWith(
                              fontWeight: FontWeight.bold,
                            ),
                            tableBody: theme.textTheme.bodyMedium,
                            tableBorder: TableBorder.all(
                              color: theme.colorScheme.outlineVariant,
                              width: 1,
                            ),
                            tableCellsPadding: const EdgeInsets.all(8),
                            // 链接样式
                            a: theme.textTheme.bodyLarge?.copyWith(
                              color: theme.colorScheme.primary,
                              decoration: TextDecoration.underline,
                            ),
                          ),
                          onTapLink: (text, href, title) async {
                            if (href != null) {
                              final uri = Uri.parse(href);
                              if (await canLaunchUrl(uri)) {
                                await launchUrl(
                                  uri,
                                  mode: LaunchMode.externalApplication,
                                );
                              }
                            }
                          },
                        ),
                    ],
                  ),
                ),
                loading: () => const Center(
                  child: CircularProgressIndicator(),
                ),
                error: (error, stack) => Center(
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
                        '加载 README 时发生错误\n$error',
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          color: theme.colorScheme.error,
                        ),
                      ),
                      const SizedBox(height: 16),
                      FilledButton.icon(
                        onPressed: () {
                          // 清除缓存并重试
                          ref.read(npmPackageServiceProvider).clearCache(packageName);
                          ref.refresh(readmeProvider(packageName));
                        },
                        icon: const Icon(Icons.refresh),
                        label: const Text('重试'),
                      ),
                    ],
                  ),
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
