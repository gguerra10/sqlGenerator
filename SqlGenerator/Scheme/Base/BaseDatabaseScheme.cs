using System.Collections.Generic;

namespace SqlGenerator.Scheme.Base
{
    public class BaseDatabaseScheme
    {
        public string ConnectionString { get; set; }

        public IEnumerable<ITableScheme> Tables => tables;

        internal List<ITableScheme> tables;

        public BaseDatabaseScheme()
        {
            tables = new List<ITableScheme>();
        }

        public BaseDatabaseScheme(string connectionString)
        {
            ConnectionString = connectionString;
            tables = new List<ITableScheme>();
        }
    }
}
