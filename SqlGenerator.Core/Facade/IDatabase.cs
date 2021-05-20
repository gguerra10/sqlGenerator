using System.Collections.Generic;
using System.Data;

namespace SqlGenerator.Core.Facade
{
    public interface IDatabase
    {
        /// <summary>
        /// Connection string used to link with database.
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Execute inner sql in order to populate tables list with their columns.
        /// </summary>
        /// <returns>True if succeded</returns>
        public bool Load();

        /// <summary>
        /// Tables list belonging to this database.
        /// </summary>
        public IEnumerable<ITable> Tables { get; }

        /// <summary>
        /// Execute SQL sentence against database.
        /// </summary>
        /// <param name="sql">Structured Query Language Sentence</param>
        /// <returns>Data seleted from database</returns>
        public DataTable ExecuteSql(string sql);
    }
}
