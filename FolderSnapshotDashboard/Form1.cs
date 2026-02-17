using System.ComponentModel;

namespace FolderSnapshotDashboard;

public partial class Form1 : Form
{
    private readonly BindingList<FileSnapshotRow> _rows = new();
    private readonly System.Windows.Forms.Timer _autoRefreshTimer;

    public Form1()
    {
        InitializeComponent();
        ConfigureGrid();

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
            DataPropertyName = nameof(FileSnapshotRow.FileName),
            HeaderText = "File Name",
            MinimumWidth = 250
        });

        snapshotGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FileSnapshotRow.LastUpdated),
            HeaderText = "Last Updated",
            MinimumWidth = 180
        });

        snapshotGridView.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = nameof(FileSnapshotRow.FileSize),
            HeaderText = "File Size",
            MinimumWidth = 120
        });

        snapshotGridView.DataSource = _rows;
    }

    private void BrowseButton_Click(object? sender, EventArgs e)
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
        LoadSnapshot(folderDialog.SelectedPath);
    }

    private void RefreshButton_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(folderPathTextBox.Text) || !Directory.Exists(folderPathTextBox.Text))
        {
            MessageBox.Show(this, "Select a valid folder first.", "Folder Snapshot Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        LoadSnapshot(folderPathTextBox.Text);
    }

    private void AutoRefreshTimer_Tick(object? sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(folderPathTextBox.Text) && Directory.Exists(folderPathTextBox.Text))
        {
            LoadSnapshot(folderPathTextBox.Text);
        }
    }

    private void LoadSnapshot(string folderPath)
    {
        try
        {
            _rows.Clear();

            FileInfo[] files = new DirectoryInfo(folderPath)
                .GetFiles("*", SearchOption.AllDirectories)
                .OrderByDescending(file => file.LastWriteTime)
                .ToArray();

            foreach (FileInfo file in files)
            {
                _rows.Add(new FileSnapshotRow
                {
                    FileName = Path.GetRelativePath(folderPath, file.FullName),
                    LastUpdated = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    FileSize = FormatFileSize(file.Length)
                });
            }

            summaryLabel.Text = $"Files found (including subfolders): {files.Length}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, $"Unable to load snapshot.\n{ex.Message}", "Folder Snapshot Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            summaryLabel.Text = "Error while loading folder snapshot.";
        }
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

        return $"{bytes} B";
    }

    private sealed class FileSnapshotRow
    {
        public required string FileName { get; init; }

        public required string LastUpdated { get; init; }

        public required string FileSize { get; init; }
    }
}
