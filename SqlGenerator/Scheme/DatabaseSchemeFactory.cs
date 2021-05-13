using SqlGenerator.Enum;
using SqlGenerator.Scheme.Impl;
using SqlGenerator.Scheme.Impl.MySql;
using SqlGenerator.Scheme.Impl.Sqlite;
using SqlGenerator.Scheme.Impl.SqlServer;

namespace SqlGenerator.Scheme
{
    public class DatabaseSchemeFactory
    {

        public IDatabaseScheme GetDatabaseScheme(DatabaseType databaseType)
        {
            IDatabaseScheme databaseScheme = null;

            switch(databaseType)
            {
                case DatabaseType.SQLite:
                    databaseScheme = new SqliteDatabaseScheme();
                    break;
                case DatabaseType.SQLServer:
                    databaseScheme = new SqlServerDatabaseScheme();
                    break;
                case DatabaseType.MySQL:
                    databaseScheme = new MySqlDatabaseScheme();
                    break;
                default:
                    break;
            }

            return databaseScheme;
        }
    }
}
