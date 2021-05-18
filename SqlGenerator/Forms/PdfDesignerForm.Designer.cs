
namespace SqlGenerator.Forms
{
    partial class PdfDesignerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.colorGroupBox = new System.Windows.Forms.GroupBox();
            this.foregroundColorButton = new System.Windows.Forms.Button();
            this.backgroundColorButton = new System.Windows.Forms.Button();
            this.alignmentGroupBox = new System.Windows.Forms.GroupBox();
            this.rightRadioButton = new System.Windows.Forms.RadioButton();
            this.centerRadioButton = new System.Windows.Forms.RadioButton();
            this.leftRadioButton = new System.Windows.Forms.RadioButton();
            this.fontGroupBox = new System.Windows.Forms.GroupBox();
            this.fontSizeLabel = new System.Windows.Forms.Label();
            this.fontButton = new System.Windows.Forms.Button();
            this.fontsizeComboBox = new System.Windows.Forms.ComboBox();
            this.orientationGroupBox = new System.Windows.Forms.GroupBox();
            this.horizontalRadioButton = new System.Windows.Forms.RadioButton();
            this.verticalRadioButton = new System.Windows.Forms.RadioButton();
            this.viewSplitContainer = new System.Windows.Forms.SplitContainer();
            this.breakdownsGroupBox = new System.Windows.Forms.GroupBox();
            this.designerDataGridView = new System.Windows.Forms.DataGridView();
            this.previewGroupBox = new System.Windows.Forms.GroupBox();
            this.previewDataGridView = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.timestampCheckBox = new System.Windows.Forms.CheckBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.mainTableLayout.SuspendLayout();
            this.panel1.SuspendLayout();
            this.colorGroupBox.SuspendLayout();
            this.alignmentGroupBox.SuspendLayout();
            this.fontGroupBox.SuspendLayout();
            this.orientationGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewSplitContainer)).BeginInit();
            this.viewSplitContainer.Panel1.SuspendLayout();
            this.viewSplitContainer.Panel2.SuspendLayout();
            this.viewSplitContainer.SuspendLayout();
            this.breakdownsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.designerDataGridView)).BeginInit();
            this.previewGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.previewDataGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 2;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Controls.Add(this.panel1, 0, 1);
            this.mainTableLayout.Controls.Add(this.viewSplitContainer, 1, 1);
            this.mainTableLayout.Controls.Add(this.panel2, 0, 0);
            this.mainTableLayout.Controls.Add(this.tableLayoutPanel1, 1, 0);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 2;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.Size = new System.Drawing.Size(1224, 827);
            this.mainTableLayout.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.colorGroupBox);
            this.panel1.Controls.Add(this.alignmentGroupBox);
            this.panel1.Controls.Add(this.fontGroupBox);
            this.panel1.Controls.Add(this.orientationGroupBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(114, 781);
            this.panel1.TabIndex = 1;
            // 
            // colorGroupBox
            // 
            this.colorGroupBox.Controls.Add(this.foregroundColorButton);
            this.colorGroupBox.Controls.Add(this.backgroundColorButton);
            this.colorGroupBox.Location = new System.Drawing.Point(0, 346);
            this.colorGroupBox.Name = "colorGroupBox";
            this.colorGroupBox.Size = new System.Drawing.Size(117, 100);
            this.colorGroupBox.TabIndex = 5;
            this.colorGroupBox.TabStop = false;
            this.colorGroupBox.Text = "Header Color";
            // 
            // foregroundColorButton
            // 
            this.foregroundColorButton.Location = new System.Drawing.Point(16, 60);
            this.foregroundColorButton.Name = "foregroundColorButton";
            this.foregroundColorButton.Size = new System.Drawing.Size(87, 23);
            this.foregroundColorButton.TabIndex = 0;
            this.foregroundColorButton.Text = "Foreground";
            this.foregroundColorButton.UseVisualStyleBackColor = true;
            this.foregroundColorButton.Click += new System.EventHandler(this.ForegroundColorButton_Click);
            // 
            // backgroundColorButton
            // 
            this.backgroundColorButton.Location = new System.Drawing.Point(16, 22);
            this.backgroundColorButton.Name = "backgroundColorButton";
            this.backgroundColorButton.Size = new System.Drawing.Size(87, 23);
            this.backgroundColorButton.TabIndex = 0;
            this.backgroundColorButton.Text = "Background";
            this.backgroundColorButton.UseVisualStyleBackColor = true;
            this.backgroundColorButton.Click += new System.EventHandler(this.BackgroundColorButton_Click);
            // 
            // alignmentGroupBox
            // 
            this.alignmentGroupBox.Controls.Add(this.rightRadioButton);
            this.alignmentGroupBox.Controls.Add(this.centerRadioButton);
            this.alignmentGroupBox.Controls.Add(this.leftRadioButton);
            this.alignmentGroupBox.Location = new System.Drawing.Point(0, 106);
            this.alignmentGroupBox.Name = "alignmentGroupBox";
            this.alignmentGroupBox.Size = new System.Drawing.Size(114, 128);
            this.alignmentGroupBox.TabIndex = 4;
            this.alignmentGroupBox.TabStop = false;
            this.alignmentGroupBox.Text = "Alignment";
            // 
            // rightRadioButton
            // 
            this.rightRadioButton.AutoSize = true;
            this.rightRadioButton.Location = new System.Drawing.Point(10, 93);
            this.rightRadioButton.Name = "rightRadioButton";
            this.rightRadioButton.Size = new System.Drawing.Size(53, 19);
            this.rightRadioButton.TabIndex = 2;
            this.rightRadioButton.Text = "Right";
            this.rightRadioButton.UseVisualStyleBackColor = true;
            // 
            // centerRadioButton
            // 
            this.centerRadioButton.AutoSize = true;
            this.centerRadioButton.Location = new System.Drawing.Point(10, 57);
            this.centerRadioButton.Name = "centerRadioButton";
            this.centerRadioButton.Size = new System.Drawing.Size(73, 19);
            this.centerRadioButton.TabIndex = 1;
            this.centerRadioButton.Text = "Centered";
            this.centerRadioButton.UseVisualStyleBackColor = true;
            // 
            // leftRadioButton
            // 
            this.leftRadioButton.AutoSize = true;
            this.leftRadioButton.Checked = true;
            this.leftRadioButton.Location = new System.Drawing.Point(10, 23);
            this.leftRadioButton.Name = "leftRadioButton";
            this.leftRadioButton.Size = new System.Drawing.Size(45, 19);
            this.leftRadioButton.TabIndex = 0;
            this.leftRadioButton.TabStop = true;
            this.leftRadioButton.Text = "Left";
            this.leftRadioButton.UseVisualStyleBackColor = true;
            // 
            // fontGroupBox
            // 
            this.fontGroupBox.Controls.Add(this.fontSizeLabel);
            this.fontGroupBox.Controls.Add(this.fontButton);
            this.fontGroupBox.Controls.Add(this.fontsizeComboBox);
            this.fontGroupBox.Location = new System.Drawing.Point(0, 240);
            this.fontGroupBox.Name = "fontGroupBox";
            this.fontGroupBox.Size = new System.Drawing.Size(117, 100);
            this.fontGroupBox.TabIndex = 3;
            this.fontGroupBox.TabStop = false;
            this.fontGroupBox.Text = "Font";
            // 
            // fontSizeLabel
            // 
            this.fontSizeLabel.AutoSize = true;
            this.fontSizeLabel.Location = new System.Drawing.Point(7, 23);
            this.fontSizeLabel.Name = "fontSizeLabel";
            this.fontSizeLabel.Size = new System.Drawing.Size(53, 15);
            this.fontSizeLabel.TabIndex = 0;
            this.fontSizeLabel.Text = "Font size";
            // 
            // fontButton
            // 
            this.fontButton.Location = new System.Drawing.Point(16, 71);
            this.fontButton.Name = "fontButton";
            this.fontButton.Size = new System.Drawing.Size(75, 23);
            this.fontButton.TabIndex = 1;
            this.fontButton.Text = "Font";
            this.fontButton.UseVisualStyleBackColor = true;
            this.fontButton.Visible = false;
            this.fontButton.Click += new System.EventHandler(this.FontButton_Click);
            // 
            // fontsizeComboBox
            // 
            this.fontsizeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fontsizeComboBox.Location = new System.Drawing.Point(16, 41);
            this.fontsizeComboBox.Name = "fontsizeComboBox";
            this.fontsizeComboBox.Size = new System.Drawing.Size(75, 23);
            this.fontsizeComboBox.TabIndex = 2;
            this.fontsizeComboBox.SelectedIndexChanged += new System.EventHandler(this.FontsizeComboBoxSelectedIndexChanged);
            // 
            // orientationGroupBox
            // 
            this.orientationGroupBox.Controls.Add(this.horizontalRadioButton);
            this.orientationGroupBox.Controls.Add(this.verticalRadioButton);
            this.orientationGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.orientationGroupBox.Location = new System.Drawing.Point(0, 0);
            this.orientationGroupBox.Name = "orientationGroupBox";
            this.orientationGroupBox.Size = new System.Drawing.Size(114, 100);
            this.orientationGroupBox.TabIndex = 0;
            this.orientationGroupBox.TabStop = false;
            this.orientationGroupBox.Text = "Orientation";
            // 
            // horizontalRadioButton
            // 
            this.horizontalRadioButton.AutoSize = true;
            this.horizontalRadioButton.Location = new System.Drawing.Point(10, 59);
            this.horizontalRadioButton.Name = "horizontalRadioButton";
            this.horizontalRadioButton.Size = new System.Drawing.Size(81, 19);
            this.horizontalRadioButton.TabIndex = 1;
            this.horizontalRadioButton.Text = "Landscape";
            this.horizontalRadioButton.UseVisualStyleBackColor = true;
            // 
            // verticalRadioButton
            // 
            this.verticalRadioButton.AutoSize = true;
            this.verticalRadioButton.Checked = true;
            this.verticalRadioButton.Location = new System.Drawing.Point(10, 23);
            this.verticalRadioButton.Name = "verticalRadioButton";
            this.verticalRadioButton.Size = new System.Drawing.Size(63, 19);
            this.verticalRadioButton.TabIndex = 0;
            this.verticalRadioButton.TabStop = true;
            this.verticalRadioButton.Text = "Vertical";
            this.verticalRadioButton.UseVisualStyleBackColor = true;
            this.verticalRadioButton.CheckedChanged += new System.EventHandler(this.VerticalRadioButtonCheckedChanged);
            // 
            // viewSplitContainer
            // 
            this.viewSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewSplitContainer.Location = new System.Drawing.Point(123, 43);
            this.viewSplitContainer.Name = "viewSplitContainer";
            this.viewSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // viewSplitContainer.Panel1
            // 
            this.viewSplitContainer.Panel1.Controls.Add(this.breakdownsGroupBox);
            // 
            // viewSplitContainer.Panel2
            // 
            this.viewSplitContainer.Panel2.Controls.Add(this.previewGroupBox);
            this.viewSplitContainer.Size = new System.Drawing.Size(1098, 781);
            this.viewSplitContainer.SplitterDistance = 433;
            this.viewSplitContainer.TabIndex = 2;
            // 
            // breakdownsGroupBox
            // 
            this.breakdownsGroupBox.Controls.Add(this.designerDataGridView);
            this.breakdownsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.breakdownsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.breakdownsGroupBox.Name = "breakdownsGroupBox";
            this.breakdownsGroupBox.Size = new System.Drawing.Size(1098, 433);
            this.breakdownsGroupBox.TabIndex = 0;
            this.breakdownsGroupBox.TabStop = false;
            this.breakdownsGroupBox.Text = "Designer";
            // 
            // designerDataGridView
            // 
            this.designerDataGridView.AllowUserToAddRows = false;
            this.designerDataGridView.AllowUserToDeleteRows = false;
            this.designerDataGridView.AllowUserToOrderColumns = true;
            this.designerDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.designerDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.designerDataGridView.Location = new System.Drawing.Point(3, 19);
            this.designerDataGridView.MultiSelect = false;
            this.designerDataGridView.Name = "designerDataGridView";
            this.designerDataGridView.RowTemplate.Height = 25;
            this.designerDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.designerDataGridView.Size = new System.Drawing.Size(1092, 411);
            this.designerDataGridView.TabIndex = 0;
            //this.designerDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DesignerDataGridViewCellContentClick);
            this.designerDataGridView.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DesignerDataGridViewCellValidated);
            this.designerDataGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DesignerDataGridViewCellValueChanged);
            this.designerDataGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.DesignerDataGridViewCurrentCellDirtyStateChanged);
            // 
            // previewGroupBox
            // 
            this.previewGroupBox.Controls.Add(this.previewDataGridView);
            this.previewGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewGroupBox.Location = new System.Drawing.Point(0, 0);
            this.previewGroupBox.Name = "previewGroupBox";
            this.previewGroupBox.Size = new System.Drawing.Size(1098, 344);
            this.previewGroupBox.TabIndex = 0;
            this.previewGroupBox.TabStop = false;
            this.previewGroupBox.Text = "Preview";
            // 
            // previewDataGridView
            // 
            this.previewDataGridView.AllowUserToAddRows = false;
            this.previewDataGridView.AllowUserToDeleteRows = false;
            this.previewDataGridView.AllowUserToOrderColumns = true;
            this.previewDataGridView.AllowUserToResizeRows = false;
            this.previewDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.previewDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewDataGridView.Location = new System.Drawing.Point(3, 19);
            this.previewDataGridView.Name = "previewDataGridView";
            this.previewDataGridView.ReadOnly = true;
            this.previewDataGridView.RowTemplate.Height = 25;
            this.previewDataGridView.Size = new System.Drawing.Size(1092, 322);
            this.previewDataGridView.TabIndex = 0;
            this.previewDataGridView.ColumnDisplayIndexChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.PreviewDataGridViewColumnDisplayIndexChanged);
            this.previewDataGridView.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.PreviewDataGridViewColumnWidthChanged);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(114, 34);
            this.panel2.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(123, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1098, 34);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.timestampCheckBox);
            this.panel3.Controls.Add(this.titleTextBox);
            this.panel3.Controls.Add(this.titleLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(543, 28);
            this.panel3.TabIndex = 1;
            // 
            // timestampCheckBox
            // 
            this.timestampCheckBox.AutoSize = true;
            this.timestampCheckBox.Checked = true;
            this.timestampCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.timestampCheckBox.Location = new System.Drawing.Point(458, 7);
            this.timestampCheckBox.Name = "timestampCheckBox";
            this.timestampCheckBox.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timestampCheckBox.Size = new System.Drawing.Size(85, 19);
            this.timestampCheckBox.TabIndex = 3;
            this.timestampCheckBox.Text = "Timestamp";
            this.timestampCheckBox.UseVisualStyleBackColor = true;
            this.timestampCheckBox.CheckedChanged += new System.EventHandler(this.TimestampCheckBoxCheckedChanged);
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(45, 4);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(286, 23);
            this.titleTextBox.TabIndex = 1;
            this.titleTextBox.TextChanged += new System.EventHandler(this.TitleTextBoxTextChanged);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(10, 7);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(29, 15);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Title";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.saveButton);
            this.panel4.Controls.Add(this.exportButton);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(826, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(269, 28);
            this.panel4.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(107, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // exportButton
            // 
            this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportButton.Location = new System.Drawing.Point(188, 3);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.ExportButton_Click);
            // 
            // PdfDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 827);
            this.Controls.Add(this.mainTableLayout);
            this.Name = "PdfDesignerForm";
            this.ShowInTaskbar = false;
            this.Text = "Pdf Designer Form";
            this.mainTableLayout.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.colorGroupBox.ResumeLayout(false);
            this.alignmentGroupBox.ResumeLayout(false);
            this.alignmentGroupBox.PerformLayout();
            this.fontGroupBox.ResumeLayout(false);
            this.fontGroupBox.PerformLayout();
            this.orientationGroupBox.ResumeLayout(false);
            this.orientationGroupBox.PerformLayout();
            this.viewSplitContainer.Panel1.ResumeLayout(false);
            this.viewSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.viewSplitContainer)).EndInit();
            this.viewSplitContainer.ResumeLayout(false);
            this.breakdownsGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.designerDataGridView)).EndInit();
            this.previewGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.previewDataGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.GroupBox previewGroupBox;
        private System.Windows.Forms.DataGridView previewDataGridView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox orientationGroupBox;
        private System.Windows.Forms.RadioButton horizontalRadioButton;
        private System.Windows.Forms.RadioButton verticalRadioButton;
        private System.Windows.Forms.SplitContainer viewSplitContainer;
        private System.Windows.Forms.GroupBox breakdownsGroupBox;
        private System.Windows.Forms.DataGridView designerDataGridView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button fontButton;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox fontsizeComboBox;
        private System.Windows.Forms.GroupBox fontGroupBox;
        private System.Windows.Forms.Label fontSizeLabel;
        private System.Windows.Forms.CheckBox timestampCheckBox;
        private System.Windows.Forms.GroupBox alignmentGroupBox;
        private System.Windows.Forms.RadioButton rightRadioButton;
        private System.Windows.Forms.RadioButton centerRadioButton;
        private System.Windows.Forms.RadioButton leftRadioButton;
        private System.Windows.Forms.GroupBox colorGroupBox;
        private System.Windows.Forms.Button backgroundColorButton;
        private System.Windows.Forms.Button foregroundColorButton;
    }
}