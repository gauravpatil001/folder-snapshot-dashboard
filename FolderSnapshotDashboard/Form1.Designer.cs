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
    private DataGridView snapshotGridView;
    private Label folderLabel;
    private Label summaryLabel;

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
        folderPathTextBox = new TextBox();
        browseButton = new Button();
        refreshButton = new Button();
        snapshotGridView = new DataGridView();
        folderLabel = new Label();
        summaryLabel = new Label();
        ((System.ComponentModel.ISupportInitialize)snapshotGridView).BeginInit();
        SuspendLayout();
        // 
        // folderPathTextBox
        // 
        folderPathTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        folderPathTextBox.Location = new Point(105, 17);
        folderPathTextBox.Name = "folderPathTextBox";
        folderPathTextBox.ReadOnly = true;
        folderPathTextBox.Size = new Size(612, 23);
        folderPathTextBox.TabIndex = 0;
        // 
        // browseButton
        // 
        browseButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        browseButton.Location = new Point(723, 16);
        browseButton.Name = "browseButton";
        browseButton.Size = new Size(95, 25);
        browseButton.TabIndex = 1;
        browseButton.Text = "Browse";
        browseButton.UseVisualStyleBackColor = true;
        browseButton.Click += BrowseButton_Click;
        // 
        // refreshButton
        // 
        refreshButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        refreshButton.Location = new Point(824, 16);
        refreshButton.Name = "refreshButton";
        refreshButton.Size = new Size(95, 25);
        refreshButton.TabIndex = 2;
        refreshButton.Text = "Refresh";
        refreshButton.UseVisualStyleBackColor = true;
        refreshButton.Click += RefreshButton_Click;
        // 
        // snapshotGridView
        // 
        snapshotGridView.AllowUserToAddRows = false;
        snapshotGridView.AllowUserToDeleteRows = false;
        snapshotGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        snapshotGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        snapshotGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        snapshotGridView.Location = new Point(20, 58);
        snapshotGridView.MultiSelect = false;
        snapshotGridView.Name = "snapshotGridView";
        snapshotGridView.ReadOnly = true;
        snapshotGridView.RowHeadersVisible = false;
        snapshotGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        snapshotGridView.Size = new Size(899, 451);
        snapshotGridView.TabIndex = 3;
        // 
        // folderLabel
        // 
        folderLabel.AutoSize = true;
        folderLabel.Location = new Point(20, 20);
        folderLabel.Name = "folderLabel";
        folderLabel.Size = new Size(79, 15);
        folderLabel.TabIndex = 4;
        folderLabel.Text = "Folder Path:";
        // 
        // summaryLabel
        // 
        summaryLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        summaryLabel.AutoSize = true;
        summaryLabel.Location = new Point(20, 519);
        summaryLabel.Name = "summaryLabel";
        summaryLabel.Size = new Size(141, 15);
        summaryLabel.TabIndex = 5;
        summaryLabel.Text = "Select a folder to begin.";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(940, 545);
        Controls.Add(summaryLabel);
        Controls.Add(folderLabel);
        Controls.Add(snapshotGridView);
        Controls.Add(refreshButton);
        Controls.Add(browseButton);
        Controls.Add(folderPathTextBox);
        MinimumSize = new Size(700, 400);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Folder Snapshot Dashboard";
        ((System.ComponentModel.ISupportInitialize)snapshotGridView).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion
}
