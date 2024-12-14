/// NPM Registry Manager - 语言环境管理
///
/// 该文件负责处理应用程序的国际化和本地化功能，包括：
/// - 语言环境切换
/// - 语言设置的持久化
/// - 支持的语言列表管理
/// - 语言显示名称的本地化
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:shared_preferences/shared_preferences.dart';

/// 存储语言设置的键名
const String _localeKey = 'app_locale';

/// 语言环境状态管理器
///
/// 负责管理应用程序的语言环境状态，包括：
/// - 加载初始语言设置
/// - 切换语言
/// - 持久化语言选择
/// - 提供支持的语言列表
class LocaleNotifier extends StateNotifier<Locale> {
  /// 本地存储实例，用于持久化语言设置
  final SharedPreferences _prefs;

  /// 构造函数
  ///
  /// 使用 [SharedPreferences] 实例初始化并加载保存的语言设置
  /// [_prefs] - SharedPreferences 实例
  LocaleNotifier(this._prefs) : super(_loadInitialLocale(_prefs));

  /// 加载初始语言设置
  ///
  /// 从本地存储中读取已保存的语言设置
  /// 如果没有保存的设置，默认使用中文
  /// [prefs] - SharedPreferences 实例
  static Locale _loadInitialLocale(SharedPreferences prefs) {
    final String? languageCode = prefs.getString(_localeKey);
    return languageCode != null ? Locale(languageCode) : const Locale('zh');
  }

  /// 设置新的语言环境
  ///
  /// 更新并持久化语言设置
  /// [locale] - 新的语言环境
  Future<void> setLocale(Locale locale) async {
    await _prefs.setString(_localeKey, locale.languageCode);
    state = locale;
  }

  /// 获取支持的语言环境列表
  ///
  /// 返回应用程序支持的所有语言环境
  static List<Locale> get supportedLocales => const [
        Locale('zh'), // 中文
        Locale('en'), // 英文
      ];

  /// 获取语言的本地化显示名称
  ///
  /// 根据语言代码返回对应的语言名称
  /// [languageCode] - ISO 639-1 语言代码
  /// 返回对应的语言显示名称
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

/// 全局 SharedPreferences 提供者
///
/// 需要在应用程序启动时通过 ProviderScope 提供实例
final sharedPreferencesProvider = Provider<SharedPreferences>((ref) {
  throw UnimplementedError('需要在 ProviderScope 中提供 SharedPreferences 实例');
});

/// 语言环境状态管理器提供者
///
/// 提供 [LocaleNotifier] 实例，用于管理语言环境状态
final localeNotifierProvider = StateNotifierProvider<LocaleNotifier, Locale>((ref) {
  final prefs = ref.watch(sharedPreferencesProvider);
  return LocaleNotifier(prefs);
});

/// 当前语言环境提供者
///
/// 提供对当前活动语言环境的访问
final localeProvider = Provider<Locale>((ref) {
  return ref.watch(localeNotifierProvider);
});
