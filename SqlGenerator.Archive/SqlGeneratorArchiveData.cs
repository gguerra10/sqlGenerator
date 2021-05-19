using SqlGenerator.Core.Enum;
using System.Collections.Generic;
using SqlGenerator.Export.Pdf;
using SqlGenerator.Archive.Content;

namespace SqlGenerator.Archive
{
    public class SqlGeneratorArchiveData
    {
        public DatabaseType DatabaseType { get; set; }

        public string ConnectionString { get; set; }

        public List<ArchiveTable> Tables { get; set; }

        public List<ArchiveColumn> Columns { get; set; }

        public ArchiveOptions Options { get; set; }

        public PdfDesign PdfDesign { get; set; }

        public SqlGeneratorArchiveData()
        {
            Tables = new List<ArchiveTable>();
            Columns = new List<ArchiveColumn>();
            Options = new ArchiveOptions();
            PdfDesign = new PdfDesign();
        }
    }
}
