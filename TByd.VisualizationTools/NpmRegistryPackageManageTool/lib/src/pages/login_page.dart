/// NPM Registry Manager - 登录页面
///
/// 该文件实现了用户登录界面，包括：
/// - 服务器选择/输入
/// - 用户认证
/// - 快速登录
/// - 登录历史管理
///
/// 作者: TByd Team
/// 创建日期: 2024-12-14

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/auth_provider.dart';
import '../widgets/custom_text_field.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import '../models/login_history_model.dart';
import 'home_page.dart';

/// 登录页面组件
///
/// 提供用户登录界面，支持：
/// - 手动输入服务器地址
/// - 选择历史登录服务器
/// - 记住登录信息
/// - 快速登录功能
class LoginPage extends ConsumerStatefulWidget {
  const LoginPage({super.key});

  @override
  ConsumerState<LoginPage> createState() => _LoginPageState();
}

/// 登录页面状态类
///
/// 管理登录页面的状态和行为，包括：
/// - 表单验证
/// - 服务器选择
/// - 用户认证
/// - 历史记录管理
class _LoginPageState extends ConsumerState<LoginPage> {
  /// 表单键，用于验证
  final _formKey = GlobalKey<FormState>();

  /// 服务器地址输入控制器
  final _serverController = TextEditingController();

  /// 用户名输入控制器
  final _usernameController = TextEditingController();

  /// 密码输入控制器
  final _passwordController = TextEditingController();

  /// 是否记住登录信息
  bool _rememberMe = false;

  /// 是否显示密码
  bool _isPasswordVisible = false;

  /// 是否使用手动输入模式
  bool _isManualInput = false;

  /// 历史服务器列表
  List<ServerHistory> _servers = [];

  /// 当前选中的服务器
  ServerHistory? _selectedServer;

  @override
  void initState() {
    super.initState();
    _loadServers();
  }

  /// 清除用户输入
  ///
  /// 清空用户名、密码等输入信息
  void _clearUserInputs() {
    _usernameController.clear();
    _passwordController.clear();
    _rememberMe = false;
    _isPasswordVisible = false;
  }

  /// 加载服务器历史记录
  ///
  /// 从存储中读取历史登录的服务器信息
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

  /// 处理服务器变更
  ///
  /// 当用户选择不同的服务器时更新界面
  /// [serverUrl] 新选择的服务器地址
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

  /// 切换到手动输入模式
  ///
  /// 清空所有输入并允许用户手动输入服务器地址
  void _switchToManualInput() {
    setState(() {
      _isManualInput = true;
      _serverController.clear();
      _selectedServer = null;
      _clearUserInputs();
    });
  }

  /// 切换到服务器列表模式
  ///
  /// 显示历史登录的服务器列表供用户选择
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

  /// 处理登录请求
  ///
  /// 验证输入并尝试登录，成功后跳转到主页
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

  /// 处理快速登录
  ///
  /// 使用保存的用户信息快速登录
  /// [server] 服务器信息
  /// [user] 用户信息
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

  /// 处理删除历史记录
  ///
  /// 删除指定服务器上的用户登录历史
  /// [server] 服务器信息
  /// [user] 用户信息
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
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16.0),
        child: Center(
          child: ConstrainedBox(
            constraints: const BoxConstraints(maxWidth: 400),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                Form(
                  key: _formKey,
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                    children: [
                      Text(
                        'TByd Npm',
                        style: theme.textTheme.headlineMedium?.copyWith(
                          fontWeight: FontWeight.bold,
                          color: theme.colorScheme.primary,
                        ),
                        textAlign: TextAlign.center,
                      ),
                      const SizedBox(height: 32.0),
                      if (_isManualInput)
                        CustomTextField(
                          controller: _serverController,
                          label: l10n.serverAddress,
                          prefixIcon: Icons.dns,
                          suffixIcon: _servers.isNotEmpty
                              ? IconButton(
                                  icon: const Icon(Icons.list),
                                  onPressed: _switchToServerList,
                                  tooltip: l10n.recentLogins,
                                )
                              : null,
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
                            suffixIcon: IconButton(
                              icon: const Icon(Icons.edit),
                              onPressed: _switchToManualInput,
                              tooltip: l10n.manualServerInput,
                            ),
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
        ),
      ),
    );
  }
}
