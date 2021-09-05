
namespace TOAWTac
{
    partial class frmSyncGamTac
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSelectGam = new System.Windows.Forms.Button();
            this.btnSelectTac = new System.Windows.Forms.Button();
            this.txtSelectedGam = new System.Windows.Forms.TextBox();
            this.txtSelectedTac = new System.Windows.Forms.TextBox();
            this.btnSync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 176);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 38);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSelectGam
            // 
            this.btnSelectGam.Location = new System.Drawing.Point(12, 12);
            this.btnSelectGam.Name = "btnSelectGam";
            this.btnSelectGam.Size = new System.Drawing.Size(537, 35);
            this.btnSelectGam.TabIndex = 1;
            this.btnSelectGam.Text = "Select .gam file with changes to sync:";
            this.btnSelectGam.UseVisualStyleBackColor = true;
            this.btnSelectGam.Click += new System.EventHandler(this.btnSelectGam_Click);
            // 
            // btnSelectTac
            // 
            this.btnSelectTac.Location = new System.Drawing.Point(12, 99);
            this.btnSelectTac.Name = "btnSelectTac";
            this.btnSelectTac.Size = new System.Drawing.Size(537, 35);
            this.btnSelectTac.TabIndex = 2;
            this.btnSelectTac.Text = "Select .tac file to up updated with data from gam file";
            this.btnSelectTac.UseVisualStyleBackColor = true;
            this.btnSelectTac.Click += new System.EventHandler(this.btnSelectTac_Click);
            // 
            // txtSelectedGam
            // 
            this.txtSelectedGam.Location = new System.Drawing.Point(12, 59);
            this.txtSelectedGam.Name = "txtSelectedGam";
            this.txtSelectedGam.ReadOnly = true;
            this.txtSelectedGam.Size = new System.Drawing.Size(536, 20);
            this.txtSelectedGam.TabIndex = 3;
            // 
            // txtSelectedTac
            // 
            this.txtSelectedTac.Location = new System.Drawing.Point(12, 146);
            this.txtSelectedTac.Name = "txtSelectedTac";
            this.txtSelectedTac.ReadOnly = true;
            this.txtSelectedTac.Size = new System.Drawing.Size(536, 20);
            this.txtSelectedTac.TabIndex = 4;
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(460, 176);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(88, 38);
            this.btnSync.TabIndex = 6;
            this.btnSync.Text = "Sync \r\ngam => tac";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // frmSyncGamTac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 227);
            this.ControlBox = false;
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.txtSelectedTac);
            this.Controls.Add(this.txtSelectedGam);
            this.Controls.Add(this.btnSelectTac);
            this.Controls.Add(this.btnSelectGam);
            this.Controls.Add(this.btnClose);
            this.Name = "frmSyncGamTac";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSelectGam;
        private System.Windows.Forms.Button btnSelectTac;
        private System.Windows.Forms.TextBox txtSelectedGam;
        private System.Windows.Forms.TextBox txtSelectedTac;
        private System.Windows.Forms.Button btnSync;
    }
}