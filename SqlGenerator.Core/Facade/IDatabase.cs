using System.Collections.Generic;
using System.Data;

namespace SqlGenerator.Core.Facade
{
    public interface IDatabase
    {
        public string ConnectionString { get; set; }

        public IEnumerable<ITable> Tables { get; }

        public bool Load();

        public DataTable ExecuteSql(string sql);
    }
}
