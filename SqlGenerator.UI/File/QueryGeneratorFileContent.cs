using SqlGenerator.Class;
using SqlGenerator.Enum;
using SqlGenerator.UI.Row;
using System.Collections.Generic;

namespace SqlGenerator.UI.File
{
    public class QueryGeneratorFileContent
    {
        public DatabaseType DatabaseType { get; set; }

        public string ConnectionString { get; set; }

        public List<QueryGeneratorFileTableContent> Tables { get; set; }

        public List<QueryGeneratorFileColumnContent> Columns { get; set; }

        public QueryGeneratorFileContent()
        {
            Tables = new List<QueryGeneratorFileTableContent>();
            Columns = new List<QueryGeneratorFileColumnContent>();
        }
    }
}
