namespace TOAWXML
{
    partial class frmEquip
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cboCountry = new System.Windows.Forms.ComboBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.cboEquipType = new System.Windows.Forms.ComboBox();
            this.cboMovement = new System.Windows.Forms.ComboBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.lblCategory = new System.Windows.Forms.Label();
            this.lblUnitChar = new System.Windows.Forms.Label();
            this.lblMovement = new System.Windows.Forms.Label();
            this.btnEquipCancel = new System.Windows.Forms.Button();
            this.btnEquipLoad = new System.Windows.Forms.Button();
            this.btnEquipAdd = new System.Windows.Forms.Button();
            this.txtEqpFile = new System.Windows.Forms.TextBox();
            this.lblEqpFile = new System.Windows.Forms.Label();
            this.lblWeapons = new System.Windows.Forms.Label();
            this.cboWeapons = new System.Windows.Forms.ComboBox();
            this.lblNaval = new System.Windows.Forms.Label();
            this.cboNaval = new System.Windows.Forms.ComboBox();
            this.lblDefensive = new System.Windows.Forms.Label();
            this.cboDefensive = new System.Windows.Forms.ComboBox();
            this.lblEngineering = new System.Windows.Forms.Label();
            this.cboEngineering = new System.Windows.Forms.ComboBox();
            this.dgvEquipment = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEquipCount = new System.Windows.Forms.TextBox();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.lbEquipChars = new System.Windows.Forms.ListBox();
            this.lblEquipChars = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.dgvEqpValues = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEqpValues)).BeginInit();
            this.SuspendLayout();
            // 
            // cboCountry
            // 
            this.cboCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCountry.DropDownWidth = 165;
            this.cboCountry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCountry.FormattingEnabled = true;
            this.cboCountry.Location = new System.Drawing.Point(15, 73);
            this.cboCountry.MaxDropDownItems = 50;
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.Size = new System.Drawing.Size(150, 21);
            this.cboCountry.TabIndex = 1;
            this.cboCountry.SelectionChangeCommitted += new System.EventHandler(this.cboCountry_SelectionChangeCommitted);
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(15, 31);
            this.cboCategory.MaxDropDownItems = 15;
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(150, 21);
            this.cboCategory.TabIndex = 0;
            this.cboCategory.SelectionChangeCommitted += new System.EventHandler(this.cboCategory_SelectionChangeCommitted);
            // 
            // cboEquipType
            // 
            this.cboEquipType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEquipType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEquipType.FormattingEnabled = true;
            this.cboEquipType.Location = new System.Drawing.Point(15, 115);
            this.cboEquipType.MaxDropDownItems = 15;
            this.cboEquipType.Name = "cboEquipType";
            this.cboEquipType.Size = new System.Drawing.Size(150, 21);
            this.cboEquipType.TabIndex = 2;
            this.cboEquipType.SelectionChangeCommitted += new System.EventHandler(this.cboEquipType_SelectionChangeCommitted);
            // 
            // cboMovement
            // 
            this.cboMovement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMovement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMovement.FormattingEnabled = true;
            this.cboMovement.Location = new System.Drawing.Point(15, 157);
            this.cboMovement.MaxDropDownItems = 20;
            this.cboMovement.Name = "cboMovement";
            this.cboMovement.Size = new System.Drawing.Size(150, 21);
            this.cboMovement.TabIndex = 3;
            this.cboMovement.SelectionChangeCommitted += new System.EventHandler(this.cboMovement_SelectionChangeCommitted);
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Location = new System.Drawing.Point(15, 56);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(46, 13);
            this.lblCountry.TabIndex = 4;
            this.lblCountry.Text = "Country:";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(15, 14);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(105, 13);
            this.lblCategory.TabIndex = 5;
            this.lblCategory.Text = "Equipment Category:";
            // 
            // lblUnitChar
            // 
            this.lblUnitChar.AutoSize = true;
            this.lblUnitChar.Location = new System.Drawing.Point(15, 98);
            this.lblUnitChar.Name = "lblUnitChar";
            this.lblUnitChar.Size = new System.Drawing.Size(87, 13);
            this.lblUnitChar.TabIndex = 6;
            this.lblUnitChar.Tag = "";
            this.lblUnitChar.Text = "Equipment Type:";
            this.lblUnitChar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMovement
            // 
            this.lblMovement.AutoSize = true;
            this.lblMovement.Location = new System.Drawing.Point(15, 140);
            this.lblMovement.Name = "lblMovement";
            this.lblMovement.Size = new System.Drawing.Size(110, 13);
            this.lblMovement.TabIndex = 7;
            this.lblMovement.Text = "Movement/Transport:";
            this.lblMovement.UseMnemonic = false;
            // 
            // btnEquipCancel
            // 
            this.btnEquipCancel.Location = new System.Drawing.Point(15, 473);
            this.btnEquipCancel.Name = "btnEquipCancel";
            this.btnEquipCancel.Size = new System.Drawing.Size(80, 37);
            this.btnEquipCancel.TabIndex = 12;
            this.btnEquipCancel.Text = "Cancel";
            this.btnEquipCancel.UseVisualStyleBackColor = true;
            this.btnEquipCancel.Click += new System.EventHandler(this.btnEquipCancel_Click);
            // 
            // btnEquipLoad
            // 
            this.btnEquipLoad.Location = new System.Drawing.Point(308, 473);
            this.btnEquipLoad.Name = "btnEquipLoad";
            this.btnEquipLoad.Size = new System.Drawing.Size(80, 37);
            this.btnEquipLoad.TabIndex = 13;
            this.btnEquipLoad.Text = "Load *.eqp File";
            this.btnEquipLoad.UseVisualStyleBackColor = true;
            this.btnEquipLoad.Click += new System.EventHandler(this.btnEquipLoad_Click);
            // 
            // btnEquipAdd
            // 
            this.btnEquipAdd.Location = new System.Drawing.Point(567, 473);
            this.btnEquipAdd.Name = "btnEquipAdd";
            this.btnEquipAdd.Size = new System.Drawing.Size(80, 37);
            this.btnEquipAdd.TabIndex = 11;
            this.btnEquipAdd.Text = "Add Equipment";
            this.btnEquipAdd.UseVisualStyleBackColor = true;
            this.btnEquipAdd.Click += new System.EventHandler(this.btnEquipAdd_Click);
            // 
            // txtEqpFile
            // 
            this.txtEqpFile.Location = new System.Drawing.Point(103, 447);
            this.txtEqpFile.Name = "txtEqpFile";
            this.txtEqpFile.ReadOnly = true;
            this.txtEqpFile.Size = new System.Drawing.Size(579, 20);
            this.txtEqpFile.TabIndex = 12;
            this.txtEqpFile.TabStop = false;
            // 
            // lblEqpFile
            // 
            this.lblEqpFile.AutoSize = true;
            this.lblEqpFile.Location = new System.Drawing.Point(12, 450);
            this.lblEqpFile.Name = "lblEqpFile";
            this.lblEqpFile.Size = new System.Drawing.Size(91, 13);
            this.lblEqpFile.TabIndex = 13;
            this.lblEqpFile.Text = "Current *.eqp File:";
            // 
            // lblWeapons
            // 
            this.lblWeapons.AutoSize = true;
            this.lblWeapons.Location = new System.Drawing.Point(15, 182);
            this.lblWeapons.Name = "lblWeapons";
            this.lblWeapons.Size = new System.Drawing.Size(98, 13);
            this.lblWeapons.TabIndex = 15;
            this.lblWeapons.Text = "Weapons Systems:";
            this.lblWeapons.UseMnemonic = false;
            // 
            // cboWeapons
            // 
            this.cboWeapons.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWeapons.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboWeapons.FormattingEnabled = true;
            this.cboWeapons.Location = new System.Drawing.Point(15, 199);
            this.cboWeapons.Name = "cboWeapons";
            this.cboWeapons.Size = new System.Drawing.Size(150, 21);
            this.cboWeapons.TabIndex = 4;
            this.cboWeapons.SelectionChangeCommitted += new System.EventHandler(this.cboWeapons_SelectionChangeCommitted);
            // 
            // lblNaval
            // 
            this.lblNaval.AutoSize = true;
            this.lblNaval.Location = new System.Drawing.Point(15, 308);
            this.lblNaval.Name = "lblNaval";
            this.lblNaval.Size = new System.Drawing.Size(38, 13);
            this.lblNaval.TabIndex = 17;
            this.lblNaval.Text = "Naval:";
            this.lblNaval.UseMnemonic = false;
            // 
            // cboNaval
            // 
            this.cboNaval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNaval.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNaval.FormattingEnabled = true;
            this.cboNaval.Location = new System.Drawing.Point(15, 325);
            this.cboNaval.Name = "cboNaval";
            this.cboNaval.Size = new System.Drawing.Size(150, 21);
            this.cboNaval.TabIndex = 7;
            this.cboNaval.SelectionChangeCommitted += new System.EventHandler(this.cboNaval_SelectionChangeCommitted);
            // 
            // lblDefensive
            // 
            this.lblDefensive.AutoSize = true;
            this.lblDefensive.Location = new System.Drawing.Point(15, 224);
            this.lblDefensive.Name = "lblDefensive";
            this.lblDefensive.Size = new System.Drawing.Size(58, 13);
            this.lblDefensive.TabIndex = 19;
            this.lblDefensive.Text = "Defensive:";
            this.lblDefensive.UseMnemonic = false;
            // 
            // cboDefensive
            // 
            this.cboDefensive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDefensive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDefensive.FormattingEnabled = true;
            this.cboDefensive.Location = new System.Drawing.Point(15, 241);
            this.cboDefensive.Name = "cboDefensive";
            this.cboDefensive.Size = new System.Drawing.Size(150, 21);
            this.cboDefensive.TabIndex = 5;
            this.cboDefensive.SelectionChangeCommitted += new System.EventHandler(this.cboDefensive_SelectionChangeCommitted);
            // 
            // lblEngineering
            // 
            this.lblEngineering.AutoSize = true;
            this.lblEngineering.Location = new System.Drawing.Point(15, 266);
            this.lblEngineering.Name = "lblEngineering";
            this.lblEngineering.Size = new System.Drawing.Size(66, 13);
            this.lblEngineering.TabIndex = 21;
            this.lblEngineering.Text = "Engineering:";
            this.lblEngineering.UseMnemonic = false;
            // 
            // cboEngineering
            // 
            this.cboEngineering.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEngineering.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEngineering.FormattingEnabled = true;
            this.cboEngineering.Location = new System.Drawing.Point(15, 283);
            this.cboEngineering.Name = "cboEngineering";
            this.cboEngineering.Size = new System.Drawing.Size(150, 21);
            this.cboEngineering.TabIndex = 6;
            this.cboEngineering.SelectionChangeCommitted += new System.EventHandler(this.cboEngineering_SelectionChangeCommitted);
            // 
            // dgvEquipment
            // 
            this.dgvEquipment.AllowUserToAddRows = false;
            this.dgvEquipment.AllowUserToDeleteRows = false;
            this.dgvEquipment.AllowUserToResizeColumns = false;
            this.dgvEquipment.AllowUserToResizeRows = false;
            this.dgvEquipment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEquipment.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvEquipment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipment.Location = new System.Drawing.Point(183, 14);
            this.dgvEquipment.Name = "dgvEquipment";
            this.dgvEquipment.ReadOnly = true;
            this.dgvEquipment.RowHeadersVisible = false;
            this.dgvEquipment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEquipment.Size = new System.Drawing.Size(321, 415);
            this.dgvEquipment.StandardTab = true;
            this.dgvEquipment.TabIndex = 8;
            this.dgvEquipment.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvEquipment_ColumnHeaderMouseClick);
            this.dgvEquipment.SelectionChanged += new System.EventHandler(this.dgvEquipment_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 365);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "Equipment Count:";
            // 
            // txtEquipCount
            // 
            this.txtEquipCount.Location = new System.Drawing.Point(109, 362);
            this.txtEquipCount.Name = "txtEquipCount";
            this.txtEquipCount.Size = new System.Drawing.Size(45, 20);
            this.txtEquipCount.TabIndex = 25;
            this.txtEquipCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNumber
            // 
            this.lblNumber.AutoSize = true;
            this.lblNumber.Location = new System.Drawing.Point(515, 384);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(86, 13);
            this.lblNumber.TabIndex = 26;
            this.lblNumber.Text = "Current Quantity:";
            this.toolTip1.SetToolTip(this.lblNumber, "Set Current Quantity to 0 to enable Max Quantity");
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Location = new System.Drawing.Point(515, 411);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(72, 13);
            this.lblMax.TabIndex = 27;
            this.lblMax.Text = "Max Quantity:";
            this.toolTip1.SetToolTip(this.lblMax, "Set Current Quantity to 0 to enable Max Quantity");
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(605, 380);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(42, 20);
            this.txtNumber.TabIndex = 10;
            this.txtNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.txtNumber, "Set Current Quantity to 0 to enable Max Quantity");
            this.txtNumber.TextChanged += new System.EventHandler(this.txtNumber_TextChanged);
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(605, 407);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(41, 20);
            this.txtMax.TabIndex = 9;
            this.txtMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolTip1.SetToolTip(this.txtMax, "Set Current Quantity to 0 to enable Max Quantity");
            this.txtMax.TextChanged += new System.EventHandler(this.txtMax_TextChanged);
            // 
            // lbEquipChars
            // 
            this.lbEquipChars.BackColor = System.Drawing.SystemColors.Control;
            this.lbEquipChars.FormattingEnabled = true;
            this.lbEquipChars.Location = new System.Drawing.Point(518, 25);
            this.lbEquipChars.Name = "lbEquipChars";
            this.lbEquipChars.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbEquipChars.Size = new System.Drawing.Size(131, 134);
            this.lbEquipChars.Sorted = true;
            this.lbEquipChars.TabIndex = 30;
            this.lbEquipChars.TabStop = false;
            this.lbEquipChars.UseTabStops = false;
            // 
            // lblEquipChars
            // 
            this.lblEquipChars.AutoSize = true;
            this.lblEquipChars.Location = new System.Drawing.Point(517, 8);
            this.lblEquipChars.Name = "lblEquipChars";
            this.lblEquipChars.Size = new System.Drawing.Size(132, 13);
            this.lblEquipChars.TabIndex = 31;
            this.lblEquipChars.Text = "Equipment Characteristics:";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(520, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Equipment Values:";
            // 
            // dgvEqpValues
            // 
            this.dgvEqpValues.AllowUserToAddRows = false;
            this.dgvEqpValues.AllowUserToDeleteRows = false;
            this.dgvEqpValues.AllowUserToResizeRows = false;
            this.dgvEqpValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEqpValues.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvEqpValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEqpValues.Location = new System.Drawing.Point(520, 181);
            this.dgvEqpValues.MultiSelect = false;
            this.dgvEqpValues.Name = "dgvEqpValues";
            this.dgvEqpValues.ReadOnly = true;
            this.dgvEqpValues.RowHeadersVisible = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvEqpValues.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvEqpValues.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvEqpValues.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEqpValues.Size = new System.Drawing.Size(129, 192);
            this.dgvEqpValues.TabIndex = 34;
            this.dgvEqpValues.TabStop = false;
            // 
            // frmEquip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 513);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvEqpValues);
            this.Controls.Add(this.lblEquipChars);
            this.Controls.Add(this.lbEquipChars);
            this.Controls.Add(this.txtMax);
            this.Controls.Add(this.txtNumber);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.lblNumber);
            this.Controls.Add(this.txtEquipCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvEquipment);
            this.Controls.Add(this.lblEngineering);
            this.Controls.Add(this.cboEngineering);
            this.Controls.Add(this.lblDefensive);
            this.Controls.Add(this.cboDefensive);
            this.Controls.Add(this.lblNaval);
            this.Controls.Add(this.cboNaval);
            this.Controls.Add(this.lblWeapons);
            this.Controls.Add(this.cboWeapons);
            this.Controls.Add(this.lblEqpFile);
            this.Controls.Add(this.txtEqpFile);
            this.Controls.Add(this.btnEquipAdd);
            this.Controls.Add(this.btnEquipLoad);
            this.Controls.Add(this.btnEquipCancel);
            this.Controls.Add(this.lblMovement);
            this.Controls.Add(this.lblUnitChar);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lblCountry);
            this.Controls.Add(this.cboMovement);
            this.Controls.Add(this.cboEquipType);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.cboCountry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "frmEquip";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "persfintestSITEgroundBuckeye Partners, L.P.23-2432497PO Box 56169";
            this.Text = "Add New Equipment";
            this.Load += new System.EventHandler(this.frmEquip_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEqpValues)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCountry;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.ComboBox cboEquipType;
        private System.Windows.Forms.ComboBox cboMovement;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Label lblUnitChar;
        private System.Windows.Forms.Label lblMovement;
        private System.Windows.Forms.Button btnEquipCancel;
        private System.Windows.Forms.Button btnEquipLoad;
        private System.Windows.Forms.Button btnEquipAdd;
        private System.Windows.Forms.TextBox txtEqpFile;
        private System.Windows.Forms.Label lblEqpFile;
        private System.Windows.Forms.Label lblWeapons;
        private System.Windows.Forms.ComboBox cboWeapons;
        private System.Windows.Forms.Label lblNaval;
        private System.Windows.Forms.ComboBox cboNaval;
        private System.Windows.Forms.Label lblDefensive;
        private System.Windows.Forms.ComboBox cboDefensive;
        private System.Windows.Forms.Label lblEngineering;
        private System.Windows.Forms.ComboBox cboEngineering;
        private System.Windows.Forms.DataGridView dgvEquipment;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEquipCount;
        private System.Windows.Forms.Label lblNumber;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.ListBox lbEquipChars;
        private System.Windows.Forms.Label lblEquipChars;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvEqpValues;
    }
}