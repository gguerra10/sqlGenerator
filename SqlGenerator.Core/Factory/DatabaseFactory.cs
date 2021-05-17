using SqlGenerator.Core.Enum;
using SqlGenerator.Core.Facade;
using SqlGenerator.Core.Facade.Impl.MySql;
using SqlGenerator.Core.Facade.Impl.Sqlite;
using SqlGenerator.Core.Facade.Impl.SqlServer;

namespace SqlGenerator.Core.Factory
{
    public class DatabaseFactory
    {

        public IDatabase GetDatabase(DatabaseType databaseType)
        {
            IDatabase databaseScheme = null;

            switch(databaseType)
            {
                case DatabaseType.SQLite:
                    databaseScheme = new SqliteDatabase();
                    break;
                case DatabaseType.SQLServer:
                    databaseScheme = new SqlServerDatabase();
                    break;
                case DatabaseType.MySQL:
                    databaseScheme = new MySqlDatabase();
                    break;
                default:
                    break;
            }

            return databaseScheme;
        }
    }
}
