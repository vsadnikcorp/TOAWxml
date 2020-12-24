namespace TOAWXML
{
    partial class frmTacFile
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
            this.btnCreateTacFile = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTacFile = new System.Windows.Forms.Label();
            this.txtTacFile = new System.Windows.Forms.TextBox();
            this.btnLoadTacFile = new System.Windows.Forms.Button();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbForce = new System.Windows.Forms.GroupBox();
            this.rbForce2 = new System.Windows.Forms.RadioButton();
            this.rbForce1 = new System.Windows.Forms.RadioButton();
            this.trvUnitTree = new System.Windows.Forms.TreeView();
            this.ssTac = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.btnSync = new System.Windows.Forms.Button();
            this.gbForce.SuspendLayout();
            this.ssTac.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateTacFile
            // 
            this.btnCreateTacFile.Location = new System.Drawing.Point(185, 627);
            this.btnCreateTacFile.Name = "btnCreateTacFile";
            this.btnCreateTacFile.Size = new System.Drawing.Size(60, 40);
            this.btnCreateTacFile.TabIndex = 0;
            this.btnCreateTacFile.Text = "Create TacFile";
            this.btnCreateTacFile.UseVisualStyleBackColor = true;
            this.btnCreateTacFile.Click += new System.EventHandler(this.btnCreateTacFile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(11, 627);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 38);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTacFile
            // 
            this.lblTacFile.AutoSize = true;
            this.lblTacFile.Location = new System.Drawing.Point(16, 604);
            this.lblTacFile.Name = "lblTacFile";
            this.lblTacFile.Size = new System.Drawing.Size(71, 13);
            this.lblTacFile.TabIndex = 5;
            this.lblTacFile.Text = "Current *.tam:";
            this.lblTacFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTacFile
            // 
            this.txtTacFile.Location = new System.Drawing.Point(90, 601);
            this.txtTacFile.Name = "txtTacFile";
            this.txtTacFile.Size = new System.Drawing.Size(784, 20);
            this.txtTacFile.TabIndex = 4;
            // 
            // btnLoadTacFile
            // 
            this.btnLoadTacFile.Location = new System.Drawing.Point(101, 627);
            this.btnLoadTacFile.Name = "btnLoadTacFile";
            this.btnLoadTacFile.Size = new System.Drawing.Size(60, 40);
            this.btnLoadTacFile.TabIndex = 6;
            this.btnLoadTacFile.Text = "Load \r\nTacFile";
            this.btnLoadTacFile.UseVisualStyleBackColor = true;
            this.btnLoadTacFile.Click += new System.EventHandler(this.btnLoadTacFile_Click);
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.CustomFormat = "\"dd MMM yyyy\"";
            this.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePicker.Location = new System.Drawing.Point(772, 12);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(102, 20);
            this.DateTimePicker.TabIndex = 7;
            this.DateTimePicker.Value = new System.DateTime(1941, 6, 22, 0, 0, 0, 0);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(796, 629);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 38);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // gbForce
            // 
            this.gbForce.Controls.Add(this.rbForce2);
            this.gbForce.Controls.Add(this.rbForce1);
            this.gbForce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbForce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbForce.Location = new System.Drawing.Point(13, 13);
            this.gbForce.Name = "gbForce";
            this.gbForce.Size = new System.Drawing.Size(291, 52);
            this.gbForce.TabIndex = 24;
            this.gbForce.TabStop = false;
            this.gbForce.Text = "FORCE";
            // 
            // rbForce2
            // 
            this.rbForce2.AutoSize = true;
            this.rbForce2.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbForce2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbForce2.Location = new System.Drawing.Point(135, 23);
            this.rbForce2.Name = "rbForce2";
            this.rbForce2.Size = new System.Drawing.Size(58, 17);
            this.rbForce2.TabIndex = 22;
            this.rbForce2.TabStop = true;
            this.rbForce2.Text = "Force2";
            this.rbForce2.UseVisualStyleBackColor = true;
            this.rbForce2.CheckedChanged += new System.EventHandler(this.rbForce2_CheckedChanged);
            // 
            // rbForce1
            // 
            this.rbForce1.AutoSize = true;
            this.rbForce1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbForce1.Location = new System.Drawing.Point(12, 23);
            this.rbForce1.Name = "rbForce1";
            this.rbForce1.Size = new System.Drawing.Size(58, 17);
            this.rbForce1.TabIndex = 21;
            this.rbForce1.TabStop = true;
            this.rbForce1.Text = "Force1";
            this.rbForce1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.rbForce1.UseVisualStyleBackColor = true;
            this.rbForce1.CheckedChanged += new System.EventHandler(this.rbForce1_CheckedChanged);
            // 
            // trvUnitTree
            // 
            this.trvUnitTree.AllowDrop = true;
            this.trvUnitTree.CheckBoxes = true;
            this.trvUnitTree.Location = new System.Drawing.Point(13, 71);
            this.trvUnitTree.Name = "trvUnitTree";
            this.trvUnitTree.Size = new System.Drawing.Size(291, 524);
            this.trvUnitTree.TabIndex = 25;
            // 
            // ssTac
            // 
            this.ssTac.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.ssTac.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ssTac.Location = new System.Drawing.Point(0, 653);
            this.ssTac.Name = "ssTac";
            this.ssTac.Size = new System.Drawing.Size(886, 22);
            this.ssTac.TabIndex = 26;
            this.ssTac.Text = "StatusStrip";
            this.ssTac.Visible = false;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(269, 629);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(60, 40);
            this.btnSync.TabIndex = 27;
            this.btnSync.Text = "Sync \r\nTacFile";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // frmTacFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 675);
            this.ControlBox = false;
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.ssTac);
            this.Controls.Add(this.trvUnitTree);
            this.Controls.Add(this.gbForce);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.DateTimePicker);
            this.Controls.Add(this.btnLoadTacFile);
            this.Controls.Add(this.lblTacFile);
            this.Controls.Add(this.txtTacFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateTacFile);
            this.Name = "frmTacFile";
            this.ShowIcon = false;
            this.Text = "TacFile";
            this.Load += new System.EventHandler(this.frmTacFile_Load);
            this.gbForce.ResumeLayout(false);
            this.gbForce.PerformLayout();
            this.ssTac.ResumeLayout(false);
            this.ssTac.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateTacFile;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTacFile;
        private System.Windows.Forms.TextBox txtTacFile;
        private System.Windows.Forms.Button btnLoadTacFile;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbForce;
        private System.Windows.Forms.RadioButton rbForce2;
        private System.Windows.Forms.RadioButton rbForce1;
        private System.Windows.Forms.TreeView trvUnitTree;
        private System.Windows.Forms.StatusStrip ssTac;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button btnSync;
    }
}