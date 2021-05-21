using SqlGenerator.Core.Sql;
using SqlGenerator.Core.Enum;
using System.Collections.Generic;
using System.Linq;

namespace SqlGenerator
{
    public class SqlGenerator
    {
        private DatabaseType DatabaseType { get; set; }

        public void SetDatabaseType(DatabaseType databaseType)
        {
            DatabaseType = databaseType;
        }

        public string Select(
            IEnumerable<SqlSelect> selects,
            string table, 
            IEnumerable<SqlJoin> joins,
            IEnumerable<SqlCondition> wheres,
            IEnumerable<SqlGroup> groups,
            IEnumerable<SqlOrder> orders,
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
            if (selects.Any())
            {
                foreach (var select in selects)
                {
                    if(!select.Equals(selects.Last()))
                    {
                        sql += $"\t{select},\r\n";
                    }
                    else
                    {
                        sql += $"\t{select}\r\n";
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
            if (wheres.Any())
            {
                sql += $"WHERE\r\n";
                foreach (var where in wheres)
                {
                    if (!where.Equals(wheres.Last()))
                    {
                        sql += $"\t{where} AND\r\n";
                    }
                    else
                    {
                        sql += $"\t{where}\r\n";
                    }
                }
            }
            if (groups.Any())
            {
                sql += $"GROUP BY\r\n";
                foreach (var group in groups)
                {
                    if (!group.Equals(groups.Last()))
                    {
                        sql += $"\t{group},\r\n";
                    }
                    else
                    {
                        sql += $"\t{group}\r\n";
                    }
                }
            }
            if (orders.Any())
            {
                sql += $"ORDER BY\r\n";
                foreach (var order in orders)
                {
                    if (!order.Equals(orders.Last()))
                    {
                        sql += $"\t{order},\r\n";
                    }
                    else
                    {
                        sql += $"\t{order}\r\n";
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


        /// <summary>
        /// Check aggregation, if there is any group or aggregate method then the rest of fiels must be appear in group clause or be used in aggregate method
        /// </summary>
        /// <param name="selects"></param>
        /// <param name="groups"></param>
        public void SelectAggregationCheck(
            IEnumerable<SqlSelect> selects,
            IEnumerable<SqlGroup> groups)
        {
            // Aggregation check if there is any group or any aggregate method
            if (selects.Any(s => s.AggregationType != AggregationType.None) ||
                groups.Any())
            {
                // Iterate selected fields
                foreach (var select in selects)
                {
                    // Check
                    if (select.AggregationType == AggregationType.None && 
                        !groups.Any(g => g.Field.Equals(select.Field)))
                    {
                        throw new System.Exception("Selected column '" + select.Field + "' must appear in the group by clause or be used in an aggregate method");
                    }
                }
            }
        }
    }
}
