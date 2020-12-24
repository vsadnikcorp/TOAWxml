namespace TOAWXML
{
    partial class frmEquipView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvEquipView = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtEquipFile = new System.Windows.Forms.TextBox();
            this.btnEqpFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipView)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEquipView
            // 
            this.dgvEquipView.AllowUserToAddRows = false;
            this.dgvEquipView.AllowUserToDeleteRows = false;
            this.dgvEquipView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            this.dgvEquipView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEquipView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipView.Location = new System.Drawing.Point(12, 12);
            this.dgvEquipView.MultiSelect = false;
            this.dgvEquipView.Name = "dgvEquipView";
            this.dgvEquipView.RowHeadersWidth = 25;
            this.dgvEquipView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvEquipView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEquipView.Size = new System.Drawing.Size(1211, 645);
            this.dgvEquipView.TabIndex = 0;
            this.dgvEquipView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvEquipView_CellFormatting);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 679);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(91, 33);
            this.btnClose.TabIndex = 1;
            this.btnClose.Tag = "  frmDepots depotform = new frmDepots();  frmDepots depotform = new frmDepots();";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1132, 679);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(91, 33);
            this.btnSave.TabIndex = 2;
            this.btnSave.Tag = "  frmDepots depotform = new frmDepots();  frmDepots depotform = new frmDepots();";
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtEquipFile
            // 
            this.txtEquipFile.Location = new System.Drawing.Point(221, 686);
            this.txtEquipFile.Name = "txtEquipFile";
            this.txtEquipFile.Size = new System.Drawing.Size(902, 20);
            this.txtEquipFile.TabIndex = 3;
            // 
            // btnEqpFile
            // 
            this.btnEqpFile.Location = new System.Drawing.Point(136, 677);
            this.btnEqpFile.Name = "btnEqpFile";
            this.btnEqpFile.Size = new System.Drawing.Size(69, 39);
            this.btnEqpFile.TabIndex = 4;
            this.btnEqpFile.Text = "Load eqp File";
            this.btnEqpFile.UseVisualStyleBackColor = true;
            this.btnEqpFile.Click += new System.EventHandler(this.btnEqpFile_Click);
            // 
            // frmEquipView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 735);
            this.ControlBox = false;
            this.Controls.Add(this.btnEqpFile);
            this.Controls.Add(this.txtEquipFile);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvEquipView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmEquipView";
            this.ShowIcon = false;
            this.Text = "Equipment Viewer";
            this.Load += new System.EventHandler(this.frmEquipView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEquipView;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtEquipFile;
        private System.Windows.Forms.Button btnEqpFile;
    }
}