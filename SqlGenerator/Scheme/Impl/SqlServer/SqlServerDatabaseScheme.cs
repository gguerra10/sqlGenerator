using SqlGenerator.Scheme.Base;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace SqlGenerator.Scheme.Impl.SqlServer
{
    public class SqlServerDatabaseScheme : BaseDatabaseScheme, IDatabaseScheme
    {
        public SqlServerDatabaseScheme() : base() { }
        public SqlServerDatabaseScheme(string connectionString) : base(connectionString) { }


        /// <summary>
        /// Load scheme from SqlServer database
        /// </summary>
        /// <returns></returns>
        public void LoadScheme()
        {
            // Read database scheme from sqlite database
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

                    var table = new SqlServerTableScheme(schemaName, tableName);
                    if (tables.Any(t => t.Equals(table)))
                    {
                        // Table already exists, add new column
                        var sqlserverTable = tables.Find(t => t.Equals(table)) as SqlServerTableScheme;
                        if (sqlserverTable != null)
                        {
                            sqlserverTable.columns.Add(new ColumnScheme(columnName));
                        }
                    }
                    else
                    {
                        // New table, must add table and column
                        table.columns.Add(new ColumnScheme(columnName));
                        tables.Add(table);
                    }
                }
            }
            connection.Close();
        }

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
