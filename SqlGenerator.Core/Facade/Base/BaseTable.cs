using System.Collections.Generic;


namespace SqlGenerator.Core.Facade.Base
{
    public class BaseTable
    {
        public string Name { get; internal set; }
        public IEnumerable<IColumn> Columns => columns;

        internal List<IColumn> columns;
        public BaseTable()
        {
            columns = new List<IColumn>();
        }
    }
}
