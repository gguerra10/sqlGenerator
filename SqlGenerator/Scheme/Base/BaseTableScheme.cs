using System.Collections.Generic;


namespace SqlGenerator.Scheme.Base
{
    public class BaseTableScheme
    {
        public string Name { get; internal set; }
        public IEnumerable<IColumnScheme> Columns => columns;

        internal List<IColumnScheme> columns;
        public BaseTableScheme()
        {
            columns = new List<IColumnScheme>();
        }
    }
}
