import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:shared_preferences/shared_preferences.dart';

const String _localeKey = 'app_locale';

class LocaleNotifier extends StateNotifier<Locale> {
  final SharedPreferences _prefs;

  LocaleNotifier(this._prefs) : super(_loadInitialLocale(_prefs));

  static Locale _loadInitialLocale(SharedPreferences prefs) {
    final String? languageCode = prefs.getString(_localeKey);
    return languageCode != null ? Locale(languageCode) : const Locale('zh');
  }

  Future<void> setLocale(Locale locale) async {
    await _prefs.setString(_localeKey, locale.languageCode);
    state = locale;
  }

  static List<Locale> get supportedLocales => const [
        Locale('zh'), // 中文
        Locale('en'), // 英文
      ];

  String getLanguageName(String languageCode) {
    switch (languageCode) {
      case 'zh':
        return '中文';
      case 'en':
        return 'English';
      default:
        return languageCode;
    }
  }
}

// 创建一个全局的 SharedPreferences 实例
final sharedPreferencesProvider = Provider<SharedPreferences>((ref) {
  throw UnimplementedError('需要在 ProviderScope 中提供 SharedPreferences 实例');
});

// 创建 LocaleNotifier 的 Provider
final localeNotifierProvider = StateNotifierProvider<LocaleNotifier, Locale>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  return LocaleNotifier(prefs);
});

// 创建当前语言的 Provider
final localeProvider = Provider<Locale>((ref) {
  return ref.watch(localeNotifierProvider);
});
