import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package_settings_button.dart';

class PackageInstallationCommand extends StatelessWidget {
  final String command;
  final String packageManager;
  final Widget icon;
  final VoidCallback onSettingsPressed;

  const PackageInstallationCommand({
    Key? key,
    required this.command,
    required this.packageManager,
    required this.icon,
    required this.onSettingsPressed,
  }) : super(key: key);

  void _copyToClipboard(BuildContext context) {
    Clipboard.setData(ClipboardData(text: command));
    ScaffoldMessenger.of(context).showSnackBar(
      SnackBar(
        content: Text('$packageManager command copied to clipboard'),
        duration: const Duration(seconds: 2),
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(vertical: 8.0, horizontal: 16.0),
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.surface,
        borderRadius: BorderRadius.circular(4.0),
      ),
      child: Row(
        children: [
          icon,
          const SizedBox(width: 12.0),
          Expanded(
            child: Text(
              command,
              style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                    fontFamily: 'Monospace',
                  ),
            ),
          ),
          IconButton(
            icon: const Icon(Icons.copy),
            onPressed: () => _copyToClipboard(context),
            tooltip: 'Copy to clipboard',
            constraints: const BoxConstraints(),
            padding: const EdgeInsets.all(8.0),
            iconSize: 20.0,
          ),
          PackageSettingsButton(
            onPressed: onSettingsPressed,
          ),
        ],
      ),
    );
  }
}
