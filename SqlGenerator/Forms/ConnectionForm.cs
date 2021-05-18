using SqlGenerator.Enum;
using System;
using System.Windows.Forms;
using SqlGenerator.Core.Enum;
using SqlGenerator.Core.Facade;
using SqlGenerator.Core.Factory;

namespace SqlGenerator.Forms
{
    public partial class ConnectionForm : Form
    {
        public IDatabase Database { get; private set; }

        public DatabaseType DatabaseType { get; private set; }

        public ConnectionForm()
        {
            InitializeComponent();

            foreach (var item in System.Enum.GetValues(typeof(DatabaseType)))
            {
                databaseTypeCmb.Items.Add(item);
            }
            databaseTypeCmb.SelectedItem = DatabaseType.SQLite;
        }

        public ConnectionForm(DatabaseType databaseType, string connectionString)
        {
            InitializeComponent();

            foreach (var item in System.Enum.GetValues(typeof(DatabaseType)))
            {
                databaseTypeCmb.Items.Add(item);
            }
            databaseTypeCmb.SelectedItem = databaseType;
            connectionStringTxt.Text = connectionString;
        }

        private void DatabaseTypeCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseType = (DatabaseType)databaseTypeCmb.SelectedItem;
            switch (DatabaseType)
            {
                case DatabaseType.SQLite:
                    browseBtn.Visible = true;
                    break;
                case DatabaseType.SQLServer:
                    browseBtn.Visible = false;
                    break;
                case DatabaseType.MySQL:
                    browseBtn.Visible = false;
                    break;
                default:
                    break;
            }
            openBtn.Enabled = false;
            connectionStringTxt.Text = string.Empty;
        }

        private void ConnectionStringTxt_TextChanged(object sender, EventArgs e)
        {
            openBtn.Enabled = false;
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            // Open file dialog to pick database scheme in filesystem
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Sqlite database (*.db)|*.db",
                InitialDirectory = "./",
                Multiselect = false
            };
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Fill connection string
                connectionStringTxt.Text = $"Data Source =\"{openFileDialog.FileName}\"";
            }
        }

        private void TestBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Database = new DatabaseFactory().GetDatabase((DatabaseType)databaseTypeCmb.SelectedItem);
                Database.ConnectionString = connectionStringTxt.Text;
                Database.Load();
                openBtn.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
