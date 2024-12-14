/// NPM Registry Manager - 语言设置页面
///
/// 该文件实现了应用程序的语言设置界面，包括：
/// - 支持的语言列表
/// - 当前语言显示
/// - 语言切换功能
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/locale_provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

/// 语言设置页面组件
///
/// 提供语言设置功能，包括：
/// - 显示所有支持的语言
/// - 当前语言标记
/// - 语言切换
class LanguageSettingsPage extends ConsumerWidget {
  /// 构造函数
  const LanguageSettingsPage({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final l10n = AppLocalizations.of(context);
    final theme = Theme.of(context);
    final currentLocale = ref.watch(localeProvider);
    final localeNotifier = ref.read(localeNotifierProvider.notifier);

    return Scaffold(
      appBar: AppBar(
        title: Text(l10n.language),
      ),
      body: ListView.builder(
        padding: const EdgeInsets.symmetric(vertical: 8),
        itemCount: LocaleNotifier.supportedLocales.length,
        itemBuilder: (context, index) {
          final locale = LocaleNotifier.supportedLocales[index];
          final isSelected = currentLocale.languageCode == locale.languageCode;

          return ListTile(
            contentPadding: const EdgeInsets.symmetric(
              horizontal: 24,
              vertical: 4,
            ),
            title: Text(
              localeNotifier.getLanguageName(locale.languageCode),
              style: theme.textTheme.bodyLarge?.copyWith(
                fontWeight: isSelected ? FontWeight.bold : null,
                color: isSelected ? theme.colorScheme.primary : theme.colorScheme.onSurface,
              ),
            ),
            trailing: isSelected
                ? Icon(
                    Icons.check_circle,
                    color: theme.colorScheme.primary,
                  )
                : null,
            shape: RoundedRectangleBorder(
              borderRadius: BorderRadius.circular(8),
            ),
            selected: isSelected,
            selectedTileColor: theme.colorScheme.primaryContainer.withOpacity(0.12),
            onTap: () {
              if (!isSelected) {
                localeNotifier.setLocale(locale);
                // 显示切换成��提示
                ScaffoldMessenger.of(context).showSnackBar(
                  SnackBar(
                    content: Text(
                      '语言已切换到${localeNotifier.getLanguageName(locale.languageCode)}',
                      style: TextStyle(
                        color: theme.colorScheme.onPrimary,
                      ),
                    ),
                    backgroundColor: theme.colorScheme.primary,
                    behavior: SnackBarBehavior.floating,
                    shape: RoundedRectangleBorder(
                      borderRadius: BorderRadius.circular(8),
                    ),
                    margin: const EdgeInsets.all(8),
                    duration: const Duration(seconds: 2),
                  ),
                );
              }
            },
          );
        },
      ),
    );
  }
}
