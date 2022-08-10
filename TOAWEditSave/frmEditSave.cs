using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using AutoIt;
using TOAWXML;

namespace TOAWEditSave
{
    
    public partial class frmEditSave : Form
    {

        public static Color rowcolor = Color.Empty;

        public frmEditSave()
        {
            InitializeComponent();
        }

        private void frmEditSave_Load(object sender, EventArgs e)
        {
            //DISABLE CONTROLS
            btnSelectLogFile.Enabled = false;
            txtSelectedLogFile.Enabled = false;
            btnCalcRP.Enabled = false;
            btnEvents.Enabled = false;
            btnDeleteEvents.Enabled = false;
            progressSavedGame.Visible = false;
            btnCurrentTurn.Enabled = false;

            //SET RP CATEGORY NAMES
            txtCity.Text = TOAWEditSave.Properties.Settings.Default.City;
            txtTown.Text = TOAWEditSave.Properties.Settings.Default.Town;
            txtVillage.Text = TOAWEditSave.Properties.Settings.Default.Village;
            txtBM1.Text = TOAWEditSave.Properties.Settings.Default.BM1;
            txtBM2.Text = TOAWEditSave.Properties.Settings.Default.BM2;
            txtBM3.Text = TOAWEditSave.Properties.Settings.Default.BM3;
            txtBM4.Text = TOAWEditSave.Properties.Settings.Default.BM4;
            txtBM5.Text = TOAWEditSave.Properties.Settings.Default.BM5;
            txtBM6.Text = TOAWEditSave.Properties.Settings.Default.BM6;
            txtBM7.Text = TOAWEditSave.Properties.Settings.Default.BM7;
            txtBM8.Text = TOAWEditSave.Properties.Settings.Default.BM8;
            txtBM9.Text = TOAWEditSave.Properties.Settings.Default.BM9;

            //SET RP VALUES
            txtRPCity.Text = TOAWEditSave.Properties.Settings.Default.RPCity;
            txtRPTown.Text = TOAWEditSave.Properties.Settings.Default.RPTown; 
            txtRPVillage.Text = TOAWEditSave.Properties.Settings.Default.RPVill;
            txtRPBM1.Text = TOAWEditSave.Properties.Settings.Default.RPBM1;
            txtRPBM2.Text = TOAWEditSave.Properties.Settings.Default.RPBM2;
            txtRPBM3.Text = TOAWEditSave.Properties.Settings.Default.RPBM3;
            txtRPBM4.Text = TOAWEditSave.Properties.Settings.Default.RPBM4;
            txtRPBM5.Text = TOAWEditSave.Properties.Settings.Default.RPBM5;
            txtRPBM6.Text = TOAWEditSave.Properties.Settings.Default.RPBM6;
            txtRPBM7.Text = TOAWEditSave.Properties.Settings.Default.RPBM7;
            txtRPBM8.Text = TOAWEditSave.Properties.Settings.Default.RPBM8;
            txtRPBM9.Text = TOAWEditSave.Properties.Settings.Default.RPBM9;
            txtF1InitRP.Text = TOAWEditSave.Properties.Settings.Default.F1InitRP;
            txtF2InitRP.Text = TOAWEditSave.Properties.Settings.Default.F2InitRP;
            txtF1AdjustRP.Text = "0";
            txtF2AdjustRP.Text = "0";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        private async void btnSelectSavedGame_Click(object sender, EventArgs e)
        {
            string FilePath;
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .gam files *.gam|*.gam";

            if (file.ShowDialog() == DialogResult.OK)
            {
                FilePath = file.FileName;
                TOAWEditSave.Properties.Settings.Default.FilePath = file.FileName;
                TOAWEditSave.Properties.Settings.Default.Save();
                txtSelectedGamFile.Text = FilePath;
                btnSelectLogFile.Enabled = true;
                txtSelectedLogFile.Enabled = true;
                btnCalcRP.Enabled = true;
                progressSavedGame.Visible = true;
            }
            else
            {
                //Application.Exit();
                this.Close();
                return;
            }
            
            //OPEN XELEM
            XElement xelem = XElement.Load(FilePath);

            //GET NAMES OF FORCES AND ASSIGN TO CONTROLS
            var forcenames = xelem.Descendants("HEADER").First();
            string fn1 = forcenames.Attribute("forceName1").Value.ToString(); ;
            string fn2 = forcenames.Attribute("forceName2").Value.ToString();
            
            ////ADD FORCE NAMES TO CONTROLS
            //fn1 = forcenames.Attribute("forceName1").Value.ToString();
            //fn2 = forcenames.Attribute("forceName2").Value.ToString();

            rbForce1.Text = fn1;
            gbForce1.Text = fn1;
            lblF1InitRP.Text = fn1 + "\nInit RP";
            lblF1CurrentRP.Text = fn1 + "\nCurrent RP";
            lblF1AdjustRP.Text = fn1 + "\nAdjust RP";
            lblF1TotalRP.Text = fn1 + "\nTotal RP";

            rbForce2.Text = fn2;
            gbForce2.Text = fn2;
            lblF2InitRP.Text = fn2 + "\nInit RP";
            lblF2CurrentRP.Text = fn2 + "\nCurrent RP";
            lblF2AdjustRP.Text = fn2 + "\nAdjust RP";
            lblF2TotalRP.Text = fn2 + "\nTotal RP";

            //SET CALENDAR DATA
            string xpathCalendar;
            string turnlength;

            int startday = 0;
            int startmonth = 0;
            int startyear = 0;
            int currentturn = 0;

            xpathCalendar = "CALENDAR";
            XElement calendardata = xelem.XPathSelectElement(xpathCalendar);

            //SET DATETIMEPICKER TO INITIAL DATE FROM XML
            startday = Int32.Parse(calendardata.Attribute("startDay").Value) +1;
            startmonth = Int32.Parse(calendardata.Attribute("startMonth").Value) +1;
            startyear = Int32.Parse(calendardata.Attribute("startYear").Value) +1;
            currentturn = Int32.Parse(calendardata.Attribute("currentTurn").Value);
                        
            dteGameDate.Value = new DateTime(startyear, startmonth, startday);
            numTurn.Value = currentturn;
            turnlength = calendardata.Attribute("turnLength").Value;

            ////>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            //POPULATES START HOUR COMBOBOX
            var starthour = new BindingList<KeyValuePair<string, string>>();

            switch (turnlength)
            {
                case "9":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("23", "Midnight"));
                    starthour.Add(new KeyValuePair<string, string>("1", "01:00"));
                    starthour.Add(new KeyValuePair<string, string>("2", "02:00"));
                    starthour.Add(new KeyValuePair<string, string>("3", "03:00"));
                    starthour.Add(new KeyValuePair<string, string>("4", "04:00"));
                    starthour.Add(new KeyValuePair<string, string>("5", "05:00"));
                    starthour.Add(new KeyValuePair<string, string>("6", "06:00"));
                    starthour.Add(new KeyValuePair<string, string>("7", "07:00"));
                    starthour.Add(new KeyValuePair<string, string>("8", "08:00"));
                    starthour.Add(new KeyValuePair<string, string>("9", "09:00"));
                    starthour.Add(new KeyValuePair<string, string>("10", "10:00"));
                    starthour.Add(new KeyValuePair<string, string>("11", "11:00"));
                    starthour.Add(new KeyValuePair<string, string>("12", "12:00"));
                    starthour.Add(new KeyValuePair<string, string>("13", "13:00"));
                    starthour.Add(new KeyValuePair<string, string>("14", "14:00"));
                    starthour.Add(new KeyValuePair<string, string>("15", "15:00"));
                    starthour.Add(new KeyValuePair<string, string>("16", "16:00"));
                    starthour.Add(new KeyValuePair<string, string>("17", "17:00"));
                    starthour.Add(new KeyValuePair<string, string>("18", "18:00"));
                    starthour.Add(new KeyValuePair<string, string>("19", "19:00"));
                    starthour.Add(new KeyValuePair<string, string>("20", "20:00"));
                    starthour.Add(new KeyValuePair<string, string>("21", "21:00"));
                    starthour.Add(new KeyValuePair<string, string>("22", "23:00"));
                    break;

                case "10":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("0", "Midnight"));
                    starthour.Add(new KeyValuePair<string, string>("3", "03:00"));
                    starthour.Add(new KeyValuePair<string, string>("6", "06:00"));
                    starthour.Add(new KeyValuePair<string, string>("9", "09:00"));
                    starthour.Add(new KeyValuePair<string, string>("12", "12:00"));
                    starthour.Add(new KeyValuePair<string, string>("15", "15:00"));
                    starthour.Add(new KeyValuePair<string, string>("18", "18:00"));
                    starthour.Add(new KeyValuePair<string, string>("21", "21:00"));
                    break;

                case "0":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("0", "Pre-Dawn"));
                    starthour.Add(new KeyValuePair<string, string>("6", "Morning"));
                    starthour.Add(new KeyValuePair<string, string>("12", "Afternoon"));
                    starthour.Add(new KeyValuePair<string, string>("18", "Night"));
                    break;

                case "1":
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("12", "AM"));
                    starthour.Add(new KeyValuePair<string, string>("0", "PM"));
                    break;

                default:
                    starthour = new BindingList<KeyValuePair<string, string>>();
                    starthour.Add(new KeyValuePair<string, string>("0", "--"));
                    break;
            }

            cboStartHour.DataSource = starthour;
            cboStartHour.ValueMember = "Key";
            cboStartHour.DisplayMember = "Value";
            cboStartHour.SelectedValue = calendardata.Attribute("startHour").Value.ToString();

            ////////////////>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            ////GET DATA FROM UPDATED DATETIMEPICKER
            //currentyear = dteGameDate.Value.Year.ToString();
            //currentmonth = dteGameDate.Value.Month.ToString();
            //currentday = dteGameDate.Value.Day.ToString();

            ////INSERT REVISED CALENDAR DATA INTO XML
            //calendardata.Attribute("startHour").Value = currenthour;
            //calendardata.Attribute("startDay").Value = currentday;
            //calendardata.Attribute("startMonth").Value = currentmonth;
            //calendardata.Attribute("startYear").Value = currentyear;

            //calendardata.Attribute("currentTurn").Value = "0"; //OR 1?  NEED TO SUBTRACT 1 WHEN ADDING TO XML?
            //calendardata.Attribute("finalTurn").Value = (finalturn - currentturn).ToString();

            ////DETERMINE IF FILE NAME ENDS IN -1 or -2, ASSIGN TO FORCE RADIO BUTTON
            ////string forceturn = "";
            //string truncfilepath = FilePath.Substring(0, FilePath.IndexOf("."));
            //string forceturn = truncfilepath.Substring(truncfilepath.Length - 2, 2);

            //if(forceturn == "-1")
            //{
            //    rbForce1.Select();
            //}
            //else if (forceturn == "-2")
            //{
            //    rbForce2.Select();
            //}
            //else
            //{
            //    MessageBox.Show("gam filename does not end in '-1' or '-2' to indicate which force has turn." 
            //        + Environment.NewLine + Environment.NewLine +
            //        "Please select the correct force manually.",
            //        "No Force Turn Indicator",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Exclamation,
            //        MessageBoxDefaultButton.Button1);
            //}


            ////FIX VARIOUS DATA GLITCHES IN THE EVENT XML (IE, EVENTS HAVING DATA IN FIELDS THAT DON'T MATTER FOR THAT EVENT, ETC.)
            /////***MOVED TO TXTCURRENTTURN****////

            //IEnumerable<XElement> zing = xelem.XPathSelectElements("EVENTS/EVENT");
            //foreach (XElement z in zing)
            //{
            //    //TRIGGER GROUP 5 = NULL 
            //    if (z.Attribute("TRIGGER").Value == "Turn" && z.Attribute("TURN") == null)
            //    {
            //        //DON'T NEED TO ADD ATTRIBUTE IF TURN VALUE = 1
            //        //SHOULD NOT BE ANY CONTINGENCY VALUES FOR TURN TRIGGERS
            //        if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();

            //        //REMOVE X, Y ATTRIBUTES FOR ANYTHING BUT EFFECT GROUPS 3-6
            //        if (z.Attribute("EFFECT").Value != "Nuclear Attack" || z.Attribute("EFFECT").Value != "Form'n orders" ||
            //            z.Attribute("EFFECT").Value != "Suppply Point 1" || z.Attribute("EFFECT").Value != "Supply Point 2" ||
            //            z.Attribute("EFFECT").Value != "Set ownership 1" || z.Attribute("EFFECT").Value != "Set ownership 2" ||
            //            z.Attribute("EFFECT").Value != "Refugee 1" || z.Attribute("EFFECT").Value != "Refugee 2")
            //        {
            //            if (z.Attribute("X") != null) z.Attribute("X").Remove();
            //            if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
            //        }

            //        //REMOVE VALUE ATTRIBUTE FOR EFFECTS GROUPS 2 & 4
            //        if (z.Attribute("EFFECT").Value == "Set ownership 1" || z.Attribute("EFFECT").Value == "Set ownership 2" ||
            //            z.Attribute("EFFECT").Value == "Cease Fire" || z.Attribute("EFFECT").Value == "Cool front" ||
            //            z.Attribute("EFFECT").Value == "End normal" || z.Attribute("EFFECT").Value == "News only" ||
            //            z.Attribute("EFFECT").Value == "Open fire" || z.Attribute("EFFECT").Value == "Storms" || z.Attribute("EFFECT").Value == "Warm front" ||
            //            z.Attribute("EFFECT").Value == "End victory 1" || z.Attribute("EFFECT").Value == "End victory 2" ||
            //            z.Attribute("EFFECT").Value == "Remove zone 1" || z.Attribute("EFFECT").Value == "Remove zone 2" ||
            //            z.Attribute("EFFECT").Value == "Use chemicals 1" || z.Attribute("EFFECT").Value == "Use chemicals 2")
            //        {
            //            if (z.Attribute("VALUE") != null) z.Attribute("VALUE").Remove();
            //        }
            //    }
            //    //TRIGGER GROUP 5 != NULL
            //    else if (z.Attribute("TRIGGER").Value == "Turn" && z.Attribute("TURN").Value != null) //T5
            //    {
            //        if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();

            //        //REMOVE X, Y ATTRIBUTES FOR ANYTHING BUT EFFECT GROUPS 3-6
            //        if (z.Attribute("EFFECT").Value != "Nuclear Attack" || z.Attribute("EFFECT").Value != "Form'n orders" ||
            //            z.Attribute("EFFECT").Value != "Suppply Point 1" || z.Attribute("EFFECT").Value != "Supply Point 2" ||
            //            z.Attribute("EFFECT").Value != "Set ownership 1" || z.Attribute("EFFECT").Value != "Set ownership 2" ||
            //            z.Attribute("EFFECT").Value != "Refugee 1" || z.Attribute("EFFECT").Value != "Refugee 2")
            //        {
            //            if (z.Attribute("X") != null) z.Attribute("X").Remove();
            //            if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
            //        }

            //        //REMOVE VALUE ATTRIBUTE FOR EFFECTS GROUPS 2 & 4
            //        if (z.Attribute("EFFECT").Value == "Set ownership 1" || z.Attribute("EFFECT").Value == "Set ownership 2" ||
            //            z.Attribute("EFFECT").Value == "Cease Fire" || z.Attribute("EFFECT").Value == "Cool front" ||
            //            z.Attribute("EFFECT").Value == "End normal" || z.Attribute("EFFECT").Value == "News only" ||
            //            z.Attribute("EFFECT").Value == "Open fire" || z.Attribute("EFFECT").Value == "Storms" || z.Attribute("EFFECT").Value == "Warm front" ||
            //            z.Attribute("EFFECT").Value == "End victory 1" || z.Attribute("EFFECT").Value == "End victory 2" ||
            //            z.Attribute("EFFECT").Value == "Remove zone 1" || z.Attribute("EFFECT").Value == "Remove zone 2" ||
            //            z.Attribute("EFFECT").Value == "Use chemicals 1" || z.Attribute("EFFECT").Value == "Use chemicals 2")
            //        {
            //            if (z.Attribute("VALUE") != null) z.Attribute("VALUE").Remove();
            //        }
            //    }

            //    //TRIGGER GROUP 4
            //    else if (z.Attribute("TRIGGER").Value == "1 uses chemical" || z.Attribute("TRIGGER").Value == "2 uses chemical" ||
            //            z.Attribute("TRIGGER").Value == "1 uses nuclear" || z.Attribute("TRIGGER").Value == "2 uses nuclear")
            //    {
            //        if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();

            //        //REMOVE X, Y ATTRIBUTES FOR ANYTHING BUT EFFECT GROUPS 3-6
            //        if (z.Attribute("EFFECT").Value != "Nuclear Attack" && z.Attribute("EFFECT").Value != "Form'n orders" &&
            //            z.Attribute("EFFECT").Value != "Suppply Point 1" && z.Attribute("EFFECT").Value != "Supply Point 2" &&
            //            z.Attribute("EFFECT").Value != "Set ownership 1" && z.Attribute("EFFECT").Value != "Set ownership 2" &&
            //            z.Attribute("EFFECT").Value != "Refugee 1" && z.Attribute("EFFECT").Value != "Refugee 2")
            //        {
            //            if (z.Attribute("X") != null) z.Attribute("X").Remove();
            //            if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
            //        }

            //        //REMOVE VALUE ATTRIBUTE FOR EFFECTS GROUPS 2 & 4
            //        if (z.Attribute("EFFECT").Value == "Set ownership 1" || z.Attribute("EFFECT").Value == "Set ownership 2" ||
            //            z.Attribute("EFFECT").Value == "Cease Fire" || z.Attribute("EFFECT").Value == "Cool front" ||
            //            z.Attribute("EFFECT").Value == "End normal" || z.Attribute("EFFECT").Value == "News only" ||
            //            z.Attribute("EFFECT").Value == "Open fire" || z.Attribute("EFFECT").Value == "Storms" || z.Attribute("EFFECT").Value == "Warm front" ||
            //            z.Attribute("EFFECT").Value == "End victory 1" || z.Attribute("EFFECT").Value == "End victory 2" ||
            //            z.Attribute("EFFECT").Value == "Remove zone 1" || z.Attribute("EFFECT").Value == "Remove zone 2" ||
            //            z.Attribute("EFFECT").Value == "Use chemicals 1" || z.Attribute("EFFECT").Value == "Use chemicals 2")
            //        {
            //            if (z.Attribute("VALUE") != null) z.Attribute("VALUE").Remove();
            //        }
            //    }
            //    //TRIGGER GROUP 3
            //    else if (z.Attribute("TRIGGER").Value == "Event activated" || z.Attribute("TRIGGER").Value == "Event cancelled")
            //    {
            //        //REMOVE X, Y ATTRIBUTES FOR ANYTHING BUT EFFECT GROUPS 3-6
            //        if (z.Attribute("EFFECT").Value != "Nuclear Attack" && z.Attribute("EFFECT").Value != "Form'n orders" &&
            //            z.Attribute("EFFECT").Value != "Suppply Point 1" && z.Attribute("EFFECT").Value != "Supply Point 2" &&
            //            z.Attribute("EFFECT").Value != "Set ownership 1" && z.Attribute("EFFECT").Value != "Set ownership 2" &&
            //            z.Attribute("EFFECT").Value != "Refugee 1" && z.Attribute("EFFECT").Value != "Refugee 2")
            //        {
            //            if (z.Attribute("X") != null) z.Attribute("X").Remove();
            //            if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
            //        }

            //        //REMOVE VALUE ATTRIBUTE FOR EFFECTS GROUPS 2 & 4
            //        if (z.Attribute("EFFECT").Value == "Set ownership 1" || z.Attribute("EFFECT").Value == "Set ownership 2" ||
            //            z.Attribute("EFFECT").Value == "Cease Fire" || z.Attribute("EFFECT").Value == "Cool front" ||
            //            z.Attribute("EFFECT").Value == "End normal" || z.Attribute("EFFECT").Value == "News only" ||
            //            z.Attribute("EFFECT").Value == "Open fire" || z.Attribute("EFFECT").Value == "Storms" || z.Attribute("EFFECT").Value == "Warm front" ||
            //            z.Attribute("EFFECT").Value == "End victory 1" || z.Attribute("EFFECT").Value == "End victory 2" ||
            //            z.Attribute("EFFECT").Value == "Remove zone 1" || z.Attribute("EFFECT").Value == "Remove zone 2" ||
            //            z.Attribute("EFFECT").Value == "Use chemicals 1" || z.Attribute("EFFECT").Value == "Use chemicals 2")
            //        {
            //            if (z.Attribute("VALUE") != null) z.Attribute("VALUE").Remove();
            //        }
            //    }
            //    //TRIGGER GROUP 2
            //    else if (z.Attribute("TRIGGER").Value == "Force 1 winning" || z.Attribute("TRIGGER").Value == "Force 2 winning" ||
            //            z.Attribute("TRIGGER").Value == "Variable value" || z.Attribute("TRIGGER").Value == "Unit destroyed")
            //    {
            //        if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();
            //        if (z.Attribute("X") != null) z.Attribute("X").Remove();
            //        if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
            //    }

            //    //TRIGGER GROUP 1
            //    else if (z.Attribute("TRIGGER").Value == "1 occupies" || z.Attribute("TRIGGER").Value == "2 occupies" ||
            //                z.Attribute("TRIGGER").Value == "1 attacks" || z.Attribute("TRIGGER").Value == "2 attacks")
            //    {
            //        if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();
            //    }
            //}  //NEW FOREACH END
            //xelem.Save(FilePath); //COMMENTED OUT JUL 29 2022

            //}  OLD FOR EACH END

            //CREATE EVENTS DATATABLE, LOAD DATAGRIDVIEW
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("TRIGGER", typeof(string));
            dt.Columns.Add("TURN", typeof(string));
            dt.Columns.Add("EVT", typeof(string));
            dt.Columns.Add("EFFECT", typeof(string));
            dt.Columns.Add("VAL", typeof(string));
            dt.Columns.Add("PROB", typeof(string));
            dt.Columns.Add("RNG", typeof(string));
            dt.Columns.Add("NEWS", typeof(string));

            var events = (from v in xelem.Descendants("EVENTS").Descendants("EVENT")
                          select new
                          {
                              ID = v.Attribute("ID").Value,
                              TRIGGER = v.Attribute("TRIGGER").Value,
                              EFFECT = v.Attribute("EFFECT").Value,
                              EVT = (string)v.Attribute("CONTINGENCY") ?? "--",
                              PROB = (string)v.Attribute("CHANCE") ?? "100",
                              NEWS = (string)v.Attribute("NEWS") ?? "--",
                              RNG = (string)(v.Attribute("VARIABLE") != null ? (Int32.Parse(v.Attribute("VARIABLE").Value) + 1).ToString() : "--") ?? "--",
                              VAL = (string)v.Attribute("VALUE") ?? "--",
                              TURN = (string)v.Attribute("TURN") ?? "--"
                          });

            //LOAD EVENT DATAGRIDVIEW
            //MUST ADJUST DGV NUMBERS FROM XML NUMBERS, BASED ON CERTAIN TRIGGERS AND EFFECTS
            foreach(var q in events.ToList())
            {
                //FIX DGV VALUES FROM XML VALUES
                if (q.TRIGGER == "Turn" || q.EFFECT == "Activate event" || q.EFFECT == "Enable event" || q.EFFECT == "Cancel event")
                {
                    if(q.TRIGGER == "Turn" && (q.EFFECT == "Activate event" || q.EFFECT == "Enable event" || q.EFFECT == "Cancel event"))
                    {
                        if (q.TURN == "--")
                        {
                            dt.Rows.Add(q.ID, q.TRIGGER, 1, q.EVT, q.EFFECT, q.VAL, q.PROB, q.RNG, q.NEWS);
                        }
                        else
                        {
                            dt.Rows.Add(q.ID, q.TRIGGER, Int32.Parse(q.TURN) + 1, q.EVT, q.EFFECT, q.VAL, q.PROB, q.RNG, q.NEWS);
                        }
                    }
                    else if(q.TRIGGER == "Turn" && (q.EFFECT != "Activate event" || q.EFFECT != "Enable event" || q.EFFECT != "Cancel event"))
                    {
                        if (q.TURN == "--")
                        {
                            dt.Rows.Add(q.ID, q.TRIGGER, 1, q.EVT, q.EFFECT, q.VAL, q.PROB, q.RNG, q.NEWS);
                        }
                        else
                        {
                            dt.Rows.Add(q.ID, q.TRIGGER, Int32.Parse(q.TURN) + 1, q.EVT, q.EFFECT, q.VAL, q.PROB, q.RNG, q.NEWS);
                        }
                    }
                    else if(q.TRIGGER != "Turn" && (q.EFFECT == "Activate event" || q.EFFECT == "Enable event" || q.EFFECT == "Cancel event"))
                    {
                        dt.Rows.Add(q.ID, q.TRIGGER, q.TURN, q.EVT, q.EFFECT, Int32.Parse(q.VAL) + 1, q.PROB, q.RNG, q.NEWS);
                    }
                }
                else
                {
                    dt.Rows.Add(q.ID, q.TRIGGER, q.TURN, q.EVT, q.EFFECT, q.VAL, q.PROB, q.RNG, q.NEWS);
                }
            }

            dgvEvents.DataSource = dt;

            dgvEvents.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[1].ReadOnly = true;
            dgvEvents.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvEvents.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvEvents.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvEvents.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEvents.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvEvents.Columns[0].Width = 40;
            dgvEvents.Columns[1].Width = 40;
            dgvEvents.Columns[2].Width = 100;
            dgvEvents.Columns[3].Width = 40;
            dgvEvents.Columns[4].Width = 40;
            dgvEvents.Columns[5].Width = 100;
            dgvEvents.Columns[6].Width = 40;
            dgvEvents.Columns[7].Width = 40;
            dgvEvents.Columns[8].Width = 40;
            dgvEvents.Columns[9].Width = 120;

            dgvEvents.Columns[1].ReadOnly = true;
            dgvEvents.Columns[2].ReadOnly = true;
            dgvEvents.Columns[3].ReadOnly = true;
            dgvEvents.Columns[4].ReadOnly = true;
            dgvEvents.Columns[5].ReadOnly = true;
            dgvEvents.Columns[6].ReadOnly = true;
            dgvEvents.Columns[7].ReadOnly = true;
            dgvEvents.Columns[8].ReadOnly = true;
            dgvEvents.Columns[9].ReadOnly = true;

            await Task.Delay(500);
            progressSavedGame.Visible = false;
        }

        private void btnSelectLogFile_Click(object sender, EventArgs e)
        {
            string LogFilePath;
            string FilePath;
            string xpathCalendar;
            string turnlength;
            //string finalturn;
            string logdate;
            string simplelogdate;
            string prefixlogdate;
            string hourslogdate = "";
            string restatelogdate;
            string daylogdate;
            //string currentdate = "";
            string currenttime = "";
            DateTime logdatetime;
            DateTime currentdatetime = new DateTime();
            double periodsperturn = 0;
            double deltaturns;
            double deltaperiods;

            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "sitrepLog.txt files *.txt|*.txt";

            if (file.ShowDialog() == DialogResult.OK)
            {
                LogFilePath = file.FileName;
                txtSelectedLogFile.Text = LogFilePath;
                FilePath = txtSelectedGamFile.Text;

                //GET TURN NUMBER FROM LOG FILE NAME
                string rightside = LogFilePath.Substring(LogFilePath.Length - 7, 7);
                string number = rightside.Substring(0, 3);
                int currentturn = 0;
                Int32.TryParse(number, out currentturn);

                //txtTurn.Text = currentturn.ToString();
                numTurn.Value = currentturn; //AUG 1, 2022

                //GET CALENDAR DATA FROM GAM FILE
                XElement xelem = XElement.Load(FilePath);
                xpathCalendar = "CALENDAR";
                XElement calendardata = xelem.XPathSelectElement(xpathCalendar);
                turnlength = calendardata.Attribute("turnLength").Value;
                //finalturn = calendardata.Attribute("finalTurn").Value;
                deltaturns = currentturn - 1;

                //GET LOG DATE
                using (var reader = new StreamReader(LogFilePath))
                {
                    reader.ReadLine(); // skip
                    logdate = reader.ReadLine();
                }

                ////TEST LINES
                //turnlength = "1";
                //logdate = "AM, July 1st, 1940";
                ////logdate = " AM, July 1st, 1940";
                ////END TEST LINES

                //REMOVE ORDINALS FROM LOG DATE
                simplelogdate = TOAWXML.Utils.RemoveOrdinals(logdate);

                switch (turnlength)
                {
                    case "9":   //1 hour
                        periodsperturn = 1;
                        logdatetime = DateTime.Parse(simplelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddHours(deltaperiods);
                        //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = currentdatetime.ToShortTimeString();
                        //Console.WriteLine(currenttime);
                        break;

                    case "10":  //3 hours
                        periodsperturn = 3;
                        logdatetime = DateTime.Parse(simplelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddHours(deltaperiods);
                        //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = currentdatetime.ToShortTimeString();
                        break;

                    case "0":  //6 hours
                        periodsperturn = 6;
                        prefixlogdate = simplelogdate.Split(',')[0];
                        daylogdate = simplelogdate.Split(new char[] { ',' }, 2)[1]; ;
                        switch (prefixlogdate)
                        {
                            case "Pre-dawn":
                                hourslogdate = "00:00,";
                                break;
                            case "Morning":
                                hourslogdate = "06:00,";
                                break;
                            case "Afternoon":
                                hourslogdate = "12:00,";
                                break;
                            case "Night":
                                hourslogdate = "18:00,";
                                break;
                        }

                        restatelogdate = hourslogdate + daylogdate;
                        logdatetime = DateTime.Parse(restatelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddHours(deltaperiods);
                        //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = currentdatetime.ToShortTimeString();

                        //switch (currenttime)
                        //{
                        //    case "00:00":
                        //        currenttime = "Pre-dawn";
                        //        break;
                        //    case "06:00":
                        //        currenttime = "Morning";
                        //        break;
                        //    case "12:00":
                        //        currenttime = "Afternoon";
                        //        break;
                        //    case "18:00":
                        //        currenttime = "Night";
                        //        break;
                        //}
                        //currenttime = currentdatetime.Date.ToShortTimeString();
                        break;

                    case "1":  //12 hours
                        periodsperturn = 12;
                        prefixlogdate = simplelogdate.Split(',')[0];
                        daylogdate = simplelogdate.Split(new char[] { ',' }, 2)[1]; ;
                        switch (prefixlogdate)
                        {
                            case "AM":
                                hourslogdate = "12:00,";
                                break;
                            case "PM":
                                hourslogdate = "00:00,";
                                break;
                        }
                        restatelogdate = hourslogdate + daylogdate;
                        logdatetime = DateTime.Parse(restatelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddHours(deltaperiods);
                        //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = currentdatetime.Date.ToShortTimeString();
                        switch (currenttime)
                        {
                            case "12:00":
                                currenttime = "AM";
                                break;
                            case "00:00":
                                currenttime = "PM";
                                break;
                        }
                        break;

                    case "2": //24 hours
                        periodsperturn = 24;
                        logdatetime = DateTime.Parse(simplelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddHours(deltaperiods);
                       //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = "";
                        break;

                    case "3": //Half week
                        periodsperturn = 84;
                        logdatetime = DateTime.Parse(simplelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddHours(deltaperiods);
                        //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = "";
                        break;

                    case "4": //One week
                        periodsperturn = 168;
                        logdatetime = DateTime.Parse(simplelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddHours(deltaperiods);
                       //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = "";
                        break;

                    case "5": //Two weeks
                        periodsperturn = 336;
                        logdatetime = DateTime.Parse(simplelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddHours(deltaperiods);
                        //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = "";
                        break;

                    case "6": //Month
                        periodsperturn = 1;
                        logdatetime = DateTime.Parse(simplelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddMonths((int)deltaperiods);
                        //currentdate = currentdatetime.Date.ToShortDateString();
                        currenttime = "";
                        break;

                    case "7": //SEASON
                        //SEASONAL TURNS DON'T SEEM TO WORK--YOU CANNOT SET THE SEASON, ONLY THE MONTH, WHICH DOES NOT CHANGE.
                        //periodsperturn = 3;
                        //logdatetime = DateTime.Parse(simplelogdate);
                        //deltaperiods = periodsperturn * deltaturns;
                        //currentdatetime = logdatetime.AddMonths((int)deltaperiods);
                        //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = "";
                        break;

                    case "8": //Year
                        periodsperturn = 1;
                        logdatetime = DateTime.Parse(simplelogdate);
                        deltaperiods = periodsperturn * deltaturns;
                        currentdatetime = logdatetime.AddYears((int)deltaperiods);
                        //currentdate = currentdatetime.Date.ToShortDateString();
                        //currenttime = "";
                        break;
                }

                //UPDATE UI
                btnCurrentTurn.Enabled = true;
                btnEvents.Enabled = true;

                //SET UI DATE AND TIME
                dteGameDate.Value = currentdatetime;//added AUG 2, 2022
                cboStartHour.SelectedValue = currentdatetime.TimeOfDay.Hours.ToString();

                ////INSERT REVISED CALENDAR DATA INTO XML
                //calendardata.Attribute("startDay").Value = currentdate;
                //calendardata.Attribute("startHour").Value = currenttime;
                //calendardata.Attribute("finalTurn").Value = (Int32.Parse(finalturn) - currentturn).ToString();
                //calendardata.Attribute("currentTurn").Value = "1";  //AUG 1, 2022

                //xelem.Save(FilePath);
            }

            else
            {
                //Application.Exit();
                this.Close();
                return;
            }
        }

        private void btnCurrentTurn_Click(object sender, EventArgs e)
        {
            string FilePath = txtSelectedGamFile.Text;
            string xpathCalendar;
            string currentturntxt = "";

            int currentturn = 0;
            int finalturn = 0;

            XElement xelem = XElement.Load(FilePath);

            Int32.TryParse(currentturntxt, out currentturn);
            //txtTurn.Text = currentturn.ToString();
            xpathCalendar = "CALENDAR";
            XElement calendardata = xelem.XPathSelectElement(xpathCalendar);
                        
            //INSERT REVISED CALENDAR DATA INTO XML
            //calendardata.Attribute("startHour").Value = cboStartHour.SelectedValue.ToString();
            calendardata.Attribute("startHour").Value = cboStartHour.SelectedValue.ToString();
            calendardata.Attribute("startDay").Value = (dteGameDate.Value.Day - 1).ToString();
            calendardata.Attribute("startMonth").Value = (dteGameDate.Value.Month - 1).ToString();
            calendardata.Attribute("startYear").Value = (dteGameDate.Value.Year -1).ToString();
            calendardata.Attribute("finalTurn").Value = (Int32.Parse(calendardata.Attribute("finalTurn").Value) - numTurn.Value).ToString();
            calendardata.Attribute("finalTurn").Value = (finalturn - currentturn).ToString();
            calendardata.Attribute("currentTurn").Value = numTurn.Value.ToString();

            //FIX VARIOUS DATA GLITCHES IN THE EVENT XML (IE, EVENTS HAVING DATA IN FIELDS THAT DON'T MATTER FOR THAT EVENT, ETC.)
            IEnumerable<XElement> zing = xelem.XPathSelectElements("EVENTS/EVENT");
            foreach (XElement z in zing)
            {
                //TRIGGER GROUP 5 = NULL 
                if (z.Attribute("TRIGGER").Value == "Turn" && z.Attribute("TURN") == null)
                {
                    //DON'T NEED TO ADD ATTRIBUTE IF TURN VALUE = 1
                    //SHOULD NOT BE ANY CONTINGENCY VALUES FOR TURN TRIGGERS
                    if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();

                    //REMOVE X, Y ATTRIBUTES FOR ANYTHING BUT EFFECT GROUPS 3-6
                    if (z.Attribute("EFFECT").Value != "Nuclear Attack" || z.Attribute("EFFECT").Value != "Form'n orders" ||
                        z.Attribute("EFFECT").Value != "Suppply Point 1" || z.Attribute("EFFECT").Value != "Supply Point 2" ||
                        z.Attribute("EFFECT").Value != "Set ownership 1" || z.Attribute("EFFECT").Value != "Set ownership 2" ||
                        z.Attribute("EFFECT").Value != "Refugee 1" || z.Attribute("EFFECT").Value != "Refugee 2")
                    {
                        if (z.Attribute("X") != null) z.Attribute("X").Remove();
                        if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
                    }

                    //REMOVE VALUE ATTRIBUTE FOR EFFECTS GROUPS 2 & 4
                    if (z.Attribute("EFFECT").Value == "Set ownership 1" || z.Attribute("EFFECT").Value == "Set ownership 2" ||
                        z.Attribute("EFFECT").Value == "Cease Fire" || z.Attribute("EFFECT").Value == "Cool front" ||
                        z.Attribute("EFFECT").Value == "End normal" || z.Attribute("EFFECT").Value == "News only" ||
                        z.Attribute("EFFECT").Value == "Open fire" || z.Attribute("EFFECT").Value == "Storms" || z.Attribute("EFFECT").Value == "Warm front" ||
                        z.Attribute("EFFECT").Value == "End victory 1" || z.Attribute("EFFECT").Value == "End victory 2" ||
                        z.Attribute("EFFECT").Value == "Remove zone 1" || z.Attribute("EFFECT").Value == "Remove zone 2" ||
                        z.Attribute("EFFECT").Value == "Use chemicals 1" || z.Attribute("EFFECT").Value == "Use chemicals 2")
                    {
                        if (z.Attribute("VALUE") != null) z.Attribute("VALUE").Remove();
                    }
                }
                //TRIGGER GROUP 5 != NULL
                else if (z.Attribute("TRIGGER").Value == "Turn" && z.Attribute("TURN").Value != null) //T5
                {
                    if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();

                    //REMOVE X, Y ATTRIBUTES FOR ANYTHING BUT EFFECT GROUPS 3-6
                    if (z.Attribute("EFFECT").Value != "Nuclear Attack" || z.Attribute("EFFECT").Value != "Form'n orders" ||
                        z.Attribute("EFFECT").Value != "Suppply Point 1" || z.Attribute("EFFECT").Value != "Supply Point 2" ||
                        z.Attribute("EFFECT").Value != "Set ownership 1" || z.Attribute("EFFECT").Value != "Set ownership 2" ||
                        z.Attribute("EFFECT").Value != "Refugee 1" || z.Attribute("EFFECT").Value != "Refugee 2")
                    {
                        if (z.Attribute("X") != null) z.Attribute("X").Remove();
                        if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
                    }

                    //REMOVE VALUE ATTRIBUTE FOR EFFECTS GROUPS 2 & 4
                    if (z.Attribute("EFFECT").Value == "Set ownership 1" || z.Attribute("EFFECT").Value == "Set ownership 2" ||
                        z.Attribute("EFFECT").Value == "Cease Fire" || z.Attribute("EFFECT").Value == "Cool front" ||
                        z.Attribute("EFFECT").Value == "End normal" || z.Attribute("EFFECT").Value == "News only" ||
                        z.Attribute("EFFECT").Value == "Open fire" || z.Attribute("EFFECT").Value == "Storms" || z.Attribute("EFFECT").Value == "Warm front" ||
                        z.Attribute("EFFECT").Value == "End victory 1" || z.Attribute("EFFECT").Value == "End victory 2" ||
                        z.Attribute("EFFECT").Value == "Remove zone 1" || z.Attribute("EFFECT").Value == "Remove zone 2" ||
                        z.Attribute("EFFECT").Value == "Use chemicals 1" || z.Attribute("EFFECT").Value == "Use chemicals 2")
                    {
                        if (z.Attribute("VALUE") != null) z.Attribute("VALUE").Remove();
                    }
                }

                //TRIGGER GROUP 4
                else if (z.Attribute("TRIGGER").Value == "1 uses chemical" || z.Attribute("TRIGGER").Value == "2 uses chemical" ||
                        z.Attribute("TRIGGER").Value == "1 uses nuclear" || z.Attribute("TRIGGER").Value == "2 uses nuclear")
                {
                    if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();

                    //REMOVE X, Y ATTRIBUTES FOR ANYTHING BUT EFFECT GROUPS 3-6
                    if (z.Attribute("EFFECT").Value != "Nuclear Attack" && z.Attribute("EFFECT").Value != "Form'n orders" &&
                        z.Attribute("EFFECT").Value != "Suppply Point 1" && z.Attribute("EFFECT").Value != "Supply Point 2" &&
                        z.Attribute("EFFECT").Value != "Set ownership 1" && z.Attribute("EFFECT").Value != "Set ownership 2" &&
                        z.Attribute("EFFECT").Value != "Refugee 1" && z.Attribute("EFFECT").Value != "Refugee 2")
                    {
                        if (z.Attribute("X") != null) z.Attribute("X").Remove();
                        if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
                    }

                    //REMOVE VALUE ATTRIBUTE FOR EFFECTS GROUPS 2 & 4
                    if (z.Attribute("EFFECT").Value == "Set ownership 1" || z.Attribute("EFFECT").Value == "Set ownership 2" ||
                        z.Attribute("EFFECT").Value == "Cease Fire" || z.Attribute("EFFECT").Value == "Cool front" ||
                        z.Attribute("EFFECT").Value == "End normal" || z.Attribute("EFFECT").Value == "News only" ||
                        z.Attribute("EFFECT").Value == "Open fire" || z.Attribute("EFFECT").Value == "Storms" || z.Attribute("EFFECT").Value == "Warm front" ||
                        z.Attribute("EFFECT").Value == "End victory 1" || z.Attribute("EFFECT").Value == "End victory 2" ||
                        z.Attribute("EFFECT").Value == "Remove zone 1" || z.Attribute("EFFECT").Value == "Remove zone 2" ||
                        z.Attribute("EFFECT").Value == "Use chemicals 1" || z.Attribute("EFFECT").Value == "Use chemicals 2")
                    {
                        if (z.Attribute("VALUE") != null) z.Attribute("VALUE").Remove();
                    }
                }
                //TRIGGER GROUP 3
                else if (z.Attribute("TRIGGER").Value == "Event activated" || z.Attribute("TRIGGER").Value == "Event cancelled")
                {
                    //REMOVE X, Y ATTRIBUTES FOR ANYTHING BUT EFFECT GROUPS 3-6
                    if (z.Attribute("EFFECT").Value != "Nuclear Attack" && z.Attribute("EFFECT").Value != "Form'n orders" &&
                        z.Attribute("EFFECT").Value != "Suppply Point 1" && z.Attribute("EFFECT").Value != "Supply Point 2" &&
                        z.Attribute("EFFECT").Value != "Set ownership 1" && z.Attribute("EFFECT").Value != "Set ownership 2" &&
                        z.Attribute("EFFECT").Value != "Refugee 1" && z.Attribute("EFFECT").Value != "Refugee 2")
                    {
                        if (z.Attribute("X") != null) z.Attribute("X").Remove();
                        if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
                    }

                    //REMOVE VALUE ATTRIBUTE FOR EFFECTS GROUPS 2 & 4
                    if (z.Attribute("EFFECT").Value == "Set ownership 1" || z.Attribute("EFFECT").Value == "Set ownership 2" ||
                        z.Attribute("EFFECT").Value == "Cease Fire" || z.Attribute("EFFECT").Value == "Cool front" ||
                        z.Attribute("EFFECT").Value == "End normal" || z.Attribute("EFFECT").Value == "News only" ||
                        z.Attribute("EFFECT").Value == "Open fire" || z.Attribute("EFFECT").Value == "Storms" || z.Attribute("EFFECT").Value == "Warm front" ||
                        z.Attribute("EFFECT").Value == "End victory 1" || z.Attribute("EFFECT").Value == "End victory 2" ||
                        z.Attribute("EFFECT").Value == "Remove zone 1" || z.Attribute("EFFECT").Value == "Remove zone 2" ||
                        z.Attribute("EFFECT").Value == "Use chemicals 1" || z.Attribute("EFFECT").Value == "Use chemicals 2")
                    {
                        if (z.Attribute("VALUE") != null) z.Attribute("VALUE").Remove();
                    }
                }
                //TRIGGER GROUP 2
                else if (z.Attribute("TRIGGER").Value == "Force 1 winning" || z.Attribute("TRIGGER").Value == "Force 2 winning" ||
                        z.Attribute("TRIGGER").Value == "Variable value" || z.Attribute("TRIGGER").Value == "Unit destroyed")
                {
                    if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();
                    if (z.Attribute("X") != null) z.Attribute("X").Remove();
                    if (z.Attribute("Y") != null) z.Attribute("Y").Remove();
                }

                //TRIGGER GROUP 1
                else if (z.Attribute("TRIGGER").Value == "1 occupies" || z.Attribute("TRIGGER").Value == "2 occupies" ||
                            z.Attribute("TRIGGER").Value == "1 attacks" || z.Attribute("TRIGGER").Value == "2 attacks")
                {
                    if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Remove();
                }
            }  //NEW FOREACH END

            btnEvents.Enabled = true;
            xelem.Save(FilePath);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            TOAWEditSave.Properties.Settings.Default.City = txtCity.Text;
            TOAWEditSave.Properties.Settings.Default.Town = txtTown.Text;
            TOAWEditSave.Properties.Settings.Default.Village = txtVillage.Text;
            TOAWEditSave.Properties.Settings.Default.BM1 = txtBM1.Text;
            TOAWEditSave.Properties.Settings.Default.BM2 = txtBM2.Text;
            TOAWEditSave.Properties.Settings.Default.BM3 = txtBM3.Text;
            TOAWEditSave.Properties.Settings.Default.BM4 = txtBM4.Text;
            TOAWEditSave.Properties.Settings.Default.BM5 = txtBM5.Text;
            TOAWEditSave.Properties.Settings.Default.BM6 = txtBM6.Text;
            TOAWEditSave.Properties.Settings.Default.BM7 = txtBM7.Text;
            TOAWEditSave.Properties.Settings.Default.BM8 = txtBM8.Text;
            TOAWEditSave.Properties.Settings.Default.BM9 = txtBM9.Text;

            TOAWEditSave.Properties.Settings.Default.RPCity = txtRPCity.Text;
            TOAWEditSave.Properties.Settings.Default.RPTown = txtRPTown.Text;
            TOAWEditSave.Properties.Settings.Default.RPVill = txtRPVillage.Text;
            TOAWEditSave.Properties.Settings.Default.RPBM1 = txtRPBM1.Text;
            TOAWEditSave.Properties.Settings.Default.RPBM2 = txtRPBM2.Text;
            TOAWEditSave.Properties.Settings.Default.RPBM3 = txtRPBM3.Text;
            TOAWEditSave.Properties.Settings.Default.RPBM4 = txtRPBM4.Text;
            TOAWEditSave.Properties.Settings.Default.RPBM5 = txtRPBM5.Text;
            TOAWEditSave.Properties.Settings.Default.RPBM6 = txtRPBM6.Text;
            TOAWEditSave.Properties.Settings.Default.RPBM7 = txtRPBM7.Text;
            TOAWEditSave.Properties.Settings.Default.RPBM8 = txtRPBM8.Text;
            TOAWEditSave.Properties.Settings.Default.RPBM9 = txtRPBM9.Text;

            TOAWEditSave.Properties.Settings.Default.Save();
        }
       
        private async void btnCalcRP_Click(object sender, EventArgs e)
        {
            progressSavedGame.Visible = true;
            await Task.Delay(25);

            string FilePath = txtSelectedGamFile.Text;
            XElement xelem = XElement.Load(FilePath);

            //GET PLACES
            var places =xelem.Descendants("MAP").Descendants("PLACE");
            var hexLoc = xelem.Descendants("MAP").Descendants("CELL");
            string placeLoc = "";
            int totalplaces = places.Count();
            int f1Cities = 0;
            int f2Cities = 0;
            int f1Towns = 0;
            int f2Towns = 0;
            int f1Vill = 0;
            int f2Vill = 0;
            int f1BM1 = 0;
            int f2BM1 = 0;
            int f1BM2 = 0;
            int f2BM2 = 0;
            int f1BM3 = 0;
            int f2BM3 = 0;
            int f1BM4 = 0;
            int f2BM4 = 0;
            int f1BM5 = 0;
            int f2BM5 = 0;
            int f1BM6 = 0;
            int f2BM6 = 0;
            int f1BM7 = 0;
            int f2BM7 = 0;
            int f1BM8 = 0;
            int f2BM8 = 0;
            int f1BM9 = 0;
            int f2BM9 = 0;

            foreach (XElement place in places)
            {
                await Task.Delay(5);
                //FIND CELL FOR PLACE
                placeLoc = place.Attribute("loc").Value;
                string xpathPlaceLoc = "MAP/CELL[@loc ='" + placeLoc + "']";
                var cell = xelem.XPathSelectElement(xpathPlaceLoc);
                bool boolVillage = false;
                bool boolTown = false;
                bool boolCity = false;
                bool boolF1Possess = false;

                //GET MAP XML DATA
                string cellData = cell.Attribute("b").Value;
                int possessindex = GetNthIndex(cellData,'/', 40);
                int possessindexb = GetNthIndex(cellData, '/', 41);
                int villageindex = GetNthIndex(cellData, '/', 15);
                int villageindexb = GetNthIndex(cellData, '/', 16);
                int villageruinindex = GetNthIndex(cellData, '/', 17);
                int villageruinindexb = GetNthIndex(cellData, '/', 18);
                int cityindex = GetNthIndex(cellData, '/', 16);
                int cityruinindex = GetNthIndex(cellData, '/', 18);
                string village = cellData.Substring(villageindex + 1, (villageindexb - villageindex)-1);
                string villageruin = cellData.Substring(villageruinindex + 1, (villageruinindexb - villageruinindex)-1);
                string city = cellData.Substring(cityindex + 1, 1);
                string cityruin = cellData.Substring(cityruinindex + 1, 1);

                //DETERMINE PLACE'S POSSESSION
                string possess = cellData.Substring(possessindex + 1, (possessindexb - possessindex) - 1);
                string name = place.Attribute("name").Value;
                if (possess == "0" || possess == "2" || possess == "4" || possess == "8" || possess == "16" || possess == "32" || possess == "64" || possess == "128") boolF1Possess = true;

                //DETERMINE IF PLACE IS VILLAGE OR TOWN
                if (village != "0" || villageruin != "0")
                {
                    if (village == "63" || villageruin == "63")
                    {
                        boolTown = true;
                    }
                    else
                    {
                        boolVillage = true;
                    }
                }

                //DETERMINE IF PLACE IS CITY
                if (city != "0" || cityruin != "0") boolCity = true;
             
                //COUNT EACH TYPE OF PLACE, BY POSSESSION               
                if (boolCity == true)  //IF CITY
                {
                    if (boolF1Possess == true)
                    {
                        f1Cities++;
                    }
                    else
                    {
                        f2Cities++;
                    }
                }
                else if (boolVillage == true) //IF VILLAGE
                {
                    if (boolF1Possess == true)
                    {
                        f1Vill++;
                    }
                    else
                    {
                        f2Vill++;
                    }
                }
                else if (boolTown == true) //IF TOWN
                {
                    if (boolF1Possess == true)
                    {
                        f1Towns++;
                    }
                    else
                    {
                        f2Towns++;
                    }
                }
                else if (place.Attribute("name").Value.Contains("<1")) //IF BITMAP 1, AND SO ON...
                {
                    if (boolF1Possess == true)
                    {
                        f1BM1++;
                    }
                    else
                    {
                        f2BM1++;
                    }
                }
                else if (place.Attribute("name").Value.Contains("<2"))
                {
                    if (boolF1Possess == true)
                    {
                        f1BM2++;
                    }
                    else
                    {
                        f2BM2++;
                    }
                }
                else if (place.Attribute("name").Value.Contains("<3"))
                {
                    if (boolF1Possess == true)
                    {
                        f1BM3++;
                    }
                    else
                    {
                        f2BM3++;
                    }
                }
                else if (place.Attribute("name").Value.Contains("<4"))
                {
                    if (boolF1Possess == true)
                    {
                        f1BM4++;
                    }
                    else
                    {
                        f2BM4++;
                    }
                }
                else if (place.Attribute("name").Value.Contains("<5"))
                {
                    if (boolF1Possess == true)
                    {
                        f1BM5++;
                    }
                    else
                    {
                        f2BM5++;
                    }
                }
                else if (place.Attribute("name").Value.Contains("<6"))
                {
                    if (boolF1Possess == true)
                    {
                        f1BM6++;
                    }
                    else
                    {
                        f2BM6++;
                    }
                }
                else if (place.Attribute("name").Value.Contains("<7"))
                {
                    if (boolF1Possess == true)
                    {
                        f1BM7++;
                    }
                    else
                    {
                        f2BM7++;
                    }
                }
                else if (place.Attribute("name").Value.Contains("<8"))
                {
                    if (boolF1Possess == true)
                    {
                        f1BM8++;
                    }
                    else
                    {
                        f2BM8++;
                    }
                }
                else if (place.Attribute("name").Value.Contains("<9"))
                {
                    if (boolF1Possess == true)
                    {
                        f1BM9++;
                    }
                    else
                    {
                        f2BM9++;
                    }
                }
            }
            //CALCULATE AND DISPLAY RPs BY QTY * RP VALUE
            txtF1QtyCity.Text = f1Cities.ToString();
            txtF2QtyCity.Text = f2Cities.ToString();
            txtF1QtyTown.Text = f1Towns.ToString();
            txtF2QtyTown.Text = f2Towns.ToString();
            txtF1QtyVill.Text = f1Vill.ToString();
            txtF2QtyVill.Text = f2Vill.ToString();
            txtF1BM1.Text = f1BM1.ToString();
            txtF2QtyBM1.Text = f2BM1.ToString();
            txtF1QtyBM2.Text = f1BM2.ToString();
            txtF2QtyBM2.Text = f2BM2.ToString();
            txtF1QtyBM3.Text = f1BM3.ToString();
            txtF2QtyBM3.Text = f2BM3.ToString();
            txtF1QtyBM4.Text = f1BM4.ToString();
            txtF2QtyBM4.Text = f2BM4.ToString();
            txtF1QtyBM5.Text = f1BM5.ToString();
            txtF2QtyBM5.Text = f2BM5.ToString();
            txtF1QtyBM6.Text = f1BM6.ToString();
            txtF2QtyBM6.Text = f2BM6.ToString();
            txtF1QtyBM7.Text = f1BM7.ToString();
            txtF2QtyBM7.Text = f2BM7.ToString();
            txtF1QtyBM8.Text = f1BM8.ToString();
            txtF2QtyBM8.Text = f2BM8.ToString();
            txtF1QtyBM9.Text = f1BM9.ToString();
            txtF2QtyBM9.Text = f2BM9.ToString();
            txtF1RPCity.Text = (f1Cities * Int32.Parse(txtRPCity.Text)).ToString();
            txtF2RPCity.Text = (f2Cities * Int32.Parse(txtRPCity.Text)).ToString();
            txtF1RPTown.Text = (f1Towns * Int32.Parse(txtRPTown.Text)).ToString();
            txtF2RPTown.Text = (f2Towns * Int32.Parse(txtRPTown.Text)).ToString();
            txtF1RPVill.Text = (f1Vill * Int32.Parse(txtRPVillage.Text)).ToString();
            txtF2RPVill.Text = (f2Vill * Int32.Parse(txtRPVillage.Text)).ToString();
            txtF1RPBM1.Text = (f1BM1 * Int32.Parse(txtRPBM1.Text)).ToString();
            txtF2RPBM1.Text = (f2BM1 * Int32.Parse(txtRPBM1.Text)).ToString();
            txtF1RPBM2.Text = (f1BM2 * Int32.Parse(txtRPBM2.Text)).ToString();
            txtF2RPBM2.Text = (f2BM2 * Int32.Parse(txtRPBM2.Text)).ToString();
            txtF1RPBM3.Text = (f1BM3 * Int32.Parse(txtRPBM3.Text)).ToString();
            txtF2RPBM3.Text = (f2BM3 * Int32.Parse(txtRPBM3.Text)).ToString();
            txtF1RPBM4.Text = (f1BM4 * Int32.Parse(txtRPBM4.Text)).ToString();
            txtF2RPBM4.Text = (f2BM4 * Int32.Parse(txtRPBM4.Text)).ToString();
            txtF1RPBM5.Text = (f1BM5 * Int32.Parse(txtRPBM5.Text)).ToString();
            txtF2RPBM5.Text = (f2BM5 * Int32.Parse(txtRPBM5.Text)).ToString();
            txtF1RPBM6.Text = (f1BM6 * Int32.Parse(txtRPBM6.Text)).ToString();
            txtF2RPBM6.Text = (f2BM6 * Int32.Parse(txtRPBM6.Text)).ToString();
            txtF1RPBM7.Text = (f1BM7 * Int32.Parse(txtRPBM7.Text)).ToString();
            txtF2RPBM7.Text = (f2BM7 * Int32.Parse(txtRPBM7.Text)).ToString();
            txtF1RPBM8.Text = (f1BM8 * Int32.Parse(txtRPBM8.Text)).ToString();
            txtF2RPBM8.Text = (f2BM8 * Int32.Parse(txtRPBM8.Text)).ToString();
            txtF1RPBM9.Text = (f1BM9 * Int32.Parse(txtRPBM9.Text)).ToString();
            txtF2RPBM9.Text = (f2BM9 * Int32.Parse(txtRPBM9.Text)).ToString();

            //CALCULATE TOTAL RPs PER FORCE
            //FORCE 1 RPs
            int f1currentRP = Int32.Parse(txtF1RPCity.Text) + Int32.Parse(txtF1RPTown.Text) + Int32.Parse(txtF1RPVill.Text) +
                Int32.Parse(txtF1RPBM1.Text) + Int32.Parse(txtF1RPBM2.Text) + Int32.Parse(txtF1RPBM3.Text) + Int32.Parse(txtF1RPBM4.Text) +
                Int32.Parse(txtF1RPBM5.Text) + Int32.Parse(txtF1RPBM6.Text) + Int32.Parse(txtF1RPBM8.Text) + Int32.Parse(txtF1RPBM7.Text) +
                Int32.Parse(txtF1RPBM8.Text) + Int32.Parse(txtF1RPBM9.Text);
           
            int f1initialRP = Int32.Parse(txtF1InitRP.Text);
            int f1adjustRP = Int32.Parse(txtF1AdjustRP.Text);
            int f1totalRP = f1initialRP + f1currentRP + f1adjustRP;

            string f1initialrp = string.Format("{0:#,#}", f1initialRP);
            string f1currentrp = string.Format("{0:#,#}", f1currentRP);
            string f1adjustrp = string.Format("{0:#,#}", f1adjustRP);
            string f1totalrp = string.Format("{0:#,#}", f1totalRP);

            txtF1CurrentRP.Text = f1currentrp;
            txtF1TotalRP.Text = f1totalrp;

            //FORCE 2 RPs
            int f2currentRP = Int32.Parse(txtF2RPCity.Text) + Int32.Parse(txtF2RPTown.Text) + Int32.Parse(txtF2RPVill.Text) +
                Int32.Parse(txtF2RPBM1.Text) + Int32.Parse(txtF2RPBM2.Text) + Int32.Parse(txtF2RPBM3.Text) + Int32.Parse(txtF2RPBM4.Text) +
                Int32.Parse(txtF2RPBM5.Text) + Int32.Parse(txtF2RPBM6.Text) + Int32.Parse(txtF2RPBM8.Text) + Int32.Parse(txtF2RPBM7.Text) +
                Int32.Parse(txtF2RPBM8.Text) + Int32.Parse(txtF2RPBM9.Text);

            int f2initialRP = Int32.Parse(txtF2InitRP.Text);
            int f2adjustRP = Int32.Parse(txtF2AdjustRP.Text);
            int f2totalRP = f2initialRP + f2currentRP + f2adjustRP;

            string f2initialrp = string.Format("{0:#,#}", f2initialRP);
            string f2currentrp = string.Format("{0:#,#}", f2currentRP);
            string f2adjustrp = string.Format("{0:#,#}", f2adjustRP);
            string f2totalrp = string.Format("{0:#,#}", f2totalRP);

            txtF2CurrentRP.Text = f2currentrp;
            txtF2TotalRP.Text = f2totalrp;

            await Task.Delay(100);
            progressSavedGame.Visible = false;
        }

        private int GetNthIndex(string s, char t, int n)
        {
            //GETS INDEX OF nTH REPETITION OF t WITHIN S
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        private void tsRename_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                   TextBox textbox = sourceControl as TextBox;
                    textbox.ReadOnly = false;
                }
            }
        }

        private void txtCity_MouseLeave(object sender, EventArgs e)
        {
            txtCity.ReadOnly = true;
        }

        private void txtTown_MouseLeave(object sender, EventArgs e)
        {
            txtTown.ReadOnly = true;
        }

        private void txtVillage_MouseLeave(object sender, EventArgs e)
        {
            txtVillage.ReadOnly = true;
        }

        private void txtBM1_MouseLeave(object sender, EventArgs e)
        {
            txtBM1.ReadOnly = true;
        }

        private void txtBM2_MouseLeave(object sender, EventArgs e)
        {
            txtBM2.ReadOnly = true;
        }

        private void txtBM3_MouseLeave(object sender, EventArgs e)
        {
            txtBM3.ReadOnly = true;
        }

        private void txtBM4_MouseLeave(object sender, EventArgs e)
        {
            txtBM4.ReadOnly = true;
        }

        private void txtBM5_MouseLeave(object sender, EventArgs e)
        {
            txtBM5.ReadOnly = true;
        }

        private void txtBM6_MouseLeave(object sender, EventArgs e)
        {
            txtBM6.ReadOnly = true;
        }

        private void txtBM7_MouseLeave(object sender, EventArgs e)
        {
            txtBM7.ReadOnly = true;
        }

        private void txtBM8_MouseLeave(object sender, EventArgs e)
        {
            txtBM8.ReadOnly = true;
        }

        private void txtBM9_MouseLeave(object sender, EventArgs e)
        {
            txtBM9.ReadOnly = true;
        }

        private async void btnEvents_Click(object sender, EventArgs e)
        {
            progressSavedGame.Visible = true;
            string logFile = txtSelectedLogFile.Text;
            //string turn = txtTurn.Text;

            List<string> eventList = new List<string>(File.ReadLines(logFile).ToList());
            List<string> triggeredEvents = new List<string>();

            foreach (var line in eventList)   //LOOP THROUGH ALL LINES IN LOG FILE TO DETECT EVENT NEWS AND ELAPSED TURNS
            {
                foreach (DataGridViewRow r in dgvEvents.Rows)
                {
                    string trigEvent = "";
                    string precEvent = "";

                    if (r.Cells[9].Value.ToString() == line) //IF EVENT HAS BEEN ANNOUNCED IN NEWS IN LOG FILE
                    {
                        //SET ROW COLOR AND CHECKBOX
                        r.Cells[0].Value = "true";
                        r.DefaultCellStyle.BackColor = Color.Red;

                        //GET EVENT IDS FOR ALL ANNOUNCED EVENTS
                        trigEvent = r.Cells[1].Value.ToString();
                        triggeredEvents.Add(trigEvent);

                        //GET EVENT IDs FOR EVENTS WHICH TRIGGERED THIS EVENT, TO DETECT PRECEDENT AND ALTERNATE EVENTS (IE, EVENTS WHICH TRIGGERED 
                        //THIS EVENT OR ARE ALTERNATE EVENTS BASED ON % PROBABILITY
                        if (r.Cells[2].Value.ToString() == "Event activated" || r.Cells[2].Value.ToString() == "Event cancelled")
                        {
                            precEvent = r.Cells[4].Value.ToString();
                            triggeredEvents.Add(precEvent);
                        }
                    }
                    //}
                    int eventturn;
                    //CHECK FOR EVENTS TRIGGERED BY PAST TURNS
                    //If EVENT WAS TRIGGERED ON TURN 1
                    if (r.Cells[2].Value.ToString() == "Turn")
                    {
                        //int currentturn = Int32.Parse(turn);
                        int currentturn = Int32.Parse(numTurn.Value.ToString());

                        if (r.Cells[3].Value.ToString() != "--")
                        {
                            eventturn = Int32.Parse(r.Cells[3].Value.ToString());
                        }
                        else
                        {
                            eventturn = 1;
                        }

                        //IF EVENT WAS TRIGGERED AFTER TURN 1
                        if (currentturn >= eventturn)
                        {
                            //IF THERE IS TURN RANGE, MAY NEED TO EVALUATE MANUALLY
                            if (r.Cells[8].Value.ToString() == "--" || currentturn >= eventturn + Int32.Parse(r.Cells[8].Value.ToString()) || 
                                Convert.ToBoolean(((DataGridViewCheckBoxCell)r.Cells[0]).Value) == true)
                            {
                                r.Cells[0].Value = "true";
                                r.DefaultCellStyle.BackColor = Color.Red;
                            }
                            else
                            {
                                r.Cells[0].Value = "false";
                                r.DefaultCellStyle.BackColor = Color.Yellow;
                            }
                        }
                    }
                }
            }  //NEW BRACKET FOR LOG FOREACH LOOP

               //LOOP BACK THROUGH DGV TO CHECK FOR PRECEDENT & ALTERNATE EVENTS
            List<string> triggeredEventsSecond = new List<string>();

            foreach (DataGridViewRow r2 in dgvEvents.Rows)
            {
                if (r2.Cells[0].Value == null)
                {
                    string eventID = "";

                    foreach (string s in triggeredEvents) //triggeredEVENTS IS LIST OF TRIGGERED EVENTS FROM NEWS FROM LOG FILE
                    {
                            //CHECK FOR ALTERNATE EVENTS (IE, CANCELLED VS ACTIVATED)  TRIGGER = 
                            if((r2.Cells[2].Value.ToString() == "Event activated" || r2.Cells[2].Value.ToString() == "Event cancelled") &&
                                //(r2.Cells[4].Value.ToString() == s || r2.Cells[6].Value.ToString() == s))
                                (r2.Cells[4].Value.ToString() == s))
                            {
                                if (r2.Cells[3].Value.ToString() == "--" && r2.Cells[8].Value.ToString() == "--")
                                {
                                    r2.Cells[0].Value = "true";
                                    r2.DefaultCellStyle.BackColor = Color.Red;
                                    eventID = r2.Cells[1].Value.ToString();
                                    triggeredEventsSecond.Add(eventID);
                                }
                                else
                                {
                                    r2.Cells[0].Value = "false";
                                    r2.DefaultCellStyle.BackColor = Color.Yellow;
                                }
                            }

                            //CHECK FOR PRECEDENT EVENTS (IE, EVENT WHICH TRIGGERED THIS EVENT)
                            if(r2.Cells[1].Value.ToString() == s)
                            {
                                //CODE FOR DELAYS AND TURN RANGE UNNECESSARY BECAUSE THESE EVENTS ALREADY HAPPENED (IE, TRIGGERED ANOTHER EVENT)
                                //if (r2.Cells[3].Value.ToString() == "--" && r2.Cells[8].Value.ToString() == "--")
                                //{
                                    r2.Cells[0].Value = "true";
                                    r2.DefaultCellStyle.BackColor = Color.Red;
                                    eventID = r2.Cells[1].Value.ToString();
                                    triggeredEventsSecond.Add(eventID);
                                //}
                                //else
                                //{
                                //    r2.Cells[0].Value = "false";
                                //    r2.DefaultCellStyle.BackColor = Color.Yellow;
                                //}
                            }
                    }
                }
            }
            //GET "SECONDARY" ACTIVATED EVENTS (IE, EVENTS ACTIVATED BY EVENTS ACTIVATED BY R2)
            foreach (DataGridViewRow r3 in dgvEvents.Rows)
            {
                if (r3.Cells[0].Value == null)
                {
                    foreach (string s2 in triggeredEventsSecond)
                    {
                        if ((r3.Cells[2].Value.ToString() == "Event activated" || r3.Cells[2].Value.ToString() == "Event cancelled") &&
                            (r3.Cells[4].Value.ToString() == s2))
                        {
                            if (r3.Cells[3].Value.ToString() == "--" && r3.Cells[8].Value.ToString() == "--")
                            {
                                r3.Cells[0].Value = "true";
                                r3.DefaultCellStyle.BackColor = Color.Red;
                            }
                            else
                            {
                                r3.Cells[0].Value = "false";
                                r3.DefaultCellStyle.BackColor = Color.Yellow;
                            }
                        }
                    }
                }
            }
            //}  //ORIGINAL BRACKET FOR LOG FOREACH LOOP
            btnDeleteEvents.Enabled = true;
            await Task.Delay(500);
            progressSavedGame.Visible = false;
        }

        private void dgvEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //CHANGE ROW COLOR IF CHECKED/UNCHECKED
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                dgvEvents.CommitEdit(DataGridViewDataErrorContexts.Commit);
                Color color = new Color();
                //Console.WriteLine("rowcolor: " + rowcolor);
                //Console.WriteLine("color: " + color);
                color = dgvEvents.CurrentRow.DefaultCellStyle.BackColor;
                //Check the value of cell
                if (dgvEvents.CurrentCell.GetType() == typeof(DataGridViewCheckBoxCell))
                {
                    if ((bool)dgvEvents.CurrentCell.Value == true)
                    {
                        //color = dgvEvents.CurrentRow.DefaultCellStyle.BackColor;
                        rowcolor = color;
                        //Console.WriteLine("color a: " + color);
                        //Console.WriteLine("rowcolor a: " + rowcolor);
                        dgvEvents.CurrentRow.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        //color = dgvEvents.CurrentRow.DefaultCellStyle.BackColor;
                        //Console.WriteLine("color b: " + color);
                        //Console.WriteLine("rowcolor b: " + rowcolor);
                        //if (rowcolor != null)
                        //{
                            dgvEvents.CurrentRow.DefaultCellStyle.BackColor = rowcolor;
                        //}
                        //else
                        //{
                        //    dgvEvents.CurrentRow.DefaultCellStyle.BackColor = Color.Empty;
                        //}
                    }
                }
            }
        }

        private void dgvEvents_SelectionChanged(object sender, EventArgs e)
        {
            dgvEvents.ClearSelection();
        }

        private void btnDeleteEvents_Click(object sender, EventArgs e)
        {
            string FilePath = txtSelectedGamFile.Text;
            string xpathEvents = "";
            string xpathEvent = "";
            bool match = false;

            //int currentturn = Int32.Parse(txtTurn.Text);
            int currentturn = Int32.Parse(numTurn.Value.ToString());

            string aircap1 = "0";
            string aircap2 = "0";
            string railcap1 = "0"; ;
            string railcap2 = "0";
            string seacap1 = "0";
            string seacap2 = "0";
            string recon1 = "0";
            string recon2 = "0";

            int n = 0;
            int Iaircap1 = Int32.Parse(aircap1);
            int Iaircap2 = Int32.Parse(aircap2);
            int Irailcap1 = Int32.Parse(railcap1);
            int Irailcap2 = Int32.Parse(railcap2);
            int Iseacap1 = Int32.Parse(seacap1);
            int Iseacap2 = Int32.Parse(seacap2);
            int Irecon1 = Int32.Parse(recon1);  
            int Irecon2 = Int32.Parse(recon2);

            XElement xelem = XElement.Load(FilePath);
            xpathEvents = "EVENTS/EVENT";
            xpathEvent = "EVENTS";
            var eventz = xelem.XPathSelectElements(xpathEvents);
            var eventy = xelem.XPathSelectElement(xpathEvent);
            var checkEvents = eventz.ToList();

            //DELETE ALL CHECKED ROWS FROM DGV AND ADJUST TRIGGER TURNS FOR UPCOMING EVENTS
            for (int i = dgvEvents.Rows.Count - 1; i >= 0; i--)
            {
                //REMOVE CHECKED EVENTS AND GET AIR, RAIL, SEA CAPS & THEATER RECON
                if ((bool)dgvEvents.Rows[i].Cells[0].FormattedValue)
                {
                    //GET AIR, RAIL, SEA CAP & THEATER RECON VALUES FROM ACTIVATED EVENTS
                    //if (dgvEvents.Rows[i].Cells[5].Value.ToString() == "Air transport 1")
                    //{
                    //    if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                    //    {
                    //        aircap1 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                    //    }
                    //    else
                    //    {
                    //        aircap1 = "0";
                    //    }
                    //}

                    //if (dgvEvents.Rows[i].Cells[5].Value.ToString() == "Air transport 2")
                    //{
                    //    if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                    //    {
                    //        aircap2 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                    //    }
                    //    else
                    //    {
                    //        aircap2 = "0";
                    //    }
                    //}
                    //if (dgvEvents.Rows[i].Cells[5].Value.ToString() == "Rail transport 1")
                    //{
                    //    if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                    //    {
                    //        railcap1 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                    //    }
                    //    else
                    //    {
                    //        railcap1 = "0";
                    //    }
                    //}
                    //if (dgvEvents.Rows[i].Cells[5].Value.ToString() == "Rail transport 2")
                    //{
                    //    if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                    //    {
                    //        railcap2 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                    //    }
                    //    else
                    //    {
                    //        railcap2 = "0";
                    //    }
                    //}
                    //if (dgvEvents.Rows[i].Cells[5].Value.ToString() == "Sea transport 1")
                    //{
                    //    if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                    //    {
                    //        seacap1 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                    //    }
                    //    else
                    //    {
                    //        seacap1 = "0";
                    //    }
                    //}
                    //if (dgvEvents.Rows[i].Cells[5].Value.ToString() == "Sea transport 2")
                    //{
                    //    if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                    //    {
                    //        seacap2 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                    //    }
                    //    else
                    //    {
                    //        seacap2 = "0";
                    //    }
                    //}

                    //if (dgvEvents.Rows[i].Cells[5].Value.ToString() == "Theater recon 1")
                    //{
                    //    if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                    //    {
                    //        recon1 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                    //    }
                    //    else
                    //    {
                    //        recon1 = "0";
                    //    }
                    //}
                    //if (dgvEvents.Rows[i].Cells[5].Value.ToString() == "Theater recon 2")
                    //{
                    //    if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                    //    {
                    //        recon2 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                    //    }
                    //    else
                    //    {
                    //        recon2 = "0";
                    //    }
                    //}


                    ///////////JULY 27
                    string eventeffect = dgvEvents.Rows[i].Cells[5].Value.ToString();

                    switch (eventeffect)
                    {
                        case "Air transport 1":
                            if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                            {
                                aircap1 = dgvEvents.Rows[i].Cells[6].Value.ToString();

                            }
                            else
                            {
                                aircap1 = "0";
                            }

                            n = Int32.Parse(aircap1);
                            Iaircap1 = Iaircap1 + n;
                            aircap1 = Iaircap1.ToString();
                            break;

                        case "Air transport 2":
                            if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                            {
                                aircap2 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                            }
                            else
                            {
                                aircap2 = "0";
                            }
                            n = Int32.Parse(aircap2);
                            Iaircap2 = Iaircap2 + n;
                            aircap2 = Iaircap2.ToString();
                            break;

                        case "Rail transport 1":
                            if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                            {
                                railcap1 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                            }
                            else
                            {
                                railcap1 = "0";
                            }
                            n = Int32.Parse(railcap1);
                            Irailcap1 = Irailcap1 + n;
                            railcap1 = Irailcap1.ToString();
                            break;

                        case "Rail transport 2":
                            if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                            {
                                railcap2 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                            }
                            else
                            {
                                railcap2 = "0";
                            }
                            n = Int32.Parse(railcap2);
                            Irailcap2 = Irailcap2 + n;
                            railcap2 = Irailcap2.ToString();
                            break;

                        case "Sea transport 1":
                            if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                            {
                                seacap1 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                            }
                            else
                            {
                                seacap1 = "0";
                            }
                            n = Int32.Parse(seacap1);
                            Iseacap1 = Iseacap1 + n;
                            seacap1 = Iseacap1.ToString();
                            break;

                        case "Sea transport 2":
                            if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                            {
                                seacap2 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                            }
                            else
                            {
                                seacap2 = "0";
                            }
                            n = Int32.Parse(seacap2);
                            Iseacap2 = Iseacap2 + n;
                            seacap2 = Iseacap2.ToString();
                            break;

                        case "Theater recon 1":
                            if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                            {
                                recon1 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                            }
                            else
                            {
                                recon1 = "0";
                            }
                            n = Int32.Parse(recon1);
                            Irecon1 = Irecon1 + n;
                            recon1 = Irecon1.ToString();

                            //Console.WriteLine ("recon1 = " + n);
                            //Console.WriteLine("RECON1 = " + Irecon1);
                            //Console.WriteLine("startHour = " + cboStartHour.SelectedValue.ToString());
                            break;

                        case "Theater recon 2":
                            if (dgvEvents.Rows[i].Cells[6].Value.ToString() != "--")
                            {
                                recon2 = dgvEvents.Rows[i].Cells[6].Value.ToString();
                            }
                            else
                            {
                                recon2 = "0";
                            }
                            n = Int32.Parse(recon2);
                            Irecon2 = Irecon2 + n;
                            recon2 = Irecon2.ToString();
                            break;
                    }
                    /////////////////////////july 27

                    //DELETE CHECKED EVENTS
                    dgvEvents.Rows.RemoveAt(i);

                    //THIS BLOCK IS NEEDED TO DELETE EVENT IF IT IS IN LAST ROW!  NO IDEA 
                    //WHY IT WORKS, BUT IT DOES
                    if (i == dgvEvents.Rows.Count)
                    {
                        continue;
                    }
                }

                //RENUMBER TURNS
                if (dgvEvents.Rows[i].Cells[2].Value.ToString() == "Turn")
                {
                    int eventturn = Int32.Parse(dgvEvents.Rows[i].Cells[3].Value.ToString());
                    if (eventturn > currentturn)
                    {
                        dgvEvents.Rows[i].Cells[3].Value = (eventturn - currentturn).ToString();
                    }
                }
            }
            dgvEvents.Refresh();
            DataTable dt = (DataTable)dgvEvents.DataSource;

            //NOW LOOP THROUGH XML EVENTS, DELETING ACTIVATED EVENTS, ADJUST TURN TRIGGERS FOR UPCOMING EVENTS, ADD EVENTS FOR AIR/RAIL/SEA CAP & RECON
            for (int i = checkEvents.Count - 1; i > -1; i--)  
            {
                string id = "";
                match = false;
                eventz.First();
                string adjustedturn = "";

                //MATCH ALL EVENTS IN XML VS CHECKED EVENTS IN DGV
                foreach (DataRow row in dt.Rows)
                { 
                    id = row["ID"].ToString();
                    adjustedturn = row["Turn"].ToString();

                    //IF EVENT ROW IS UNCHECKED
                    if (id == checkEvents[i].Attribute("ID").Value.ToString())
                    {
                        match = true;

                        //IF TRIGGER IS TURN, ADJUST TURN FOR REMAINING EVENTS
                        if (checkEvents[i].Attribute("TRIGGER").Value.ToString() == "Turn")
                            {
                            if (checkEvents[i].Attribute("TURN") != null)
                                {
                                 checkEvents[i].Attribute("TURN").Value = (Int32.Parse(adjustedturn) - 1).ToString();
                                }
                            }
                        continue;
                    }
                }

                //IF EVENT ROW IS CHECKED
                if (match == false)
                {
                    checkEvents[i].Remove();  
                }
            }

            xelem.Descendants("EVENT");

            //ADD AIR,RAIL,SEA CAP & THEATER RECON EVENTS TO DATATABLE
            int firstrow = 0;

            DataRow dtr = dt.NewRow();

            dtr[0] = "8";
            dtr[1] = "Turn";
            dtr[2] = "1";
            dtr[3] = "--";
            dtr[4] = "Theater recon 1";
            dtr[5] = recon1;
            dtr[6] = 100;
            dtr[7] = "--";
            dtr[8] = "--";
            dt.Rows.InsertAt(dtr, firstrow);

            dtr = dt.NewRow();
            dtr[0] = "7";
            dtr[1] = "Turn";
            dtr[2] = "1";
            dtr[3] = "--";
            dtr[4] = "Theater recon 2";
            dtr[5] = recon2;
            dtr[6] = 100;
            dtr[7] = "--";
            dtr[8] = "--";
            dt.Rows.InsertAt(dtr, firstrow);

            dtr = dt.NewRow();
            dtr[0] = "6";
            dtr[1] = "Turn";
            dtr[2] = "1";
            dtr[3] = "--";
            dtr[4] = "Air transport 2";
            dtr[5] = aircap2;
            dtr[6] = 100;
            dtr[7] = "--";
            dtr[8] = "--";
            dt.Rows.InsertAt(dtr, firstrow);

            dtr = dt.NewRow();
            dtr[0] = "5";
            dtr[1] = "Turn";
            dtr[2] = "1";
            dtr[3] = "--";
            dtr[4] = "Air transport 1";
            dtr[5] = aircap1;
            dtr[6] = "100";
            dtr[7] = "--";
            dtr[8] = "--";
            dt.Rows.InsertAt(dtr, firstrow);

            dtr = dt.NewRow();
            dtr[0] = "4";
            dtr[1] = "Turn";
            dtr[2] = "1";
            dtr[3] = "--";
            dtr[4] = "Sea transport 2";
            dtr[5] = seacap2;
            dtr[6] = "100";
            dtr[7] = "--";
            dtr[8] = "--";
            dt.Rows.InsertAt(dtr, firstrow);

            dtr = dt.NewRow();
            dtr[0] = "3";
            dtr[1] = "Turn";
            dtr[2] = "1";
            dtr[3] = "--";
            dtr[4] = "Sea transport 1";
            dtr[5] = seacap1;
            dtr[6] = "100";
            dtr[7] = "--";
            dtr[8] = "--";
            dt.Rows.InsertAt(dtr, firstrow);

            dtr = dt.NewRow();
            dtr[0] = "2";
            dtr[1] = "Turn";
            dtr[2] = "1";
            dtr[3] = "--";
            dtr[4] = "Rail transport 2";
            dtr[5] = railcap2;
            dtr[6] = "100";
            dtr[7] = "--";
            dtr[8] = "--";
            dt.Rows.InsertAt(dtr, firstrow);

            dtr = dt.NewRow();
            dtr[0] = "1";
            dtr[1] = "Turn";
            dtr[2] = "1";
            dtr[3] = "--";
            dtr[4] = "Rail transport 1";
            dtr[5] = railcap1;
            dtr[6] = "100";
            dtr[7] = "--";
            dtr[8] = "--";
            dt.Rows.InsertAt(dtr, firstrow);

            dgvEvents.DataSource = dt;
            dgvEvents.Refresh();

            //ADD TRANSPORT & RECON EVENTS TO XML

            eventy.AddFirst(new XElement("EVENT",
                            new XAttribute("ID", "8"),
                            new XAttribute("TRIGGER", "Turn"),
                            new XAttribute("EFFECT", "Theater recon 1"),
                            new XAttribute("VALUE", recon1),
                            new XAttribute("TURN", "1"))
              );

            eventy.AddFirst(new XElement("EVENT",
                            new XAttribute("ID", "7"),
                            new XAttribute("TRIGGER", "Turn"),
                            new XAttribute("EFFECT", "Theater recon 2"),
                            new XAttribute("VALUE", recon2),
                            new XAttribute("TURN", "1"))
              );

            eventy.AddFirst(new XElement("EVENT",
                             new XAttribute("ID", "6"),
                             new XAttribute("TRIGGER", "Turn"),
                             new XAttribute("EFFECT", "Air transport 2"),
                             new XAttribute("VALUE", aircap2),
                             new XAttribute("TURN", "1"))
               );

            eventy.AddFirst(new XElement("EVENT",
                             new XAttribute("ID", "5"),
                             new XAttribute("TRIGGER", "Turn"),
                             new XAttribute("EFFECT", "Air transport 1"),
                             new XAttribute("VALUE", aircap1),
                             new XAttribute("TURN", "1"))
                );

            eventy.AddFirst(new XElement("EVENT",
                             new XAttribute("ID", "4"),
                             new XAttribute("TRIGGER", "Turn"),
                             new XAttribute("EFFECT", "Sea transport 2"),
                             new XAttribute("VALUE", seacap2),
                             new XAttribute("TURN", "1"))
                );

            eventy.AddFirst(new XElement("EVENT",
                             new XAttribute("ID", "3"),
                             new XAttribute("TRIGGER", "Turn"),
                             new XAttribute("EFFECT", "Sea transport 1"),
                             new XAttribute("VALUE", seacap1),
                             new XAttribute("TURN", "1"))
                );

            eventy.AddFirst(new XElement("EVENT",
                             new XAttribute("ID", "2"),
                             new XAttribute("TRIGGER", "Turn"),
                             new XAttribute("EFFECT", "Rail transport 2"),
                             new XAttribute("VALUE", railcap2),
                             new XAttribute("TURN", "1"))
                );

            eventy.AddFirst(new XElement("EVENT",
                             new XAttribute("ID", "1"),
                             new XAttribute("TRIGGER", "Turn"),
                             new XAttribute("EFFECT", "Rail transport 1"),
                             new XAttribute("VALUE", railcap1),
                             new XAttribute("TURN", "1"))
                );

            //ADJUST EVENT IDs IN DATATABLE TO SEQUENTIAL IDs, ADJUST EVENT NUMBERS
            int j = 1;
            string origevent2 = "";
            string origevent3 = "";

            foreach (DataRow dtr2 in dt.Rows)
            {
                origevent2 = dtr2["ID"].ToString();
                string xpathevent = "EVENTS/EVENT[@ID= " + origevent2 + "]";
                var reviseevent = xelem.XPathSelectElement(xpathevent);

                foreach (DataRow dtr3 in dt.Rows)
                {
                    origevent3 = dtr3["ID"].ToString();
                    string xpathevent2 = "EVENTS/EVENT[@ID= " + origevent3 + "]";
                    var reviseevent2 = xelem.XPathSelectElement(xpathevent2);

                    //ADJUST EVENT NUMBERS FOR TRIGGERS
                    if (((dtr3["TRIGGER"].ToString() == "Event activated" || dtr3["TRIGGER"].ToString() == "Event cancelled"))
                            && dtr3["EVT"].ToString() == origevent2)
                    {
                        dtr3["EVT"] = j;

                        //ADJUST EVENT IN XML
                        if ((reviseevent2.Attribute("TRIGGER").Value == "Event activated" ||
                            reviseevent2.Attribute("TRIGGER").Value == "Event cancelled") && 
                            reviseevent2.Attribute("CONTINGENCY").Value == origevent2)
                        {
                            reviseevent2.Attribute("CONTINGENCY").Value = j.ToString();
                        }
                    }

                    //ADJUST EVENT NUMBERS FOR EFFECTS
                    if ((dtr3["EFFECT"].ToString() == "Activate event" ||
                        dtr3["EFFECT"].ToString() == "Enable event" ||
                        dtr3["EFFECT"].ToString() == "Cancel event" ||
                        dtr3["EFFECT"].ToString() == "Theater Option 1" ||
                        dtr3["EFFECT"].ToString() == "Theater Option 2") &&
                        dtr3["VAL"].ToString() == origevent2)
                    {
                        dtr3["VAL"] = j;

                        //ADJUST EVENT IN XML
                        if ((reviseevent2.Attribute("EFFECT").Value == "Activate event" ||
                           reviseevent2.Attribute("EFFECT").Value == "Enable event" ||
                           reviseevent2.Attribute("EFFECT").Value == "Cancel event" ||
                           reviseevent2.Attribute("EFFECT").Value == "Theater Option 1" ||
                           reviseevent2.Attribute("EFFECT").Value == "Theater Option 2") &&
                           reviseevent2.Attribute("VALUE").Value == (Int32.Parse(origevent2)-1).ToString())
                        {
                            reviseevent2.Attribute("VALUE").Value = j.ToString();
                        }
                    }
                }

                dtr2["ID"] = j;
                if (reviseevent != null)
                {
                    reviseevent.Attribute("ID").Value = j.ToString();
                }
                j++;
            }

            dgvEvents.DataSource = dt;
            dgvEvents.Refresh();

            xelem.Save(FilePath);
        }

        private void dgvEvents_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;

        }

        private void tpRP_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void numTurn_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
