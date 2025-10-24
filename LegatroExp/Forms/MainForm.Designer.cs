namespace LegatroExp.Forms;

partial class MainForm
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
        _menuStrip = new MenuStrip();
        _fileToolStripMenuItem = new ToolStripMenuItem();
        _editToolStripMenuItem = new ToolStripMenuItem();
        _toolsToolStripMenuItem = new ToolStripMenuItem();
        _statusStrip = new StatusStrip();
        _toolStripStatusLabel = new ToolStripStatusLabel();

        SuspendLayout();
        _menuStrip.SuspendLayout();
        _statusStrip.SuspendLayout();

        // _menuStrip
        _menuStrip.ImageScalingSize = new Size(20, 20);
        _menuStrip.Items.AddRange(new ToolStripItem[] {
            _fileToolStripMenuItem,
            _editToolStripMenuItem,
            _toolsToolStripMenuItem});
        _menuStrip.Location = new Point(0, 0);
        _menuStrip.Name = "_menuStrip";
        _menuStrip.Padding = new Padding(8, 3, 0, 3);
        _menuStrip.Size = new Size(1200, 32);
        _menuStrip.TabIndex = 0;
        _menuStrip.Text = "menuStrip1";

        // _fileToolStripMenuItem
        _fileToolStripMenuItem.Name = "_fileToolStripMenuItem";
        _fileToolStripMenuItem.Size = new Size(54, 26);
        _fileToolStripMenuItem.Text = "&File";

        // _editToolStripMenuItem
        _editToolStripMenuItem.Name = "_editToolStripMenuItem";
        _editToolStripMenuItem.Size = new Size(54, 26);
        _editToolStripMenuItem.Text = "&Edit";

        // _toolsToolStripMenuItem
        _toolsToolStripMenuItem.Name = "_toolsToolStripMenuItem";
        _toolsToolStripMenuItem.Size = new Size(65, 26);
        _toolsToolStripMenuItem.Text = "&Tools";

        // _statusStrip
        _statusStrip.ImageScalingSize = new Size(20, 20);
        _statusStrip.Items.AddRange(new ToolStripItem[] {
            _toolStripStatusLabel});
        _statusStrip.Location = new Point(0, 628);
        _statusStrip.Name = "_statusStrip";
        _statusStrip.Padding = new Padding(1, 0, 18, 0);
        _statusStrip.Size = new Size(1200, 32);
        _statusStrip.TabIndex = 1;
        _statusStrip.Text = "statusStrip1";

        // _toolStripStatusLabel
        _toolStripStatusLabel.Name = "_toolStripStatusLabel";
        _toolStripStatusLabel.Size = new Size(59, 25);
        _toolStripStatusLabel.Text = "Ready";

        // MainForm
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1200, 660);
        Controls.Add(_statusStrip);
        Controls.Add(_menuStrip);
        MainMenuStrip = _menuStrip;
        Name = "MainForm";
        Text = "LegatroExp - Time Tracking";

        _menuStrip.ResumeLayout(false);
        _menuStrip.PerformLayout();
        _statusStrip.ResumeLayout(false);
        _statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    private MenuStrip _menuStrip;
    private ToolStripMenuItem _fileToolStripMenuItem;
    private ToolStripMenuItem _editToolStripMenuItem;
    private ToolStripMenuItem _toolsToolStripMenuItem;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _toolStripStatusLabel;
}
