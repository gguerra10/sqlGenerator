using SqlGenerator.Core.Enum;
using SqlGenerator.Core.Facade;
using SqlGenerator.Core.Sql;

namespace SqlGenerator.Row
{
    public class ColumnsGridViewRow
    {
        public ITable Table { get; set; }

        public IColumn Column { get; set; }

        public bool Selected { get; set; }

        public string Alias { get; set; }

        public SqlCondition Condition { get; set; }

        public GroupType Group { get; set; }

        public AggregationType Agreggation { get; set; }

        public OrderType Order { get; set; }
    }
}
