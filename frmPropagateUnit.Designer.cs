namespace TOAWXML
{
    partial class frmPropagateUnit
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
            this.btnCancel4 = new System.Windows.Forms.Button();
            this.btnUnitPropagate = new System.Windows.Forms.Button();
            this.grpUnitPropagate = new System.Windows.Forms.GroupBox();
            this.rbUnitPropAllUnits = new System.Windows.Forms.RadioButton();
            this.rbUnitPropForm = new System.Windows.Forms.RadioButton();
            this.chkUnitProf = new System.Windows.Forms.CheckBox();
            this.chkUnitSupply = new System.Windows.Forms.CheckBox();
            this.chkLossTol = new System.Windows.Forms.CheckBox();
            this.chkUnitReplace = new System.Windows.Forms.CheckBox();
            this.chkUnitDeploy = new System.Windows.Forms.CheckBox();
            this.chkUnitReadiness = new System.Windows.Forms.CheckBox();
            this.chkExperience = new System.Windows.Forms.CheckBox();
            this.chkIcon = new System.Windows.Forms.CheckBox();
            this.grpUnitPropagate.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel4
            // 
            this.btnCancel4.Location = new System.Drawing.Point(12, 191);
            this.btnCancel4.Name = "btnCancel4";
            this.btnCancel4.Size = new System.Drawing.Size(100, 33);
            this.btnCancel4.TabIndex = 0;
            this.btnCancel4.Text = "Cancel";
            this.btnCancel4.UseVisualStyleBackColor = true;
            this.btnCancel4.Click += new System.EventHandler(this.btnCancel4_Click);
            // 
            // btnUnitPropagate
            // 
            this.btnUnitPropagate.Location = new System.Drawing.Point(243, 191);
            this.btnUnitPropagate.Name = "btnUnitPropagate";
            this.btnUnitPropagate.Size = new System.Drawing.Size(100, 33);
            this.btnUnitPropagate.TabIndex = 1;
            this.btnUnitPropagate.Text = "Propagate";
            this.btnUnitPropagate.UseVisualStyleBackColor = true;
            this.btnUnitPropagate.Click += new System.EventHandler(this.btnUnitPropagate_Click);
            // 
            // grpUnitPropagate
            // 
            this.grpUnitPropagate.Controls.Add(this.rbUnitPropAllUnits);
            this.grpUnitPropagate.Controls.Add(this.rbUnitPropForm);
            this.grpUnitPropagate.Location = new System.Drawing.Point(12, 12);
            this.grpUnitPropagate.Name = "grpUnitPropagate";
            this.grpUnitPropagate.Size = new System.Drawing.Size(330, 65);
            this.grpUnitPropagate.TabIndex = 2;
            this.grpUnitPropagate.TabStop = false;
            this.grpUnitPropagate.Text = "Unit Propagation";
            // 
            // rbUnitPropAllUnits
            // 
            this.rbUnitPropAllUnits.AutoSize = true;
            this.rbUnitPropAllUnits.Location = new System.Drawing.Point(191, 27);
            this.rbUnitPropAllUnits.Name = "rbUnitPropAllUnits";
            this.rbUnitPropAllUnits.Size = new System.Drawing.Size(127, 17);
            this.rbUnitPropAllUnits.TabIndex = 1;
            this.rbUnitPropAllUnits.TabStop = true;
            this.rbUnitPropAllUnits.Text = "Propagate to All Units";
            this.rbUnitPropAllUnits.UseVisualStyleBackColor = true;
            // 
            // rbUnitPropForm
            // 
            this.rbUnitPropForm.AutoSize = true;
            this.rbUnitPropForm.Location = new System.Drawing.Point(18, 28);
            this.rbUnitPropForm.Name = "rbUnitPropForm";
            this.rbUnitPropForm.Size = new System.Drawing.Size(153, 17);
            this.rbUnitPropForm.TabIndex = 0;
            this.rbUnitPropForm.TabStop = true;
            this.rbUnitPropForm.Text = "Propagate within Formation";
            this.rbUnitPropForm.UseVisualStyleBackColor = true;
            // 
            // chkUnitProf
            // 
            this.chkUnitProf.AutoSize = true;
            this.chkUnitProf.Location = new System.Drawing.Point(30, 92);
            this.chkUnitProf.Name = "chkUnitProf";
            this.chkUnitProf.Size = new System.Drawing.Size(78, 17);
            this.chkUnitProf.TabIndex = 3;
            this.chkUnitProf.Text = "Proficiency";
            this.chkUnitProf.UseVisualStyleBackColor = true;
            // 
            // chkUnitSupply
            // 
            this.chkUnitSupply.AutoSize = true;
            this.chkUnitSupply.Location = new System.Drawing.Point(30, 114);
            this.chkUnitSupply.Name = "chkUnitSupply";
            this.chkUnitSupply.Size = new System.Drawing.Size(58, 17);
            this.chkUnitSupply.TabIndex = 4;
            this.chkUnitSupply.Text = "Supply";
            this.chkUnitSupply.UseVisualStyleBackColor = true;
            // 
            // chkLossTol
            // 
            this.chkLossTol.AutoSize = true;
            this.chkLossTol.Location = new System.Drawing.Point(30, 136);
            this.chkLossTol.Name = "chkLossTol";
            this.chkLossTol.Size = new System.Drawing.Size(99, 17);
            this.chkLossTol.TabIndex = 5;
            this.chkLossTol.Text = "Loss Tolerance";
            this.chkLossTol.UseVisualStyleBackColor = true;
            // 
            // chkUnitReplace
            // 
            this.chkUnitReplace.AutoSize = true;
            this.chkUnitReplace.Location = new System.Drawing.Point(203, 158);
            this.chkUnitReplace.Name = "chkUnitReplace";
            this.chkUnitReplace.Size = new System.Drawing.Size(123, 17);
            this.chkUnitReplace.TabIndex = 8;
            this.chkUnitReplace.Text = "Replacement Priority";
            this.chkUnitReplace.UseVisualStyleBackColor = true;
            // 
            // chkUnitDeploy
            // 
            this.chkUnitDeploy.AutoSize = true;
            this.chkUnitDeploy.Location = new System.Drawing.Point(203, 136);
            this.chkUnitDeploy.Name = "chkUnitDeploy";
            this.chkUnitDeploy.Size = new System.Drawing.Size(82, 17);
            this.chkUnitDeploy.TabIndex = 7;
            this.chkUnitDeploy.Text = "Deployment";
            this.chkUnitDeploy.UseVisualStyleBackColor = true;
            // 
            // chkUnitReadiness
            // 
            this.chkUnitReadiness.AutoSize = true;
            this.chkUnitReadiness.Location = new System.Drawing.Point(203, 92);
            this.chkUnitReadiness.Name = "chkUnitReadiness";
            this.chkUnitReadiness.Size = new System.Drawing.Size(76, 17);
            this.chkUnitReadiness.TabIndex = 6;
            this.chkUnitReadiness.Text = "Readiness";
            this.chkUnitReadiness.UseVisualStyleBackColor = true;
            // 
            // chkExperience
            // 
            this.chkExperience.AutoSize = true;
            this.chkExperience.Location = new System.Drawing.Point(203, 114);
            this.chkExperience.Name = "chkExperience";
            this.chkExperience.Size = new System.Drawing.Size(108, 17);
            this.chkExperience.TabIndex = 9;
            this.chkExperience.Text = "Experience Level";
            this.chkExperience.UseVisualStyleBackColor = true;
            // 
            // chkIcon
            // 
            this.chkIcon.AutoSize = true;
            this.chkIcon.Location = new System.Drawing.Point(30, 158);
            this.chkIcon.Name = "chkIcon";
            this.chkIcon.Size = new System.Drawing.Size(74, 17);
            this.chkIcon.TabIndex = 10;
            this.chkIcon.Text = "Icon Color";
            this.chkIcon.UseVisualStyleBackColor = true;
            // 
            // frmPropagateUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 232);
            this.ControlBox = false;
            this.Controls.Add(this.chkIcon);
            this.Controls.Add(this.chkExperience);
            this.Controls.Add(this.chkUnitReplace);
            this.Controls.Add(this.chkUnitDeploy);
            this.Controls.Add(this.chkUnitReadiness);
            this.Controls.Add(this.chkLossTol);
            this.Controls.Add(this.chkUnitSupply);
            this.Controls.Add(this.chkUnitProf);
            this.Controls.Add(this.grpUnitPropagate);
            this.Controls.Add(this.btnUnitPropagate);
            this.Controls.Add(this.btnCancel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPropagateUnit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Unit Propagation";
            this.Load += new System.EventHandler(this.frmPropagateUnit_Load);
            this.grpUnitPropagate.ResumeLayout(false);
            this.grpUnitPropagate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel4;
        private System.Windows.Forms.Button btnUnitPropagate;
        private System.Windows.Forms.GroupBox grpUnitPropagate;
        private System.Windows.Forms.RadioButton rbUnitPropForm;
        private System.Windows.Forms.RadioButton rbUnitPropAllUnits;
        private System.Windows.Forms.CheckBox chkUnitProf;
        private System.Windows.Forms.CheckBox chkUnitSupply;
        private System.Windows.Forms.CheckBox chkLossTol;
        private System.Windows.Forms.CheckBox chkUnitReplace;
        private System.Windows.Forms.CheckBox chkUnitDeploy;
        private System.Windows.Forms.CheckBox chkUnitReadiness;
        private System.Windows.Forms.CheckBox chkExperience;
        private System.Windows.Forms.CheckBox chkIcon;
    }
}