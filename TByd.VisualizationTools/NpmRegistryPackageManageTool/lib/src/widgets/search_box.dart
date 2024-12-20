import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../providers/search_provider.dart';

class SearchBox extends ConsumerWidget {
  final String? hintText;

  const SearchBox({
    super.key,
    this.hintText,
  });

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final searchText = ref.watch(searchProvider);
    final theme = Theme.of(context);

    return TextField(
      controller: TextEditingController(text: searchText)
        ..selection = TextSelection.fromPosition(
          TextPosition(offset: searchText.length),
        ),
      decoration: InputDecoration(
        hintText: hintText ?? '搜索包',
        prefixIcon: const Icon(Icons.search),
        suffixIcon: searchText.isNotEmpty
            ? IconButton(
                icon: const Icon(Icons.clear),
                onPressed: () {
                  ref.read(searchProvider.notifier).state = '';
                },
              )
            : null,
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(8),
        ),
        filled: true,
        fillColor: theme.colorScheme.surface,
      ),
      onChanged: (value) {
        ref.read(searchProvider.notifier).state = value;
      },
    );
  }
}
