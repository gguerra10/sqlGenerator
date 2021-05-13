using SqlGenerator.Class;
using SqlGenerator.Enum;
using System.Collections.Generic;


namespace SqlGenerator
{
    public class SqlGenerator
    {
        private DatabaseType DatabaseType { get; set; }

        public void SetDatabaseType(DatabaseType databaseType)
        {
            DatabaseType = databaseType;
        }

        public string Select(List<string> selects,
            string table, List<SqlJoin> joins,
            List<SqlCondition> wheres,
            List<string> groups,
            List<SqlOrder> orders,
            int limit = 0)
        {
            var sql = $"SELECT" + "\r\n";

            switch (DatabaseType)
            {
                case DatabaseType.SQLServer:
                    if (limit != 0)
                    {
                        sql += $"TOP \t{limit}\r\n";
                    }
                    break;
                case DatabaseType.SQLite:
                case DatabaseType.MySQL:
                default:
                    break;
            }
            if (selects.Count > 0)
            {
                for (int i = 0; i < selects.Count; i++)
                {
                    if (i != selects.Count - 1)
                    {
                        sql += $"\t{selects[i]},\r\n";
                    }
                    else
                    {
                        sql += $"\t{selects[i]}\r\n";
                    }
                }
            }
            else
            {
                sql += $"\t*\r\n";
            }
            sql += $"FROM\r\n";
            sql += $"\t {table} \r\n";
            foreach (var join in joins)
            {
                sql += $"{join}\r\n";
            }
            if (wheres.Count > 0)
            {
                sql += $"WHERE\r\n";
                for (int i = 0; i < wheres.Count; i++)
                {
                    if (i != wheres.Count - 1)
                    {
                        sql += $"\t{wheres[i]} AND\r\n";
                    }
                    else
                    {
                        sql += $"\t{wheres[i]}\r\n";
                    }
                }
            }
            if (groups.Count > 0)
            {
                sql += $"GROUP BY\r\n";
                for (int i = 0; i < groups.Count; i++)
                {
                    if (i != groups.Count - 1)
                    {
                        sql += $"\t{groups[i]},\r\n";
                    }
                    else
                    {
                        sql += $"\t{groups[i]}\r\n";
                    }
                }
            }
            if (orders.Count > 0)
            {
                sql += $"ORDER BY\r\n";
                for (int i = 0; i < orders.Count; i++)
                {
                    if (i != orders.Count - 1)
                    {
                        sql += $"\t{orders[i]},\r\n";
                    }
                    else
                    {
                        sql += $"\t{orders[i]}\r\n";
                    }
                }
            }
            switch (DatabaseType)
            {
                case DatabaseType.SQLServer:
                    break;
                case DatabaseType.SQLite:
                case DatabaseType.MySQL:
                default:
                    if (limit != 0)
                    {
                        sql += $"LIMIT \r\n\t{limit}\r\n";
                    }
                    break;
            }

            return sql;
        }
    }
}
