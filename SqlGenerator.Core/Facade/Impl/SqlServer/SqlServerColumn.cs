using SqlGenerator.Core.Facade.Base;


namespace SqlGenerator.Core.Facade.Impl.SqlServer
{
    public class SqlServerColumn : BaseColumn, IColumn
    {
        public string DataType { get; set; }
        public SqlServerColumn(string name) : base(name) { }

        public override string ToString()
        {
            return $"[{Name}]";
        }
    }
}
