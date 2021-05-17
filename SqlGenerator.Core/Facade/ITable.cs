using System.Collections.Generic;

namespace SqlGenerator.Core.Facade
{
    public interface ITable
    {
        public IEnumerable<IColumn> Columns { get; }

        public string ToString();

        public bool Equals(object obj);
    }
}
