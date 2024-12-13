import 'package:equatable/equatable.dart';

class AuthModel extends Equatable {
  final String token;
  final String username;
  final String serverUrl;
  final DateTime expiresAt;

  const AuthModel({
    required this.token,
    required this.username,
    required this.serverUrl,
    required this.expiresAt,
  });

  bool get isExpired => DateTime.now().isAfter(expiresAt);

  @override
  List<Object?> get props => [token, username, serverUrl, expiresAt];

  Map<String, dynamic> toJson() {
    return {
      'token': token,
      'username': username,
      'serverUrl': serverUrl,
      'expiresAt': expiresAt.toIso8601String(),
    };
  }

  factory AuthModel.fromJson(Map<String, dynamic> json) {
    return AuthModel(
      token: json['token'] as String,
      username: json['username'] as String,
      serverUrl: json['serverUrl'] as String,
      expiresAt: DateTime.parse(json['expiresAt'] as String),
    );
  }
}
