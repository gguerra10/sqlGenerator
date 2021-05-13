using SqlGenerator.Class;
using SqlGenerator.UI.Row;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlGenerator.UI.File
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
