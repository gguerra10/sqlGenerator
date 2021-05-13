using System.Windows.Forms;

namespace SqlGenerator.UI.Extension
{
    public static class DataGridViewExtensions
    {
        public static void AutoSizeColumns(this DataGridView dataGridView)
        {
            foreach(DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                var columnWidth = column.Width;
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                column.Width = columnWidth;
            }

        }
    }
}
