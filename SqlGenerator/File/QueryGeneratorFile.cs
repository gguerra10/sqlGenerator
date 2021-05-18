using Newtonsoft.Json;
using System.IO;


namespace SqlGenerator.File
{
    public class QueryGeneratorFile
    {
        public string FilePath { get; internal set; }

        public QueryGeneratorFileContent Content { get; set; }

        public QueryGeneratorFile()
        {
            Content = new QueryGeneratorFileContent();
        }

        public QueryGeneratorFile(string filePath, QueryGeneratorFileContent content)
        {
            FilePath = filePath;
            Content = content;
        }

        public QueryGeneratorFile(QueryGeneratorFileContent content)
        {
            Content = content;
        }

        public void Load(string filePath)
        {
            FilePath = filePath;

            var content = string.Empty;
            using (var sr = new StreamReader(filePath))
            {
                content = sr.ReadToEnd();
            }
            Content = JsonConvert.DeserializeObject<QueryGeneratorFileContent>(content);
        }

        public void Save(string filePath = null)
        {
            if(filePath != null)
            {
                FilePath = filePath;
            }
            var content = JsonConvert.SerializeObject(Content, Formatting.Indented);
            using (var sw = new StreamWriter(FilePath))
            {
                sw.Write(content);
                sw.Close();
            }
        }
    }
}
