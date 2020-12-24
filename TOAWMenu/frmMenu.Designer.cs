
namespace toawMenu
{
    partial class frmMenu
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
            this.btnOpen = new System.Windows.Forms.Button();
            this.rbTOAWxml = new System.Windows.Forms.RadioButton();
            this.rbEquipView = new System.Windows.Forms.RadioButton();
            this.rbSavedGame = new System.Windows.Forms.RadioButton();
            this.rbTacLayer = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(11, 258);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(69, 25);
            this.btnClose.TabIndex = 0;
            this.btnClose.Tag = "D:\\PROJECTS\\VS\\TOAWXML\\";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(112, 258);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(69, 25);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Tag = "D:\\PROJECTS\\VS\\TOAWXML\\";
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // rbTOAWxml
            // 
            this.rbTOAWxml.AutoSize = true;
            this.rbTOAWxml.Location = new System.Drawing.Point(23, 35);
            this.rbTOAWxml.Name = "rbTOAWxml";
            this.rbTOAWxml.Size = new System.Drawing.Size(77, 17);
            this.rbTOAWxml.TabIndex = 2;
            this.rbTOAWxml.TabStop = true;
            this.rbTOAWxml.Text = "XML Editor";
            this.rbTOAWxml.UseVisualStyleBackColor = true;
            // 
            // rbEquipView
            // 
            this.rbEquipView.AutoSize = true;
            this.rbEquipView.Location = new System.Drawing.Point(23, 58);
            this.rbEquipView.Name = "rbEquipView";
            this.rbEquipView.Size = new System.Drawing.Size(110, 17);
            this.rbEquipView.TabIndex = 3;
            this.rbEquipView.TabStop = true;
            this.rbEquipView.Text = "Equipment Viewer";
            this.rbEquipView.UseVisualStyleBackColor = true;
            // 
            // rbSavedGame
            // 
            this.rbSavedGame.AutoSize = true;
            this.rbSavedGame.Location = new System.Drawing.Point(23, 81);
            this.rbSavedGame.Name = "rbSavedGame";
            this.rbSavedGame.Size = new System.Drawing.Size(117, 17);
            this.rbSavedGame.TabIndex = 4;
            this.rbSavedGame.TabStop = true;
            this.rbSavedGame.Text = "Saved Game Editor";
            this.rbSavedGame.UseVisualStyleBackColor = true;
            // 
            // rbTacLayer
            // 
            this.rbTacLayer.AutoSize = true;
            this.rbTacLayer.Location = new System.Drawing.Point(23, 104);
            this.rbTacLayer.Name = "rbTacLayer";
            this.rbTacLayer.Size = new System.Drawing.Size(92, 17);
            this.rbTacLayer.TabIndex = 5;
            this.rbTacLayer.TabStop = true;
            this.rbTacLayer.Text = "Tactical Layer";
            this.rbTacLayer.UseVisualStyleBackColor = true;
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(193, 303);
            this.ControlBox = false;
            this.Controls.Add(this.rbTacLayer);
            this.Controls.Add(this.rbSavedGame);
            this.Controls.Add(this.rbEquipView);
            this.Controls.Add(this.rbTOAWxml);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnClose);
            this.Name = "frmMenu";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TOAWxml Menu";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.RadioButton rbTOAWxml;
        private System.Windows.Forms.RadioButton rbEquipView;
        private System.Windows.Forms.RadioButton rbSavedGame;
        private System.Windows.Forms.RadioButton rbTacLayer;
    }
}

