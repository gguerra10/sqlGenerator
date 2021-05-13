using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SqlGenerator.UI.Extension
{
    public static class DataGridViewRowExtensions
    {
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
