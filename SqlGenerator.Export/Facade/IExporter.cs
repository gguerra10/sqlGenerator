using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SqlGenerator.Export.Facade
{
    public interface IExporter
    {
        public string Filter { get; }
        public bool Export(string filePath, DataTable dataTable, params object[] arguments);
    }
}
