using SqlGenerator.Core.Facade.Base;


namespace SqlGenerator.Core.Facade.Impl.MySql
{
#pragma warning disable CS0659 // El tipo reemplaza a Object.Equals(object o), pero no reemplaza a Object.GetHashCode()
    public class MySqlTable : BaseTable, ITable
#pragma warning restore CS0659 // El tipo reemplaza a Object.Equals(object o), pero no reemplaza a Object.GetHashCode()
    {
        public MySqlTable() : base() { }

        public MySqlTable(string name)
        {
            Name = name;
        }
        public override string ToString()
        {
            return $"{Name}";
        }

        public override bool Equals(object obj)
        {
            var result = false;
            var castedObj = obj as MySqlTable;
            if (castedObj != null)
            {
                result = Name.Equals(castedObj.Name);
            }
            return result;
        }
    }
}
