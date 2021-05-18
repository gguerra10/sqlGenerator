using System.Collections.Generic;

namespace SqlGenerator.Core.Facade.Base
{
    public class BaseDatabase
    {
        public string ConnectionString { get; set; }

        public IEnumerable<ITable> Tables => tables;

        internal List<ITable> tables;

        public BaseDatabase()
        {
            tables = new List<ITable>();
        }

        public BaseDatabase(string connectionString)
        {
            ConnectionString = connectionString;
            tables = new List<ITable>();
        }
    }
}
