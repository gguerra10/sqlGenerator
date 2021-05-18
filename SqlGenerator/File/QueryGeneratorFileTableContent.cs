using SqlGenerator.Core.Sql;
using SqlGenerator.Row;


namespace SqlGenerator.File
{
    public class QueryGeneratorFileTableContent
    {

        public string TableName { get; set; }

        public SqlJoin Join { get; set; }

        public QueryGeneratorFileTableContent()
        {

        }

        public QueryGeneratorFileTableContent(TablesGridViewRow tablesGridViewRow)
        {
            TableName = tablesGridViewRow.Table.ToString();
            Join = tablesGridViewRow.Join;
        }
    }
}
