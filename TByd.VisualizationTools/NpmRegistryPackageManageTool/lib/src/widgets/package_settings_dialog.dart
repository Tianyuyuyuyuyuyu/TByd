import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

final globalPackageProvider = StateProvider<bool>((ref) => false);
final yarnModernSyntaxProvider = StateProvider<bool>((ref) => false);

class PackageSettingsDialog extends ConsumerWidget {
  final String packageName;

  const PackageSettingsDialog({
    Key? key,
    required this.packageName,
  }) : super(key: key);

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final isGlobalPackage = ref.watch(globalPackageProvider);
    final isYarnModernSyntax = ref.watch(yarnModernSyntaxProvider);

    return Column(
      mainAxisSize: MainAxisSize.min,
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        CheckboxListTile(
          title: const Text('global package'),
          value: isGlobalPackage,
          onChanged: (value) {
            ref.read(globalPackageProvider.notifier).state = value ?? false;
          },
          dense: true,
          controlAffinity: ListTileControlAffinity.leading,
          visualDensity: VisualDensity.compact,
        ),
        CheckboxListTile(
          title: const Text('yarn modern syntax'),
          value: isYarnModernSyntax,
          onChanged: (value) {
            ref.read(yarnModernSyntaxProvider.notifier).state = value ?? false;
          },
          dense: true,
          controlAffinity: ListTileControlAffinity.leading,
          visualDensity: VisualDensity.compact,
        ),
      ],
    );
  }
}
