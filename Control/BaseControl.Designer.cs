namespace BulkTestUploader.Control
{
    partial class BaseControl
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            inputTableLayoutPanel = new TableLayoutPanel();
            patTokenLabel = new Label();
            orgNameLabel = new Label();
            projectLabel = new Label();
            planLabel = new Label();
            suiteLabel = new Label();
            patTokenMaskedTextBox = new MaskedTextBox();
            orgNameTextBox = new TextBox();
            projectComboBox = new ComboBox();
            planComboBox = new ComboBox();
            suiteButton = new Button();
            validateButton = new Button();
            suitesGrid = new DataGridView();
            SuiteId = new DataGridViewTextBoxColumn();
            SuiteName = new DataGridViewTextBoxColumn();
            SuitePath = new DataGridViewTextBoxColumn();
            FilePath = new DataGridViewTextBoxColumn();
            action = new DataGridViewButtonColumn();
            groupBox1 = new GroupBox();
            logsTextBox = new RichTextBox();
            actionControls = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            toolStripSeparator1 = new ToolStripSeparator();
            exportTemplateButton = new ToolStripButton();
            importTemplateButton = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            uploadTestCasesButton = new ToolStripButton();
            clearLogsButton = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            cancelButton = new ToolStripButton();
            statusBarStrip = new StatusStrip();
            countStatusLabel = new ToolStripStatusLabel();
            timeStatusLabel = new ToolStripStatusLabel();
            progressStatusBar = new ToolStripProgressBar();
            timerControl = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            inputTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)suitesGrid).BeginInit();
            groupBox1.SuspendLayout();
            actionControls.SuspendLayout();
            statusBarStrip.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 49);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1.Padding = new Padding(10);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(groupBox1);
            splitContainer1.Panel2.Padding = new Padding(10);
            splitContainer1.Size = new Size(1303, 626);
            splitContainer1.SplitterDistance = 956;
            splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel1;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(10, 10);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(inputTableLayoutPanel);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(suitesGrid);
            splitContainer2.Size = new Size(932, 602);
            splitContainer2.SplitterDistance = 253;
            splitContainer2.TabIndex = 0;
            // 
            // inputTableLayoutPanel
            // 
            inputTableLayoutPanel.ColumnCount = 3;
            inputTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            inputTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            inputTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            inputTableLayoutPanel.Controls.Add(patTokenLabel, 0, 0);
            inputTableLayoutPanel.Controls.Add(orgNameLabel, 0, 1);
            inputTableLayoutPanel.Controls.Add(projectLabel, 0, 2);
            inputTableLayoutPanel.Controls.Add(planLabel, 0, 3);
            inputTableLayoutPanel.Controls.Add(suiteLabel, 0, 4);
            inputTableLayoutPanel.Controls.Add(patTokenMaskedTextBox, 1, 0);
            inputTableLayoutPanel.Controls.Add(orgNameTextBox, 1, 1);
            inputTableLayoutPanel.Controls.Add(projectComboBox, 1, 2);
            inputTableLayoutPanel.Controls.Add(planComboBox, 1, 3);
            inputTableLayoutPanel.Controls.Add(suiteButton, 1, 4);
            inputTableLayoutPanel.Controls.Add(validateButton, 2, 1);
            inputTableLayoutPanel.Dock = DockStyle.Fill;
            inputTableLayoutPanel.Location = new Point(0, 0);
            inputTableLayoutPanel.Name = "inputTableLayoutPanel";
            inputTableLayoutPanel.RowCount = 5;
            inputTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            inputTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            inputTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            inputTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            inputTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            inputTableLayoutPanel.Size = new Size(932, 253);
            inputTableLayoutPanel.TabIndex = 0;
            // 
            // patTokenLabel
            // 
            patTokenLabel.AutoSize = true;
            patTokenLabel.Dock = DockStyle.Fill;
            patTokenLabel.Location = new Point(3, 0);
            patTokenLabel.Name = "patTokenLabel";
            patTokenLabel.Size = new Size(166, 50);
            patTokenLabel.TabIndex = 0;
            patTokenLabel.Text = "PAT Token";
            // 
            // orgNameLabel
            // 
            orgNameLabel.AutoSize = true;
            orgNameLabel.Dock = DockStyle.Fill;
            orgNameLabel.Location = new Point(3, 50);
            orgNameLabel.Name = "orgNameLabel";
            orgNameLabel.Size = new Size(166, 50);
            orgNameLabel.TabIndex = 1;
            orgNameLabel.Text = "Organization Name";
            // 
            // projectLabel
            // 
            projectLabel.AutoSize = true;
            projectLabel.Dock = DockStyle.Fill;
            projectLabel.Location = new Point(3, 100);
            projectLabel.Name = "projectLabel";
            projectLabel.Size = new Size(166, 50);
            projectLabel.TabIndex = 2;
            projectLabel.Text = "Select Project";
            // 
            // planLabel
            // 
            planLabel.AutoSize = true;
            planLabel.Dock = DockStyle.Fill;
            planLabel.Location = new Point(3, 150);
            planLabel.Name = "planLabel";
            planLabel.Size = new Size(166, 50);
            planLabel.TabIndex = 3;
            planLabel.Text = "Select Test Plan";
            // 
            // suiteLabel
            // 
            suiteLabel.AutoSize = true;
            suiteLabel.Dock = DockStyle.Fill;
            suiteLabel.Location = new Point(3, 200);
            suiteLabel.Name = "suiteLabel";
            suiteLabel.Size = new Size(166, 53);
            suiteLabel.TabIndex = 4;
            suiteLabel.Text = "Select Test Suite(s)";
            // 
            // patTokenMaskedTextBox
            // 
            inputTableLayoutPanel.SetColumnSpan(patTokenMaskedTextBox, 2);
            patTokenMaskedTextBox.CutCopyMaskFormat = MaskFormat.IncludePromptAndLiterals;
            patTokenMaskedTextBox.Dock = DockStyle.Fill;
            patTokenMaskedTextBox.Location = new Point(175, 3);
            patTokenMaskedTextBox.Name = "patTokenMaskedTextBox";
            patTokenMaskedTextBox.PasswordChar = '*';
            patTokenMaskedTextBox.Size = new Size(754, 31);
            patTokenMaskedTextBox.TabIndex = 5;
            patTokenMaskedTextBox.UseSystemPasswordChar = true;
            // 
            // orgNameTextBox
            // 
            orgNameTextBox.Dock = DockStyle.Fill;
            orgNameTextBox.Location = new Point(175, 53);
            orgNameTextBox.Name = "orgNameTextBox";
            orgNameTextBox.Size = new Size(648, 31);
            orgNameTextBox.TabIndex = 6;
            // 
            // projectComboBox
            // 
            inputTableLayoutPanel.SetColumnSpan(projectComboBox, 2);
            projectComboBox.Dock = DockStyle.Fill;
            projectComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            projectComboBox.Location = new Point(175, 103);
            projectComboBox.Name = "projectComboBox";
            projectComboBox.Size = new Size(754, 33);
            projectComboBox.Sorted = true;
            projectComboBox.TabIndex = 7;
            // 
            // planComboBox
            // 
            inputTableLayoutPanel.SetColumnSpan(planComboBox, 2);
            planComboBox.Dock = DockStyle.Fill;
            planComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            planComboBox.Location = new Point(175, 153);
            planComboBox.Name = "planComboBox";
            planComboBox.Size = new Size(754, 33);
            planComboBox.TabIndex = 8;
            // 
            // suiteButton
            // 
            inputTableLayoutPanel.SetColumnSpan(suiteButton, 2);
            suiteButton.Dock = DockStyle.Top;
            suiteButton.Location = new Point(175, 203);
            suiteButton.Name = "suiteButton";
            suiteButton.Size = new Size(754, 35);
            suiteButton.TabIndex = 9;
            suiteButton.Text = "Select Test Suite";
            suiteButton.TextAlign = ContentAlignment.MiddleLeft;
            suiteButton.UseVisualStyleBackColor = true;
            // 
            // validateButton
            // 
            validateButton.Dock = DockStyle.Top;
            validateButton.Location = new Point(829, 53);
            validateButton.Name = "validateButton";
            validateButton.Size = new Size(100, 35);
            validateButton.TabIndex = 10;
            validateButton.Text = "Validate";
            validateButton.UseVisualStyleBackColor = true;
            // 
            // suitesGrid
            // 
            suitesGrid.AllowUserToAddRows = false;
            suitesGrid.AllowUserToDeleteRows = false;
            suitesGrid.AllowUserToResizeRows = false;
            suitesGrid.BackgroundColor = SystemColors.ControlLightLight;
            suitesGrid.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            suitesGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            suitesGrid.Columns.AddRange(new DataGridViewColumn[] { SuiteId, SuiteName, SuitePath, FilePath, action });
            suitesGrid.Dock = DockStyle.Fill;
            suitesGrid.Location = new Point(0, 0);
            suitesGrid.Name = "suitesGrid";
            suitesGrid.ReadOnly = true;
            suitesGrid.RowHeadersWidth = 62;
            suitesGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            suitesGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            suitesGrid.Size = new Size(932, 345);
            suitesGrid.TabIndex = 0;
            // 
            // SuiteId
            // 
            SuiteId.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            SuiteId.DataPropertyName = "SuiteId";
            SuiteId.HeaderText = "Suite ID";
            SuiteId.MinimumWidth = 8;
            SuiteId.Name = "SuiteId";
            SuiteId.ReadOnly = true;
            SuiteId.Width = 110;
            // 
            // SuiteName
            // 
            SuiteName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            SuiteName.DataPropertyName = "SuiteName";
            SuiteName.HeaderText = "Suite Name";
            SuiteName.MinimumWidth = 8;
            SuiteName.Name = "SuiteName";
            SuiteName.ReadOnly = true;
            SuiteName.Width = 139;
            // 
            // SuitePath
            // 
            SuitePath.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            SuitePath.DataPropertyName = "SuitePath";
            SuitePath.HeaderText = "Suite Path";
            SuitePath.MinimumWidth = 8;
            SuitePath.Name = "SuitePath";
            SuitePath.ReadOnly = true;
            SuitePath.Width = 126;
            // 
            // FilePath
            // 
            FilePath.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            FilePath.DataPropertyName = "FilePath";
            FilePath.HeaderText = "Test File Path";
            FilePath.MinimumWidth = 8;
            FilePath.Name = "FilePath";
            FilePath.ReadOnly = true;
            // 
            // action
            // 
            action.DataPropertyName = "action";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            action.DefaultCellStyle = dataGridViewCellStyle1;
            action.HeaderText = "Action";
            action.MinimumWidth = 8;
            action.Name = "action";
            action.ReadOnly = true;
            action.Text = "Select File";
            action.ToolTipText = "Select File";
            action.UseColumnTextForButtonValue = true;
            action.Width = 120;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(logsTextBox);
            groupBox1.Dock = DockStyle.Fill;
            groupBox1.Location = new Point(10, 10);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(319, 602);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Logs";
            // 
            // logsTextBox
            // 
            logsTextBox.BackColor = SystemColors.Control;
            logsTextBox.BorderStyle = BorderStyle.None;
            logsTextBox.Dock = DockStyle.Fill;
            logsTextBox.Location = new Point(3, 27);
            logsTextBox.Name = "logsTextBox";
            logsTextBox.ReadOnly = true;
            logsTextBox.Size = new Size(313, 572);
            logsTextBox.TabIndex = 0;
            logsTextBox.Text = "";
            // 
            // actionControls
            // 
            actionControls.AutoSize = false;
            actionControls.BackColor = SystemColors.Control;
            actionControls.ImageScalingSize = new Size(24, 24);
            actionControls.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripSeparator1, exportTemplateButton, importTemplateButton, toolStripSeparator2, uploadTestCasesButton, clearLogsButton, toolStripSeparator3, cancelButton });
            actionControls.Location = new Point(0, 0);
            actionControls.Name = "actionControls";
            actionControls.Size = new Size(1303, 49);
            actionControls.TabIndex = 0;
            actionControls.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(172, 44);
            toolStripLabel1.Text = "Bulk Test Uploader";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 49);
            // 
            // exportTemplateButton
            // 
            exportTemplateButton.Image = Properties.Resources.export;
            exportTemplateButton.ImageTransparentColor = Color.Magenta;
            exportTemplateButton.Name = "exportTemplateButton";
            exportTemplateButton.Size = new Size(167, 44);
            exportTemplateButton.Text = "Export Template";
            // 
            // importTemplateButton
            // 
            importTemplateButton.Image = Properties.Resources.import;
            importTemplateButton.ImageTransparentColor = Color.Magenta;
            importTemplateButton.Name = "importTemplateButton";
            importTemplateButton.Size = new Size(171, 44);
            importTemplateButton.Text = "Import Template";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 49);
            // 
            // uploadTestCasesButton
            // 
            uploadTestCasesButton.Image = Properties.Resources.setting;
            uploadTestCasesButton.ImageTransparentColor = Color.Magenta;
            uploadTestCasesButton.Name = "uploadTestCasesButton";
            uploadTestCasesButton.Size = new Size(98, 44);
            uploadTestCasesButton.Text = "Upload";
            // 
            // clearLogsButton
            // 
            clearLogsButton.Alignment = ToolStripItemAlignment.Right;
            clearLogsButton.Image = Properties.Resources.broom;
            clearLogsButton.ImageTransparentColor = Color.Magenta;
            clearLogsButton.Name = "clearLogsButton";
            clearLogsButton.Size = new Size(122, 44);
            clearLogsButton.Text = "Clear Logs";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 49);
            // 
            // cancelButton
            // 
            cancelButton.Enabled = false;
            cancelButton.Image = Properties.Resources.cancel__1_;
            cancelButton.ImageTransparentColor = Color.Magenta;
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(91, 44);
            cancelButton.Text = "Cancel";
            // 
            // statusBarStrip
            // 
            statusBarStrip.ImageScalingSize = new Size(24, 24);
            statusBarStrip.Items.AddRange(new ToolStripItem[] { countStatusLabel, timeStatusLabel, progressStatusBar });
            statusBarStrip.Location = new Point(0, 675);
            statusBarStrip.Name = "statusBarStrip";
            statusBarStrip.RightToLeft = RightToLeft.Yes;
            statusBarStrip.Size = new Size(1303, 32);
            statusBarStrip.TabIndex = 1;
            statusBarStrip.Text = "statusStrip1";
            // 
            // countStatusLabel
            // 
            countStatusLabel.Name = "countStatusLabel";
            countStatusLabel.Size = new Size(192, 25);
            countStatusLabel.Text = "Uploaded 0 of 0 suites";
            // 
            // timeStatusLabel
            // 
            timeStatusLabel.Name = "timeStatusLabel";
            timeStatusLabel.Size = new Size(80, 25);
            timeStatusLabel.Text = "00:00:00";
            // 
            // progressStatusBar
            // 
            progressStatusBar.ForeColor = Color.SpringGreen;
            progressStatusBar.Name = "progressStatusBar";
            progressStatusBar.Size = new Size(150, 24);
            progressStatusBar.Style = ProgressBarStyle.Continuous;
            // 
            // timerControl
            // 
            timerControl.Interval = 1000;
            // 
            // BaseControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1303, 712);
            Controls.Add(splitContainer1);
            Controls.Add(statusBarStrip);
            Controls.Add(actionControls);
            Name = "BaseControl";
            Padding = new Padding(0, 0, 0, 5);
            Text = "Bulk Test Uploader";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            inputTableLayoutPanel.ResumeLayout(false);
            inputTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)suitesGrid).EndInit();
            groupBox1.ResumeLayout(false);
            actionControls.ResumeLayout(false);
            actionControls.PerformLayout();
            statusBarStrip.ResumeLayout(false);
            statusBarStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private GroupBox groupBox1;
        private RichTextBox logsTextBox;
        private ToolStrip actionControls;
        private ToolStripButton uploadTestCasesButton;
        private SplitContainer splitContainer2;
        private TableLayoutPanel inputTableLayoutPanel;
        private Label patTokenLabel;
        private Label orgNameLabel;
        private Label projectLabel;
        private Label planLabel;
        private Label suiteLabel;
        private MaskedTextBox patTokenMaskedTextBox;
        private TextBox orgNameTextBox;
        private ComboBox projectComboBox;
        private ComboBox planComboBox;
        private Button suiteButton;
        private Button validateButton;
        private DataGridView suitesGrid;
        private DataGridViewTextBoxColumn SuiteId;
        private DataGridViewTextBoxColumn SuiteName;
        private DataGridViewTextBoxColumn SuitePath;
        private DataGridViewTextBoxColumn FilePath;
        private DataGridViewButtonColumn action;
        private ToolStripButton importTemplateButton;
        private ToolStripButton exportTemplateButton;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton clearLogsButton;
        private ToolStripButton cancelButton;
        private ToolStripSeparator toolStripSeparator3;
        private StatusStrip statusBarStrip;
        private ToolStripStatusLabel countStatusLabel;
        private ToolStripStatusLabel timeStatusLabel;
        private ToolStripProgressBar progressStatusBar;
        private System.Windows.Forms.Timer timerControl;
    }
}
