namespace LegatroExp.Forms;

partial class UserSetupForm
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        _lblTitle = new Label();
        _lblUserName = new Label();
        _txtUserName = new TextBox();
        _lblDisplayName = new Label();
        _txtDisplayName = new TextBox();
        _lblFirstName = new Label();
        _txtFirstName = new TextBox();
        _lblMiddleName = new Label();
        _txtMiddleName = new TextBox();
        _lblLastName = new Label();
        _txtLastName = new TextBox();
        _lblEmail = new Label();
        _txtEmail = new TextBox();
        _lblDomain = new Label();
        _txtDomain = new TextBox();
        _btnOK = new Button();
        _btnCancel = new Button();
        _tlpMain = new TableLayoutPanel();
        _flpButtons = new FlowLayoutPanel();
        
        _tlpMain.SuspendLayout();
        _flpButtons.SuspendLayout();
        SuspendLayout();
        
        _lblTitle.AutoSize = true;
        _lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        _lblTitle.Location = new Point(3, 3);
        _lblTitle.Margin = new Padding(3);
        _lblTitle.Name = "_lblTitle";
        _lblTitle.Size = new Size(200, 25);
        _lblTitle.Text = "User Setup Assistant";
        
        _lblUserName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblUserName.AutoSize = true;
        _lblUserName.Location = new Point(3, 43);
        _lblUserName.Margin = new Padding(3);
        _lblUserName.Name = "_lblUserName";
        _lblUserName.Size = new Size(100, 20);
        _lblUserName.Text = "Username:";
        
        _txtUserName.Dock = DockStyle.Fill;
        _txtUserName.Location = new Point(109, 40);
        _txtUserName.Margin = new Padding(3);
        _txtUserName.Name = "_txtUserName";
        _txtUserName.ReadOnly = true;
        _txtUserName.Size = new Size(388, 27);
        _txtUserName.TabStop = false;
        
        _lblDisplayName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDisplayName.AutoSize = true;
        _lblDisplayName.Location = new Point(3, 76);
        _lblDisplayName.Margin = new Padding(3);
        _lblDisplayName.Name = "_lblDisplayName";
        _lblDisplayName.Size = new Size(100, 20);
        _lblDisplayName.Text = "&Display Name:";
        
        _txtDisplayName.Dock = DockStyle.Fill;
        _txtDisplayName.Location = new Point(109, 73);
        _txtDisplayName.Margin = new Padding(3);
        _txtDisplayName.Name = "_txtDisplayName";
        _txtDisplayName.Size = new Size(388, 27);
        
        _lblFirstName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblFirstName.AutoSize = true;
        _lblFirstName.Location = new Point(3, 109);
        _lblFirstName.Margin = new Padding(3);
        _lblFirstName.Name = "_lblFirstName";
        _lblFirstName.Size = new Size(100, 20);
        _lblFirstName.Text = "&First Name:";
        
        _txtFirstName.Dock = DockStyle.Fill;
        _txtFirstName.Location = new Point(109, 106);
        _txtFirstName.Margin = new Padding(3);
        _txtFirstName.Name = "_txtFirstName";
        _txtFirstName.Size = new Size(388, 27);
        
        _lblMiddleName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblMiddleName.AutoSize = true;
        _lblMiddleName.Location = new Point(3, 142);
        _lblMiddleName.Margin = new Padding(3);
        _lblMiddleName.Name = "_lblMiddleName";
        _lblMiddleName.Size = new Size(100, 20);
        _lblMiddleName.Text = "&Middle Name:";
        
        _txtMiddleName.Dock = DockStyle.Fill;
        _txtMiddleName.Location = new Point(109, 139);
        _txtMiddleName.Margin = new Padding(3);
        _txtMiddleName.Name = "_txtMiddleName";
        _txtMiddleName.Size = new Size(388, 27);
        
        _lblLastName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblLastName.AutoSize = true;
        _lblLastName.Location = new Point(3, 175);
        _lblLastName.Margin = new Padding(3);
        _lblLastName.Name = "_lblLastName";
        _lblLastName.Size = new Size(100, 20);
        _lblLastName.Text = "&Last Name:";
        
        _txtLastName.Dock = DockStyle.Fill;
        _txtLastName.Location = new Point(109, 172);
        _txtLastName.Margin = new Padding(3);
        _txtLastName.Name = "_txtLastName";
        _txtLastName.Size = new Size(388, 27);
        
        _lblEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEmail.AutoSize = true;
        _lblEmail.Location = new Point(3, 208);
        _lblEmail.Margin = new Padding(3);
        _lblEmail.Name = "_lblEmail";
        _lblEmail.Size = new Size(100, 20);
        _lblEmail.Text = "&Email:";
        
        _txtEmail.Dock = DockStyle.Fill;
        _txtEmail.Location = new Point(109, 205);
        _txtEmail.Margin = new Padding(3);
        _txtEmail.Name = "_txtEmail";
        _txtEmail.Size = new Size(388, 27);
        
        _lblDomain.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDomain.AutoSize = true;
        _lblDomain.Location = new Point(3, 241);
        _lblDomain.Margin = new Padding(3);
        _lblDomain.Name = "_lblDomain";
        _lblDomain.Size = new Size(100, 20);
        _lblDomain.Text = "Domain:";
        
        _txtDomain.Dock = DockStyle.Fill;
        _txtDomain.Location = new Point(109, 238);
        _txtDomain.Margin = new Padding(3);
        _txtDomain.Name = "_txtDomain";
        _txtDomain.ReadOnly = true;
        _txtDomain.Size = new Size(388, 27);
        _txtDomain.TabStop = false;
        
        _btnOK.DialogResult = DialogResult.OK;
        _btnOK.Location = new Point(3, 3);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(100, 30);
        _btnOK.Text = "OK";
        _btnOK.Click += BtnOK_Click;
        
        _btnCancel.DialogResult = DialogResult.Cancel;
        _btnCancel.Location = new Point(109, 3);
        _btnCancel.Name = "_btnCancel";
        _btnCancel.Size = new Size(100, 30);
        _btnCancel.Text = "Cancel";
        _btnCancel.Click += BtnCancel_Click;
        
        _flpButtons.AutoSize = true;
        _flpButtons.Dock = DockStyle.Fill;
        _flpButtons.FlowDirection = FlowDirection.RightToLeft;
        _flpButtons.Location = new Point(3, 271);
        _flpButtons.Margin = new Padding(3);
        _flpButtons.Name = "_flpButtons";
        _flpButtons.Size = new Size(494, 36);
        _flpButtons.Controls.Add(_btnCancel);
        _flpButtons.Controls.Add(_btnOK);
        
        _tlpMain.ColumnCount = 2;
        _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpMain.Controls.Add(_lblTitle, 0, 0);
        _tlpMain.Controls.Add(_lblUserName, 0, 1);
        _tlpMain.Controls.Add(_txtUserName, 1, 1);
        _tlpMain.Controls.Add(_lblDisplayName, 0, 2);
        _tlpMain.Controls.Add(_txtDisplayName, 1, 2);
        _tlpMain.Controls.Add(_lblFirstName, 0, 3);
        _tlpMain.Controls.Add(_txtFirstName, 1, 3);
        _tlpMain.Controls.Add(_lblMiddleName, 0, 4);
        _tlpMain.Controls.Add(_txtMiddleName, 1, 4);
        _tlpMain.Controls.Add(_lblLastName, 0, 5);
        _tlpMain.Controls.Add(_txtLastName, 1, 5);
        _tlpMain.Controls.Add(_lblEmail, 0, 6);
        _tlpMain.Controls.Add(_txtEmail, 1, 6);
        _tlpMain.Controls.Add(_lblDomain, 0, 7);
        _tlpMain.Controls.Add(_txtDomain, 1, 7);
        _tlpMain.Controls.Add(_flpButtons, 0, 8);
        _tlpMain.Dock = DockStyle.Fill;
        _tlpMain.Location = new Point(10, 10);
        _tlpMain.Name = "_tlpMain";
        _tlpMain.RowCount = 9;
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tlpMain.SetColumnSpan(_lblTitle, 2);
        _tlpMain.SetColumnSpan(_flpButtons, 2);
        _tlpMain.Size = new Size(500, 310);
        
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnCancel;
        ClientSize = new Size(520, 330);
        Controls.Add(_tlpMain);
        Font = new Font("Segoe UI", 11F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "UserSetupForm";
        Padding = new Padding(10);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "User Setup";
        
        _tlpMain.ResumeLayout(false);
        _tlpMain.PerformLayout();
        _flpButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Label _lblTitle;
    private Label _lblUserName;
    private TextBox _txtUserName;
    private Label _lblDisplayName;
    private TextBox _txtDisplayName;
    private Label _lblFirstName;
    private TextBox _txtFirstName;
    private Label _lblMiddleName;
    private TextBox _txtMiddleName;
    private Label _lblLastName;
    private TextBox _txtLastName;
    private Label _lblEmail;
    private TextBox _txtEmail;
    private Label _lblDomain;
    private TextBox _txtDomain;
    private Button _btnOK;
    private Button _btnCancel;
    private TableLayoutPanel _tlpMain;
    private FlowLayoutPanel _flpButtons;
}
