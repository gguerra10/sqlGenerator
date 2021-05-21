using SqlGenerator.Core.Sql;


namespace SqlGenerator.Archive.Content
{
    /// <summary>
    /// Content related to tables data grid view
    /// </summary>
    public class ArchiveTable
    {

        public string TableName { get; set; }

        public SqlJoin Join { get; set; }

    }
}
