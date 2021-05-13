using SqlGenerator.Class;
using SqlGenerator.Enum;
using System;
using System.Windows.Forms;

namespace SqlGenerator.UI.Forms
{
    public partial class FilterForm : Form
    {
        public SqlCondition SqlCondition { get; private set; }

        public FilterForm(SqlCondition sqlCondition)
        {
            InitializeComponent();

            acceptBtn.Enabled = false;
            SqlCondition = sqlCondition;

            groupBox1.Text = "Filtering " + SqlCondition.Field;
            foreach (var item in System.Enum.GetValues(typeof(ConditionType)))
            {
                filterCmb.Items.Add(item);
            }

            filterCmb.SelectedItem = SqlCondition.ConditionType;
            txtValue.Text = SqlCondition.Value;
            txtValue2.Text = SqlCondition.Value2;
        }

        private void FilterCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            acceptBtn.Enabled = filterCmb.SelectedItem != null;
            switch((ConditionType)(filterCmb.SelectedItem))
            {
                case ConditionType.Between:
                case ConditionType.NotBetween:
                    txtValue2.Visible = true;
                    break;
                default:
                    txtValue2.Visible = false;
                    break;
            }
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            bool filterOk;
            switch ((ConditionType)(filterCmb.SelectedItem))
            {
                case ConditionType.None:
                    filterOk = true;
                    break;
                case ConditionType.Between:
                case ConditionType.NotBetween:
                    filterOk = txtValue.Text.Length > 0 && txtValue2.Text.Length > 0;
                    break;
                default:
                    filterOk = txtValue.Text.Length > 0;
                    break;
            }
            if (filterOk)
            {
                FillCondition();
                Close();
            }
            else
            {
                MessageBox.Show("Filter has some error", "Error");
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FillCondition()
        {
            SqlCondition.ConditionType = (ConditionType)filterCmb.SelectedItem;
            SqlCondition.Value = txtValue.Text;
            SqlCondition.Value2 = txtValue2.Text;
        }
    }
}
