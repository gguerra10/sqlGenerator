using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SqlGenerator.File;
using SqlGenerator.Extension;
using SqlGenerator.Row;
using SqlGenerator.Core.Facade;
using SqlGenerator.Core.Factory;
using SqlGenerator.Core.Enum;
using SqlGenerator.Core.Sql;
using SqlGenerator.Export.Facade;
using SqlGenerator.Enum;
using SqlGenerator.Export.Facade.Impl.Csv;
using SqlGenerator.Export.Facade.Impl.Excel;

namespace SqlGenerator.Forms
{
    public partial class SelectorForm : Form
    {
        private SqlGenerator sqlGenerator;
        private QueryGeneratorFile _queryGeneratorFile;

        private IDatabase _database;
        private IExporter _exporter;

        /// <summary>
        /// CTOR
        /// </summary>
        public SelectorForm()
        {
            InitializeComponent();


            sqlGenerator = new SqlGenerator();

            // Generate grid views
            ColumnsGridViewCreateColumns();
            TablesGridViewCreateColumns();
            newToolStripMenuItem.Click += NewToolStripMenuItem_Click;
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            editToolStripMenuItem.Click += EditToolStripMenuItem_Click;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            excelToolStripMenuItem.Click += ExcelToolStripMenuItem_Click;
            csvToolStripMenuItem.Click += CsvToolStripMenuItem_Click;
            pdfToolStripMenuItem.Click += PdfToolStripMenuItem_Click;
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;

            // Enable / disable menu items
            newToolStripMenuItem.Enabled = true;
            openToolStripMenuItem.Enabled = true;
            editToolStripMenuItem.Enabled = false;
            saveToolStripMenuItem.Enabled = false;
            saveAsToolStripMenuItem.Enabled = false;
            exportToolStripMenuItem.Enabled = false;

            mainSplitContainer.Visible = false;
        }


        #region File Menu
        private void NewConnection()
        {
            // Open Connection Form to set connection string
            var connectionForm = new ConnectionForm()
            {
                StartPosition = FormStartPosition.CenterParent
            };
            var result = connectionForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                _database = connectionForm.Database;
                _queryGeneratorFile = new QueryGeneratorFile();
                _queryGeneratorFile.Content.DatabaseType = connectionForm.DatabaseType;
                _queryGeneratorFile.Content.ConnectionString = _database.ConnectionString;

                sqlGenerator.SetDatabaseType(connectionForm.DatabaseType);

                DatabaseTreeViewAddNodes();

                // Enable / disable menu items
                editToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = false;
                mainSplitContainer.Visible = true;
            }
        }

        private void EditConnection()
        {
            // Open Connection Form to set connection string
            var connectionForm = new ConnectionForm(
                _queryGeneratorFile.Content.DatabaseType,
                _queryGeneratorFile.Content.ConnectionString
                )
            {
                StartPosition = FormStartPosition.CenterParent
            };
            var result = connectionForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                _database = connectionForm.Database;

                _queryGeneratorFile.Content.DatabaseType = connectionForm.DatabaseType;
                _queryGeneratorFile.Content.ConnectionString = _database.ConnectionString;

                sqlGenerator.SetDatabaseType(connectionForm.DatabaseType);

                DatabaseTreeViewAddNodes();
            }
        }

        private void OpenFile()
        {
            // Open file dialog to pick query file
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Query Generator File (*.qgf)|*.qgf",
                InitialDirectory = "./",
                Multiselect = false
            };
            var result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {

                try
                {
                    _queryGeneratorFile = new QueryGeneratorFile();
                    _queryGeneratorFile.Load(openFileDialog.FileName);

                    _database = new DatabaseFactory().GetDatabase(_queryGeneratorFile.Content.DatabaseType);
                    _database.ConnectionString = _queryGeneratorFile.Content.ConnectionString;
                    if (_database.Load())
                    {
                        sqlGenerator.SetDatabaseType(_queryGeneratorFile.Content.DatabaseType);

                        // Enable / disable menu items
                        editToolStripMenuItem.Enabled = true;
                        mainSplitContainer.Visible = true;
                        saveToolStripMenuItem.Enabled = true;
                        saveAsToolStripMenuItem.Enabled = true;

                        DatabaseTreeViewAddNodes();

                        if (_database.Tables.Any())
                        {
                            var selectedTable = databaseTreeView.SearchRecursive(_queryGeneratorFile.Content.Tables.First().TableName.ToString());
                            if (selectedTable != null)
                            {
                                databaseTreeView.AfterSelect -= DatabaseTreeViewAfterSelect;
                                databaseTreeView.SelectedNode = selectedTable;
                                databaseTreeView.AfterSelect += DatabaseTreeViewAfterSelect;
                            }

                            tablesGridView.Rows.Clear();

                            foreach (var fileTable in _queryGeneratorFile.Content.Tables)
                            {
                                var table = (ITable)databaseTreeView.SearchRecursive(fileTable.TableName).Tag;
                                var tablesGridViewRow = new TablesGridViewRow()
                                {
                                    Table = table,
                                    Join = fileTable.Join
                                };
                                TablesGridViewAddRow(tablesGridViewRow);
                            }

                            columnsGridView.Rows.Clear();
                            foreach (var fileColumn in _queryGeneratorFile.Content.Columns)
                            {
                                // Find table
                                var table = (ITable)databaseTreeView.SearchRecursive(fileColumn.TableName).Tag;
                                var column = (IColumn)databaseTreeView.SearchRecursive(fileColumn.ColumnName).Tag;
                                var columnGridViewRow = new ColumnsGridViewRow()
                                {
                                    Table = table,
                                    Column = column,
                                    Selected = fileColumn.Selected,
                                    Alias = fileColumn.Alias,
                                    Condition = fileColumn.Condition,
                                    Group = fileColumn.Group,
                                    Agreggation = fileColumn.Agreggation,
                                    Order = fileColumn.Order,

                                };
                                ColumnsGridViewRowAddRow(columnGridViewRow);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Database has no tables.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        limitTextBox.Text = _queryGeneratorFile.Content.Options.Limit.ToString();

                        BuildSql();
                    }
                    else
                    {
                        MessageBox.Show("Database unreachable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveFile()
        {
            _queryGeneratorFile.Save();
        }

        private void SaveAsFile()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = "Query Generator File (*.qgf)|*.qgf",
                InitialDirectory = "./",
            };
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                _queryGeneratorFile.Save(saveFileDialog.FileName);

                // Enable / disable menu items
                saveToolStripMenuItem.Enabled = true;
            }

        }

        private void Exit()
        {
            var dlgResult = MessageBox.Show("Are you sure you want to close?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlgResult == DialogResult.Yes)
            {
                Close();
            }
        }

        private void Export()
        {
            var saveFileDialog = new SaveFileDialog()
            {
                Filter = _exporter.Filter,
                InitialDirectory = "./",
            };
            var result = saveFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                var dataTable = (DataTable)resultGridView.DataSource;
                // Export dataTable to file
                if(_exporter.Export(saveFileDialog.FileName, dataTable))
                {
                    new Process
                    {
                        // Open file using shell
                        StartInfo = new ProcessStartInfo(saveFileDialog.FileName)
                        {
                            UseShellExecute = true
                        }
                    }.Start();
                }
            }
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewConnection();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditConnection();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }

        private void ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _exporter = new ExcelExporter();
            Export();
        }

        private void CsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _exporter = new CsvExporter();
            Export();
        }

        private void PdfToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var pdfDesigner = new PdfDesignerForm(resultGridView.DataSource as DataTable, ref _queryGeneratorFile)
            {
                StartPosition = FormStartPosition.CenterParent,
            };
            pdfDesigner.ShowDialog();
            _queryGeneratorFile = pdfDesigner.QueryGeneratorFile;
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox()
            {
                StartPosition = FormStartPosition.CenterParent,
            };
            aboutBox.ShowDialog();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }
        #endregion


        #region Tables tree view


        /// <summary>
        /// Populate tables tree view from databaseScheme
        /// </summary>
        private void DatabaseTreeViewAddNodes()
        {
            try
            {
                // Clear tables treeview before populate
                databaseTreeView.Nodes.Clear();
                foreach (var table in _database.Tables)
                {
                    // Populate tables treeview from database scheme
                    var tableNode = new TreeNode(table.ToString())
                    {
                        Tag = table
                    };
                    foreach (var column in table.Columns)
                    {
                        var columnNode = new TreeNode(column.ToString())
                        {
                            Tag = column
                        };
                        tableNode.Nodes.Add(columnNode);
                    }
                    databaseTreeView.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Table selected in treeview event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                var selectedTable = (ITable)e.Node.Tag;
                if (selectedTable != null)
                {

                    // Populate tables view from selected table in TreeView
                    tablesGridView.Rows.Clear();
                    var tablesGridViewRow = new TablesGridViewRow()
                    {
                        Table = selectedTable
                    };
                    TablesGridViewAddRow(tablesGridViewRow);

                    // Populate columns view from selected table in TreeView
                    columnsGridView.Rows.Clear();
                    foreach (var column in selectedTable.Columns)
                    {
                        var columnsGridViewRow = new ColumnsGridViewRow()
                        {
                            Table = selectedTable,
                            Column = column,
                            Selected = true,
                        };
                        ColumnsGridViewRowAddRow(columnsGridViewRow);
                    }

                    UpdateQueryGeneratorFile();
                    BuildSql();
                }
            }

        }

        private void DatabaseTreeViewItemDrag(object sender, ItemDragEventArgs e)
        {
            // Begin drag effect
            ((TreeView)sender).DoDragDrop(e.Item, DragDropEffects.Copy);
        }

        #endregion


        #region Tables (joins) grid view

        /// <summary>
        /// Generate Tables GridView
        /// </summary>
        private void TablesGridViewCreateColumns()
        {
            //Grid column (text type): database table column name
            DataGridViewTextBoxColumn columnName = new DataGridViewTextBoxColumn()
            {
                Name = TablesGridViewColumns.TableName.GetName(),
                HeaderText = TablesGridViewColumns.TableName.GetDescription(),
                ReadOnly = true,
            };
            tablesGridView.Columns.Add(columnName);

            //Grid column (button type): database table column condition
            DataGridViewButtonColumn columnCondition = new DataGridViewButtonColumn()
            {
                Name = TablesGridViewColumns.Join.GetName(),
                Text = TablesGridViewColumns.Join.GetName(),
                HeaderText = TablesGridViewColumns.Join.GetDescription(),
            };
            tablesGridView.Columns.Add(columnCondition);
        }

        /// <summary>
        /// Cell clicked in tables grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TablesGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (e.ColumnIndex == TablesGridViewColumns.Join.GetPosition() &&
                senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 1)
            {
                // Handle condition column button clicked, open filter dialog
                var tableScheme = (ITable)senderGrid.Rows[e.RowIndex].Cells[TablesGridViewColumns.TableName.GetName()].Tag;

                var previousTableSchemes = new List<ITable>();
                for (int i = 0; i < e.RowIndex; i++)
                {
                    previousTableSchemes.Add((ITable)tablesGridView.Rows[i].Cells[TablesGridViewColumns.TableName.GetName()].Tag);
                }

                var sqlJoin = (SqlJoin)tablesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                var joinForm = new JoinForm(sqlJoin, tableScheme, previousTableSchemes)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                joinForm.ShowDialog();
                tablesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = joinForm.SqlJoin;
                tablesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = joinForm.SqlJoin.ToString();

                tablesGridView.AutoSizeColumns();

                UpdateQueryGeneratorFile();
                BuildSql();
            }
        }

        private void TablesGridViewDragEnter(object sender, DragEventArgs e)
        {
            // Retrieve the node that was dragged.
            var draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Check if item is new and allow drop
            bool draggedNodeIsNew = true;
            foreach (DataGridViewRow row in tablesGridView.Rows)
            {
                if (row.Cells[TablesGridViewColumns.TableName.GetPosition()].Value.Equals(draggedNode.Text))
                {
                    draggedNodeIsNew = false;
                }
            }
            if (draggedNodeIsNew)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void TablesGridViewDragDrop(object sender, DragEventArgs e)
        {
            // Retrieve the node that was dragged.
            var draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            bool draggedNodeIsNew = true;
            foreach (DataGridViewRow row in tablesGridView.Rows)
            {
                if (row.Cells[TablesGridViewColumns.TableName.GetPosition()].Value.Equals(draggedNode.Text))
                {
                    draggedNodeIsNew = false;
                }
            }
            if (draggedNodeIsNew)
            {
                var table = (ITable)draggedNode.Tag;
                if (table != null)
                {
                    var previousTableSchemes = new List<ITable>();
                    foreach (DataGridViewRow row in tablesGridView.Rows)
                    {
                        previousTableSchemes.Add((ITable)row.Cells[TablesGridViewColumns.TableName.GetName()].Tag);
                    }
                    var sqlJoin = new SqlJoin()
                    {
                        Table = table.ToString(),
                        JoinType = JoinType.Join,
                    };
                    var joinForm = new JoinForm(sqlJoin, table, previousTableSchemes)
                    {
                        StartPosition = FormStartPosition.CenterParent
                    };
                    joinForm.ShowDialog();

                    var tablesGridViewRow = new TablesGridViewRow()
                    {
                        Table = table,
                        Join = joinForm.SqlJoin
                    };

                    TablesGridViewAddRow(tablesGridViewRow);


                    // Add columns view from selected table in TreeView
                    foreach (var column in table.Columns)
                    {
                        var columnsGridViewRow = new ColumnsGridViewRow()
                        {
                            Table = table,
                            Column = column,
                            Selected = true,
                        };
                        ColumnsGridViewRowAddRow(columnsGridViewRow);
                    }


                    UpdateQueryGeneratorFile();
                    BuildSql();
                }
            }
        }

        private void TablesGridViewUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            if (e.Row.Index == 0)
            {
                e.Cancel = true;
            }
            else
            {
                var tableScheme = (ITable)tablesGridView.Rows[e.Row.Index].Cells[TablesGridViewColumns.TableName.GetName()].Tag;
                ColumnsGridViewRowRemoveRows(tableScheme);
            }
        }

        private void TablesGridViewUserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            UpdateQueryGeneratorFile();
            BuildSql();
        }

        private void TablesGridViewAddRow(TablesGridViewRow tablesGridViewRow)
        {
            var dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.CreateCells(tablesGridView);
            dataGridViewRow.Cells[TablesGridViewColumns.TableName.GetPosition()].Tag = tablesGridViewRow.Table;
            dataGridViewRow.Cells[TablesGridViewColumns.TableName.GetPosition()].Value = tablesGridViewRow.Table.ToString();

            if (tablesGridViewRow.Join != null)
            {
                dataGridViewRow.Cells[TablesGridViewColumns.Join.GetPosition()].Tag = tablesGridViewRow.Join;
                dataGridViewRow.Cells[TablesGridViewColumns.Join.GetPosition()].Value = tablesGridViewRow.Join.ToString(true);
                dataGridViewRow.Cells[TablesGridViewColumns.Join.GetPosition()].Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft };
            }
            else
            {
                dataGridViewRow.Cells[TablesGridViewColumns.Join.GetPosition()].Value = string.Empty;
            }

            tablesGridView.Rows.Add(dataGridViewRow);

            tablesGridView.AutoSizeColumns();
        }



        #endregion


        #region Columns grid view

        /// <summary>
        /// Create columns in columns grid view
        /// </summary>
        private void ColumnsGridViewCreateColumns()
        {
            // Grid column (text type): database table name
            DataGridViewTextBoxColumn tableName = new DataGridViewTextBoxColumn()
            {
                Name = ColumnsGridViewColumns.TableName.GetName(),
                HeaderText = ColumnsGridViewColumns.TableName.GetDescription(),
                ReadOnly = true,
            };
            columnsGridView.Columns.Add(tableName);

            // Grid column (text type): database table column name
            DataGridViewTextBoxColumn columnName = new DataGridViewTextBoxColumn()
            {
                Name = ColumnsGridViewColumns.ColumnName.GetName(),
                HeaderText = ColumnsGridViewColumns.ColumnName.GetDescription(),
                ReadOnly = true,
            };
            columnsGridView.Columns.Add(columnName);

            // Grid column (check type): database table column selected check
            DataGridViewCheckBoxColumn columnSelected = new DataGridViewCheckBoxColumn()
            {
                Name = ColumnsGridViewColumns.Selected.GetName(),
                HeaderText = ColumnsGridViewColumns.Selected.GetDescription(),
                Resizable = DataGridViewTriState.True,
                SortMode = DataGridViewColumnSortMode.Automatic,
            };
            columnsGridView.Columns.Add(columnSelected);

            // Grid column (text type): database table column alias
            DataGridViewTextBoxColumn columnAlias = new DataGridViewTextBoxColumn()
            {
                Name = ColumnsGridViewColumns.Alias.GetName(),
                HeaderText = ColumnsGridViewColumns.Alias.GetDescription(),
            };
            columnsGridView.Columns.Add(columnAlias);

            // Grid column (button type): database table column condition
            DataGridViewButtonColumn columnCondition = new DataGridViewButtonColumn()
            {
                Name = ColumnsGridViewColumns.Condition.GetName(),
                Text = ColumnsGridViewColumns.Condition.GetName(),
                HeaderText = ColumnsGridViewColumns.Condition.GetDescription(),
            };
            columnsGridView.Columns.Add(columnCondition);

            // Grid column (combo type): database table column agreggation (MAX(), MIN(), etc)
            DataGridViewComboBoxColumn columnGroup = new DataGridViewComboBoxColumn()
            {
                Name = ColumnsGridViewColumns.Group.GetName(),
                HeaderText = ColumnsGridViewColumns.Group.GetDescription(),
                ValueType = typeof(GroupType),
                DisplayMember = "Display",
                ValueMember = "Value",
                DataSource = System.Enum.GetValues(typeof(GroupType)).OfType<GroupType>().ToList().Select(value => new { Value = value, Display = value.ToString() }).ToList(),
            };
            columnsGridView.Columns.Add(columnGroup);

            // Grid column (combo type): database table column agreggation (MAX(), MIN(), etc)
            DataGridViewComboBoxColumn columnAggregation = new DataGridViewComboBoxColumn()
            {
                Name = ColumnsGridViewColumns.Aggregation.GetName(),
                HeaderText = ColumnsGridViewColumns.Aggregation.GetDescription(),
                ValueType = typeof(AggregationType),
                DisplayMember = "Display",
                ValueMember = "Value",
                DataSource = System.Enum.GetValues(typeof(AggregationType)).OfType<AggregationType>().ToList().Select(value => new { Value = value, Display = value.ToString() }).ToList(),
            };
            columnsGridView.Columns.Add(columnAggregation);

            // Grid column (combo type): database table column order (ASC, DESC)
            DataGridViewComboBoxColumn columnOrder = new DataGridViewComboBoxColumn()
            {
                Name = ColumnsGridViewColumns.Order.GetName(),
                HeaderText = ColumnsGridViewColumns.Order.GetDescription(),
                ValueType = typeof(OrderType),
                DisplayMember = "Display",
                ValueMember = "Value",
                DataSource = System.Enum.GetValues(typeof(OrderType)).OfType<OrderType>().ToList().Select(value => new { Value = value, Display = value.ToString() }).ToList(),
            };
            columnsGridView.Columns.Add(columnOrder);
        }


        /// <summary>
        /// Add new column in columns grid view
        /// </summary>
        /// <param name="table"></param>
        /// <param name="column"></param>
        /// <param name="selected"></param>
        private void ColumnsGridViewRowAddRow(ColumnsGridViewRow columnGridViewRow)
        {
            var dataGridViewRow = new DataGridViewRow();
            dataGridViewRow.CreateCells(columnsGridView);

            dataGridViewRow.Cells[ColumnsGridViewColumns.TableName.GetPosition()].Value = columnGridViewRow.Table.ToString();
            dataGridViewRow.Cells[ColumnsGridViewColumns.TableName.GetPosition()].Tag = columnGridViewRow.Table;
            dataGridViewRow.Cells[ColumnsGridViewColumns.ColumnName.GetPosition()].Value = columnGridViewRow.Column.ToString();
            dataGridViewRow.Cells[ColumnsGridViewColumns.ColumnName.GetPosition()].Tag = columnGridViewRow.Column;
            dataGridViewRow.Cells[ColumnsGridViewColumns.Selected.GetPosition()].Value = columnGridViewRow.Selected;
            dataGridViewRow.Cells[ColumnsGridViewColumns.Alias.GetPosition()].Value = columnGridViewRow.Alias;
            dataGridViewRow.Cells[ColumnsGridViewColumns.Condition.GetPosition()].Tag = columnGridViewRow.Condition;
            dataGridViewRow.Cells[ColumnsGridViewColumns.Condition.GetPosition()].Value = columnGridViewRow.Condition != null ? columnGridViewRow.Condition.ToString() : "None";
            dataGridViewRow.Cells[ColumnsGridViewColumns.Condition.GetPosition()].Style = new DataGridViewCellStyle() { Alignment = DataGridViewContentAlignment.MiddleLeft };
            dataGridViewRow.Cells[ColumnsGridViewColumns.Group.GetPosition()].Value = columnGridViewRow.Group;
            dataGridViewRow.Cells[ColumnsGridViewColumns.Aggregation.GetPosition()].Value = columnGridViewRow.Agreggation;
            dataGridViewRow.Cells[ColumnsGridViewColumns.Order.GetPosition()].Value = columnGridViewRow.Order;

            columnsGridView.Rows.Add(dataGridViewRow);

            columnsGridView.AutoSizeColumns();
        }

        /// <summary>
        /// Remove every table's columns
        /// </summary>
        /// <param name="table"></param>
        private void ColumnsGridViewRowRemoveRows(ITable table)
        {
            var temp = new List<DataGridViewRow>();
            // Iterate and save the tables' columns in temp
            foreach (DataGridViewRow row in columnsGridView.Rows)
            {
                var tableScheme = (ITable)row.Cells[TablesGridViewColumns.TableName.GetName()].Tag;
                if (!tableScheme.Equals(table))
                {
                    temp.Add(row);
                }
            }
            // Add in control
            columnsGridView.Rows.Clear();
            foreach (var row in temp)
            {
                columnsGridView.Rows.Add(row);
            }
        }


        /// <summary>
        /// Check state changed in columns grid view event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnsGridViewCurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("CurrentCellDirtyStateChanged");

            UpdateQueryGeneratorFile();
            BuildSql();
        }



        /// <summary>
        /// Cell clicked in columns grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnsGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Debug.WriteLine("Column: " + e.ColumnIndex + ", row: " + e.RowIndex + " cell content clicked");
            var senderGrid = (DataGridView)sender;

            if (e.RowIndex >= 0)
            {
                var tableScheme = (ITable)senderGrid.Rows[e.RowIndex].Cells[ColumnsGridViewColumns.TableName.GetPosition()].Tag;
                var columnScheme = (IColumn)senderGrid.Rows[e.RowIndex].Cells[ColumnsGridViewColumns.ColumnName.GetPosition()].Tag;
                if (e.ColumnIndex.Equals(ColumnsGridViewColumns.Condition.GetPosition()) &&
                    senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    //Handle condition column button clicked, open filter dialog
                    var sqlCondition = (SqlCondition)senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                    if (sqlCondition == null)
                    {
                        sqlCondition = new SqlCondition($"{tableScheme.ToString()}.{columnScheme.ToString()}");
                    }
                    var conditionForm = new ConditionForm(sqlCondition)
                    {
                        StartPosition = FormStartPosition.CenterParent
                    };
                    conditionForm.ShowDialog();
                    columnsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = conditionForm.SqlCondition;
                    columnsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = conditionForm.SqlCondition != null ? conditionForm.SqlCondition.ToString() : "None";

                    columnsGridView.AutoSizeColumns();

                    UpdateQueryGeneratorFile();
                    BuildSql();
                }
                else if (e.ColumnIndex.Equals(ColumnsGridViewColumns.Selected.GetPosition()) &&
                    senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
                    senderGrid.EndEdit();
                    // Handle selected checkbox button clicked, selection
                    var dataGridViewCheckBoxCell = (DataGridViewCheckBoxCell)senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (Convert.ToBoolean(dataGridViewCheckBoxCell.Value))
                    {
                        Debug.WriteLine($"{tableScheme.ToString()}.{columnScheme.ToString()}" + " selected");
                    }
                    else
                    {
                        Debug.WriteLine($"{tableScheme.ToString()}.{columnScheme.ToString()}" + " unselected");
                    }

                    UpdateQueryGeneratorFile();
                    BuildSql();
                }
            }
        }

        #endregion


        /// <summary>
        /// Keep selection in file
        /// </summary>
        private void UpdateQueryGeneratorFile()
        {
            _queryGeneratorFile.Content.Tables.Clear();
            foreach (DataGridViewRow row in tablesGridView.Rows)
            {
                var fileTable = new QueryGeneratorFileTableContent()
                {
                    TableName = row.Cells[TablesGridViewColumns.TableName.GetPosition()].Value.ToString(),
                    Join = (SqlJoin)(row.Cells[TablesGridViewColumns.Join.GetPosition()].Tag)
                };
                _queryGeneratorFile.Content.Tables.Add(fileTable);
            }

            _queryGeneratorFile.Content.Columns.Clear();
            foreach (DataGridViewRow row in columnsGridView.Rows)
            {
                var checkBoxCell = ((DataGridViewCheckBoxCell)row.Cells[ColumnsGridViewColumns.Selected.GetPosition()]);
                var fileColumn = new QueryGeneratorFileColumnContent()
                {
                    TableName = row.Cells[ColumnsGridViewColumns.TableName.GetPosition()].Value.ToString(),
                    ColumnName = row.Cells[ColumnsGridViewColumns.ColumnName.GetPosition()].Value.ToString(),
                    Selected = Convert.ToBoolean(checkBoxCell.EditingCellValueChanged ? checkBoxCell.EditingCellFormattedValue : checkBoxCell.Value),
                    Alias = row.Cells[ColumnsGridViewColumns.Alias.GetPosition()].Value?.ToString(),
                    Condition = (SqlCondition)row.Cells[ColumnsGridViewColumns.Condition.GetPosition()].Tag,
                    Group = (GroupType)System.Enum.Parse(typeof(GroupType), ((DataGridViewComboBoxCell)row.Cells[ColumnsGridViewColumns.Group.GetPosition()]).EditedFormattedValue.ToString()),
                    Agreggation = (AggregationType)System.Enum.Parse(typeof(AggregationType), ((DataGridViewComboBoxCell)row.Cells[ColumnsGridViewColumns.Aggregation.GetPosition()]).EditedFormattedValue.ToString()),
                    Order = (OrderType)System.Enum.Parse(typeof(OrderType), ((DataGridViewComboBoxCell)row.Cells[ColumnsGridViewColumns.Order.GetPosition()]).EditedFormattedValue.ToString()),
                };
                _queryGeneratorFile.Content.Columns.Add(fileColumn);
            }

            _queryGeneratorFile.Content.Options.Limit = Convert.ToUInt32(limitTextBox.Text);

        }
        /// <summary>
        /// Retrieve data among controls in order to build SQL
        /// </summary>
        private void BuildSql()
        {
            var sqlSelects = new List<SqlSelect>();
            var sqlTable = string.Empty;
            var sqlJoins = new List<SqlJoin>();
            var sqlWheres = new List<SqlCondition>();
            var sqlGroups = new List<SqlGroup>();
            var sqlOrders = new List<SqlOrder>();
            for (int i = 0; i < _queryGeneratorFile.Content.Tables.Count; i++)
            {
                if (i == 0)
                {
                    // Principal table is the first one
                    sqlTable = _queryGeneratorFile.Content.Tables[i].TableName;
                }
                else
                {
                    // All the others are joins
                    if (_queryGeneratorFile.Content.Tables[i].Join != null)
                    {
                        sqlJoins.Add(_queryGeneratorFile.Content.Tables[i].Join);
                    }
                }
            }

            foreach (var column in _queryGeneratorFile.Content.Columns)
            {
                // Add select if requested
                if (column.Selected)
                {
                    var select = new SqlSelect($"{column.TableName}.{column.ColumnName}");
                    select.AggregationType = column.Agreggation;
                    select.Alias = column.Alias;
                    sqlSelects.Add(select);
                }

                // Add conditions if requested
                if (column.Condition != null && column.Condition.ConditionType != ConditionType.None)
                {
                    sqlWheres.Add(column.Condition);
                }

                // Add groups if requested
                switch (column.Group)
                {
                    case GroupType.Grouped:
                        var sqlGroup = new SqlGroup($"{column.TableName}.{column.ColumnName}");
                        sqlGroups.Add(sqlGroup);
                        break;
                    default:
                        break;
                }

                // Add order if requested
                if (!column.Order.Equals(OrderType.None))
                {
                    var sqlOrder = new SqlOrder($"{column.TableName}.{column.ColumnName}")
                    {
                        OrderType = column.Order,
                    };
                    sqlOrders.Add(sqlOrder);
                }
            }

            int sqlLimit;
            int.TryParse(limitTextBox.Text, out sqlLimit);

            try
            {
                if (automaticCheckBox.Checked)
                {
                    // Generate Sql
                    var sql = sqlGenerator.Select(
                        sqlSelects, 
                        sqlTable, 
                        sqlJoins, 
                        sqlWheres, 
                        sqlGroups, 
                        sqlOrders, 
                        sqlLimit);

                    // Show sql
                    sqlTextBox.Text = sql;

                    // Execute Sql
                    ExecuteSql(sql);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resultLabel.Text = $"Error: {ex.Message}.";
                automaticCheckBox.Checked = false;
            }
        }

        private void ExecuteSql(string sql)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {

                var dataTable = _database.ExecuteSql(sql);
                dataTable.TableName = "Results";
                resultGridView.DataSource = dataTable;
                resultGridView.AutoSizeColumns();

                resultLabel.Text = $"{dataTable.Rows.Count} rows found.";

                // Enable export menu
                exportToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resultLabel.Text = $"Error: {ex.Message}.";
                automaticCheckBox.Checked = false;
            }
            Cursor.Current = Cursors.Arrow;
        }

        private void LimitTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow to enter digits on
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LimitTextBoxTextChanged(object sender, EventArgs e)
        {
            UpdateQueryGeneratorFile();
            BuildSql();
        }

        private void AutomaticCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            executeSqlButton.Visible = !automaticCheckBox.Checked;
        }

        private void SqlEditableCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            // TODO:
        }

        private void ExecuteSqlButton_Click(object sender, EventArgs e)
        {
            // Execute Sql
            ExecuteSql(sqlTextBox.Text);
        }

        private void SelectorForm_Shown(object sender, EventArgs e)
        {
            //NewFile();
        }
    }
}
