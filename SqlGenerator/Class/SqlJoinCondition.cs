using SqlGenerator.Enum;


namespace SqlGenerator.Class
{
    public class SqlJoinCondition
    {
        public JoinConditionType JoinConditionType { get; set; }

        public string Field { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            string result = $"{Field}";
            switch (JoinConditionType)
            {
                case JoinConditionType.Equals:
                    result += " = " + Value;
                    break;
                /*
                case JoinConditionType.GreaterThan:
                    result += " > " + Value;
                    break;
                case JoinConditionType.GreaterEqualThan:
                    result += " >= " + Value;
                    break;
                case JoinConditionType.LessThan:
                    result += " < " + Value;
                    break;
                case JoinConditionType.LessEqualThan:
                    result += " <= " + Value;
                    break;
                */
                default:
                    break;
            }
            return result;
        }
    }
}
