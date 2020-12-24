namespace TOAWXML
{
    partial class frmSelectUnit
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
            this.gbSUForce = new System.Windows.Forms.GroupBox();
            this.trvSUUnitTree = new System.Windows.Forms.TreeView();
            this.rbSUForce1 = new System.Windows.Forms.RadioButton();
            this.rbSUForce2 = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.gbSUForce.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbSUForce
            // 
            this.gbSUForce.Controls.Add(this.rbSUForce2);
            this.gbSUForce.Controls.Add(this.rbSUForce1);
            this.gbSUForce.Location = new System.Drawing.Point(12, 12);
            this.gbSUForce.Name = "gbSUForce";
            this.gbSUForce.Size = new System.Drawing.Size(290, 45);
            this.gbSUForce.TabIndex = 0;
            this.gbSUForce.TabStop = false;
            this.gbSUForce.Text = "FORCE";
            // 
            // trvSUUnitTree
            // 
            this.trvSUUnitTree.Location = new System.Drawing.Point(12, 63);
            this.trvSUUnitTree.Name = "trvSUUnitTree";
            this.trvSUUnitTree.Size = new System.Drawing.Size(290, 503);
            this.trvSUUnitTree.TabIndex = 1;
            this.trvSUUnitTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvSUUnitTree_AfterSelect);
            // 
            // rbSUForce1
            // 
            this.rbSUForce1.AutoSize = true;
            this.rbSUForce1.Location = new System.Drawing.Point(22, 19);
            this.rbSUForce1.Name = "rbSUForce1";
            this.rbSUForce1.Size = new System.Drawing.Size(61, 17);
            this.rbSUForce1.TabIndex = 0;
            this.rbSUForce1.Text = "Force 1";
            this.rbSUForce1.UseVisualStyleBackColor = true;
            this.rbSUForce1.CheckedChanged += new System.EventHandler(this.rbSUForce1_CheckedChanged);
            // 
            // rbSUForce2
            // 
            this.rbSUForce2.AutoSize = true;
            this.rbSUForce2.Location = new System.Drawing.Point(174, 19);
            this.rbSUForce2.Name = "rbSUForce2";
            this.rbSUForce2.Size = new System.Drawing.Size(61, 17);
            this.rbSUForce2.TabIndex = 1;
            this.rbSUForce2.Text = "Force 2";
            this.rbSUForce2.UseVisualStyleBackColor = true;
            this.rbSUForce2.CheckedChanged += new System.EventHandler(this.rbSUForce2_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(12, 578);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Tag = " var f = new frmEvents();";
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(219, 576);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(83, 27);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Tag = " var f = new frmEvents();";
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // frmSelectUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 614);
            this.ControlBox = false;
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.trvSUUnitTree);
            this.Controls.Add(this.gbSUForce);
            this.Name = "frmSelectUnit";
            this.ShowIcon = false;
            this.Text = "Select Unit";
            this.Load += new System.EventHandler(this.frmSelectUnit_Load);
            this.gbSUForce.ResumeLayout(false);
            this.gbSUForce.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbSUForce;
        private System.Windows.Forms.TreeView trvSUUnitTree;
        private System.Windows.Forms.RadioButton rbSUForce2;
        private System.Windows.Forms.RadioButton rbSUForce1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelect;
    }
}