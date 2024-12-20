import 'package:flutter_riverpod/flutter_riverpod.dart';

class TerminalOutputNotifier extends StateNotifier<List<String>> {
  TerminalOutputNotifier()
      : super([
          'Windows PowerShell',
          '版权所有 (C) Microsoft Corporation。保留所有权利。',
          '',
        ]);

  void clear() {
    state = [];
  }

  void addLine(String line) {
    state = [...state, line];
  }

  void insertLine(int index, String line) {
    final newList = [...state];
    if (index >= 0 && index <= newList.length) {
      newList.insert(index, line);
      state = newList;
    }
  }

  void removeLast() {
    if (state.isNotEmpty) {
      state = [...state.sublist(0, state.length - 1)];
    }
  }

  void initialize() {
    if (state.isEmpty) {
      state = [
        'Windows PowerShell',
        '版权所有 (C) Microsoft Corporation。保留所有权利。',
        '',
      ];
    }
  }
}

final terminalOutputProvider = StateNotifierProvider<TerminalOutputNotifier, List<String>>((ref) {
  return TerminalOutputNotifier();
});
