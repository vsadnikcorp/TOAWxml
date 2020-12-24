namespace TOAWXML
{
    partial class frmSelectFormation
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
            this.gbForce = new System.Windows.Forms.GroupBox();
            this.trvSFTree = new System.Windows.Forms.TreeView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.rbForce1 = new System.Windows.Forms.RadioButton();
            this.rbForce2 = new System.Windows.Forms.RadioButton();
            this.gbForce.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbForce
            // 
            this.gbForce.Controls.Add(this.rbForce2);
            this.gbForce.Controls.Add(this.rbForce1);
            this.gbForce.Location = new System.Drawing.Point(11, 12);
            this.gbForce.Name = "gbForce";
            this.gbForce.Size = new System.Drawing.Size(290, 45);
            this.gbForce.TabIndex = 0;
            this.gbForce.TabStop = false;
            this.gbForce.Text = "FORCE";
            // 
            // trvSFTree
            // 
            this.trvSFTree.Location = new System.Drawing.Point(11, 65);
            this.trvSFTree.Name = "trvSFTree";
            this.trvSFTree.Size = new System.Drawing.Size(290, 503);
            this.trvSFTree.TabIndex = 1;
            this.trvSFTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvSFTree_AfterSelect);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(11, 575);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(83, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Tag = "C:\\Users\\Thomas Reiter\\OneDrive\\Documents\\Visual Studio 2017\\Projects\\CMOO-2-2\\TO" +
    "AWXML\\frmSelectUnit.cs";
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(218, 575);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(83, 27);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Tag = "";
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // rbForce1
            // 
            this.rbForce1.AutoSize = true;
            this.rbForce1.Location = new System.Drawing.Point(16, 19);
            this.rbForce1.Name = "rbForce1";
            this.rbForce1.Size = new System.Drawing.Size(61, 17);
            this.rbForce1.TabIndex = 0;
            this.rbForce1.Text = "Force 1";
            this.rbForce1.UseVisualStyleBackColor = true;
            this.rbForce1.CheckedChanged += new System.EventHandler(this.rbForce1_CheckedChanged);
            // 
            // rbForce2
            // 
            this.rbForce2.AutoSize = true;
            this.rbForce2.Location = new System.Drawing.Point(174, 19);
            this.rbForce2.Name = "rbForce2";
            this.rbForce2.Size = new System.Drawing.Size(61, 17);
            this.rbForce2.TabIndex = 1;
            this.rbForce2.Text = "Force 2";
            this.rbForce2.UseVisualStyleBackColor = true;
            this.rbForce2.CheckedChanged += new System.EventHandler(this.rbForce2_CheckedChanged);
            // 
            // frmSelectFormation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 614);
            this.ControlBox = false;
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.trvSFTree);
            this.Controls.Add(this.gbForce);
            this.Name = "frmSelectFormation";
            this.ShowIcon = false;
            this.Text = "Select Formation";
            this.Load += new System.EventHandler(this.frmSelectFormation_Load);
            this.gbForce.ResumeLayout(false);
            this.gbForce.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbForce;
        private System.Windows.Forms.TreeView trvSFTree;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.RadioButton rbForce2;
        private System.Windows.Forms.RadioButton rbForce1;
    }
}