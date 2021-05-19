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
            IDatabase database = null;

            switch(databaseType)
            {
                case DatabaseType.SQLite:
                    database = new SqliteDatabase();
                    break;
                case DatabaseType.SQLServer:
                    database = new SqlServerDatabase();
                    break;
                case DatabaseType.MySQL:
                    database = new MySqlDatabase();
                    break;
                default:
                    break;
            }

            return database;
        }
    }
}
