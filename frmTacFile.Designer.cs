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
            this.SuspendLayout();
            // 
            // btnCreateTacFile
            // 
            this.btnCreateTacFile.Location = new System.Drawing.Point(12, 104);
            this.btnCreateTacFile.Name = "btnCreateTacFile";
            this.btnCreateTacFile.Size = new System.Drawing.Size(105, 38);
            this.btnCreateTacFile.TabIndex = 0;
            this.btnCreateTacFile.Text = "Create TacFile";
            this.btnCreateTacFile.UseVisualStyleBackColor = true;
            this.btnCreateTacFile.Click += new System.EventHandler(this.btnCreateTacFile_Click);
            // 
            // frmTacFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 154);
            this.Controls.Add(this.btnCreateTacFile);
            this.Name = "frmTacFile";
            this.Text = "TacFile";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCreateTacFile;
    }
}