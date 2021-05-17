using System.Collections.Generic;
using System.Data;


namespace SqlGenerator.Export.Pdf
{
    public class PdfGroupedDataTable
    {
        public Dictionary<object[],DataTable> DataTables { get; set; }

        public PdfGroupedDataTable()
        {
            DataTables = new Dictionary<object[], DataTable>();
        }
    }
}
