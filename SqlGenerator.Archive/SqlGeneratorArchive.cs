using Newtonsoft.Json;
using System.IO;


namespace SqlGenerator.Archive
{
    public class SqlGeneratorArchive
    {

        /// <summary>
        /// File path
        /// </summary>
        public string FilePath { get; internal set; }

        /// <summary>
        /// Data saved in file
        /// </summary>
        public SqlGeneratorArchiveData Data { get; set; }

        /// <summary>
        /// CTOR
        /// </summary>
        public SqlGeneratorArchive()
        {
            Data = new SqlGeneratorArchiveData();
        }


        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="content">Data to save</param>
        public SqlGeneratorArchive(string filePath, SqlGeneratorArchiveData content)
        {
            FilePath = filePath;
            Data = content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        public SqlGeneratorArchive(SqlGeneratorArchiveData content)
        {
            Data = content;
        }

        /// <summary>
        /// Load data from file path
        /// </summary>
        /// <param name="filePath">File path</param>
        public void Load(string filePath)
        {
            FilePath = filePath;

            var content = string.Empty;
            using (var sr = new StreamReader(filePath))
            {
                content = sr.ReadToEnd();
                sr.Close();
            }
            Data = JsonConvert.DeserializeObject<SqlGeneratorArchiveData>(content);
        }

        /// <summary>
        /// Save data to file path
        /// </summary>
        /// <param name="filePath"></param>
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
