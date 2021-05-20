using SqlGenerator.Core.Enum;
using SqlGenerator.Core.Facade;
using SqlGenerator.Core.Facade.Impl.MySql;
using SqlGenerator.Core.Facade.Impl.Sqlite;
using SqlGenerator.Core.Facade.Impl.SqlServer;

namespace SqlGenerator.Core.Factory
{
    public class DatabaseFactory
    {

        /// <summary>
        /// Get a new database instance according database type
        /// </summary>
        /// <param name="databaseType">Database type</param>
        /// <param name="connectionString">Connection string</param>
        /// <returns>IDatabase object</returns>
        public IDatabase GetDatabase(DatabaseType databaseType, string connectionString)
        {
            IDatabase database = null;

            switch(databaseType)
            {
                case DatabaseType.SQLite:
                    database = new SqliteDatabase(connectionString);
                    break;
                case DatabaseType.SQLServer:
                    database = new SqlServerDatabase(connectionString);
                    break;
                case DatabaseType.MySQL:
                    database = new MySqlDatabase(connectionString);
                    break;
                default:
                    break;
            }
            return database;
        }
    }
}
