namespace TOAWXML
{
    partial class frmEvents
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
            this.cboTrigger = new System.Windows.Forms.ComboBox();
            this.cboEffect = new System.Windows.Forms.ComboBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtTriggerValue = new System.Windows.Forms.TextBox();
            this.nudTurnRange = new System.Windows.Forms.NumericUpDown();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtY = new System.Windows.Forms.TextBox();
            this.nupRadius = new System.Windows.Forms.NumericUpDown();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtNews = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.trvEvents = new System.Windows.Forms.TreeView();
            this.cmsEvent = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEventToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTrigger = new System.Windows.Forms.Label();
            this.lblEffect = new System.Windows.Forms.Label();
            this.lblTriggerValue = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblChance = new System.Windows.Forms.Label();
            this.lblTurnRange = new System.Windows.Forms.Label();
            this.lblDelay = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.nudProb = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.txtCausalChain = new System.Windows.Forms.TextBox();
            this.cboFiltCausal = new System.Windows.Forms.ComboBox();
            this.cboFiltEffect = new System.Windows.Forms.ComboBox();
            this.cboFiltTrigger = new System.Windows.Forms.ComboBox();
            this.gbLocation = new System.Windows.Forms.GroupBox();
            this.gbTiming = new System.Windows.Forms.GroupBox();
            this.gbEffect = new System.Windows.Forms.GroupBox();
            this.btnUnitEffect = new System.Windows.Forms.Button();
            this.cboBias = new System.Windows.Forms.ComboBox();
            this.gbTrigger = new System.Windows.Forms.GroupBox();
            this.lblTriggerDate = new System.Windows.Forms.Label();
            this.btnUnitTrigger = new System.Windows.Forms.Button();
            this.lbTriggers = new System.Windows.Forms.ListBox();
            this.lbTriggeredBy = new System.Windows.Forms.ListBox();
            this.gbCausal = new System.Windows.Forms.GroupBox();
            this.gbVisibility = new System.Windows.Forms.GroupBox();
            this.rbCollapse = new System.Windows.Forms.RadioButton();
            this.rbExpand = new System.Windows.Forms.RadioButton();
            this.gbFormation = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboEmphasis = new System.Windows.Forms.ComboBox();
            this.cboOrders = new System.Windows.Forms.ComboBox();
            this.ssEvents = new System.Windows.Forms.StatusStrip();
            this.ssEventsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ssEventsProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.epEvents = new System.Windows.Forms.ErrorProvider(this.components);
            this.tssLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nudTurnRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupRadius)).BeginInit();
            this.cmsEvent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudProb)).BeginInit();
            this.gbFilter.SuspendLayout();
            this.gbLocation.SuspendLayout();
            this.gbTiming.SuspendLayout();
            this.gbEffect.SuspendLayout();
            this.gbTrigger.SuspendLayout();
            this.gbCausal.SuspendLayout();
            this.gbVisibility.SuspendLayout();
            this.gbFormation.SuspendLayout();
            this.ssEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // cboTrigger
            // 
            this.cboTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTrigger.FormattingEnabled = true;
            this.cboTrigger.Items.AddRange(new object[] {
            "No trigger",
            "1 attacks",
            "2 attacks",
            "1 occupies",
            "2 occupies",
            "1 uses chemical",
            "2 uses chemical",
            "1 uses nuclear",
            "2 uses nuclear",
            "Event activated",
            "Event cancelled",
            "Force 1 winning",
            "Force 2 winning",
            "Turn",
            "Unit destroyed",
            "Variable value"});
            this.cboTrigger.Location = new System.Drawing.Point(47, 26);
            this.cboTrigger.Name = "cboTrigger";
            this.cboTrigger.Size = new System.Drawing.Size(151, 21);
            this.cboTrigger.TabIndex = 3;
            this.cboTrigger.SelectionChangeCommitted += new System.EventHandler(this.cboTrigger_SelectionChangeCommitted);
            // 
            // cboEffect
            // 
            this.cboEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEffect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboEffect.FormattingEnabled = true;
            this.cboEffect.Location = new System.Drawing.Point(43, 19);
            this.cboEffect.MaxDropDownItems = 35;
            this.cboEffect.Name = "cboEffect";
            this.cboEffect.Size = new System.Drawing.Size(155, 21);
            this.cboEffect.TabIndex = 4;
            this.cboEffect.SelectionChangeCommitted += new System.EventHandler(this.cboEffect_SelectionChangeCommitted);
            // 
            // txtID
            // 
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(328, 85);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(37, 20);
            this.txtID.TabIndex = 3;
            // 
            // txtTriggerValue
            // 
            this.txtTriggerValue.Location = new System.Drawing.Point(240, 27);
            this.txtTriggerValue.Name = "txtTriggerValue";
            this.txtTriggerValue.Size = new System.Drawing.Size(30, 20);
            this.txtTriggerValue.TabIndex = 5;
            this.txtTriggerValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTriggerValue.TextChanged += new System.EventHandler(this.txtTriggerValue_TextChanged);
            // 
            // nudTurnRange
            // 
            this.nudTurnRange.Location = new System.Drawing.Point(157, 23);
            this.nudTurnRange.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTurnRange.Name = "nudTurnRange";
            this.nudTurnRange.Size = new System.Drawing.Size(40, 20);
            this.nudTurnRange.TabIndex = 6;
            this.nudTurnRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudTurnRange.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudDelay
            // 
            this.nudDelay.Location = new System.Drawing.Point(241, 24);
            this.nudDelay.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size(40, 20);
            this.nudDelay.TabIndex = 8;
            this.nudDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(42, 19);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(32, 20);
            this.txtX.TabIndex = 9;
            this.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtY
            // 
            this.txtY.Location = new System.Drawing.Point(142, 20);
            this.txtY.Name = "txtY";
            this.txtY.Size = new System.Drawing.Size(33, 20);
            this.txtY.TabIndex = 10;
            this.txtY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nupRadius
            // 
            this.nupRadius.Location = new System.Drawing.Point(248, 20);
            this.nupRadius.Name = "nupRadius";
            this.nupRadius.Size = new System.Drawing.Size(34, 20);
            this.nupRadius.TabIndex = 11;
            this.nupRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(257, 18);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(30, 20);
            this.txtValue.TabIndex = 12;
            this.txtValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNews
            // 
            this.txtNews.Location = new System.Drawing.Point(292, 406);
            this.txtNews.Multiline = true;
            this.txtNews.Name = "txtNews";
            this.txtNews.Size = new System.Drawing.Size(339, 45);
            this.txtNews.TabIndex = 13;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancel.Location = new System.Drawing.Point(12, 580);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(545, 580);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 30);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // trvEvents
            // 
            this.trvEvents.ContextMenuStrip = this.cmsEvent;
            this.trvEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.trvEvents.HideSelection = false;
            this.trvEvents.Location = new System.Drawing.Point(12, 76);
            this.trvEvents.Name = "trvEvents";
            this.trvEvents.Size = new System.Drawing.Size(266, 447);
            this.trvEvents.TabIndex = 16;
            this.trvEvents.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvEvents_AfterSelect);
            // 
            // cmsEvent
            // 
            this.cmsEvent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEventToolStripMenuItem,
            this.addEventToolStripMenuItem});
            this.cmsEvent.Name = "cmsEvent";
            this.cmsEvent.Size = new System.Drawing.Size(160, 48);
            // 
            // deleteEventToolStripMenuItem
            // 
            this.deleteEventToolStripMenuItem.Name = "deleteEventToolStripMenuItem";
            this.deleteEventToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.deleteEventToolStripMenuItem.Text = "Neutralize Event";
            this.deleteEventToolStripMenuItem.Click += new System.EventHandler(this.deleteEventToolStripMenuItem_Click);
            // 
            // addEventToolStripMenuItem
            // 
            this.addEventToolStripMenuItem.Name = "addEventToolStripMenuItem";
            this.addEventToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.addEventToolStripMenuItem.Text = "Add Event";
            this.addEventToolStripMenuItem.Click += new System.EventHandler(this.addEventToolStripMenuItem_Click);
            // 
            // lblTrigger
            // 
            this.lblTrigger.AutoSize = true;
            this.lblTrigger.Location = new System.Drawing.Point(5, 29);
            this.lblTrigger.Name = "lblTrigger";
            this.lblTrigger.Size = new System.Drawing.Size(43, 13);
            this.lblTrigger.TabIndex = 18;
            this.lblTrigger.Text = "Trigger:";
            // 
            // lblEffect
            // 
            this.lblEffect.AutoSize = true;
            this.lblEffect.Location = new System.Drawing.Point(5, 22);
            this.lblEffect.Name = "lblEffect";
            this.lblEffect.Size = new System.Drawing.Size(38, 13);
            this.lblEffect.TabIndex = 20;
            this.lblEffect.Text = "Effect:";
            // 
            // lblTriggerValue
            // 
            this.lblTriggerValue.Location = new System.Drawing.Point(201, 29);
            this.lblTriggerValue.Name = "lblTriggerValue";
            this.lblTriggerValue.Size = new System.Drawing.Size(38, 14);
            this.lblTriggerValue.TabIndex = 21;
            this.lblTriggerValue.Text = "Value:";
            this.lblTriggerValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTriggerValue.MouseLeave += new System.EventHandler(this.lblTriggerValue_MouseLeave);
            this.lblTriggerValue.MouseHover += new System.EventHandler(this.lblTriggerValue_MouseHover);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(301, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "ID:";
            // 
            // lblChance
            // 
            this.lblChance.AutoSize = true;
            this.lblChance.Location = new System.Drawing.Point(3, 28);
            this.lblChance.Name = "lblChance";
            this.lblChance.Size = new System.Drawing.Size(43, 13);
            this.lblChance.TabIndex = 23;
            this.lblChance.Text = "% Prob:";
            this.lblChance.MouseLeave += new System.EventHandler(this.lblChance_MouseLeave);
            this.lblChance.MouseHover += new System.EventHandler(this.lblChance_MouseHover);
            // 
            // lblTurnRange
            // 
            this.lblTurnRange.AutoSize = true;
            this.lblTurnRange.Location = new System.Drawing.Point(111, 18);
            this.lblTurnRange.Name = "lblTurnRange";
            this.lblTurnRange.Size = new System.Drawing.Size(42, 26);
            this.lblTurnRange.TabIndex = 24;
            this.lblTurnRange.Text = "Turn \r\nRange:";
            this.lblTurnRange.MouseLeave += new System.EventHandler(this.lblTurnRange_MouseLeave);
            this.lblTurnRange.MouseHover += new System.EventHandler(this.lblTurnRange_MouseHover);
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(205, 26);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(37, 13);
            this.lblDelay.TabIndex = 25;
            this.lblDelay.Text = "Delay:";
            this.lblDelay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblDelay.MouseLeave += new System.EventHandler(this.lblDelay_MouseLeave);
            this.lblDelay.MouseHover += new System.EventHandler(this.lblDelay_MouseHover);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "X:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(119, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Y:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(201, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Radius:";
            // 
            // lblValue
            // 
            this.lblValue.Location = new System.Drawing.Point(218, 22);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(40, 13);
            this.lblValue.TabIndex = 29;
            this.lblValue.Text = "Value:";
            this.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblValue.MouseLeave += new System.EventHandler(this.lblValue_MouseLeave);
            this.lblValue.MouseHover += new System.EventHandler(this.lblValue_MouseHover);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(300, 399);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(37, 13);
            this.label12.TabIndex = 14;
            this.label12.Text = "News:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nudProb
            // 
            this.nudProb.Location = new System.Drawing.Point(51, 25);
            this.nudProb.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudProb.Name = "nudProb";
            this.nudProb.Size = new System.Drawing.Size(42, 20);
            this.nudProb.TabIndex = 30;
            this.nudProb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudProb.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(102, 13);
            this.label13.TabIndex = 31;
            this.label13.Text = "This Event Triggers:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 58);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 13);
            this.label14.TabIndex = 32;
            this.label14.Text = "This Event Triggered By:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(65, 9);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(43, 13);
            this.label16.TabIndex = 34;
            this.label16.Text = "Trigger:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(228, 9);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(38, 13);
            this.label17.TabIndex = 35;
            this.label17.Text = "Effect:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(330, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 13);
            this.label18.TabIndex = 36;
            this.label18.Text = "Causal Chain:";
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.txtCausalChain);
            this.gbFilter.Controls.Add(this.cboFiltCausal);
            this.gbFilter.Controls.Add(this.cboFiltEffect);
            this.gbFilter.Controls.Add(this.cboFiltTrigger);
            this.gbFilter.Controls.Add(this.label18);
            this.gbFilter.Controls.Add(this.label17);
            this.gbFilter.Controls.Add(this.label16);
            this.gbFilter.Location = new System.Drawing.Point(13, 12);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(622, 58);
            this.gbFilter.TabIndex = 37;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter By:";
            // 
            // txtCausalChain
            // 
            this.txtCausalChain.Location = new System.Drawing.Point(483, 25);
            this.txtCausalChain.Name = "txtCausalChain";
            this.txtCausalChain.Size = new System.Drawing.Size(35, 20);
            this.txtCausalChain.TabIndex = 40;
            // 
            // cboFiltCausal
            // 
            this.cboFiltCausal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltCausal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFiltCausal.FormattingEnabled = true;
            this.cboFiltCausal.Items.AddRange(new object[] {
            "--NONE--",
            "This Event Triggers",
            "This Event Triggered By"});
            this.cboFiltCausal.Location = new System.Drawing.Point(325, 24);
            this.cboFiltCausal.MaxDropDownItems = 2;
            this.cboFiltCausal.Name = "cboFiltCausal";
            this.cboFiltCausal.Size = new System.Drawing.Size(153, 21);
            this.cboFiltCausal.TabIndex = 39;
            // 
            // cboFiltEffect
            // 
            this.cboFiltEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltEffect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFiltEffect.FormattingEnabled = true;
            this.cboFiltEffect.Items.AddRange(new object[] {
            "--ALL--",
            "No effect",
            "Activate event",
            "Air Shock 1",
            "Air Shock 2",
            "Air transport 1",
            "Air transport 2",
            "Cancel event",
            "Cease Fire",
            "Cool front",
            "Disband unit",
            "Enable event",
            "End normal",
            "End victory 1",
            "End victory 2",
            "Force 1 track",
            "Force 2 track",
            "Form\'n orders",
            "Guerrillas 1",
            "Guerrillas 2",
            "Move Bias 1",
            "Move Bias 2",
            "News only",
            "Nuclear Attack",
            "Nuclear OK 1",
            "Nuclear OK 2",
            "Open Fire",
            "Pestilence 1",
            "Pestilence 2",
            "PO 1 Activate",
            "PO 2 Activate",
            "Rail damage 1",
            "Rail damage 2",
            "Rail repair 1",
            "Rail repair 2",
            "Rail transport 1",
            "Rail transport 2",
            "Refugees 1",
            "Refugees 2",
            "Remove zone 1",
            "Remove zone 2",
            "Replacements 1*",
            "Replacements 2*",
            "Sea transport 1",
            "Sea transport 2",
            "Set ownership 1",
            "Set ownership 2",
            "Shock 1",
            "Shock 2",
            "Storms",
            "Strategic bias 1",
            "Strategic bias 2",
            "Supply 1+",
            "Supply 2+",
            "Supply 1-",
            "Supply 2-",
            "Supply Point 1",
            "Supply Point 2",
            "Supply radius 1",
            "Supply radius 2",
            "Theater Option 1",
            "Theater Option 2",
            "Theater recon 1",
            "Theater recon 2",
            "Use chemicals 1",
            "Use chemicals 2",
            "Variable +",
            "Variable -",
            "Victory 1+",
            "Victory 2+",
            "Warm front",
            "Withdraw army",
            "Withdraw unit",
            "ZOC Cost 1",
            "ZOC Cost 2"});
            this.cboFiltEffect.Location = new System.Drawing.Point(185, 25);
            this.cboFiltEffect.MaxDropDownItems = 35;
            this.cboFiltEffect.Name = "cboFiltEffect";
            this.cboFiltEffect.Size = new System.Drawing.Size(127, 21);
            this.cboFiltEffect.TabIndex = 38;
            this.cboFiltEffect.SelectionChangeCommitted += new System.EventHandler(this.cboFiltEffect_SelectionChangeCommitted);
            // 
            // cboFiltTrigger
            // 
            this.cboFiltTrigger.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltTrigger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFiltTrigger.FormattingEnabled = true;
            this.cboFiltTrigger.Items.AddRange(new object[] {
            "--ALL--",
            "No trigger",
            "1 attacks",
            "2 attacks",
            "1 occupies",
            "2 occupies",
            "1 uses chemical",
            "2 uses chemical",
            "1 uses nuclear",
            "2 uses nuclear",
            "Event activated",
            "Event cancelled",
            "Force 1 winning",
            "Force 2 winning",
            "Turn",
            "Unit destroyed",
            "Variable value"});
            this.cboFiltTrigger.Location = new System.Drawing.Point(18, 25);
            this.cboFiltTrigger.Name = "cboFiltTrigger";
            this.cboFiltTrigger.Size = new System.Drawing.Size(151, 21);
            this.cboFiltTrigger.TabIndex = 38;
            this.cboFiltTrigger.SelectionChangeCommitted += new System.EventHandler(this.cboFiltTrigger_SelectionChangeCommitted);
            // 
            // gbLocation
            // 
            this.gbLocation.Controls.Add(this.txtX);
            this.gbLocation.Controls.Add(this.txtY);
            this.gbLocation.Controls.Add(this.label8);
            this.gbLocation.Controls.Add(this.nupRadius);
            this.gbLocation.Controls.Add(this.label9);
            this.gbLocation.Controls.Add(this.label10);
            this.gbLocation.Location = new System.Drawing.Point(296, 330);
            this.gbLocation.Name = "gbLocation";
            this.gbLocation.Size = new System.Drawing.Size(339, 53);
            this.gbLocation.TabIndex = 38;
            this.gbLocation.TabStop = false;
            this.gbLocation.Text = "Location:";
            // 
            // gbTiming
            // 
            this.gbTiming.Controls.Add(this.nudDelay);
            this.gbTiming.Controls.Add(this.nudTurnRange);
            this.gbTiming.Controls.Add(this.lblChance);
            this.gbTiming.Controls.Add(this.lblTurnRange);
            this.gbTiming.Controls.Add(this.lblDelay);
            this.gbTiming.Controls.Add(this.nudProb);
            this.gbTiming.Location = new System.Drawing.Point(292, 187);
            this.gbTiming.Name = "gbTiming";
            this.gbTiming.Size = new System.Drawing.Size(339, 65);
            this.gbTiming.TabIndex = 39;
            this.gbTiming.TabStop = false;
            this.gbTiming.Text = "Timing/Probability:";
            // 
            // gbEffect
            // 
            this.gbEffect.Controls.Add(this.btnUnitEffect);
            this.gbEffect.Controls.Add(this.cboBias);
            this.gbEffect.Controls.Add(this.cboEffect);
            this.gbEffect.Controls.Add(this.lblEffect);
            this.gbEffect.Controls.Add(this.txtValue);
            this.gbEffect.Controls.Add(this.lblValue);
            this.gbEffect.Location = new System.Drawing.Point(292, 262);
            this.gbEffect.Name = "gbEffect";
            this.gbEffect.Size = new System.Drawing.Size(339, 58);
            this.gbEffect.TabIndex = 40;
            this.gbEffect.TabStop = false;
            this.gbEffect.Text = "Effect:";
            // 
            // btnUnitEffect
            // 
            this.btnUnitEffect.Location = new System.Drawing.Point(208, 18);
            this.btnUnitEffect.Name = "btnUnitEffect";
            this.btnUnitEffect.Size = new System.Drawing.Size(121, 24);
            this.btnUnitEffect.TabIndex = 23;
            this.btnUnitEffect.Text = "Select Unit";
            this.btnUnitEffect.UseVisualStyleBackColor = true;
            this.btnUnitEffect.Click += new System.EventHandler(this.btnUnitEffect_Click);
            // 
            // cboBias
            // 
            this.cboBias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboBias.FormattingEnabled = true;
            this.cboBias.Location = new System.Drawing.Point(208, 18);
            this.cboBias.Name = "cboBias";
            this.cboBias.Size = new System.Drawing.Size(121, 21);
            this.cboBias.TabIndex = 47;
            // 
            // gbTrigger
            // 
            this.gbTrigger.Controls.Add(this.lblTriggerDate);
            this.gbTrigger.Controls.Add(this.btnUnitTrigger);
            this.gbTrigger.Controls.Add(this.cboTrigger);
            this.gbTrigger.Controls.Add(this.lblTrigger);
            this.gbTrigger.Controls.Add(this.lblTriggerValue);
            this.gbTrigger.Controls.Add(this.txtTriggerValue);
            this.gbTrigger.Location = new System.Drawing.Point(292, 111);
            this.gbTrigger.Name = "gbTrigger";
            this.gbTrigger.Size = new System.Drawing.Size(339, 65);
            this.gbTrigger.TabIndex = 41;
            this.gbTrigger.TabStop = false;
            this.gbTrigger.Text = "Trigger:";
            // 
            // lblTriggerDate
            // 
            this.lblTriggerDate.AutoSize = true;
            this.lblTriggerDate.Location = new System.Drawing.Point(272, 25);
            this.lblTriggerDate.Name = "lblTriggerDate";
            this.lblTriggerDate.Size = new System.Drawing.Size(10, 13);
            this.lblTriggerDate.TabIndex = 51;
            this.lblTriggerDate.Text = " ";
            // 
            // btnUnitTrigger
            // 
            this.btnUnitTrigger.Location = new System.Drawing.Point(207, 26);
            this.btnUnitTrigger.Name = "btnUnitTrigger";
            this.btnUnitTrigger.Size = new System.Drawing.Size(122, 24);
            this.btnUnitTrigger.TabIndex = 22;
            this.btnUnitTrigger.Text = "Select Unit";
            this.btnUnitTrigger.UseVisualStyleBackColor = true;
            this.btnUnitTrigger.Click += new System.EventHandler(this.btnUnitTrigger_Click);
            // 
            // lbTriggers
            // 
            this.lbTriggers.FormattingEnabled = true;
            this.lbTriggers.Location = new System.Drawing.Point(134, 14);
            this.lbTriggers.Name = "lbTriggers";
            this.lbTriggers.Size = new System.Drawing.Size(152, 43);
            this.lbTriggers.TabIndex = 43;
            // 
            // lbTriggeredBy
            // 
            this.lbTriggeredBy.FormattingEnabled = true;
            this.lbTriggeredBy.Location = new System.Drawing.Point(134, 62);
            this.lbTriggeredBy.Name = "lbTriggeredBy";
            this.lbTriggeredBy.Size = new System.Drawing.Size(151, 43);
            this.lbTriggeredBy.TabIndex = 44;
            // 
            // gbCausal
            // 
            this.gbCausal.Controls.Add(this.lbTriggeredBy);
            this.gbCausal.Controls.Add(this.lbTriggers);
            this.gbCausal.Controls.Add(this.label14);
            this.gbCausal.Controls.Add(this.label13);
            this.gbCausal.Location = new System.Drawing.Point(292, 459);
            this.gbCausal.Name = "gbCausal";
            this.gbCausal.Size = new System.Drawing.Size(343, 110);
            this.gbCausal.TabIndex = 45;
            this.gbCausal.TabStop = false;
            this.gbCausal.Text = "Causal Chain:";
            // 
            // gbVisibility
            // 
            this.gbVisibility.Controls.Add(this.rbCollapse);
            this.gbVisibility.Controls.Add(this.rbExpand);
            this.gbVisibility.Location = new System.Drawing.Point(13, 529);
            this.gbVisibility.Name = "gbVisibility";
            this.gbVisibility.Size = new System.Drawing.Size(265, 40);
            this.gbVisibility.TabIndex = 46;
            this.gbVisibility.TabStop = false;
            this.gbVisibility.Text = "Visibility:";
            // 
            // rbCollapse
            // 
            this.rbCollapse.AutoSize = true;
            this.rbCollapse.Checked = true;
            this.rbCollapse.Location = new System.Drawing.Point(153, 16);
            this.rbCollapse.Name = "rbCollapse";
            this.rbCollapse.Size = new System.Drawing.Size(79, 17);
            this.rbCollapse.TabIndex = 1;
            this.rbCollapse.TabStop = true;
            this.rbCollapse.Text = "Collapse All";
            this.rbCollapse.UseVisualStyleBackColor = true;
            this.rbCollapse.Click += new System.EventHandler(this.rbCollapse_Click);
            // 
            // rbExpand
            // 
            this.rbExpand.AutoSize = true;
            this.rbExpand.Location = new System.Drawing.Point(18, 16);
            this.rbExpand.Name = "rbExpand";
            this.rbExpand.Size = new System.Drawing.Size(75, 17);
            this.rbExpand.TabIndex = 0;
            this.rbExpand.Text = "Expand All";
            this.rbExpand.UseVisualStyleBackColor = true;
            this.rbExpand.Click += new System.EventHandler(this.rbExpand_Click);
            // 
            // gbFormation
            // 
            this.gbFormation.Controls.Add(this.label2);
            this.gbFormation.Controls.Add(this.label1);
            this.gbFormation.Controls.Add(this.cboEmphasis);
            this.gbFormation.Controls.Add(this.cboOrders);
            this.gbFormation.Location = new System.Drawing.Point(292, 328);
            this.gbFormation.Name = "gbFormation";
            this.gbFormation.Size = new System.Drawing.Size(338, 55);
            this.gbFormation.TabIndex = 48;
            this.gbFormation.TabStop = false;
            this.gbFormation.Text = "Formation Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(159, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Emphasis:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Orders:";
            // 
            // cboEmphasis
            // 
            this.cboEmphasis.FormattingEnabled = true;
            this.cboEmphasis.Location = new System.Drawing.Point(225, 21);
            this.cboEmphasis.Name = "cboEmphasis";
            this.cboEmphasis.Size = new System.Drawing.Size(103, 21);
            this.cboEmphasis.TabIndex = 1;
            // 
            // cboOrders
            // 
            this.cboOrders.FormattingEnabled = true;
            this.cboOrders.Location = new System.Drawing.Point(54, 21);
            this.cboOrders.Name = "cboOrders";
            this.cboOrders.Size = new System.Drawing.Size(85, 21);
            this.cboOrders.TabIndex = 0;
            // 
            // ssEvents
            // 
            this.ssEvents.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ssEventsLabel,
            this.ssEventsProgress,
            this.tssLabel1});
            this.ssEvents.Location = new System.Drawing.Point(0, 617);
            this.ssEvents.Name = "ssEvents";
            this.ssEvents.Size = new System.Drawing.Size(634, 22);
            this.ssEvents.TabIndex = 50;
            this.ssEvents.Text = "statusStrip1";
            // 
            // ssEventsLabel
            // 
            this.ssEventsLabel.Name = "ssEventsLabel";
            this.ssEventsLabel.Size = new System.Drawing.Size(486, 17);
            this.ssEventsLabel.Spring = true;
            this.ssEventsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ssEventsProgress
            // 
            this.ssEventsProgress.MarqueeAnimationSpeed = 30;
            this.ssEventsProgress.Name = "ssEventsProgress";
            this.ssEventsProgress.Size = new System.Drawing.Size(100, 16);
            this.ssEventsProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // epEvents
            // 
            this.epEvents.ContainerControl = this;
            // 
            // tssLabel1
            // 
            this.tssLabel1.Name = "tssLabel1";
            this.tssLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // frmEvents
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(634, 639);
            this.ControlBox = false;
            this.Controls.Add(this.ssEvents);
            this.Controls.Add(this.gbFormation);
            this.Controls.Add(this.gbVisibility);
            this.Controls.Add(this.gbCausal);
            this.Controls.Add(this.gbTrigger);
            this.Controls.Add(this.gbEffect);
            this.Controls.Add(this.gbTiming);
            this.Controls.Add(this.gbLocation);
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtNews);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.trvEvents);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Name = "frmEvents";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Event Editor";
            this.Load += new System.EventHandler(this.frmEvents_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudTurnRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nupRadius)).EndInit();
            this.cmsEvent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudProb)).EndInit();
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            this.gbLocation.ResumeLayout(false);
            this.gbLocation.PerformLayout();
            this.gbTiming.ResumeLayout(false);
            this.gbTiming.PerformLayout();
            this.gbEffect.ResumeLayout(false);
            this.gbEffect.PerformLayout();
            this.gbTrigger.ResumeLayout(false);
            this.gbTrigger.PerformLayout();
            this.gbCausal.ResumeLayout(false);
            this.gbCausal.PerformLayout();
            this.gbVisibility.ResumeLayout(false);
            this.gbVisibility.PerformLayout();
            this.gbFormation.ResumeLayout(false);
            this.gbFormation.PerformLayout();
            this.ssEvents.ResumeLayout(false);
            this.ssEvents.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epEvents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.ComboBox cboTrigger;
        private System.Windows.Forms.ComboBox cboEffect;
        private System.Windows.Forms.TextBox txtTriggerValue;
        private System.Windows.Forms.NumericUpDown nudTurnRange;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.TextBox txtY;
        private System.Windows.Forms.NumericUpDown nupRadius;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtNews;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TreeView trvEvents;
        private System.Windows.Forms.Label lblTrigger;
        private System.Windows.Forms.Label lblEffect;
        private System.Windows.Forms.Label lblTriggerValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblChance;
        private System.Windows.Forms.Label lblTurnRange;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudProb;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox gbFilter;
        private System.Windows.Forms.TextBox txtCausalChain;
        private System.Windows.Forms.ComboBox cboFiltCausal;
        private System.Windows.Forms.ComboBox cboFiltEffect;
        private System.Windows.Forms.ComboBox cboFiltTrigger;
        private System.Windows.Forms.GroupBox gbLocation;
        private System.Windows.Forms.GroupBox gbTiming;
        private System.Windows.Forms.GroupBox gbEffect;
        private System.Windows.Forms.GroupBox gbTrigger;
        private System.Windows.Forms.ListBox lbTriggers;
        private System.Windows.Forms.ListBox lbTriggeredBy;
        private System.Windows.Forms.GroupBox gbCausal;
        private System.Windows.Forms.GroupBox gbVisibility;
        private System.Windows.Forms.RadioButton rbCollapse;
        private System.Windows.Forms.RadioButton rbExpand;
        private System.Windows.Forms.Button btnUnitTrigger;
        private System.Windows.Forms.Button btnUnitEffect;
        private System.Windows.Forms.ComboBox cboBias;
        private System.Windows.Forms.GroupBox gbFormation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboEmphasis;
        private System.Windows.Forms.ComboBox cboOrders;
        private System.Windows.Forms.ContextMenuStrip cmsEvent;
        private System.Windows.Forms.ToolStripMenuItem deleteEventToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEventToolStripMenuItem;
        private System.Windows.Forms.StatusStrip ssEvents;
        private System.Windows.Forms.ToolStripStatusLabel ssEventsLabel;
        private System.Windows.Forms.ToolStripProgressBar ssEventsProgress;
        private System.Windows.Forms.ErrorProvider epEvents;
        private System.Windows.Forms.Label lblTriggerDate;
        private System.Windows.Forms.ToolStripStatusLabel tssLabel1;
    }
}