namespace LegatroExp.Controls;

public class DarkModeDataGridView : DataGridView
{
    public DarkModeDataGridView()
    {
        DoubleBuffered = true;
        UpdateTheme();
    }

    public void UpdateTheme()
    {
#pragma warning disable WFO5001
        bool isDarkMode = Application.IsDarkModeEnabled;
#pragma warning restore WFO5001

        if (isDarkMode)
        {
            BackgroundColor = Color.FromArgb(32, 32, 32);
            ForeColor = Color.White;
            GridColor = Color.FromArgb(64, 64, 64);

            DefaultCellStyle.BackColor = Color.FromArgb(45, 45, 48);
            DefaultCellStyle.ForeColor = Color.White;
            DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 122, 204);
            DefaultCellStyle.SelectionForeColor = Color.White;

            AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(51, 51, 51);

            ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            RowHeadersDefaultCellStyle.BackColor = Color.FromArgb(28, 28, 28);
            RowHeadersDefaultCellStyle.ForeColor = Color.White;

            EnableHeadersVisualStyles = false;
        }
        else
        {
            BackgroundColor = SystemColors.Window;
            ForeColor = SystemColors.ControlText;
            GridColor = SystemColors.ControlLight;

            DefaultCellStyle.BackColor = SystemColors.Window;
            DefaultCellStyle.ForeColor = SystemColors.ControlText;
            DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
            DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;

            AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);

            ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

            RowHeadersDefaultCellStyle.BackColor = SystemColors.Control;
            RowHeadersDefaultCellStyle.ForeColor = SystemColors.ControlText;

            EnableHeadersVisualStyles = true;
        }
    }
}
