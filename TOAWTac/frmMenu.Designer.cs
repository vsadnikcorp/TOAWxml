namespace TOAWTac
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
            this.rbTOAWXML = new System.Windows.Forms.RadioButton();
            this.rbEquipEditor = new System.Windows.Forms.RadioButton();
            this.rbSavedGame = new System.Windows.Forms.RadioButton();
            this.rbMapEdit = new System.Windows.Forms.RadioButton();
            this.rbTacLayer = new System.Windows.Forms.RadioButton();
            this.rbTacMaps = new System.Windows.Forms.RadioButton();
            this.gbTOAWmenu = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.gbTOAWmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbTOAWXML
            // 
            this.rbTOAWXML.AutoSize = true;
            this.rbTOAWXML.Location = new System.Drawing.Point(11, 32);
            this.rbTOAWXML.Name = "rbTOAWXML";
            this.rbTOAWXML.Size = new System.Drawing.Size(77, 17);
            this.rbTOAWXML.TabIndex = 0;
            this.rbTOAWXML.TabStop = true;
            this.rbTOAWXML.Text = "XML Editor";
            this.rbTOAWXML.UseVisualStyleBackColor = true;
            // 
            // rbEquipEditor
            // 
            this.rbEquipEditor.AutoSize = true;
            this.rbEquipEditor.Location = new System.Drawing.Point(11, 64);
            this.rbEquipEditor.Name = "rbEquipEditor";
            this.rbEquipEditor.Size = new System.Drawing.Size(105, 17);
            this.rbEquipEditor.TabIndex = 1;
            this.rbEquipEditor.TabStop = true;
            this.rbEquipEditor.Text = "Equipment Editor";
            this.rbEquipEditor.UseVisualStyleBackColor = true;
            // 
            // rbSavedGame
            // 
            this.rbSavedGame.AutoSize = true;
            this.rbSavedGame.Location = new System.Drawing.Point(11, 96);
            this.rbSavedGame.Name = "rbSavedGame";
            this.rbSavedGame.Size = new System.Drawing.Size(108, 17);
            this.rbSavedGame.TabIndex = 2;
            this.rbSavedGame.TabStop = true;
            this.rbSavedGame.Text = "Edit Saved Game";
            this.rbSavedGame.UseVisualStyleBackColor = true;
            // 
            // rbMapEdit
            // 
            this.rbMapEdit.AutoSize = true;
            this.rbMapEdit.Location = new System.Drawing.Point(11, 128);
            this.rbMapEdit.Name = "rbMapEdit";
            this.rbMapEdit.Size = new System.Drawing.Size(67, 17);
            this.rbMapEdit.TabIndex = 3;
            this.rbMapEdit.TabStop = true;
            this.rbMapEdit.Text = "Edit Map";
            this.rbMapEdit.UseVisualStyleBackColor = true;
            // 
            // rbTacLayer
            // 
            this.rbTacLayer.AutoSize = true;
            this.rbTacLayer.Location = new System.Drawing.Point(11, 160);
            this.rbTacLayer.Name = "rbTacLayer";
            this.rbTacLayer.Size = new System.Drawing.Size(94, 17);
            this.rbTacLayer.TabIndex = 4;
            this.rbTacLayer.TabStop = true;
            this.rbTacLayer.Text = "Edit Tac Layer";
            this.rbTacLayer.UseVisualStyleBackColor = true;
            // 
            // rbTacMaps
            // 
            this.rbTacMaps.AutoSize = true;
            this.rbTacMaps.Location = new System.Drawing.Point(11, 192);
            this.rbTacMaps.Name = "rbTacMaps";
            this.rbTacMaps.Size = new System.Drawing.Size(94, 17);
            this.rbTacMaps.TabIndex = 5;
            this.rbTacMaps.TabStop = true;
            this.rbTacMaps.Text = "Tac Maps Info";
            this.rbTacMaps.UseVisualStyleBackColor = true;
            // 
            // gbTOAWmenu
            // 
            this.gbTOAWmenu.Controls.Add(this.rbTacMaps);
            this.gbTOAWmenu.Controls.Add(this.rbTacLayer);
            this.gbTOAWmenu.Controls.Add(this.rbMapEdit);
            this.gbTOAWmenu.Controls.Add(this.rbSavedGame);
            this.gbTOAWmenu.Controls.Add(this.rbEquipEditor);
            this.gbTOAWmenu.Controls.Add(this.rbTOAWXML);
            this.gbTOAWmenu.Location = new System.Drawing.Point(24, 21);
            this.gbTOAWmenu.Name = "gbTOAWmenu";
            this.gbTOAWmenu.Size = new System.Drawing.Size(138, 236);
            this.gbTOAWmenu.TabIndex = 6;
            this.gbTOAWmenu.TabStop = false;
            this.gbTOAWmenu.Text = "TOAWxml Menu";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(43, 272);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(85, 29);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(192, 317);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.gbTOAWmenu);
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TOAWxml Menu";
            this.gbTOAWmenu.ResumeLayout(false);
            this.gbTOAWmenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbTOAWXML;
        private System.Windows.Forms.RadioButton rbEquipEditor;
        private System.Windows.Forms.RadioButton rbSavedGame;
        private System.Windows.Forms.RadioButton rbMapEdit;
        private System.Windows.Forms.RadioButton rbTacLayer;
        private System.Windows.Forms.RadioButton rbTacMaps;
        private System.Windows.Forms.GroupBox gbTOAWmenu;
        private System.Windows.Forms.Button btnClose;
    }
}