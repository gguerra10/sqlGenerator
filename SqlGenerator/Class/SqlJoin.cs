using SqlGenerator.Enum;
using System.Collections.Generic;

namespace SqlGenerator.Class
{
    public class SqlJoin
    {
        public JoinType JoinType { get; set; }
        public string Table { get; set; }
        public List<SqlJoinCondition> Conditions { get; set; }

        public SqlJoin()
        {
            JoinType = JoinType.Join;
            Conditions = new List<SqlJoinCondition>();
        }
        public override string ToString()
        {
            var sql = string.Empty;
            switch (JoinType)
            {
                case JoinType.Join:
                    sql += "JOIN\r\n";
                    break;
                case JoinType.LeftJoin:
                    sql += "LEFT JOIN\r\n";
                    break;
            }
            sql += $"\t{Table}";
            if (Conditions.Count > 0)
            {
                sql += " ON ";
                for (int i = 0; i < Conditions.Count; i++)
                {
                    if (i != Conditions.Count - 1)
                    {
                        sql += $"{Conditions[i]} AND ";
                    }
                    else
                    {
                        sql += $"{Conditions[i]}";
                    }
                }
            }
            return sql;
        }

        public string ToString(bool oneLine)
        {
            if(oneLine)
            {
                return ToString().Replace("\r\n", " ").Replace("\t", "");
            }
            else
            {
                return ToString();
            }
        }
    }
}
