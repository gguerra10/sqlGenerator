using Microsoft.Data.Sqlite;
using SqlGenerator.Core.Facade.Base;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace SqlGenerator.Core.Facade.Impl.Sqlite
{
    public class SqliteDatabase : BaseDatabase, IDatabase
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public SqliteDatabase() : base() { }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public SqliteDatabase(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Execute inner sql in order to populate tables list with their columns.
        /// </summary>
        /// <returns>True if success</returns>
        public bool Load()
        {
            var result = false;
            try
            {
                // Read database scheme from sqlite database
                var connection = new SqliteConnection(ConnectionString);
                connection.Open();

                var sqlScheme = $"SELECT " +
                        "m.name as table_name, " +
                        "p.name as column_name " +
                        " FROM sqlite_master " +
                        " AS m JOIN pragma_table_info(m.name) AS p WHERE m.name NOT LIKE 'sqlite%' " +
                        " ORDER BY  m.name, p.cid";

                using (var cmd = new SqliteCommand(sqlScheme, connection))
                {
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var tableName = dataReader[0].ToString();
                        var columnName = dataReader[1].ToString();

                        var table = new SqliteTable(tableName);
                        if (tables.Any(t => t.Equals(table)))
                        {
                            // Table already exists, add new column
                            var sqliteTable = tables.Find(t => t.Equals(table)) as SqliteTable;
                            if (sqliteTable != null)
                            {
                                sqliteTable.columns.Add(new SqliteColumn(columnName));
                            }
                        }
                        else
                        {
                            // New table, must add table and column
                            table.columns.Add(new SqliteColumn(columnName));
                            tables.Add(table);
                        }
                    }
                }
                connection.Close();
                result = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Loading database exception: " + ex.Message);
            }
            return result;
        }


        public DataTable ExecuteSql(string sql)
        {
            var dataTable = new DataTable();
            // Execute query against sqlite database
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using (var cmd = new SqliteCommand(sql, connection))
            {
                dataTable.Load(cmd.ExecuteReader());
            }
            connection.Close();
            return dataTable;
        }
    }
}
