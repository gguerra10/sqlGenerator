using SqlGenerator.Scheme.Base;
using System;
using System.Data;

namespace SqlGenerator.Scheme.Impl.MySql
{
    public class MySqlDatabaseScheme : BaseDatabaseScheme, IDatabaseScheme
    {
        public MySqlDatabaseScheme() : base() { }
        public MySqlDatabaseScheme(string connectionString) : base(connectionString) { }

        public DataTable ExecuteSql(string sql)
        {
            throw new NotImplementedException();
        }

        public void LoadScheme()
        {
            throw new NotImplementedException();
        }
    }
}
