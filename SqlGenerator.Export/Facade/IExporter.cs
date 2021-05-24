using System.Data;


namespace SqlGenerator.Export.Facade
{
    public interface IExporter
    {
        public string Filter { get; }
        public bool Export(string filePath, DataTable dataTable);
    }
}
