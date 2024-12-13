import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../services/encryption_service.dart';
import 'storage_provider.dart';

final encryptionServiceProvider = Provider<EncryptionService>((ref) {
  final storage = ref.watch(storageServiceProvider);
  return EncryptionService(storage);
});
