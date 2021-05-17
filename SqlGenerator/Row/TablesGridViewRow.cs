using SqlGenerator.Core.Sql;
using SqlGenerator.Core.Facade;

namespace SqlGenerator.Row
{
    public class TablesGridViewRow
    {
        public ITable Table { get; set; }

        public SqlJoin Join { get; set; }
    }
}
