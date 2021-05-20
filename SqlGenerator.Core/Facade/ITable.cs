using System.Collections.Generic;

namespace SqlGenerator.Core.Facade
{
    public interface ITable
    {
        /// <summary>
        /// Columns list belonging to this table
        /// </summary>
        public IEnumerable<IColumn> Columns { get; }

        /// <summary>
        /// Text representation
        /// </summary>
        /// <returns></returns>
        public string ToString();

        /// <summary>
        /// Equal operator
        /// </summary>
        /// <param name="obj">Another ITable</param>
        /// <returns></returns>
        public bool Equals(object obj);
    }
}
