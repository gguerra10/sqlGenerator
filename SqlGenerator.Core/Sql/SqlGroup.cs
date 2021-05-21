

namespace SqlGenerator.Core.Sql
{
    public class SqlGroup
    {
        public string Field { get; }


        public SqlGroup(string field)
        {
            Field = field;
        }

        public override string ToString()
        {
            return Field;
        }
    }
}
