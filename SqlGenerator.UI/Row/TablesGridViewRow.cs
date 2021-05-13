using SqlGenerator.Class;
using SqlGenerator.Scheme;

namespace SqlGenerator.UI.Row
{
    public class TablesGridViewRow
    {
        public ITableScheme Table { get; set; }

        public SqlJoin Join { get; set; }
    }
}
