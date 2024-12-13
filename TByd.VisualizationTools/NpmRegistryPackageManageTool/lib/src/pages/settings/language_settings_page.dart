import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/locale_provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

class LanguageSettingsPage extends ConsumerWidget {
  const LanguageSettingsPage({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final l10n = AppLocalizations.of(context);
    final currentLocale = ref.watch(localeProvider);
    final localeNotifier = ref.read(localeNotifierProvider.notifier);

    return Scaffold(
      appBar: AppBar(
        title: Text(l10n.language),
      ),
      body: ListView.builder(
        itemCount: LocaleNotifier.supportedLocales.length,
        itemBuilder: (context, index) {
          final locale = LocaleNotifier.supportedLocales[index];
          final isSelected = currentLocale.languageCode == locale.languageCode;

          return ListTile(
            title: Text(localeNotifier.getLanguageName(locale.languageCode)),
            trailing: isSelected
                ? Icon(
                    Icons.check,
                    color: Theme.of(context).colorScheme.primary,
                  )
                : null,
            onTap: () {
              if (!isSelected) {
                localeNotifier.setLocale(locale);
              }
            },
          );
        },
      ),
    );
  }
}
