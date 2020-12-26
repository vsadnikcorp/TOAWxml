﻿using System;
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

namespace TOAWEditSave
{

    public partial class frmEditSave : Form
    {
        public frmEditSave()
        {
            InitializeComponent();
        }

        private void frmEditSave_Load(object sender, EventArgs e)
        {
            //DISABLE CONTROLS
            btnSelectLogFile.Enabled = false;
            txtSelectedLogFile.Enabled = false;
            btnCurrentTurn.Enabled = false;
            btnCalcRP.Enabled = false;
            btnDeleteEvents.Enabled = false;

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

        private void btnSelectSavedGame_Click(object sender, EventArgs e)
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
            }
            else
            {
                Application.Exit();
                this.Close();
                return;
            }
            
            //OPEN XELEM
            XElement xelem = XElement.Load(FilePath);

            //GET NAMES OF FORCES AND ASSIGN TO CONTROLS
            var forcenames = xelem.Descendants("HEADER").First();
            string fn1 = "";
            string fn2 = "";
            
            //ADD FORCE NAMES TO CONTROLS
            fn1 = forcenames.Attribute("forceName1").Value.ToString();
            fn2 = forcenames.Attribute("forceName2").Value.ToString();

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

            //FIX VARIOUS DATA GLITCHES IN THE EVENT XML (IE, EVENTS HAVING DATA IN FIELDS THAT DON'T MATTER FOR THAT EVENT, ETC.)
            IEnumerable<XElement> zing = xelem.XPathSelectElements("EVENTS/EVENT");
            foreach (XElement z in zing)
            {
                if (z.Attribute("TRIGGER").Value == "Turn" && z.Attribute("TURN") == null)
                {
                    z.Add(new XAttribute("TURN", "1"));
                }
                else if (z.Attribute("TRIGGER").Value == "Turn" && z.Attribute("TURN").Value != null)
                {
                    z.Attribute("TURN").Value = (Int32.Parse(z.Attribute("TURN").Value) + 1).ToString();
                }
                else if (z.Attribute("TRIGGER").Value == "Event activated")
                {
                    if (z.Attribute("TURN") != null) z.Attribute("TURN").Value = "--";
                    if (z.Attribute("VARIABLE") != null) z.Attribute("VARIABLE").Remove();
                }
                else if (z.Attribute("TRIGGER").Value == "1 occupies" || z.Attribute("TRIGGER").Value == "2 occupies")
                {
                    if (z.Attribute("VALUE") != null) z.Attribute("VALUE").Value = "--";
                    if (z.Attribute("CONTINGENCY") != null) z.Attribute("CONTINGENCY").Value = "--";
                    if (z.Attribute("TURN") != null) z.Attribute("TURN").Value = "--";
                }

                if (z.Attribute("EFFECT").Value == "Activate event" || z.Attribute("EFFECT").Value == "Enable event" ||
                    z.Attribute("EFFECT").Value == "Theater Option 1" || z.Attribute("EFFECT").Value == "Theater Option 2")
                {
                    z.Attribute("VALUE").Value = (Int32.Parse(z.Attribute("VALUE").Value) + 1).ToString();
                }

                if (z.Attribute("VARIABLE") != null)
                {
                    z.Attribute("VARIABLE").Value = (Int32.Parse(z.Attribute("VARIABLE").Value) + 1).ToString();
                }
            }

            //CREATE EVENTS DATATABLE, LOAD DATAGRIDVIEW
            DataTable dt = new DataTable();

            //dt.Columns.Add("ID", typeof(string));
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
                              NEWS = (string)v.Attribute("NEWS") ?? "--",
                              VAL = (string)v.Attribute("VALUE") ?? "--",
                              TURN = (string)v.Attribute("TURN") ?? "--",
                              PROB = (string)v.Attribute("CHANCE") ?? "100",
                              EVT = (string)v.Attribute("CONTINGENCY") ?? "--",
                              RNG = (string)v.Attribute("VARIABLE") ?? "--"
                          });
           
            //LOAD EVENT DATAGRIDVIEW
            events.ToList().ForEach(i => dt.Rows.Add(i.ID, i.TRIGGER, i.TURN, i.EVT, i.EFFECT, i.VAL, i.PROB, i.RNG, i.NEWS));
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
        }

        private void btnSelectLogFile_Click(object sender, EventArgs e)
        {
            string LogFilePath;
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "toawlog.txt files *.txt|*.txt";

            if (file.ShowDialog() == DialogResult.OK)
            {
                LogFilePath = file.FileName;
                txtSelectedLogFile.Text = LogFilePath;
                string rightside = LogFilePath.Substring(LogFilePath.Length - 7, 7);
                string number = rightside.Substring(0, 3);
                int t = 0;
                Int32.TryParse(number, out t);
                txtTurn.Text = t.ToString();
                btnCurrentTurn.Enabled = true;
            }
            else
            {
                Application.Exit();
                this.Close();
                return;
            }
        }

        private void btnCurrentTurn_Click(object sender, EventArgs e)
        {
            string toawlogFile = txtSelectedLogFile.Text;
            string gamFile = txtSelectedGamFile.Text;

            List<string> turnList = new List<string>(File.ReadLines(toawlogFile).ToList());
            List<string> toawSaveLog = new List<string>();
            string turn="";
            string currentturn = "";

            //COPY RELEVANT LINES FROM TOAWLOG.TXT TO TOAWSAVELOG.TXT
            foreach (var line in turnList.Where(l => l.Contains("SetState") || l.Contains("event")))
            {
                if (line.Contains("SetState"))
                {
                    turn = line.Split('|', '|')[1];//BETTER TO USE SIDESTART?  TURNSTART ONLY USEFUL FOR FINDING EVENTS
                    currentturn = line.Substring(19);
                }
                else
                {
                    currentturn = line.Substring(10);
                }
                toawSaveLog.Add(currentturn);
            }

            string path = (txtSelectedLogFile.Text).Remove(txtSelectedLogFile.Text.Length - 11);
            string logpath = path + "TOAWSaveLog.txt";
            System.IO.File.WriteAllLines(logpath, toawSaveLog);

            //ADJUST TURN, DATE, SIDE SETTINGS 
            string turnnumber = Regex.Match(turn, @"\d+").Value;
            string force = "";
            int index = 0;
            txtTurn.Text = turnnumber;

            //STRIP FORCE NAME OUT OF CURRENTTURN
            index = currentturn.IndexOf("|");
            if (currentturn.Contains("TurnStart") || currentturn.Contains("SideStart"))
            {
                force = currentturn.Substring(10, index-10);

            }
            if (currentturn.Contains("TurnEnd") ||currentturn.Contains("SideEnd"))
            {
                force = currentturn.Substring(8, index-8);
            }

            //SET FORCE RADIOBUTTON
            if (force == rbForce1.Text)
            {
                rbForce1.Checked = true;
            }
            else
            {
                rbForce2.Checked = true;
            }

            //SET CURRENT DATE
            var date = currentturn.Substring(currentturn.LastIndexOf('|') + 1);
            string dateonly = new string(date.Where(c => (Char.IsDigit(c) || c == '/')).ToArray());
            txtDate.Text = dateonly.ToString();
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
       
        private void btnCalcRP_Click(object sender, EventArgs e)
        {
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

        private void btnEvents_Click(object sender, EventArgs e)
        {
            string logFile = txtSelectedLogFile.Text;
            string turn = txtTurn.Text;

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

                    //CHECK FOR EVENTS TRIGGERED BY PAST TURNS
                    if (r.Cells[2].Value.ToString() == "Turn")
                    {
                        int currentturn = Int32.Parse(turn);
                        int eventturn = Int32.Parse(r.Cells[3].Value.ToString());

                        if (currentturn >= eventturn)
                        {
                            r.Cells[0].Value = "true";
                            r.DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }

                //LOOP BACK THROUGH DGV TO CHECK FOR PRECEDENT & ALTERNATE EVENTS
                foreach (DataGridViewRow r2 in dgvEvents.Rows)
                {
                    if (r2.Cells[0].Value == null)
                    {
                        string eventID = "";

                        foreach (string s in triggeredEvents) //triggeredEVENTS IS LIST OF TRIGGERED EVENTS FROM NEWS FROM LOG FILE
                        {
                            //CHECK FOR ALTERNATE EVENTS (IE, CANCELLED VS ACTIVATED)  TRIGGER = 
                            if((r2.Cells[2].Value.ToString() == "Event activated" || r2.Cells[2].Value.ToString() == "Event cancelled") &&
                                (r2.Cells[4].Value.ToString() == s || r2.Cells[6].Value.ToString() == s))
                            {
                                r2.Cells[0].Value = "true";
                                r2.DefaultCellStyle.BackColor = Color.Red;
                                eventID = r2.Cells[1].Value.ToString();
                            }

                            //CHECK FOR PRECEDENT EVENTS (IE, EVENT WHICH TRIGGERED THIS EVENT)
                            if(r2.Cells[1].Value.ToString() == s)
                            {
                                r2.Cells[0].Value = "true";
                                r2.DefaultCellStyle.BackColor = Color.Red;
                            }
                        }
                    }
                }
            }
            btnDeleteEvents.Enabled = true;
        }

        private void dgvEvents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //CHANGE ROW COLOR IF CHECKED/UNCHECKED
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
                dgvEvents.CommitEdit(DataGridViewDataErrorContexts.Commit);

            //Check the value of cell
            if (dgvEvents.CurrentCell.GetType() == typeof(DataGridViewCheckBoxCell))
                {
                    if ((bool)dgvEvents.CurrentCell.Value == true)
                    {
                        dgvEvents.CurrentRow.DefaultCellStyle.BackColor = Color.Red;
                    }
                    else
                    {
                        dgvEvents.CurrentRow.DefaultCellStyle.BackColor = Color.White;
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
            //string id = "";
            bool match = false;

            XElement xelem = XElement.Load(FilePath);
            xpathEvents = "EVENTS/EVENT";

            var eventz = xelem.XPathSelectElements(xpathEvents);
            var checkEvents = eventz.ToList();

            //DELETE ALL CHECKED ROWS FROM DGV
            for (int i = dgvEvents.Rows.Count - 1; i >= 0; i--)
            {
                if ((bool)dgvEvents.Rows[i].Cells[0].FormattedValue)
                {
                    dgvEvents.Rows.RemoveAt(i);
                }
            }

            dgvEvents.Refresh();

            DataTable dt = (DataTable)dgvEvents.DataSource;

            for (int i = checkEvents.Count - 1; i > -1; i--)
            {
                string id = "";
                match = false;
                eventz.First();

                foreach (DataRow row in dt.Rows)
                {
                    id = row["ID"].ToString();
                    if (id == checkEvents[i].Attribute("ID").Value.ToString())
                    {
                        match = true;
                        continue;
                    }
                }
                
                if (match == false)
                {
                    checkEvents[i].Remove();
                }
            }

            //xelem.Save(FilePath);
        }
    }
}
