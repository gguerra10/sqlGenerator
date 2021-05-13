using SqlGenerator.Class;
using SqlGenerator.Enum;
using SqlGenerator.Scheme;


namespace SqlGenerator.UI.Row
{
    public class ColumnsGridViewRow
    {
        public ITableScheme Table { get; set; }

        public IColumnScheme Column { get; set; }

        public bool Selected { get; set; }

        public string Alias { get; set; }

        public SqlCondition Condition { get; set; }

        public GroupType Group { get; set; }

        public AggregationType Agreggation { get; set; }

        public OrderType Order { get; set; }
    }
}
