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
        /// 
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
            this.lblFormID = new System.Windows.Forms.Label();
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
            this.drUnit = new Microsoft.VisualBasic.PowerPacks.DataRepeater();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblEquipName = new System.Windows.Forms.Label();
            this.txtMax = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtEquipName = new System.Windows.Forms.TextBox();
            this.pnlForce = new System.Windows.Forms.Panel();
            this.btnForceAttribs = new System.Windows.Forms.Button();
            this.txtHdrForceSupply = new System.Windows.Forms.TextBox();
            this.txtHdrForceProf = new System.Windows.Forms.TextBox();
            this.txtHdrForceName = new System.Windows.Forms.TextBox();
            this.lblForceSupply = new System.Windows.Forms.Label();
            this.lblForceProf = new System.Windows.Forms.Label();
            this.lblForceName = new System.Windows.Forms.Label();
            this.pnlFormation = new System.Windows.Forms.Panel();
            this.txtHdrFormSupply = new System.Windows.Forms.TextBox();
            this.txtHdrFormProf = new System.Windows.Forms.TextBox();
            this.txtHdrFormName = new System.Windows.Forms.TextBox();
            this.lblHdrFormSupply = new System.Windows.Forms.Label();
            this.lblHdrFormProf = new System.Windows.Forms.Label();
            this.lblHdrFormName = new System.Windows.Forms.Label();
            this.pnlUnit = new System.Windows.Forms.Panel();
            this.txtHdrUnitSupply = new System.Windows.Forms.TextBox();
            this.txtHdrUnitProf = new System.Windows.Forms.TextBox();
            this.txtHdrUnitName = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.gbForce.SuspendLayout();
            this.ssTac.SuspendLayout();
            this.drForce.ItemTemplate.SuspendLayout();
            this.drForce.SuspendLayout();
            this.drFormation.ItemTemplate.SuspendLayout();
            this.drFormation.SuspendLayout();
            this.drUnit.ItemTemplate.SuspendLayout();
            this.drUnit.SuspendLayout();
            this.pnlForce.SuspendLayout();
            this.pnlFormation.SuspendLayout();
            this.pnlUnit.SuspendLayout();
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
            this.lblTacFile.Size = new System.Drawing.Size(69, 13);
            this.lblTacFile.TabIndex = 5;
            this.lblTacFile.Text = "Current *.tac:";
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
            this.DateTimePicker.Location = new System.Drawing.Point(222, 10);
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
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gbForce
            // 
            this.gbForce.Controls.Add(this.rbForce2);
            this.gbForce.Controls.Add(this.rbForce1);
            this.gbForce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbForce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbForce.Location = new System.Drawing.Point(13, 4);
            this.gbForce.Name = "gbForce";
            this.gbForce.Size = new System.Drawing.Size(197, 71);
            this.gbForce.TabIndex = 24;
            this.gbForce.TabStop = false;
            this.gbForce.Text = "FORCE";
            // 
            // rbForce2
            // 
            this.rbForce2.AutoSize = true;
            this.rbForce2.Cursor = System.Windows.Forms.Cursors.Default;
            this.rbForce2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbForce2.Location = new System.Drawing.Point(12, 46);
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
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // txtProf
            // 
            this.txtProf.Location = new System.Drawing.Point(111, 18);
            this.txtProf.Name = "txtProf";
            this.txtProf.Size = new System.Drawing.Size(40, 20);
            this.txtProf.TabIndex = 29;
            this.txtProf.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtProf.TextChanged += new System.EventHandler(this.txtProf_TextChanged);
            // 
            // txtSupply
            // 
            this.txtSupply.Location = new System.Drawing.Point(171, 18);
            this.txtSupply.Name = "txtSupply";
            this.txtSupply.Size = new System.Drawing.Size(40, 20);
            this.txtSupply.TabIndex = 30;
            this.txtSupply.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSupply.TextChanged += new System.EventHandler(this.txtSupply_TextChanged);
            // 
            // drForce
            // 
            // 
            // drForce.ItemTemplate
            // 
            this.drForce.ItemTemplate.Controls.Add(this.lblFormID);
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
            this.drForce.Size = new System.Drawing.Size(951, 511);
            this.drForce.TabIndex = 34;
            this.drForce.Text = "drForce";
            // 
            // lblFormID
            // 
            this.lblFormID.AutoSize = true;
            this.lblFormID.Location = new System.Drawing.Point(884, 0);
            this.lblFormID.Name = "lblFormID";
            this.lblFormID.Size = new System.Drawing.Size(41, 13);
            this.lblFormID.TabIndex = 43;
            this.lblFormID.Text = "label16";
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
            this.lblLossTol.Location = new System.Drawing.Point(454, 3);
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
            this.lblProf.Size = new System.Drawing.Size(29, 13);
            this.lblProf.TabIndex = 35;
            this.lblProf.Text = "Prof:";
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
            this.drFormation.ItemTemplate.Size = new System.Drawing.Size(943, 54);
            this.drFormation.Location = new System.Drawing.Point(216, 285);
            this.drFormation.Name = "drFormation";
            this.drFormation.Size = new System.Drawing.Size(951, 68);
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
            // drUnit
            // 
            // 
            // drUnit.ItemTemplate
            // 
            this.drUnit.ItemTemplate.Controls.Add(this.label15);
            this.drUnit.ItemTemplate.Controls.Add(this.label14);
            this.drUnit.ItemTemplate.Controls.Add(this.lblEquipName);
            this.drUnit.ItemTemplate.Controls.Add(this.txtMax);
            this.drUnit.ItemTemplate.Controls.Add(this.txtQty);
            this.drUnit.ItemTemplate.Controls.Add(this.txtEquipName);
            this.drUnit.ItemTemplate.Size = new System.Drawing.Size(943, 45);
            this.drUnit.Location = new System.Drawing.Point(216, 381);
            this.drUnit.Name = "drUnit";
            this.drUnit.Size = new System.Drawing.Size(951, 64);
            this.drUnit.TabIndex = 55;
            this.drUnit.Text = "dataRepeater1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(252, 1);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(30, 13);
            this.label15.TabIndex = 60;
            this.label15.Text = "Max:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(200, 1);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(26, 13);
            this.label14.TabIndex = 59;
            this.label14.Text = "Qty:";
            // 
            // lblEquipName
            // 
            this.lblEquipName.AutoSize = true;
            this.lblEquipName.Location = new System.Drawing.Point(4, 1);
            this.lblEquipName.Name = "lblEquipName";
            this.lblEquipName.Size = new System.Drawing.Size(38, 13);
            this.lblEquipName.TabIndex = 58;
            this.lblEquipName.Text = "Name:";
            // 
            // txtMax
            // 
            this.txtMax.Location = new System.Drawing.Point(248, 17);
            this.txtMax.Name = "txtMax";
            this.txtMax.Size = new System.Drawing.Size(35, 20);
            this.txtMax.TabIndex = 57;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(192, 17);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(38, 20);
            this.txtQty.TabIndex = 56;
            // 
            // txtEquipName
            // 
            this.txtEquipName.Location = new System.Drawing.Point(3, 17);
            this.txtEquipName.Name = "txtEquipName";
            this.txtEquipName.Size = new System.Drawing.Size(183, 20);
            this.txtEquipName.TabIndex = 55;
            // 
            // pnlForce
            // 
            this.pnlForce.Controls.Add(this.btnForceAttribs);
            this.pnlForce.Controls.Add(this.txtHdrForceSupply);
            this.pnlForce.Controls.Add(this.txtHdrForceProf);
            this.pnlForce.Controls.Add(this.txtHdrForceName);
            this.pnlForce.Controls.Add(this.lblForceSupply);
            this.pnlForce.Controls.Add(this.lblForceProf);
            this.pnlForce.Controls.Add(this.lblForceName);
            this.pnlForce.Location = new System.Drawing.Point(221, 33);
            this.pnlForce.Name = "pnlForce";
            this.pnlForce.Size = new System.Drawing.Size(946, 48);
            this.pnlForce.TabIndex = 56;
            this.pnlForce.Visible = false;
            // 
            // btnForceAttribs
            // 
            this.btnForceAttribs.Location = new System.Drawing.Point(240, 11);
            this.btnForceAttribs.Name = "btnForceAttribs";
            this.btnForceAttribs.Size = new System.Drawing.Size(95, 31);
            this.btnForceAttribs.TabIndex = 6;
            this.btnForceAttribs.Text = "Force Attributes";
            this.btnForceAttribs.UseVisualStyleBackColor = true;
            // 
            // txtHdrForceSupply
            // 
            this.txtHdrForceSupply.Location = new System.Drawing.Point(185, 23);
            this.txtHdrForceSupply.Name = "txtHdrForceSupply";
            this.txtHdrForceSupply.Size = new System.Drawing.Size(40, 20);
            this.txtHdrForceSupply.TabIndex = 5;
            this.txtHdrForceSupply.TextChanged += new System.EventHandler(this.txtHdrForceSupply_TextChanged);
            // 
            // txtHdrForceProf
            // 
            this.txtHdrForceProf.Location = new System.Drawing.Point(128, 23);
            this.txtHdrForceProf.Name = "txtHdrForceProf";
            this.txtHdrForceProf.Size = new System.Drawing.Size(40, 20);
            this.txtHdrForceProf.TabIndex = 4;
            this.txtHdrForceProf.TextChanged += new System.EventHandler(this.txtHdrForceProf_TextChanged);
            // 
            // txtHdrForceName
            // 
            this.txtHdrForceName.Location = new System.Drawing.Point(17, 23);
            this.txtHdrForceName.Name = "txtHdrForceName";
            this.txtHdrForceName.Size = new System.Drawing.Size(102, 20);
            this.txtHdrForceName.TabIndex = 3;
            this.txtHdrForceName.TextChanged += new System.EventHandler(this.txtHdrForceName_TextChanged);
            // 
            // lblForceSupply
            // 
            this.lblForceSupply.AutoSize = true;
            this.lblForceSupply.Location = new System.Drawing.Point(185, 7);
            this.lblForceSupply.Name = "lblForceSupply";
            this.lblForceSupply.Size = new System.Drawing.Size(42, 13);
            this.lblForceSupply.TabIndex = 2;
            this.lblForceSupply.Text = "Supply:";
            // 
            // lblForceProf
            // 
            this.lblForceProf.AutoSize = true;
            this.lblForceProf.Location = new System.Drawing.Point(129, 7);
            this.lblForceProf.Name = "lblForceProf";
            this.lblForceProf.Size = new System.Drawing.Size(29, 13);
            this.lblForceProf.TabIndex = 1;
            this.lblForceProf.Text = "Prof:";
            // 
            // lblForceName
            // 
            this.lblForceName.AutoSize = true;
            this.lblForceName.Location = new System.Drawing.Point(16, 7);
            this.lblForceName.Name = "lblForceName";
            this.lblForceName.Size = new System.Drawing.Size(38, 13);
            this.lblForceName.TabIndex = 0;
            this.lblForceName.Text = "Name:";
            // 
            // pnlFormation
            // 
            this.pnlFormation.Controls.Add(this.txtHdrFormSupply);
            this.pnlFormation.Controls.Add(this.txtHdrFormProf);
            this.pnlFormation.Controls.Add(this.txtHdrFormName);
            this.pnlFormation.Controls.Add(this.lblHdrFormSupply);
            this.pnlFormation.Controls.Add(this.lblHdrFormProf);
            this.pnlFormation.Controls.Add(this.lblHdrFormName);
            this.pnlFormation.Location = new System.Drawing.Point(216, 451);
            this.pnlFormation.Name = "pnlFormation";
            this.pnlFormation.Size = new System.Drawing.Size(946, 53);
            this.pnlFormation.TabIndex = 57;
            this.pnlFormation.Visible = false;
            // 
            // txtHdrFormSupply
            // 
            this.txtHdrFormSupply.Location = new System.Drawing.Point(179, 20);
            this.txtHdrFormSupply.Name = "txtHdrFormSupply";
            this.txtHdrFormSupply.Size = new System.Drawing.Size(40, 20);
            this.txtHdrFormSupply.TabIndex = 11;
            // 
            // txtHdrFormProf
            // 
            this.txtHdrFormProf.Location = new System.Drawing.Point(132, 20);
            this.txtHdrFormProf.Name = "txtHdrFormProf";
            this.txtHdrFormProf.Size = new System.Drawing.Size(40, 20);
            this.txtHdrFormProf.TabIndex = 10;
            // 
            // txtHdrFormName
            // 
            this.txtHdrFormName.Location = new System.Drawing.Point(23, 20);
            this.txtHdrFormName.Name = "txtHdrFormName";
            this.txtHdrFormName.Size = new System.Drawing.Size(102, 20);
            this.txtHdrFormName.TabIndex = 9;
            // 
            // lblHdrFormSupply
            // 
            this.lblHdrFormSupply.AutoSize = true;
            this.lblHdrFormSupply.Location = new System.Drawing.Point(179, 4);
            this.lblHdrFormSupply.Name = "lblHdrFormSupply";
            this.lblHdrFormSupply.Size = new System.Drawing.Size(42, 13);
            this.lblHdrFormSupply.TabIndex = 8;
            this.lblHdrFormSupply.Text = "Supply:";
            // 
            // lblHdrFormProf
            // 
            this.lblHdrFormProf.AutoSize = true;
            this.lblHdrFormProf.Location = new System.Drawing.Point(133, 4);
            this.lblHdrFormProf.Name = "lblHdrFormProf";
            this.lblHdrFormProf.Size = new System.Drawing.Size(29, 13);
            this.lblHdrFormProf.TabIndex = 7;
            this.lblHdrFormProf.Text = "Prof:";
            // 
            // lblHdrFormName
            // 
            this.lblHdrFormName.AutoSize = true;
            this.lblHdrFormName.Location = new System.Drawing.Point(22, 4);
            this.lblHdrFormName.Name = "lblHdrFormName";
            this.lblHdrFormName.Size = new System.Drawing.Size(38, 13);
            this.lblHdrFormName.TabIndex = 6;
            this.lblHdrFormName.Text = "Name:";
            // 
            // pnlUnit
            // 
            this.pnlUnit.Controls.Add(this.txtHdrUnitSupply);
            this.pnlUnit.Controls.Add(this.txtHdrUnitProf);
            this.pnlUnit.Controls.Add(this.txtHdrUnitName);
            this.pnlUnit.Controls.Add(this.label11);
            this.pnlUnit.Controls.Add(this.label12);
            this.pnlUnit.Controls.Add(this.label13);
            this.pnlUnit.Location = new System.Drawing.Point(216, 510);
            this.pnlUnit.Name = "pnlUnit";
            this.pnlUnit.Size = new System.Drawing.Size(946, 53);
            this.pnlUnit.TabIndex = 58;
            this.pnlUnit.Visible = false;
            // 
            // txtHdrUnitSupply
            // 
            this.txtHdrUnitSupply.Location = new System.Drawing.Point(177, 24);
            this.txtHdrUnitSupply.Name = "txtHdrUnitSupply";
            this.txtHdrUnitSupply.Size = new System.Drawing.Size(40, 20);
            this.txtHdrUnitSupply.TabIndex = 17;
            // 
            // txtHdrUnitProf
            // 
            this.txtHdrUnitProf.Location = new System.Drawing.Point(130, 24);
            this.txtHdrUnitProf.Name = "txtHdrUnitProf";
            this.txtHdrUnitProf.Size = new System.Drawing.Size(40, 20);
            this.txtHdrUnitProf.TabIndex = 16;
            // 
            // txtHdrUnitName
            // 
            this.txtHdrUnitName.Location = new System.Drawing.Point(21, 24);
            this.txtHdrUnitName.Name = "txtHdrUnitName";
            this.txtHdrUnitName.Size = new System.Drawing.Size(102, 20);
            this.txtHdrUnitName.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(177, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(42, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "Supply:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(131, 8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "Prof:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(20, 8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(38, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Name:";
            // 
            // frmTacFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 697);
            this.ControlBox = false;
            this.Controls.Add(this.pnlUnit);
            this.Controls.Add(this.pnlFormation);
            this.Controls.Add(this.pnlForce);
            this.Controls.Add(this.drUnit);
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
            this.drUnit.ItemTemplate.ResumeLayout(false);
            this.drUnit.ItemTemplate.PerformLayout();
            this.drUnit.ResumeLayout(false);
            this.pnlForce.ResumeLayout(false);
            this.pnlForce.PerformLayout();
            this.pnlFormation.ResumeLayout(false);
            this.pnlFormation.PerformLayout();
            this.pnlUnit.ResumeLayout(false);
            this.pnlUnit.PerformLayout();
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
        private DataRepeater drUnit;
        private System.Windows.Forms.Label lblEquipName;
        private System.Windows.Forms.TextBox txtMax;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtEquipName;
        private System.Windows.Forms.Panel pnlForce;
        private System.Windows.Forms.Label lblForceName;
        private System.Windows.Forms.Panel pnlFormation;
        private System.Windows.Forms.Panel pnlUnit;
        private System.Windows.Forms.Label lblForceSupply;
        private System.Windows.Forms.Label lblForceProf;
        private System.Windows.Forms.TextBox txtHdrForceName;
        private System.Windows.Forms.Button btnForceAttribs;
        private System.Windows.Forms.TextBox txtHdrForceSupply;
        private System.Windows.Forms.TextBox txtHdrForceProf;
        private System.Windows.Forms.TextBox txtHdrFormSupply;
        private System.Windows.Forms.TextBox txtHdrFormProf;
        private System.Windows.Forms.TextBox txtHdrFormName;
        private System.Windows.Forms.Label lblHdrFormSupply;
        private System.Windows.Forms.Label lblHdrFormProf;
        private System.Windows.Forms.Label lblHdrFormName;
        private System.Windows.Forms.TextBox txtHdrUnitSupply;
        private System.Windows.Forms.TextBox txtHdrUnitProf;
        private System.Windows.Forms.TextBox txtHdrUnitName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblFormID;
    }
}