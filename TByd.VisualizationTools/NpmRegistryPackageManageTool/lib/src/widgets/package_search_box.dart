import 'package:flutter/material.dart';

class PackageSearchBox extends StatefulWidget {
  final void Function(String) onSearch;
  final String? hintText;

  const PackageSearchBox({
    super.key,
    required this.onSearch,
    this.hintText,
  });

  @override
  State<PackageSearchBox> createState() => _PackageSearchBoxState();
}

class _PackageSearchBoxState extends State<PackageSearchBox> {
  final TextEditingController _controller = TextEditingController();

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 36,
      decoration: BoxDecoration(
        color: Colors.grey[200],
        borderRadius: BorderRadius.circular(4),
      ),
      child: TextField(
        controller: _controller,
        onChanged: widget.onSearch,
        decoration: InputDecoration(
          hintText: widget.hintText ?? '搜索',
          border: InputBorder.none,
          contentPadding: const EdgeInsets.symmetric(
            horizontal: 12,
            vertical: 8,
          ),
          prefixIcon: const Icon(
            Icons.search,
            size: 20,
            color: Colors.grey,
          ),
          suffixIcon: _controller.text.isNotEmpty
              ? IconButton(
                  icon: const Icon(
                    Icons.clear,
                    size: 20,
                    color: Colors.grey,
                  ),
                  onPressed: () {
                    _controller.clear();
                    widget.onSearch('');
                  },
                )
              : null,
        ),
      ),
    );
  }
}
