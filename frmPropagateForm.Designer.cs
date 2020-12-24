namespace TOAWXML
{
    partial class frmPropagateForm
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
            this.btnCancel3 = new System.Windows.Forms.Button();
            this.btnFormPropagate = new System.Windows.Forms.Button();
            this.grpFormPropagate = new System.Windows.Forms.GroupBox();
            this.rbFormPropForms = new System.Windows.Forms.RadioButton();
            this.rbFormPropSubunits = new System.Windows.Forms.RadioButton();
            this.chkFormProf = new System.Windows.Forms.CheckBox();
            this.chkFormSupply = new System.Windows.Forms.CheckBox();
            this.chkFormLossTol = new System.Windows.Forms.CheckBox();
            this.chkFormSuppScope = new System.Windows.Forms.CheckBox();
            this.chkFormOrders = new System.Windows.Forms.CheckBox();
            this.grpFormPropagate.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel3
            // 
            this.btnCancel3.Location = new System.Drawing.Point(12, 187);
            this.btnCancel3.Name = "btnCancel3";
            this.btnCancel3.Size = new System.Drawing.Size(100, 33);
            this.btnCancel3.TabIndex = 0;
            this.btnCancel3.Text = "Cancel";
            this.btnCancel3.UseVisualStyleBackColor = true;
            this.btnCancel3.Click += new System.EventHandler(this.btnCancel3_Click);
            // 
            // btnFormPropagate
            // 
            this.btnFormPropagate.Location = new System.Drawing.Point(235, 187);
            this.btnFormPropagate.Name = "btnFormPropagate";
            this.btnFormPropagate.Size = new System.Drawing.Size(100, 33);
            this.btnFormPropagate.TabIndex = 1;
            this.btnFormPropagate.Text = "Propagate";
            this.btnFormPropagate.UseVisualStyleBackColor = true;
            this.btnFormPropagate.Click += new System.EventHandler(this.btnFormPropagate_Click);
            // 
            // grpFormPropagate
            // 
            this.grpFormPropagate.Controls.Add(this.rbFormPropSubunits);
            this.grpFormPropagate.Controls.Add(this.rbFormPropForms);
            this.grpFormPropagate.Location = new System.Drawing.Point(12, 12);
            this.grpFormPropagate.Name = "grpFormPropagate";
            this.grpFormPropagate.Size = new System.Drawing.Size(330, 65);
            this.grpFormPropagate.TabIndex = 2;
            this.grpFormPropagate.TabStop = false;
            this.grpFormPropagate.Text = "Formation Propagation";
            // 
            // rbFormPropForms
            // 
            this.rbFormPropForms.AutoSize = true;
            this.rbFormPropForms.Location = new System.Drawing.Point(170, 27);
            this.rbFormPropForms.Name = "rbFormPropForms";
            this.rbFormPropForms.Size = new System.Drawing.Size(154, 17);
            this.rbFormPropForms.TabIndex = 0;
            this.rbFormPropForms.TabStop = true;
            this.rbFormPropForms.Text = "Propagate to All Formations";
            this.rbFormPropForms.UseVisualStyleBackColor = true;
            this.rbFormPropForms.CheckedChanged += new System.EventHandler(this.rbFormPropForms_CheckedChanged);
            // 
            // rbFormPropSubunits
            // 
            this.rbFormPropSubunits.AutoSize = true;
            this.rbFormPropSubunits.Location = new System.Drawing.Point(10, 27);
            this.rbFormPropSubunits.Name = "rbFormPropSubunits";
            this.rbFormPropSubunits.Size = new System.Drawing.Size(144, 17);
            this.rbFormPropSubunits.TabIndex = 1;
            this.rbFormPropSubunits.TabStop = true;
            this.rbFormPropSubunits.Text = "Propagate to All Subunits";
            this.rbFormPropSubunits.UseVisualStyleBackColor = true;
            this.rbFormPropSubunits.CheckedChanged += new System.EventHandler(this.rbFormPropSubunits_CheckedChanged);
            // 
            // chkFormProf
            // 
            this.chkFormProf.AutoSize = true;
            this.chkFormProf.Location = new System.Drawing.Point(22, 95);
            this.chkFormProf.Name = "chkFormProf";
            this.chkFormProf.Size = new System.Drawing.Size(78, 17);
            this.chkFormProf.TabIndex = 3;
            this.chkFormProf.Text = "Proficiency";
            this.chkFormProf.UseVisualStyleBackColor = true;
            // 
            // chkFormSupply
            // 
            this.chkFormSupply.AutoSize = true;
            this.chkFormSupply.Location = new System.Drawing.Point(22, 118);
            this.chkFormSupply.Name = "chkFormSupply";
            this.chkFormSupply.Size = new System.Drawing.Size(58, 17);
            this.chkFormSupply.TabIndex = 4;
            this.chkFormSupply.Text = "Supply";
            this.chkFormSupply.UseVisualStyleBackColor = true;
            // 
            // chkFormLossTol
            // 
            this.chkFormLossTol.AutoSize = true;
            this.chkFormLossTol.Location = new System.Drawing.Point(22, 141);
            this.chkFormLossTol.Name = "chkFormLossTol";
            this.chkFormLossTol.Size = new System.Drawing.Size(99, 17);
            this.chkFormLossTol.TabIndex = 5;
            this.chkFormLossTol.Text = "Loss Tolerance";
            this.chkFormLossTol.UseVisualStyleBackColor = true;
            // 
            // chkFormSuppScope
            // 
            this.chkFormSuppScope.AutoSize = true;
            this.chkFormSuppScope.Location = new System.Drawing.Point(182, 95);
            this.chkFormSuppScope.Name = "chkFormSuppScope";
            this.chkFormSuppScope.Size = new System.Drawing.Size(97, 17);
            this.chkFormSuppScope.TabIndex = 6;
            this.chkFormSuppScope.Text = "Support Scope";
            this.chkFormSuppScope.UseVisualStyleBackColor = true;
            // 
            // chkFormOrders
            // 
            this.chkFormOrders.AutoSize = true;
            this.chkFormOrders.Location = new System.Drawing.Point(182, 118);
            this.chkFormOrders.Name = "chkFormOrders";
            this.chkFormOrders.Size = new System.Drawing.Size(57, 17);
            this.chkFormOrders.TabIndex = 7;
            this.chkFormOrders.Text = "Orders";
            this.chkFormOrders.UseVisualStyleBackColor = true;
            // 
            // frmPropagateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 232);
            this.ControlBox = false;
            this.Controls.Add(this.chkFormOrders);
            this.Controls.Add(this.chkFormSuppScope);
            this.Controls.Add(this.chkFormLossTol);
            this.Controls.Add(this.chkFormSupply);
            this.Controls.Add(this.chkFormProf);
            this.Controls.Add(this.grpFormPropagate);
            this.Controls.Add(this.btnFormPropagate);
            this.Controls.Add(this.btnCancel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPropagateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formation Propagation";
            this.Load += new System.EventHandler(this.frmPropagateForm_Load);
            this.grpFormPropagate.ResumeLayout(false);
            this.grpFormPropagate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel3;
        private System.Windows.Forms.Button btnFormPropagate;
        private System.Windows.Forms.GroupBox grpFormPropagate;
        private System.Windows.Forms.RadioButton rbFormPropSubunits;
        private System.Windows.Forms.RadioButton rbFormPropForms;
        private System.Windows.Forms.CheckBox chkFormProf;
        private System.Windows.Forms.CheckBox chkFormSupply;
        private System.Windows.Forms.CheckBox chkFormLossTol;
        private System.Windows.Forms.CheckBox chkFormSuppScope;
        private System.Windows.Forms.CheckBox chkFormOrders;
    }
}