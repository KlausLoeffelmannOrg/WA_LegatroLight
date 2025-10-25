namespace LegatroExp.Forms;

partial class UserSetupAssistant
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
        _lblTitle = new Label();
        _lblWelcome = new Label();
        _lblUserName = new Label();
        _txtUserName = new TextBox();
        _lblNickname = new Label();
        _txtNickname = new TextBox();
        _lblEmail = new Label();
        _txtEmail = new TextBox();
        _btnOK = new Button();
        _btnCancel = new Button();
        _tableLayoutPanel = new TableLayoutPanel();
        _flowLayoutButtons = new FlowLayoutPanel();

        SuspendLayout();

        _tableLayoutPanel.SuspendLayout();
        _flowLayoutButtons.SuspendLayout();

        // _tableLayoutPanel
        _tableLayoutPanel.ColumnCount = 2;
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        _tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        _tableLayoutPanel.Dock = DockStyle.Fill;
        _tableLayoutPanel.Location = new Point(20, 20);
        _tableLayoutPanel.Name = "_tableLayoutPanel";
        _tableLayoutPanel.Padding = new Padding(10);
        _tableLayoutPanel.RowCount = 7;
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        _tableLayoutPanel.Size = new Size(544, 331);
        _tableLayoutPanel.TabIndex = 0;

        // _lblTitle
        _tableLayoutPanel.SetColumnSpan(_lblTitle, 2);
        _lblTitle.AutoSize = true;
        _lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        _lblTitle.Location = new Point(13, 10);
        _lblTitle.Margin = new Padding(3, 0, 3, 15);
        _lblTitle.Name = "_lblTitle";
        _lblTitle.Size = new Size(231, 32);
        _lblTitle.TabIndex = 0;
        _lblTitle.Text = "User Setup Assistant";

        // _lblWelcome
        _tableLayoutPanel.SetColumnSpan(_lblWelcome, 2);
        _lblWelcome.AutoSize = true;
        _lblWelcome.Location = new Point(13, 57);
        _lblWelcome.Margin = new Padding(3, 0, 3, 15);
        _lblWelcome.Name = "_lblWelcome";
        _lblWelcome.Size = new Size(449, 25);
        _lblWelcome.TabIndex = 1;
        _lblWelcome.Text = "Welcome! Please verify your user information below.";

        // _lblUserName
        _lblUserName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblUserName.AutoSize = true;
        _lblUserName.Location = new Point(13, 101);
        _lblUserName.Margin = new Padding(3);
        _lblUserName.Name = "_lblUserName";
        _lblUserName.Size = new Size(96, 25);
        _lblUserName.TabIndex = 2;
        _lblUserName.Text = "&User Name:";

        // _txtUserName
        _txtUserName.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtUserName.Location = new Point(115, 97);
        _txtUserName.Margin = new Padding(3);
        _txtUserName.Name = "_txtUserName";
        _txtUserName.ReadOnly = true;
        _txtUserName.Size = new Size(416, 32);
        _txtUserName.TabIndex = 3;

        // _lblNickname
        _lblNickname.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblNickname.AutoSize = true;
        _lblNickname.Location = new Point(13, 141);
        _lblNickname.Margin = new Padding(3);
        _lblNickname.Name = "_lblNickname";
        _lblNickname.Size = new Size(90, 25);
        _lblNickname.TabIndex = 4;
        _lblNickname.Text = "&Nickname:";

        // _txtNickname
        _txtNickname.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtNickname.Location = new Point(115, 137);
        _txtNickname.Margin = new Padding(3);
        _txtNickname.Name = "_txtNickname";
        _txtNickname.Size = new Size(416, 32);
        _txtNickname.TabIndex = 5;

        // _lblEmail
        _lblEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _lblEmail.AutoSize = true;
        _lblEmail.Location = new Point(13, 181);
        _lblEmail.Margin = new Padding(3);
        _lblEmail.Name = "_lblEmail";
        _lblEmail.Size = new Size(54, 25);
        _lblEmail.TabIndex = 6;
        _lblEmail.Text = "&Email:";

        // _txtEmail
        _txtEmail.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        _txtEmail.Location = new Point(115, 177);
        _txtEmail.Margin = new Padding(3);
        _txtEmail.Name = "_txtEmail";
        _txtEmail.Size = new Size(416, 32);
        _txtEmail.TabIndex = 7;

        // _flowLayoutButtons
        _tableLayoutPanel.SetColumnSpan(_flowLayoutButtons, 2);
        _flowLayoutButtons.AutoSize = true;
        _flowLayoutButtons.Dock = DockStyle.Fill;
        _flowLayoutButtons.FlowDirection = FlowDirection.RightToLeft;
        _flowLayoutButtons.Location = new Point(13, 215);
        _flowLayoutButtons.Name = "_flowLayoutButtons";
        _flowLayoutButtons.Padding = new Padding(0, 10, 0, 0);
        _flowLayoutButtons.Size = new Size(518, 103);
        _flowLayoutButtons.TabIndex = 8;

        // _btnOK
        _btnOK.DialogResult = DialogResult.OK;
        _btnOK.Location = new Point(405, 13);
        _btnOK.Name = "_btnOK";
        _btnOK.Size = new Size(110, 40);
        _btnOK.TabIndex = 0;
        _btnOK.Text = "OK";
        _btnOK.UseVisualStyleBackColor = true;
        _btnOK.Click += BtnOK_Click;

        // _btnCancel
        _btnCancel.DialogResult = DialogResult.Cancel;
        _btnCancel.Location = new Point(289, 13);
        _btnCancel.Name = "_btnCancel";
        _btnCancel.Size = new Size(110, 40);
        _btnCancel.TabIndex = 1;
        _btnCancel.Text = "Cancel";
        _btnCancel.UseVisualStyleBackColor = true;

        // Add controls to layouts
        _flowLayoutButtons.Controls.Add(_btnOK);
        _flowLayoutButtons.Controls.Add(_btnCancel);

        _tableLayoutPanel.Controls.Add(_lblTitle, 0, 0);
        _tableLayoutPanel.Controls.Add(_lblWelcome, 0, 1);
        _tableLayoutPanel.Controls.Add(_lblUserName, 0, 2);
        _tableLayoutPanel.Controls.Add(_txtUserName, 1, 2);
        _tableLayoutPanel.Controls.Add(_lblNickname, 0, 3);
        _tableLayoutPanel.Controls.Add(_txtNickname, 1, 3);
        _tableLayoutPanel.Controls.Add(_lblEmail, 0, 4);
        _tableLayoutPanel.Controls.Add(_txtEmail, 1, 4);
        _tableLayoutPanel.Controls.Add(_flowLayoutButtons, 0, 6);

        // UserSetupAssistant
        AcceptButton = _btnOK;
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        CancelButton = _btnCancel;
        ClientSize = new Size(584, 371);
        Controls.Add(_tableLayoutPanel);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "UserSetupAssistant";
        Padding = new Padding(20);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "User Setup Assistant";

        _tableLayoutPanel.ResumeLayout(false);
        _tableLayoutPanel.PerformLayout();
        _flowLayoutButtons.ResumeLayout(false);
        ResumeLayout(false);
    }

    private Label _lblTitle;
    private Label _lblWelcome;
    private Label _lblUserName;
    private TextBox _txtUserName;
    private Label _lblNickname;
    private TextBox _txtNickname;
    private Label _lblEmail;
    private TextBox _txtEmail;
    private Button _btnOK;
    private Button _btnCancel;
    private TableLayoutPanel _tableLayoutPanel;
    private FlowLayoutPanel _flowLayoutButtons;
}
