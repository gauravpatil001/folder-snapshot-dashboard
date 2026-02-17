# Folder Snapshot Dashboard

A Windows desktop application (WinForms, .NET 9) that shows a snapshot of files in a selected folder.

The dashboard displays:
- File name (relative path)
- Last updated timestamp
- File size

It supports:
- Recursive folder scanning (includes subfolders)
- Manual refresh
- Automatic refresh every 10 seconds

## Tech Stack

- .NET 9
- Windows Forms
- C#

## Getting Started

### Prerequisites

- Windows
- .NET 9 SDK

### Run Locally

```powershell
cd FolderSnapshotDashboard
dotnet build
dotnet run
```

## Usage

1. Click `Browse` and select a folder.
2. The snapshot grid loads file details.
3. Click `Refresh` to reload manually.
4. Data also refreshes automatically every 10 seconds.

## Project Structure

- `FolderSnapshotDashboard/Program.cs` - app entry point
- `FolderSnapshotDashboard/Form1.Designer.cs` - dashboard UI layout
- `FolderSnapshotDashboard/Form1.cs` - snapshot loading, formatting, and refresh logic

## Roadmap

- CSV export
- Search and filter
- Configurable refresh interval

## Contributing

Please read `CONTRIBUTING.md` before submitting changes.

## Security

Please report vulnerabilities using `SECURITY.md`.

## License

This project is licensed under the MIT License. See `LICENSE`.
