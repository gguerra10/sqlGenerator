using SqlGenerator.Scheme.Base;


namespace SqlGenerator.Scheme.Impl.Sqlite
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class SqliteTableScheme : BaseTableScheme, ITableScheme
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public SqliteTableScheme() : base() { }

        public SqliteTableScheme(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return $"[{Name}]";
        }

        public override bool Equals(object obj)
        {
            var result = false;
            var castedObj = obj as SqliteTableScheme;
            if (castedObj != null)
            {
                result = Name.Equals(castedObj.Name);
            }
            return result;
        }
    }
}
