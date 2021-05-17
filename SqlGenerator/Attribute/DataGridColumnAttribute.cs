using System.Windows.Forms;

namespace SqlGenerator.Attribute
{
    public class DataGridColumnAttribute : System.Attribute
    {
        public string Name { get; }
        public int Position { get; }

        private string _description { get; set; }
        public string Description
        {
            get
            {
                if (!string.IsNullOrEmpty(_description)) return _description;
                else return Name;
            }
            set
            {
                _description = value;
            }
        }
        public DataGridColumnAttribute(string name, int position)
        {
            Name = name;
            Position = position;
        }
    }
}
