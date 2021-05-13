
namespace SqlGenerator.Scheme.Base
{
    public class ColumnScheme : IColumnScheme
    {
        internal string Name { get; }

        public ColumnScheme(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"[{Name}]";
        }
    }
}
