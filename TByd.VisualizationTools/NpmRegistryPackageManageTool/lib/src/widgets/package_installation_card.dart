import 'package:flutter/material.dart';
import 'package_settings_button.dart';
import 'package_settings_dialog.dart';
import 'package_installation_command.dart';

class PackageInstallationCard extends StatelessWidget {
  final String packageName;
  final String version;

  const PackageInstallationCard({
    Key? key,
    required this.packageName,
    required this.version,
  }) : super(key: key);

  void _showSettingsDialog(BuildContext context) {
    showDialog(
      context: context,
      builder: (context) => PackageSettingsDialog(
        packageName: packageName,
      ),
    );
  }

  @override
  Widget build(BuildContext context) {
    final String fullPackageName = '$packageName@$version';

    return Card(
      margin: const EdgeInsets.all(8.0),
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            PackageInstallationCommand(
              packageManager: 'npm',
              command: 'npm install $fullPackageName',
              icon: Image.asset(
                'assets/images/npm-logo.png',
                width: 24,
                height: 24,
              ),
              onSettingsPressed: () => _showSettingsDialog(context),
            ),
            const SizedBox(height: 8.0),
            PackageInstallationCommand(
              packageManager: 'yarn',
              command: 'yarn add $fullPackageName',
              icon: Image.asset(
                'assets/images/yarn-logo.png',
                width: 24,
                height: 24,
              ),
              onSettingsPressed: () => _showSettingsDialog(context),
            ),
            const SizedBox(height: 8.0),
            PackageInstallationCommand(
              packageManager: 'pnpm',
              command: 'pnpm install $fullPackageName',
              icon: Image.asset(
                'assets/images/pnpm-logo.png',
                width: 24,
                height: 24,
              ),
              onSettingsPressed: () => _showSettingsDialog(context),
            ),
          ],
        ),
      ),
    );
  }
}
