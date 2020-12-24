namespace TOAWXML
{
    partial class frmEnviron
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnviron));
            this.gbCalendar = new System.Windows.Forms.GroupBox();
            this.txtCurrentTurn = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtLastTurn = new System.Windows.Forms.TextBox();
            this.txtStartYear = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cboStartMonth = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboStartDay = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboStartHour = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboTurnLength = new System.Windows.Forms.ComboBox();
            this.gbEnviron = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboClimateArea = new System.Windows.Forms.ComboBox();
            this.cboMapScale = new System.Windows.Forms.ComboBox();
            this.cboTemperature = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboPrecipitation = new System.Windows.Forms.ComboBox();
            this.cboWZ1Vis = new System.Windows.Forms.ComboBox();
            this.gbWZ2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtWZ2Border = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cboWZ2Vis = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cboWZ2Temp = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cboWZ2Precip = new System.Windows.Forms.ComboBox();
            this.gbWZ3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtWZ3Border = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cboWZ3Vis = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cboWZ3Temp = new System.Windows.Forms.ComboBox();
            this.cboWZ3Precip = new System.Windows.Forms.ComboBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSaveEnviron = new System.Windows.Forms.Button();
            this.btnCloseEnviron = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gbCalendar.SuspendLayout();
            this.gbEnviron.SuspendLayout();
            this.gbWZ2.SuspendLayout();
            this.gbWZ3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCalendar
            // 
            resources.ApplyResources(this.gbCalendar, "gbCalendar");
            this.gbCalendar.Controls.Add(this.txtCurrentTurn);
            this.gbCalendar.Controls.Add(this.label20);
            this.gbCalendar.Controls.Add(this.txtLastTurn);
            this.gbCalendar.Controls.Add(this.txtStartYear);
            this.gbCalendar.Controls.Add(this.label6);
            this.gbCalendar.Controls.Add(this.label5);
            this.gbCalendar.Controls.Add(this.label4);
            this.gbCalendar.Controls.Add(this.cboStartMonth);
            this.gbCalendar.Controls.Add(this.label3);
            this.gbCalendar.Controls.Add(this.cboStartDay);
            this.gbCalendar.Controls.Add(this.label2);
            this.gbCalendar.Controls.Add(this.cboStartHour);
            this.gbCalendar.Controls.Add(this.label1);
            this.gbCalendar.Controls.Add(this.cboTurnLength);
            this.errorProvider1.SetError(this.gbCalendar, resources.GetString("gbCalendar.Error"));
            this.errorProvider1.SetIconAlignment(this.gbCalendar, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gbCalendar.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gbCalendar, ((int)(resources.GetObject("gbCalendar.IconPadding"))));
            this.gbCalendar.Name = "gbCalendar";
            this.gbCalendar.TabStop = false;
            // 
            // txtCurrentTurn
            // 
            resources.ApplyResources(this.txtCurrentTurn, "txtCurrentTurn");
            this.errorProvider1.SetError(this.txtCurrentTurn, resources.GetString("txtCurrentTurn.Error"));
            this.errorProvider1.SetIconAlignment(this.txtCurrentTurn, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtCurrentTurn.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtCurrentTurn, ((int)(resources.GetObject("txtCurrentTurn.IconPadding"))));
            this.txtCurrentTurn.Name = "txtCurrentTurn";
            this.txtCurrentTurn.TextChanged += new System.EventHandler(this.txtCurrentTurn_TextChanged);
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.errorProvider1.SetError(this.label20, resources.GetString("label20.Error"));
            this.errorProvider1.SetIconAlignment(this.label20, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label20.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label20, ((int)(resources.GetObject("label20.IconPadding"))));
            this.label20.Name = "label20";
            // 
            // txtLastTurn
            // 
            resources.ApplyResources(this.txtLastTurn, "txtLastTurn");
            this.errorProvider1.SetError(this.txtLastTurn, resources.GetString("txtLastTurn.Error"));
            this.errorProvider1.SetIconAlignment(this.txtLastTurn, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtLastTurn.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtLastTurn, ((int)(resources.GetObject("txtLastTurn.IconPadding"))));
            this.txtLastTurn.Name = "txtLastTurn";
            this.txtLastTurn.TextChanged += new System.EventHandler(this.txtLastTurn_TextChanged);
            // 
            // txtStartYear
            // 
            resources.ApplyResources(this.txtStartYear, "txtStartYear");
            this.errorProvider1.SetError(this.txtStartYear, resources.GetString("txtStartYear.Error"));
            this.errorProvider1.SetIconAlignment(this.txtStartYear, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtStartYear.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtStartYear, ((int)(resources.GetObject("txtStartYear.IconPadding"))));
            this.txtStartYear.Name = "txtStartYear";
            this.txtStartYear.TextChanged += new System.EventHandler(this.txtStartYear_TextChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.errorProvider1.SetError(this.label6, resources.GetString("label6.Error"));
            this.errorProvider1.SetIconAlignment(this.label6, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label6.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label6, ((int)(resources.GetObject("label6.IconPadding"))));
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.errorProvider1.SetError(this.label5, resources.GetString("label5.Error"));
            this.errorProvider1.SetIconAlignment(this.label5, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label5.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label5, ((int)(resources.GetObject("label5.IconPadding"))));
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.errorProvider1.SetError(this.label4, resources.GetString("label4.Error"));
            this.errorProvider1.SetIconAlignment(this.label4, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label4.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label4, ((int)(resources.GetObject("label4.IconPadding"))));
            this.label4.Name = "label4";
            // 
            // cboStartMonth
            // 
            resources.ApplyResources(this.cboStartMonth, "cboStartMonth");
            this.cboStartMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboStartMonth, resources.GetString("cboStartMonth.Error"));
            this.cboStartMonth.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboStartMonth, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboStartMonth.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboStartMonth, ((int)(resources.GetObject("cboStartMonth.IconPadding"))));
            this.cboStartMonth.Name = "cboStartMonth";
            this.cboStartMonth.SelectionChangeCommitted += new System.EventHandler(this.cboStartMonth_SelectionChangeCommitted);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.errorProvider1.SetError(this.label3, resources.GetString("label3.Error"));
            this.errorProvider1.SetIconAlignment(this.label3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label3, ((int)(resources.GetObject("label3.IconPadding"))));
            this.label3.Name = "label3";
            // 
            // cboStartDay
            // 
            resources.ApplyResources(this.cboStartDay, "cboStartDay");
            this.cboStartDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboStartDay, resources.GetString("cboStartDay.Error"));
            this.cboStartDay.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboStartDay, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboStartDay.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboStartDay, ((int)(resources.GetObject("cboStartDay.IconPadding"))));
            this.cboStartDay.Name = "cboStartDay";
            this.cboStartDay.SelectionChangeCommitted += new System.EventHandler(this.cboStartDay_SelectionChangeCommitted);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.errorProvider1.SetError(this.label2, resources.GetString("label2.Error"));
            this.errorProvider1.SetIconAlignment(this.label2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label2, ((int)(resources.GetObject("label2.IconPadding"))));
            this.label2.Name = "label2";
            // 
            // cboStartHour
            // 
            resources.ApplyResources(this.cboStartHour, "cboStartHour");
            this.cboStartHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboStartHour, resources.GetString("cboStartHour.Error"));
            this.cboStartHour.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboStartHour, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboStartHour.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboStartHour, ((int)(resources.GetObject("cboStartHour.IconPadding"))));
            this.cboStartHour.Name = "cboStartHour";
            this.cboStartHour.SelectionChangeCommitted += new System.EventHandler(this.cboStartHour_SelectionChangeCommitted);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.errorProvider1.SetError(this.label1, resources.GetString("label1.Error"));
            this.errorProvider1.SetIconAlignment(this.label1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label1, ((int)(resources.GetObject("label1.IconPadding"))));
            this.label1.Name = "label1";
            // 
            // cboTurnLength
            // 
            resources.ApplyResources(this.cboTurnLength, "cboTurnLength");
            this.cboTurnLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboTurnLength, resources.GetString("cboTurnLength.Error"));
            this.cboTurnLength.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboTurnLength, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboTurnLength.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboTurnLength, ((int)(resources.GetObject("cboTurnLength.IconPadding"))));
            this.cboTurnLength.Name = "cboTurnLength";
            this.cboTurnLength.SelectionChangeCommitted += new System.EventHandler(this.cboTurnLength_SelectionChangeCommitted);
            // 
            // gbEnviron
            // 
            resources.ApplyResources(this.gbEnviron, "gbEnviron");
            this.gbEnviron.Controls.Add(this.label8);
            this.gbEnviron.Controls.Add(this.label7);
            this.gbEnviron.Controls.Add(this.cboClimateArea);
            this.gbEnviron.Controls.Add(this.cboMapScale);
            this.errorProvider1.SetError(this.gbEnviron, resources.GetString("gbEnviron.Error"));
            this.errorProvider1.SetIconAlignment(this.gbEnviron, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gbEnviron.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gbEnviron, ((int)(resources.GetObject("gbEnviron.IconPadding"))));
            this.gbEnviron.Name = "gbEnviron";
            this.gbEnviron.TabStop = false;
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.errorProvider1.SetError(this.label8, resources.GetString("label8.Error"));
            this.errorProvider1.SetIconAlignment(this.label8, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label8.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label8, ((int)(resources.GetObject("label8.IconPadding"))));
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.errorProvider1.SetError(this.label7, resources.GetString("label7.Error"));
            this.errorProvider1.SetIconAlignment(this.label7, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label7.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label7, ((int)(resources.GetObject("label7.IconPadding"))));
            this.label7.Name = "label7";
            // 
            // cboClimateArea
            // 
            resources.ApplyResources(this.cboClimateArea, "cboClimateArea");
            this.cboClimateArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboClimateArea, resources.GetString("cboClimateArea.Error"));
            this.cboClimateArea.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboClimateArea, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboClimateArea.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboClimateArea, ((int)(resources.GetObject("cboClimateArea.IconPadding"))));
            this.cboClimateArea.Name = "cboClimateArea";
            this.cboClimateArea.SelectionChangeCommitted += new System.EventHandler(this.cboClimateArea_SelectionChangeCommitted);
            // 
            // cboMapScale
            // 
            resources.ApplyResources(this.cboMapScale, "cboMapScale");
            this.cboMapScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboMapScale, resources.GetString("cboMapScale.Error"));
            this.cboMapScale.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboMapScale, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboMapScale.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboMapScale, ((int)(resources.GetObject("cboMapScale.IconPadding"))));
            this.cboMapScale.Name = "cboMapScale";
            this.cboMapScale.SelectionChangeCommitted += new System.EventHandler(this.cboMapScale_SelectionChangeCommitted);
            // 
            // cboTemperature
            // 
            resources.ApplyResources(this.cboTemperature, "cboTemperature");
            this.cboTemperature.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboTemperature, resources.GetString("cboTemperature.Error"));
            this.cboTemperature.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboTemperature, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboTemperature.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboTemperature, ((int)(resources.GetObject("cboTemperature.IconPadding"))));
            this.cboTemperature.Name = "cboTemperature";
            this.cboTemperature.SelectionChangeCommitted += new System.EventHandler(this.cboTemperature_SelectionChangeCommitted);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.errorProvider1.SetError(this.label17, resources.GetString("label17.Error"));
            this.errorProvider1.SetIconAlignment(this.label17, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label17.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label17, ((int)(resources.GetObject("label17.IconPadding"))));
            this.label17.Name = "label17";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.errorProvider1.SetError(this.label10, resources.GetString("label10.Error"));
            this.errorProvider1.SetIconAlignment(this.label10, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label10.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label10, ((int)(resources.GetObject("label10.IconPadding"))));
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.errorProvider1.SetError(this.label9, resources.GetString("label9.Error"));
            this.errorProvider1.SetIconAlignment(this.label9, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label9.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label9, ((int)(resources.GetObject("label9.IconPadding"))));
            this.label9.Name = "label9";
            // 
            // cboPrecipitation
            // 
            resources.ApplyResources(this.cboPrecipitation, "cboPrecipitation");
            this.cboPrecipitation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboPrecipitation, resources.GetString("cboPrecipitation.Error"));
            this.cboPrecipitation.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboPrecipitation, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboPrecipitation.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboPrecipitation, ((int)(resources.GetObject("cboPrecipitation.IconPadding"))));
            this.cboPrecipitation.Name = "cboPrecipitation";
            this.cboPrecipitation.SelectionChangeCommitted += new System.EventHandler(this.cboPrecipitation_SelectionChangeCommitted);
            // 
            // cboWZ1Vis
            // 
            resources.ApplyResources(this.cboWZ1Vis, "cboWZ1Vis");
            this.cboWZ1Vis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboWZ1Vis, resources.GetString("cboWZ1Vis.Error"));
            this.cboWZ1Vis.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboWZ1Vis, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboWZ1Vis.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboWZ1Vis, ((int)(resources.GetObject("cboWZ1Vis.IconPadding"))));
            this.cboWZ1Vis.Name = "cboWZ1Vis";
            this.cboWZ1Vis.SelectionChangeCommitted += new System.EventHandler(this.cboWZ1Vis_SelectionChangeCommitted);
            // 
            // gbWZ2
            // 
            resources.ApplyResources(this.gbWZ2, "gbWZ2");
            this.gbWZ2.Controls.Add(this.label11);
            this.gbWZ2.Controls.Add(this.txtWZ2Border);
            this.gbWZ2.Controls.Add(this.label18);
            this.gbWZ2.Controls.Add(this.cboWZ2Vis);
            this.gbWZ2.Controls.Add(this.label13);
            this.gbWZ2.Controls.Add(this.cboWZ2Temp);
            this.gbWZ2.Controls.Add(this.label14);
            this.gbWZ2.Controls.Add(this.cboWZ2Precip);
            this.errorProvider1.SetError(this.gbWZ2, resources.GetString("gbWZ2.Error"));
            this.errorProvider1.SetIconAlignment(this.gbWZ2, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gbWZ2.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gbWZ2, ((int)(resources.GetObject("gbWZ2.IconPadding"))));
            this.gbWZ2.Name = "gbWZ2";
            this.gbWZ2.TabStop = false;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.errorProvider1.SetError(this.label11, resources.GetString("label11.Error"));
            this.errorProvider1.SetIconAlignment(this.label11, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label11.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label11, ((int)(resources.GetObject("label11.IconPadding"))));
            this.label11.Name = "label11";
            // 
            // txtWZ2Border
            // 
            resources.ApplyResources(this.txtWZ2Border, "txtWZ2Border");
            this.errorProvider1.SetError(this.txtWZ2Border, resources.GetString("txtWZ2Border.Error"));
            this.errorProvider1.SetIconAlignment(this.txtWZ2Border, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtWZ2Border.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtWZ2Border, ((int)(resources.GetObject("txtWZ2Border.IconPadding"))));
            this.txtWZ2Border.Name = "txtWZ2Border";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.errorProvider1.SetError(this.label18, resources.GetString("label18.Error"));
            this.errorProvider1.SetIconAlignment(this.label18, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label18.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label18, ((int)(resources.GetObject("label18.IconPadding"))));
            this.label18.Name = "label18";
            // 
            // cboWZ2Vis
            // 
            resources.ApplyResources(this.cboWZ2Vis, "cboWZ2Vis");
            this.cboWZ2Vis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboWZ2Vis, resources.GetString("cboWZ2Vis.Error"));
            this.cboWZ2Vis.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboWZ2Vis, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboWZ2Vis.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboWZ2Vis, ((int)(resources.GetObject("cboWZ2Vis.IconPadding"))));
            this.cboWZ2Vis.Name = "cboWZ2Vis";
            this.cboWZ2Vis.SelectionChangeCommitted += new System.EventHandler(this.cboWZ2Vis_SelectionChangeCommitted);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.errorProvider1.SetError(this.label13, resources.GetString("label13.Error"));
            this.errorProvider1.SetIconAlignment(this.label13, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label13.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label13, ((int)(resources.GetObject("label13.IconPadding"))));
            this.label13.Name = "label13";
            // 
            // cboWZ2Temp
            // 
            resources.ApplyResources(this.cboWZ2Temp, "cboWZ2Temp");
            this.cboWZ2Temp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboWZ2Temp, resources.GetString("cboWZ2Temp.Error"));
            this.cboWZ2Temp.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboWZ2Temp, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboWZ2Temp.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboWZ2Temp, ((int)(resources.GetObject("cboWZ2Temp.IconPadding"))));
            this.cboWZ2Temp.Name = "cboWZ2Temp";
            this.cboWZ2Temp.SelectionChangeCommitted += new System.EventHandler(this.cboWZ2Temp_SelectionChangeCommitted);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.errorProvider1.SetError(this.label14, resources.GetString("label14.Error"));
            this.errorProvider1.SetIconAlignment(this.label14, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label14.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label14, ((int)(resources.GetObject("label14.IconPadding"))));
            this.label14.Name = "label14";
            // 
            // cboWZ2Precip
            // 
            resources.ApplyResources(this.cboWZ2Precip, "cboWZ2Precip");
            this.cboWZ2Precip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboWZ2Precip, resources.GetString("cboWZ2Precip.Error"));
            this.cboWZ2Precip.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboWZ2Precip, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboWZ2Precip.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboWZ2Precip, ((int)(resources.GetObject("cboWZ2Precip.IconPadding"))));
            this.cboWZ2Precip.Name = "cboWZ2Precip";
            this.cboWZ2Precip.SelectionChangeCommitted += new System.EventHandler(this.cboWZ2Precip_SelectionChangeCommitted);
            // 
            // gbWZ3
            // 
            resources.ApplyResources(this.gbWZ3, "gbWZ3");
            this.gbWZ3.Controls.Add(this.label12);
            this.gbWZ3.Controls.Add(this.txtWZ3Border);
            this.gbWZ3.Controls.Add(this.label19);
            this.gbWZ3.Controls.Add(this.cboWZ3Vis);
            this.gbWZ3.Controls.Add(this.label15);
            this.gbWZ3.Controls.Add(this.label16);
            this.gbWZ3.Controls.Add(this.cboWZ3Temp);
            this.gbWZ3.Controls.Add(this.cboWZ3Precip);
            this.errorProvider1.SetError(this.gbWZ3, resources.GetString("gbWZ3.Error"));
            this.errorProvider1.SetIconAlignment(this.gbWZ3, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("gbWZ3.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.gbWZ3, ((int)(resources.GetObject("gbWZ3.IconPadding"))));
            this.gbWZ3.Name = "gbWZ3";
            this.gbWZ3.TabStop = false;
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.errorProvider1.SetError(this.label12, resources.GetString("label12.Error"));
            this.errorProvider1.SetIconAlignment(this.label12, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label12.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label12, ((int)(resources.GetObject("label12.IconPadding"))));
            this.label12.Name = "label12";
            // 
            // txtWZ3Border
            // 
            resources.ApplyResources(this.txtWZ3Border, "txtWZ3Border");
            this.errorProvider1.SetError(this.txtWZ3Border, resources.GetString("txtWZ3Border.Error"));
            this.errorProvider1.SetIconAlignment(this.txtWZ3Border, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("txtWZ3Border.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.txtWZ3Border, ((int)(resources.GetObject("txtWZ3Border.IconPadding"))));
            this.txtWZ3Border.Name = "txtWZ3Border";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.errorProvider1.SetError(this.label19, resources.GetString("label19.Error"));
            this.errorProvider1.SetIconAlignment(this.label19, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label19.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label19, ((int)(resources.GetObject("label19.IconPadding"))));
            this.label19.Name = "label19";
            // 
            // cboWZ3Vis
            // 
            resources.ApplyResources(this.cboWZ3Vis, "cboWZ3Vis");
            this.cboWZ3Vis.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboWZ3Vis, resources.GetString("cboWZ3Vis.Error"));
            this.cboWZ3Vis.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboWZ3Vis, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboWZ3Vis.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboWZ3Vis, ((int)(resources.GetObject("cboWZ3Vis.IconPadding"))));
            this.cboWZ3Vis.Name = "cboWZ3Vis";
            this.cboWZ3Vis.SelectionChangeCommitted += new System.EventHandler(this.cboWZ3Vis_SelectionChangeCommitted);
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.errorProvider1.SetError(this.label15, resources.GetString("label15.Error"));
            this.errorProvider1.SetIconAlignment(this.label15, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label15.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label15, ((int)(resources.GetObject("label15.IconPadding"))));
            this.label15.Name = "label15";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.errorProvider1.SetError(this.label16, resources.GetString("label16.Error"));
            this.errorProvider1.SetIconAlignment(this.label16, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("label16.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.label16, ((int)(resources.GetObject("label16.IconPadding"))));
            this.label16.Name = "label16";
            // 
            // cboWZ3Temp
            // 
            resources.ApplyResources(this.cboWZ3Temp, "cboWZ3Temp");
            this.cboWZ3Temp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboWZ3Temp, resources.GetString("cboWZ3Temp.Error"));
            this.cboWZ3Temp.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboWZ3Temp, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboWZ3Temp.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboWZ3Temp, ((int)(resources.GetObject("cboWZ3Temp.IconPadding"))));
            this.cboWZ3Temp.Name = "cboWZ3Temp";
            this.cboWZ3Temp.SelectionChangeCommitted += new System.EventHandler(this.cboWZ3Temp_SelectionChangeCommitted);
            // 
            // cboWZ3Precip
            // 
            resources.ApplyResources(this.cboWZ3Precip, "cboWZ3Precip");
            this.cboWZ3Precip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.errorProvider1.SetError(this.cboWZ3Precip, resources.GetString("cboWZ3Precip.Error"));
            this.cboWZ3Precip.FormattingEnabled = true;
            this.errorProvider1.SetIconAlignment(this.cboWZ3Precip, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("cboWZ3Precip.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.cboWZ3Precip, ((int)(resources.GetObject("cboWZ3Precip.IconPadding"))));
            this.cboWZ3Precip.Name = "cboWZ3Precip";
            this.cboWZ3Precip.SelectionChangeCommitted += new System.EventHandler(this.cboWZ3Precip_SelectionChangeCommitted);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            resources.ApplyResources(this.errorProvider1, "errorProvider1");
            // 
            // btnSaveEnviron
            // 
            resources.ApplyResources(this.btnSaveEnviron, "btnSaveEnviron");
            this.errorProvider1.SetError(this.btnSaveEnviron, resources.GetString("btnSaveEnviron.Error"));
            this.errorProvider1.SetIconAlignment(this.btnSaveEnviron, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnSaveEnviron.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnSaveEnviron, ((int)(resources.GetObject("btnSaveEnviron.IconPadding"))));
            this.btnSaveEnviron.Name = "btnSaveEnviron";
            this.btnSaveEnviron.UseMnemonic = false;
            this.btnSaveEnviron.UseVisualStyleBackColor = true;
            this.btnSaveEnviron.Click += new System.EventHandler(this.btnSaveEnviron_Click);
            // 
            // btnCloseEnviron
            // 
            resources.ApplyResources(this.btnCloseEnviron, "btnCloseEnviron");
            this.errorProvider1.SetError(this.btnCloseEnviron, resources.GetString("btnCloseEnviron.Error"));
            this.errorProvider1.SetIconAlignment(this.btnCloseEnviron, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("btnCloseEnviron.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.btnCloseEnviron, ((int)(resources.GetObject("btnCloseEnviron.IconPadding"))));
            this.btnCloseEnviron.Name = "btnCloseEnviron";
            this.btnCloseEnviron.UseVisualStyleBackColor = true;
            this.btnCloseEnviron.Click += new System.EventHandler(this.btnCloseEnviron_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.cboTemperature);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cboWZ1Vis);
            this.groupBox1.Controls.Add(this.cboPrecipitation);
            this.errorProvider1.SetError(this.groupBox1, resources.GetString("groupBox1.Error"));
            this.errorProvider1.SetIconAlignment(this.groupBox1, ((System.Windows.Forms.ErrorIconAlignment)(resources.GetObject("groupBox1.IconAlignment"))));
            this.errorProvider1.SetIconPadding(this.groupBox1, ((int)(resources.GetObject("groupBox1.IconPadding"))));
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // frmEnviron
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCloseEnviron);
            this.Controls.Add(this.btnSaveEnviron);
            this.Controls.Add(this.gbEnviron);
            this.Controls.Add(this.gbWZ3);
            this.Controls.Add(this.gbCalendar);
            this.Controls.Add(this.gbWZ2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmEnviron";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmEnviron_Load);
            this.gbCalendar.ResumeLayout(false);
            this.gbCalendar.PerformLayout();
            this.gbEnviron.ResumeLayout(false);
            this.gbEnviron.PerformLayout();
            this.gbWZ2.ResumeLayout(false);
            this.gbWZ2.PerformLayout();
            this.gbWZ3.ResumeLayout(false);
            this.gbWZ3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCalendar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboStartMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboStartDay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboStartHour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTurnLength;
        private System.Windows.Forms.GroupBox gbEnviron;
        private System.Windows.Forms.GroupBox gbWZ3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cboWZ3Temp;
        private System.Windows.Forms.ComboBox cboWZ3Precip;
        private System.Windows.Forms.GroupBox gbWZ2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cboWZ2Temp;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cboWZ2Precip;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cboWZ1Vis;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPrecipitation;
        private System.Windows.Forms.ComboBox cboClimateArea;
        private System.Windows.Forms.ComboBox cboMapScale;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cboWZ3Vis;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cboWZ2Vis;
        private System.Windows.Forms.TextBox txtLastTurn;
        private System.Windows.Forms.TextBox txtStartYear;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnSaveEnviron;
        private System.Windows.Forms.Button btnCloseEnviron;
        private System.Windows.Forms.TextBox txtCurrentTurn;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cboTemperature;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtWZ2Border;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtWZ3Border;
    }
}