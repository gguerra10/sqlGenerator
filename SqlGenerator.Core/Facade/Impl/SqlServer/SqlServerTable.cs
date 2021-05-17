using SqlGenerator.Core.Facade.Base;


namespace SqlGenerator.Core.Facade.Impl.SqlServer
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class SqlServerTable : BaseTable, ITable
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public SqlServerTable() : base() { }

        internal readonly string Scheme;
        public SqlServerTable(string scheme, string name)
        {
            Scheme = scheme;
            Name = name;
        }
        public override string ToString()
        {
            return $"[{Scheme}].[{Name}]";
        }

        public override bool Equals(object obj)
        {
            var result = false;
            var castedObj = obj as SqlServerTable;
            if (castedObj != null)
            {
                result = Name.Equals(castedObj.Name) && Scheme.Equals(castedObj.Scheme);
            }
            return result;
        }
    }
}
