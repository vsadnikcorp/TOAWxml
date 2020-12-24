namespace TOAWXML
{
    partial class frmUnitDivide
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
            this.btn2Subs = new System.Windows.Forms.Button();
            this.btn3Subs = new System.Windows.Forms.Button();
            this.btnCancelDiv = new System.Windows.Forms.Button();
            this.lblCannotDivide = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn2Subs
            // 
            this.btn2Subs.Location = new System.Drawing.Point(12, 24);
            this.btn2Subs.Name = "btn2Subs";
            this.btn2Subs.Size = new System.Drawing.Size(100, 33);
            this.btn2Subs.TabIndex = 0;
            this.btn2Subs.Text = "2 Subunits";
            this.btn2Subs.UseVisualStyleBackColor = true;
            this.btn2Subs.Click += new System.EventHandler(this.btn2Subs_Click);
            // 
            // btn3Subs
            // 
            this.btn3Subs.Location = new System.Drawing.Point(118, 24);
            this.btn3Subs.Name = "btn3Subs";
            this.btn3Subs.Size = new System.Drawing.Size(100, 33);
            this.btn3Subs.TabIndex = 1;
            this.btn3Subs.Text = "3 Subunits";
            this.btn3Subs.UseVisualStyleBackColor = true;
            this.btn3Subs.Click += new System.EventHandler(this.btn3Subs_Click);
            // 
            // btnCancelDiv
            // 
            this.btnCancelDiv.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelDiv.Location = new System.Drawing.Point(224, 24);
            this.btnCancelDiv.Name = "btnCancelDiv";
            this.btnCancelDiv.Size = new System.Drawing.Size(100, 33);
            this.btnCancelDiv.TabIndex = 2;
            this.btnCancelDiv.Text = "Cancel";
            this.btnCancelDiv.UseVisualStyleBackColor = true;
            this.btnCancelDiv.Click += new System.EventHandler(this.btnCancelDiv_Click);
            // 
            // lblCannotDivide
            // 
            this.lblCannotDivide.AutoSize = true;
            this.lblCannotDivide.Location = new System.Drawing.Point(12, 8);
            this.lblCannotDivide.Name = "lblCannotDivide";
            this.lblCannotDivide.Size = new System.Drawing.Size(0, 13);
            this.lblCannotDivide.TabIndex = 3;
            // 
            // frmUnitDivide
            // 
            this.AcceptButton = this.btnCancelDiv;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelDiv;
            this.ClientSize = new System.Drawing.Size(338, 80);
            this.ControlBox = false;
            this.Controls.Add(this.lblCannotDivide);
            this.Controls.Add(this.btnCancelDiv);
            this.Controls.Add(this.btn3Subs);
            this.Controls.Add(this.btn2Subs);
            this.Name = "frmUnitDivide";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Divide Unit";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn2Subs;
        private System.Windows.Forms.Button btn3Subs;
        private System.Windows.Forms.Button btnCancelDiv;
        private System.Windows.Forms.Label lblCannotDivide;
    }
}