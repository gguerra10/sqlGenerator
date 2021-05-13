using SqlGenerator.UI.Enum;
using System;
using System.Windows.Forms;

namespace SqlGenerator.UI.Forms
{
    partial class SelectorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.databaseGroupBox = new System.Windows.Forms.GroupBox();
            this.tablesTreeView = new System.Windows.Forms.TreeView();
            this.submainRightSplitContainer = new System.Windows.Forms.SplitContainer();
            this.selectableTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.optionsGroupBox = new System.Windows.Forms.GroupBox();
            this.optionsTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.limitLabel = new System.Windows.Forms.Label();
            this.limitTextBox = new System.Windows.Forms.TextBox();
            this.selectableSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tablesGroupBox = new System.Windows.Forms.GroupBox();
            this.tablesGridView = new System.Windows.Forms.DataGridView();
            this.columnsGroupBox = new System.Windows.Forms.GroupBox();
            this.columnsGridView = new System.Windows.Forms.DataGridView();
            this.outputTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.resultsGroupBox = new System.Windows.Forms.GroupBox();
            this.resultLabel = new System.Windows.Forms.Label();
            this.outputSplitContainer = new System.Windows.Forms.SplitContainer();
            this.sqlGroupBox = new System.Windows.Forms.GroupBox();
            this.sqlTextBox = new System.Windows.Forms.RichTextBox();
            this.dataGroupBox = new System.Windows.Forms.GroupBox();
            this.resultGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.databaseGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.submainRightSplitContainer)).BeginInit();
            this.submainRightSplitContainer.Panel1.SuspendLayout();
            this.submainRightSplitContainer.Panel2.SuspendLayout();
            this.submainRightSplitContainer.SuspendLayout();
            this.selectableTableLayout.SuspendLayout();
            this.optionsGroupBox.SuspendLayout();
            this.optionsTableLayoutPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.selectableSplitContainer)).BeginInit();
            this.selectableSplitContainer.Panel1.SuspendLayout();
            this.selectableSplitContainer.Panel2.SuspendLayout();
            this.selectableSplitContainer.SuspendLayout();
            this.tablesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablesGridView)).BeginInit();
            this.columnsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.columnsGridView)).BeginInit();
            this.outputTableLayout.SuspendLayout();
            this.resultsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputSplitContainer)).BeginInit();
            this.outputSplitContainer.Panel1.SuspendLayout();
            this.outputSplitContainer.Panel2.SuspendLayout();
            this.outputSplitContainer.SuspendLayout();
            this.sqlGroupBox.SuspendLayout();
            this.dataGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(993, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(153, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(153, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.databaseGroupBox);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.submainRightSplitContainer);
            this.mainSplitContainer.Size = new System.Drawing.Size(993, 603);
            this.mainSplitContainer.SplitterDistance = 137;
            this.mainSplitContainer.TabIndex = 6;
            // 
            // databaseGroupBox
            // 
            this.databaseGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.databaseGroupBox.Controls.Add(this.tablesTreeView);
            this.databaseGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.databaseGroupBox.Location = new System.Drawing.Point(0, 0);
            this.databaseGroupBox.Name = "databaseGroupBox";
            this.databaseGroupBox.Size = new System.Drawing.Size(137, 603);
            this.databaseGroupBox.TabIndex = 2;
            this.databaseGroupBox.TabStop = false;
            this.databaseGroupBox.Text = "Database";
            // 
            // tablesTreeView
            // 
            this.tablesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablesTreeView.Location = new System.Drawing.Point(3, 19);
            this.tablesTreeView.Name = "tablesTreeView";
            this.tablesTreeView.ShowNodeToolTips = true;
            this.tablesTreeView.Size = new System.Drawing.Size(131, 581);
            this.tablesTreeView.TabIndex = 1;
            this.tablesTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.TablesTreeViewItemDrag);
            this.tablesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TablesTreeViewAfterSelect);
            this.tablesTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TablesGridViewDragDrop);
            this.tablesTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TablesGridViewDragEnter);
            // 
            // submainRightSplitContainer
            // 
            this.submainRightSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.submainRightSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.submainRightSplitContainer.Name = "submainRightSplitContainer";
            this.submainRightSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // submainRightSplitContainer.Panel1
            // 
            this.submainRightSplitContainer.Panel1.Controls.Add(this.selectableTableLayout);
            // 
            // submainRightSplitContainer.Panel2
            // 
            this.submainRightSplitContainer.Panel2.Controls.Add(this.outputTableLayout);
            this.submainRightSplitContainer.Size = new System.Drawing.Size(852, 603);
            this.submainRightSplitContainer.SplitterDistance = 300;
            this.submainRightSplitContainer.TabIndex = 6;
            // 
            // selectableTableLayout
            // 
            this.selectableTableLayout.ColumnCount = 1;
            this.selectableTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.selectableTableLayout.Controls.Add(this.optionsGroupBox, 0, 1);
            this.selectableTableLayout.Controls.Add(this.selectableSplitContainer, 0, 0);
            this.selectableTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectableTableLayout.Location = new System.Drawing.Point(0, 0);
            this.selectableTableLayout.Name = "selectableTableLayout";
            this.selectableTableLayout.RowCount = 2;
            this.selectableTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.selectableTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.selectableTableLayout.Size = new System.Drawing.Size(852, 300);
            this.selectableTableLayout.TabIndex = 11;
            // 
            // optionsGroupBox
            // 
            this.optionsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionsGroupBox.Controls.Add(this.optionsTableLayoutPanel);
            this.optionsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsGroupBox.Location = new System.Drawing.Point(3, 243);
            this.optionsGroupBox.Name = "optionsGroupBox";
            this.optionsGroupBox.Size = new System.Drawing.Size(846, 54);
            this.optionsGroupBox.TabIndex = 0;
            this.optionsGroupBox.TabStop = false;
            this.optionsGroupBox.Text = "Options";
            // 
            // optionsTableLayoutPanel
            // 
            this.optionsTableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionsTableLayoutPanel.ColumnCount = 2;
            this.optionsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.95238F));
            this.optionsTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 79.04762F));
            this.optionsTableLayoutPanel.Controls.Add(this.panel1, 0, 0);
            this.optionsTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsTableLayoutPanel.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.optionsTableLayoutPanel.Location = new System.Drawing.Point(3, 19);
            this.optionsTableLayoutPanel.Name = "optionsTableLayoutPanel";
            this.optionsTableLayoutPanel.RowCount = 1;
            this.optionsTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.optionsTableLayoutPanel.Size = new System.Drawing.Size(840, 32);
            this.optionsTableLayoutPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.limitLabel);
            this.panel1.Controls.Add(this.limitTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(169, 26);
            this.panel1.TabIndex = 2;
            // 
            // limitLabel
            // 
            this.limitLabel.AutoSize = true;
            this.limitLabel.Location = new System.Drawing.Point(10, 5);
            this.limitLabel.Name = "limitLabel";
            this.limitLabel.Size = new System.Drawing.Size(34, 15);
            this.limitLabel.TabIndex = 3;
            this.limitLabel.Text = "Limit";
            // 
            // limitTextBox
            // 
            this.limitTextBox.Location = new System.Drawing.Point(55, 2);
            this.limitTextBox.Name = "limitTextBox";
            this.limitTextBox.Size = new System.Drawing.Size(100, 23);
            this.limitTextBox.TabIndex = 2;
            this.limitTextBox.Text = "1000";
            this.limitTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // selectableSplitContainer
            // 
            this.selectableSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectableSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.selectableSplitContainer.Name = "selectableSplitContainer";
            // 
            // selectableSplitContainer.Panel1
            // 
            this.selectableSplitContainer.Panel1.Controls.Add(this.tablesGroupBox);
            // 
            // selectableSplitContainer.Panel2
            // 
            this.selectableSplitContainer.Panel2.Controls.Add(this.columnsGroupBox);
            this.selectableSplitContainer.Size = new System.Drawing.Size(846, 234);
            this.selectableSplitContainer.SplitterDistance = 274;
            this.selectableSplitContainer.TabIndex = 10;
            // 
            // tablesGroupBox
            // 
            this.tablesGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablesGroupBox.Controls.Add(this.tablesGridView);
            this.tablesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.tablesGroupBox.Name = "tablesGroupBox";
            this.tablesGroupBox.Size = new System.Drawing.Size(274, 234);
            this.tablesGroupBox.TabIndex = 2;
            this.tablesGroupBox.TabStop = false;
            this.tablesGroupBox.Text = "Tables";
            // 
            // tablesGridView
            // 
            this.tablesGridView.AllowDrop = true;
            this.tablesGridView.AllowUserToAddRows = false;
            this.tablesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.tablesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablesGridView.Location = new System.Drawing.Point(3, 19);
            this.tablesGridView.Name = "tablesGridView";
            this.tablesGridView.ReadOnly = true;
            this.tablesGridView.RowTemplate.Height = 25;
            this.tablesGridView.Size = new System.Drawing.Size(268, 212);
            this.tablesGridView.TabIndex = 1;
            this.tablesGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TablesGridViewCellContentClick);
            this.tablesGridView.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.TablesGridViewUserDeletedRow);
            this.tablesGridView.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.TablesGridViewUserDeletingRow);
            this.tablesGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.TablesGridViewDragDrop);
            this.tablesGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.TablesGridViewDragEnter);
            // 
            // columnsGroupBox
            // 
            this.columnsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.columnsGroupBox.Controls.Add(this.columnsGridView);
            this.columnsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.columnsGroupBox.Name = "columnsGroupBox";
            this.columnsGroupBox.Size = new System.Drawing.Size(568, 234);
            this.columnsGroupBox.TabIndex = 1;
            this.columnsGroupBox.TabStop = false;
            this.columnsGroupBox.Text = "Columns";
            // 
            // columnsGridView
            // 
            this.columnsGridView.AllowUserToAddRows = false;
            this.columnsGridView.AllowUserToDeleteRows = false;
            this.columnsGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.columnsGridView.Location = new System.Drawing.Point(3, 19);
            this.columnsGridView.Name = "columnsGridView";
            this.columnsGridView.RowTemplate.Height = 25;
            this.columnsGridView.Size = new System.Drawing.Size(562, 212);
            this.columnsGridView.TabIndex = 0;
            this.columnsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ColumnsGridViewCellContentClick);
            this.columnsGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.ColumnsGridViewCurrentCellDirtyStateChanged);
            // 
            // outputTableLayout
            // 
            this.outputTableLayout.ColumnCount = 1;
            this.outputTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.outputTableLayout.Controls.Add(this.resultsGroupBox, 0, 1);
            this.outputTableLayout.Controls.Add(this.outputSplitContainer, 0, 0);
            this.outputTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputTableLayout.Location = new System.Drawing.Point(0, 0);
            this.outputTableLayout.Name = "outputTableLayout";
            this.outputTableLayout.RowCount = 2;
            this.outputTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.outputTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.outputTableLayout.Size = new System.Drawing.Size(852, 299);
            this.outputTableLayout.TabIndex = 3;
            // 
            // resultsGroupBox
            // 
            this.resultsGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.resultsGroupBox.Controls.Add(this.resultLabel);
            this.resultsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultsGroupBox.Location = new System.Drawing.Point(3, 252);
            this.resultsGroupBox.Name = "resultsGroupBox";
            this.resultsGroupBox.Size = new System.Drawing.Size(846, 44);
            this.resultsGroupBox.TabIndex = 4;
            this.resultsGroupBox.TabStop = false;
            this.resultsGroupBox.Text = "Results";
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(13, 19);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(76, 15);
            this.resultLabel.TabIndex = 6;
            this.resultLabel.Text = "0 rows found";
            // 
            // outputSplitContainer
            // 
            this.outputSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.outputSplitContainer.Name = "outputSplitContainer";
            // 
            // outputSplitContainer.Panel1
            // 
            this.outputSplitContainer.Panel1.Controls.Add(this.sqlGroupBox);
            // 
            // outputSplitContainer.Panel2
            // 
            this.outputSplitContainer.Panel2.Controls.Add(this.dataGroupBox);
            this.outputSplitContainer.Size = new System.Drawing.Size(846, 243);
            this.outputSplitContainer.SplitterDistance = 276;
            this.outputSplitContainer.TabIndex = 3;
            // 
            // sqlGroupBox
            // 
            this.sqlGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sqlGroupBox.Controls.Add(this.sqlTextBox);
            this.sqlGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlGroupBox.Location = new System.Drawing.Point(0, 0);
            this.sqlGroupBox.Name = "sqlGroupBox";
            this.sqlGroupBox.Size = new System.Drawing.Size(276, 243);
            this.sqlGroupBox.TabIndex = 1;
            this.sqlGroupBox.TabStop = false;
            this.sqlGroupBox.Text = "SQL";
            // 
            // sqlTextBox
            // 
            this.sqlTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sqlTextBox.Location = new System.Drawing.Point(3, 19);
            this.sqlTextBox.Name = "sqlTextBox";
            this.sqlTextBox.ReadOnly = true;
            this.sqlTextBox.Size = new System.Drawing.Size(270, 221);
            this.sqlTextBox.TabIndex = 0;
            this.sqlTextBox.Text = "";
            // 
            // dataGroupBox
            // 
            this.dataGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.dataGroupBox.Controls.Add(this.resultGridView);
            this.dataGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGroupBox.Location = new System.Drawing.Point(0, 0);
            this.dataGroupBox.Name = "dataGroupBox";
            this.dataGroupBox.Size = new System.Drawing.Size(566, 243);
            this.dataGroupBox.TabIndex = 2;
            this.dataGroupBox.TabStop = false;
            this.dataGroupBox.Text = "Data";
            // 
            // resultGridView
            // 
            this.resultGridView.AllowUserToAddRows = false;
            this.resultGridView.AllowUserToDeleteRows = false;
            this.resultGridView.AllowUserToOrderColumns = true;
            this.resultGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.resultGridView.Location = new System.Drawing.Point(3, 19);
            this.resultGridView.Name = "resultGridView";
            this.resultGridView.ReadOnly = true;
            this.resultGridView.RowTemplate.Height = 25;
            this.resultGridView.Size = new System.Drawing.Size(560, 221);
            this.resultGridView.TabIndex = 1;
            // 
            // SelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 627);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SelectorForm";
            this.Text = "Sql Generator";
            this.Shown += new System.EventHandler(this.SelectorForm_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.databaseGroupBox.ResumeLayout(false);
            this.submainRightSplitContainer.Panel1.ResumeLayout(false);
            this.submainRightSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.submainRightSplitContainer)).EndInit();
            this.submainRightSplitContainer.ResumeLayout(false);
            this.selectableTableLayout.ResumeLayout(false);
            this.optionsGroupBox.ResumeLayout(false);
            this.optionsTableLayoutPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.selectableSplitContainer.Panel1.ResumeLayout(false);
            this.selectableSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.selectableSplitContainer)).EndInit();
            this.selectableSplitContainer.ResumeLayout(false);
            this.tablesGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablesGridView)).EndInit();
            this.columnsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.columnsGridView)).EndInit();
            this.outputTableLayout.ResumeLayout(false);
            this.resultsGroupBox.ResumeLayout(false);
            this.resultsGroupBox.PerformLayout();
            this.outputSplitContainer.Panel1.ResumeLayout(false);
            this.outputSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputSplitContainer)).EndInit();
            this.outputSplitContainer.ResumeLayout(false);
            this.sqlGroupBox.ResumeLayout(false);
            this.dataGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.resultGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private SplitContainer mainSplitContainer;
        private SplitContainer submainRightSplitContainer;
        private GroupBox databaseGroupBox;
        private TreeView tablesTreeView;
        private SplitContainer selectableSplitContainer;
        private GroupBox tablesGroupBox;
        private DataGridView tablesGridView;
        private GroupBox columnsGroupBox;
        private DataGridView columnsGridView;
        private SplitContainer outputSplitContainer;
        private RichTextBox sqlTextBox;
        private DataGridView resultGridView;
        private GroupBox optionsGroupBox;
        private GroupBox sqlGroupBox;
        private GroupBox dataGroupBox;
        private TableLayoutPanel optionsTableLayoutPanel;
        private Panel panel1;
        private Label limitLabel;
        private TextBox limitTextBox;
        private TableLayoutPanel outputTableLayout;
        private GroupBox resultsGroupBox;
        private Label resultLabel;
        private TableLayoutPanel selectableTableLayout;
    }
}

