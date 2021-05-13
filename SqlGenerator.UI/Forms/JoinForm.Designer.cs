
namespace SqlGenerator.UI.Forms
{
    partial class JoinForm
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
            this.cancelBtn = new System.Windows.Forms.Button();
            this.acceptBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSqlJoin = new System.Windows.Forms.TextBox();
            this.conditionsGridView = new System.Windows.Forms.DataGridView();
            this.lblType = new System.Windows.Forms.Label();
            this.joinTypeCmb = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conditionsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(570, 292);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 6;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // acceptBtn
            // 
            this.acceptBtn.Location = new System.Drawing.Point(489, 292);
            this.acceptBtn.Name = "acceptBtn";
            this.acceptBtn.Size = new System.Drawing.Size(75, 23);
            this.acceptBtn.TabIndex = 5;
            this.acceptBtn.Text = "Accept";
            this.acceptBtn.UseVisualStyleBackColor = true;
            this.acceptBtn.Click += new System.EventHandler(this.AcceptBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSqlJoin);
            this.groupBox1.Controls.Add(this.conditionsGridView);
            this.groupBox1.Controls.Add(this.lblType);
            this.groupBox1.Controls.Add(this.joinTypeCmb);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(635, 274);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Join";
            // 
            // txtSqlJoin
            // 
            this.txtSqlJoin.Enabled = false;
            this.txtSqlJoin.Location = new System.Drawing.Point(181, 38);
            this.txtSqlJoin.Name = "txtSqlJoin";
            this.txtSqlJoin.Size = new System.Drawing.Size(448, 23);
            this.txtSqlJoin.TabIndex = 5;
            // 
            // conditionsGridView
            // 
            this.conditionsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conditionsGridView.Location = new System.Drawing.Point(12, 77);
            this.conditionsGridView.Name = "conditionsGridView";
            this.conditionsGridView.RowTemplate.Height = 25;
            this.conditionsGridView.Size = new System.Drawing.Size(617, 191);
            this.conditionsGridView.TabIndex = 3;
            this.conditionsGridView.CurrentCellDirtyStateChanged += new System.EventHandler(this.ConditionsGridView_CurrentCellDirtyStateChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(12, 41);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 15);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Type:";
            // 
            // joinTypeCmb
            // 
            this.joinTypeCmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.joinTypeCmb.FormattingEnabled = true;
            this.joinTypeCmb.Location = new System.Drawing.Point(56, 38);
            this.joinTypeCmb.Name = "joinTypeCmb";
            this.joinTypeCmb.Size = new System.Drawing.Size(119, 23);
            this.joinTypeCmb.TabIndex = 0;
            this.joinTypeCmb.SelectedIndexChanged += new System.EventHandler(this.FilterCmb_SelectedIndexChanged);
            // 
            // JoinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(659, 327);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.acceptBtn);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "JoinForm";
            this.ShowInTaskbar = false;
            this.Text = "Join";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conditionsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button acceptBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox joinTypeCmb;
        private System.Windows.Forms.DataGridView conditionsGridView;
        private System.Windows.Forms.TextBox txtSqlJoin;
    }
}