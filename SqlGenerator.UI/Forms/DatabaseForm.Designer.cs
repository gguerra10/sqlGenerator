
namespace SqlGenerator.UI.Forms
{
    partial class DatabaseForm
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
            this.Database = new System.Windows.Forms.GroupBox();
            this.connectionStringTxt = new System.Windows.Forms.TextBox();
            this.browseBtn = new System.Windows.Forms.Button();
            this.databaseLbl2 = new System.Windows.Forms.Label();
            this.testBtn = new System.Windows.Forms.Button();
            this.openBtn = new System.Windows.Forms.Button();
            this.databaseLabel = new System.Windows.Forms.Label();
            this.databaseTypeCmb = new System.Windows.Forms.ComboBox();
            this.Database.SuspendLayout();
            this.SuspendLayout();
            // 
            // Database
            // 
            this.Database.Controls.Add(this.connectionStringTxt);
            this.Database.Controls.Add(this.browseBtn);
            this.Database.Controls.Add(this.databaseLbl2);
            this.Database.Controls.Add(this.testBtn);
            this.Database.Controls.Add(this.openBtn);
            this.Database.Controls.Add(this.databaseLabel);
            this.Database.Controls.Add(this.databaseTypeCmb);
            this.Database.Location = new System.Drawing.Point(13, 13);
            this.Database.Name = "Database";
            this.Database.Size = new System.Drawing.Size(270, 294);
            this.Database.TabIndex = 0;
            this.Database.TabStop = false;
            this.Database.Text = "Database";
            // 
            // connectionStringTxt
            // 
            this.connectionStringTxt.Location = new System.Drawing.Point(6, 121);
            this.connectionStringTxt.Multiline = true;
            this.connectionStringTxt.Name = "connectionStringTxt";
            this.connectionStringTxt.Size = new System.Drawing.Size(256, 138);
            this.connectionStringTxt.TabIndex = 5;
            this.connectionStringTxt.TextChanged += new System.EventHandler(this.ConnectionStringTxt_TextChanged);
            // 
            // browseBtn
            // 
            this.browseBtn.Location = new System.Drawing.Point(187, 79);
            this.browseBtn.Name = "browseBtn";
            this.browseBtn.Size = new System.Drawing.Size(75, 23);
            this.browseBtn.TabIndex = 4;
            this.browseBtn.Text = "Browse";
            this.browseBtn.UseVisualStyleBackColor = true;
            this.browseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // databaseLbl2
            // 
            this.databaseLbl2.AutoSize = true;
            this.databaseLbl2.Location = new System.Drawing.Point(6, 83);
            this.databaseLbl2.Name = "databaseLbl2";
            this.databaseLbl2.Size = new System.Drawing.Size(105, 15);
            this.databaseLbl2.TabIndex = 3;
            this.databaseLbl2.Text = "Connection string:";
            // 
            // testBtn
            // 
            this.testBtn.Location = new System.Drawing.Point(6, 265);
            this.testBtn.Name = "testBtn";
            this.testBtn.Size = new System.Drawing.Size(75, 23);
            this.testBtn.TabIndex = 2;
            this.testBtn.Text = "Test";
            this.testBtn.UseVisualStyleBackColor = true;
            this.testBtn.Click += new System.EventHandler(this.TestBtn_Click);
            // 
            // openBtn
            // 
            this.openBtn.Enabled = false;
            this.openBtn.Location = new System.Drawing.Point(187, 265);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(75, 23);
            this.openBtn.TabIndex = 2;
            this.openBtn.Text = "Open";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.OpenBtn_Click);
            // 
            // databaseLabel
            // 
            this.databaseLabel.AutoSize = true;
            this.databaseLabel.Location = new System.Drawing.Point(6, 30);
            this.databaseLabel.Name = "databaseLabel";
            this.databaseLabel.Size = new System.Drawing.Size(84, 15);
            this.databaseLabel.TabIndex = 1;
            this.databaseLabel.Text = "Database type:";
            // 
            // databaseTypeCmb
            // 
            this.databaseTypeCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.databaseTypeCmb.Location = new System.Drawing.Point(122, 27);
            this.databaseTypeCmb.Name = "databaseTypeCmb";
            this.databaseTypeCmb.Size = new System.Drawing.Size(140, 23);
            this.databaseTypeCmb.TabIndex = 0;
            this.databaseTypeCmb.SelectedIndexChanged += new System.EventHandler(this.DatabaseTypeCmb_SelectedIndexChanged);
            // 
            // DatabaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 319);
            this.Controls.Add(this.Database);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "DatabaseForm";
            this.ShowInTaskbar = false;
            this.Text = "Database";
            this.Database.ResumeLayout(false);
            this.Database.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Database;
        private System.Windows.Forms.Label databaseLabel;
        private System.Windows.Forms.ComboBox databaseTypeCmb;
        private System.Windows.Forms.Button testBtn;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Label databaseLbl2;
        private System.Windows.Forms.Button browseBtn;
        private System.Windows.Forms.TextBox connectionStringTxt;
    }
}