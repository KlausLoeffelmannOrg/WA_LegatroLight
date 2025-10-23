namespace LegatroExp.Forms;

partial class OptionsForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components is not null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        _tlpMain = new TableLayoutPanel();
        _tlpContent = new TableLayoutPanel();
        _lblSortOrder = new Label();
        _cmbSortOrder = new ComboBox();
        _chkAutoBackup = new CheckBox();
        _lblBaseFont = new Label();
        _cmbBaseFont = new ComboBox();
        _lblLanguage = new Label();
        _cmbLanguage = new ComboBox();
        _flpButtons = new FlowLayoutPanel();
        _btnOK = new Button();
        _btnCancel = new Button();
        _tlpMain.SuspendLayout();
        _tlpContent.SuspendLayout();
        _flpButtons.SuspendLayout();
        SuspendLayout();
        // 
        // _tlpMain
        // 
        _tlpMain.ColumnCount = 1;
        _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpMain.Controls.Add(_tlpContent, 0, 0);
        _tlpMain.Controls.Add(_flpButtons, 0, 1);
        _tlpMain.Dock = DockStyle.Fill;
        _tlpMain.Location = new Point(0, 0);
        _tlpMain.Name = "_tlpMain";
        _tlpMain.RowCount = 2;
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        _tlpMain.Size = new Size(701, 378);
        _tlpMain.TabIndex = 0;
        // 
        // _tlpContent
        // 
        _tlpContent.ColumnCount = 2;
        _tlpContent.ColumnStyles.Add(new ColumnStyle());
        _tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpContent.Controls.Add(_lblSortOrder, 0, 0);
        _tlpContent.Controls.Add(_cmbSortOrder, 1, 0);
        _tlpContent.Controls.Add(_chkAutoBackup, 0, 1);
        _tlpContent.Controls.Add(_lblBaseFont, 0, 2);
        _tlpContent.Controls.Add(_cmbBaseFont, 1, 2);
        _tlpContent.Controls.Add(_lblLanguage, 0, 3);
        _tlpContent.Controls.Add(_cmbLanguage, 1, 3);
        _tlpContent.Dock = DockStyle.Fill;
        _tlpContent.Location = new Point(3, 3);
        _tlpContent.Name = "_tlpContent";
        _tlpContent.Padding = new Padding(10);
        _tlpContent.RowCount = 4;
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
        _tlpContent.Size = new Size(695, 322);
        _tlpContent.TabIndex = 0;
        // 
        // _lblSortOrder
        // 
        _lblSortOrder.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblSortOrder.AutoSize = true;
        _lblSortOrder.Location = new Point(13, 15);
        _lblSortOrder.Name = "_lblSortOrder";
        _lblSortOrder.Size = new Size(244, 30);
        _lblSortOrder.TabIndex = 0;
        _lblSortOrder.Text = "&Default Task Sort Order:";
        // 
        // _cmbSortOrder
        // 
        _cmbSortOrder.Dock = DockStyle.Fill;
        _cmbSortOrder.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbSortOrder.FormattingEnabled = true;
        _cmbSortOrder.Items.AddRange(new object[] { "DisplayName", "DueDate", "PercentDone", "EstimatedEffort", "DateCreated" });
        _cmbSortOrder.Location = new Point(263, 13);
        _cmbSortOrder.Name = "_cmbSortOrder";
        _cmbSortOrder.Size = new Size(419, 38);
        _cmbSortOrder.TabIndex = 1;
        // 
        // _chkAutoBackup
        // 
        _chkAutoBackup.AutoSize = true;
        _chkAutoBackup.Checked = true;
        _chkAutoBackup.CheckState = CheckState.Checked;
        _tlpContent.SetColumnSpan(_chkAutoBackup, 2);
        _chkAutoBackup.Dock = DockStyle.Fill;
        _chkAutoBackup.Location = new Point(13, 53);
        _chkAutoBackup.Name = "_chkAutoBackup";
        _chkAutoBackup.Size = new Size(669, 34);
        _chkAutoBackup.TabIndex = 2;
        _chkAutoBackup.Text = "&Automatically backup database on shutdown";
        _chkAutoBackup.UseVisualStyleBackColor = true;
        // 
        // _lblBaseFont
        // 
        _lblBaseFont.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblBaseFont.AutoSize = true;
        _lblBaseFont.Location = new Point(13, 95);
        _lblBaseFont.Name = "_lblBaseFont";
        _lblBaseFont.Size = new Size(244, 30);
        _lblBaseFont.TabIndex = 3;
        _lblBaseFont.Text = "&Base Font:";
        // 
        // _cmbBaseFont
        // 
        _cmbBaseFont.Dock = DockStyle.Fill;
        _cmbBaseFont.FormattingEnabled = true;
        _cmbBaseFont.Location = new Point(263, 93);
        _cmbBaseFont.Name = "_cmbBaseFont";
        _cmbBaseFont.Size = new Size(419, 38);
        _cmbBaseFont.TabIndex = 4;
        // 
        // _lblLanguage
        // 
        _lblLanguage.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblLanguage.AutoSize = true;
        _lblLanguage.Location = new Point(13, 206);
        _lblLanguage.Name = "_lblLanguage";
        _lblLanguage.Size = new Size(244, 30);
        _lblLanguage.TabIndex = 5;
        _lblLanguage.Text = "&Language:";
        // 
        // _cmbLanguage
        // 
        _cmbLanguage.Dock = DockStyle.Fill;
        _cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
        _cmbLanguage.FormattingEnabled = true;
        _cmbLanguage.Items.AddRange(new object[] { "English", "German", "Spanish", "Japanese", "Chinese (Simplified)" });
        _cmbLanguage.Location = new Point(263, 133);
        _cmbLanguage.Name = "_cmbLanguage";
        _cmbLanguage.Size = new Size(419, 38);
        _cmbLanguage.TabIndex = 6;
        // 
        // _flpButtons
        // 
        _flpButtons.Controls.Add(_btnOK);
        _flpButtons.Controls.Add(_btnCancel);
        _flpButtons.Dock = DockStyle.Right;
        _flpButtons.FlowDirection = FlowDirection.RightToLeft;
        _flpButtons.Location = new Point(494, 331);
        _flpButtons.Name = "_flpButtons";
        _flpButtons.Size = new Size(204, 44);
        _flpButtons.TabIndex = 1;
        // 
        // _btnOK
        // 
        _btnOK.Location = new Point(106, 3);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(95, 35);
        _btnOK.TabIndex = 0;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;
        _btnOK.Click += BtnOK_Click;
        // 
        // _btnCancel
        // 
        _btnCancel.Location = new Point(5, 3);
        _btnCancel.Name = "_btnCancel";
        _btnCancel.Size = new Size(95, 35);
        _btnCancel.TabIndex = 1;
        _btnCancel.Text = "Cancel";
        _btnCancel.UseVisualStyleBackColor = true;
        _btnCancel.Click += BtnCancel_Click;
        // 
        // OptionsForm
        // 
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(144F, 144F);
        AutoScaleMode = AutoScaleMode.Dpi;
        CancelButton = _btnCancel;
        ClientSize = new Size(701, 378);
        Controls.Add(_tlpMain);
        Font = new Font("Segoe UI", 11F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "OptionsForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Options";
        _tlpMain.ResumeLayout(false);
        _tlpContent.ResumeLayout(false);
        _tlpContent.PerformLayout();
        _flpButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    private TableLayoutPanel _tlpMain;
    private TableLayoutPanel _tlpContent;
    private Label _lblSortOrder;
    private ComboBox _cmbSortOrder;
    private CheckBox _chkAutoBackup;
    private Label _lblBaseFont;
    private ComboBox _cmbBaseFont;
    private Label _lblLanguage;
    private ComboBox _cmbLanguage;
    private FlowLayoutPanel _flpButtons;
    private Button _btnOK;
    private Button _btnCancel;
}
