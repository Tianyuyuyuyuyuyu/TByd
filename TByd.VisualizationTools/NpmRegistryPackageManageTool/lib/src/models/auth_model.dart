import 'package:equatable/equatable.dart';

class User extends Equatable {
  final String username;
  final String email;
  final String token;

  const User({
    required this.username,
    required this.email,
    required this.token,
  });

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      username: json['username'] as String,
      email: json['email'] as String,
      token: json['token'] as String,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'username': username,
      'email': email,
      'token': token,
    };
  }

  @override
  List<Object?> get props => [username, email, token];
}

class AuthCredentials {
  final String username;
  final String password;
  final String email;

  const AuthCredentials({
    required this.username,
    required this.password,
    required this.email,
  });

  Map<String, dynamic> toJson() {
    return {
      'name': username,
      'password': password,
      'email': email,
    };
  }
}
