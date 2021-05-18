
namespace SqlGenerator.Core.Facade.Base
{
    public class BaseColumn : IColumn
    {
        internal string Name { get; }

        public BaseColumn(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"[{Name}]";
        }
    }
}
