using System.Windows.Forms;

namespace SqlGenerator.Extensions

{
    public static class DataGridViewRowExtensions
    {

        /// <summary>
        /// Check if every cell in a row has value
        /// </summary>
        /// <param name="dataGridViewRow"></param>
        /// <returns></returns>
        public static bool AllCellsHaveValue(this DataGridViewRow dataGridViewRow)
        {
            var result = true;
            foreach(DataGridViewCell cell in dataGridViewRow.Cells)
            {
                if(cell.Value == null)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
