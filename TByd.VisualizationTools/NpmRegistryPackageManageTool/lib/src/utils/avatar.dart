import 'dart:convert';
import 'package:crypto/crypto.dart';
import 'package:flutter/material.dart';

/// 头像工具类
class AvatarUtil {
  /// 获取用户头像颜色
  static Color getUserColor(String username) {
    final hash = md5.convert(utf8.encode(username.toLowerCase())).toString();
    final colors = [
      const Color(0xFF001F3F), // Navy Blue
      const Color(0xFF0074D9), // Blue
      const Color(0xFF7FDBFF), // Aqua
      const Color(0xFF39CCCC), // Teal
      const Color(0xFF3D9970), // Olive
      const Color(0xFF2ECC40), // Green
      const Color(0xFF01FF70), // Lime
      const Color(0xFFFFDC00), // Yellow
      const Color(0xFFFF851B), // Orange
      const Color(0xFFFF4136), // Red
      const Color(0xFFF012BE), // Fuchsia
      const Color(0xFFB10DC9), // Purple
      const Color(0xFF85144B), // Maroon
      const Color(0xFFAAAAAA), // Gray
    ];

    final colorIndex = int.parse(hash.substring(0, 2), radix: 16) % colors.length;
    return colors[colorIndex];
  }

  /// 获取用户头像文本
  static String getInitial(String username) {
    return username.isNotEmpty ? username[0].toUpperCase() : '?';
  }
}

/// 自定义头像绘制组件
class CustomUserAvatar extends StatelessWidget {
  final String username;
  final double size;

  const CustomUserAvatar({
    super.key,
    required this.username,
    this.size = 80,
  });

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: size,
      height: size,
      child: CustomPaint(
        painter: _AvatarPainter(
          backgroundColor: AvatarUtil.getUserColor(username),
          text: AvatarUtil.getInitial(username),
        ),
      ),
    );
  }
}

/// 头像绘制器
class _AvatarPainter extends CustomPainter {
  final Color backgroundColor;
  final String text;

  _AvatarPainter({
    required this.backgroundColor,
    required this.text,
  });

  @override
  void paint(Canvas canvas, Size size) {
    // 绘制背景
    final paint = Paint()..color = backgroundColor;
    canvas.drawCircle(
      Offset(size.width / 2, size.height / 2),
      size.width / 2,
      paint,
    );

    // 绘制文本
    final textPainter = TextPainter(
      text: TextSpan(
        text: text,
        style: TextStyle(
          color: Colors.white,
          fontSize: size.width * 0.5,
          fontWeight: FontWeight.bold,
        ),
      ),
      textDirection: TextDirection.ltr,
      textAlign: TextAlign.center,
    );

    textPainter.layout();
    textPainter.paint(
      canvas,
      Offset(
        (size.width - textPainter.width) / 2,
        (size.height - textPainter.height) / 2,
      ),
    );
  }

  @override
  bool shouldRepaint(_AvatarPainter oldDelegate) {
    return backgroundColor != oldDelegate.backgroundColor || text != oldDelegate.text;
  }
}
