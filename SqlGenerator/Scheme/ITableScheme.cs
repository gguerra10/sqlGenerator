using System.Collections.Generic;

namespace SqlGenerator.Scheme
{
    public interface ITableScheme
    {
        public IEnumerable<IColumnScheme> Columns { get; }

        public string ToString();

        public bool Equals(object obj);
    }
}
