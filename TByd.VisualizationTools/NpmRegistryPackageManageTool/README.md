# NPM Registry Manager

A Flutter application for managing Verdaccio NPM registry. This application supports both Windows and iOS platforms.

## Features

- Connect to Verdaccio NPM registry with secure authentication
- Package management operations (push, delete, update)
- View detailed package information
- Search and filter packages
- Audit logging
- Multi-language support (English and Chinese)

## Getting Started

### Prerequisites

- Flutter SDK (>=3.1.0)
- Dart SDK (>=3.1.0)
- Windows development environment for Windows build
- macOS with Xcode for iOS build

### Installation

1. Clone the repository
2. Install dependencies:
   ```bash
   flutter pub get
   ```
3. Run the application:
   ```bash
   flutter run
   ```

## Development

### Project Structure

```
lib/
  ├── src/
  │   ├── core/       # Core functionality and utilities
  │   ├── features/   # Feature modules
  │   ├── l10n/       # Localization files
  │   └── shared/     # Shared widgets and components
  └── main.dart       # Application entry point
```

### Building

- For Windows:
  ```bash
  flutter build windows
  ```
- For iOS:
  ```bash
  flutter build ios
  ```

## License

This project is licensed under the MIT License. 