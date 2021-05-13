using SqlGenerator.Scheme.Base;


namespace SqlGenerator.Scheme.Impl.SqlServer
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class SqlServerTableScheme : BaseTableScheme, ITableScheme
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public SqlServerTableScheme() : base() { }

        internal readonly string Scheme;
        public SqlServerTableScheme(string scheme, string name)
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
            var castedObj = obj as SqlServerTableScheme;
            if (castedObj != null)
            {
                result = Name.Equals(castedObj.Name) && Scheme.Equals(castedObj.Scheme);
            }
            return result;
        }
    }
}
