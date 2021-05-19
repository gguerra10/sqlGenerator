using SqlGenerator.Core.Facade.Base;


namespace SqlGenerator.Core.Facade.Impl.Sqlite
{
    public class SqliteColumn : BaseColumn, IColumn
    {
        public SqliteColumn(string name) : base(name) { }

        public override string ToString()
        {
            return $"[{Name}]";
        }
    }
}
