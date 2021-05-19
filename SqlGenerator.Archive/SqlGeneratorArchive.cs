using Newtonsoft.Json;
using System.IO;


namespace SqlGenerator.Archive
{
    public class SqlGeneratorArchive
    {
        public string FilePath { get; internal set; }

        public SqlGeneratorArchiveData Data { get; set; }

        public SqlGeneratorArchive()
        {
            Data = new SqlGeneratorArchiveData();
        }

        public SqlGeneratorArchive(string filePath, SqlGeneratorArchiveData content)
        {
            FilePath = filePath;
            Data = content;
        }

        public SqlGeneratorArchive(SqlGeneratorArchiveData content)
        {
            Data = content;
        }

        public void Load(string filePath)
        {
            FilePath = filePath;

            var content = string.Empty;
            using (var sr = new StreamReader(filePath))
            {
                content = sr.ReadToEnd();
            }
            Data = JsonConvert.DeserializeObject<SqlGeneratorArchiveData>(content);
        }

        public void Save(string filePath = null)
        {
            if(filePath != null)
            {
                FilePath = filePath;
            }
            var content = JsonConvert.SerializeObject(Data, Formatting.Indented);
            using (var sw = new StreamWriter(FilePath))
            {
                sw.Write(content);
                sw.Close();
            }
        }
    }
}
