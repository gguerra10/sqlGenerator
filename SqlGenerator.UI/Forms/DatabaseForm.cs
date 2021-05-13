using SqlGenerator.Enum;
using SqlGenerator.Scheme;
using System;
using System.Windows.Forms;

namespace SqlGenerator.UI.Forms
{
    public partial class DatabaseForm : Form
    {
        public IDatabaseScheme DatabaseScheme { get; private set; }

        public DatabaseType DatabaseType { get; private set; }

        public DatabaseForm()
        {
            InitializeComponent();

            foreach (var item in System.Enum.GetValues(typeof(DatabaseType)))
            {
                databaseTypeCmb.Items.Add(item);
            }
            databaseTypeCmb.SelectedItem = DatabaseType.SQLite;
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
                DatabaseScheme = new DatabaseSchemeFactory().GetDatabaseScheme((DatabaseType)databaseTypeCmb.SelectedItem);
                DatabaseScheme.ConnectionString = connectionStringTxt.Text;
                DatabaseScheme.LoadScheme();
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
