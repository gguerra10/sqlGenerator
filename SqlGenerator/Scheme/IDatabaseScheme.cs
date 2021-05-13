using System.Collections.Generic;
using System.Data;

namespace SqlGenerator.Scheme
{
    public interface IDatabaseScheme
    {
        public string ConnectionString { get; set; }

        public IEnumerable<ITableScheme> Tables { get; }

        public void LoadScheme();

        public DataTable ExecuteSql(string sql);
    }
}
