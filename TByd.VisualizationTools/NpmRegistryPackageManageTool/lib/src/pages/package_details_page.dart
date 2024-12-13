import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/package_provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package:flutter_svg/flutter_svg.dart';
// 暂时注释掉 url_launcher 的导入，直到包安装完成
// import 'package:url_launcher/url_launcher.dart';

class PackageDetailsPage extends ConsumerWidget {
  final String packageName;

  const PackageDetailsPage({
    super.key,
    required this.packageName,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final theme = Theme.of(context);
    final l10n = AppLocalizations.of(context);
    final packageDetails = ref.watch(packageDetailsProvider(packageName));

    return DefaultTabController(
      length: 5,
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // 包标题和版本信息
          Padding(
            padding: const EdgeInsets.all(16.0),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                // 显示包名
                Text(
                  packageName,
                  style: theme.textTheme.titleMedium?.copyWith(
                    color: theme.colorScheme.onSurfaceVariant,
                  ),
                ),
                if (packageDetails.package != null) ...[
                  const SizedBox(height: 4),
                  // 显示 displayName
                  Text(
                    packageDetails.package!.displayName,
                    style: theme.textTheme.headlineMedium,
                  ),
                  const SizedBox(height: 8),
                  Text(
                    packageDetails.package!.description,
                    style: theme.textTheme.bodyLarge,
                  ),
                  const SizedBox(height: 16),
                  Text(
                    'Latest v${packageDetails.package!.version} · Published ${_formatDate(packageDetails.package!.publishedAt)}',
                    style: theme.textTheme.bodyMedium?.copyWith(
                      color: theme.colorScheme.onSurfaceVariant,
                    ),
                  ),
                ],
              ],
            ),
          ),
          // 操作按钮
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16.0),
            child: Row(
              children: [
                _ActionButton(
                  icon: Icons.home,
                  label: 'Homepage',
                  onPressed: () {},
                ),
                const SizedBox(width: 8),
                _ActionButton(
                  icon: Icons.cloud_download,
                  label: 'Download',
                  onPressed: () {},
                ),
                const SizedBox(width: 8),
                _ActionButton(
                  icon: Icons.code,
                  label: 'Repository',
                  onPressed: () {},
                ),
                const SizedBox(width: 8),
                _ActionButton(
                  icon: Icons.bug_report,
                  label: 'Issues',
                  onPressed: () {},
                ),
              ],
            ),
          ),
          const SizedBox(height: 16),
          // 标签栏
          TabBar(
            tabs: [
              Tab(text: l10n.readmeTab),
              Tab(text: l10n.dependenciesTab),
              Tab(text: l10n.versionsTab),
              Tab(text: l10n.uplinksTab),
              Tab(text: l10n.packageInstallation),
            ],
          ),
          Expanded(
            child: TabBarView(
              children: [
                _ReadmeTab(packageDetails: packageDetails),
                _DependenciesTab(packageDetails: packageDetails),
                _VersionsTab(packageDetails: packageDetails),
                const _UplinksTab(),
                // 添加 Installation 标签页
                if (packageDetails.package != null)
                  InstallationSection(
                    packageName: packageDetails.package!.name,
                    version: packageDetails.package!.version,
                  )
                else
                  const Center(child: CircularProgressIndicator()),
              ],
            ),
          ),
        ],
      ),
    );
  }

  String _formatDate(DateTime date) {
    final now = DateTime.now();
    final difference = now.difference(date);

    if (difference.inDays < 1) {
      return 'today';
    } else if (difference.inDays < 30) {
      return '${difference.inDays} days ago';
    } else if (difference.inDays < 365) {
      final months = (difference.inDays / 30).floor();
      return '$months months ago';
    } else {
      final years = (difference.inDays / 365).floor();
      return '$years years ago';
    }
  }
}

class _ActionButton extends StatelessWidget {
  final IconData icon;
  final String label;
  final VoidCallback onPressed;

  const _ActionButton({
    required this.icon,
    required this.label,
    required this.onPressed,
  });

  @override
  Widget build(BuildContext context) {
    return ElevatedButton.icon(
      icon: Icon(icon, size: 18),
      label: Text(label),
      onPressed: onPressed,
      style: ElevatedButton.styleFrom(
        padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
      ),
    );
  }
}

// 标签页内容组件
class _ReadmeTab extends StatelessWidget {
  final PackageDetailsState packageDetails;

  const _ReadmeTab({required this.packageDetails});

  @override
  Widget build(BuildContext context) {
    if (packageDetails.isLoading) {
      return const Center(child: CircularProgressIndicator());
    }

    if (packageDetails.error != null) {
      return Center(child: Text(packageDetails.error!));
    }

    return SingleChildScrollView(
      padding: const EdgeInsets.all(16),
      child: Text(packageDetails.package?.description ?? ''),
    );
  }
}

class _DependenciesTab extends StatelessWidget {
  final PackageDetailsState packageDetails;

  const _DependenciesTab({required this.packageDetails});

  @override
  Widget build(BuildContext context) {
    if (packageDetails.package == null) {
      return const Center(child: Text('No dependencies information available'));
    }

    return ListView.builder(
      padding: const EdgeInsets.all(16),
      itemCount: packageDetails.package!.dependencies.length,
      itemBuilder: (context, index) {
        final entry = packageDetails.package!.dependencies.entries.elementAt(index);
        return ListTile(
          title: Text(entry.key),
          subtitle: Text(entry.value),
        );
      },
    );
  }
}

class _VersionsTab extends StatelessWidget {
  final PackageDetailsState packageDetails;

  const _VersionsTab({required this.packageDetails});

  @override
  Widget build(BuildContext context) {
    return ListView.builder(
      padding: const EdgeInsets.all(16),
      itemCount: packageDetails.versions.length,
      itemBuilder: (context, index) {
        final version = packageDetails.versions[index];
        return ListTile(
          title: Text('v${version.version}'),
          subtitle: Text(_formatDate(version.publishedAt)),
        );
      },
    );
  }

  String _formatDate(DateTime date) {
    return '${date.year}-${date.month.toString().padLeft(2, '0')}-${date.day.toString().padLeft(2, '0')}';
  }
}

class _UplinksTab extends StatelessWidget {
  const _UplinksTab();

  @override
  Widget build(BuildContext context) {
    return const Center(child: Text('No uplinks information available'));
  }
}

class InstallationSection extends StatelessWidget {
  final String packageName;
  final String version;

  const InstallationSection({
    super.key,
    required this.packageName,
    required this.version,
  });

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final l10n = AppLocalizations.of(context);

    return SingleChildScrollView(
      child: Container(
        width: double.infinity,
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            _InstallCommand(
              icon: 'assets/images/npm.svg',
              label: 'npm',
              command: 'npm install $packageName@$version',
            ),
            const Divider(height: 1),
            _InstallCommand(
              icon: 'assets/images/yarn.svg',
              label: 'yarn',
              command: 'yarn add $packageName@$version',
            ),
            const Divider(height: 1),
            _InstallCommand(
              icon: 'assets/images/pnpm.svg',
              label: 'pnpm',
              command: 'pnpm install $packageName@$version',
            ),
          ],
        ),
      ),
    );
  }
}

class _InstallCommand extends StatelessWidget {
  final String icon;
  final String label;
  final String command;

  const _InstallCommand({
    required this.icon,
    required this.label,
    required this.command,
  });

  @override
  Widget build(BuildContext context) {
    return ListTile(
      leading: SizedBox(
        width: 24,
        height: 24,
        child: SvgPicture.asset(icon),
      ),
      title: Row(
        children: [
          Expanded(
            child: Text(command),
          ),
          IconButton(
            icon: const Icon(Icons.copy, size: 20),
            onPressed: () {
              // 复制命令到剪贴板
              // TODO: 添加 url_launcher 包后实现
            },
            tooltip: 'Copy to clipboard',
          ),
        ],
      ),
    );
  }
}
