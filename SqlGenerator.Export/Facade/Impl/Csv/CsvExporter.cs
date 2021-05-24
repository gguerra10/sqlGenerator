using System;
using System.Data;
using System.Diagnostics;
using System.IO;


namespace SqlGenerator.Export.Facade.Impl.Csv
{
    public class CsvExporter : IExporter
    {
        private const string separator = "\t";
        private const string newLine = "\r\n";
        public string Filter => "Comma separated values (*.csv)|*.csv";

        public bool Export(string filePath, DataTable dataTable)
        {
            var result = false;

            try
            {
                using(var sw = new  StreamWriter(filePath))
                {
                    var columnOrder = 0;
                    foreach(DataColumn column in dataTable.Columns)
                    {
                        sw.Write(column.ColumnName);
                        columnOrder++;
                        if (columnOrder != dataTable.Columns.Count-1)
                        {
                            sw.Write(separator);
                        }
                    }
                    sw.Write(newLine);
                    foreach(DataRow row in dataTable.Rows)
                    {
                        columnOrder = 0;
                        foreach(var item in row.ItemArray)
                        {
                            sw.Write(item);
                            columnOrder++;
                            if(columnOrder != row.ItemArray.Length-1)
                            {
                                sw.Write(separator);
                            }
                        }
                        sw.Write(newLine);
                    }
                    sw.Close();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message);
            }
            return result;
        }
    }
}
