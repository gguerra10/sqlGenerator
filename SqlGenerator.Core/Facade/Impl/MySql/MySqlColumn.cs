using SqlGenerator.Core.Facade.Base;


namespace SqlGenerator.Core.Facade.Impl.MySql
{
    public class MySqlColumn : BaseColumn, IColumn
    {
        public MySqlColumn(string name) : base(name) { }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
