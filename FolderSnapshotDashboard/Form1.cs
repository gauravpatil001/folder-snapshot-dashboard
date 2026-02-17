using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace FolderSnapshotDashboard;

public partial class Form1 : Form
{
    private readonly BindingList<FileSnapshotRow> _displayRows = new();
    private readonly System.Windows.Forms.Timer _autoRefreshTimer;
    private IReadOnlyList<(string Extension, int Count)> _extensionChartData = Array.Empty<(string Extension, int Count)>();

    private readonly List<SnapshotRecord> _history = new();
    private readonly Dictionary<string, FileState> _previousState = new(StringComparer.OrdinalIgnoreCase);

    private CancellationTokenSource? _scanCancellationTokenSource;
    private bool _suppressFilterEvents;

    public Form1()
    {
        InitializeComponent();
        ConfigureGrid();
        ConfigureChart();
        ConfigureFilters();

        _autoRefreshTimer = new System.Windows.Forms.Timer
        {
            Interval = 10_000
        };
        _autoRefreshTimer.Tick += AutoRefreshTimer_Tick;
        _autoRefreshTimer.Start();
    }

    private void ConfigureGrid()
    {
        snapshotGridView.AutoGenerateColumns = false;
        snapshotGridView.Columns.Clear();

        snapshotGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FileSnapshotRow.ChangeType),
            HeaderText = "Change",
            MinimumWidth = 80,
            FillWeight = 15
        });

        snapshotGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FileSnapshotRow.FileName),
            HeaderText = "File Name",
            MinimumWidth = 250,
            FillWeight = 45
        });

        snapshotGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FileSnapshotRow.Extension),
            HeaderText = "Ext",
            MinimumWidth = 70,
            FillWeight = 10
        });

        snapshotGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FileSnapshotRow.LastUpdated),
            HeaderText = "Last Updated",
            MinimumWidth = 170,
            FillWeight = 20
        });

        snapshotGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FileSnapshotRow.FileSize),
            HeaderText = "File Size",
            MinimumWidth = 110,
            FillWeight = 15
        });

        snapshotGridView.DataSource = _displayRows;
    }

    private void ConfigureChart()
    {
        extensionChartPanel.BackColor = Color.White;
    }

    private void ConfigureFilters()
    {
        _suppressFilterEvents = true;

        sortByComboBox.Items.Clear();
        sortByComboBox.Items.AddRange(["Last Updated", "File Name", "File Size"]);
        sortByComboBox.SelectedIndex = 0;

        sortOrderComboBox.Items.Clear();
        sortOrderComboBox.Items.AddRange(["Descending", "Ascending"]);
        sortOrderComboBox.SelectedIndex = 0;

        extensionFilterComboBox.Items.Clear();
        extensionFilterComboBox.Items.Add("All");
        extensionFilterComboBox.SelectedIndex = 0;

        _suppressFilterEvents = false;
    }

    private async void BrowseButton_Click(object? sender, EventArgs e)
    {
        using FolderBrowserDialog folderDialog = new();

        if (!string.IsNullOrWhiteSpace(folderPathTextBox.Text) && Directory.Exists(folderPathTextBox.Text))
        {
            folderDialog.SelectedPath = folderPathTextBox.Text;
        }

        if (folderDialog.ShowDialog(this) != DialogResult.OK)
        {
            return;
        }

        folderPathTextBox.Text = folderDialog.SelectedPath;
        await RefreshSnapshotAsync("Manual refresh");
    }

    private async void RefreshButton_Click(object? sender, EventArgs e)
    {
        await RefreshSnapshotAsync("Manual refresh");
    }

    private void CancelScanButton_Click(object? sender, EventArgs e)
    {
        _scanCancellationTokenSource?.Cancel();
    }

    private async void AutoRefreshTimer_Tick(object? sender, EventArgs e)
    {
        await RefreshSnapshotAsync("Auto refresh", suppressBusyMessage: true);
    }

    private async Task RefreshSnapshotAsync(string source, bool suppressBusyMessage = false)
    {
        if (string.IsNullOrWhiteSpace(folderPathTextBox.Text) || !Directory.Exists(folderPathTextBox.Text))
        {
            statusLabel.Text = "Select a valid folder to start scanning.";
            return;
        }

        if (_scanCancellationTokenSource is not null)
        {
            if (!suppressBusyMessage)
            {
                statusLabel.Text = "Scan already running.";
            }
            return;
        }

        string folderPath = folderPathTextBox.Text;
        _scanCancellationTokenSource = new CancellationTokenSource();
        SetBusyState(true, source);

        try
        {
            ScanProgressInfo progressInfo = new();
            IProgress<ScanProgressInfo> progress = new Progress<ScanProgressInfo>(info =>
            {
                progressInfo = info;
                statusLabel.Text = $"Scanning... Files: {info.FilesScanned:N0}, Folders: {info.FoldersScanned:N0}";
            });

            ScanResult scanResult = await Task.Run(() => ScanFolder(folderPath, _scanCancellationTokenSource.Token, progress));

            SnapshotDelta delta = BuildDelta(scanResult.Items);
            SnapshotRecord record = new(
                CapturedAt: DateTime.Now,
                Source: source,
                Items: scanResult.Items,
                Errors: scanResult.Errors,
                Delta: delta,
                ScannedFiles: progressInfo.FilesScanned,
                ScannedFolders: progressInfo.FoldersScanned);

            _history.Insert(0, record);
            UpdateHistoryList();
            historyListBox.SelectedIndex = 0;
            ApplyFiltersAndRender(record);

            statusLabel.Text = $"Last scan: {record.CapturedAt:yyyy-MM-dd HH:mm:ss} ({record.Items.Count:N0} files)";
        }
        catch (OperationCanceledException)
        {
            statusLabel.Text = "Scan canceled.";
        }
        catch (Exception ex)
        {
            statusLabel.Text = "Scan failed.";
            MessageBox.Show(this, $"Unable to scan folder.\n{ex.Message}", "Folder Snapshot Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _scanCancellationTokenSource?.Dispose();
            _scanCancellationTokenSource = null;
            SetBusyState(false, string.Empty);
        }
    }

    private static ScanResult ScanFolder(string rootPath, CancellationToken token, IProgress<ScanProgressInfo> progress)
    {
        List<FileSnapshotItem> items = new();
        List<string> errors = new();
        Stack<string> pendingFolders = new();
        pendingFolders.Push(rootPath);

        long filesScanned = 0;
        long foldersScanned = 0;

        while (pendingFolders.Count > 0)
        {
            token.ThrowIfCancellationRequested();
            string currentFolder = pendingFolders.Pop();
            foldersScanned++;

            try
            {
                foreach (string folder in Directory.EnumerateDirectories(currentFolder))
                {
                    pendingFolders.Push(folder);
                }
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException or IOException)
            {
                errors.Add($"Directory skipped: {currentFolder} ({ex.Message})");
                progress.Report(new ScanProgressInfo { FilesScanned = filesScanned, FoldersScanned = foldersScanned });
                continue;
            }

            IEnumerable<string> files;
            try
            {
                files = Directory.EnumerateFiles(currentFolder);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException or IOException)
            {
                errors.Add($"Files skipped in: {currentFolder} ({ex.Message})");
                progress.Report(new ScanProgressInfo { FilesScanned = filesScanned, FoldersScanned = foldersScanned });
                continue;
            }

            foreach (string filePath in files)
            {
                token.ThrowIfCancellationRequested();

                try
                {
                    FileInfo fileInfo = new(filePath);
                    string relativePath = Path.GetRelativePath(rootPath, fileInfo.FullName);
                    string extension = string.IsNullOrWhiteSpace(fileInfo.Extension) ? "(none)" : fileInfo.Extension.ToLowerInvariant();

                    items.Add(new FileSnapshotItem(
                        RelativePath: relativePath,
                        FullPath: fileInfo.FullName,
                        SizeBytes: fileInfo.Length,
                        LastWriteTimeUtc: fileInfo.LastWriteTimeUtc,
                        Extension: extension));

                    filesScanned++;
                    if (filesScanned % 100 == 0)
                    {
                        progress.Report(new ScanProgressInfo { FilesScanned = filesScanned, FoldersScanned = foldersScanned });
                    }
                }
                catch (Exception ex) when (ex is UnauthorizedAccessException or IOException)
                {
                    errors.Add($"File skipped: {filePath} ({ex.Message})");
                }
            }

            progress.Report(new ScanProgressInfo { FilesScanned = filesScanned, FoldersScanned = foldersScanned });
        }

        return new ScanResult(items, errors);
    }

    private SnapshotDelta BuildDelta(IReadOnlyList<FileSnapshotItem> newItems)
    {
        Dictionary<string, FileState> newState = new(StringComparer.OrdinalIgnoreCase);
        int newCount = 0;
        int modifiedCount = 0;

        foreach (FileSnapshotItem item in newItems)
        {
            FileState current = new(item.SizeBytes, item.LastWriteTimeUtc);
            newState[item.RelativePath] = current;

            if (!_previousState.TryGetValue(item.RelativePath, out FileState? previous) || previous is null)
            {
                newCount++;
            }
            else if (!previous.Equals(current))
            {
                modifiedCount++;
            }
        }

        int deletedCount = _previousState.Keys.Count(path => !newState.ContainsKey(path));

        _previousState.Clear();
        foreach ((string path, FileState state) in newState)
        {
            _previousState[path] = state;
        }

        return new SnapshotDelta(newCount, modifiedCount, deletedCount);
    }

    private void ApplyFiltersAndRender(SnapshotRecord record)
    {
        IEnumerable<FileSnapshotItem> filtered = record.Items;

        string searchText = filterTextBox.Text.Trim();
        if (!string.IsNullOrWhiteSpace(searchText))
        {
            filtered = filtered.Where(item => item.RelativePath.Contains(searchText, StringComparison.OrdinalIgnoreCase));
        }

        string selectedExtension = extensionFilterComboBox.SelectedItem?.ToString() ?? "All";
        if (!string.Equals(selectedExtension, "All", StringComparison.OrdinalIgnoreCase))
        {
            filtered = filtered.Where(item => string.Equals(item.Extension, selectedExtension, StringComparison.OrdinalIgnoreCase));
        }

        string sortBy = sortByComboBox.SelectedItem?.ToString() ?? "Last Updated";
        bool ascending = string.Equals(sortOrderComboBox.SelectedItem?.ToString(), "Ascending", StringComparison.Ordinal);

        IOrderedEnumerable<FileSnapshotItem> ordered = sortBy switch
        {
            "File Name" => ascending
                ? filtered.OrderBy(item => item.RelativePath, StringComparer.OrdinalIgnoreCase)
                : filtered.OrderByDescending(item => item.RelativePath, StringComparer.OrdinalIgnoreCase),
            "File Size" => ascending
                ? filtered.OrderBy(item => item.SizeBytes)
                : filtered.OrderByDescending(item => item.SizeBytes),
            _ => ascending
                ? filtered.OrderBy(item => item.LastWriteTimeUtc)
                : filtered.OrderByDescending(item => item.LastWriteTimeUtc)
        };

        List<FileSnapshotItem> visibleItems = ordered.ToList();
        HashSet<string> newFiles = GetNewFilePaths(record);
        HashSet<string> modifiedFiles = GetModifiedFilePaths(record);

        _displayRows.RaiseListChangedEvents = false;
        _displayRows.Clear();

        foreach (FileSnapshotItem item in visibleItems)
        {
            string changeType = newFiles.Contains(item.RelativePath)
                ? "New"
                : modifiedFiles.Contains(item.RelativePath)
                    ? "Modified"
                    : "Unchanged";

            _displayRows.Add(new FileSnapshotRow
            {
                ChangeType = changeType,
                FileName = item.RelativePath,
                Extension = item.Extension,
                LastUpdated = item.LastWriteTimeUtc.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                FileSize = FormatFileSize(item.SizeBytes),
                FullPath = item.FullPath
            });
        }

        _displayRows.RaiseListChangedEvents = true;
        _displayRows.ResetBindings();

        RenderStats(record, visibleItems);
        RenderExtensionChart(visibleItems);
        RenderErrors(record.Errors);

        changeSummaryLabel.Text = $"Delta: +{record.Delta.NewCount} new, {record.Delta.ModifiedCount} modified, -{record.Delta.DeletedCount} deleted";
    }

    private HashSet<string> GetNewFilePaths(SnapshotRecord record)
    {
        if (_history.Count < 2 || !ReferenceEquals(record, _history[0]))
        {
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        HashSet<string> previous = _history[1].Items.Select(item => item.RelativePath).ToHashSet(StringComparer.OrdinalIgnoreCase);
        return record.Items
            .Where(item => !previous.Contains(item.RelativePath))
            .Select(item => item.RelativePath)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);
    }

    private HashSet<string> GetModifiedFilePaths(SnapshotRecord record)
    {
        if (_history.Count < 2 || !ReferenceEquals(record, _history[0]))
        {
            return new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        Dictionary<string, FileState> previous = _history[1].Items
            .ToDictionary(item => item.RelativePath, item => new FileState(item.SizeBytes, item.LastWriteTimeUtc), StringComparer.OrdinalIgnoreCase);

        HashSet<string> modified = new(StringComparer.OrdinalIgnoreCase);
        foreach (FileSnapshotItem item in record.Items)
        {
            if (previous.TryGetValue(item.RelativePath, out FileState? oldState) && oldState is not null)
            {
                FileState currentState = new(item.SizeBytes, item.LastWriteTimeUtc);
                if (!oldState.Equals(currentState))
                {
                    modified.Add(item.RelativePath);
                }
            }
        }

        return modified;
    }

    private void RenderStats(SnapshotRecord record, IReadOnlyList<FileSnapshotItem> visibleItems)
    {
        long totalBytes = visibleItems.Sum(item => item.SizeBytes);
        FileSnapshotItem? largest = visibleItems.OrderByDescending(item => item.SizeBytes).FirstOrDefault();
        FileSnapshotItem? latest = visibleItems.OrderByDescending(item => item.LastWriteTimeUtc).FirstOrDefault();

        totalFilesValueLabel.Text = visibleItems.Count.ToString("N0", CultureInfo.InvariantCulture);
        totalSizeValueLabel.Text = FormatFileSize(totalBytes);
        largestFileValueLabel.Text = largest is null
            ? "-"
            : $"{largest.RelativePath} ({FormatFileSize(largest.SizeBytes)})";
        mostRecentValueLabel.Text = latest is null
            ? "-"
            : $"{latest.RelativePath} ({latest.LastWriteTimeUtc.ToLocalTime():yyyy-MM-dd HH:mm:ss})";

        historyGroupBox.Text = $"Snapshot History ({_history.Count})";
        errorsGroupBox.Text = $"Scan Issues ({record.Errors.Count})";
    }

    private void RenderExtensionChart(IReadOnlyList<FileSnapshotItem> visibleItems)
    {
        _extensionChartData = visibleItems
            .GroupBy(item => item.Extension)
            .Select(group => (Extension: group.Key, Count: group.Count()))
            .OrderByDescending(item => item.Count)
            .ThenBy(item => item.Extension, StringComparer.OrdinalIgnoreCase)
            .Take(8)
            .ToList();

        extensionChartPanel.Invalidate();
    }

    private void ExtensionChartPanel_Paint(object? sender, PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        graphics.Clear(Color.White);

        if (_extensionChartData.Count == 0)
        {
            using Font font = new("Segoe UI", 9F);
            using SolidBrush brush = new(Color.DimGray);
            graphics.DrawString("No extension data", font, brush, new PointF(10, 10));
            return;
        }

        int width = extensionChartPanel.ClientSize.Width;
        int height = extensionChartPanel.ClientSize.Height;
        int marginLeft = 70;
        int marginTop = 14;
        int marginBottom = 18;
        int barAreaHeight = Math.Max(1, height - marginTop - marginBottom);
        int rowHeight = Math.Max(14, barAreaHeight / _extensionChartData.Count);
        int maxCount = Math.Max(1, _extensionChartData.Max(x => x.Count));
        int barAreaWidth = Math.Max(1, width - marginLeft - 14);

        using Font labelFont = new("Segoe UI", 8F);
        using SolidBrush textBrush = new(Color.Black);
        using SolidBrush barBrush = new(Color.FromArgb(66, 133, 244));

        for (int i = 0; i < _extensionChartData.Count; i++)
        {
            (string extension, int count) = _extensionChartData[i];
            int y = marginTop + i * rowHeight;
            int barHeight = Math.Max(8, rowHeight - 4);
            int barWidth = (int)Math.Round((double)count / maxCount * barAreaWidth);

            graphics.FillRectangle(barBrush, marginLeft, y, barWidth, barHeight);
            graphics.DrawString(extension, labelFont, textBrush, new PointF(6, y));
            graphics.DrawString(count.ToString("N0", CultureInfo.InvariantCulture), labelFont, textBrush, new PointF(marginLeft + barWidth + 4, y));
        }
    }

    private void RenderErrors(IReadOnlyList<string> errors)
    {
        errorsListBox.BeginUpdate();
        errorsListBox.Items.Clear();

        if (errors.Count == 0)
        {
            errorsListBox.Items.Add("No scan issues detected.");
        }
        else
        {
            foreach (string error in errors.Take(250))
            {
                errorsListBox.Items.Add(error);
            }

            if (errors.Count > 250)
            {
                errorsListBox.Items.Add($"... {errors.Count - 250} additional issues not shown.");
            }
        }

        errorsListBox.EndUpdate();
    }

    private void UpdateHistoryList()
    {
        historyListBox.BeginUpdate();
        historyListBox.Items.Clear();

        foreach (SnapshotRecord record in _history)
        {
            historyListBox.Items.Add(record);
        }

        historyListBox.EndUpdate();
    }

    private void SetBusyState(bool isBusy, string source)
    {
        cancelScanButton.Enabled = isBusy;
        refreshButton.Enabled = !isBusy;
        browseButton.Enabled = !isBusy;
        scanProgressBar.Visible = isBusy;

        if (isBusy)
        {
            statusLabel.Text = $"Starting {source.ToLowerInvariant()}...";
        }
    }

    private void ClearFiltersButton_Click(object? sender, EventArgs e)
    {
        _suppressFilterEvents = true;
        filterTextBox.Text = string.Empty;
        extensionFilterComboBox.SelectedIndex = 0;
        sortByComboBox.SelectedIndex = 0;
        sortOrderComboBox.SelectedIndex = 0;
        _suppressFilterEvents = false;

        RefreshCurrentView();
    }

    private void FilterControls_Changed(object? sender, EventArgs e)
    {
        if (_suppressFilterEvents)
        {
            return;
        }

        RefreshCurrentView();
    }

    private void RefreshCurrentView()
    {
        SnapshotRecord? selectedRecord = historyListBox.SelectedItem as SnapshotRecord;
        if (selectedRecord is null)
        {
            return;
        }

        ApplyFiltersAndRender(selectedRecord);
    }

    private void HistoryListBox_SelectedIndexChanged(object? sender, EventArgs e)
    {
        SnapshotRecord? selectedRecord = historyListBox.SelectedItem as SnapshotRecord;
        if (selectedRecord is null)
        {
            return;
        }

        UpdateExtensionFilterOptions(selectedRecord.Items);
        ApplyFiltersAndRender(selectedRecord);
    }

    private void UpdateExtensionFilterOptions(IReadOnlyList<FileSnapshotItem> items)
    {
        string? previousSelection = extensionFilterComboBox.SelectedItem?.ToString();

        string[] values = items
            .Select(item => item.Extension)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(ext => ext, StringComparer.OrdinalIgnoreCase)
            .ToArray();

        _suppressFilterEvents = true;
        extensionFilterComboBox.Items.Clear();
        extensionFilterComboBox.Items.Add("All");
        extensionFilterComboBox.Items.AddRange(values.Cast<object>().ToArray());

        if (!string.IsNullOrWhiteSpace(previousSelection) && extensionFilterComboBox.Items.Contains(previousSelection))
        {
            extensionFilterComboBox.SelectedItem = previousSelection;
        }
        else
        {
            extensionFilterComboBox.SelectedIndex = 0;
        }

        _suppressFilterEvents = false;
    }

    private void SnapshotGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        OpenSelectedFile();
    }

    private void OpenFileToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        OpenSelectedFile();
    }

    private void OpenFolderToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        FileSnapshotRow? row = GetSelectedRow();
        if (row is null)
        {
            return;
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "explorer.exe",
                Arguments = $"/select,\"{row.FullPath}\"",
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Unable to open containing folder.\n{ex.Message}", "Folder Snapshot Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void OpenSelectedFile()
    {
        FileSnapshotRow? row = GetSelectedRow();
        if (row is null)
        {
            return;
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = row.FullPath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Unable to open file.\n{ex.Message}", "Folder Snapshot Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private FileSnapshotRow? GetSelectedRow()
    {
        if (snapshotGridView.CurrentRow?.DataBoundItem is FileSnapshotRow row)
        {
            return row;
        }

        return null;
    }

    private static string FormatFileSize(long bytes)
    {
        const double kilo = 1024;
        const double mega = kilo * 1024;
        const double giga = mega * 1024;

        if (bytes >= giga)
        {
            return $"{bytes / giga:0.##} GB";
        }

        if (bytes >= mega)
        {
            return $"{bytes / mega:0.##} MB";
        }

        if (bytes >= kilo)
        {
            return $"{bytes / kilo:0.##} KB";
        }

        return $"{bytes:N0} B";
    }

    private sealed class FileSnapshotRow
    {
        public required string ChangeType { get; init; }

        public required string FileName { get; init; }

        public required string Extension { get; init; }

        public required string LastUpdated { get; init; }

        public required string FileSize { get; init; }

        public required string FullPath { get; init; }
    }

    private sealed class ScanProgressInfo
    {
        public long FilesScanned { get; init; }

        public long FoldersScanned { get; init; }
    }

    private sealed record FileSnapshotItem(
        string RelativePath,
        string FullPath,
        long SizeBytes,
        DateTime LastWriteTimeUtc,
        string Extension);

    private sealed record FileState(long SizeBytes, DateTime LastWriteTimeUtc);

    private sealed record SnapshotDelta(int NewCount, int ModifiedCount, int DeletedCount);

    private sealed record SnapshotRecord(
        DateTime CapturedAt,
        string Source,
        IReadOnlyList<FileSnapshotItem> Items,
        IReadOnlyList<string> Errors,
        SnapshotDelta Delta,
        long ScannedFiles,
        long ScannedFolders)
    {
        public override string ToString()
        {
            return $"{CapturedAt:HH:mm:ss} [{Source}] F:{ScannedFiles:N0} +{Delta.NewCount}/~{Delta.ModifiedCount}/-{Delta.DeletedCount}";
        }
    }

    private sealed record ScanResult(IReadOnlyList<FileSnapshotItem> Items, IReadOnlyList<string> Errors);
}
