using SqlGenerator.Class;
using SqlGenerator.Enum;
using SqlGenerator.UI.Enum;
using SqlGenerator.Scheme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SqlGenerator.UI.Extension;

namespace SqlGenerator.UI.Forms
{
    public partial class JoinForm : Form
    {
        private readonly ITableScheme _table;
        private readonly List<ITableScheme> _previousTables;
        public SqlJoin SqlJoin { get; private set; }

        public JoinForm(SqlJoin sqlJoin, ITableScheme table, List<ITableScheme> previousTables)
        {
            _table = table;
            _previousTables = previousTables;
            SqlJoin = sqlJoin;

            InitializeComponent();

            foreach (var item in System.Enum.GetValues(typeof(JoinType)))
            {
                joinTypeCmb.Items.Add(item);
            }
            joinTypeCmb.SelectedItem = sqlJoin.JoinType;

            ConditionsGridViewCreateColumns();
            ConditionsGridViewAddRows();
        }

        private void ConditionsGridViewCreateColumns()
        {

            var fields = new List<string>();
            foreach (var column in _table.Columns)
            {
                fields.Add($"{_table.ToString()}.{column.ToString()}");
            }
            // Grid column (combo type): columns from join table
            DataGridViewComboBoxColumn columnRight = new DataGridViewComboBoxColumn()
            {
                Name = JoinConditionsGridViewColumns.ColumnName.GetName(),
                HeaderText = JoinConditionsGridViewColumns.ColumnName.GetDescription(),
                ValueType = typeof(string),
                DataSource = fields.ToList(),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            };
            conditionsGridView.Columns.Add(columnRight);

            // Grid column (combo type): condition
            DataGridViewComboBoxColumn columnCondition = new DataGridViewComboBoxColumn()
            {
                Name = JoinConditionsGridViewColumns.Condition.GetName(),
                HeaderText = JoinConditionsGridViewColumns.Condition.GetDescription(),
                ValueType = typeof(JoinConditionType),
                DisplayMember = "Display",
                ValueMember = "Value",
                DataSource = System.Enum.GetValues(typeof(JoinConditionType)).OfType<JoinConditionType>().ToList().Select(value => new { Value = value, Display = value.ToString() }).ToList(),
            };
            conditionsGridView.Columns.Add(columnCondition);


            var combinations = new List<string>();
            foreach(var previousTable in _previousTables)
            {
                foreach(var column in previousTable.Columns)
                {
                    combinations.Add($"{previousTable.ToString()}.{column.ToString()}");
                }
            }
            // Grid column (combo type): columns from join table
            DataGridViewComboBoxColumn columnCombination = new DataGridViewComboBoxColumn()
            {
                Name = JoinConditionsGridViewColumns.Combination.GetName(),
                HeaderText = JoinConditionsGridViewColumns.Combination.GetDescription(),
                ValueType = typeof(string),
                DataSource = combinations.ToList(),
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
            };
            conditionsGridView.Columns.Add(columnCombination);
        }

        private void ConditionsGridViewAddRows()
        {
            foreach(var condition in SqlJoin.Conditions)
            {
                var conditionGridViewRow = new DataGridViewRow();
                conditionGridViewRow.CreateCells(conditionsGridView);

                conditionGridViewRow.Cells[JoinConditionsGridViewColumns.ColumnName.GetPosition()].Value = condition.Field;
                conditionGridViewRow.Cells[JoinConditionsGridViewColumns.Condition.GetPosition()].Value = condition.JoinConditionType;
                conditionGridViewRow.Cells[JoinConditionsGridViewColumns.Combination.GetPosition()].Value = condition.Value;

                conditionsGridView.Rows.Add(conditionGridViewRow);
            }
        }

        private void AcceptBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConditionsGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            SqlJoin.Conditions.Clear();
            foreach(DataGridViewRow row in conditionsGridView.Rows)
            {
                if (row.AllCellsHaveValue())
                {
                    var sqlJoinCondition = new SqlJoinCondition()
                    {
                        Field = row.Cells[JoinConditionsGridViewColumns.ColumnName.GetPosition()].Value?.ToString(),
                        JoinConditionType = (JoinConditionType)row.Cells[JoinConditionsGridViewColumns.Condition.GetPosition()].Value,
                        Value = row.Cells[JoinConditionsGridViewColumns.Combination.GetPosition()].Value?.ToString()
                    };
                    SqlJoin.Conditions.Add(sqlJoinCondition);
                }
            }
            txtSqlJoin.Text = SqlJoin.ToString(true);
        }

        private void FilterCmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlJoin.JoinType = (JoinType)joinTypeCmb.SelectedItem;
            txtSqlJoin.Text = SqlJoin.ToString(true);
        }
    }
}
