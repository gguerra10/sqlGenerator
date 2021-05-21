using System.Windows.Forms;

namespace SqlGenerator.Extensions
{
    public static class DataGridViewExtensions
    {
        /// <summary>
        /// Set automatic size in displayed columns
        /// </summary>
        /// <param name="dataGridView"></param>
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
