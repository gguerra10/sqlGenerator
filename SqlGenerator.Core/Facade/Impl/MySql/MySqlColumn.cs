using SqlGenerator.Core.Facade.Base;
using System;
using System.Collections.Generic;
using System.Text;

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
