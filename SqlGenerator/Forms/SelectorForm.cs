using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SqlGenerator.Archive;
using SqlGenerator.Extensions;
using SqlGenerator.Row;
using SqlGenerator.Core.Facade;
using SqlGenerator.Core.Factory;
using SqlGenerator.Core.Enum;
using SqlGenerator.Core.Sql;
using SqlGenerator.Export.Facade;
using SqlGenerator.Enum;
using SqlGenerator.Export.Facade.Impl.Csv;
using SqlGenerator.Export.Facade.Impl.Excel;
using SqlGenerator.Archive.Content;

namespace SqlGenerator.Forms
{
    public partial class SelectorForm : Form
    {
        private SqlGenerator sqlGenerator;
        private SqlGeneratorArchive _sqlGeneratorArchive;

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

            // Add handlers to menu items
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
            // Show connection dialog
            var result = connectionForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Connection was ok and database scheme retrieved
                _database = connectionForm.Database;

                // Create new data archive
                _sqlGeneratorArchive = new SqlGeneratorArchive();
                _sqlGeneratorArchive.Data.DatabaseType = connectionForm.DatabaseType;
                _sqlGeneratorArchive.Data.ConnectionString = _database.ConnectionString;

                // Set database type in sql generator
                sqlGenerator.SetDatabaseType(connectionForm.DatabaseType);

                // Populate database tree view
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
                _sqlGeneratorArchive.Data.DatabaseType,
                _sqlGeneratorArchive.Data.ConnectionString
                )
            {
                StartPosition = FormStartPosition.CenterParent
            };
            // Show connection dialog
            var result = connectionForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Connection was ok and database scheme retrieved
                _database = connectionForm.Database;

                // Create new data archive
                _sqlGeneratorArchive = new SqlGeneratorArchive();
                _sqlGeneratorArchive.Data.DatabaseType = connectionForm.DatabaseType;
                _sqlGeneratorArchive.Data.ConnectionString = _database.ConnectionString;

                // Set database type in sql generator
                sqlGenerator.SetDatabaseType(connectionForm.DatabaseType);

                // Refresh view with archived data.. some errors may raise
                ArchiveToView();
                // Build the sentence
                BuildSql();
            }
        }

        private void OpenFile()
        {
            // Open file dialog to pick archive
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
                    // Create new data archive and try to load content
                    _sqlGeneratorArchive = new SqlGeneratorArchive();
                    _sqlGeneratorArchive.Load(openFileDialog.FileName);

                    // Create new database from archived data
                    _database = new DatabaseFactory().GetDatabase(
                        _sqlGeneratorArchive.Data.DatabaseType,
                        _sqlGeneratorArchive.Data.ConnectionString);

                    // Set database type in sql generator
                    sqlGenerator.SetDatabaseType(_sqlGeneratorArchive.Data.DatabaseType);

                    // Connect to database and try to retrieve scheme
                    if (_database.Load())
                    {
                        // Refresh view with archived data
                        ArchiveToView();
                        // Build the sentence
                        BuildSql();
                    }
                    else
                    {
                        MessageBox.Show("Database unreachable.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        editToolStripMenuItem.Enabled = true;
                    }

                }
                catch (Exception ex)
                {
                    // Bad format file
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveFile()
        {
            // Save archive data to file
            _sqlGeneratorArchive.Save();
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
                // Save archive data on selected file
                _sqlGeneratorArchive.Save(saveFileDialog.FileName);

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
                // Get results from view to export them
                var dataTable = (DataTable)resultGridView.DataSource;
                // Export dataTable to file
                if (_exporter.Export(saveFileDialog.FileName, dataTable))
                {
                    // Open file automatically using shell
                    new Process
                    {
                        StartInfo = new ProcessStartInfo(saveFileDialog.FileName)
                        {
                            UseShellExecute = true
                        }
                    }.Start();
                }
            }
        }

        /// <summary>
        /// New Connection menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewConnection();
        }

        /// <summary>
        /// Edit connection menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditConnection();
        }

        /// <summary>
        /// Open archive data menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        /// <summary>
        /// Save archive data menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        /// <summary>
        /// Save as archive data menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAsFile();
        }

        /// <summary>
        /// Excel export menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _exporter = new ExcelExporter();
            Export();
        }

        /// <summary>
        /// Csv export menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _exporter = new CsvExporter();
            Export();
        }

        /// <summary>
        /// Pdf designer menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PdfToolStripMenuItem_Click(object sender, EventArgs e)
        {

            var pdfDesigner = new PdfDesignerForm(resultGridView.DataSource as DataTable, _sqlGeneratorArchive)
            {
                StartPosition = FormStartPosition.CenterParent,
            };
            pdfDesigner.ShowDialog();
            _sqlGeneratorArchive = pdfDesigner.QueryGeneratorFile;
        }

        /// <summary>
        /// About menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox()
            {
                StartPosition = FormStartPosition.CenterParent,
            };
            aboutBox.ShowDialog();
        }


        /// <summary>
        /// Exit menu button handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }
        #endregion


        #region Tables tree view



        /// <summary>
        /// Populate database treeview with database data
        /// </summary>
        private void DatabaseTreeViewAddNodes()
        {
            try
            {
                // Clear database treeview before populate
                databaseTreeView.Nodes.Clear();
                // Populate tables treeview from database object
                foreach (var table in _database.Tables)
                {
                    // Create new node with table object 
                    var tableNode = new TreeNode(table.ToString())
                    {
                        Tag = table
                    };
                    foreach (var column in table.Columns)
                    {
                        // Create new child node from column object
                        var columnNode = new TreeNode(column.ToString())
                        {
                            Tag = column
                        };
                        // Add column child node
                        tableNode.Nodes.Add(columnNode);
                    }
                    // Add table node
                    databaseTreeView.Nodes.Add(tableNode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Table selected in database treeview event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            // First level are tables
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

                    ViewToArchive();
                    BuildSql();
                }
            }

        }

        /// <summary>
        /// Table dragged from database treeview event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            // Grid column (text type): database table column name
            DataGridViewTextBoxColumn columnName = new DataGridViewTextBoxColumn()
            {
                Name = TablesGridViewColumns.TableName.GetName(),
                HeaderText = TablesGridViewColumns.TableName.GetDescription(),
                ReadOnly = true,
            };
            tablesGridView.Columns.Add(columnName);

            // Grid column (button type): database table column condition
            DataGridViewButtonColumn columnCondition = new DataGridViewButtonColumn()
            {
                Name = TablesGridViewColumns.Join.GetName(),
                Text = TablesGridViewColumns.Join.GetName(),
                HeaderText = TablesGridViewColumns.Join.GetDescription(),
            };
            tablesGridView.Columns.Add(columnCondition);
        }

        /// <summary>
        /// Cell clicked in tables grid view handler
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
                // Show join dialog
                if (joinForm.ShowDialog() == DialogResult.OK)
                {
                    // if Ok refresh view
                    tablesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = joinForm.SqlJoin;
                    tablesGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = joinForm.SqlJoin.ToString();
                }
                tablesGridView.AutoSizeColumns();

                ViewToArchive();
                BuildSql();
            }
        }

        /// <summary>
        /// Table drag enter to tables grid view event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TablesGridViewDragEnter(object sender, DragEventArgs e)
        {
            // Retrieve the node that was dragged.
            var draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));

            // Check if item is new and allow drop, prevent from add same table twice
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

        /// <summary>
        /// Table drag drop on tables grid view event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                // Retrieve the table object
                var table = (ITable)draggedNode.Tag;
                if (table != null)
                {
                    // Retrieve every table already added in order to build the join relation
                    var previousTables = new List<ITable>();
                    foreach (DataGridViewRow row in tablesGridView.Rows)
                    {
                        previousTables.Add((ITable)row.Cells[TablesGridViewColumns.TableName.GetName()].Tag);
                    }
                    var sqlJoin = new SqlJoin()
                    {
                        Table = table.ToString(),
                        JoinType = JoinType.Join,
                    };
                    var joinForm = new JoinForm(sqlJoin, table, previousTables)
                    {
                        StartPosition = FormStartPosition.CenterParent
                    };
                    // Show join dialog
                    if (joinForm.ShowDialog() == DialogResult.OK)
                    {

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
                    }
                    // Save to memory archive
                    ViewToArchive();
                    // Build the new sentence
                    BuildSql();
                }
            }
        }

        /// <summary>
        /// Tables grid view deleting row event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TablesGridViewUserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            // First table cannot be erased
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

        /// <summary>
        /// Tables grid view deleted row event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TablesGridViewUserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

            // Save to memory archive
            ViewToArchive();
            // Build the new sentence
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
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true,
            };
            columnsGridView.Columns.Add(tableName);

            // Grid column (text type): database table column name
            DataGridViewTextBoxColumn columnName = new DataGridViewTextBoxColumn()
            {
                Name = ColumnsGridViewColumns.ColumnName.GetName(),
                HeaderText = ColumnsGridViewColumns.ColumnName.GetDescription(),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true,
            };
            columnsGridView.Columns.Add(columnName);

            // Grid column (check type): database table column selected check
            DataGridViewCheckBoxColumn columnSelected = new DataGridViewCheckBoxColumn()
            {
                Name = ColumnsGridViewColumns.Selected.GetName(),
                HeaderText = ColumnsGridViewColumns.Selected.GetDescription(),
                Resizable = DataGridViewTriState.True,
                SortMode = DataGridViewColumnSortMode.NotSortable,
            };
            columnsGridView.Columns.Add(columnSelected);

            // Grid column (text type): database table column alias
            DataGridViewTextBoxColumn columnAlias = new DataGridViewTextBoxColumn()
            {
                Name = ColumnsGridViewColumns.Alias.GetName(),
                HeaderText = ColumnsGridViewColumns.Alias.GetDescription(),
                SortMode = DataGridViewColumnSortMode.NotSortable,
            };
            columnsGridView.Columns.Add(columnAlias);

            // Grid column (button type): database table column condition
            DataGridViewButtonColumn columnCondition = new DataGridViewButtonColumn()
            {
                Name = ColumnsGridViewColumns.Condition.GetName(),
                Text = ColumnsGridViewColumns.Condition.GetName(),
                HeaderText = ColumnsGridViewColumns.Condition.GetDescription(),
                SortMode = DataGridViewColumnSortMode.NotSortable,
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
                SortMode = DataGridViewColumnSortMode.NotSortable,
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
                SortMode = DataGridViewColumnSortMode.NotSortable,
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
                SortMode = DataGridViewColumnSortMode.NotSortable,
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
            if (columnsGridView.IsCurrentCellDirty)
            {
                columnsGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
                ViewToArchive();
                BuildSql();
            }
        }



        /// <summary>
        /// Cell clicked in columns grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColumnsGridViewCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (e.RowIndex >= 0)
            {
                var tableScheme = (ITable)senderGrid.Rows[e.RowIndex].Cells[ColumnsGridViewColumns.TableName.GetPosition()].Tag;
                var columnScheme = (IColumn)senderGrid.Rows[e.RowIndex].Cells[ColumnsGridViewColumns.ColumnName.GetPosition()].Tag;
                if (e.ColumnIndex.Equals(ColumnsGridViewColumns.Condition.GetPosition()) &&
                    senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
                {
                    // Handle condition column button clicked, open filter dialog
                    var sqlCondition = (SqlCondition)senderGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag;
                    if (sqlCondition == null)
                    {
                        sqlCondition = new SqlCondition($"{tableScheme.ToString()}.{columnScheme.ToString()}");
                    }
                    var conditionForm = new ConditionForm(sqlCondition)
                    {
                        StartPosition = FormStartPosition.CenterParent
                    };

                    if (conditionForm.ShowDialog() == DialogResult.OK)
                    {
                        columnsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag = conditionForm.SqlCondition;
                        columnsGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = conditionForm.SqlCondition != null ? conditionForm.SqlCondition.ToString() : "None";
                    }
                    columnsGridView.AutoSizeColumns();

                    ViewToArchive();
                    BuildSql();
                }
            }
        }

        private void ColumnsGridViewCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;
            if (e.RowIndex >= 0)
            {
                var tableScheme = (ITable)senderGrid.Rows[e.RowIndex].Cells[ColumnsGridViewColumns.TableName.GetPosition()].Tag;
                var columnScheme = (IColumn)senderGrid.Rows[e.RowIndex].Cells[ColumnsGridViewColumns.ColumnName.GetPosition()].Tag;
                if (e.ColumnIndex.Equals(ColumnsGridViewColumns.Selected.GetPosition()) &&
                    senderGrid.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
                {
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

                    // Save to memory archive
                    ViewToArchive();
                    // Build the new sentence
                    BuildSql();
                }
            }
        }

        #endregion


        /// <summary>
        /// Get all selected data from archive and refresh view
        /// </summary>
        private void ArchiveToView()
        {
            // Enable / disable menu items
            editToolStripMenuItem.Enabled = true;
            mainSplitContainer.Visible = true;
            saveToolStripMenuItem.Enabled = true;
            saveAsToolStripMenuItem.Enabled = true;

            // Populate database tree view
            DatabaseTreeViewAddNodes();

            if (_database.Tables.Any())
            {
                // Set selected table on database tree view  from archive
                if (_sqlGeneratorArchive.Data.Tables.Any())
                {
                    var firstTable = _sqlGeneratorArchive.Data.Tables.First();
                    var selectedTable = databaseTreeView.SearchRecursive(firstTable.TableName.ToString());
                    if (selectedTable != null)
                    {
                        databaseTreeView.AfterSelect -= DatabaseTreeViewAfterSelect;
                        databaseTreeView.SelectedNode = selectedTable;
                        databaseTreeView.AfterSelect += DatabaseTreeViewAfterSelect;
                    }
                }

                // Populate tables grid view from archive
                tablesGridView.Rows.Clear();
                foreach (var fileTable in _sqlGeneratorArchive.Data.Tables)
                {
                    // Retrive table object from database tree view control
                    var table = (ITable)databaseTreeView.SearchRecursive(fileTable.TableName).Tag;

                    // Create a new row for tables grid view
                    var tablesGridViewRow = new TablesGridViewRow()
                    {
                        Table = table,
                        Join = fileTable.Join
                    };
                    TablesGridViewAddRow(tablesGridViewRow);
                }

                // Populate columns grid view from archive
                columnsGridView.Rows.Clear();
                foreach (var fileColumn in _sqlGeneratorArchive.Data.Columns)
                {
                    // Retrieve table and column object from database tree view control
                    var table = (ITable)databaseTreeView.SearchRecursive(fileColumn.TableName).Tag;
                    var column = (IColumn)databaseTreeView.SearchRecursive(fileColumn.ColumnName).Tag;

                    // Create a new row for columns grid view
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
                    // Add the item
                    ColumnsGridViewRowAddRow(columnGridViewRow);
                }
            }
            else
            {
                MessageBox.Show("Database has no tables.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            limitTextBox.Text = _sqlGeneratorArchive.Data.Options.Limit.ToString();
        }

        /// <summary>
        /// Get selected input from view to save on memory archive
        /// </summary>
        private void ViewToArchive()
        {
            // Save tables from tables grid view
            _sqlGeneratorArchive.Data.Tables.Clear();
            foreach (DataGridViewRow row in tablesGridView.Rows)
            {
                var fileTable = new ArchiveTable()
                {
                    TableName = row.Cells[TablesGridViewColumns.TableName.GetPosition()].Value.ToString(),
                    Join = (SqlJoin)(row.Cells[TablesGridViewColumns.Join.GetPosition()].Tag)
                };
                _sqlGeneratorArchive.Data.Tables.Add(fileTable);
            }

            // Save columns from columns grid view
            _sqlGeneratorArchive.Data.Columns.Clear();
            foreach (DataGridViewRow row in columnsGridView.Rows)
            {
                var checkBoxCell = ((DataGridViewCheckBoxCell)row.Cells[ColumnsGridViewColumns.Selected.GetPosition()]);
                var fileColumn = new ArchiveColumn()
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
                _sqlGeneratorArchive.Data.Columns.Add(fileColumn);
            }

            // Save limit results value
            _sqlGeneratorArchive.Data.Options.Limit = Convert.ToUInt32(limitTextBox.Text);
        }

        /// <summary>
        /// Retrieve data from memory archive and build sql sentence
        /// </summary>
        private void BuildSql()
        {
            var sqlSelects = new List<SqlSelect>();
            var sqlTable = string.Empty;
            var sqlJoins = new List<SqlJoin>();
            var sqlWheres = new List<SqlCondition>();
            var sqlGroups = new List<SqlGroup>();
            var sqlOrders = new List<SqlOrder>();
            for (int i = 0; i < _sqlGeneratorArchive.Data.Tables.Count; i++)
            {
                if (i == 0)
                {
                    // Principal table is the first one
                    sqlTable = _sqlGeneratorArchive.Data.Tables[i].TableName;
                }
                else
                {
                    // All the others are joins
                    if (_sqlGeneratorArchive.Data.Tables[i].Join != null)
                    {
                        sqlJoins.Add(_sqlGeneratorArchive.Data.Tables[i].Join);
                    }
                }
            }

            foreach (var column in _sqlGeneratorArchive.Data.Columns)
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
                if (automaticCheckBox.Checked)
                {
                    if (agreggationCheckBox.Checked)
                    {
                        // Check aggregation
                        sqlGenerator.SelectAggregationCheck(sqlSelects, sqlGroups);
                    }

                    // Execute Sql
                    ExecuteSql(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resultLabel.Text = $"Error: {ex.Message}.";
                automaticCheckBox.Checked = false;
            }
        }

        /// <summary>
        /// Sql sentence excecution against database
        /// </summary>
        /// <param name="sql"></param>
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
                // If something went wrong, then disable automatic sql execution
                automaticCheckBox.Checked = false;
            }
            Cursor.Current = Cursors.Arrow;
        }

        /// <summary>
        /// Limit text box key press event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LimitTextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow to enter digits on
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Limit text box value changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LimitTextBoxTextChanged(object sender, EventArgs e)
        {
            // Save all content on memory archive
            ViewToArchive();
            // Build new sentence
            BuildSql();
        }

        private void AutomaticCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            executeSqlButton.Visible = !automaticCheckBox.Checked;
            if (automaticCheckBox.Checked)
            {
                ExecuteSql(sqlTextBox.Text);
            }
        }

        /// <summary>
        /// Sql editable check box checked event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SqlEditableCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            // If sql editable is active then disable tables and columns grid view
            // and make sql text box editable
            sqlTextBox.ReadOnly = !sqlEditableCheckBox.Checked;
            tablesGridView.Enabled = !sqlEditableCheckBox.Checked;
            columnsGridView.Enabled = !sqlEditableCheckBox.Checked;
            if (automaticCheckBox.Checked && sqlEditableCheckBox.Checked)
            {
                automaticCheckBox.Checked = false;
            }
        }


        /// <summary>
        /// Excete sql button clicked event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExecuteSqlButton_Click(object sender, EventArgs e)
        {
            // Execute Sql
            ExecuteSql(sqlTextBox.Text);
        }


        /// <summary>
        /// Auxiliar var to toggle all columns selection.
        /// </summary>
        private bool _allSelected;

        /// <summary>
        /// Select all button clicked event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAllButton_Click(object sender, EventArgs e)
        {
            // Iterate every item in columns grid view to set same value
            foreach (DataGridViewRow row in columnsGridView.Rows)
            {
                var checkBoxCell = ((DataGridViewCheckBoxCell)row.Cells[ColumnsGridViewColumns.Selected.GetPosition()]);
                checkBoxCell.Value = _allSelected;
            }
            // Swap value
            _allSelected = !_allSelected;
        }

        /// <summary>
        /// Agreggation check box checked changed event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AgreggationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Nothing to do
        }

        /// <summary>
        /// Form shown event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectorForm_Shown(object sender, EventArgs e)
        {
            // Nothing to do.
        }
    }
}
