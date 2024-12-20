import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/search_provider.dart';

class SearchBox extends ConsumerWidget {
  final ValueChanged<String>? onSearch;
  final String? hintText;
  final TextEditingController? controller;

  const SearchBox({
    super.key,
    this.onSearch,
    this.hintText,
    this.controller,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final searchState = ref.watch(searchProvider);
    final effectiveController = controller ?? searchState.controller;

    return Container(
      height: 36,
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.surfaceVariant.withOpacity(0.3),
        borderRadius: BorderRadius.circular(8),
      ),
      child: Center(
        child: TextField(
          controller: effectiveController,
          decoration: InputDecoration(
            hintText: hintText ?? '搜索',
            border: InputBorder.none,
            contentPadding: const EdgeInsets.symmetric(horizontal: 12),
            isDense: true,
            prefixIcon: const Icon(Icons.search, size: 20),
            prefixIconConstraints: const BoxConstraints(
              minWidth: 36,
              minHeight: 36,
            ),
            suffixIcon: effectiveController.text.isNotEmpty
                ? IconButton(
                    icon: const Icon(Icons.clear, size: 20),
                    onPressed: () {
                      effectiveController.clear();
                      onSearch?.call('');
                    },
                    padding: EdgeInsets.zero,
                    constraints: const BoxConstraints(
                      minWidth: 36,
                      minHeight: 36,
                    ),
                  )
                : null,
          ),
          style: Theme.of(context).textTheme.bodyMedium,
          textAlignVertical: TextAlignVertical.center,
          onChanged: (value) {
            onSearch?.call(value);
          },
        ),
      ),
    );
  }
}
