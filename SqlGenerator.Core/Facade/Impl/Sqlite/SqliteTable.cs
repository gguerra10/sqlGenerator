using SqlGenerator.Core.Facade.Base;


namespace SqlGenerator.Core.Facade.Impl.Sqlite
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class SqliteTable : BaseTable, ITable
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public SqliteTable() : base() { }

        public SqliteTable(string name)
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
            var castedObj = obj as SqliteTable;
            if (castedObj != null)
            {
                result = Name.Equals(castedObj.Name);
            }
            return result;
        }
    }
}
