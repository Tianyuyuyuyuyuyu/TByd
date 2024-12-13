import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import '../widgets/custom_text_field.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import '../models/login_history_model.dart';
import 'home_page.dart';

class LoginPage extends ConsumerStatefulWidget {
  const LoginPage({super.key});

  @override
  ConsumerState<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends ConsumerState<LoginPage> {
  final _formKey = GlobalKey<FormState>();
  final _serverController = TextEditingController();
  final _usernameController = TextEditingController();
  final _passwordController = TextEditingController();
  bool _rememberMe = false;
  bool _isPasswordVisible = false;
  bool _isManualInput = false;
  List<ServerHistory> _servers = [];
  ServerHistory? _selectedServer;

  @override
  void initState() {
    super.initState();
    _loadServers();
  }

  void _clearUserInputs() {
    _usernameController.clear();
    _passwordController.clear();
    _rememberMe = false;
    _isPasswordVisible = false;
  }

  Future<void> _loadServers() async {
    final history = await ref.read(loginHistoryServiceProvider).getHistory();
    setState(() {
      _servers = history.servers;
      if (_servers.isNotEmpty) {
        // 按最后使用时间排序，选择最近使用的服务器
        _servers.sort((a, b) => b.lastUsed.compareTo(a.lastUsed));
        _selectedServer = _servers.first;
        _serverController.text = _selectedServer!.serverUrl;
        // 不自动填充用户信息
        _clearUserInputs();
      } else {
        _isManualInput = true;
      }
    });
  }

  void _handleServerChange(String? serverUrl) {
    if (serverUrl == null) return;

    setState(() {
      _serverController.text = serverUrl;
      _selectedServer = _servers.firstWhere(
        (s) => s.serverUrl == serverUrl,
        orElse: () => ServerHistory(
          serverUrl: serverUrl,
          serverName: serverUrl,
          users: const [],
          lastUsed: DateTime.now(),
        ),
      );
      // 切换服务器时清空用户信息
      _clearUserInputs();
    });
  }

  void _switchToManualInput() {
    setState(() {
      _isManualInput = true;
      _serverController.clear();
      _selectedServer = null;
      _clearUserInputs();
    });
  }

  void _switchToServerList() {
    if (_servers.isEmpty) return;

    setState(() {
      _isManualInput = false;
      if (_selectedServer != null) {
        _serverController.text = _selectedServer!.serverUrl;
      } else if (_servers.isNotEmpty) {
        _selectedServer = _servers.first;
        _serverController.text = _selectedServer!.serverUrl;
      }
      // 切换到服务器列表时清空用户信息
      _clearUserInputs();
    });
  }

  @override
  void dispose() {
    _serverController.dispose();
    _usernameController.dispose();
    _passwordController.dispose();
    super.dispose();
  }

  Future<void> _handleLogin() async {
    if (!_formKey.currentState!.validate()) return;

    final serverUrl = _serverController.text.trim();
    final username = _usernameController.text.trim();
    final password = _passwordController.text;

    try {
      await ref.read(authProvider.notifier).login(
            serverUrl,
            username,
            password,
            rememberMe: _rememberMe,
          );

      if (!mounted) return;

      final authState = ref.read(authProvider);
      if (authState.isAuthenticated) {
        Navigator.pushAndRemoveUntil(
          context,
          MaterialPageRoute(builder: (context) => const HomePage()),
          (route) => false,
        );
      }
    } catch (e) {
      if (!mounted) return;
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(e.toString()),
          backgroundColor: Theme.of(context).colorScheme.error,
        ),
      );
    }
  }

  Future<void> _handleQuickLogin(ServerHistory server, UserHistory user) async {
    if (!mounted) return;

    try {
      // 显示加载指示器
      showDialog(
        context: context,
        barrierDismissible: false,
        builder: (context) => WillPopScope(
          onWillPop: () async => false,
          child: const Center(
            child: CircularProgressIndicator(),
          ),
        ),
      );

      // 执行登录
      await ref.read(authProvider.notifier).login(
            server.serverUrl,
            user.username,
            user.savedPassword!,
            rememberMe: true, // 快速登录时自动记住密码
          );

      if (!mounted) return;

      // 关闭加载指示器
      Navigator.pop(context);

      final authState = ref.read(authProvider);
      if (authState.isAuthenticated) {
        // 导航到主页
        Navigator.pushAndRemoveUntil(
          context,
          MaterialPageRoute(builder: (context) => const HomePage()),
          (route) => false,
        );
      }
    } catch (e) {
      if (!mounted) return;

      // 关闭加载指示器
      Navigator.pop(context);

      // 显示错误消息
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text(e.toString()),
          backgroundColor: Theme.of(context).colorScheme.error,
        ),
      );

      // 清空用户信息，以便用户重新输入
      _clearUserInputs();
    }
  }

  Future<void> _handleDeleteHistory(ServerHistory server, UserHistory user) async {
    final confirmed = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: Text(AppLocalizations.of(context).confirmDelete),
        content: Text(
          AppLocalizations.of(context).confirmDeleteMessage(
            user.username,
            server.serverUrl,
          ),
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context, false),
            child: Text(AppLocalizations.of(context).cancel),
          ),
          TextButton(
            onPressed: () => Navigator.pop(context, true),
            child: Text(AppLocalizations.of(context).confirm),
          ),
        ],
      ),
    );

    if (confirmed == true && mounted) {
      await ref.read(loginHistoryServiceProvider).removeUser(
            server.serverUrl,
            user.username,
          );
      await _loadServers();
    }
  }

  @override
  Widget build(BuildContext context) {
    final l10n = AppLocalizations.of(context);
    final theme = Theme.of(context);
    final authState = ref.watch(authProvider);

    return Scaffold(
      appBar: AppBar(
        title: Text(l10n.loginTitle),
        actions: [
          if (_servers.isNotEmpty)
            IconButton(
              icon: Icon(_isManualInput ? Icons.list : Icons.edit),
              onPressed: _isManualInput ? _switchToServerList : _switchToManualInput,
              tooltip: _isManualInput ? l10n.recentLogins : l10n.manualServerInput,
            ),
        ],
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Form(
              key: _formKey,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  if (_isManualInput)
                    CustomTextField(
                      controller: _serverController,
                      label: l10n.serverAddress,
                      prefixIcon: Icons.dns,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return l10n.required(l10n.serverAddress);
                        }
                        final uri = Uri.tryParse(value);
                        if (uri == null) {
                          return l10n.invalidServerUrl;
                        }
                        return null;
                      },
                    )
                  else
                    DropdownButtonFormField<String>(
                      value: _serverController.text.isEmpty ? null : _serverController.text,
                      decoration: InputDecoration(
                        labelText: l10n.serverAddress,
                        prefixIcon: const Icon(Icons.dns),
                        border: OutlineInputBorder(
                          borderRadius: BorderRadius.circular(8),
                        ),
                        filled: true,
                        fillColor: theme.colorScheme.surface,
                      ),
                      items: _servers.map((server) {
                        return DropdownMenuItem(
                          value: server.serverUrl,
                          child: Text(
                            server.serverName,
                            style: theme.textTheme.bodyLarge,
                          ),
                        );
                      }).toList(),
                      onChanged: _handleServerChange,
                      validator: (value) {
                        if (value == null || value.isEmpty) {
                          return l10n.required(l10n.serverAddress);
                        }
                        return null;
                      },
                      style: theme.textTheme.bodyLarge,
                      icon: const Icon(Icons.arrow_drop_down),
                      isExpanded: true,
                    ),
                  const SizedBox(height: 16.0),
                  CustomTextField(
                    controller: _usernameController,
                    label: l10n.username,
                    prefixIcon: Icons.person,
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return l10n.required(l10n.username);
                      }
                      return null;
                    },
                  ),
                  const SizedBox(height: 16.0),
                  CustomTextField(
                    controller: _passwordController,
                    label: l10n.password,
                    prefixIcon: Icons.lock,
                    obscureText: !_isPasswordVisible,
                    validator: (value) {
                      if (value == null || value.isEmpty) {
                        return l10n.required(l10n.password);
                      }
                      return null;
                    },
                    suffixIcon: IconButton(
                      icon: Icon(
                        _isPasswordVisible ? Icons.visibility_off : Icons.visibility,
                      ),
                      onPressed: () {
                        setState(() {
                          _isPasswordVisible = !_isPasswordVisible;
                        });
                      },
                      tooltip: _isPasswordVisible ? l10n.hidePassword : l10n.showPassword,
                    ),
                  ),
                  const SizedBox(height: 16.0),
                  Row(
                    children: [
                      Checkbox(
                        value: _rememberMe,
                        onChanged: (value) {
                          setState(() {
                            _rememberMe = value ?? false;
                          });
                        },
                      ),
                      Text(l10n.rememberMe),
                    ],
                  ),
                  const SizedBox(height: 16.0),
                  ElevatedButton(
                    onPressed: authState.isLoading ? null : _handleLogin,
                    child: Text(authState.isLoading ? l10n.loginLoading : l10n.login),
                  ),
                ],
              ),
            ),
            if (_servers.isNotEmpty) ...[
              const SizedBox(height: 32.0),
              Text(
                l10n.recentLogins,
                style: theme.textTheme.titleMedium,
              ),
              const SizedBox(height: 8.0),
              ..._servers.map((server) {
                return ExpansionTile(
                  title: Text(server.serverName),
                  subtitle: Text(server.serverUrl),
                  initiallyExpanded: server == _selectedServer,
                  children: server.users.map((user) {
                    final color = Colors.primaries[user.username.hashCode % Colors.primaries.length];
                    return ListTile(
                      leading: CircleAvatar(
                        backgroundColor: color.withOpacity(0.2),
                        child: Text(
                          user.username[0].toUpperCase(),
                          style: TextStyle(
                            color: color,
                            fontWeight: FontWeight.bold,
                          ),
                        ),
                      ),
                      title: Text(user.username),
                      subtitle: Text(
                        l10n.lastLoginTime(
                          user.lastLogin.toLocal().toString(),
                        ),
                      ),
                      trailing: Row(
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          IconButton(
                            icon: const Icon(Icons.delete_outline),
                            onPressed: () => _handleDeleteHistory(server, user),
                            tooltip: l10n.delete,
                          ),
                          if (user.rememberPassword && user.savedPassword != null)
                            IconButton(
                              icon: const Icon(Icons.login),
                              onPressed: () => _handleQuickLogin(server, user),
                              tooltip: l10n.quickLogin,
                            ),
                        ],
                      ),
                      onTap: () {
                        setState(() {
                          _selectedServer = server;
                          _serverController.text = server.serverUrl;
                          _usernameController.text = user.username;
                          _passwordController.clear();
                          _rememberMe = false;
                        });
                      },
                    );
                  }).toList(),
                );
              }).toList(),
            ],
          ],
        ),
      ),
    );
  }
}
