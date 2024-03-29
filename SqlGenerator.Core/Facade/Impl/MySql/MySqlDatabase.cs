﻿using MySql.Data.MySqlClient;
using SqlGenerator.Core.Facade.Base;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;

namespace SqlGenerator.Core.Facade.Impl.MySql
{
    public class MySqlDatabase : BaseDatabase, IDatabase
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public MySqlDatabase() : base() { }

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="connectionString">Connection string</param>
        public MySqlDatabase(string connectionString) : base(connectionString) { }


        /// <summary>
        /// Execute inner sql in order to populate tables list with their columns.
        /// </summary>
        /// <returns>True if success</returns>
        public bool Load()
        {
            var result = false;
            try
            {

                var connection = new MySqlConnection(ConnectionString);
                connection.Open();
                var sqlScheme = "select tab.table_schema as database_schema, " +
                                " tab.table_name as table_name," +
                                " col.ordinal_position as column_id," +
                                " col.column_name as column_name," +
                                " col.data_type as data_type," +
                                " case when col.numeric_precision is not null" +
                                "     then col.numeric_precision" +
                                "     else col.character_maximum_length end as max_length," +
                                " case when col.datetime_precision is not null" +
                                "     then col.datetime_precision" +
                                "     when col.numeric_scale is not null" +
                                "     then col.numeric_scale" +
                                "     else 0 end as 'precision'" +
                                " from information_schema.tables as tab" +
                                " inner join information_schema.columns as col" +
                                "     on col.table_schema = tab.table_schema" +
                                "     and col.table_name = tab.table_name" +
                                " where tab.table_type = 'BASE TABLE'" +
                                "     and tab.table_schema not in ('information_schema', 'mysql'," +
                                "     'performance_schema', 'sys')" +
                                "     and tab.table_schema = database()" +
                                " order by tab.table_name," +
                                "     col.ordinal_position; ";
                using (var cmd = new MySqlCommand(sqlScheme, connection))
                {
                    var dataReader = cmd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var tableName = dataReader[1].ToString();
                        var columnName = dataReader[3].ToString();

                        var table = new MySqlTable(tableName);
                        if (tables.Any(t => t.Equals(table)))
                        {
                            // Table already exists, add new column
                            var mySqlTable = tables.Find(t => t.Equals(table)) as MySqlTable;
                            if (mySqlTable != null)
                            {
                                mySqlTable.columns.Add(new MySqlColumn(columnName));
                            }
                        }
                        else
                        {
                            // New table, must add table and column
                            table.columns.Add(new MySqlColumn(columnName));
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

        public DataTable ExecuteSql(string sql)
        {
            var dataTable = new DataTable();
            // Execute query against mysql database
            var connection = new MySqlConnection(ConnectionString);
            connection.Open();
            using (var cmd = new MySqlCommand(sql, connection))
            {
                dataTable.Load(cmd.ExecuteReader());
            }
            connection.Close();
            return dataTable;
        }
    }
}
