namespace TOAWXML
{
    partial class frmDepots
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDepots = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDepot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCloseDepot = new System.Windows.Forms.Button();
            this.depotDataSet = new System.Data.DataSet();
            this.btnSaveDepot = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepots)).BeginInit();
            this.contextMenuStripDepot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.depotDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDepots
            // 
            this.dgvDepots.AllowUserToAddRows = false;
            this.dgvDepots.AllowUserToDeleteRows = false;
            this.dgvDepots.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.dgvDepots.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDepots.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepots.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDepots.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvDepots.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepots.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDepots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDepots.ContextMenuStrip = this.contextMenuStripDepot;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDepots.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDepots.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDepots.Location = new System.Drawing.Point(13, 13);
            this.dgvDepots.Margin = new System.Windows.Forms.Padding(4);
            this.dgvDepots.MultiSelect = false;
            this.dgvDepots.Name = "dgvDepots";
            this.dgvDepots.RowHeadersVisible = false;
            this.dgvDepots.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDepots.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepots.Size = new System.Drawing.Size(390, 194);
            this.dgvDepots.TabIndex = 0;
            this.dgvDepots.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDepots_CellFormatting);
            this.dgvDepots.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDepots_CellValidating);
            // 
            // contextMenuStripDepot
            // 
            this.contextMenuStripDepot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.addToolStripMenuItem1});
            this.contextMenuStripDepot.Name = "contextMenuStripDepot";
            this.contextMenuStripDepot.Size = new System.Drawing.Size(108, 48);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem1
            // 
            this.addToolStripMenuItem1.Name = "addToolStripMenuItem1";
            this.addToolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.addToolStripMenuItem1.Text = "Add";
            this.addToolStripMenuItem1.Click += new System.EventHandler(this.addToolStripMenuItem1_Click);
            // 
            // btnCloseDepot
            // 
            this.btnCloseDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCloseDepot.Location = new System.Drawing.Point(13, 221);
            this.btnCloseDepot.Margin = new System.Windows.Forms.Padding(4);
            this.btnCloseDepot.Name = "btnCloseDepot";
            this.btnCloseDepot.Size = new System.Drawing.Size(100, 33);
            this.btnCloseDepot.TabIndex = 2;
            this.btnCloseDepot.Text = "Close";
            this.btnCloseDepot.UseVisualStyleBackColor = true;
            this.btnCloseDepot.Click += new System.EventHandler(this.btnCloseDepot_Click);
            // 
            // depotDataSet
            // 
            this.depotDataSet.DataSetName = "depotDataSet";
            // 
            // btnSaveDepot
            // 
            this.btnSaveDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveDepot.Location = new System.Drawing.Point(303, 221);
            this.btnSaveDepot.Name = "btnSaveDepot";
            this.btnSaveDepot.Size = new System.Drawing.Size(100, 33);
            this.btnSaveDepot.TabIndex = 3;
            this.btnSaveDepot.Text = "Save";
            this.btnSaveDepot.UseVisualStyleBackColor = true;
            this.btnSaveDepot.Click += new System.EventHandler(this.btnSaveDepot_Click);
            // 
            // frmDepots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 268);
            this.ControlBox = false;
            this.Controls.Add(this.btnSaveDepot);
            this.Controls.Add(this.btnCloseDepot);
            this.Controls.Add(this.dgvDepots);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDepots";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supply Depots";
            this.Load += new System.EventHandler(this.frmDepots_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepots)).EndInit();
            this.contextMenuStripDepot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.depotDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDepots;
        private System.Windows.Forms.Button btnCloseDepot;
        private System.Data.DataSet depotDataSet;
        private System.Windows.Forms.Button btnSaveDepot;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDepot;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem1;
    }
}