using SqlGenerator.Core.Sql;
using SqlGenerator.Core.Enum;


namespace SqlGenerator.Archive.Content
{
    /// <summary>
    /// Content related to columns data grid view
    /// </summary>
    public class ArchiveColumn
    {
        public string TableName { get; set; }

        public string ColumnName { get; set; }

        public bool Selected { get; set; }

        public string Alias { get; set; }

        public SqlCondition Condition { get; set; }

        public GroupType Group { get; set; }

        public AggregationType Agreggation { get; set; }

        public OrderType Order { get; set; }

    }
}
