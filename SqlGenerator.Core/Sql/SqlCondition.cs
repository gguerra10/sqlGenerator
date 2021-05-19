using SqlGenerator.Core.Enum;


namespace SqlGenerator.Core.Sql
{
    public class SqlCondition
    {
        public ConditionType ConditionType { get; set; }

        public string Field { get; }
        
        public string Value { get; set; }

        public string Value2 { get; set; }

        public SqlCondition(string field)
        {
            Field = field;
            ConditionType = ConditionType.None;
        }

        public override string ToString()
        {
            var result = string.Empty;

            if (ConditionType != ConditionType.None)
            {
                result = $"{Field}";
                switch (ConditionType)
                {
                    case ConditionType.Equals:
                        result += " = " + Value;
                        break;
                    case ConditionType.Different:
                        result += " <> " + Value;
                        break;
                    case ConditionType.GreaterThan:
                        result += " > " + Value;
                        break;
                    case ConditionType.GreaterEqualThan:
                        result += " >= " + Value;
                        break;
                    case ConditionType.LessThan:
                        result += " < " + Value;
                        break;
                    case ConditionType.LessEqualThan:
                        result += " <= " + Value;
                        break;
                    case ConditionType.Between:
                        result += " BETWEEN " + Value + " AND " + Value2;
                        break;
                    case ConditionType.NotBetween:
                        result += " NOT BETWEEN " + Value + " AND " + Value2;
                        break;
                    case ConditionType.ClauseIn:
                        result += " IN ( " + Value + ")";
                        break;
                    case ConditionType.Like:
                        result += " LIKE '%" + Value + "%'";
                        break;
                    case ConditionType.NotLike:
                        result += " NOT LIKE '%" + Value + "%'";
                        break;
                    default:
                        break;
                }
            }
            return result;
        }
    }
}
