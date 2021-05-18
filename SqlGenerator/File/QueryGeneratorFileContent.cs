using SqlGenerator.Core.Enum;
using System.Collections.Generic;
using SqlGenerator.Export.Pdf;

namespace SqlGenerator.File
{
    public class QueryGeneratorFileContent
    {
        public DatabaseType DatabaseType { get; set; }

        public string ConnectionString { get; set; }

        public List<QueryGeneratorFileTableContent> Tables { get; set; }

        public List<QueryGeneratorFileColumnContent> Columns { get; set; }

        public QueryGeneratorFileOptions Options { get; set; }

        public PdfDesign PdfDesign { get; set; }

        public QueryGeneratorFileContent()
        {
            Tables = new List<QueryGeneratorFileTableContent>();
            Columns = new List<QueryGeneratorFileColumnContent>();
            Options = new QueryGeneratorFileOptions();
            PdfDesign = new PdfDesign();
        }
    }
}
