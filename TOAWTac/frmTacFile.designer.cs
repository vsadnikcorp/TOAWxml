using Microsoft.VisualBasic.PowerPacks;

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
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTacFile = new System.Windows.Forms.Label();
            this.txtTacFile = new System.Windows.Forms.TextBox();
            this.btnLoadTacFile = new System.Windows.Forms.Button();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbForce = new System.Windows.Forms.GroupBox();
            this.rbForce2 = new System.Windows.Forms.RadioButton();
            this.rbForce1 = new System.Windows.Forms.RadioButton();
            this.ssTac = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.btnSync = new System.Windows.Forms.Button();
            this.trvUnitTree = new System.Windows.Forms.TreeView();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtProf = new System.Windows.Forms.TextBox();
            this.txtSupply = new System.Windows.Forms.TextBox();
            this.drForce = new Microsoft.VisualBasic.PowerPacks.DataRepeater();
            this.lblOrders = new System.Windows.Forms.Label();
            this.cboOrders = new System.Windows.Forms.ComboBox();
            this.lblSupport = new System.Windows.Forms.Label();
            this.cboSupport = new System.Windows.Forms.ComboBox();
            this.lblLossTol = new System.Windows.Forms.Label();
            this.cboLossTol = new System.Windows.Forms.ComboBox();
            this.lblSupply = new System.Windows.Forms.Label();
            this.lblProf = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.drFormation = new Microsoft.VisualBasic.PowerPacks.DataRepeater();
            this.label10 = new System.Windows.Forms.Label();
            this.cboReplace = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboExp = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboUnitSize = new System.Windows.Forms.ComboBox();
            this.cboUnitType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUnitReadiness = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboUnitLossTol = new System.Windows.Forms.ComboBox();
            this.cboUnitOrders = new System.Windows.Forms.ComboBox();
            this.txtUnitSupply = new System.Windows.Forms.TextBox();
            this.txtUnitProf = new System.Windows.Forms.TextBox();
            this.txtUnitName = new System.Windows.Forms.TextBox();
            this.gbForce.SuspendLayout();
            this.ssTac.SuspendLayout();
            this.drForce.ItemTemplate.SuspendLayout();
            this.drForce.SuspendLayout();
            this.drFormation.ItemTemplate.SuspendLayout();
            this.drFormation.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCreateTacFile
            // 
            this.btnCreateTacFile.Location = new System.Drawing.Point(107, 627);
            this.btnCreateTacFile.Name = "btnCreateTacFile";
            this.btnCreateTacFile.Size = new System.Drawing.Size(60, 40);
            this.btnCreateTacFile.TabIndex = 0;
            this.btnCreateTacFile.Text = "Create TacFile";
            this.btnCreateTacFile.UseVisualStyleBackColor = true;
            this.btnCreateTacFile.Click += new System.EventHandler(this.btnCreateTacFile_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(11, 627);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(78, 38);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblTacFile
            // 
            this.lblTacFile.AutoSize = true;
            this.lblTacFile.Location = new System.Drawing.Point(16, 604);
            this.lblTacFile.Name = "lblTacFile";
            this.lblTacFile.Size = new System.Drawing.Size(71, 13);
            this.lblTacFile.TabIndex = 5;
            this.lblTacFile.Text = "Current *.tam:";
            this.lblTacFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTacFile
            // 
            this.txtTacFile.Location = new System.Drawing.Point(90, 601);
            this.txtTacFile.Name = "txtTacFile";
            this.txtTacFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTacFile.Size = new System.Drawing.Size(581, 20);
            this.txtTacFile.TabIndex = 4;
            // 
            // btnLoadTacFile
            // 
            this.btnLoadTacFile.Location = new System.Drawing.Point(185, 627);
            this.btnLoadTacFile.Name = "btnLoadTacFile";
            this.btnLoadTacFile.Size = new System.Drawing.Size(60, 40);
            this.btnLoadTacFile.TabIndex = 6;
            this.btnLoadTacFile.Text = "Load \r\nTacFile";
            this.btnLoadTacFile.UseVisualStyleBackColor = true;
            this.btnLoadTacFile.Click += new System.EventHandler(this.btnLoadTacFile_Click);
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.CustomFormat = "dd MMM yyyy";
            this.DateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DateTimePicker.Location = new System.Drawing.Point(772, 12);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(102, 20);
            this.DateTimePicker.TabIndex = 7;
            this.DateTimePicker.Value = new System.DateTime(1941, 6, 22, 0, 0, 0, 0);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1084, 629);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(78, 38);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // gbForce
            // 
            this.gbForce.Controls.Add(this.rbForce2);
            this.gbForce.Controls.Add(this.rbForce1);
            this.gbForce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbForce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbForce.Location = new System.Drawing.Point(13, 13);
            this.gbForce.Name = "gbForce";
            this.gbForce.Size = new System.Drawing.Size(276, 52);
            this.gbForce.TabIndex = 24;
            this.gbForce.TabStop = false;
            this.gbForce.Text = "FORCE";
            // 
            // rbForce2
            // 
            this.rbForce2.AutoSize = true;
            this.rbForce2.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbForce2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbForce2.Location = new System.Drawing.Point(136, 23);
            this.rbForce2.Name = "rbForce2";
            this.rbForce2.Size = new System.Drawing.Size(58, 17);
            this.rbForce2.TabIndex = 22;
            this.rbForce2.Text = "Force2";
            this.rbForce2.UseVisualStyleBackColor = true;
            this.rbForce2.CheckedChanged += new System.EventHandler(this.rbForce2_CheckedChanged);
            // 
            // rbForce1
            // 
            this.rbForce1.AutoSize = true;
            this.rbForce1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbForce1.Location = new System.Drawing.Point(12, 23);
            this.rbForce1.Name = "rbForce1";
            this.rbForce1.Size = new System.Drawing.Size(58, 17);
            this.rbForce1.TabIndex = 21;
            this.rbForce1.Text = "Force1";
            this.rbForce1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.rbForce1.UseVisualStyleBackColor = true;
            this.rbForce1.CheckedChanged += new System.EventHandler(this.rbForce1_CheckedChanged);
            // 
            // ssTac
            // 
            this.ssTac.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.ssTac.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ssTac.Location = new System.Drawing.Point(0, 675);
            this.ssTac.Name = "ssTac";
            this.ssTac.Size = new System.Drawing.Size(1179, 22);
            this.ssTac.TabIndex = 26;
            this.ssTac.Text = "StatusStrip";
            this.ssTac.Visible = false;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(300, 16);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(263, 627);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(60, 40);
            this.btnSync.TabIndex = 27;
            this.btnSync.Text = "Sync \r\nTacFile";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // trvUnitTree
            // 
            this.trvUnitTree.AllowDrop = true;
            this.trvUnitTree.CheckBoxes = true;
            this.trvUnitTree.Location = new System.Drawing.Point(13, 84);
            this.trvUnitTree.Name = "trvUnitTree";
            this.trvUnitTree.Size = new System.Drawing.Size(197, 500);
            this.trvUnitTree.TabIndex = 25;
            this.trvUnitTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvUnitTree_AfterSelect);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(3, 18);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(102, 20);
            this.txtName.TabIndex = 28;
            // 
            // txtProf
            // 
            this.txtProf.Location = new System.Drawing.Point(111, 18);
            this.txtProf.Name = "txtProf";
            this.txtProf.Size = new System.Drawing.Size(51, 20);
            this.txtProf.TabIndex = 29;
            this.txtProf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtSupply
            // 
            this.txtSupply.Location = new System.Drawing.Point(171, 18);
            this.txtSupply.Name = "txtSupply";
            this.txtSupply.Size = new System.Drawing.Size(49, 20);
            this.txtSupply.TabIndex = 30;
            this.txtSupply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // drForce
            // 
            // 
            // drForce.ItemTemplate
            // 
            this.drForce.ItemTemplate.Controls.Add(this.lblOrders);
            this.drForce.ItemTemplate.Controls.Add(this.cboOrders);
            this.drForce.ItemTemplate.Controls.Add(this.lblSupport);
            this.drForce.ItemTemplate.Controls.Add(this.cboSupport);
            this.drForce.ItemTemplate.Controls.Add(this.lblLossTol);
            this.drForce.ItemTemplate.Controls.Add(this.cboLossTol);
            this.drForce.ItemTemplate.Controls.Add(this.lblSupply);
            this.drForce.ItemTemplate.Controls.Add(this.lblProf);
            this.drForce.ItemTemplate.Controls.Add(this.lblName);
            this.drForce.ItemTemplate.Controls.Add(this.txtName);
            this.drForce.ItemTemplate.Controls.Add(this.txtProf);
            this.drForce.ItemTemplate.Controls.Add(this.txtSupply);
            this.drForce.ItemTemplate.Size = new System.Drawing.Size(943, 53);
            this.drForce.Location = new System.Drawing.Point(216, 84);
            this.drForce.Name = "drForce";
            this.drForce.Size = new System.Drawing.Size(951, 500);
            this.drForce.TabIndex = 34;
            this.drForce.Text = "drForce";
            // 
            // lblOrders
            // 
            this.lblOrders.AutoSize = true;
            this.lblOrders.Location = new System.Drawing.Point(337, 3);
            this.lblOrders.Name = "lblOrders";
            this.lblOrders.Size = new System.Drawing.Size(41, 13);
            this.lblOrders.TabIndex = 42;
            this.lblOrders.Text = "Orders:";
            // 
            // cboOrders
            // 
            this.cboOrders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboOrders.FormattingEnabled = true;
            this.cboOrders.Items.AddRange(new object[] {
            "Manual",
            "Screen",
            "Delay",
            "Defend",
            "Hold",
            "Secure",
            "Static",
            "Wait",
            "Garrison",
            "Advance",
            "Attack",
            "Independent"});
            this.cboOrders.Location = new System.Drawing.Point(335, 18);
            this.cboOrders.Name = "cboOrders";
            this.cboOrders.Size = new System.Drawing.Size(101, 21);
            this.cboOrders.TabIndex = 41;
            // 
            // lblSupport
            // 
            this.lblSupport.AutoSize = true;
            this.lblSupport.Location = new System.Drawing.Point(227, 3);
            this.lblSupport.Name = "lblSupport";
            this.lblSupport.Size = new System.Drawing.Size(81, 13);
            this.lblSupport.TabIndex = 40;
            this.lblSupport.Text = "Support Scope:";
            // 
            // cboSupport
            // 
            this.cboSupport.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSupport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSupport.FormattingEnabled = true;
            this.cboSupport.Items.AddRange(new object[] {
            "Internal Support",
            "Army Support",
            "Force Support",
            "Free Support"});
            this.cboSupport.Location = new System.Drawing.Point(227, 18);
            this.cboSupport.Name = "cboSupport";
            this.cboSupport.Size = new System.Drawing.Size(102, 21);
            this.cboSupport.TabIndex = 39;
            // 
            // lblLossTol
            // 
            this.lblLossTol.AutoSize = true;
            this.lblLossTol.Location = new System.Drawing.Point(440, 3);
            this.lblLossTol.Name = "lblLossTol";
            this.lblLossTol.Size = new System.Drawing.Size(83, 13);
            this.lblLossTol.TabIndex = 38;
            this.lblLossTol.Text = "Loss Tolerance:";
            // 
            // cboLossTol
            // 
            this.cboLossTol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLossTol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLossTol.FormattingEnabled = true;
            this.cboLossTol.Items.AddRange(new object[] {
            "Minimize Losses",
            "Limit Losses",
            "Ignore Losses"});
            this.cboLossTol.Location = new System.Drawing.Point(454, 17);
            this.cboLossTol.Name = "cboLossTol";
            this.cboLossTol.Size = new System.Drawing.Size(109, 21);
            this.cboLossTol.TabIndex = 37;
            // 
            // lblSupply
            // 
            this.lblSupply.AutoSize = true;
            this.lblSupply.Location = new System.Drawing.Point(173, 3);
            this.lblSupply.Name = "lblSupply";
            this.lblSupply.Size = new System.Drawing.Size(42, 13);
            this.lblSupply.TabIndex = 36;
            this.lblSupply.Text = "Supply:";
            // 
            // lblProf
            // 
            this.lblProf.AutoSize = true;
            this.lblProf.Location = new System.Drawing.Point(111, 3);
            this.lblProf.Name = "lblProf";
            this.lblProf.Size = new System.Drawing.Size(62, 13);
            this.lblProf.TabIndex = 35;
            this.lblProf.Text = "Proficiency:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(4, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 34;
            this.lblName.Text = "Name:";
            // 
            // drFormation
            // 
            // 
            // drFormation.ItemTemplate
            // 
            this.drFormation.ItemTemplate.Controls.Add(this.label10);
            this.drFormation.ItemTemplate.Controls.Add(this.cboReplace);
            this.drFormation.ItemTemplate.Controls.Add(this.label9);
            this.drFormation.ItemTemplate.Controls.Add(this.cboExp);
            this.drFormation.ItemTemplate.Controls.Add(this.label8);
            this.drFormation.ItemTemplate.Controls.Add(this.label7);
            this.drFormation.ItemTemplate.Controls.Add(this.cboUnitSize);
            this.drFormation.ItemTemplate.Controls.Add(this.cboUnitType);
            this.drFormation.ItemTemplate.Controls.Add(this.label6);
            this.drFormation.ItemTemplate.Controls.Add(this.txtUnitReadiness);
            this.drFormation.ItemTemplate.Controls.Add(this.label5);
            this.drFormation.ItemTemplate.Controls.Add(this.label4);
            this.drFormation.ItemTemplate.Controls.Add(this.label3);
            this.drFormation.ItemTemplate.Controls.Add(this.label2);
            this.drFormation.ItemTemplate.Controls.Add(this.label1);
            this.drFormation.ItemTemplate.Controls.Add(this.cboUnitLossTol);
            this.drFormation.ItemTemplate.Controls.Add(this.cboUnitOrders);
            this.drFormation.ItemTemplate.Controls.Add(this.txtUnitSupply);
            this.drFormation.ItemTemplate.Controls.Add(this.txtUnitProf);
            this.drFormation.ItemTemplate.Controls.Add(this.txtUnitName);
            this.drFormation.ItemTemplate.Size = new System.Drawing.Size(943, 46);
            this.drFormation.Location = new System.Drawing.Point(216, 285);
            this.drFormation.Name = "drFormation";
            this.drFormation.Size = new System.Drawing.Size(951, 296);
            this.drFormation.TabIndex = 43;
            this.drFormation.Text = "dataRepeater2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(724, 3);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 13);
            this.label10.TabIndex = 53;
            this.label10.Text = "Replacement Priority:";
            // 
            // cboReplace
            // 
            this.cboReplace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReplace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboReplace.FormattingEnabled = true;
            this.cboReplace.Location = new System.Drawing.Point(727, 20);
            this.cboReplace.Name = "cboReplace";
            this.cboReplace.Size = new System.Drawing.Size(101, 21);
            this.cboReplace.TabIndex = 52;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(635, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 13);
            this.label9.TabIndex = 51;
            this.label9.Text = "Experience:";
            // 
            // cboExp
            // 
            this.cboExp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboExp.FormattingEnabled = true;
            this.cboExp.Items.AddRange(new object[] {
            "untried",
            "veteran"});
            this.cboExp.Location = new System.Drawing.Point(637, 20);
            this.cboExp.Name = "cboExp";
            this.cboExp.Size = new System.Drawing.Size(83, 21);
            this.cboExp.TabIndex = 50;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(213, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Size:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(98, 3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "Type:";
            // 
            // cboUnitSize
            // 
            this.cboUnitSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnitSize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboUnitSize.FormattingEnabled = true;
            this.cboUnitSize.Items.AddRange(new object[] {
            "Section",
            "Platoon",
            "Company",
            "Battalion",
            "Regiment",
            "Brigade",
            "Division",
            "Corps",
            "Army",
            "Army Group",
            "Theater",
            "Supreme Command"});
            this.cboUnitSize.Location = new System.Drawing.Point(212, 20);
            this.cboUnitSize.Name = "cboUnitSize";
            this.cboUnitSize.Size = new System.Drawing.Size(74, 21);
            this.cboUnitSize.TabIndex = 47;
            // 
            // cboUnitType
            // 
            this.cboUnitType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnitType.DropDownWidth = 150;
            this.cboUnitType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboUnitType.FormattingEnabled = true;
            this.cboUnitType.Items.AddRange(new object[] {
            "Air",
            "AA",
            "AA (Airmob)",
            "AA (Mot)",
            "AA (Para)",
            "Airmobile",
            "Amphibious",
            "Antitank [v1]",
            "Antitank [v2]",
            "Antitank (Armored)",
            "Antitank (Airmob)",
            "Antitank (Glider)",
            "Antitank (Heavy)",
            "Antitank (Mot) [v1]",
            "Antitank (Mot) [v2]",
            "Antitank (Para)",
            "Armor",
            "Armor (Amphib)",
            "Armor (Asslt Gun)",
            "Armor (Glider)",
            "Armor (Heavy)",
            "Armored Train",
            "Artillery",
            "Artillery (Abn)",
            "Artillery (Airmob)",
            "Artillery (Armored)",
            "Artillery (Arm, Hvy)",
            "Artillery (Chem)",
            "Artillery (Coast) [icon]",
            "Artillery (Coast) [silh]",
            "Artillery (Fixed)",
            "Artillery (Glider)",
            "Artillery (Heavy)",
            "Artillery (Horse)",
            "Artillery (Infantry)",
            "Artillery (Missile)",
            "Artillery (Mot)",
            "Artillery (Rail)",
            "Artillery (Rocket)",
            "Artillery (Rocket, Mot)",
            "Bicycle",
            "Bomber (Heavy) [icon]",
            "Bomber (Heavy) [silh]",
            "Bomber (Jet)",
            "Bomber (Jet, Heavy)",
            "Bomber (Light) [icon]",
            "Bomber (Light) [silh]",
            "Bomber (Medium)",
            "Bomber (Naval)",
            "Border",
            "Cavalry",
            "Cavalry (Airmob)",
            "Cavalry (Armored)",
            "Cavalry (Mot)",
            "Cavalry (Mtn)",
            "Civilian",
            "Embarked Air",
            "Embarked Heli",
            "Embarked Naval",
            "Embarked Rail",
            "Engineer",
            "Engineer (Abn)",
            "Engineer (Airmob)",
            "Engineer (Armored)",
            "Engineer (Ferry)",
            "Engineer (Mot)",
            "Fighter [icon]",
            "Fighter [silh]",
            "Fighter (Jet)",
            "Fighter (Naval)",
            "Fighter Bomber [icon]",
            "Fighter Bomber [silh]",
            "Garrison",
            "Guerilla",
            "Headquarters [v1]",
            "Headquarters [v2]",
            "Heavy Wpns (Airmob)",
            "Heavy Wpns (Mtn Cav)",
            "Heavy Wpns (Glider)",
            "Heavy Wpns (Infantry)",
            "Heavy Wpns (Mot)",
            "Heavy Wpns (Mtn)",
            "Heavy Wpns (Para)",
            "Helicopter (Attack)",
            "Helicopter (Recon)",
            "Helicopter (Transport)",
            "Infantry",
            "Infantry (Airmob)",
            "Infantry (Glider)",
            "Infantry (Marine)",
            "Infantry (Mech)",
            "Infantry (Mot)",
            "Infantry (Mtn)",
            "Infantry (Para)",
            "Irregular",
            "Machine Gun",
            "Machine Gun (Mot)",
            "Military Police",
            "Mortar",
            "Mortar (Heavy)",
            "Naval (Carrier)",
            "Naval (Heavy)",
            "Naval (Light)",
            "Naval (Medium)",
            "Naval (Riverine)",
            "Naval (Task Force)",
            "Naval Attack Aircraft",
            "Parachute",
            "Railroad Repair",
            "Recon (Airborne)",
            "Recon (Armored)",
            "Recon (Glider)",
            "Reserve",
            "Security",
            "Ski",
            "Special Forces",
            "Supply",
            "Transport [icon]",
            "Transport [silh]",
            "Transport (Amphib)",
            "Task Force",
            "Battle Group",
            "Kampfgruppe",
            "Combat Command A",
            "Combat Command B",
            "Combat Command C",
            "Combat Command R"});
            this.cboUnitType.Location = new System.Drawing.Point(98, 20);
            this.cboUnitType.MaxDropDownItems = 50;
            this.cboUnitType.Name = "cboUnitType";
            this.cboUnitType.Size = new System.Drawing.Size(108, 21);
            this.cboUnitType.TabIndex = 46;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(369, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 45;
            this.label6.Text = "Read:";
            // 
            // txtUnitReadiness
            // 
            this.txtUnitReadiness.Location = new System.Drawing.Point(370, 20);
            this.txtUnitReadiness.Name = "txtUnitReadiness";
            this.txtUnitReadiness.Size = new System.Drawing.Size(37, 20);
            this.txtUnitReadiness.TabIndex = 44;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(521, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 43;
            this.label5.Text = "Loss Tolerance:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(414, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 43;
            this.label4.Text = "Orders:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(326, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 43;
            this.label3.Text = "Supply:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 43;
            this.label2.Text = "Prof:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Name:";
            // 
            // cboUnitLossTol
            // 
            this.cboUnitLossTol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnitLossTol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboUnitLossTol.FormattingEnabled = true;
            this.cboUnitLossTol.Items.AddRange(new object[] {
            "Minimize Losses",
            "Limit Losses",
            "Ignore Losses"});
            this.cboUnitLossTol.Location = new System.Drawing.Point(521, 20);
            this.cboUnitLossTol.Name = "cboUnitLossTol";
            this.cboUnitLossTol.Size = new System.Drawing.Size(110, 21);
            this.cboUnitLossTol.TabIndex = 4;
            // 
            // cboUnitOrders
            // 
            this.cboUnitOrders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUnitOrders.DropDownWidth = 110;
            this.cboUnitOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboUnitOrders.FormattingEnabled = true;
            this.cboUnitOrders.Items.AddRange(new object[] {
            "Reinforce (Turn)",
            "Reinforce (Event)",
            "Defend/Dig In",
            "Entrenched",
            "Fortified",
            "Tactical Reserve",
            "Local Reserve",
            "Mobile",
            "Moving",
            "Attacking",
            "Supporting",
            "Retreated",
            "Routed",
            "Advancing",
            "Withdrawn",
            "Exited",
            "Embarked",
            "Disbanded",
            "Tact React",
            "Local React",
            "Entrained",
            "Airborne",
            "Seaborne",
            "Divided",
            "Nuclear",
            "Airmobile",
            "Bridge Attack",
            "Airfield Attack",
            "Reorganizing",
            "Port Attack"});
            this.cboUnitOrders.Location = new System.Drawing.Point(415, 20);
            this.cboUnitOrders.MaxDropDownItems = 20;
            this.cboUnitOrders.Name = "cboUnitOrders";
            this.cboUnitOrders.Size = new System.Drawing.Size(98, 21);
            this.cboUnitOrders.TabIndex = 3;
            // 
            // txtUnitSupply
            // 
            this.txtUnitSupply.Location = new System.Drawing.Point(329, 20);
            this.txtUnitSupply.Name = "txtUnitSupply";
            this.txtUnitSupply.Size = new System.Drawing.Size(32, 20);
            this.txtUnitSupply.TabIndex = 2;
            this.txtUnitSupply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtUnitProf
            // 
            this.txtUnitProf.Location = new System.Drawing.Point(292, 20);
            this.txtUnitProf.Name = "txtUnitProf";
            this.txtUnitProf.Size = new System.Drawing.Size(32, 20);
            this.txtUnitProf.TabIndex = 1;
            this.txtUnitProf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtUnitName
            // 
            this.txtUnitName.Location = new System.Drawing.Point(3, 20);
            this.txtUnitName.Name = "txtUnitName";
            this.txtUnitName.Size = new System.Drawing.Size(87, 20);
            this.txtUnitName.TabIndex = 0;
            // 
            // frmTacFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 697);
            this.ControlBox = false;
            this.Controls.Add(this.drFormation);
            this.Controls.Add(this.drForce);
            this.Controls.Add(this.trvUnitTree);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.gbForce);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.DateTimePicker);
            this.Controls.Add(this.btnLoadTacFile);
            this.Controls.Add(this.lblTacFile);
            this.Controls.Add(this.txtTacFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnCreateTacFile);
            this.Controls.Add(this.ssTac);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmTacFile";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TacFile";
            this.Load += new System.EventHandler(this.frmTacFile_Load);
            this.gbForce.ResumeLayout(false);
            this.gbForce.PerformLayout();
            this.ssTac.ResumeLayout(false);
            this.ssTac.PerformLayout();
            this.drForce.ItemTemplate.ResumeLayout(false);
            this.drForce.ItemTemplate.PerformLayout();
            this.drForce.ResumeLayout(false);
            this.drFormation.ItemTemplate.ResumeLayout(false);
            this.drFormation.ItemTemplate.PerformLayout();
            this.drFormation.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateTacFile;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblTacFile;
        private System.Windows.Forms.TextBox txtTacFile;
        private System.Windows.Forms.Button btnLoadTacFile;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbForce;
        private System.Windows.Forms.RadioButton rbForce2;
        private System.Windows.Forms.RadioButton rbForce1;
        private System.Windows.Forms.StatusStrip ssTac;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.TreeView trvUnitTree;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtProf;
        private System.Windows.Forms.TextBox txtSupply;
        private DataRepeater drForce;
        private System.Windows.Forms.Label lblSupply;
        private System.Windows.Forms.Label lblProf;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblOrders;
        private System.Windows.Forms.ComboBox cboOrders;
        private System.Windows.Forms.Label lblSupport;
        private System.Windows.Forms.ComboBox cboSupport;
        private System.Windows.Forms.Label lblLossTol;
        private System.Windows.Forms.ComboBox cboLossTol;
        private DataRepeater drFormation;
        private System.Windows.Forms.TextBox txtUnitName;
        private System.Windows.Forms.TextBox txtUnitSupply;
        private System.Windows.Forms.TextBox txtUnitProf;
        private System.Windows.Forms.ComboBox cboUnitLossTol;
        private System.Windows.Forms.ComboBox cboUnitOrders;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboReplace;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboExp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboUnitSize;
        private System.Windows.Forms.ComboBox cboUnitType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUnitReadiness;
    }
}