import 'package:flutter/material.dart';

class PackageSettingsButton extends StatelessWidget {
  final VoidCallback onPressed;

  const PackageSettingsButton({
    Key? key,
    required this.onPressed,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return IconButton(
      icon: const Icon(Icons.settings),
      onPressed: onPressed,
      tooltip: 'Settings',
      constraints: const BoxConstraints(),
      padding: const EdgeInsets.all(8.0),
      iconSize: 20.0,
    );
  }
}
