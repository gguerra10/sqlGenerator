using SqlGenerator.Core.Enum;


namespace SqlGenerator.Core.Sql
{
    public class SqlOrder
    {
        public OrderType OrderType { get; set; }

        public string Field { get; }


        public SqlOrder(string field)
        {
            Field = field;
            OrderType = OrderType.None;
        }

        public override string ToString()
        {
            var result = string.Empty;

            if (OrderType != OrderType.None)
            {
                result = $"{Field}";
                switch (OrderType)
                {
                    case OrderType.Ascending:
                        result += " ASC";
                        break;
                    case OrderType.Descending:
                        result += " DESC";
                        break;
                }
            }
            return result;
        }
    }
}
