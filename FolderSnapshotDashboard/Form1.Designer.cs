namespace FolderSnapshotDashboard;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private TextBox folderPathTextBox;
    private Button browseButton;
    private Button refreshButton;
    private Button cancelScanButton;
    private Label folderLabel;
    private TextBox filterTextBox;
    private Label filterLabel;
    private ComboBox extensionFilterComboBox;
    private Label extensionFilterLabel;
    private ComboBox sortByComboBox;
    private Label sortByLabel;
    private ComboBox sortOrderComboBox;
    private Button clearFiltersButton;
    private SplitContainer mainSplitContainer;
    private DataGridView snapshotGridView;
    private GroupBox statsGroupBox;
    private TableLayoutPanel statsTable;
    private Label totalFilesTitleLabel;
    private Label totalFilesValueLabel;
    private Label totalSizeTitleLabel;
    private Label totalSizeValueLabel;
    private Label largestFileTitleLabel;
    private Label largestFileValueLabel;
    private Label mostRecentTitleLabel;
    private Label mostRecentValueLabel;
    private GroupBox extensionGroupBox;
    private Panel extensionChartPanel;
    private GroupBox historyGroupBox;
    private ListBox historyListBox;
    private GroupBox errorsGroupBox;
    private ListBox errorsListBox;
    private StatusStrip dashboardStatusStrip;
    private ToolStripStatusLabel statusLabel;
    private ToolStripStatusLabel changeSummaryLabel;
    private ToolStripProgressBar scanProgressBar;
    private ContextMenuStrip gridContextMenuStrip;
    private ToolStripMenuItem openFileToolStripMenuItem;
    private ToolStripMenuItem openFolderToolStripMenuItem;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        folderPathTextBox = new TextBox();
        browseButton = new Button();
        refreshButton = new Button();
        cancelScanButton = new Button();
        folderLabel = new Label();
        filterTextBox = new TextBox();
        filterLabel = new Label();
        extensionFilterComboBox = new ComboBox();
        extensionFilterLabel = new Label();
        sortByComboBox = new ComboBox();
        sortByLabel = new Label();
        sortOrderComboBox = new ComboBox();
        clearFiltersButton = new Button();
        mainSplitContainer = new SplitContainer();
        snapshotGridView = new DataGridView();
        gridContextMenuStrip = new ContextMenuStrip(components);
        openFileToolStripMenuItem = new ToolStripMenuItem();
        openFolderToolStripMenuItem = new ToolStripMenuItem();
        statsGroupBox = new GroupBox();
        statsTable = new TableLayoutPanel();
        totalFilesTitleLabel = new Label();
        totalFilesValueLabel = new Label();
        totalSizeTitleLabel = new Label();
        totalSizeValueLabel = new Label();
        largestFileTitleLabel = new Label();
        largestFileValueLabel = new Label();
        mostRecentTitleLabel = new Label();
        mostRecentValueLabel = new Label();
        extensionGroupBox = new GroupBox();
        extensionChartPanel = new Panel();
        historyGroupBox = new GroupBox();
        historyListBox = new ListBox();
        errorsGroupBox = new GroupBox();
        errorsListBox = new ListBox();
        dashboardStatusStrip = new StatusStrip();
        statusLabel = new ToolStripStatusLabel();
        changeSummaryLabel = new ToolStripStatusLabel();
        scanProgressBar = new ToolStripProgressBar();
        ((System.ComponentModel.ISupportInitialize)mainSplitContainer).BeginInit();
        mainSplitContainer.Panel1.SuspendLayout();
        mainSplitContainer.Panel2.SuspendLayout();
        mainSplitContainer.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)snapshotGridView).BeginInit();
        gridContextMenuStrip.SuspendLayout();
        statsGroupBox.SuspendLayout();
        statsTable.SuspendLayout();
        extensionGroupBox.SuspendLayout();
        
        historyGroupBox.SuspendLayout();
        errorsGroupBox.SuspendLayout();
        dashboardStatusStrip.SuspendLayout();
        SuspendLayout();
        // 
        // folderPathTextBox
        // 
        folderPathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        folderPathTextBox.Location = new Point(102, 14);
        folderPathTextBox.Name = "folderPathTextBox";
        folderPathTextBox.ReadOnly = true;
        folderPathTextBox.Size = new Size(573, 23);
        folderPathTextBox.TabIndex = 0;
        // 
        // browseButton
        // 
        browseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        browseButton.Location = new Point(681, 13);
        browseButton.Name = "browseButton";
        browseButton.Size = new Size(88, 25);
        browseButton.TabIndex = 1;
        browseButton.Text = "Browse";
        browseButton.UseVisualStyleBackColor = true;
        browseButton.Click += BrowseButton_Click;
        // 
        // refreshButton
        // 
        refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        refreshButton.Location = new Point(775, 13);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new Size(88, 25);
        refreshButton.TabIndex = 2;
        refreshButton.Text = "Refresh";
        refreshButton.UseVisualStyleBackColor = true;
        refreshButton.Click += RefreshButton_Click;
        // 
        // cancelScanButton
        // 
        cancelScanButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        cancelScanButton.Enabled = false;
        cancelScanButton.Location = new Point(869, 13);
        cancelScanButton.Name = "cancelScanButton";
        cancelScanButton.Size = new Size(88, 25);
        cancelScanButton.TabIndex = 3;
        cancelScanButton.Text = "Cancel";
        cancelScanButton.UseVisualStyleBackColor = true;
        cancelScanButton.Click += CancelScanButton_Click;
        // 
        // folderLabel
        // 
        folderLabel.AutoSize = true;
        folderLabel.Location = new Point(12, 18);
        folderLabel.Name = "folderLabel";
        folderLabel.Size = new Size(79, 15);
        folderLabel.TabIndex = 4;
        folderLabel.Text = "Folder Path:";
        // 
        // filterTextBox
        // 
        filterTextBox.Location = new Point(102, 49);
        filterTextBox.Name = "filterTextBox";
        filterTextBox.Size = new Size(220, 23);
        filterTextBox.TabIndex = 5;
        filterTextBox.TextChanged += FilterControls_Changed;
        // 
        // filterLabel
        // 
        filterLabel.AutoSize = true;
        filterLabel.Location = new Point(12, 53);
        filterLabel.Name = "filterLabel";
        filterLabel.Size = new Size(65, 15);
        filterLabel.TabIndex = 6;
        filterLabel.Text = "File Filter:";
        // 
        // extensionFilterComboBox
        // 
        extensionFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        extensionFilterComboBox.FormattingEnabled = true;
        extensionFilterComboBox.Location = new Point(437, 49);
        extensionFilterComboBox.Name = "extensionFilterComboBox";
        extensionFilterComboBox.Size = new Size(120, 23);
        extensionFilterComboBox.TabIndex = 7;
        extensionFilterComboBox.SelectedIndexChanged += FilterControls_Changed;
        // 
        // extensionFilterLabel
        // 
        extensionFilterLabel.AutoSize = true;
        extensionFilterLabel.Location = new Point(334, 53);
        extensionFilterLabel.Name = "extensionFilterLabel";
        extensionFilterLabel.Size = new Size(95, 15);
        extensionFilterLabel.TabIndex = 8;
        extensionFilterLabel.Text = "File Extension:";
        // 
        // sortByComboBox
        // 
        sortByComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        sortByComboBox.FormattingEnabled = true;
        sortByComboBox.Location = new Point(623, 49);
        sortByComboBox.Name = "sortByComboBox";
        sortByComboBox.Size = new Size(121, 23);
        sortByComboBox.TabIndex = 9;
        sortByComboBox.SelectedIndexChanged += FilterControls_Changed;
        // 
        // sortByLabel
        // 
        sortByLabel.AutoSize = true;
        sortByLabel.Location = new Point(568, 53);
        sortByLabel.Name = "sortByLabel";
        sortByLabel.Size = new Size(49, 15);
        sortByLabel.TabIndex = 10;
        sortByLabel.Text = "Sort By:";
        // 
        // sortOrderComboBox
        // 
        sortOrderComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        sortOrderComboBox.FormattingEnabled = true;
        sortOrderComboBox.Location = new Point(750, 49);
        sortOrderComboBox.Name = "sortOrderComboBox";
        sortOrderComboBox.Size = new Size(100, 23);
        sortOrderComboBox.TabIndex = 11;
        sortOrderComboBox.SelectedIndexChanged += FilterControls_Changed;
        // 
        // clearFiltersButton
        // 
        clearFiltersButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        clearFiltersButton.Location = new Point(856, 48);
        clearFiltersButton.Name = "clearFiltersButton";
        clearFiltersButton.Size = new Size(101, 25);
        clearFiltersButton.TabIndex = 12;
        clearFiltersButton.Text = "Clear Filters";
        clearFiltersButton.UseVisualStyleBackColor = true;
        clearFiltersButton.Click += ClearFiltersButton_Click;
        // 
        // mainSplitContainer
        // 
        mainSplitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        mainSplitContainer.Location = new Point(12, 84);
        mainSplitContainer.Name = "mainSplitContainer";
        // 
        // mainSplitContainer.Panel1
        // 
        mainSplitContainer.Panel1.Controls.Add(snapshotGridView);
        // 
        // mainSplitContainer.Panel2
        // 
        mainSplitContainer.Panel2.Controls.Add(errorsGroupBox);
        mainSplitContainer.Panel2.Controls.Add(historyGroupBox);
        mainSplitContainer.Panel2.Controls.Add(extensionGroupBox);
        mainSplitContainer.Panel2.Controls.Add(statsGroupBox);
        mainSplitContainer.Size = new Size(945, 519);
        mainSplitContainer.SplitterDistance = 620;
        mainSplitContainer.TabIndex = 13;
        // 
        // snapshotGridView
        // 
        snapshotGridView.AllowUserToAddRows = false;
        snapshotGridView.AllowUserToDeleteRows = false;
        snapshotGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        snapshotGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        snapshotGridView.ContextMenuStrip = gridContextMenuStrip;
        snapshotGridView.Dock = DockStyle.Fill;
        snapshotGridView.Location = new Point(0, 0);
        snapshotGridView.MultiSelect = false;
        snapshotGridView.Name = "snapshotGridView";
        snapshotGridView.ReadOnly = true;
        snapshotGridView.RowHeadersVisible = false;
        snapshotGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        snapshotGridView.Size = new Size(620, 519);
        snapshotGridView.TabIndex = 0;
        snapshotGridView.CellDoubleClick += SnapshotGridView_CellDoubleClick;
        // 
        // gridContextMenuStrip
        // 
        gridContextMenuStrip.Items.AddRange(new ToolStripItem[] { openFileToolStripMenuItem, openFolderToolStripMenuItem });
        gridContextMenuStrip.Name = "gridContextMenuStrip";
        gridContextMenuStrip.Size = new Size(227, 48);
        // 
        // openFileToolStripMenuItem
        // 
        openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
        openFileToolStripMenuItem.Size = new Size(226, 22);
        openFileToolStripMenuItem.Text = "Open File";
        openFileToolStripMenuItem.Click += OpenFileToolStripMenuItem_Click;
        // 
        // openFolderToolStripMenuItem
        // 
        openFolderToolStripMenuItem.Name = "openFolderToolStripMenuItem";
        openFolderToolStripMenuItem.Size = new Size(226, 22);
        openFolderToolStripMenuItem.Text = "Open Containing Folder";
        openFolderToolStripMenuItem.Click += OpenFolderToolStripMenuItem_Click;
        // 
        // statsGroupBox
        // 
        statsGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        statsGroupBox.Controls.Add(statsTable);
        statsGroupBox.Location = new Point(3, 3);
        statsGroupBox.Name = "statsGroupBox";
        statsGroupBox.Size = new Size(311, 130);
        statsGroupBox.TabIndex = 0;
        statsGroupBox.TabStop = false;
        statsGroupBox.Text = "Snapshot Stats";
        // 
        // statsTable
        // 
        statsTable.ColumnCount = 2;
        statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
        statsTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 56F));
        statsTable.Controls.Add(totalFilesTitleLabel, 0, 0);
        statsTable.Controls.Add(totalFilesValueLabel, 1, 0);
        statsTable.Controls.Add(totalSizeTitleLabel, 0, 1);
        statsTable.Controls.Add(totalSizeValueLabel, 1, 1);
        statsTable.Controls.Add(largestFileTitleLabel, 0, 2);
        statsTable.Controls.Add(largestFileValueLabel, 1, 2);
        statsTable.Controls.Add(mostRecentTitleLabel, 0, 3);
        statsTable.Controls.Add(mostRecentValueLabel, 1, 3);
        statsTable.Dock = DockStyle.Fill;
        statsTable.Location = new Point(3, 19);
        statsTable.Name = "statsTable";
        statsTable.RowCount = 4;
        statsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        statsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        statsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        statsTable.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        statsTable.Size = new Size(305, 108);
        statsTable.TabIndex = 0;
        // 
        // totalFilesTitleLabel
        // 
        totalFilesTitleLabel.Anchor = AnchorStyles.Left;
        totalFilesTitleLabel.AutoSize = true;
        totalFilesTitleLabel.Location = new Point(3, 6);
        totalFilesTitleLabel.Name = "totalFilesTitleLabel";
        totalFilesTitleLabel.Size = new Size(58, 15);
        totalFilesTitleLabel.TabIndex = 0;
        totalFilesTitleLabel.Text = "Total Files";
        // 
        // totalFilesValueLabel
        // 
        totalFilesValueLabel.Anchor = AnchorStyles.Left;
        totalFilesValueLabel.AutoSize = true;
        totalFilesValueLabel.Location = new Point(137, 6);
        totalFilesValueLabel.Name = "totalFilesValueLabel";
        totalFilesValueLabel.Size = new Size(13, 15);
        totalFilesValueLabel.TabIndex = 1;
        totalFilesValueLabel.Text = "-";
        // 
        // totalSizeTitleLabel
        // 
        totalSizeTitleLabel.Anchor = AnchorStyles.Left;
        totalSizeTitleLabel.AutoSize = true;
        totalSizeTitleLabel.Location = new Point(3, 33);
        totalSizeTitleLabel.Name = "totalSizeTitleLabel";
        totalSizeTitleLabel.Size = new Size(56, 15);
        totalSizeTitleLabel.TabIndex = 2;
        totalSizeTitleLabel.Text = "Total Size";
        // 
        // totalSizeValueLabel
        // 
        totalSizeValueLabel.Anchor = AnchorStyles.Left;
        totalSizeValueLabel.AutoSize = true;
        totalSizeValueLabel.Location = new Point(137, 33);
        totalSizeValueLabel.Name = "totalSizeValueLabel";
        totalSizeValueLabel.Size = new Size(13, 15);
        totalSizeValueLabel.TabIndex = 3;
        totalSizeValueLabel.Text = "-";
        // 
        // largestFileTitleLabel
        // 
        largestFileTitleLabel.Anchor = AnchorStyles.Left;
        largestFileTitleLabel.AutoSize = true;
        largestFileTitleLabel.Location = new Point(3, 60);
        largestFileTitleLabel.Name = "largestFileTitleLabel";
        largestFileTitleLabel.Size = new Size(67, 15);
        largestFileTitleLabel.TabIndex = 4;
        largestFileTitleLabel.Text = "Largest File";
        // 
        // largestFileValueLabel
        // 
        largestFileValueLabel.Anchor = AnchorStyles.Left;
        largestFileValueLabel.AutoSize = true;
        largestFileValueLabel.Location = new Point(137, 60);
        largestFileValueLabel.Name = "largestFileValueLabel";
        largestFileValueLabel.Size = new Size(13, 15);
        largestFileValueLabel.TabIndex = 5;
        largestFileValueLabel.Text = "-";
        // 
        // mostRecentTitleLabel
        // 
        mostRecentTitleLabel.Anchor = AnchorStyles.Left;
        mostRecentTitleLabel.AutoSize = true;
        mostRecentTitleLabel.Location = new Point(3, 87);
        mostRecentTitleLabel.Name = "mostRecentTitleLabel";
        mostRecentTitleLabel.Size = new Size(112, 15);
        mostRecentTitleLabel.TabIndex = 6;
        mostRecentTitleLabel.Text = "Most Recent Change";
        // 
        // mostRecentValueLabel
        // 
        mostRecentValueLabel.Anchor = AnchorStyles.Left;
        mostRecentValueLabel.AutoSize = true;
        mostRecentValueLabel.Location = new Point(137, 87);
        mostRecentValueLabel.Name = "mostRecentValueLabel";
        mostRecentValueLabel.Size = new Size(13, 15);
        mostRecentValueLabel.TabIndex = 7;
        mostRecentValueLabel.Text = "-";
        // 
        // extensionGroupBox
        // 
        extensionGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        extensionGroupBox.Controls.Add(extensionChartPanel);
        extensionGroupBox.Location = new Point(3, 139);
        extensionGroupBox.Name = "extensionGroupBox";
        extensionGroupBox.Size = new Size(311, 160);
        extensionGroupBox.TabIndex = 1;
        extensionGroupBox.TabStop = false;
        extensionGroupBox.Text = "Top Extensions";
        // 
        // extensionChartPanel
        // 
        extensionChartPanel.Dock = DockStyle.Fill;
        extensionChartPanel.Location = new Point(3, 19);
        extensionChartPanel.Name = "extensionChartPanel";
        extensionChartPanel.Size = new Size(305, 138);
        extensionChartPanel.TabIndex = 0;
        extensionChartPanel.BackColor = Color.WhiteSmoke; extensionChartPanel.Paint += ExtensionChartPanel_Paint;
        // 
        // historyGroupBox
        // 
        historyGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        historyGroupBox.Controls.Add(historyListBox);
        historyGroupBox.Location = new Point(3, 305);
        historyGroupBox.Name = "historyGroupBox";
        historyGroupBox.Size = new Size(311, 110);
        historyGroupBox.TabIndex = 2;
        historyGroupBox.TabStop = false;
        historyGroupBox.Text = "Snapshot History";
        // 
        // historyListBox
        // 
        historyListBox.Dock = DockStyle.Fill;
        historyListBox.FormattingEnabled = true;
        historyListBox.ItemHeight = 15;
        historyListBox.Location = new Point(3, 19);
        historyListBox.Name = "historyListBox";
        historyListBox.Size = new Size(305, 88);
        historyListBox.TabIndex = 0;
        historyListBox.SelectedIndexChanged += HistoryListBox_SelectedIndexChanged;
        // 
        // errorsGroupBox
        // 
        errorsGroupBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        errorsGroupBox.Controls.Add(errorsListBox);
        errorsGroupBox.Location = new Point(3, 421);
        errorsGroupBox.Name = "errorsGroupBox";
        errorsGroupBox.Size = new Size(311, 95);
        errorsGroupBox.TabIndex = 3;
        errorsGroupBox.TabStop = false;
        errorsGroupBox.Text = "Scan Issues";
        // 
        // errorsListBox
        // 
        errorsListBox.Dock = DockStyle.Fill;
        errorsListBox.FormattingEnabled = true;
        errorsListBox.HorizontalScrollbar = true;
        errorsListBox.ItemHeight = 15;
        errorsListBox.Location = new Point(3, 19);
        errorsListBox.Name = "errorsListBox";
        errorsListBox.Size = new Size(305, 73);
        errorsListBox.TabIndex = 0;
        // 
        // dashboardStatusStrip
        // 
        dashboardStatusStrip.Items.AddRange(new ToolStripItem[] { statusLabel, changeSummaryLabel, scanProgressBar });
        dashboardStatusStrip.Location = new Point(0, 606);
        dashboardStatusStrip.Name = "dashboardStatusStrip";
        dashboardStatusStrip.Size = new Size(969, 22);
        dashboardStatusStrip.TabIndex = 14;
        dashboardStatusStrip.Text = "statusStrip1";
        // 
        // statusLabel
        // 
        statusLabel.Name = "statusLabel";
        statusLabel.Size = new Size(91, 17);
        statusLabel.Text = "Ready to scan.";
        // 
        // changeSummaryLabel
        // 
        changeSummaryLabel.Name = "changeSummaryLabel";
        changeSummaryLabel.Size = new Size(783, 17);
        changeSummaryLabel.Spring = true;
        changeSummaryLabel.Text = "No snapshot yet.";
        changeSummaryLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // scanProgressBar
        // 
        scanProgressBar.MarqueeAnimationSpeed = 25;
        scanProgressBar.Name = "scanProgressBar";
        scanProgressBar.Size = new Size(80, 16);
        scanProgressBar.Style = ProgressBarStyle.Marquee;
        scanProgressBar.Visible = false;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(969, 628);
        Controls.Add(dashboardStatusStrip);
        Controls.Add(mainSplitContainer);
        Controls.Add(clearFiltersButton);
        Controls.Add(sortOrderComboBox);
        Controls.Add(sortByLabel);
        Controls.Add(sortByComboBox);
        Controls.Add(extensionFilterLabel);
        Controls.Add(extensionFilterComboBox);
        Controls.Add(filterLabel);
        Controls.Add(filterTextBox);
        Controls.Add(folderLabel);
        Controls.Add(cancelScanButton);
        Controls.Add(refreshButton);
        Controls.Add(browseButton);
        Controls.Add(folderPathTextBox);
        MinimumSize = new Size(1000, 660);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Folder Snapshot Dashboard";
        mainSplitContainer.Panel1.ResumeLayout(false);
        mainSplitContainer.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)mainSplitContainer).EndInit();
        mainSplitContainer.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)snapshotGridView).EndInit();
        gridContextMenuStrip.ResumeLayout(false);
        statsGroupBox.ResumeLayout(false);
        statsTable.ResumeLayout(false);
        statsTable.PerformLayout();
        extensionGroupBox.ResumeLayout(false);
        
        historyGroupBox.ResumeLayout(false);
        errorsGroupBox.ResumeLayout(false);
        dashboardStatusStrip.ResumeLayout(false);
        dashboardStatusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}

