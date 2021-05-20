using SqlGenerator.Core.Facade.Base;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;

namespace SqlGenerator.Core.Facade.Impl.SqlServer
{
    public class SqlServerDatabase : BaseDatabase, IDatabase
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public SqlServerDatabase() : base() { }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public SqlServerDatabase(string connectionString) : base(connectionString) { }


        /// <summary>
        /// Execute inner sql in order to populate tables list with their columns.
        /// </summary>
        /// <returns>True if success</returns>
        public bool Load()
        {
            var result = false;
            try
            {
                // Read database scheme from sql server database
                var connection = new SqlConnection(ConnectionString);
                connection.Open();

                var sqlScheme = $"SELECT " +
                    "t.table_schema as schema_name, " +
                    "t.table_name as table_name, " +
                    "c.column_name as column_name, " +
                    "DataType = data_type " +
                    "FROM information_schema.columns c " +
                    "JOIN information_schema.tables t " +
                    "  ON c.table_name = t.table_name " +
                    "  AND c.table_schema = t.table_schema " +
                    "  AND t.table_type = 'BASE TABLE' " +
                    " WHERE c.data_type NOT IN('geography', 'hierarchyid') " + // Cannot represent these datatype
                    "ORDER BY table_name, ordinal_position";
                using (var cmd = new SqlCommand(sqlScheme, connection))
                {
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var schemaName = dataReader[0].ToString();
                        var tableName = dataReader[1].ToString();
                        var columnName = dataReader[2].ToString();
                        var columnDataType = dataReader[3].ToString();

                        var table = new SqlServerTable(schemaName, tableName);
                        if (tables.Any(t => t.Equals(table)))
                        {
                            // Table already exists, add new column
                            var sqlserverTable = tables.Find(t => t.Equals(table)) as SqlServerTable;
                            if (sqlserverTable != null)
                            {
                                sqlserverTable.columns.Add(new SqlServerColumn(columnName)
                                {
                                    DataType = columnDataType,
                                });
                            }
                        }
                        else
                        {
                            // New table, must add table and column
                            table.columns.Add(new SqlServerColumn(columnName)
                            {
                                DataType = columnDataType,
                            });
                            tables.Add(table);
                        }
                    }
                }
                connection.Close();
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Loading database exception: " + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// Execute query against SqlServer database
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteSql(string sql)
        {
            var dataTable = new DataTable();
            // Execute query against sqlite database
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = new SqlCommand(sql, connection))
            {
                dataTable.Load(cmd.ExecuteReader());
            }
            connection.Close();
            return dataTable;
        }
    }
}
