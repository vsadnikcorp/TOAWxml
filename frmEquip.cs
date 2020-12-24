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
using System.Configuration;

namespace TOAWXML
{
    public partial class frmEquip : Form
    {

        public frmEquip()
        {
            InitializeComponent();
        }

        private void frmEquip_Load(object sender, EventArgs e)
        {
            cboCategory.Enabled = true;
            cboCountry.Enabled = false;
            cboEquipType.Enabled = false;
            cboMovement.Enabled = false;
            cboWeapons.Enabled = false;
            cboDefensive.Enabled = false;
            cboEngineering.Enabled = false;
            cboNaval.Enabled = false;
            txtNumber.Enabled = false;
            txtMax.Enabled = false;
            btnEquipAdd.Enabled = false;

            txtNumber.Text = "0";
            txtMax.Text = "1";
            dgvEquipment.ClearSelection();

            //Checks that EqpFilePath.txt exists
            if (System.IO.File.Exists("EqpFilePath.txt"))
            {
                string fileEqpPath = File.ReadAllText("EqpFilePath.txt");
                Globals.GlobalVariables.EQPPATH = System.IO.Path.Combine(fileEqpPath);
                txtEqpFile.Text = fileEqpPath;

                if (!System.IO.File.Exists(Globals.GlobalVariables.EQPPATH))
                {
                    frmMissingEqpFile loadfileform = new frmMissingEqpFile();
                    loadfileform.ShowDialog();
                    return;
                }

                //SET UP XML
                XDocument xdoc = XDocument.Load(Globals.GlobalVariables.EQPPATH);
                XElement xelem = XElement.Load(Globals.GlobalVariables.EQPPATH);

                var dequip = xdoc.Descendants("UNITS_DATABASE");
                var xequip = xelem.Descendants("UNITS_DATABASE");

                //POPULATES COUNTRY COMBO BOX
                List<string> countryList = new List<string>();
                var eqp = xdoc.Descendants("COUNTRY");
                var eqpd = eqp.GroupBy(x => x.ToString().Split(new[] { '-' }, 2)[0]).Select(y => y.First()).Where(x => x.Value.ToString() != "Do not use").Distinct().OrderBy(z => z.ToString()); //SORTS DISTINCT COUNTRY NAMES
                countryList.Add("-ALL-");
                
                foreach (XElement edis in eqpd)
                {
                    countryList.Add(edis.Value.ToString().Split(new[] { '-' }, 2)[0]);
                }
                cboCountry.DataSource = countryList;

                //POPULATES CATEGORY COMBO BOX
                var xcat = from c in xequip.Descendants()
                           where c.Element("FLAG0") != null && c.Element("FLAG0").Value.ToString() == "1"
                           select c;

                List<string> categoryList = new List<string>();
                categoryList.Add("-SELECT-");

                foreach (XElement cc in xcat)
                {
                    categoryList.Add(cc.Element("NAME").Value.ToString());
                }

                cboCategory.DataSource = categoryList;
            }
            else
            {
                frmLoadEqpFile loadfileform = new frmLoadEqpFile();
                loadfileform.ShowDialog();
                return;
            }

            ///SETUP DGV EQP CHARACTERISTICS
            dgvEquipment.AutoGenerateColumns = false;
            dgvEquipment.ColumnCount = 20;
            dgvEquipment.ReadOnly = true;
            dgvEquipment.Columns[0].Name = "Equipment";
            dgvEquipment.Columns["Equipment"].DataPropertyName = "equipz";
            dgvEquipment.Columns[1].Name = "Country";
            dgvEquipment.Columns["Country"].DataPropertyName = "country";
            dgvEquipment.Columns[2].Name = "FLAG0";
            dgvEquipment.Columns["FLAG0"].DataPropertyName = "flag0";
            dgvEquipment.Columns[3].Name = "FLAG1";
            dgvEquipment.Columns["FLAG1"].DataPropertyName = "flag1";
            dgvEquipment.Columns[4].Name = "FLAG2";
            dgvEquipment.Columns["FLAG2"].DataPropertyName = "flag2";
            dgvEquipment.Columns[5].Name = "FLAG3";
            dgvEquipment.Columns["FLAG3"].DataPropertyName = "flag3";
            dgvEquipment.Columns[6].Name = "FLAG4";
            dgvEquipment.Columns["FLAG4"].DataPropertyName = "flag4";
            dgvEquipment.Columns[7].Name = "FLAG5";
            dgvEquipment.Columns["FLAG5"].DataPropertyName = "flag5";
            dgvEquipment.Columns[8].Name = "FLAG6";
            dgvEquipment.Columns["FLAG6"].DataPropertyName = "flag6";
            dgvEquipment.Columns[9].Name = "FLAG7";
            dgvEquipment.Columns["FLAG7"].DataPropertyName = "flag7";
            dgvEquipment.Columns[10].Name = "AT";
            dgvEquipment.Columns["AT"].DataPropertyName = "at";
            dgvEquipment.Columns[11].Name = "AP";
            dgvEquipment.Columns["AP"].DataPropertyName = "ap";
            dgvEquipment.Columns[12].Name = "AA";
            dgvEquipment.Columns["AA"].DataPropertyName = "aa";
            dgvEquipment.Columns[13].Name = "DEFENSE";
            dgvEquipment.Columns["DEFENSE"].DataPropertyName = "defense";
            dgvEquipment.Columns[14].Name = "RANGE";
            dgvEquipment.Columns["RANGE"].DataPropertyName = "range";
            dgvEquipment.Columns[15].Name = "VOLUME";
            dgvEquipment.Columns["VOLUME"].DataPropertyName = "volume";
            dgvEquipment.Columns[16].Name = "WEIGHT";
            dgvEquipment.Columns["WEIGHT"].DataPropertyName = "weight";
            dgvEquipment.Columns[17].Name = "SHELLWGHT";
            dgvEquipment.Columns["SHELLWGHT"].DataPropertyName = "shellwght";
            dgvEquipment.Columns[18].Name = "ARMOR";
            dgvEquipment.Columns["ARMOR"].DataPropertyName = "armor";
            dgvEquipment.Columns[19].Name = "SAMRNG";
            dgvEquipment.Columns["SAMRNG"].DataPropertyName = "samrng";

            dgvEquipment.Columns["FLAG0"].Visible = false;
            dgvEquipment.Columns["FLAG1"].Visible = false;
            dgvEquipment.Columns["FLAG2"].Visible = false;
            dgvEquipment.Columns["FLAG3"].Visible = false;
            dgvEquipment.Columns["FLAG4"].Visible = false;
            dgvEquipment.Columns["FLAG5"].Visible = false;
            dgvEquipment.Columns["FLAG6"].Visible = false;
            dgvEquipment.Columns["FLAG7"].Visible = false;
            dgvEquipment.Columns["AT"].Visible = false;
            dgvEquipment.Columns["AP"].Visible = false;
            dgvEquipment.Columns["AA"].Visible = false;
            dgvEquipment.Columns["DEFENSE"].Visible = false;
            dgvEquipment.Columns["RANGE"].Visible = false;
            dgvEquipment.Columns["VOLUME"].Visible = false;
            dgvEquipment.Columns["WEIGHT"].Visible = false;
            dgvEquipment.Columns["SHELLWGHT"].Visible = false;
            dgvEquipment.Columns["ARMOR"].Visible = false;
            dgvEquipment.Columns["SAMRNG"].Visible = false;

            ///SETUP DGV EQP VALUES
            dgvEqpValues.AutoGenerateColumns = false;
            dgvEqpValues.ColumnCount = 2;
            dgvEqpValues.ReadOnly = true;
            dgvEqpValues.RowTemplate.Height = 17;

            dgvEqpValues.Columns[0].Name = "Factor";
            dgvEqpValues.Columns["Factor"].DataPropertyName = "factor";
            dgvEqpValues.Columns[1].Name = "Value";
            dgvEqpValues.Columns["Value"].DataPropertyName = "valuez";
            dgvEqpValues.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEqpValues.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvEqpValues.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgvEqpValues.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

            //POPULATES EQUIPTYPE COMBO BOX
            var equiptype = new BindingList<KeyValuePair<string, string>>();
            equiptype.Add(new KeyValuePair<string, string>("xxxxx-00", "-ALL-"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG7-8", "Infantry (59)"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG0-2", "Armored (1)"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG0-32", "Engineer (5)"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG0-8", "Recon (3)"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG5-1", "Airborne (40)"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG1-32", "Long Range (13)"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG7-2", "Shock Cavalry (57)"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG5-64", "Command (46)"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG5-32", "Support (45)"));
            equiptype.Add(new KeyValuePair<string, string>("FLAG6-2", "Police (49)"));
            cboEquipType.DataSource = equiptype;
            cboEquipType.ValueMember = "Key";
            cboEquipType.DisplayMember = "Value";

            //POPULATES MOVEMENT COMBO BOX
            var movement = new BindingList<KeyValuePair<string, string>>();
            movement.Add(new KeyValuePair<string, string>("xxxxx-00", "-ALL-"));
            movement.Add(new KeyValuePair<string, string>("FLAG1-2", "Slow Movement (9)"));
            movement.Add(new KeyValuePair<string, string>("FLAG1-4", "Motorized (10)"));
            movement.Add(new KeyValuePair<string, string>("FLAG3-64", "Slow Motorized"));
            movement.Add(new KeyValuePair<string, string>("FLAG3-128", "Fast Motorized"));
            movement.Add(new KeyValuePair<string, string>("FLAG0-64", "Horse Movement (6)"));
            movement.Add(new KeyValuePair<string, string>("FLAG3-4", "Fast Horse Movement (26)"));
            movement.Add(new KeyValuePair<string, string>("FLAG2-16", "Amphibious (20)"));
            movement.Add(new KeyValuePair<string, string>("FLAG1-8", "Helicopter Movement (11)"));
            movement.Add(new KeyValuePair<string, string>("FLAG0-16", "Static (4)"));
            movement.Add(new KeyValuePair<string, string>("FLAG0-128", "Fixed (7)"));
            movement.Add(new KeyValuePair<string, string>("FLAG6-64", "Roadbound (54)"));
            movement.Add(new KeyValuePair<string, string>("FLAG3-16", "Railbound (28)"));
            movement.Add(new KeyValuePair<string, string>("FLAG4-64", "In-Flight Refueling (38)"));
            movement.Add(new KeyValuePair<string, string>("FLAG1-1", "Transport (8)"));
            movement.Add(new KeyValuePair<string, string>("FLAG6-4", "Light Transport Helo (50)"));
            movement.Add(new KeyValuePair<string, string>("FLAG6-8", "Medium Transport Helo (51)"));
            movement.Add(new KeyValuePair<string, string>("FLAG6-16", "Heavy Transport Helo (52)"));
            cboMovement.DataSource = movement;
            cboMovement.ValueMember = "Key";
            cboMovement.DisplayMember = "Value";

            //POPULATES WEAPONS COMBO BOX
            var weapons = new BindingList<KeyValuePair<string, string>>();
            weapons.Add(new KeyValuePair<string, string>("xxxxx-00", "-ALL-"));
            weapons.Add(new KeyValuePair<string, string>("FLAG5-2", "Optics1 (41)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG5-4", "Optics2 (42)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG5-8", "Optics3 (43)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG5-16", "Optics4 (44)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG4-16", "Kinetic Anti-Armor (36)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG1-32", "Long Range (13)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG4-32", "Precision Guided Weapons (37)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG6-128", "Extended Range Weapon (55)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG2-128", "Low-Hi Alt AA (23)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG1-16", "Hi Alt AA (12)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG7-1", "Standoff Weapons (56)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG3-1", "All-Weather (24)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG2-4", "Low Alt Attack Aircraft (18)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG2-8", "Hi Alt Attack Aircraft (19)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG7-64", "Dual Purpose Missile (62)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG3-2", "Anti-Shipping (25)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG3-32", "Anti-Shipping Only (29)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG7-128", "Torpedo Bomber (63)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG4-8", "Nuclear Capable (35)"));
            weapons.Add(new KeyValuePair<string, string>("FLAG5-128", "Smoke Only (47)"));
            cboWeapons.DataSource = weapons;
            cboWeapons.ValueMember = "Key";
            cboWeapons.DisplayMember = "Value";

            //POPULATES DEFENSIVE COMBO BOX
            var defense = new BindingList<KeyValuePair<string, string>>();
            defense.Add(new KeyValuePair<string, string>("xxxxx-00", "-ALL-"));
            defense.Add(new KeyValuePair<string, string>("FLAG4-1", "Composite Armor (32)"));
            defense.Add(new KeyValuePair<string, string>("FLAG4-2", "Laminate Armor (33)"));
            defense.Add(new KeyValuePair<string, string>("FLAG6-1", "Reactive Armor (48)"));
            defense.Add(new KeyValuePair<string, string>("FLAG0-4", "Active Defender (2)"));
            defense.Add(new KeyValuePair<string, string>("FLAG4-128", "Lightweight (39)"));
            defense.Add(new KeyValuePair<string, string>("FLAG6-32", "Agile (53)"));
            defense.Add(new KeyValuePair<string, string>("FLAG7-16", "Poor Geometry (60)"));
            defense.Add(new KeyValuePair<string, string>("FLAG7-32", "Fair Geometry (61)"));
            defense.Add(new KeyValuePair<string, string>("FLAG4-4", "NBC Protection (34)"));
            cboDefensive.DataSource = defense;
            cboDefensive.ValueMember = "Key";
            cboDefensive.DisplayMember = "Value";

            //POPULATES ENGINEER COMBO BOX
            var engineer = new BindingList<KeyValuePair<string, string>>();
            engineer.Add(new KeyValuePair<string, string>("xxxxx-00", "-ALL-"));
            engineer.Add(new KeyValuePair<string, string>("FLAG0-32", "Engineer (5)"));
            engineer.Add(new KeyValuePair<string, string>("FLAG7-4", "Railroad Repair (58)"));
            engineer.Add(new KeyValuePair<string, string>("FLAG3-8", "Major Ford Capable (27)"));
            cboEngineering.DataSource = engineer;
            cboEngineering.ValueMember = "Key";
            cboEngineering.DisplayMember = "Value";
                       
            //POPULATES NAVAL COMBO BOX
            var naval = new BindingList<KeyValuePair<string, string>>();
            naval.Add(new KeyValuePair<string, string>("xxxxx-00", "-ALL-"));
            naval.Add(new KeyValuePair<string, string>("FLAG2-64", "Riverine Naval (22)"));
            naval.Add(new KeyValuePair<string, string>("FLAG1-64", "Light Naval (14)"));
            naval.Add(new KeyValuePair<string, string>("FLAG1-128", "Medium Naval ((15)"));
            naval.Add(new KeyValuePair<string, string>("FLAG2-1", "Heavy Naval (16)"));
            naval.Add(new KeyValuePair<string, string>("FLAG2-2", "Carrier Naval (17)"));
            naval.Add(new KeyValuePair<string, string>("FLAG2-32", "Naval Air (21)"));
            cboNaval.DataSource = naval;
            cboNaval.ValueMember = "Key";
            cboNaval.DisplayMember = "Value";
            
            cboCategory.SelectedIndex = 0;
            cboCountry.SelectedIndex = 0;
            cboEquipType.SelectedIndex = 0;
            cboEquipType.SelectedIndex = 0;
            cboMovement.SelectedIndex = 0;
            cboWeapons.SelectedIndex = 0;
            cboNaval.SelectedIndex = 0;
        }

        private void cboCategory_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cboCountry.Enabled = true;
            cboCountry.SelectedIndex = 0;
            cboEquipType.Enabled = true;
            cboEquipType.SelectedIndex = 0;
            cboMovement.Enabled = true;
            cboMovement.SelectedIndex = 0;
            cboWeapons.Enabled = true;
            cboWeapons.SelectedIndex = 0;
            cboDefensive.Enabled = true;
            cboDefensive.SelectedIndex = 0;
            cboEngineering.Enabled = true;
            cboEngineering.SelectedIndex = 0;
            cboNaval.Enabled = true;
            cboNaval.SelectedIndex = 0;

            //POPULATE DT FOR DGVEQUIPMENT
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("equipz", typeof(string));
            dt.Columns.Add("country", typeof(string));
            dt.Columns.Add("flag0", typeof(string));
            dt.Columns.Add("flag1", typeof(string));
            dt.Columns.Add("flag2", typeof(string));
            dt.Columns.Add("flag3", typeof(string));
            dt.Columns.Add("flag4", typeof(string));
            dt.Columns.Add("flag5", typeof(string));
            dt.Columns.Add("flag6", typeof(string));
            dt.Columns.Add("flag7", typeof(string));
            dt.Columns.Add("at", typeof(string));
            dt.Columns.Add("ap", typeof(string));
            dt.Columns.Add("aa", typeof(string));
            dt.Columns.Add("defense", typeof(string));
            dt.Columns.Add("range", typeof(string));
            dt.Columns.Add("volume", typeof(string));
            dt.Columns.Add("weight", typeof(string));
            dt.Columns.Add("shellwght", typeof(string));
            dt.Columns.Add("armor", typeof(string));
            dt.Columns.Add("samrng", typeof(string));

            //POPULATE EMPTY DT FOR DGQEQUIPVALUES
            DataTable dt2 = new DataTable();
            dt2.Clear();
            dt2.Columns.Add("factor", typeof(string));
            dt2.Columns.Add("valuez", typeof(string));

            if (cboCategory.SelectedIndex != 0)
            {
                //POPULATES dgv
                XElement xelem = XElement.Load(txtEqpFile.Text);
                var xequip = xelem.Descendants("UNITS_DATABASE");
                List<string> equipmentList = new List<string>();

                IEnumerable<XElement> xcat = (from c in xequip.Descendants()
                                              where c.Element("NAME") != null && c.Element("NAME").Value.ToString() == cboCategory.Text
                                              select c).FirstOrDefault().ElementsAfterSelf();

                foreach (XElement cc in xcat)
                {
                    if (cc.Element("FLAG0").Value.ToString() == "1")
                    {
                        break;
                    }
                    //**
                    if (cc.Element("NAME").Value.ToString() != "EMPTY" && cc.Element("NAME").Value.ToString() != "Empty")
                    { 
                        //**
                    dt.Rows.Add(cc.Element("NAME").Value.ToString(), cc.Element("COUNTRY").Value.ToString(), cc.Element("FLAG0").Value.ToString(), cc.Element("FLAG1").Value.ToString(),
                            cc.Element("FLAG2").Value.ToString(), cc.Element("FLAG3").Value.ToString(), cc.Element("FLAG4").Value.ToString(), cc.Element("FLAG5").Value.ToString(),
                            cc.Element("FLAG6").Value.ToString(), cc.Element("FLAG7").Value.ToString(), cc.Element("AT").Value.ToString(), cc.Element("AP").Value.ToString(),
                            cc.Element("AA").Value.ToString(), cc.Element("DF").Value.ToString(), cc.Element("ARTY_RNG").Value.ToString(), cc.Element("VOL").Value.ToString(),
                            cc.Element("WEIGHT").Value.ToString(), cc.Element("SHELL_W").Value.ToString(), cc.Element("ARMOR").Value.ToString(), cc.Element("SAM_RNG").Value.ToString());
                        //**
                    }
                    //**
                }
                btnEquipLoad.Enabled = false;
            }
            else
            {
                dt.Clear();
                btnEquipLoad.Enabled = true;
                cboCountry.Enabled = false;
                cboEquipType.Enabled = false;
                cboMovement.Enabled = false;
                cboWeapons.Enabled = false;
                cboDefensive.Enabled = false;
                cboEngineering.Enabled = false;
                cboNaval.Enabled = false;
            }

            dgvEquipment.DataSource = dt;
            dgvEqpValues.DataSource = dt2;
            dgvEquipment.ClearSelection();
            dgvEqpValues.ClearSelection();

            //RESET COMBOBOXES
            cboCountry.SelectedIndex = 0;
            cboEquipType.SelectedIndex = 0;
            cboWeapons.SelectedIndex = 0;
            cboDefensive.SelectedIndex = 0;
            cboMovement.SelectedIndex = 0;
            cboEngineering.SelectedIndex = 0;
            cboNaval.SelectedIndex = 0;

            txtEquipCount.Text = dgvEquipment.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void cboCountry_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //POPULATES EQUIPMENT DGV FILTERED BY COUNTRY
            XElement xelem = XElement.Load(txtEqpFile.Text);
            var xequip = xelem.Descendants("UNITS_DATABASE");
            List<string> equipmentList = new List<string>();

            if (cboCountry.SelectedIndex.ToString() != "0")
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {

                    if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common ")) && (r.Cells[0].Value.ToString() != "EMPTY" && r.Cells[0].Value.ToString() != "Empty ")))
                        {
                            dgvEquipment.Rows[r.Index].Visible = true;
                        }
                        else
                        {
                            dgvEquipment.CurrentCell = null;
                            dgvEquipment.Rows[r.Index].Visible = false;
                        }
                    }
                }
                else
                {
                    foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                }
            cboEquipType.SelectedIndex = 0;
            cboWeapons.SelectedIndex = 0;
            cboDefensive.SelectedIndex = 0;
            cboMovement.SelectedIndex = 0;
            cboEngineering.SelectedIndex = 0;
            cboNaval.SelectedIndex = 0;

            txtEquipCount.Text = dgvEquipment.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void cboEquipType_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string strEquipKey = cboEquipType.SelectedValue.ToString();
            string strcboFlag = strEquipKey.Substring(0, 5);
            string strcboBytes = strEquipKey.Split('-').Reverse().Skip(0).First();
            int intcboBytes = int.Parse(strcboBytes);

            if (cboEquipType.SelectedIndex.ToString() != "0")
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {

                    switch (strcboFlag)
                    {
                        case "FLAG0":
                            int intdgvF0Bytes = int.Parse(r.Cells[2].Value.ToString());
                            Globals.FLAG0 dgvflag0 = (Globals.FLAG0)intdgvF0Bytes;
                            Globals.FLAG0 cboflag0 = (Globals.FLAG0)intcboBytes;

                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag0.HasFlag(cboflag0))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag0.HasFlag(cboflag0))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG1":
                            int intdgvF1Bytes = int.Parse(r.Cells[3].Value.ToString());
                            Globals.FLAG1 dgvflag1 = (Globals.FLAG1)intdgvF1Bytes;
                            Globals.FLAG1 cboflag1 = (Globals.FLAG1)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag1.HasFlag(cboflag1))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag1.HasFlag(cboflag1))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG5":
                            int intdgvF5Bytes = int.Parse(r.Cells[7].Value.ToString());
                            Globals.FLAG5 dgvflag5 = (Globals.FLAG5)intdgvF5Bytes;
                            Globals.FLAG5 cboflag5 = (Globals.FLAG5)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag5.HasFlag(cboflag5))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag5.HasFlag(cboflag5))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }


                            break;

                        case "FLAG6":
                            int intdgvF6Bytes = int.Parse(r.Cells[8].Value.ToString());
                            Globals.FLAG6 dgvflag6 = (Globals.FLAG6)intdgvF6Bytes;
                            Globals.FLAG6 cboflag6 = (Globals.FLAG6)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag6.HasFlag(cboflag6))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag6.HasFlag(cboflag6))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG7":
                            int intdgvF7Bytes = int.Parse(r.Cells[9].Value.ToString());
                            Globals.FLAG7 dgvflag7 = (Globals.FLAG7)intdgvF7Bytes;
                            Globals.FLAG7 cboflag7 = (Globals.FLAG7)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag7.HasFlag(cboflag7))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag7.HasFlag(cboflag7))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;
                    }

                }
                cboWeapons.SelectedIndex = 0;
                cboDefensive.SelectedIndex = 0;
                cboMovement.SelectedIndex = 0;
                cboEngineering.SelectedIndex = 0;
                cboNaval.SelectedIndex = 0;
            }
            else
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {
                    if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common ")))
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                    else
                    {
                        dgvEquipment.CurrentCell = null;
                        dgvEquipment.Rows[r.Index].Visible = false;
                    }

                    if (cboCountry.SelectedIndex.ToString() == "0")
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                }
            }
            txtEquipCount.Text = dgvEquipment.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void cboMovement_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string strMoveKey = cboMovement.SelectedValue.ToString();
            string strcboFlag = strMoveKey.Substring(0, 5);
            string strcboBytes = strMoveKey.Split('-').Reverse().Skip(0).First();
            int intcboBytes = int.Parse(strcboBytes);

            if (cboMovement.SelectedIndex.ToString() != "0")
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {

                    switch (strcboFlag)
                    {
                        case "FLAG0":
                            int intdgvF0Bytes = int.Parse(r.Cells[2].Value.ToString());
                            Globals.FLAG0 dgvflag0 = (Globals.FLAG0)intdgvF0Bytes;
                            Globals.FLAG0 cboflag0 = (Globals.FLAG0)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag0.HasFlag(cboflag0))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag0.HasFlag(cboflag0))
                                {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag0.HasFlag(cboflag0))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG1":
                            int intdgvF1Bytes = int.Parse(r.Cells[3].Value.ToString());
                            Globals.FLAG1 dgvflag1 = (Globals.FLAG1)intdgvF1Bytes;
                            Globals.FLAG1 cboflag1 = (Globals.FLAG1)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag1.HasFlag(cboflag1))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag1.HasFlag(cboflag1))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG2":
                            int intdgvF2Bytes = int.Parse(r.Cells[4].Value.ToString());
                            Globals.FLAG2 dgvflag2 = (Globals.FLAG2)intdgvF2Bytes;
                            Globals.FLAG2 cboflag2 = (Globals.FLAG2)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag2.HasFlag(cboflag2))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag2.HasFlag(cboflag2))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG3":
                            int intdgvF3Bytes = int.Parse(r.Cells[5].Value.ToString());
                            Globals.FLAG3 dgvflag3 = (Globals.FLAG3)intdgvF3Bytes;
                            Globals.FLAG3 cboflag3 = (Globals.FLAG3)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag3.HasFlag(cboflag3))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag3.HasFlag(cboflag3))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG4":
                            int intdgvF4Bytes = int.Parse(r.Cells[6].Value.ToString());
                            Globals.FLAG4 dgvflag4 = (Globals.FLAG4)intdgvF4Bytes;
                            Globals.FLAG4 cboflag4 = (Globals.FLAG4)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag4.HasFlag(cboflag4))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag4.HasFlag(cboflag4))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG6":
                            int intdgvF6Bytes = int.Parse(r.Cells[8].Value.ToString());
                            Globals.FLAG6 dgvflag6 = (Globals.FLAG6)intdgvF6Bytes;
                            Globals.FLAG6 cboflag6 = (Globals.FLAG6)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag6.HasFlag(cboflag6))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag6.HasFlag(cboflag6))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG7":
                            int intdgvF7Bytes = int.Parse(r.Cells[9].Value.ToString());
                            Globals.FLAG7 dgvflag7 = (Globals.FLAG7)intdgvF7Bytes;
                            Globals.FLAG7 cboflag7 = (Globals.FLAG7)intcboBytes;

                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag7.HasFlag(cboflag7))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag7.HasFlag(cboflag7))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }
                            break;
                    }
                }
            }
            else
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {
                    if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common ")))
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                    else
                    {
                        dgvEquipment.CurrentCell = null;
                        dgvEquipment.Rows[r.Index].Visible = false;
                    }

                    if (cboCountry.SelectedIndex.ToString() == "0")
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                }
            }
            cboEquipType.SelectedIndex = 0;
            cboWeapons.SelectedIndex = 0;
            cboDefensive.SelectedIndex = 0;
            cboEngineering.SelectedIndex = 0;
            cboNaval.SelectedIndex = 0;

            txtEquipCount.Text = dgvEquipment.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void cboWeapons_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string strWeaponsKey = cboWeapons.SelectedValue.ToString();
            string strcboFlag = strWeaponsKey.Substring(0, 5);
            string strcboBytes = strWeaponsKey.Split('-').Reverse().Skip(0).First();
            int intcboBytes = int.Parse(strcboBytes);

            if (cboWeapons.SelectedIndex.ToString() != "0")
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {

                    switch (strcboFlag)
                    {
                        case "FLAG0":
                            int intdgvF0Bytes = int.Parse(r.Cells[2].Value.ToString());
                            Globals.FLAG0 dgvflag0 = (Globals.FLAG0)intdgvF0Bytes;
                            Globals.FLAG0 cboflag0 = (Globals.FLAG0)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag0.HasFlag(cboflag0))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag0.HasFlag(cboflag0))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG1":
                            int intdgvF1Bytes = int.Parse(r.Cells[3].Value.ToString());
                            Globals.FLAG1 dgvflag1 = (Globals.FLAG1)intdgvF1Bytes;
                            Globals.FLAG1 cboflag1 = (Globals.FLAG1)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag1.HasFlag(cboflag1))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag1.HasFlag(cboflag1))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG2":
                            int intdgvF2Bytes = int.Parse(r.Cells[4].Value.ToString());
                            Globals.FLAG2 dgvflag2 = (Globals.FLAG2)intdgvF2Bytes;
                            Globals.FLAG2 cboflag2 = (Globals.FLAG2)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag2.HasFlag(cboflag2))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag2.HasFlag(cboflag2))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG3":
                            int intdgvF3Bytes = int.Parse(r.Cells[5].Value.ToString());
                            Globals.FLAG3 dgvflag3 = (Globals.FLAG3)intdgvF3Bytes;
                            Globals.FLAG3 cboflag3 = (Globals.FLAG3)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag3.HasFlag(cboflag3))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag3.HasFlag(cboflag3))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG4":
                            int intdgvF4Bytes = int.Parse(r.Cells[6].Value.ToString());
                            Globals.FLAG4 dgvflag4 = (Globals.FLAG4)intdgvF4Bytes;
                            Globals.FLAG4 cboflag4 = (Globals.FLAG4)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag4.HasFlag(cboflag4))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag4.HasFlag(cboflag4))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG5":
                            int intdgvF5Bytes = int.Parse(r.Cells[7].Value.ToString());
                            Globals.FLAG5 dgvflag5 = (Globals.FLAG5)intdgvF5Bytes;
                            Globals.FLAG5 cboflag5 = (Globals.FLAG5)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag5.HasFlag(cboflag5))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag5.HasFlag(cboflag5))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG6":
                            int intdgvF6Bytes = int.Parse(r.Cells[8].Value.ToString());
                            Globals.FLAG6 dgvflag6 = (Globals.FLAG6)intdgvF6Bytes;
                            Globals.FLAG6 cboflag6 = (Globals.FLAG6)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag6.HasFlag(cboflag6))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag6.HasFlag(cboflag6))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG7":
                            int intdgvF7Bytes = int.Parse(r.Cells[9].Value.ToString());
                            Globals.FLAG7 dgvflag7 = (Globals.FLAG7)intdgvF7Bytes;
                            Globals.FLAG7 cboflag7 = (Globals.FLAG7)intcboBytes;
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag7.HasFlag(cboflag7))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag7.HasFlag(cboflag7))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;
                    }
                }
            }
            else
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {
                    if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common ")))
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                    else
                    {
                        dgvEquipment.CurrentCell = null;
                        dgvEquipment.Rows[r.Index].Visible = false;
                    }

                    if (cboCountry.SelectedIndex.ToString() == "0")
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                }
            }
            cboEquipType.SelectedIndex = 0;
            cboDefensive.SelectedIndex = 0;
            cboMovement.SelectedIndex = 0;
            cboEngineering.SelectedIndex = 0;
            cboNaval.SelectedIndex = 0;

            txtEquipCount.Text = dgvEquipment.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void cboDefensive_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string strDefensiveKey = cboDefensive.SelectedValue.ToString();
            string strcboFlag = strDefensiveKey.Substring(0, 5);
            string strcboBytes = strDefensiveKey.Split('-').Reverse().Skip(0).First();
            int intcboBytes = int.Parse(strcboBytes);

            if (cboDefensive.SelectedIndex.ToString() != "0")
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {

                    switch (strcboFlag)
                    {
                        case "FLAG0":
                            int intdgvF0Bytes = int.Parse(r.Cells[2].Value.ToString());
                            Globals.FLAG0 dgvflag0 = (Globals.FLAG0)intdgvF0Bytes;
                            Globals.FLAG0 cboflag0 = (Globals.FLAG0)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag0.HasFlag(cboflag0))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag0.HasFlag(cboflag0))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag0.HasFlag(cboflag0))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG4":
                            int intdgvF4Bytes = int.Parse(r.Cells[6].Value.ToString());
                            Globals.FLAG4 dgvflag4 = (Globals.FLAG4)intdgvF4Bytes;
                            Globals.FLAG4 cboflag4 = (Globals.FLAG4)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag4.HasFlag(cboflag4))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag4.HasFlag(cboflag4))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag4.HasFlag(cboflag4))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG6":
                            int intdgvF6Bytes = int.Parse(r.Cells[8].Value.ToString());
                            Globals.FLAG6 dgvflag6 = (Globals.FLAG6)intdgvF6Bytes;
                            Globals.FLAG6 cboflag6 = (Globals.FLAG6)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag6.HasFlag(cboflag6))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag6.HasFlag(cboflag6))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag6.HasFlag(cboflag6))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG7":
                            int intdgvF7Bytes = int.Parse(r.Cells[9].Value.ToString());
                            Globals.FLAG7 dgvflag7 = (Globals.FLAG7)intdgvF7Bytes;
                            Globals.FLAG7 cboflag7 = (Globals.FLAG7)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag7.HasFlag(cboflag7))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag7.HasFlag(cboflag7))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag7.HasFlag(cboflag7))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;
                    }
                }
            }
            else
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {
                    //dgvEquipment.Rows[r.Index].Visible = true;
                    if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common ")))
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                    else
                    {
                        dgvEquipment.CurrentCell = null;
                        dgvEquipment.Rows[r.Index].Visible = false;
                    }

                    if (cboCountry.SelectedIndex.ToString() == "0")
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                }
            }
            //cboCountry.SelectedIndex = 0;
            cboEquipType.SelectedIndex = 0;
            cboWeapons.SelectedIndex = 0;
            cboMovement.SelectedIndex = 0;
            cboEngineering.SelectedIndex = 0;
            cboNaval.SelectedIndex = 0;

            txtEquipCount.Text = dgvEquipment.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void cboEngineering_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string strEngineeringKey = cboEngineering.SelectedValue.ToString();
            string strcboFlag = strEngineeringKey.Substring(0, 5);
            string strcboBytes = strEngineeringKey.Split('-').Reverse().Skip(0).First();
            int intcboBytes = int.Parse(strcboBytes);

            if (cboEngineering.SelectedIndex.ToString() != "0")
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {

                    switch (strcboFlag)
                    {
                        case "FLAG0":
                            int intdgvF0Bytes = int.Parse(r.Cells[2].Value.ToString());
                            Globals.FLAG0 dgvflag0 = (Globals.FLAG0)intdgvF0Bytes;
                            Globals.FLAG0 cboflag0 = (Globals.FLAG0)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag0.HasFlag(cboflag0))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag0.HasFlag(cboflag0))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag0.HasFlag(cboflag0))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG3":
                            int intdgvF3Bytes = int.Parse(r.Cells[5].Value.ToString());
                            Globals.FLAG3 dgvflag3 = (Globals.FLAG3)intdgvF3Bytes;
                            Globals.FLAG3 cboflag3 = (Globals.FLAG3)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag3.HasFlag(cboflag3))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag3.HasFlag(cboflag3))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag3.HasFlag(cboflag3))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG7":
                            int intdgvF7Bytes = int.Parse(r.Cells[9].Value.ToString());
                            Globals.FLAG7 dgvflag7 = (Globals.FLAG7)intdgvF7Bytes;
                            Globals.FLAG7 cboflag7 = (Globals.FLAG7)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag7.HasFlag(cboflag7))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag7.HasFlag(cboflag7))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag7.HasFlag(cboflag7))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;
                    }
                }
            }
            else
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {
                    //dgvEquipment.Rows[r.Index].Visible = true;
                    if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common ")))
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                    else
                    {
                        dgvEquipment.CurrentCell = null;
                        dgvEquipment.Rows[r.Index].Visible = false;
                    }

                    if (cboCountry.SelectedIndex.ToString() == "0")
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                }
            }
            //cboCountry.SelectedIndex = 0;
            cboEquipType.SelectedIndex = 0;
            cboWeapons.SelectedIndex = 0;
            cboDefensive.SelectedIndex = 0;
            cboMovement.SelectedIndex = 0;
            cboNaval.SelectedIndex = 0;

            txtEquipCount.Text = dgvEquipment.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void cboNaval_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string strNavalKey = cboNaval.SelectedValue.ToString();
            string strcboFlag = strNavalKey.Substring(0, 5);
            string strcboBytes = strNavalKey.Split('-').Reverse().Skip(0).First();
            int intcboBytes = int.Parse(strcboBytes);

            if (cboNaval.SelectedIndex.ToString() != "0")
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {

                    switch (strcboFlag)
                    {
                        case "FLAG1":
                            int intdgvF1Bytes = int.Parse(r.Cells[3].Value.ToString());
                            Globals.FLAG1 dgvflag1 = (Globals.FLAG1)intdgvF1Bytes;
                            Globals.FLAG1 cboflag1 = (Globals.FLAG1)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag1.HasFlag(cboflag1))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag1.HasFlag(cboflag1))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag1.HasFlag(cboflag1))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;

                        case "FLAG2":
                            int intdgvF2Bytes = int.Parse(r.Cells[4].Value.ToString());
                            Globals.FLAG2 dgvflag2 = (Globals.FLAG2)intdgvF2Bytes;
                            Globals.FLAG2 cboflag2 = (Globals.FLAG2)intcboBytes;
                            //if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) && dgvflag2.HasFlag(cboflag2))
                            if (((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common "))) && dgvflag2.HasFlag(cboflag2))
                            {
                                dgvEquipment.Rows[r.Index].Visible = true;
                            }
                            else
                            {
                                dgvEquipment.CurrentCell = null;
                                dgvEquipment.Rows[r.Index].Visible = false;
                            }

                            //SHOW ALL COUNTRIES IF NO COUNTRY SELECTED
                            if (cboCountry.SelectedIndex.ToString() == "0")
                            {
                                if (dgvflag2.HasFlag(cboflag2))
                                {
                                    dgvEquipment.Rows[r.Index].Visible = true;
                                }
                                else
                                {
                                    dgvEquipment.CurrentCell = null;
                                    dgvEquipment.Rows[r.Index].Visible = false;
                                }
                            }

                            break;
                    }
                }
            }
            else
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                { 
                    //dgvEquipment.Rows[r.Index].Visible = true;
                    if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common ")))
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                    else
                    {
                        dgvEquipment.CurrentCell = null;
                        dgvEquipment.Rows[r.Index].Visible = false;
                    }

                    if (cboCountry.SelectedIndex.ToString() == "0")
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                }
            }
            //cboCountry.SelectedIndex = 0;
            cboEquipType.SelectedIndex = 0;
            cboWeapons.SelectedIndex = 0;
            cboDefensive.SelectedIndex = 0;
            cboMovement.SelectedIndex = 0;
            cboEngineering.SelectedIndex = 0;

            txtEquipCount.Text = dgvEquipment.Rows.GetRowCount(DataGridViewElementStates.Visible).ToString();
        }

        private void dgvEquipment_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (cboCountry.SelectedIndex.ToString() != "0")
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {

                    if ((r.Cells[1].Value).ToString().Contains(cboCountry.Text) || (r.Cells[1].Value.ToString().Contains("Common ")))
                    {
                        dgvEquipment.Rows[r.Index].Visible = true;
                    }
                    else
                    {
                        dgvEquipment.CurrentCell = null;
                        dgvEquipment.Rows[r.Index].Visible = false;
                    }
                }
            }
            else
            {
                foreach (System.Windows.Forms.DataGridViewRow r in dgvEquipment.Rows)
                {
                    dgvEquipment.Rows[r.Index].Visible = true;
                }
            }
            //cboCountry.SelectedIndex = 0;
            //cboWeapons.SelectedIndex = 0;
            //cboDefensive.SelectedIndex = 0;
            //cboMovement.SelectedIndex = 0;
            //cboEngineering.SelectedIndex = 0;
            //cboNaval.SelectedIndex = 0;
        }

        private void btnEquipCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEquipLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .eqp files *.eqp|*.eqp";
            if (file.ShowDialog() == DialogResult.OK)
            {
                Globals.GlobalVariables.EQPPATH = file.FileName;
                txtEqpFile.Text = file.FileName;
                System.IO.File.WriteAllText("EqpFilePath.txt", Globals.GlobalVariables.EQPPATH);

                //**********************FIX INVALID XML
                
                StreamReader streamReader = new StreamReader(Globals.GlobalVariables.EQPPATH);
                string xmlText = streamReader.ReadToEnd();
                streamReader.Close();

                string hex = @"[^\x09\x0A\x0D\x20 -\xD7FF\xE000 -\xFFFD\x10000 - x10FFFF]";

                //+++++++++++CHECK FOR INVALID XML CHARACTERS
                Match match = Regex.Match(xmlText, hex);
                // CHECK MATCH RESULTS
                if (match.Success == true)
                {
                    string goodXML = Regex.Replace(xmlText, hex, "$", RegexOptions.Compiled);
                    XmlDocument goodXMLdoc = new XmlDocument();
                    goodXMLdoc.LoadXml(goodXML);
                    goodXMLdoc.Save(Globals.GlobalVariables.EQPPATH);

                    MessageBox.Show("TOAWxml has detected invalid XML characters.  The invalid characters have been replaced with '$'.",
                        "Invalid XML Characters",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation,
                        MessageBoxDefaultButton.Button1);
                }
                var f = new frmEquip();
                f.Show();
                this.Close();
            }
        }

        private void dgvEquipment_SelectionChanged(object sender, EventArgs e)
        {
            var currentRow = dgvEquipment.CurrentRow;

            if (currentRow != null)
            {
                string strName = dgvEquipment.CurrentRow.Cells[0].Value.ToString();
                string strCountry = dgvEquipment.CurrentRow.Cells[1].Value.ToString();
                string strFlag0 = dgvEquipment.CurrentRow.Cells[2].Value.ToString();
                string strFlag1 = dgvEquipment.CurrentRow.Cells[3].Value.ToString();
                string strFlag2 = dgvEquipment.CurrentRow.Cells[4].Value.ToString();
                string strFlag3 = dgvEquipment.CurrentRow.Cells[5].Value.ToString();
                string strFlag4 = dgvEquipment.CurrentRow.Cells[6].Value.ToString();
                string strFlag5 = dgvEquipment.CurrentRow.Cells[7].Value.ToString();
                string strFlag6 = dgvEquipment.CurrentRow.Cells[8].Value.ToString();
                string strFlag7 = dgvEquipment.CurrentRow.Cells[9].Value.ToString();
                string strAT = dgvEquipment.CurrentRow.Cells[10].Value.ToString();
                string strAP = dgvEquipment.CurrentRow.Cells[11].Value.ToString();
                string strAA = dgvEquipment.CurrentRow.Cells[12].Value.ToString();
                string strDef = dgvEquipment.CurrentRow.Cells[13].Value.ToString();
                string strRange = dgvEquipment.CurrentRow.Cells[14].Value.ToString();
                string strVol = dgvEquipment.CurrentRow.Cells[15].Value.ToString();
                string strWeight = dgvEquipment.CurrentRow.Cells[16].Value.ToString();
                string strShWght = dgvEquipment.CurrentRow.Cells[17].Value.ToString();
                string strArmor = dgvEquipment.CurrentRow.Cells[18].Value.ToString();
                string strSAMRng = dgvEquipment.CurrentRow.Cells[19].Value.ToString();


                int intFlag0 = int.Parse(strFlag0);
                int intFlag1 = int.Parse(strFlag1);
                int intFlag2 = int.Parse(strFlag2);
                int intFlag3 = int.Parse(strFlag3);
                int intFlag4 = int.Parse(strFlag4);
                int intFlag5 = int.Parse(strFlag5);
                int intFlag6 = int.Parse(strFlag6);
                int intFlag7 = int.Parse(strFlag7);

                Globals.FLAG0 flag0 = (Globals.FLAG0)intFlag0;
                Globals.FLAG1 flag1 = (Globals.FLAG1)intFlag1;
                Globals.FLAG2 flag2 = (Globals.FLAG2)intFlag2;
                Globals.FLAG3 flag3 = (Globals.FLAG3)intFlag3;
                Globals.FLAG4 flag4 = (Globals.FLAG4)intFlag4;
                Globals.FLAG5 flag5 = (Globals.FLAG5)intFlag5;
                Globals.FLAG6 flag6 = (Globals.FLAG6)intFlag6;
                Globals.FLAG7 flag7 = (Globals.FLAG7)intFlag7;

                Globals.FLAG0[] fl0 = {flag0};
                Globals.FLAG1[] fl1 = {flag1};
                Globals.FLAG2[] fl2 = {flag2};
                Globals.FLAG3[] fl3 = {flag3};
                Globals.FLAG4[] fl4 = {flag4};
                Globals.FLAG5[] fl5 = {flag5};
                Globals.FLAG6[] fl6 = {flag6};
                Globals.FLAG7[] fl7 = {flag7};

                List<string> F0stringlist = new List<string>();
                List<string> F1stringlist = new List<string>();
                List<string> F2stringlist = new List<string>();
                List<string> F3stringlist = new List<string>();
                List<string> F4stringlist = new List<string>();
                List<string> F5stringlist = new List<string>();
                List<string> F6stringlist = new List<string>();
                List<string> F7stringlist = new List<string>();
                List<string> Allstringlist = new List<string>();
                List<string> Emptystringlist = new List<string>();

                foreach (Globals.FLAG0 f0 in fl0)
                {
                    if (f0.HasFlag(Globals.FLAG0.ActiveDefend)) F0stringlist.Add("Active Defender");
                    if (f0.HasFlag(Globals.FLAG0.Armored)) F0stringlist.Add("Armored");
                    if (f0.HasFlag(Globals.FLAG0.Engineer)) F0stringlist.Add("Engineer");
                    if (f0.HasFlag(Globals.FLAG0.Fixed)) F0stringlist.Add("Fixed");
                    if (f0.HasFlag(Globals.FLAG0.HorseMove)) F0stringlist.Add("Horse Movement");
                    if (f0.HasFlag(Globals.FLAG0.Recon)) F0stringlist.Add("Recon Capable");
                    if (f0.HasFlag(Globals.FLAG0.Static)) F0stringlist.Add("Static");

                    Allstringlist.AddRange(F0stringlist);
                }

                foreach (Globals.FLAG1 f1 in fl1)
                {
                    if (f1.HasFlag(Globals.FLAG1.HelicopterMove)) F1stringlist.Add("Helicopter Movement");
                    if (f1.HasFlag(Globals.FLAG1.HiAltAA)) F1stringlist.Add("High Alt AA");
                    if (f1.HasFlag(Globals.FLAG1.LightNaval)) F1stringlist.Add("Light Naval");
                    if (f1.HasFlag(Globals.FLAG1.LongRange)) F1stringlist.Add("Long Range");
                    if (f1.HasFlag(Globals.FLAG1.MediumNaval)) F1stringlist.Add("Medium Naval");
                    if (f1.HasFlag(Globals.FLAG1.Motorized)) F1stringlist.Add("Motorized");
                    if (f1.HasFlag(Globals.FLAG1.Slow)) F1stringlist.Add("Slow");
                    if (f1.HasFlag(Globals.FLAG1.Transport)) F1stringlist.Add("Transport");

                    Allstringlist.AddRange(F1stringlist);
                }

                foreach (Globals.FLAG2 f2 in fl2)
                {
                    if (f2.HasFlag(Globals.FLAG2.Amphib)) F2stringlist.Add("Amphibious");
                    if (f2.HasFlag(Globals.FLAG2.CarrierNaval)) F2stringlist.Add("Carrier Naval");
                    if (f2.HasFlag(Globals.FLAG2.HeavyNaval)) F2stringlist.Add("Heavy Naval");
                    if (f2.HasFlag(Globals.FLAG2.HighAltAir)) F2stringlist.Add("High Alt Air Attack");
                    if (f2.HasFlag(Globals.FLAG2.HiLowAltAA)) F2stringlist.Add("Low-High Alt AA");
                    if (f2.HasFlag(Globals.FLAG2.LowAltAir)) F2stringlist.Add("Low Alt Air Attack");
                    if (f2.HasFlag(Globals.FLAG2.NavalAir)) F2stringlist.Add("Naval Aircraft");
                    if (f2.HasFlag(Globals.FLAG2.Riverine)) F2stringlist.Add("Riverine Naval");

                    Allstringlist.AddRange(F2stringlist);
                }

                foreach (Globals.FLAG3 f3 in fl3)
                {
                    if (f3.HasFlag(Globals.FLAG3.AllWeather)) F3stringlist.Add("All-Weather");
                    if (f3.HasFlag(Globals.FLAG3.AntiShip)) F3stringlist.Add("Anti-Ship");
                    if (f3.HasFlag(Globals.FLAG3.AntiShipOnly)) F3stringlist.Add("Anti-Ship Only");
                    if (f3.HasFlag(Globals.FLAG3.FastHorse)) F3stringlist.Add("Fast Horse Movement");
                    if (f3.HasFlag(Globals.FLAG3.FastMotorized)) F3stringlist.Add("Fast Motorized");
                    if (f3.HasFlag(Globals.FLAG3.MajorFord)) F3stringlist.Add("Major Ford Capable");
                    if (f3.HasFlag(Globals.FLAG3.Railbound)) F3stringlist.Add("Railbound");
                    if (f3.HasFlag(Globals.FLAG3.SlowMotorized)) F3stringlist.Add("Slow Motorized");

                    Allstringlist.AddRange(F3stringlist);
                }

                foreach (Globals.FLAG4 f4 in fl4)
                {
                    if (f4.HasFlag(Globals.FLAG4.CompositeArm)) F4stringlist.Add("Composite Armor");
                    if (f4.HasFlag(Globals.FLAG4.InFlightRefuel)) F4stringlist.Add("In-Flight Refueling");
                    if (f4.HasFlag(Globals.FLAG4.Kinetic)) F4stringlist.Add("Kinetic Anti-Armor");
                    if (f4.HasFlag(Globals.FLAG4.LaminateArm)) F4stringlist.Add("Laminate Armor");
                    if (f4.HasFlag(Globals.FLAG4.Lightweight)) F4stringlist.Add("Lightweight");
                    if (f4.HasFlag(Globals.FLAG4.NBC)) F4stringlist.Add("NBC Protection");
                    if (f4.HasFlag(Globals.FLAG4.Nuclear)) F4stringlist.Add("Nuclear Capable");
                    if (f4.HasFlag(Globals.FLAG4.PGW)) F4stringlist.Add("Precision Guided Weapons");

                    Allstringlist.AddRange(F4stringlist);
                }

                foreach (Globals.FLAG5 f5 in fl5)
                {
                    if (f5.HasFlag(Globals.FLAG5.Airborne)) F5stringlist.Add("Airborne");
                    if (f5.HasFlag(Globals.FLAG5.Command)) F5stringlist.Add("Command");
                    if (f5.HasFlag(Globals.FLAG5.Optics1)) F5stringlist.Add("Optics1");
                    if (f5.HasFlag(Globals.FLAG5.Optics2)) F5stringlist.Add("Optics2");
                    if (f5.HasFlag(Globals.FLAG5.Optics3)) F5stringlist.Add("Optics3");
                    if (f5.HasFlag(Globals.FLAG5.Optics4)) F5stringlist.Add("Optics4");
                    if (f5.HasFlag(Globals.FLAG5.Smoke)) F5stringlist.Add("Smoke Only");
                    if (f5.HasFlag(Globals.FLAG5.Support)) F5stringlist.Add("Support");

                    Allstringlist.AddRange(F5stringlist);
                }

                foreach (Globals.FLAG6 f6 in fl6)
                {
                    if (f6.HasFlag(Globals.FLAG6.Agile)) F6stringlist.Add("Agile");
                    if (f6.HasFlag(Globals.FLAG6.ExtendedRange)) F6stringlist.Add("Extended Range");
                    if (f6.HasFlag(Globals.FLAG6.HvyTransHelo)) F6stringlist.Add("Heavy Transport Helo");
                    if (f6.HasFlag(Globals.FLAG6.LtTransHelo)) F6stringlist.Add("Light Transport Helo");
                    if (f6.HasFlag(Globals.FLAG6.MedTransHelo)) F6stringlist.Add("Medium Transport Helo");
                    if (f6.HasFlag(Globals.FLAG6.Police)) F6stringlist.Add("Police");
                    if (f6.HasFlag(Globals.FLAG6.Reactive)) F6stringlist.Add("Reactive Armor");
                    if (f6.HasFlag(Globals.FLAG6.Roadbound)) F6stringlist.Add("Roadbound");

                    Allstringlist.AddRange(F6stringlist);
                }

                foreach (Globals.FLAG7 f7 in fl7)
                {
                    if (f7.HasFlag(Globals.FLAG7.DualPurpose)) F7stringlist.Add("Dual Purpose Missiles");
                    if (f7.HasFlag(Globals.FLAG7.FairGeom)) F7stringlist.Add("Fair Geometry");
                    if (f7.HasFlag(Globals.FLAG7.PoorGeom)) F7stringlist.Add("Poor Geometry");
                    if (f7.HasFlag(Globals.FLAG7.RailRepair)) F7stringlist.Add("Rail Repair");
                    if (f7.HasFlag(Globals.FLAG7.ShockCav)) F7stringlist.Add("Shock Cavalry");
                    if (f7.HasFlag(Globals.FLAG7.Standoff)) F7stringlist.Add("Stand-Off Weapons");
                    if (f7.HasFlag(Globals.FLAG7.TorpedoBomb)) F7stringlist.Add("Torpedo Bomber");
                    if (f7.HasFlag(Globals.FLAG7.Infantry)) F7stringlist.Add("Infantry");

                    Allstringlist.AddRange(F7stringlist);
                }

                //POPULATE DGV EQPVALUES
                DataTable dt2 = new DataTable();
                dt2.Clear();
                dt2.Columns.Add("factor", typeof(string));
                dt2.Columns.Add("valuez", typeof(string));
                dt2.Rows.Add("Anti-Armor", strAT);
                dt2.Rows.Add("Anti-Person", strAP);
                dt2.Rows.Add("Anti-Air", strAA);
                dt2.Rows.Add("Defense", strDef);
                dt2.Rows.Add("Art Range", strRange);
                dt2.Rows.Add("Volume", strVol);
                dt2.Rows.Add("Weight", strWeight);
                dt2.Rows.Add("Shell Weight", strShWght);
                dt2.Rows.Add("Armor", strArmor);
                dt2.Rows.Add("SAM Range", strSAMRng);

                DataTable dtEmpty = new DataTable();
                dtEmpty.Clear();
                dtEmpty.Columns.Add("factor", typeof(string));
                dtEmpty.Columns.Add("valuez", typeof(string));

                if (dgvEquipment.SelectedRows.Count > 0)
                {
                    lbEquipChars.DataSource = Allstringlist;
                    dgvEqpValues.DataSource = dt2;
                    txtNumber.Enabled = true;
                    txtMax.Enabled = true;
                }
                else
                {
                    lbEquipChars.DataSource = Emptystringlist;
                    dgvEqpValues.DataSource = dtEmpty;
                    txtNumber.Enabled = false;
                    txtMax.Enabled = false;
                }
                dgvEqpValues.ClearSelection();
            }
        }

        private void btnEquipAdd_Click(object sender, EventArgs e)
        {
            xmlform f = Application
            .OpenForms
            .OfType<xmlform>()
            .FirstOrDefault();

            //string unitID = Globals.GlobalVariables.ID.ToString();
            TreeNode selectedTNode = f.getSelectedNode();
            string unitID = selectedTNode.Tag.ToString();

            Globals.GlobalVariables.TREEVIEWCHANGED = true;

            //ADD XML ELEMENT
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathUnit = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitID + "]";
            var targetunit = xelem.XPathSelectElement(xpathUnit);
            int intChildren = targetunit.Elements("EQUIPMENT").Count();
            int newMax = 0;

            string strName = dgvEquipment.CurrentRow.Cells[0].Value.ToString();

            //GET HIGHEST ID OF EXISTING EQUIPMENT ELEMENTS, ADD 1 FOR NEW  EQUIP ELEMENT
            if (intChildren > 0)
            {
                var maxid = targetunit.Descendants("EQUIPMENT").Attributes("ID").ToList();
                int oldMax = targetunit.Descendants("EQUIPMENT").Max(m => (int)m.Attribute("ID"));
                newMax = oldMax + 1;
            }
            else
            {
                newMax = 1;
            }
            var newequipXNode = new XElement("EQUIPMENT",
                new XAttribute("ID", newMax.ToString()),
                new XAttribute("NAME", strName),
                new XAttribute("NUMBER",txtNumber.Text),
                new XAttribute("MAX", txtMax.Text),
                new XAttribute("DAMAGE", "0")
                );

            targetunit.Add(newequipXNode);

            //ADD TREENODE
            f.Focus();
            TreeNode targetTNode = f.getSelectedNode();
            TreeNode newTNode = new TreeNode();
            newTNode.Text = strName;
            newTNode.Tag = newMax.ToString();
            newTNode.Name = "EQUIPMENT";
            targetTNode.Nodes.Insert(targetTNode.Index + 1, newTNode);

            xelem.Save(Globals.GlobalVariables.PATH);

            //RESET CONTROLS
            Globals.GlobalVariables.TREEVIEWCHANGED = false;
            txtNumber.Text = "0";
            txtMax.Text = "1";
            dgvEquipment.ClearSelection();
            btnEquipAdd.Enabled = false;
            f.refreshTreeView();
            f.ReloadTree(targetTNode);
            f.ExpandTreeNode(targetTNode);
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtNumber.Text))
            {
                btnEquipAdd.Enabled = false;
                //errorProvider1.SetError(txtNumber, "Please enter positive whole number between 0 and 999.");
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtNumber.Text, out var intValue))
            {
                errorProvider1.SetError(txtNumber, "Please enter positive whole number between 0 and 999.");
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                //if (cboTrigger.Text == "Force 1 winning" || cboTrigger.Text == "Force 2 winning" || cboTrigger.Text == "Variable value")
                //{
                if (intValue >= 0 && (intValue <= 999)) //
                {
                    errorProvider1.SetError(txtNumber, "");
                    btnEquipAdd.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtNumber, "Please enter positive whole number between 0 and 999.");
                    btnEquipAdd.Enabled = false;
                }
            }
        }

        private void txtMax_TextChanged(object sender, EventArgs e)
        {

            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtMax.Text))
            {
                btnEquipAdd.Enabled = false;
                //errorProvider1.SetError(txtMax, "Please enter positive whole number between 0 and 999.");
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtMax.Text, out var intValue))
            {
                errorProvider1.SetError(txtMax, "Please enter positive whole number between 1 and 999.");
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1 && (intValue <= 999)) //
                {
                    errorProvider1.SetError(txtMax, "");
                    btnEquipAdd.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtMax, "Please enter positive whole number between 1 and 999.");
                    btnEquipAdd.Enabled = false;
                }
            }
            
        }
    }
}

