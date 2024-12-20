import 'dart:convert';
import 'package:crypto/crypto.dart';

/// Gravatar 工具类
class GravatarUtil {
  /// 生成 Gravatar URL
  ///
  /// [email] 用户邮箱
  /// [size] 头像大小（像素）
  /// [defaultImage] 默认头像类型，可选值：404, mp, identicon, monsterid, wavatar, retro, robohash, blank
  static String getAvatarUrl(String email, {int size = 80, String defaultImage = 'retro'}) {
    final trimmedEmail = email.trim().toLowerCase();
    final hash = md5.convert(utf8.encode(trimmedEmail)).toString();
    return 'https://www.gravatar.com/avatar/$hash?s=$size&d=$defaultImage';
  }

  /// 从用户名生成默认头像 URL
  ///
  /// 如果用户没有设置邮箱，使用用户名生成一个默认的头像
  /// [username] 用户名
  /// [size] 头像大小（像素）
  static String getDefaultAvatarUrl(String username, {int size = 80}) {
    final hash = md5.convert(utf8.encode(username.toLowerCase())).toString();
    return 'https://www.gravatar.com/avatar/$hash?s=$size&d=retro&f=y';
  }
}
