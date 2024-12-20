import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class TerminalLine {
  final String text;
  final Color? color;

  const TerminalLine(this.text, {this.color});
}

class TerminalOutputNotifier extends StateNotifier<List<TerminalLine>> {
  TerminalOutputNotifier()
      : super([
          const TerminalLine('Windows PowerShell'),
          const TerminalLine('版权所有 (C) Microsoft Corporation。保留所有权利。'),
          const TerminalLine(''),
        ]);

  void clear() {
    state = [];
  }

  void addLine(String line, {Color? color}) {
    state = [...state, TerminalLine(line, color: color)];
  }

  void insertLine(int index, String line, {Color? color}) {
    final newList = [...state];
    if (index >= 0 && index <= newList.length) {
      newList.insert(index, TerminalLine(line, color: color));
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
        const TerminalLine('Windows PowerShell'),
        const TerminalLine('版权所有 (C) Microsoft Corporation。保留所有权利。'),
        const TerminalLine(''),
      ];
    }
  }
}

final terminalOutputProvider = StateNotifierProvider<TerminalOutputNotifier, List<TerminalLine>>((ref) {
  return TerminalOutputNotifier();
});
