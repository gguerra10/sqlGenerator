using SqlGenerator.Class;
using SqlGenerator.Enum;
using SqlGenerator.UI.Row;
using System;
using System.Collections.Generic;
using System.Text;

namespace SqlGenerator.UI.File
{
    public class QueryGeneratorFileColumnContent
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public bool Selected { get; set; }

        public string Alias { get; set; }

        public SqlCondition Condition { get; set; }

        public GroupType Group { get; set; }

        public AggregationType Agreggation { get; set; }

        public OrderType Order { get; set; }

        public QueryGeneratorFileColumnContent()
        {

        }

        public QueryGeneratorFileColumnContent(ColumnsGridViewRow columnsGridViewRow)
        {
            TableName = columnsGridViewRow.Table.ToString();
            ColumnName = columnsGridViewRow.Column.ToString();
            Selected = columnsGridViewRow.Selected;
            Condition = columnsGridViewRow.Condition;
            Group = columnsGridViewRow.Group;
            Agreggation = columnsGridViewRow.Agreggation;
            Order = columnsGridViewRow.Order;
        }
    }
}
