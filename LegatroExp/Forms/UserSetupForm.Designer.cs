namespace LegatroExp.Forms;

partial class UserSetupForm
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
        _lblTitle = new Label();
        _tlpContent = new TableLayoutPanel();
        _lblUserDisplayId = new Label();
        _txtUserDisplayId = new TextBox();
        _lblUserAuthId = new Label();
        _txtUserAuthId = new TextBox();
        _lblUserSid = new Label();
        _txtUserSid = new TextBox();
        _lblDomain = new Label();
        _txtDomain = new TextBox();
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
        _flpButtons = new FlowLayoutPanel();
        _btnOK = new Button();
        _btnCancel = new Button();

        _tlpMain.SuspendLayout();
        _tlpContent.SuspendLayout();
        _flpButtons.SuspendLayout();
        SuspendLayout();

        _tlpMain.ColumnCount = 1;
        _tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpMain.Controls.Add(_lblTitle, 0, 0);
        _tlpMain.Controls.Add(_tlpContent, 0, 1);
        _tlpMain.Controls.Add(_flpButtons, 0, 2);
        _tlpMain.Dock = DockStyle.Fill;
        _tlpMain.Location = new Point(0, 0);
        _tlpMain.Name = "_tlpMain";
        _tlpMain.RowCount = 3;
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        _tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
        _tlpMain.Size = new Size(584, 561);
        _tlpMain.TabIndex = 0;

        _lblTitle.Anchor = AnchorStyles.Left;
        _lblTitle.AutoSize = true;
        _lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        _lblTitle.Location = new Point(3, 10);
        _lblTitle.Name = "_lblTitle";
        _lblTitle.Size = new Size(242, 30);
        _lblTitle.TabIndex = 0;
        _lblTitle.Text = "User Setup Assistant";

        _tlpContent.ColumnCount = 2;
        _tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tlpContent.Controls.Add(_lblUserDisplayId, 0, 0);
        _tlpContent.Controls.Add(_txtUserDisplayId, 1, 0);
        _tlpContent.Controls.Add(_lblUserAuthId, 0, 1);
        _tlpContent.Controls.Add(_txtUserAuthId, 1, 1);
        _tlpContent.Controls.Add(_lblUserSid, 0, 2);
        _tlpContent.Controls.Add(_txtUserSid, 1, 2);
        _tlpContent.Controls.Add(_lblDomain, 0, 3);
        _tlpContent.Controls.Add(_txtDomain, 1, 3);
        _tlpContent.Controls.Add(_lblUserName, 0, 4);
        _tlpContent.Controls.Add(_txtUserName, 1, 4);
        _tlpContent.Controls.Add(_lblDisplayName, 0, 5);
        _tlpContent.Controls.Add(_txtDisplayName, 1, 5);
        _tlpContent.Controls.Add(_lblFirstName, 0, 6);
        _tlpContent.Controls.Add(_txtFirstName, 1, 6);
        _tlpContent.Controls.Add(_lblMiddleName, 0, 7);
        _tlpContent.Controls.Add(_txtMiddleName, 1, 7);
        _tlpContent.Controls.Add(_lblLastName, 0, 8);
        _tlpContent.Controls.Add(_txtLastName, 1, 8);
        _tlpContent.Controls.Add(_lblEmail, 0, 9);
        _tlpContent.Controls.Add(_txtEmail, 1, 9);
        _tlpContent.Dock = DockStyle.Fill;
        _tlpContent.Location = new Point(3, 53);
        _tlpContent.Name = "_tlpContent";
        _tlpContent.Padding = new Padding(10);
        _tlpContent.RowCount = 10;
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        _tlpContent.Size = new Size(578, 455);
        _tlpContent.TabIndex = 1;

        _lblUserDisplayId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblUserDisplayId.AutoSize = true;
        _lblUserDisplayId.Location = new Point(13, 18);
        _lblUserDisplayId.Name = "_lblUserDisplayId";
        _lblUserDisplayId.Size = new Size(112, 20);
        _lblUserDisplayId.TabIndex = 0;
        _lblUserDisplayId.Text = "&User Display ID:";

        _txtUserDisplayId.Dock = DockStyle.Fill;
        _txtUserDisplayId.Location = new Point(131, 13);
        _txtUserDisplayId.Margin = new Padding(3);
        _txtUserDisplayId.Name = "_txtUserDisplayId";
        _txtUserDisplayId.Size = new Size(434, 27);
        _txtUserDisplayId.TabIndex = 1;

        _lblUserAuthId.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblUserAuthId.AutoSize = true;
        _lblUserAuthId.Location = new Point(13, 53);
        _lblUserAuthId.Name = "_lblUserAuthId";
        _lblUserAuthId.Size = new Size(112, 20);
        _lblUserAuthId.TabIndex = 2;
        _lblUserAuthId.Text = "User Auth ID:";

        _txtUserAuthId.Dock = DockStyle.Fill;
        _txtUserAuthId.Location = new Point(131, 48);
        _txtUserAuthId.Margin = new Padding(3);
        _txtUserAuthId.Name = "_txtUserAuthId";
        _txtUserAuthId.ReadOnly = true;
        _txtUserAuthId.Size = new Size(434, 27);
        _txtUserAuthId.TabIndex = 3;
        _txtUserAuthId.TabStop = false;

        _lblUserSid.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblUserSid.AutoSize = true;
        _lblUserSid.Location = new Point(13, 88);
        _lblUserSid.Name = "_lblUserSid";
        _lblUserSid.Size = new Size(112, 20);
        _lblUserSid.TabIndex = 4;
        _lblUserSid.Text = "User SID:";

        _txtUserSid.Dock = DockStyle.Fill;
        _txtUserSid.Location = new Point(131, 83);
        _txtUserSid.Margin = new Padding(3);
        _txtUserSid.Name = "_txtUserSid";
        _txtUserSid.ReadOnly = true;
        _txtUserSid.Size = new Size(434, 27);
        _txtUserSid.TabIndex = 5;
        _txtUserSid.TabStop = false;

        _lblDomain.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDomain.AutoSize = true;
        _lblDomain.Location = new Point(13, 123);
        _lblDomain.Name = "_lblDomain";
        _lblDomain.Size = new Size(112, 20);
        _lblDomain.TabIndex = 6;
        _lblDomain.Text = "Domain:";

        _txtDomain.Dock = DockStyle.Fill;
        _txtDomain.Location = new Point(131, 118);
        _txtDomain.Margin = new Padding(3);
        _txtDomain.Name = "_txtDomain";
        _txtDomain.ReadOnly = true;
        _txtDomain.Size = new Size(434, 27);
        _txtDomain.TabIndex = 7;
        _txtDomain.TabStop = false;

        _lblUserName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblUserName.AutoSize = true;
        _lblUserName.Location = new Point(13, 158);
        _lblUserName.Name = "_lblUserName";
        _lblUserName.Size = new Size(112, 20);
        _lblUserName.TabIndex = 8;
        _lblUserName.Text = "User Name:";

        _txtUserName.Dock = DockStyle.Fill;
        _txtUserName.Location = new Point(131, 153);
        _txtUserName.Margin = new Padding(3);
        _txtUserName.Name = "_txtUserName";
        _txtUserName.ReadOnly = true;
        _txtUserName.Size = new Size(434, 27);
        _txtUserName.TabIndex = 9;
        _txtUserName.TabStop = false;

        _lblDisplayName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblDisplayName.AutoSize = true;
        _lblDisplayName.Location = new Point(13, 193);
        _lblDisplayName.Name = "_lblDisplayName";
        _lblDisplayName.Size = new Size(112, 20);
        _lblDisplayName.TabIndex = 10;
        _lblDisplayName.Text = "&Display Name:";

        _txtDisplayName.Dock = DockStyle.Fill;
        _txtDisplayName.Location = new Point(131, 188);
        _txtDisplayName.Margin = new Padding(3);
        _txtDisplayName.Name = "_txtDisplayName";
        _txtDisplayName.Size = new Size(434, 27);
        _txtDisplayName.TabIndex = 11;

        _lblFirstName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblFirstName.AutoSize = true;
        _lblFirstName.Location = new Point(13, 228);
        _lblFirstName.Name = "_lblFirstName";
        _lblFirstName.Size = new Size(112, 20);
        _lblFirstName.TabIndex = 12;
        _lblFirstName.Text = "&First Name:";

        _txtFirstName.Dock = DockStyle.Fill;
        _txtFirstName.Location = new Point(131, 223);
        _txtFirstName.Margin = new Padding(3);
        _txtFirstName.Name = "_txtFirstName";
        _txtFirstName.Size = new Size(434, 27);
        _txtFirstName.TabIndex = 13;

        _lblMiddleName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblMiddleName.AutoSize = true;
        _lblMiddleName.Location = new Point(13, 263);
        _lblMiddleName.Name = "_lblMiddleName";
        _lblMiddleName.Size = new Size(112, 20);
        _lblMiddleName.TabIndex = 14;
        _lblMiddleName.Text = "&Middle Name:";

        _txtMiddleName.Dock = DockStyle.Fill;
        _txtMiddleName.Location = new Point(131, 258);
        _txtMiddleName.Margin = new Padding(3);
        _txtMiddleName.Name = "_txtMiddleName";
        _txtMiddleName.Size = new Size(434, 27);
        _txtMiddleName.TabIndex = 15;

        _lblLastName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblLastName.AutoSize = true;
        _lblLastName.Location = new Point(13, 298);
        _lblLastName.Name = "_lblLastName";
        _lblLastName.Size = new Size(112, 20);
        _lblLastName.TabIndex = 16;
        _lblLastName.Text = "&Last Name:";

        _txtLastName.Dock = DockStyle.Fill;
        _txtLastName.Location = new Point(131, 293);
        _txtLastName.Margin = new Padding(3);
        _txtLastName.Name = "_txtLastName";
        _txtLastName.Size = new Size(434, 27);
        _txtLastName.TabIndex = 17;

        _lblEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEmail.AutoSize = true;
        _lblEmail.Location = new Point(13, 333);
        _lblEmail.Name = "_lblEmail";
        _lblEmail.Size = new Size(112, 20);
        _lblEmail.TabIndex = 18;
        _lblEmail.Text = "&Email:";

        _txtEmail.Dock = DockStyle.Fill;
        _txtEmail.Location = new Point(131, 328);
        _txtEmail.Margin = new Padding(3);
        _txtEmail.Name = "_txtEmail";
        _txtEmail.Size = new Size(434, 27);
        _txtEmail.TabIndex = 19;

        _flpButtons.Dock = DockStyle.Right;
        _flpButtons.FlowDirection = FlowDirection.RightToLeft;
        _flpButtons.Location = new Point(374, 514);
        _flpButtons.Name = "_flpButtons";
        _flpButtons.Size = new Size(207, 44);
        _flpButtons.TabIndex = 2;

        _btnOK.Location = new Point(109, 3);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(95, 35);
        _btnOK.TabIndex = 0;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;
        _btnOK.Click += BtnOK_Click;

        _btnCancel.Location = new Point(8, 3);
        _btnCancel.Name = "_btnCancel";
        _btnCancel.Size = new Size(95, 35);
        _btnCancel.TabIndex = 1;
        _btnCancel.Text = "Cancel";
        _btnCancel.UseVisualStyleBackColor = true;
        _btnCancel.Click += BtnCancel_Click;

        _flpButtons.Controls.Add(_btnOK);
        _flpButtons.Controls.Add(_btnCancel);

        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;
        ClientSize = new Size(584, 561);
        Controls.Add(_tlpMain);
        Font = new Font("Segoe UI", 11F);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "UserSetupForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "User Setup Assistant";

        _tlpMain.ResumeLayout(false);
        _tlpMain.PerformLayout();
        _tlpContent.ResumeLayout(false);
        _tlpContent.PerformLayout();
        _flpButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    private TableLayoutPanel _tlpMain;
    private Label _lblTitle;
    private TableLayoutPanel _tlpContent;
    private Label _lblUserDisplayId;
    private TextBox _txtUserDisplayId;
    private Label _lblUserAuthId;
    private TextBox _txtUserAuthId;
    private Label _lblUserSid;
    private TextBox _txtUserSid;
    private Label _lblDomain;
    private TextBox _txtDomain;
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
    private FlowLayoutPanel _flpButtons;
    private Button _btnOK;
    private Button _btnCancel;
}
