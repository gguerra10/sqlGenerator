using SqlGenerator.Core.Enum;

namespace SqlGenerator.Core.Sql
{
    public class SqlSelect
    {
        public AggregationType AggregationType { get; set; }

        public string Alias { get; set; }

        public string Field { get; }

        public SqlSelect(string field)
        {
            Field = field;
            AggregationType = AggregationType.None;
        }

        public override string ToString()
        {
            var sql = string.Empty;

            // Add agregation method
            switch (AggregationType)
            {
                case AggregationType.Count:
                    sql += "COUNT(" + Field + ")";
                    break;
                case AggregationType.Sum:
                    sql += "SUM(" + Field + ")";
                    break;
                case AggregationType.Min:
                    sql += "MIN(" + Field + ")";
                    break;
                case AggregationType.Max:
                    sql += "MAX(" + Field + ")";
                    break;
                case AggregationType.Average:
                    sql += "AVG(" + Field + ")";
                    break;
                default:
                    sql += Field;
                    break;
            }

            // Add alias if requested
            if (!string.IsNullOrEmpty(Alias))
            {
                sql += " AS '" + Alias + "'";
            }
            return sql;
        }
    }
}
