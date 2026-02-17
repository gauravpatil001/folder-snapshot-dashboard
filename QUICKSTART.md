# Quickstart

## Run (Development)

```powershell
cd FolderSnapshotDashboard
dotnet run
```

## Build (Debug)

```powershell
dotnet build FolderSnapshotDashboard/FolderSnapshotDashboard.csproj
```

## Publish (Release, win-x64)

```powershell
dotnet publish FolderSnapshotDashboard/FolderSnapshotDashboard.csproj -c Release -r win-x64 --self-contained false
```

Published output:

`FolderSnapshotDashboard/bin/Release/net9.0-windows/win-x64/publish`

## Notes

- Auto-refresh runs every 10 seconds.
- Use `Browse` to pick a folder.
- Double-click a row to open a file.
- Right-click a row for file/folder actions.
