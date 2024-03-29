﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using TOAWTac;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualBasic.PowerPacks;

namespace TOAWXML
{
    //public static class ObjectCloner
    //{
    //    /// 
    //    /// Clones an object by using the .
    //    /// 
    //    /// The object to clone.
    //    /// 
    //    /// The object to be cloned must be serializable.
    //    /// 
    //    public static object Clone(object obj)
    //    {
    //        using (MemoryStream buffer = new MemoryStream())
    //        {
    //            BinaryFormatter formatter = new BinaryFormatter();
    //            formatter.Serialize(buffer, obj);
    //            buffer.Position = 0;
    //            object temp = formatter.Deserialize(buffer);
    //            return temp;
    //        }
    //    }
    //}
    public partial class frmTacFile : Form
    {
        //$$$$$$$$$$$$$$$$$$$$$$$$
        //CODE FOR HIDING CHECKBOXES ON TREEVIEW
        private const int TVIF_STATE = 0x8;
        private const int TVIS_STATEIMAGEMASK = 0xF000;
        private const int TV_FIRST = 0x1100;
        private const int TVM_SETITEM = TV_FIRST + 63;

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TVITEM lParam);

        DataTable dtFormation = new DataTable();
        DataTable dtUnit = new DataTable();
        DataTable dtEquip = new DataTable();

        static XElement tacFile;
        static string forceID;
        static string oldprof;
        static string oldsupply;
        static string oldready;
        static string oldname;
        static bool haschanged;

        public frmTacFile()
        {
            InitializeComponent();
            cboSupport.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboOrders.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboLossTol.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboUnitType.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboUnitSize.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboUnitOrders.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboUnitLossTol.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboExp.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboReplace.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboHdrUnitType.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboHdrUnitSize.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboHdrUnitOrders.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboHdrUnitLossTol.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboHdrUnitExp.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboHdrUnitReplace.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboHdrFormSupport.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboHdrFormOrders.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
            cboHdrFormLossTol.MouseWheel += (o, e) => ((HandledMouseEventArgs)e).Handled = true;
        }
        private void frmTacFile_Load(object sender, EventArgs e)
        {
            //SET GAME DATE
            DateTimePicker.Format = DateTimePickerFormat.Custom;
            DateTimePicker.CustomFormat = "d-MMM-y";

            //MAKE PANELS INVISIBLE
            pnlForce.Visible = false;
            pnlFormation.Visible = false;
            pnlUnit.Visible = false;

            //MAKE DATA REPEATERS INVISIBLE
            drForce.Visible = false;
            drFormation.Visible = false;
            drUnit.Visible = false;

            //DISABLE FORCE RADIO BUTTONS
            rbForce1.Enabled = false;
            rbForce2.Enabled = false;

            rbForce1.Checked = false;
            rbForce2.Checked = false;

            //DISABLE SYNC BUTTONS
            btnSync.Enabled = false;
            btnPostBattle.Enabled = false;

            //CREATE DATATABLE FOR FORMATIONS
            dtFormation.Columns.Add("Name", typeof(string));
            dtFormation.Columns.Add("Prof", typeof(string));
            dtFormation.Columns.Add("Supply", typeof(string));
            dtFormation.Columns.Add("Support", typeof(string));
            dtFormation.Columns.Add("Orders", typeof(string));
            dtFormation.Columns.Add("LossTol", typeof(string));
            dtFormation.Columns.Add("FormID", typeof(string));
            dtFormation.Columns.Add("Cdr", typeof(string));
            dtFormation.Columns.Add("Rank", typeof(string));
            dtFormation.Columns.Add("Rating", typeof(string));
            dtFormation.Columns.Add("FormDate", typeof(string));

            //CREATE DATATABLE FOR UNITS
            dtUnit.Columns.Add("UnitName", typeof(string));
            dtUnit.Columns.Add("UnitProf", typeof(string));
            dtUnit.Columns.Add("UnitSupply", typeof(string));
            dtUnit.Columns.Add("UnitOrders", typeof(string));
            dtUnit.Columns.Add("UnitLossTol", typeof(string));
            dtUnit.Columns.Add("UnitReadiness", typeof(string));
            dtUnit.Columns.Add("UnitType", typeof(string));
            dtUnit.Columns.Add("UnitSize", typeof(string));
            dtUnit.Columns.Add("UnitExp", typeof(string));
            dtUnit.Columns.Add("UnitReplace", typeof(string));
            dtUnit.Columns.Add("UnitID", typeof(string));
            dtUnit.Columns.Add("Cdr", typeof(string));
            dtUnit.Columns.Add("Rank", typeof(string));
            dtUnit.Columns.Add("Rating", typeof(string));
            dtUnit.Columns.Add("FormDate", typeof(string));

            //CREATE DATATABLE FOR EQUIP
            dtEquip.Columns.Add("ItemID", typeof(string));
            dtEquip.Columns.Add("EquipID", typeof(string));
            dtEquip.Columns.Add("ItemName", typeof(string));
            dtEquip.Columns.Add("ItemCdr", typeof(string));
            dtEquip.Columns.Add("ItemExp", typeof(string));
            dtEquip.Columns.Add("ItemKills", typeof(string));
            dtEquip.Columns.Add("Casualty", typeof(string));
            dtEquip.Columns.Add("ItemDamage", typeof(string));
            dtEquip.Columns.Add("ItemFormDate", typeof(string));
            dtEquip.Columns.Add("ItemNote", typeof(string));

            txtName.Visible = false;
            txtProf.Visible = false;
            txtSupply.Visible = false;
            cboSupport.Visible = false;
            cboOrders.Visible = false;
            cboLossTol.Visible = false;
            txtCdr.Visible = false;
            txtRank.Visible = false;
            txtFormDate.Visible = false;

            //POPULATES REPLACEMENT PRIORITY COMBO BOX
            cboReplace.Items.Add("None");
            cboReplace.Items.Add("Very Low");
            cboReplace.Items.Add("Low");
            cboReplace.Items.Add("Normal");
            cboReplace.Items.Add("High");
            cboReplace.Items.Add("Very High");
        }

        private async void btnCreateTacFile_Click(object sender, EventArgs e)
        {
            string FilePath = TOAWTac.Properties.Settings.Default.FilePath.ToString();
            string dateformat = "dd MMM yyyy";
            string date = DateTimePicker.Value.ToString(dateformat);

            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .gam files *.gam|*.gam";

            if (file.ShowDialog() == DialogResult.OK)
            {
                TOAWTac.Properties.Settings.Default.FilePath = file.FileName;
                TOAWTac.Properties.Settings.Default.Save();

                FilePath = file.FileName;

                trvUnitTree.Nodes.Clear();
                dtFormation.Clear();
                dtUnit.Clear();
                dtEquip.Clear();

                rbForce1.Checked = false;
                rbForce2.Checked = false;

                if (FilePath != "" && FilePath != null)  //THERE IS IS NO "ELSE" TO COVER WHAT IF FILEPATH == "" OR NULL!!
                {
                    ssTac.Visible = true;

                    //CREATE TACFILE
                    tacFile = XElement.Load(FilePath);
                    string TacFilePath = FilePath.Substring(0, FilePath.Length - 3) + "tac";
                    txtTacFile.Text = TacFilePath;
                    TOAWTac.Properties.Settings.Default.TacFilePath = TacFilePath;
                    TOAWTac.Properties.Settings.Default.Save();

                    //RUN ASYNC TO FILL TACFILE
                    await Task.Run(() =>
                    {
                        Random rng = new Random();

                        foreach (XElement force in tacFile.Descendants("OOB").Descendants("FORCE"))
                        {
                            string forceID = force.Attribute("ID").Value;
                            string forcecdrname = Utils.AssignCdrName(forceID, rng);

                            force.Add(new XAttribute("CDR", forcecdrname),
                            new XAttribute("RANK", "COL"),
                            new XAttribute("RATING", "--"));

                            //LIST FORMATIONS
                            foreach (XElement formation in force.Descendants("FORMATION").Where(f => f.Parent.Attribute("ID").Value == forceID))
                            {  //FORMATION
                                //ADD FORMATIONS TO TACFILE
                                string formcdrname = Utils.AssignCdrName(forceID, rng);

                                formation.Add(
                                    new XAttribute("CDR", formcdrname),
                                    new XAttribute("RANK", "CPT"),
                                    new XAttribute("RATING", "--"),
                                    new XAttribute("FORMDATE", date));

                                string formID = formation.Attribute("ID").Value;

                                bool isFirstUnit = true;

                                //LIST UNITS
                                foreach (XElement unit in formation.Descendants("UNIT")
                                    .Where(u => u.Parent.Attribute("ID").Value == formID)
                                    .Where(u => u.Parent.Parent.Attribute("ID").Value == forceID))
                                {  //UNIT
                                    string unitcdrname; 
                                    if (isFirstUnit == true)
                                    {
                                        unitcdrname = formation.Attribute("CDR").Value;
                                        isFirstUnit = false;
                                    }
                                    else
                                    {
                                        unitcdrname = Utils.AssignCdrName(forceID, rng);
                                    }
                                      
                                    //ADD UNITS TO TACFILE
                                    unit.Add(
                                         new XAttribute("CDR", unitcdrname),
                                         new XAttribute("RANK", "LT"),
                                         new XAttribute("RATING", "--"),
                                         new XAttribute("FORMDATE", date));

                                    string unitID = unit.Attribute("ID").Value;

                                    //ADD EQUIP FOR TAC FILE
                                    string equipcdrname;
                                    bool isFirstEqp = true;
                                    int n = 0;

                                    foreach (XElement equip in unit.Descendants("EQUIPMENT")
                                        .Where(u => u.Parent.Attribute("ID").Value == unitID)
                                        .Where(u => u.Parent.Parent.Attribute("ID").Value == formID)
                                        .Where(u => u.Parent.Parent.Parent.Attribute("ID").Value == forceID))
                                    {
                                        int qty = Int32.Parse(equip.Attribute("NUMBER").Value);

                                        for (int i = 1; i <= qty; i++) //ITEM
                                        {
                                            if (isFirstEqp == true)
                                            {
                                                equipcdrname = unit.Attribute("CDR").Value;
                                            }
                                            else if ((isFirstEqp != true) &&
                                            unit.Attribute("ICON").Value == "Air" ||
                                            unit.Attribute("ICON").Value == "Fighter Bomber" ||
                                            unit.Attribute("ICON").Value == "Light Bomber" ||
                                            unit.Attribute("ICON").Value == "Medium Bomber" ||
                                            unit.Attribute("ICON").Value == "Naval Bomber" ||
                                            unit.Attribute("ICON").Value == "Heavy Bomber" ||
                                            unit.Attribute("ICON").Value == "Jet Bomber" ||
                                            unit.Attribute("ICON").Value == "Heavy Jet Bomber" ||
                                            unit.Attribute("ICON").Value == "Fighter" ||
                                            unit.Attribute("ICON").Value == "Jet Fighter" ||
                                            unit.Attribute("ICON").Value == "Naval Fighter" ||
                                            unit.Attribute("ICON").Value == "Riverine" ||
                                            unit.Attribute("ICON").Value == "Light Naval" ||
                                            unit.Attribute("ICON").Value == "Medium Naval" ||
                                            unit.Attribute("ICON").Value == "Naval Task Force" ||
                                            unit.Attribute("ICON").Value == "Naval Attack")
                                            {
                                                equipcdrname = Utils.AssignCdrName(forceID,rng);
                                            }
                                            else
                                            {
                                                equipcdrname = "--";
                                            }
                                            //i = n + i;
                                            n++;

                                            equip.Add(
                                             new XElement("ITEM",
                                             new XAttribute("ID", n),
                                             new XAttribute("NAME", equip.Attribute("NAME").Value),
                                             new XAttribute("ITEMCDR", equipcdrname),
                                             new XAttribute("ITEMEXP", "40"),
                                             new XAttribute("ITEMKILLS", "0"),
                                             new XAttribute("CASUALTY", "None"),
                                             new XAttribute("ITEMDAMAGE", "0"),
                                             new XAttribute("ITEMFORMDATE", date),
                                             new XAttribute("ITEMNOTE", "--")));

                                            isFirstEqp = false;
                                        } //item
                                    } //equip
                                }  //unit
                            } //formation
                        }
                    });

                    txtTacFile.Text = TacFilePath;

                    //ENABLE FORCE RADIO BUTTONS, SET FORCE NAMES
                    rbForce1.Enabled = true;
                    rbForce2.Enabled = true;

                    rbForce1.Text = tacFile.Descendants("HEADER").First().Attribute("forceName1").Value;
                    rbForce2.Text = tacFile.Descendants("HEADER").First().Attribute("forceName2").Value;
                    tacFile.Save(TacFilePath);
                    ssTac.Visible = false;
                }
            }
            else
            {
                TOAWTac.Properties.Settings.Default.FilePath = "";
            }

            btnSync.Enabled = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoadTacFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "*.tac files *.tac|*.tac";

            if (file.ShowDialog() == DialogResult.OK)
            {
                TOAWTac.Properties.Settings.Default.TacFilePath = file.FileName;
                TOAWTac.Properties.Settings.Default.Save();

                TOAWTac.Properties.Settings.Default.FilePath = file.FileName;
                TOAWTac.Properties.Settings.Default.Save();

                tacFile = XElement.Load(file.FileName);
                txtTacFile.Text = file.FileName;

                //ENABLE FORCE RADIO BUTTONS, SET FORCE NAMES
                var forcenames = tacFile.Descendants("HEADER");
                foreach (var f in forcenames)
                {
                    rbForce1.Enabled = true;
                    rbForce2.Enabled = true;

                    string fn1 = f.Attribute("forceName1").Value.ToString();
                    this.rbForce1.Text = fn1;

                    string fn2 = f.Attribute("forceName2").Value.ToString();
                    this.rbForce2.Text = fn2;
                }
                rbForce1.Checked = false;
                rbForce2.Checked = false;
                btnPostBattle.Enabled = true;
                btnSync.Enabled = true;
            }
            else
            {
                TOAWTac.Properties.Settings.Default.TacFilePath = "";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tacFile.Save(txtTacFile.Text);
            trvUnitTree.Focus();
        }

        //private string AssignCdrName(XDocument xdoc, string forceID, Random rng)
        //{
        //    var commanders = xdoc.Descendants("CDRNAMES").Descendants("CDRNAME")
        //        .Where(f => f.Attribute("forceid").Value == forceID);

        //    int cdrnameCount = commanders.Count();
        //    string cdrname = commanders.ElementAt(rng.Next(0, cdrnameCount)).Attribute("cdrname").Value;

        //    return cdrname;
        //}

        private void rbForce1_CheckedChanged(object sender, EventArgs e)
        {
            ssTac.Visible = true;
            pnlFormation.Visible = false;
            pnlUnit.Visible = false;
            pnlForce.Visible = true;

            if (rbForce1.Checked == true)
            {
                forceID = "1";
                LoadTree(forceID);
            }
            trvUnitTree.SelectedNode = trvUnitTree.TopNode;
            ssTac.Visible = false;
        }

        private void rbForce2_CheckedChanged(object sender, EventArgs e)
        {
            ssTac.Visible = true;
            pnlFormation.Visible = false;
            pnlUnit.Visible = false;
            pnlForce.Visible = true;

            if (rbForce2.Checked == true)
            {
                forceID = "2";
                LoadTree(forceID);
            }
            trvUnitTree.SelectedNode = trvUnitTree.TopNode;
            ssTac.Visible = false;
        }

        private void LoadTree(string forceID)
        {
            ssTac.Visible = true;
            XElement xelem;

            if (txtTacFile.Text != "")
            {
                string TacFilePath = TOAWTac.Properties.Settings.Default.TacFilePath.ToString();
                xelem = XElement.Load(TacFilePath);
            }
            else
            {
                return;
            }
            TreeNode forceNode;
            TreeNode formationNode;
            TreeNode unitNode;

            trvUnitTree.Nodes.Clear();


            foreach (XElement force in xelem.Descendants("FORCE").Where(f => f.Attribute("ID").Value == forceID))
            {
                if (force.Attribute("NAME") != null)
                {
                    forceNode = trvUnitTree.Nodes.Add(force.Attribute("NAME").Value);
                    forceNode.Tag = force.Attribute("ID").Value;
                    forceNode.Name = "FORCE";
                    HideCheckBox(trvUnitTree, forceNode);
                }

                forceNode = trvUnitTree.TopNode;

                foreach (XElement formation in force.Descendants("FORMATION"))
                {
                    if (force.Attribute("NAME").Value != null)
                    {
                        formationNode = forceNode.Nodes.Add(formation.Attribute("NAME").Value);
                        formationNode.Tag = formation.Attribute("ID").Value;
                        formationNode.Name = "FORMATION";
                        HideCheckBox(trvUnitTree, formationNode);

                        //###CHANGE TREENODE COLOR IF FORMATION IS STATIC
                        if (formation.Attribute("ORDERS").Value == "Static" || formation.Attribute("ORDERS").Value == "Wait" || formation.Attribute("ORDERS").Value == "Delay" || formation.Attribute("ORDERS").Value == "Hold" || formation.Attribute("ORDERS").Value == "Manual" || formation.Attribute("ORDERS").Value == "Garrison")
                        {
                            formationNode.ForeColor = System.Drawing.Color.IndianRed;
                            Font f = new Font(trvUnitTree.Font, FontStyle.Bold);
                            formationNode.NodeFont = f;
                        }
                    }
                    else
                    {
                        formationNode = null;
                    }

                    foreach (XElement unit in formation.Descendants("UNIT"))
                    {
                        unitNode = formationNode.Nodes.Add(unit.Attribute("NAME").Value);
                        unitNode.Tag = unit.Attribute("ID").Value;
                        unitNode.Name = "UNIT";
                        //+++CHANGE TREENODE FONT COLOR IF UNIT IS DIVIDED
                        if (unit.Attribute("STATUS").Value == "24")
                        {
                            unitNode.ForeColor = System.Drawing.Color.Gray;
                        }

                        //***CHANGE TREENODE FONT COLOR IF UNIT IS SUBUNIT
                        if (unit.Attribute("PARENT") != null)
                        {
                            unitNode.ForeColor = System.Drawing.Color.CornflowerBlue;
                            Font f = new Font(trvUnitTree.Font, FontStyle.Bold);
                            unitNode.NodeFont = f;
                        }

                        //^^^CHANGE TREENODE FONT COLOR IF UNIT IS REINFORCEMENT
                        if (unit.Attribute("X") == null && unit.Attribute("STATUS").Value != "24")
                        {
                            unitNode.ForeColor = System.Drawing.Color.ForestGreen;
                            Font f = new Font(trvUnitTree.Font, FontStyle.Bold);
                            unitNode.NodeFont = f;
                        }

                        foreach (XElement equip in unit.Descendants("EQUIPMENT"))
                        {
                            unitNode.Tag = unit.Attribute("ID").Value;
                            TreeNode equipTnode = unitNode.Nodes.Add(equip.Attribute("NAME").Value + " x" + equip.Attribute("NUMBER").Value + " [" + equip.Attribute("MAX").Value + "]");
                            equipTnode.Tag = equip.Attribute("ID").Value;
                            equipTnode.Name = "EQUIPMENT";
                            HideCheckBox(trvUnitTree, equipTnode);
                        }
                    }
                }
            }

            trvUnitTree.Nodes[0].Expand();
            ssTac.Visible = false;

            drForce.DataSource = dtFormation;
        }

        private void HideCheckBox(TreeView tvw, TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, ref tvi);
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            ///WHAT FOR?
            var checkedlist = new List<TreeNode>();
            GetCheckedNodes(trvUnitTree.Nodes, checkedlist);
            ///

            string dateformat = "dd MMM yyyy";
            string date = DateTimePicker.Value.ToString(dateformat);
            string forceID;
            string unitID;

            List<string> toRemove = new System.Collections.Generic.List<string>();

            string TacFileName = txtTacFile.Text;

            XElement gamFile = XElement.Load(TacFileName);

            //[STEP THROUGH TACFILE TO MATCH NAMES??]

            foreach (XElement force in gamFile.Descendants("OOB").Descendants("FORCE"))
            {
                XAttribute attCdr = force.Attribute("CDR");
                XAttribute attRank = force.Attribute("RANK");
                XAttribute attRating = force.Attribute("RATING");

                forceID = force.Attribute("ID").Value;
                
                attCdr.Remove();
                attRank.Remove();
                attRating.Remove();

                foreach (XElement formation in force.Descendants("FORMATION"))
                {
                    XAttribute attFormCdr = formation.Attribute("CDR");
                    XAttribute attFormRank = formation.Attribute("RANK");
                    XAttribute attFormRating = formation.Attribute("RATING");
                    XAttribute attFormDate = formation.Attribute("FORMDATE");

                    attFormCdr.Remove();
                    attFormRank.Remove();
                    attFormRating.Remove();
                    attFormDate.Remove();

                    foreach (XElement unit in formation.Descendants("UNIT"))
                    {
                        int u = 0; //number of equipment items in unit

                        unitID = unit.Attribute("ID").Value;

                        XAttribute attUnitCdr = unit.Attribute("CDR");
                        XAttribute attUnitRank = unit.Attribute("RANK");
                        XAttribute attUnitRating = unit.Attribute("RATING");
                        XAttribute attUnitDate = unit.Attribute("FORMDATE");

                        attUnitCdr.Remove();
                        attUnitRank.Remove();
                        attUnitRating.Remove();
                        attUnitDate.Remove();

                        foreach (XElement equipment in unit.Descendants("EQUIPMENT"))
                        {
                            float f = 0;  ///number of items in equipment

                            foreach (XElement item in equipment.Descendants("ITEM"))
                            {
                                if (item.Attribute("CASUALTY").Value == "None")
                                {
                                    f++;
                                    u++;
                                }
                                if (item.Attribute("CASUALTY").Value == "Half")
                                {
                                    f = f + 0.5f;
                                }
                            }//item

                            int i = (int)Math.Round(f, MidpointRounding.AwayFromZero); 

                            equipment.Attribute("NUMBER").Value = i.ToString();
                            equipment.Descendants("ITEM").Remove();
                        }//equipment
                        if (u == 0) toRemove.Add(unitID);
                    }//unit-5.0
                }//formation

                if (toRemove.Count > 0) //IF AT LEAST ONE UNIT HAS NO EQP REMAINING
                {
                    //REMOVE UNITS WITH NO ITEMS
                    foreach (string uid in toRemove)
                    {
                        DeleteUnit(gamFile, forceID, uid);
                    }

                    ////[RENUMBER UNIT IDs IF AT LEAST ONE UNIT IS DELETED]
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT";
                    var allunits = gamFile.XPathSelectElements(xpath);
                    Renumbering.RenumberAll(allunits);
                }
                toRemove.Clear();

            }//force

            string GamFileName = TacFileName.Substring(0, TacFileName.Length - 4) + " " + date + ".gam";
            txtGamFile.Text = GamFileName;
            gamFile.Save(GamFileName);
        }

        public void GetCheckedNodes(TreeNodeCollection nodes, List<TreeNode> list)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Checked)
                {
                    list.Add(node);
                    //Console.WriteLine("name: " + node.Text + " ID: " + node.Tag);
                }

                GetCheckedNodes(node.Nodes, list);
            }
        }

        private void trvUnitTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string equipid = trvUnitTree.SelectedNode.Tag.ToString();

            string replacePriority = "";
            string iconID = "";
            string icon = "";
            string orderID = "";
            bool isAirUnit;
            string iconDisplay = "";
            string unitorders = "";

            dtFormation.Clear();
            dtUnit.Clear();
            dtEquip.Clear();

            drFormation.DataSource = null;

            int treeLevel = trvUnitTree.SelectedNode.Level;

            trvUnitTree.Update();

            switch (treeLevel)
            {
                case 0:  //IF FORCE SELECTED
                    //SET CONTROL VISIBILITY
                    drForce.Visible = true;
                    drFormation.Visible = false;
                    drUnit.Visible = false;

                    pnlForce.Visible = true;
                    pnlForce.Location = new Point(221, 33);
                    pnlForce.Size = new Size(1097, 46);
                    pnlFormation.Visible = false;
                    pnlUnit.Visible = false;

                    txtName.Visible = true;
                    txtProf.Visible = true;
                    txtSupply.Visible = true;
                    cboSupport.Visible = true;
                    cboOrders.Visible = true;
                    cboLossTol.Visible = true;
                    txtCdr.Visible = true;
                    txtRank.Visible = true;
                    txtFormDate.Visible = true;

                    //CLEAR CONTROL DATABINDINGS
                    txtName.DataBindings.Clear();
                    txtProf.DataBindings.Clear();
                    txtSupply.DataBindings.Clear();
                    cboSupport.DataBindings.Clear();
                    cboOrders.DataBindings.Clear();
                    cboLossTol.DataBindings.Clear();
                    lblFormID.DataBindings.Clear();
                    txtCdr.DataBindings.Clear();
                    txtRank.DataBindings.Clear();
                    txtRating.DataBindings.Clear();
                    txtFormDate.DataBindings.Clear();

                    //XPATH FOR OOB PORTION OF XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]";
                    var force = tacFile.XPathSelectElement(xpath);

                    //XPATH FOR FORCE VARIABLES PORTION OF XML
                    string xpathforcevariables = "FORCEVARIABLES/FORCE[@ID =" + forceID + "]";

                    var forcevariables = tacFile.XPathSelectElement(xpathforcevariables);

                    //SET CONTROLS ON HEADER
                    if (forceID == "1")
                    {
                        txtHdrForceName.Text = rbForce1.Text;
                    }
                    else if (forceID == "2")
                    {
                        txtHdrForceName.Text = rbForce2.Text;
                    }
                    txtHdrForceProf.Text = force.Attribute("proficiency").Value;
                    txtHdrForceSupply.Text = force.Attribute("supply").Value;
                    txtHdrForceCdr.Text = force.Attribute("CDR").Value;
                    txtHdrForceRank.Text = force.Attribute("RANK").Value;
                    txtHdrForceRating.Text = force.Attribute("RATING").Value;

                    //SET DATA BINDINGS
                    txtName.DataBindings.Add("Text", dtFormation, "Name");
                    txtProf.DataBindings.Add("Text", dtFormation, "Prof");
                    txtSupply.DataBindings.Add("Text", dtFormation, "Supply");
                    cboSupport.DataBindings.Add("Text", dtFormation, "Support");
                    cboOrders.DataBindings.Add("Text", dtFormation, "Orders");
                    cboLossTol.DataBindings.Add("Text", dtFormation, "LossTol");
                    lblFormID.DataBindings.Add("Text", dtFormation, "FormID");
                    txtCdr.DataBindings.Add("Text", dtFormation, "Cdr");
                    txtRank.DataBindings.Add("Text", dtFormation, "Rank");
                    txtRating.DataBindings.Add("Text", dtFormation, "Rating");
                    txtFormDate.DataBindings.Add("Text", dtFormation, "FormDate");

                    foreach (XElement formation in force.Descendants("FORMATION").Where(f => f.Parent.Attribute("ID").Value == forceID))
                    {
                        dtFormation.Rows.Add(formation.Attribute("NAME").Value, formation.Attribute("PROFICIENCY").Value,
                            formation.Attribute("SUPPLY").Value, formation.Attribute("SUPPORTSCOPE").Value,
                            formation.Attribute("ORDERS").Value, formation.Attribute("EMPHASIS").Value, formation.Attribute("ID").Value,
                            formation.Attribute("CDR").Value, formation.Attribute("RANK").Value, formation.Attribute("RATING").Value,
                            formation.Attribute("FORMDATE").Value);
                    }

                    drForce.DataSource = dtFormation;

                    break;

                case 1: //IF FORMATION SELECTED

                    drForce.Visible = false;
                    drFormation.Visible = true;
                    drFormation.Location = new Point(216, 84);
                    drFormation.Size = new Size(1097, 500);
                    drUnit.Visible = false;

                    pnlForce.Visible = false;
                    pnlFormation.Visible = true;
                    pnlFormation.Location = new Point(221, 33);
                    pnlFormation.Size = new Size(1097, 46);
                    pnlUnit.Visible = false;

                    txtUnitName.Visible = true;
                    txtUnitProf.Visible = true;
                    txtUnitSupply.Visible = true;
                    txtUnitReadiness.Visible = true;
                    cboUnitLossTol.Visible = true;
                    cboUnitSize.Visible = true;
                    cboUnitType.Visible = true;
                    cboExp.Visible = true;
                    cboReplace.Visible = true;
                    cboUnitOrders.Visible = true;
                    txtUnitCdr.Visible = true;
                    txtUnitRank.Visible = true;
                    txtUnitDate.Visible = true;

                    xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
                    var units = tacFile.XPathSelectElements(xpath);

                    //SET HEADER DATA
                    txtHdrFormName.Text = units.First().Attribute("NAME").Value;
                    txtHdrFormProf.Text = units.First().Attribute("PROFICIENCY").Value;
                    txtHdrFormSupply.Text = units.First().Attribute("SUPPLY").Value;
                    cboHdrFormSupport.Text = units.First().Attribute("SUPPORTSCOPE").Value;
                    cboHdrFormOrders.Text = units.First().Attribute("ORDERS").Value;
                    cboHdrFormLossTol.Text = units.First().Attribute("EMPHASIS").Value;
                    txtHdrFormCdr.Text = units.First().Attribute("CDR").Value;
                    txtHdrFormRank.Text = units.First().Attribute("RANK").Value;
                    txtHdrFormRating.Text = units.First().Attribute("RATING").Value;
                    txtHdrFormDate.Text = units.First().Attribute("FORMDATE").Value;

                    //SET DATABINDINGS
                    txtUnitName.DataBindings.Clear();
                    txtUnitProf.DataBindings.Clear();
                    txtUnitSupply.DataBindings.Clear();
                    txtUnitReadiness.DataBindings.Clear();
                    cboUnitOrders.DataBindings.Clear();
                    cboUnitLossTol.DataBindings.Clear();
                    cboUnitSize.DataBindings.Clear();
                    cboUnitType.DataBindings.Clear();
                    cboExp.DataBindings.Clear();
                    cboReplace.DataBindings.Clear();
                    lblUnitID.DataBindings.Clear();
                    txtUnitCdr.DataBindings.Clear();
                    txtUnitRank.DataBindings.Clear();
                    txtUnitRating.DataBindings.Clear();
                    txtUnitDate.DataBindings.Clear();

                    txtUnitName.DataBindings.Add("Text", dtUnit, "UnitName");
                    txtUnitProf.DataBindings.Add("Text", dtUnit, "UnitProf");
                    txtUnitSupply.DataBindings.Add("Text", dtUnit, "UnitSupply");
                    cboUnitOrders.DataBindings.Add("Text", dtUnit, "UnitOrders");
                    cboUnitLossTol.DataBindings.Add("Text", dtUnit, "UnitLossTol");
                    txtUnitReadiness.DataBindings.Add("Text", dtUnit, "UnitReadiness");
                    cboUnitType.DataBindings.Add("Text", dtUnit, "UnitType");
                    cboUnitSize.DataBindings.Add("Text", dtUnit, "UnitSize");
                    cboExp.DataBindings.Add("Text", dtUnit, "UnitExp");
                    cboReplace.DataBindings.Add("Text", dtUnit, "UnitReplace");
                    lblUnitID.DataBindings.Add("Text", dtUnit, "UnitID");
                    txtUnitCdr.DataBindings.Add("Text", dtUnit, "Cdr");
                    txtUnitRank.DataBindings.Add("Text", dtUnit, "Rank");
                    txtUnitRating.DataBindings.Add("Text", dtUnit, "Rating");
                    txtUnitDate.DataBindings.Add("Text", dtUnit, "FormDate");

                    drFormation.DataSource = dtUnit;

                    foreach (XElement unit in units.Descendants("UNIT"))
                    {
                        replacePriority = GetReplacementPriorityText(unit.Attribute("REPLACEMENTPRIORITY").Value);

                        iconID = "";
                        orderID = unit.Attribute("STATUS").Value;
                        icon = unit.Attribute("ICON").Value;
                        isAirUnit = IsAirUnit(icon);

                        if (unit.Attribute("ICONID") != null) iconID = unit.Attribute("ICONID").Value;
                        iconDisplay = GetIconText(iconID, icon);

                        unitorders = SetUnitOrders(isAirUnit, orderID);

                        dtUnit.Rows.Add(unit.Attribute("NAME").Value, unit.Attribute("PROFICIENCY").Value,
                            unit.Attribute("SUPPLY").Value, unitorders, unit.Attribute("EMPHASIS").Value,
                            unit.Attribute("READINESS").Value, iconDisplay, unit.Attribute("SIZE").Value,
                            unit.Attribute("EXPERIENCE").Value, replacePriority, unit.Attribute("ID").Value,
                            unit.Attribute("CDR").Value, unit.Attribute("RANK").Value, unit.Attribute("RATING").Value,
                            unit.Attribute("FORMDATE").Value);
                    }

                    drFormation.DataSource = dtUnit;
                    break;

                case 2:  //IF UNIT SELECTED
                    drForce.Visible = false;
                    drFormation.Visible = false;
                    drUnit.Location = new Point(216, 84);
                    drUnit.Size = new Size(1097, 500);
                    drUnit.Visible = true;

                    //SET DATA BINDING FOR CBOHDRUNITTYPE
                    var icons = new BindingList<KeyValuePair<string, string>>();

                    icons.Add(new KeyValuePair<string, string>("Air", "Air"));
                    icons.Add(new KeyValuePair<string, string>("Anti Aircraft", "AA"));
                    icons.Add(new KeyValuePair<string, string>("Airmobile Anti Air", "AA (Airmob)"));
                    icons.Add(new KeyValuePair<string, string>("Motor Anti Air", "AA (Mot)"));
                    icons.Add(new KeyValuePair<string, string>("Parachute Anti Air", "AA (Para)"));
                    icons.Add(new KeyValuePair<string, string>("Airmobile", "Airmobile"));
                    icons.Add(new KeyValuePair<string, string>("Amphibious", "Amphibious"));
                    icons.Add(new KeyValuePair<string, string>("Antitank", "Antitank [v1]"));
                    icons.Add(new KeyValuePair<string, string>("Antitank", "Antitank [v2]"));
                    icons.Add(new KeyValuePair<string, string>("Armored Antitank", "Antitank (Armored)"));
                    icons.Add(new KeyValuePair<string, string>("Airmobile Antitank", "Antitank (Airmob)"));
                    icons.Add(new KeyValuePair<string, string>("Glider Antitank", "Antitank (Glider)"));
                    icons.Add(new KeyValuePair<string, string>("Hvy Antitank", "Antitank (Heavy)"));
                    icons.Add(new KeyValuePair<string, string>("Motor Antitank", "Antitank (Mot) [v1]"));
                    icons.Add(new KeyValuePair<string, string>("Motor Antitank", "Antitank (Mot) [v2]"));
                    icons.Add(new KeyValuePair<string, string>("Parachute Antitank", "Antitank (Para)"));
                    icons.Add(new KeyValuePair<string, string>("Tank", "Armor"));
                    icons.Add(new KeyValuePair<string, string>("Amphibious Armor", "Armor (Amphib)"));
                    icons.Add(new KeyValuePair<string, string>("Assault Gun", "Armor (Asslt Gun)"));
                    icons.Add(new KeyValuePair<string, string>("Glider Tank", "Armor (Glider)"));
                    icons.Add(new KeyValuePair<string, string>("Hvy Armor", "Armor (Heavy)"));
                    icons.Add(new KeyValuePair<string, string>("Armored Train", "Armored Train"));
                    icons.Add(new KeyValuePair<string, string>("Artillery", "Artillery"));
                    icons.Add(new KeyValuePair<string, string>("Airborne Artillery", "Artillery (Abn)"));
                    icons.Add(new KeyValuePair<string, string>("Airmobile Arty", "Artillery (Airmob)"));
                    icons.Add(new KeyValuePair<string, string>("Armored Artillery", "Artillery (Armored)"));
                    icons.Add(new KeyValuePair<string, string>("Armored Hvy Arty", "Artillery (Arm, Hvy)"));
                    icons.Add(new KeyValuePair<string, string>("Chemical Artillery", "Artillery (Chem)"));
                    icons.Add(new KeyValuePair<string, string>("Coastal Artillery", "Artillery (Coast) [icon]"));
                    icons.Add(new KeyValuePair<string, string>("Coastal Artillery", "Artillery (Coast) [silh]"));
                    icons.Add(new KeyValuePair<string, string>("Fixed Artillery", "Artillery (Fixed)"));
                    icons.Add(new KeyValuePair<string, string>("Glider Artillery", "Artillery (Glider)"));
                    icons.Add(new KeyValuePair<string, string>("Hvy Artillery", "Artillery (Heavy)"));
                    icons.Add(new KeyValuePair<string, string>("Horse Artillery", "Artillery (Horse)"));
                    icons.Add(new KeyValuePair<string, string>("Inf Artillery", "Artillery (Infantry)"));
                    icons.Add(new KeyValuePair<string, string>("Missile Artillery", "Artillery (Missile)"));
                    icons.Add(new KeyValuePair<string, string>("Motor Artillery", "Artillery (Mot)"));
                    icons.Add(new KeyValuePair<string, string>("Rail Artillery", "Artillery (Rail)"));
                    icons.Add(new KeyValuePair<string, string>("Rocket Artillery", "Artillery (Rocket)"));
                    icons.Add(new KeyValuePair<string, string>("Motor Rocket", "Artillery (Rocket, Mot)"));
                    icons.Add(new KeyValuePair<string, string>("Bicycle", "Bicycle"));
                    icons.Add(new KeyValuePair<string, string>("Heavy Bomber", "Bomber (Heavy) [icon]"));
                    icons.Add(new KeyValuePair<string, string>("Heavy Bomber", "Bomber (Heavy) [silh]"));
                    icons.Add(new KeyValuePair<string, string>("Jet Bomber", "Bomber (Jet)"));
                    icons.Add(new KeyValuePair<string, string>("Jet Heavy Bomber", "Bomber (Jet, Heavy)"));
                    icons.Add(new KeyValuePair<string, string>("Light Bomber", "Bomber (Light) [icon]"));
                    icons.Add(new KeyValuePair<string, string>("Light Bomber", "Bomber (Light) [silh]"));
                    icons.Add(new KeyValuePair<string, string>("Medium Bomber", "Bomber (Medium)"));
                    icons.Add(new KeyValuePair<string, string>("Naval Bomber", "Bomber (Naval)"));
                    icons.Add(new KeyValuePair<string, string>("Border", "Border"));
                    icons.Add(new KeyValuePair<string, string>("Cavalry", "Cavalry"));
                    icons.Add(new KeyValuePair<string, string>("Airmobile Cavalry", "Cavalry (Airmob)"));
                    icons.Add(new KeyValuePair<string, string>("Armored Cavalry", "Cavalry (Armored)"));
                    icons.Add(new KeyValuePair<string, string>("Motor Cavalry", "Cavalry (Mot)"));
                    icons.Add(new KeyValuePair<string, string>("Mountain Cavalry", "Cavalry (Mtn)"));
                    icons.Add(new KeyValuePair<string, string>("Civilian", "Civilian"));
                    icons.Add(new KeyValuePair<string, string>("Embarked Air", "Embarked Air"));
                    icons.Add(new KeyValuePair<string, string>("Embarked Heli", "Embarked Heli"));
                    icons.Add(new KeyValuePair<string, string>("Embarked Naval", "Embarked Naval"));
                    icons.Add(new KeyValuePair<string, string>("Embarked Rail", "Embarked Rail"));
                    icons.Add(new KeyValuePair<string, string>("Engineer", "Engineer"));
                    icons.Add(new KeyValuePair<string, string>("Airborne Engineer", "Engineer (Abn)"));
                    icons.Add(new KeyValuePair<string, string>("Airmobile Engineer", "Engineer (Airmob)"));
                    icons.Add(new KeyValuePair<string, string>("Armored Engineer", "Engineer (Armored)"));
                    icons.Add(new KeyValuePair<string, string>("Ferry Engineer", "Engineer (Ferry)"));
                    icons.Add(new KeyValuePair<string, string>("Motor Engineer", "Engineer (Mot)"));
                    icons.Add(new KeyValuePair<string, string>("Fighter", "Fighter [icon]"));
                    icons.Add(new KeyValuePair<string, string>("Fighter", "Fighter [silh]"));
                    icons.Add(new KeyValuePair<string, string>("Jet Fighter", "Fighter (Jet)"));
                    icons.Add(new KeyValuePair<string, string>("Naval Fighter", "Fighter (Naval)"));
                    icons.Add(new KeyValuePair<string, string>("Fighter Bomber", "Fighter Bomber [icon]"));
                    icons.Add(new KeyValuePair<string, string>("Fighter Bomber", "Fighter Bomber [silh]"));
                    icons.Add(new KeyValuePair<string, string>("Garrison", "Garrison"));
                    icons.Add(new KeyValuePair<string, string>("Guerilla", "Guerilla"));
                    icons.Add(new KeyValuePair<string, string>("Headquarters", "Headquarters [v1]"));
                    icons.Add(new KeyValuePair<string, string>("Headquarters", "Headquarters [v2]"));
                    icons.Add(new KeyValuePair<string, string>("Airmobile Hvy Wpns", "Heavy Wpns (Airmob)"));
                    icons.Add(new KeyValuePair<string, string>("Mountain Cav Hvy Wpns", "Heavy Wpns (Mtn Cav)"));
                    icons.Add(new KeyValuePair<string, string>("Glider Hvy Wpns", "Heavy Wpns (Glider)"));
                    icons.Add(new KeyValuePair<string, string>("Infantry Hvy Wpns", "Heavy Wpns (Infantry)"));
                    icons.Add(new KeyValuePair<string, string>("Motor Hvy Wpns", "Heavy Wpns (Mot)"));
                    icons.Add(new KeyValuePair<string, string>("Mountain Hvy Wpns", "Heavy Wpns (Mtn)"));
                    icons.Add(new KeyValuePair<string, string>("Parachute Hvy Wpns", "Heavy Wpns (Para)"));
                    icons.Add(new KeyValuePair<string, string>("Attack Helicopter", "Helicopter (Attack)"));
                    icons.Add(new KeyValuePair<string, string>("Recon Helicopter", "Helicopter (Recon)"));
                    icons.Add(new KeyValuePair<string, string>("Trans Helicopter", "Helicopter (Transport)"));
                    icons.Add(new KeyValuePair<string, string>("Infantry", "Infantry"));
                    icons.Add(new KeyValuePair<string, string>("Airmobile Infantry", "Infantry (Airmob)"));
                    icons.Add(new KeyValuePair<string, string>("Glider Infantry", "Infantry (Glider)"));
                    icons.Add(new KeyValuePair<string, string>("Marine Infantry", "Infantry (Marine)"));
                    icons.Add(new KeyValuePair<string, string>("Mechanized", "Infantry (Mech)"));
                    icons.Add(new KeyValuePair<string, string>("Motor Infantry", "Infantry (Mot)"));
                    icons.Add(new KeyValuePair<string, string>("Mountain Infantry", "Infantry (Mtn)"));
                    icons.Add(new KeyValuePair<string, string>("Parachute Infantry", "Infantry (Para)"));
                    icons.Add(new KeyValuePair<string, string>("Irregular", "Irregular"));
                    icons.Add(new KeyValuePair<string, string>("Machine Gun", "Machine Gun"));
                    icons.Add(new KeyValuePair<string, string>("Motor Machinegun", "Machine Gun (Mot)"));
                    icons.Add(new KeyValuePair<string, string>("Military Police", "Military Police"));
                    icons.Add(new KeyValuePair<string, string>("Mortar", "Mortar"));
                    icons.Add(new KeyValuePair<string, string>("Hvy Mortar", "Mortar (Heavy)"));
                    icons.Add(new KeyValuePair<string, string>("Carrier Naval", "Naval (Carrier)"));
                    icons.Add(new KeyValuePair<string, string>("Heavy Naval", "Naval (Heavy)"));
                    icons.Add(new KeyValuePair<string, string>("Light Naval", "Naval (Light)"));
                    icons.Add(new KeyValuePair<string, string>("Medium Naval", "Naval (Medium)"));
                    icons.Add(new KeyValuePair<string, string>("Riverine", "Naval (Riverine)"));
                    icons.Add(new KeyValuePair<string, string>("Naval Task Force", "Naval (Task Force)"));
                    icons.Add(new KeyValuePair<string, string>("Naval Attack", "Naval Attack Aircraft"));
                    icons.Add(new KeyValuePair<string, string>("Parachute", "Parachute"));
                    icons.Add(new KeyValuePair<string, string>("Railroad Repair", "Railroad Repair"));
                    icons.Add(new KeyValuePair<string, string>("Airborne Recon", "Recon (Airborne)"));
                    icons.Add(new KeyValuePair<string, string>("Armored Recon", "Recon (Armored)"));
                    icons.Add(new KeyValuePair<string, string>("Glider Recon", "Recon (Glider)"));
                    icons.Add(new KeyValuePair<string, string>("Reserve", "Reserve"));
                    icons.Add(new KeyValuePair<string, string>("Security", "Security"));
                    icons.Add(new KeyValuePair<string, string>("Ski", "Ski"));
                    icons.Add(new KeyValuePair<string, string>("Special Forces", "Special Forces"));
                    icons.Add(new KeyValuePair<string, string>("Supply", "Supply"));
                    icons.Add(new KeyValuePair<string, string>("Transport", "Transport [icon]"));
                    icons.Add(new KeyValuePair<string, string>("Transport", "Transport [silh]"));
                    icons.Add(new KeyValuePair<string, string>("Amphib Transport", "Transport (Amphib)"));
                    icons.Add(new KeyValuePair<string, string>("Task Force", "Task Force"));
                    icons.Add(new KeyValuePair<string, string>("Battlegroup", "Battle Group"));
                    icons.Add(new KeyValuePair<string, string>("Kampfgruppe", "Kampfgruppe"));
                    icons.Add(new KeyValuePair<string, string>("Combat Command A", "Combat Command A"));
                    icons.Add(new KeyValuePair<string, string>("Combat Command B", "Combat Command B"));
                    icons.Add(new KeyValuePair<string, string>("Combat Command C", "Combat Command C"));
                    icons.Add(new KeyValuePair<string, string>("Combat Command R", "Combat Command R"));

                    cboHdrUnitType.DataSource = icons;
                    cboHdrUnitType.ValueMember = "Key";
                    cboHdrUnitType.DisplayMember = "Value";

                    //SET HEADER PANEL VISIBILITY
                    pnlForce.Visible = false;
                    pnlFormation.Visible = false;
                    pnlUnit.Visible = true;
                    pnlUnit.Location = new Point(221, 33);
                    pnlUnit.Size = new Size(1097, 46);

                    dtEquip.Clear();

                    xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                    XElement equipment = tacFile.XPathSelectElement(xpath);

                    //SET HEADER PANEL DATA
                    txtHdrUnitName.Text = equipment.Attribute("NAME").Value;
                    txtHdrUnitProf.Text = equipment.Attribute("PROFICIENCY").Value;
                    txtHdrUnitSupply.Text = equipment.Attribute("SUPPLY").Value;
                    txtHdrUnitReady.Text = equipment.Attribute("READINESS").Value;
                    cboHdrUnitSize.Text = equipment.Attribute("SIZE").Value;
                    cboHdrUnitLossTol.Text = equipment.Attribute("EMPHASIS").Value;
                    cboHdrUnitExp.Text = equipment.Attribute("EXPERIENCE").Value;
                    txtHdrUnitCdr.Text = equipment.Attribute("CDR").Value;
                    txtHdrUnitRank.Text = equipment.Attribute("RANK").Value;
                    txtHdrUnitRating.Text = equipment.Attribute("RATING").Value;
                    txtHdrUnitDate.Text = equipment.Attribute("FORMDATE").Value;

                    replacePriority = GetReplacementPriorityText(equipment.Attribute("REPLACEMENTPRIORITY").Value);
                    iconID = "";
                    orderID = equipment.Attribute("STATUS").Value;
                    icon = equipment.Attribute("ICON").Value;
                    isAirUnit = IsAirUnit(icon);
                    if (equipment.Attribute("ICONID") != null) iconID = equipment.Attribute("ICONID").Value;
                    unitorders = SetUnitOrders(isAirUnit, orderID);

                    List<string> iconsVariants = new List<string>();
                    iconsVariants.Add("Headquarters");
                    iconsVariants.Add("Antitank");
                    iconsVariants.Add("Motor Antitank");
                    iconsVariants.Add("Fighter");
                    iconsVariants.Add("Fighter Bomber");
                    iconsVariants.Add("Light Bomber");
                    iconsVariants.Add("Heavy Bomber");
                    iconsVariants.Add("Coastal Artillery");
                    iconsVariants.Add("Transport");

                    icon = equipment.Attribute("ICON").Value;
                    bool hasVariant = iconsVariants.Any(v => v == icon);

                    if (!hasVariant)
                    {
                        cboHdrUnitType.SelectedValue = equipment.Attribute("ICON").Value;
                    }
                    else if (hasVariant)
                    {
                        switch (icon)
                        {
                            case "Antitank":
                                if (equipment.Attribute("ICONID").Value == "14")
                                {
                                    cboHdrUnitType.Text = "Antitank [v1]";
                                }
                                else if (equipment.Attribute("ICONID").Value == "15")
                                {
                                    cboHdrUnitType.Text = "Antitank [v2]";
                                }
                                break;

                            case "Motor Antitank":
                                if (equipment.Attribute("ICONID").Value == "25")
                                {
                                    cboHdrUnitType.Text = "Antitank (Mot) [v1]";
                                }
                                else if (equipment.Attribute("ICONID").Value == "26")
                                {
                                    cboHdrUnitType.Text = "Antitank (Mot) [v2]";
                                }
                                break;

                            case "Coastal Artillery":
                                if (equipment.Attribute("ICONID").Value == "62")
                                {
                                    cboHdrUnitType.Text = "Artillery (Coast) [icon]";
                                }
                                else if (equipment.Attribute("ICONID").Value == "63")
                                {
                                    cboHdrUnitType.Text = "Artillery (Coast) [silh]";
                                }
                                break;

                            case "Heavy Bomber":
                                if (equipment.Attribute("ICONID").Value == "69")
                                {
                                    cboHdrUnitType.Text = "Bomber (Heavy) [icon]";
                                }
                                else if (equipment.Attribute("ICONID").Value == "45")
                                {
                                    cboHdrUnitType.Text = "Bomber (Heavy) [silh]";
                                }
                                break;

                            case "Light Bomber":
                                if (equipment.Attribute("ICONID").Value == "43")
                                {
                                    cboHdrUnitType.Text = "Bomber (Light) [icon]";
                                }
                                else if (equipment.Attribute("ICONID").Value == "68")
                                {
                                    cboHdrUnitType.Text = "Bomber (Light) [silh]";
                                }
                                break;

                            case "Fighter":
                                if (equipment.Attribute("ICONID").Value == "41")
                                {
                                    cboHdrUnitType.Text = "Fighter [icon]";
                                }
                                else if (equipment.Attribute("ICONID").Value == "66")
                                {
                                    cboHdrUnitType.Text = "Fighter [silh]";
                                }
                                break;

                            case "Fighter Bomber":
                                if (equipment.Attribute("ICONID").Value == "42")
                                {
                                    cboHdrUnitType.Text = "Fighter Bomber [icon]";
                                }
                                else if (equipment.Attribute("ICONID").Value == "67")
                                {
                                    cboHdrUnitType.Text = "Fighter Bomber [silh]";
                                }
                                break;

                            case "Headquarters":
                                if (equipment.Attribute("ICONID").Value == "0")
                                {
                                    cboHdrUnitType.Text = "Headquarters [v1]";
                                }
                                else if (equipment.Attribute("ICONID").Value == "1")
                                {
                                    cboHdrUnitType.Text = "Headquarters [v2]";
                                }
                                break;

                            case "Transport":
                                if (equipment.Attribute("ICONID").Value == "82")
                                {
                                    cboHdrUnitType.Text = "Transport [icon]";
                                }
                                else if (equipment.Attribute("ICONID").Value == "94")
                                {
                                    cboHdrUnitType.Text = "Transport [silh]";
                                }
                                break;
                        }
                    }

                    cboHdrUnitOrders.Text = unitorders;
                    cboHdrUnitReplace.Text = replacePriority;

                    //SET EQUIPMENT DATA REPEATER DATA
                    txtItemID.DataBindings.Clear();
                    txtItemName.DataBindings.Clear();
                    txtItemCdr.DataBindings.Clear();
                    txtItemExp.DataBindings.Clear();
                    txtItemKills.DataBindings.Clear();
                    cboCasualty.DataBindings.Clear();
                    txtItemDamage.DataBindings.Clear();
                    txtItemFormDate.DataBindings.Clear();
                    txtItemNote.DataBindings.Clear();
                    lblEquipID.DataBindings.Clear();

                    drUnit.DataSource = dtEquip;
                    txtItemID.DataBindings.Add("Text", dtEquip, "ItemID");
                    lblEquipID.DataBindings.Add("Text", dtEquip, "EquipID");
                    txtItemName.DataBindings.Add("Text", dtEquip, "ItemName");
                    txtItemCdr.DataBindings.Add("Text", dtEquip, "ItemCdr");
                    txtItemExp.DataBindings.Add("Text", dtEquip, "ItemExp");
                    txtItemKills.DataBindings.Add("Text", dtEquip, "ItemKills");
                    cboCasualty.DataBindings.Add("Text", dtEquip, "Casualty");
                    txtItemDamage.DataBindings.Add("Text", dtEquip, "ItemDamage");
                    txtItemFormDate.DataBindings.Add("Text", dtEquip, "ItemFormDate");
                    txtItemNote.DataBindings.Add("Text", dtEquip, "ItemNote");

                    foreach (XElement item in equipment.Descendants("EQUIPMENT").Descendants("ITEM"))
                        {
                        dtEquip.Rows.Add(
                            item.Attribute("ID").Value,
                            item.Parent.Attribute("ID").Value,
                            item.Attribute("NAME").Value,
                            item.Attribute("ITEMCDR").Value,
                            item.Attribute("ITEMEXP").Value,
                            item.Attribute("ITEMKILLS").Value,
                            item.Attribute("CASUALTY").Value,
                            item.Attribute("ITEMDAMAGE").Value,
                            item.Attribute("ITEMFORMDATE").Value,
                            item.Attribute("ITEMNOTE").Value);
                        }

                        drUnit.DataSource = dtEquip;
                        break;

                        ////IF NO DEPLOYMENT HAS BEEN SET PREVIOUSLY
                        //if (cboDeployment.SelectedValue != null)
                        //{
                        //    deploy = cboDeployment.SelectedValue.ToString();
                        //}
                        //else
                        //{
                        //    cboDeployment.SelectedValue = "8";
                        //    deploy = cboDeployment.SelectedValue.ToString();
                        //}

                        //if (cboDeployment.SelectedValue.ToString() == "1" || cboDeployment.SelectedValue.ToString() == "2")
                        //{
                        //    txtReinforceTrigger.Enabled = true;
                        //    //lblReinfDate.Visible = true;

                        //}
                        //else
                        //{
                        //    //lblReinfDate.Visible = false;
                        //    tssLabel1.Text = "";
                        //}

                        //switch (deploy)
                        //{
                        //    case "1":  //SET ENTRY LOCATION FOR UNITS WHICH ARE REINFORCEMENTS BY TURN
                        //        lblReinforce.Visible = true;
                        //        lblReinforce2.Visible = true;
                        //        txtReinforceTrigger.Visible = true;
                        //        lblReinforce.Text = "Turn";

                        //        //SET X AND Y, AFTER CHECKING FOR NULL
                        //        if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                        //        {
                        //            unit.Add(new XAttribute("GOINGTOX", "1"));
                        //            unit.Add(new XAttribute("GOINGTOY", "1"));
                        //        }

                        //        if (unit.Attribute("GOINGTOX") != null)
                        //        {
                        //            txtX.Text = unit.Attribute("GOINGTOX").Value.ToString();
                        //        }
                        //        else
                        //        {
                        //            txtX.Text = "1";
                        //        }

                        //        if (unit.Attribute("GOINGTOY") != null)
                        //        {
                        //            txtY.Text = unit.Attribute("GOINGTOY").Value.ToString();
                        //        }
                        //        else
                        //        {
                        //            txtY.Text = "1";
                        //        }

                        //        //SET ENTRY TURN AFTER CHECKING FOR NULL
                        //        if (unit.Attribute("ENTRY") != null)
                        //        {
                        //            //lblReinfDate.Visible = true;
                        //            txtReinforceTrigger.Text = unit.Attribute("ENTRY").Value.ToString();
                        //            tssLabel1.Text = GameTime.getReleaseDate(txtReinforceTrigger.Text);
                        //        }
                        //        else
                        //        {
                        //            //lblReinfDate.Visible = false;
                        //            tssLabel1.Text = "";
                        //            txtReinforceTrigger.Text = "998";
                        //        }

                        //        //ENABLE UNIT ATTRIBUTES FOR NON-DIVIDED UNITS
                        //        NonDivideUnitGUI();

                        //        ////DISABLE "DIVIDE UNIT" MENU ITEM
                        //        //divideUnitToolStripMenuItem1.Enabled = false;

                        //        break;

                        //    case "2": //SET ENTRY LOCATION FOR UNITS WHICH ARE REINFORCEMENTS BY EVENT
                        //        lblReinforce.Visible = true;
                        //        lblReinforce2.Visible = true;
                        //        txtReinforceTrigger.Visible = true;
                        //        txtReinforceTrigger.Enabled = true;
                        //        lblReinforce.Text = "Event";

                        //        //SET X AND Y, AFTER CHECKING FOR NULL
                        //        if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                        //        {
                        //            unit.Add(new XAttribute("GOINGTOX", "1"));
                        //            unit.Add(new XAttribute("GOINGTOY", "1"));
                        //        }

                        //        if (unit.Attribute("GOINGTOX") != null)
                        //        {
                        //            txtX.Text = unit.Attribute("GOINGTOX").Value.ToString();
                        //        }
                        //        else
                        //        {
                        //            txtX.Text = "1";
                        //        }

                        //        if (unit.Attribute("GOINGTOY") != null)
                        //        {
                        //            txtY.Text = unit.Attribute("GOINGTOY").Value.ToString();
                        //        }
                        //        else
                        //        {
                        //            txtY.Text = "1";
                        //        }

                        //        //SET ENTRY EVENT AFTER CHECKING FOR NULL
                        //        if (unit.Attribute("ENTRY") != null)
                        //        {
                        //            txtReinforceTrigger.Text = unit.Attribute("ENTRY").Value.ToString();
                        //        }
                        //        else
                        //        {
                        //            txtReinforceTrigger.Text = "999";
                        //        }
                        //        //ENABLE UNIT ATTRIBUTES FOR NON-DIVIDED UNITS
                        //        NonDivideUnitGUI();

                        //        break;

                        //    case "24": //SET NO LOCATION FOR DIVIDED UNITS
                        //        txtX.Text = "--";
                        //        txtY.Text = "--";

                        //        //DISABLE UNIT ATTRIBUTES FOR DIVIDED UNITS
                        //        DivideUnitGUI();

                        //        break;

                        //    default: //SET LOCATION FOR ON-MAP UNITS
                        //        lblReinforce.Visible = false;
                        //        lblReinforce2.Visible = false;
                        //        txtReinforceTrigger.Visible = false;

                        //        if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                        //        {
                        //            unit.Add(new XAttribute("X", "--"));
                        //            unit.Add(new XAttribute("Y", "--"));
                        //        }

                        //        if (unit.Attribute("X") != null)
                        //        {
                        //            txtX.Text = unit.Attribute("X").Value.ToString();
                        //        }
                        //        else
                        //        {
                        //            txtX.Text = "--";
                        //        }

                        //        if (unit.Attribute("Y") != null)
                        //        {
                        //            txtY.Text = unit.Attribute("Y").Value.ToString();
                        //        }
                        //        else
                        //        {
                        //            txtY.Text = "--";
                        //        }

                        //        //ENABLE UNIT ATTRIBUTES FOR NON-DIVIDED UNITS
                        //        NonDivideUnitGUI();

                        //        break;
                        //}

                        //lblNumber.Visible = false;
                        //txtNumber.Visible = false;
                        //lblMax.Visible = false;
                        //txtMax.Visible = false;
                        //lblDamage.Visible = false;
                        //txtDamage.Visible = false;

                        ////SHOW LABEL FOR DIVIDED SUBUNITS
                        //if (unit.Attribute("PARENT") != null)
                        //{
                        //    lblDivided.Text = "DIVIDED SUBUNIT";
                        //    lblDivided.Visible = true;
                        //}

                        ////IF UNIT IS NOT DIVIDED SUBUNIT, CAN DIVIDE & ADD EQUIP, CANNOT REUNITE
                        //if (unit.Attribute("PARENT") == null)
                        //{
                        //    divideUnitToolStripMenuItem1.Enabled = true;
                        //    reuniteUnitToolStripMenuItem1.Enabled = false;
                        //    addeqpNewEquipUnitStripMenuItem.Enabled = true;
                        //}
                        ////
                        ////IF UNIT IS DIVIDED PARENT, CANNOT DIVIDE OR ADD EQUIP, CAN REUNITE
                        //if ((unit.Attribute("PARENT") == null) && (unit.Attribute("STATUS").Value.ToString() == "24"))
                        //{
                        //    divideUnitToolStripMenuItem1.Enabled = false;
                        //    reuniteUnitToolStripMenuItem1.Enabled = true;
                        //    addeqpNewEquipUnitStripMenuItem.Enabled = false;
                        //}

                        ////IF UNIT IS DIVIDED SUBUNIT, CANNOT DIVIDE, REUNITE, OR ADD EQUIP
                        //if ((unit.Attribute("PARENT") != null) && (unit.Attribute("STATUS").Value.ToString() != "24"))
                        //{
                        //    divideUnitToolStripMenuItem1.Enabled = false;
                        //    reuniteUnitToolStripMenuItem1.Enabled = false;
                        //    addeqpNewEquipUnitStripMenuItem.Enabled = false;
                        //}

                        ////IF UNIT IF REINFORCMENT, CANNOT DIVIDE
                        //if (deploy == "1" || deploy == "2")
                        //{
                        //    divideUnitToolStripMenuItem1.Enabled = false;
                        //}

                        ////IF UNIT IS SECTION-SIZED, CANNOT DIVIDE OR REUNITE, BUT CAN ADD EQUIP
                        //if (unit.Attribute("SIZE").Value.ToString() == "Section")
                        //{
                        //    divideUnitToolStripMenuItem1.Enabled = false;
                        //    reuniteUnitToolStripMenuItem1.Enabled = false;
                        //    addeqpNewEquipUnitStripMenuItem.Enabled = true;
                        //}
                  // }
                    //break;

                case 3: //EQUIPMENT
                    //tabUnits.SelectedIndex = 3;
                    //string parentunitid = trvUnitTree.SelectedNode.Parent.Tag.ToString();
                    //if (Globals.GlobalVariables.TREEVIEWCHANGED == true)
                    //{
                    //    parentunitid = Globals.GlobalVariables.DRAGGEDPARENTID;
                    //}
                    //xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + parentunitid + "]/EQUIPMENT[@ID =" + equipid + "]";
                    //unit = xelem.XPathSelectElement(xpath);

                    //if (unit != null)
                    //{
                    //    lblUnitName.Visible = true;
                    //    txtUnitName.Visible = true;
                    //    txtUnitName.Enabled = false;
                    //    txtUnitName.Text = unit.Attribute("NAME").Value.ToString();
                    //    lblID.Visible = true;
                    //    txtID.Visible = true;
                    //    txtID.Text = trvUnitTree.SelectedNode.Tag.ToString();
                    //    lblType.Visible = true;
                    //    txtType.Visible = true;
                    //    lblSize.Visible = false;
                    //    cboSize.Visible = false;
                    //    lblIcon.Visible = false;
                    //    cboIcon.Visible = false;
                    //    lblColor.Visible = false;
                    //    cboColor.Visible = false;
                    //    lblProficiency.Visible = false;
                    //    txtProficiency.Visible = false;
                    //    lblSupply.Visible = false;
                    //    txtSupply.Visible = false;
                    //    lblSupportScope.Visible = false;
                    //    cboSupportScope.Visible = false;
                    //    lblOrders.Visible = false;
                    //    cboOrders.Visible = false;
                    //    lblEmphasis.Visible = false;
                    //    cboEmphasis.Visible = false;
                    //    lblReadiness.Visible = false;
                    //    txtReadiness.Visible = false;
                    //    lblExperience.Visible = false;
                    //    cboExperience.Visible = false;
                    //    lblDeployment.Visible = false;
                    //    cboDeployment.Visible = false;
                    //    lblReplacements.Visible = false;
                    //    cboReplacements.Visible = false;
                    //    lblNumber.Visible = true;
                    //    txtNumber.Visible = true;
                    //    txtNumber.Text = unit.Attribute("NUMBER").Value.ToString();
                    //    lblMax.Visible = true;
                    //    txtMax.Visible = true;
                    //    lblMax.Visible = true;
                    //    txtMax.Text = unit.Attribute("MAX").Value.ToString();
                    //    lblDamage.Visible = false;
                    //    txtDamage.Visible = false;
                    //    txtDamage.Text = unit.Attribute("DAMAGE").Value.ToString();
                    //    txtEntryTurn.Visible = false;
                    //    lblEntryTurn.Visible = false;
                    //    tssLabel1.Text = "";
                    //    //lblEntryDate.Visible = false;

                    //    if ((unit.Parent.Attribute("STATUS").Value.ToString() == "24") || unit.Parent.Attribute("PARENT") != null) //IF UNIT DIVIDED PARENT OR SUBUNIT
                    //    {
                    //        deleteToolStripMenuItem2.Enabled = false;
                    //        copyToolStripMenuItem2.Enabled = false;
                    //    }
                    //    else
                    //    {
                    //        deleteToolStripMenuItem2.Enabled = true;
                    //        copyToolStripMenuItem2.Enabled = true;
                    //    }
                    //}
                    break;
            }
        }

        public string GetReplacementPriorityText(string replacePriorityID)
        {
            string replacePriorityText = "";
            switch (replacePriorityID)
            {
                case "0":
                    replacePriorityText = "Normal";
                    break;
                case "3":
                    replacePriorityText = "None";
                    break;
                case "4":
                    replacePriorityText = "Very Low";
                    break;
                case "5":
                    replacePriorityText = "Low";
                    break;
                case "1":
                    replacePriorityText = "High";
                    break;
                case "2":
                    replacePriorityText = "Very High";
                    break;
            }
            return replacePriorityText;
        }

        public string GetReplacementPriorityID(string replacePriorityText)
        {
            string replacePriorityID= "";
            switch (replacePriorityText)
            {
                case "Normal":
                    replacePriorityID = "0";
                    break;
                case "None":
                    replacePriorityID = "3";
                    break;
                case "Very Low":
                    replacePriorityID = "4";
                    break;
                case "Low":
                    replacePriorityID = "5";
                    break;
                case "High":
                    replacePriorityID = "1";
                    break;
                case "Very High":
                    replacePriorityID = "2";
                    break;
            }
            return replacePriorityID;
        }

        public string GetIconText(string iconID, string iconName)
        {
            string iconDisplay = "";

            //SET ICON COMBOBOX
            //if (iconID == null)
            if (iconID == "")
            {
                switch (iconName)
                {
                    case "Air":
                        iconDisplay = "Air";
                        break;
                    case "Anti Aircraft":
                        iconDisplay = "AA";
                        break;
                    case "Airmobile Anti Air":
                        iconDisplay = "AA (Airmob)";
                        break;
                    case "Motor Anti Air":
                        iconDisplay = "AA (Mot)";
                        break;
                    case "Parachute Anti Air":
                        iconDisplay = "AA (Para)";
                        break;
                    case "Airmobile":
                        iconDisplay = "Airmobile";
                        break;
                    case "Amphibious":
                        iconDisplay = "Amphibious";
                        break;
                    case "Armored Antitank":
                        iconDisplay = "Antitank (Armored)";
                        break;
                    case "Airmobile Antitank":
                        iconDisplay = "Antitank (Airmob)";
                        break;
                    case "Glider Antitank":
                        iconDisplay = "Antitank (Glider)";
                        break;
                    case "Hvy Antitank":
                        iconDisplay = "Antitank (Heavy)";
                        break;
                    case "Parachute Antitank":
                        iconDisplay = "Antitank (Para)";
                        break;
                    case "Tank":
                        iconDisplay = "Armor";
                        break;
                    case "Amphibious Armor":
                        iconDisplay = "Armor (Amphib)";
                        break;
                    case "Assault Gun":
                        iconDisplay = "Armor (Asslt Gun)";
                        break;
                    case "Glider Tank":
                        iconDisplay = "Armor (Glider)";
                        break;
                    case "Hvy Armor":
                        iconDisplay = "Armor (Heavy)";
                        break;
                    case "Armored Train":
                        iconDisplay = "Armored Train";
                        break;
                    case "Artillery":
                        iconDisplay = "Artillery";
                        break;
                    case "Airborne Artillery":
                        iconDisplay = "Artillery (Abn)";
                        break;
                    case "Airmobile Arty":
                        iconDisplay = "Artillery (Airmob)";
                        break;
                    case "Armored Artillery":
                        iconDisplay = "Artillery (Armored)";
                        break;
                    case "Armored Hvy Arty":
                        iconDisplay = "Artillery (Arm, Hvy)";
                        break;
                    case "Chemical Artillery":
                        iconDisplay = "Artillery (Chem)";
                        break;
                    case "Fixed Artillery":
                        iconDisplay = "Artillery (Fixed)";
                        break;
                    case "Glider Artillery":
                        iconDisplay = "Artillery (Glider)";
                        break;
                    case "Hvy Artillery":
                        iconDisplay = "Artillery (Heavy)";
                        break;
                    case "Horse Artillery":
                        iconDisplay = "Artillery (Horse)";
                        break;
                    case "Inf Artillery":
                        iconDisplay = "Artillery (Infantry)";
                        break;
                    case "Missile Artillery":
                        iconDisplay = "Artillery (Missile)";
                        break;
                    case "Motor Artillery":
                        iconDisplay = "Artillery (Mot)";
                        break;
                    case "Rail Artillery":
                        iconDisplay = "Artillery (Rail)";
                        break;
                    case "Rocket Artillery":
                        iconDisplay = "Artillery (Rocket)";
                        break;
                    case "Motor Rocket":
                        iconDisplay = "Artillery (Rocket, Mot)";
                        break;
                    case "Bicycle":
                        iconDisplay = "Bicycle";
                        break;
                    case "Jet Bomber":
                        iconDisplay = "Bomber (Jet)";
                        break;
                    case "Jet Heavy Bomber":
                        iconDisplay = "Bomber (Jet, Heavy)";
                        break;
                    case "Medium Bomber":
                        iconDisplay = "Bomber (Medium)";
                        break;
                    case "Naval Bomber":
                        iconDisplay = "Bomber (Naval)";
                        break;
                    case "Border":
                        iconDisplay = "Border";
                        break;
                    case "Cavalry":
                        iconDisplay = "Cavalry";
                        break;
                    case "Airmobile Cavalry":
                        iconDisplay = "Cavalry (Airmob)";
                        break;
                    case "Armored Cavalry":
                        iconDisplay = "Cavalry (Armored)";
                        break;
                    case "Motor Cavalry":
                        iconDisplay = "Cavalry (Mot)";
                        break;
                    case "Mountain Cavalry":
                        iconDisplay = "Cavalry (Mtn)";
                        break;
                    case "Civilian":
                        iconDisplay = "Civilian";
                        break;
                    case "Embarked Air":
                        iconDisplay = "Embarked Air";
                        break;
                    case "Embarked Heli":
                        iconDisplay = "Embarked Heli";
                        break;
                    case "Embarked Naval":
                        iconDisplay = "Embarked Naval";
                        break;
                    case "Embarked Rail":
                        iconDisplay = "Embarked Rail";
                        break;
                    case "Engineer":
                        iconDisplay = "Engineer";
                        break;
                    case "Airborne Engineer":
                        iconDisplay = "Engineer (Abn)";
                        break;
                    case "Airmobile Engineer":
                        iconDisplay = "Engineer (Airmob)";
                        break;
                    case "Armored Engineer":
                        iconDisplay = "Engineer (Armored)";
                        break;
                    case "Ferry Engineer":
                        iconDisplay = "Engineer (Ferry)";
                        break;
                    case "Motor Engineer":
                        iconDisplay = "Engineer (Mot)";
                        break;
                    case "Jet Fighter":
                        iconDisplay = "Fighter (Jet)";
                        break;
                    case "Naval Fighter":
                        iconDisplay = "Fighter (Naval)";
                        break;
                    case "Garrison":
                        iconDisplay = "Garrison";
                        break;
                    case "Guerilla":
                        iconDisplay = "Guerilla";
                        break;
                    case "Airmobile Hvy Wpns":
                        iconDisplay = "Heavy Wpns (Airmob)";
                        break;
                    case "Mountain Cav Hvy Wpns":
                        iconDisplay = "Heavy Wpns (Mtn Cav)";
                        break;
                    case "Glider Hvy Wpns":
                        iconDisplay = "Heavy Wpns (Glider)";
                        break;
                    case "Infantry Hvy Wpns":
                        iconDisplay = "Heavy Wpns (Infantry)";
                        break;
                    case "Motor Hvy Wpns":
                        iconDisplay = "Heavy Wpns (Mot)";
                        break;
                    case "Mountain Hvy Wpns":
                        iconDisplay = "Heavy Wpns (Mtn)";
                        break;
                    case "Parachute Hvy Wpns":
                        iconDisplay = "Heavy Wpns (Para)";
                        break;
                    case "Attack Helicopter":
                        iconDisplay = "Helicopter (Attack)";
                        break;
                    case "Recon Helicopter":
                        iconDisplay = "Helicopter (Recon)";
                        break;
                    case "Trans Helicopter":
                        iconDisplay = "Helicopter (Transport)";
                        break;
                    case "Infantry":
                        iconDisplay = "Infantry";
                        break;
                    case "Airmobile Infantry":
                        iconDisplay = "Infantry (Airmob)";
                        break;
                    case "Glider Infantry":
                        iconDisplay = "Infantry (Glider)";
                        break;
                    case "Marine Infantry":
                        iconDisplay = "Infantry (Marine)";
                        break;
                    case "Mechanized":
                        iconDisplay = "Infantry (Mech)";
                        break;
                    case "Motor Infantry":
                        iconDisplay = "Infantry (Mot)";
                        break;
                    case "Mountain Infantry":
                        iconDisplay = "Infantry (Mtn)";
                        break;
                    case "Parachute Infantry":
                        iconDisplay = "Infantry (Para)";
                        break;
                    case "Irregular":
                        iconDisplay = "Irregular";
                        break;
                    case "Machine Gun":
                        iconDisplay = "Machine Gun";
                        break;
                    case "Motor Machinegun":
                        iconDisplay = "Machine Gun (Mot)";
                        break;
                    case "Military Police":
                        iconDisplay = "Military Police";
                        break;
                    case "Mortar":
                        iconDisplay = "Mortar";
                        break;
                    case "Hvy Mortar":
                        iconDisplay = "Mortar (Heavy)";
                        break;
                    case "Carrier Naval":
                        iconDisplay = "Naval (Carrier)";
                        break;
                    case "Heavy Naval":
                        iconDisplay = "Naval (Heavy)";
                        break;
                    case "Light Naval":
                        iconDisplay = "Naval (Light)";
                        break;
                    case "Medium Naval":
                        iconDisplay = "Naval (Medium)";
                        break;
                    case "Riverine":
                        iconDisplay = "Naval (Riverine)";
                        break;
                    case "Naval Task Force":
                        iconDisplay = "Naval (Task Force)";
                        break;
                    case "Naval Attack":
                        iconDisplay = "Naval Attack Aircraft";
                        break;
                    case "Parachute":
                        iconDisplay = "Parachute";
                        break;
                    case "Railroad Repair":
                        iconDisplay = "Railroad Repair";
                        break;
                    case "Airborne Recon":
                        iconDisplay = "Recon (Airborne)";
                        break;
                    case "Armored Recon":
                        iconDisplay = "Recon (Armored)";
                        break;
                    case "Glider Recon":
                        iconDisplay = "Recon (Glider)";
                        break;
                    case "Reserve":
                        iconDisplay = "Reserve";
                        break;
                    case "Security":
                        iconDisplay = "Security";
                        break;
                    case "Ski":
                        iconDisplay = "Ski";
                        break;
                    case "Special Forces":
                        iconDisplay = "Special Forces";
                        break;
                    case "Supply":
                        iconDisplay = "Supply";
                        break;
                    case "Amphib Transport":
                        iconDisplay = "Transport (Amphib)";
                        break;
                    case "Task Force":
                        iconDisplay = "Task Force";
                        break;
                    case "Battlegroup":
                        iconDisplay = "Battle Group";
                        break;
                    case "Kampfgruppe":
                        iconDisplay = "Kampfgruppe";
                        break;
                    case "Combat Command A":
                        iconDisplay = "Combat Command A";
                        break;
                    case "Combat Command B":
                        iconDisplay = "Combat Command B";
                        break;
                    case "Combat Command C":
                        iconDisplay = "Combat Command C";
                        break;
                    case "Combat Command R":
                        iconDisplay = "Combat Command R";
                        break;
                    case "":
                        iconDisplay = "NO ICON";
                        break;
                    default:
                        iconDisplay = "NO ICON";
                        break;
                }
            }
            else //IF ALTERNATE ICONS EXIST
            {
                switch (iconID)
                {
                    case "0":
                        iconDisplay = "Headquarters [v1]";
                        break;
                    case "1":
                        iconDisplay = "Headquarters [v2]";
                        break;
                    case "14":
                        iconDisplay = "Antitank [v1]";
                        break;
                    case "15":
                        iconDisplay = "Antitank [v2]";
                        break;
                    case "25":
                        iconDisplay = "Antitank (Mot) [v1]";
                        break;
                    case "26":
                        iconDisplay = "Antitank (Mot) [v2]";
                        break;
                    case "41":
                        iconDisplay = "Fighter [icon]";
                        break;
                    case "42":
                        iconDisplay = "Fighter Bomber [icon]";
                        break;
                    case "43":
                        iconDisplay = "Bomber (Light) [icon]";
                        break;
                    case "45":
                        iconDisplay = "Bomber (Heavy) [icon]";
                        break;
                    case "62":
                        iconDisplay = "Artillery (Coast) [icon]";
                        break;
                    case "63":
                        iconDisplay = "Artillery (Coast) [silh]";
                        break;
                    case "66":
                        iconDisplay = "Fighter [silh]";
                        break;
                    case "67":
                        iconDisplay = "Fighter Bomber [silh]";
                        break;
                    case "68":
                        iconDisplay = "Bomber (Light) [silh]";
                        break;
                    case "69":
                        iconDisplay = "Bomber (Heavy) [silh]";
                        break;
                    case "82":
                        iconDisplay = "Transport [icon]";
                        break;
                    case "94":
                        iconDisplay = "Transport [silh]";
                        break;
                    default:
                        iconDisplay = "NO ICON";
                        break;
                }
            }
            return iconDisplay;
        }

        private void cboSupport_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void cboOrders_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void cboLossTol_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void cboUnitType_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void cboUnitSize_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void cboUnitOrders_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void cboUnitLossTol_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void cboExp_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void cboReplace_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private bool IsAirUnit(string unittype)
        {
            bool isAirUnit = false;

            //LIST ALL AIR UNIT ICON TYPES
            HashSet<string> airunits = new HashSet<string>();
            airunits.Add("Air");
            airunits.Add("Fighter");
            airunits.Add("Fighter Bomber");
            airunits.Add("Light Bomber");
            airunits.Add("Medium Bomber");
            airunits.Add("Heavy Bomber");
            airunits.Add("Jet Fighter");
            airunits.Add("Jet Bomber");
            airunits.Add("Jet Heavy Bomber");
            airunits.Add("Naval Fighter");
            airunits.Add("Naval Attack");
            airunits.Add("Naval Bomber");

            if (airunits.Contains(unittype)) isAirUnit = true;

            return isAirUnit;
        }

        private string SetUnitOrders(bool isAirUnit, string orderID)
        {
            string unitorders = "";
            cboUnitOrders.Items.Clear();

            if (isAirUnit == false)
            {
                this.cboUnitOrders.Visible = true;
                this.cboUnitOrders.Enabled = true;
                this.cboUnitOrders.Focus();

                switch (orderID)
                {
                    case "1":
                        unitorders = "Reinforce (Turn)";
                        break;
                    case "2":
                        unitorders = "Reinforce (Event)";
                        break;
                    case "3":
                        unitorders = "Defend/Dig In";
                        break;
                    case "4":
                        unitorders = "Entrenched";
                        break;
                    case "5":
                        unitorders = "Fortified";
                        break;
                    case "6":
                        unitorders = "Tactical Reserve";
                        break;
                    case "7":
                        unitorders = "Local Reserve";
                        break;
                    case "8":
                        unitorders = "Mobile";
                        break;
                    case "9":
                        unitorders = "Moving";
                        break;
                    case "10":
                        unitorders = "Attacking";
                        break;
                    case "11":
                        unitorders = "Supporting";
                        break;
                    case "12":
                        unitorders = "Retreated";
                        break;
                    case "13":
                        unitorders = "Routed";
                        break;
                    case "14":
                        unitorders = "Advancing";
                        break;
                    case "15":
                        unitorders = "Withdrawn";
                        break;
                    case "16":
                        unitorders = "Exited";
                        break;
                    case "17":
                        unitorders = "Embarked";
                        break;
                    case "18":
                        unitorders = "Disbanded";
                        break;
                    case "19":
                        unitorders = "Tact React";
                        break;
                    case "20":
                        unitorders = "Local React";
                        break;
                    case "21":
                        unitorders = "Entrained";
                        break;
                    case "22":
                        unitorders = "Airborne";
                        break;
                    case "23":
                        unitorders = "Seaborne";
                        break;
                    case "24":
                        unitorders = "Divided";
                        break;
                    case "25":
                        unitorders = "Nuclear";
                        break;
                    case "26":
                        unitorders = "Airmobile";
                        break;
                    case "27":
                        unitorders = "Bridge Attack";
                        break;
                    case "28":
                        unitorders = "Airfield Attack";
                        break;
                    case "29":
                        unitorders = "Reorganizing";
                        break;
                    case "30":
                        unitorders = "Port Attack";
                        break;
                }

                cboUnitOrders.Items.Add("Reinforce (Turn)");
                cboUnitOrders.Items.Add("Reinforce (Event)");
                cboUnitOrders.Items.Add("Defend/Dig In");
                cboUnitOrders.Items.Add("Entrenched");
                cboUnitOrders.Items.Add("Fortified");
                cboUnitOrders.Items.Add("Tactical Reserve");
                cboUnitOrders.Items.Add("Local Reserve");
                cboUnitOrders.Items.Add("Mobile");
                cboUnitOrders.Items.Add("Moving");
                cboUnitOrders.Items.Add("Attacking");
                cboUnitOrders.Items.Add("Supporting");
                cboUnitOrders.Items.Add("Retreated");
                cboUnitOrders.Items.Add("Routed");
                cboUnitOrders.Items.Add("Advancing");
                cboUnitOrders.Items.Add("Withdrawn");
                cboUnitOrders.Items.Add("Exited");
                cboUnitOrders.Items.Add("Embarked");
                cboUnitOrders.Items.Add("Disbanded");
                cboUnitOrders.Items.Add("Tact React");
                cboUnitOrders.Items.Add("Local React");
                cboUnitOrders.Items.Add("Entrained");
                cboUnitOrders.Items.Add("Airborne");
                cboUnitOrders.Items.Add("Seaborne");
                cboUnitOrders.Items.Add("Divided");
                cboUnitOrders.Items.Add("Nuclear");
                cboUnitOrders.Items.Add("Airmobile");
                cboUnitOrders.Items.Add("Bridge Attack");
                cboUnitOrders.Items.Add("Airfield Attack");
                cboUnitOrders.Items.Add("Reorganizing");
                cboUnitOrders.Items.Add("Port Attack");
            }

            else if (isAirUnit == true)
            {
                this.cboUnitOrders.Visible = true;
                this.cboUnitOrders.Enabled = true;

                switch (orderID)
                {
                    case "3":
                        unitorders = "Interdiction";
                        break;
                    case "4":
                        unitorders = "Air Superiority";
                        break;
                    case "5":
                        unitorders = "Combat Support";
                        break;
                    case "8":
                        unitorders = "Rest";
                        break;
                    case "23":
                        unitorders = "Sea Interdiction";
                        break;
                }
                //unitorders = "Interdiction";

                cboUnitOrders.Items.Add("Interdiction");
                cboUnitOrders.Items.Add("Air Superiority");
                cboUnitOrders.Items.Add("Combat Support");
                cboUnitOrders.Items.Add("Rest");
                cboUnitOrders.Items.Add("Sea Interdiction");
            }

            return unitorders;
        }

        private string SetUnitOrderID(string order)
        {
            string unitorderID = "";

            switch (order)
            {
                case "Reinforce (Turn)":
                    unitorderID = "1";
                    break;
                case "Reinforce (Event)":
                    unitorderID = "2";
                    break;
                case "Defend/Dig In":
                case "Interdiction":
                    unitorderID = "3";
                    break;
                case "Entrenched":
                case "Air Superiority":
                    unitorderID = "4";
                    break;
                case "Fortified":
                case "Combat Support":
                    unitorderID = "5";
                    break;
                case "Tactical Reserve":
                    unitorderID = "6";
                    break;
                case "Local Reserve":
                    unitorderID = "7";
                    break;
                case "Mobile":
                case "Rest":
                    unitorderID = "8";
                    break;
                case "Moving":
                    unitorderID = "9";
                    break;
                case "Attacking":
                    unitorderID = "10";
                    break;
                case "Supporting":
                    unitorderID = "11";
                    break;
                case "Retreated":
                    unitorderID = "12";
                    break;
                case "Routed":
                    unitorderID = "13";
                    break;
                case "Advancing":
                    unitorderID = "14";
                    break;
                case "Withdrawn":
                    unitorderID = "15";
                    break;
                case "Exited":
                    unitorderID = "16";
                    break;
                case "Embarked":
                    unitorderID = "17";
                    break;
                case "Disbanded":
                    unitorderID = "18";
                    break;
                case "Tact React":
                    unitorderID = "19";
                    break;
                case "Local React":
                    unitorderID = "20";
                    break;
                case "Entrained":
                    unitorderID = "21";
                    break;
                case "Airborne":
                    unitorderID = "22";
                    break;
                case "Seaborne":
                case "Sea Interdiction":
                    unitorderID = "23";
                    break;
                case "Divided":
                    unitorderID = "24";
                    break;
                case "Nuclear":
                    unitorderID = "25";
                    break;
                case "Airmobile":
                    unitorderID = "26";
                    break;
                case "Bridge Attack":
                    unitorderID = "27";
                    break;
                case "Airfield Attack":
                    unitorderID = "28";
                    break;
                case "Reorganizing":
                    unitorderID = "29";
                    break;
                case "Port Attack":
                    unitorderID = "30";
                    break;
            }

            return unitorderID;
        }

        private void txtHdrForceName_TextChanged(object sender, EventArgs e)
        {
            //CHANGE TACFILE XML
            string xpath = "HEADER";
            string xpath2 = "OOB/FORCE[@ID=" + forceID + "]";

            var force = tacFile.XPathSelectElement(xpath);
            var force2 = tacFile.XPathSelectElement(xpath2);

            force.Attribute("forceName1").Value = txtHdrForceName.Text;
            force2.Attribute("NAME").Value = txtHdrForceName.Text;

            //CHANGE RADIO BUTTON TEXT
            if (forceID == "1")
            {
                rbForce1.Text = txtHdrForceName.Text;
            }
            else if (forceID == "2")
            {
                rbForce2.Text = txtHdrForceName.Text;
            }
            trvUnitTree.TopNode.Text = txtHdrForceName.Text;
            trvUnitTree.Refresh();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    Control name = row.Controls.Find("txtName", true).First();
                    formID = label.Text;
                    string newname = name.Text;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (name.Text != null) formation.Attribute("NAME").Value = name.Text;

                    IEnumerable<XElement> unitname =
                        from f in tacFile.Elements("OOB").Elements("FORCE").Elements("FORMATION")
                        where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                        select f;

                    if ((haschanged == true) && (unitname.Count() > 1))
                    {
                        MessageBox.Show("Formation cannot have same name as another formation!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        name.Text = oldname;
                        haschanged = false;
                    }

                    //REVISE TREE NODE
                    foreach (TreeNode node in trvUnitTree.Nodes)
                    {
                        foreach (TreeNode child in node.Nodes)
                        {
                            if (child.Name == "FORMATION")
                            {
                                if (child.Tag.ToString() == formID)
                                {
                                    child.Text = name.Text;
                                    trvUnitTree.Refresh();
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void txtProf_Enter(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control prof = row.Controls.Find("txtProf", true).First();

                    if (!prof.Focused) return;

                    oldprof = prof.Text;
                }
            }
        }

        private void txtProf_Leave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control prof = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    prof = row.Controls.Find("txtProf", true).First();
                    formID = label.Text;

                    if (!prof.Focused) return;

                    //VALIDATE AS NUMBER
                    int profnum = 0;
                    bool isNum = int.TryParse(prof.Text, out profnum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        prof.Text = oldprof;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);
                        if (!withinRange) prof.Text = oldprof;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (prof.Text != null) formation.Attribute("PROFICIENCY").Value = prof.Text;
                    break;
                }
            }
        }

        private void txtProf_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control prof = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    prof = row.Controls.Find("txtProf", true).First();
                    formID = label.Text;

                    if (!prof.Focused) return;

                    //VALIDATE AS NUMBER
                    int profnum = 0;
                    bool isNum = int.TryParse(prof.Text, out profnum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        prof.Text = oldprof;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);
                        if (!withinRange) prof.Text = oldprof;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (prof.Text != null) formation.Attribute("PROFICIENCY").Value = prof.Text;
                    break;
                }
            }

        }

        private void txtSupply_Enter(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control supply = row.Controls.Find("txtSupply", true).First();

                    if (!supply.Focused) return;

                    oldsupply = supply.Text;
                }
            }
        }

        private void txtSupply_Leave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control supply = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    supply = row.Controls.Find("txtSupply", true).First();
                    formID = label.Text;

                    if (!supply.Focused) return;

                    //VALIDATE AS NUMBER
                    int supplynum = 0;
                    bool isNum = int.TryParse(supply.Text, out supplynum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        supply.Text = oldsupply;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);
                        if (!withinRange) supply.Text = oldsupply;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (supply.Text != null) formation.Attribute("SUPPLY").Value = supply.Text;
                    break;
                }
            }
        }

        private void txtSupply_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control supply = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    supply = row.Controls.Find("txtSupply", true).First();
                    formID = label.Text;

                    if (!supply.Focused) return;

                    //VALIDATE AS NUMBER
                    int supplynum = 0;
                    bool isNum = int.TryParse(supply.Text, out supplynum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        supply.Text = oldsupply;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);
                        if (!withinRange) supply.Text = oldsupply;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (supply.Text != null) formation.Attribute("SUPPLY").Value = supply.Text;
                    break;
                }
            }
        }

        private void cboSupport_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    Control support = row.Controls.Find("cboSupport", true).First();
                    formID = label.Text;

                    if (!support.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (support.Text != null) formation.Attribute("SUPPORTSCOPE").Value = support.Text;
                    break;
                }
            }
        }

        private void cboOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    Control orders = row.Controls.Find("cboOrders", true).First();
                    formID = label.Text;

                    if (!orders.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (orders.Text != null) formation.Attribute("ORDERS").Value = orders.Text;
                    break;
                }
            }
        }

        private void cboLossTol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    Control lossTol = row.Controls.Find("cboLossTol", true).First();
                    formID = label.Text;

                    if (!lossTol.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (lossTol.Text != null) formation.Attribute("EMPHASIS").Value = lossTol.Text;
                    break;
                }
            }
        }

        public bool IsWithinRange(int a, string desc, int min, int max)
        {
            if (a < min || a > max)
            {
                MessageBox.Show(desc + " must be between " + min + " and " + max + ".", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else
            {
                return true;
            }

        }

        private void txtHdrForceProf_Enter(object sender, EventArgs e)
        {
            oldprof = txtHdrForceProf.Text;
        }

        private void txtHdrForceProf_Validating(object sender, CancelEventArgs e)
        {
            if (!txtHdrForceProf.Focused) return;

            //VALIDATE AS NUMBER
            int profnum = 0;
            bool isNum = int.TryParse(txtHdrForceProf.Text, out profnum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrForceProf.Text = oldprof;
                e.Cancel = true;
            }
            else
            {
                bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);

                if (!withinRange)
                {
                    txtHdrForceProf.Text = oldprof;
                    e.Cancel = true;
                }
            }
            //CHANGE TACFILE XML
            string xpath = "OOB/FORCE[@ID=" + forceID + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrForceProf.Text != null) force.Attribute("proficiency").Value = txtHdrForceProf.Text;
        }

        private void txtHdrForceProf_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrForceProf.Focused) return;

            //VALIDATE AS NUMBER
            int profnum = 0;
            bool isNum = int.TryParse(txtHdrForceProf.Text, out profnum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrForceProf.Text = oldprof;
            }
            else
            {
                bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);

                if (!withinRange)
                {
                    txtHdrForceProf.Text = oldprof;
                }
                else
                {
                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]";
                    var force = tacFile.XPathSelectElement(xpath);
                    if (txtHdrForceProf.Text != null) force.Attribute("proficiency").Value = txtHdrForceProf.Text;
                }
            }

        }

        private void txtHdrForceSupply_Enter(object sender, EventArgs e)
        {
            oldsupply = txtHdrForceSupply.Text;
        }

        private void txtHdrForceSupply_Validating(object sender, CancelEventArgs e)
        {
            if (!txtHdrForceSupply.Focused) return;

            //VALIDATE AS NUMBER
            int supplynum = 0;
            bool isNum = int.TryParse(txtHdrForceSupply.Text, out supplynum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrForceSupply.Text = oldsupply;
                e.Cancel = true;
            }
            else
            {
                bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);

                if (!withinRange)
                {
                    txtHdrForceSupply.Text = oldsupply;
                    e.Cancel = true;
                }
            }
            //CHANGE TACFILE XML
            string xpath = "OOB/FORCE[@ID=" + forceID + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrForceSupply.Text != null) force.Attribute("supply").Value = txtHdrForceSupply.Text;
        }

        private void txtHdrForceSupply_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrForceSupply.Focused) return;

            //VALIDATE AS NUMBER
            int supplynum = 0;
            bool isNum = int.TryParse(txtHdrForceSupply.Text, out supplynum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrForceSupply.Text = oldsupply;
            }
            else
            {
                bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);

                if (!withinRange)
                {
                    txtHdrForceSupply.Text = oldsupply;
                }
            }
            //CHANGE TACFILE XML
            string xpath = "OOB/FORCE[@ID=" + forceID + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrForceSupply.Text != null) force.Attribute("supply").Value = txtHdrForceSupply.Text;
        }

        private void txtHdrFormProf_Enter(object sender, EventArgs e)
        {
            oldprof = txtHdrFormProf.Text;
        }

        private void txtHdrFormProf_Validating(object sender, CancelEventArgs e)
        {
            if (!txtHdrFormProf.Focused) return;

            //VALIDATE AS NUMBER
            int profnum = 0;
            bool isNum = int.TryParse(txtHdrFormProf.Text, out profnum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrFormProf.Text = oldprof;
                e.Cancel = true;
            }
            else
            {
                bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);

                if (!withinRange)
                {
                    txtHdrFormProf.Text = oldprof;
                    e.Cancel = true;
                }
            }
            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormProf.Text != null) force.Attribute("PROFICIENCY").Value = txtHdrFormProf.Text;
        }

        private void txtHdrFormProf_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrFormProf.Focused) return;

            //VALIDATE AS NUMBER
            int profnum = 0;
            bool isNum = int.TryParse(txtHdrFormProf.Text, out profnum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrFormProf.Text = oldprof;
            }
            else
            {
                bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);

                if (!withinRange)
                {
                    txtHdrFormProf.Text = oldprof;
                }
            }
            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormProf.Text != null) force.Attribute("PROFICIENCY").Value = txtHdrFormProf.Text;
        }

        private void txtHdrFormSupply_Enter(object sender, EventArgs e)
        {
            oldsupply = txtHdrFormSupply.Text;
        }

        private void txtHdrFormSupply_Validating(object sender, CancelEventArgs e)
        {
            if (!txtHdrFormSupply.Focused) return;

            //VALIDATE AS NUMBER
            int supplynum = 0;
            bool isNum = int.TryParse(txtHdrFormSupply.Text, out supplynum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrFormSupply.Text = oldsupply;
                e.Cancel = true;
            }
            else
            {
                bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);

                if (!withinRange)
                {
                    txtHdrFormSupply.Text = oldsupply;
                    e.Cancel = true;
                }
            }
            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormSupply.Text != null) force.Attribute("SUPPLY").Value = txtHdrFormSupply.Text;
        }

        private void txtHdrFormSupply_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrFormSupply.Focused) return;

            //VALIDATE AS NUMBER
            int supplynum = 0;
            bool isNum = int.TryParse(txtHdrFormSupply.Text, out supplynum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrFormSupply.Text = oldsupply;
            }
            else
            {
                bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);

                if (!withinRange)
                {
                    txtHdrFormSupply.Text = oldsupply;
                }
            }
            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormSupply.Text != null) force.Attribute("SUPPLY").Value = txtHdrFormSupply.Text;
        }

        private void txtHdrUnitProf_Enter(object sender, EventArgs e)
        {
            oldprof = txtHdrUnitProf.Text;
        }

        private void txtHdrUnitProf_Validating(object sender, CancelEventArgs e)
        {
            if (!txtHdrUnitProf.Focused) return;

            //VALIDATE AS NUMBER
            int profnum = 0;
            bool isNum = int.TryParse(txtHdrUnitProf.Text, out profnum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrUnitProf.Text = oldprof;
                e.Cancel = true;
            }
            else
            {
                bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);

                if (!withinRange)
                {
                    txtHdrUnitProf.Text = oldprof;
                    e.Cancel = true;
                }
            }
            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitProf.Text != null) unit.Attribute("PROFICIENCY").Value = txtHdrUnitProf.Text;
        }

        private void txtHdrUnitProf_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrUnitProf.Focused) return;

            //VALIDATE AS NUMBER
            int profnum = 0;
            bool isNum = int.TryParse(txtHdrUnitProf.Text, out profnum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrUnitProf.Text = oldprof;
            }
            else
            {
                bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);

                if (!withinRange)
                {
                    txtHdrUnitProf.Text = oldprof;
                }
            }
            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitProf.Text != null) unit.Attribute("PROFICIENCY").Value = txtHdrUnitProf.Text;
        }

        private void txtHdrUnitSupply_Enter(object sender, EventArgs e)
        {
            oldsupply = txtHdrUnitSupply.Text;
        }

        private void txtHdrUnitSupply_Validating(object sender, CancelEventArgs e)
        {
            if (!txtHdrUnitSupply.Focused) return;

            //VALIDATE AS NUMBER
            int supplynum = 0;
            bool isNum = int.TryParse(txtHdrUnitSupply.Text, out supplynum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrUnitSupply.Text = oldsupply;
                e.Cancel = true;
            }
            else
            {
                bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);

                if (!withinRange)
                {
                    txtHdrUnitSupply.Text = oldsupply;
                    e.Cancel = true;
                }
            }
            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitSupply.Text != null) unit.Attribute("SUPPLY").Value = txtHdrUnitSupply.Text;
        }

        private void txtHdrUnitSupply_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrUnitSupply.Focused) return;

            //VALIDATE AS NUMBER
            int supplynum = 0;
            bool isNum = int.TryParse(txtHdrUnitSupply.Text, out supplynum);

            if (isNum == false)
            {
                MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrUnitSupply.Text = oldsupply;
            }
            else
            {
                bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);

                if (!withinRange)
                {
                    txtHdrUnitSupply.Text = oldsupply;
                }
            }
            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitSupply.Text != null) unit.Attribute("SUPPLY").Value = txtHdrUnitSupply.Text;
        }

        private void txtUnitName_Leave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            string newname = "";

            if (!txtUnitName.Focused) return;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    Control name = row.Controls.Find("txtUnitName", true).First();
                    unitID = label.Text;
                    newname = name.Text;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (name.Text != null) unit.Attribute("NAME").Value = name.Text;

                    IEnumerable<XElement> unitname =
                       //from f in tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT")
                       from f in tacFile.Elements("OOB").Elements("FORCE").Elements("FORMATION").Elements("UNIT")
                       where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                       select f;

                    if ((haschanged == true) && (unitname.Count() > 1))
                    {
                        MessageBox.Show("Unit cannot have same name as another unit!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        name.Text = oldname;
                        haschanged = false;
                    }


                    //IEnumerable<XElement> unitname =
                    //   from f in tacFile.Elements("OOB").Elements("FORCE").Elements("FORMATION")
                    //   where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                    //   select f;

                    //if ((haschanged == true) && (unitname.Count() > 1))
                    //{
                    //    MessageBox.Show("Formation cannot have same name as another formation!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    name.Text = oldname;
                    //    haschanged = false;
                    //}

                    //REVISE TREE NODE
                    foreach (TreeNode node in trvUnitTree.Nodes)
                    {
                        foreach (TreeNode child in node.Nodes)
                        {
                            foreach (TreeNode grandchild in child.Nodes)
                                if (grandchild.Name == "UNIT")
                                {
                                    if (grandchild.Tag.ToString() == unitID)
                                    {
                                        grandchild.Text = name.Text;
                                        trvUnitTree.Refresh();
                                        break;
                                    }
                                }
                        }
                    }
                    break;
                }
            }
        }

        private void cboUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";

            //BINDING LIST OF ALL ICONS
            var icons = new BindingList<KeyValuePair<string, string>>();

            icons.Add(new KeyValuePair<string, string>("Air", "Air"));
            icons.Add(new KeyValuePair<string, string>("Anti Aircraft", "AA"));
            icons.Add(new KeyValuePair<string, string>("Airmobile Anti Air", "AA (Airmob)"));
            icons.Add(new KeyValuePair<string, string>("Motor Anti Air", "AA (Mot)"));
            icons.Add(new KeyValuePair<string, string>("Parachute Anti Air", "AA (Para)"));
            icons.Add(new KeyValuePair<string, string>("Airmobile", "Airmobile"));
            icons.Add(new KeyValuePair<string, string>("Amphibious", "Amphibious"));
            icons.Add(new KeyValuePair<string, string>("Antitank", "Antitank [v1]"));
            icons.Add(new KeyValuePair<string, string>("Antitank", "Antitank [v2]"));
            icons.Add(new KeyValuePair<string, string>("Armored Antitank", "Antitank (Armored)"));
            icons.Add(new KeyValuePair<string, string>("Airmobile Antitank", "Antitank (Airmob)"));
            icons.Add(new KeyValuePair<string, string>("Glider Antitank", "Antitank (Glider)"));
            icons.Add(new KeyValuePair<string, string>("Hvy Antitank", "Antitank (Heavy)"));
            icons.Add(new KeyValuePair<string, string>("Motor Antitank", "Antitank (Mot) [v1]"));
            icons.Add(new KeyValuePair<string, string>("Motor Antitank", "Antitank (Mot) [v2]"));
            icons.Add(new KeyValuePair<string, string>("Parachute Antitank", "Antitank (Para)"));
            icons.Add(new KeyValuePair<string, string>("Tank", "Armor"));
            icons.Add(new KeyValuePair<string, string>("Amphibious Armor", "Armor (Amphib)"));
            icons.Add(new KeyValuePair<string, string>("Assault Gun", "Armor (Asslt Gun)"));
            icons.Add(new KeyValuePair<string, string>("Glider Tank", "Armor (Glider)"));
            icons.Add(new KeyValuePair<string, string>("Hvy Armor", "Armor (Heavy)"));
            icons.Add(new KeyValuePair<string, string>("Armored Train", "Armored Train"));
            icons.Add(new KeyValuePair<string, string>("Artillery", "Artillery"));
            icons.Add(new KeyValuePair<string, string>("Airborne Artillery", "Artillery (Abn)"));
            icons.Add(new KeyValuePair<string, string>("Airmobile Arty", "Artillery (Airmob)"));
            icons.Add(new KeyValuePair<string, string>("Armored Artillery", "Artillery (Armored)"));
            icons.Add(new KeyValuePair<string, string>("Armored Hvy Arty", "Artillery (Arm, Hvy)"));
            icons.Add(new KeyValuePair<string, string>("Chemical Artillery", "Artillery (Chem)"));
            icons.Add(new KeyValuePair<string, string>("Coastal Artillery", "Artillery (Coast) [icon]"));
            icons.Add(new KeyValuePair<string, string>("Coastal Artillery", "Artillery (Coast) [silh]"));
            icons.Add(new KeyValuePair<string, string>("Fixed Artillery", "Artillery (Fixed)"));
            icons.Add(new KeyValuePair<string, string>("Glider Artillery", "Artillery (Glider)"));
            icons.Add(new KeyValuePair<string, string>("Hvy Artillery", "Artillery (Heavy)"));
            icons.Add(new KeyValuePair<string, string>("Horse Artillery", "Artillery (Horse)"));
            icons.Add(new KeyValuePair<string, string>("Inf Artillery", "Artillery (Infantry)"));
            icons.Add(new KeyValuePair<string, string>("Missile Artillery", "Artillery (Missile)"));
            icons.Add(new KeyValuePair<string, string>("Motor Artillery", "Artillery (Mot)"));
            icons.Add(new KeyValuePair<string, string>("Rail Artillery", "Artillery (Rail)"));
            icons.Add(new KeyValuePair<string, string>("Rocket Artillery", "Artillery (Rocket)"));
            icons.Add(new KeyValuePair<string, string>("Motor Rocket", "Artillery (Rocket, Mot)"));
            icons.Add(new KeyValuePair<string, string>("Bicycle", "Bicycle"));
            icons.Add(new KeyValuePair<string, string>("Heavy Bomber", "Bomber (Heavy) [icon]"));
            icons.Add(new KeyValuePair<string, string>("Heavy Bomber", "Bomber (Heavy) [silh]"));
            icons.Add(new KeyValuePair<string, string>("Jet Bomber", "Bomber (Jet)"));
            icons.Add(new KeyValuePair<string, string>("Jet Heavy Bomber", "Bomber (Jet, Heavy)"));
            icons.Add(new KeyValuePair<string, string>("Light Bomber", "Bomber (Light) [icon]"));
            icons.Add(new KeyValuePair<string, string>("Light Bomber", "Bomber (Light) [silh]"));
            icons.Add(new KeyValuePair<string, string>("Medium Bomber", "Bomber (Medium)"));
            icons.Add(new KeyValuePair<string, string>("Naval Bomber", "Bomber (Naval)"));
            icons.Add(new KeyValuePair<string, string>("Border", "Border"));
            icons.Add(new KeyValuePair<string, string>("Cavalry", "Cavalry"));
            icons.Add(new KeyValuePair<string, string>("Airmobile Cavalry", "Cavalry (Airmob)"));
            icons.Add(new KeyValuePair<string, string>("Armored Cavalry", "Cavalry (Armored)"));
            icons.Add(new KeyValuePair<string, string>("Motor Cavalry", "Cavalry (Mot)"));
            icons.Add(new KeyValuePair<string, string>("Mountain Cavalry", "Cavalry (Mtn)"));
            icons.Add(new KeyValuePair<string, string>("Civilian", "Civilian"));
            icons.Add(new KeyValuePair<string, string>("Embarked Air", "Embarked Air"));
            icons.Add(new KeyValuePair<string, string>("Embarked Heli", "Embarked Heli"));
            icons.Add(new KeyValuePair<string, string>("Embarked Naval", "Embarked Naval"));
            icons.Add(new KeyValuePair<string, string>("Embarked Rail", "Embarked Rail"));
            icons.Add(new KeyValuePair<string, string>("Engineer", "Engineer"));
            icons.Add(new KeyValuePair<string, string>("Airborne Engineer", "Engineer (Abn)"));
            icons.Add(new KeyValuePair<string, string>("Airmobile Engineer", "Engineer (Airmob)"));
            icons.Add(new KeyValuePair<string, string>("Armored Engineer", "Engineer (Armored)"));
            icons.Add(new KeyValuePair<string, string>("Ferry Engineer", "Engineer (Ferry)"));
            icons.Add(new KeyValuePair<string, string>("Motor Engineer", "Engineer (Mot)"));
            icons.Add(new KeyValuePair<string, string>("Fighter", "Fighter [icon]"));
            icons.Add(new KeyValuePair<string, string>("Fighter", "Fighter [silh]"));
            icons.Add(new KeyValuePair<string, string>("Jet Fighter", "Fighter (Jet)"));
            icons.Add(new KeyValuePair<string, string>("Naval Fighter", "Fighter (Naval)"));
            icons.Add(new KeyValuePair<string, string>("Fighter Bomber", "Fighter Bomber [icon]"));
            icons.Add(new KeyValuePair<string, string>("Fighter Bomber", "Fighter Bomber [silh]"));
            icons.Add(new KeyValuePair<string, string>("Garrison", "Garrison"));
            icons.Add(new KeyValuePair<string, string>("Guerilla", "Guerilla"));
            icons.Add(new KeyValuePair<string, string>("Headquarters", "Headquarters [v1]"));
            icons.Add(new KeyValuePair<string, string>("Headquarters", "Headquarters [v2]"));
            icons.Add(new KeyValuePair<string, string>("Airmobile Hvy Wpns", "Heavy Wpns (Airmob)"));
            icons.Add(new KeyValuePair<string, string>("Mountain Cav Hvy Wpns", "Heavy Wpns (Mtn Cav)"));
            icons.Add(new KeyValuePair<string, string>("Glider Hvy Wpns", "Heavy Wpns (Glider)"));
            icons.Add(new KeyValuePair<string, string>("Infantry Hvy Wpns", "Heavy Wpns (Infantry)"));
            icons.Add(new KeyValuePair<string, string>("Motor Hvy Wpns", "Heavy Wpns (Mot)"));
            icons.Add(new KeyValuePair<string, string>("Mountain Hvy Wpns", "Heavy Wpns (Mtn)"));
            icons.Add(new KeyValuePair<string, string>("Parachute Hvy Wpns", "Heavy Wpns (Para)"));
            icons.Add(new KeyValuePair<string, string>("Attack Helicopter", "Helicopter (Attack)"));
            icons.Add(new KeyValuePair<string, string>("Recon Helicopter", "Helicopter (Recon)"));
            icons.Add(new KeyValuePair<string, string>("Trans Helicopter", "Helicopter (Transport)"));
            icons.Add(new KeyValuePair<string, string>("Infantry", "Infantry"));
            icons.Add(new KeyValuePair<string, string>("Airmobile Infantry", "Infantry (Airmob)"));
            icons.Add(new KeyValuePair<string, string>("Glider Infantry", "Infantry (Glider)"));
            icons.Add(new KeyValuePair<string, string>("Marine Infantry", "Infantry (Marine)"));
            icons.Add(new KeyValuePair<string, string>("Mechanized", "Infantry (Mech)"));
            icons.Add(new KeyValuePair<string, string>("Motor Infantry", "Infantry (Mot)"));
            icons.Add(new KeyValuePair<string, string>("Mountain Infantry", "Infantry (Mtn)"));
            icons.Add(new KeyValuePair<string, string>("Parachute Infantry", "Infantry (Para)"));
            icons.Add(new KeyValuePair<string, string>("Irregular", "Irregular"));
            icons.Add(new KeyValuePair<string, string>("Machine Gun", "Machine Gun"));
            icons.Add(new KeyValuePair<string, string>("Motor Machinegun", "Machine Gun (Mot)"));
            icons.Add(new KeyValuePair<string, string>("Military Police", "Military Police"));
            icons.Add(new KeyValuePair<string, string>("Mortar", "Mortar"));
            icons.Add(new KeyValuePair<string, string>("Hvy Mortar", "Mortar (Heavy)"));
            icons.Add(new KeyValuePair<string, string>("Carrier Naval", "Naval (Carrier)"));
            icons.Add(new KeyValuePair<string, string>("Heavy Naval", "Naval (Heavy)"));
            icons.Add(new KeyValuePair<string, string>("Light Naval", "Naval (Light)"));
            icons.Add(new KeyValuePair<string, string>("Medium Naval", "Naval (Medium)"));
            icons.Add(new KeyValuePair<string, string>("Riverine", "Naval (Riverine)"));
            icons.Add(new KeyValuePair<string, string>("Naval Task Force", "Naval (Task Force)"));
            icons.Add(new KeyValuePair<string, string>("Naval Attack", "Naval Attack Aircraft"));
            icons.Add(new KeyValuePair<string, string>("Parachute", "Parachute"));
            icons.Add(new KeyValuePair<string, string>("Railroad Repair", "Railroad Repair"));
            icons.Add(new KeyValuePair<string, string>("Airborne Recon", "Recon (Airborne)"));
            icons.Add(new KeyValuePair<string, string>("Armored Recon", "Recon (Armored)"));
            icons.Add(new KeyValuePair<string, string>("Glider Recon", "Recon (Glider)"));
            icons.Add(new KeyValuePair<string, string>("Reserve", "Reserve"));
            icons.Add(new KeyValuePair<string, string>("Security", "Security"));
            icons.Add(new KeyValuePair<string, string>("Ski", "Ski"));
            icons.Add(new KeyValuePair<string, string>("Special Forces", "Special Forces"));
            icons.Add(new KeyValuePair<string, string>("Supply", "Supply"));
            icons.Add(new KeyValuePair<string, string>("Transport", "Transport [icon]"));
            icons.Add(new KeyValuePair<string, string>("Transport", "Transport [silh]"));
            icons.Add(new KeyValuePair<string, string>("Amphib Transport", "Transport (Amphib)"));
            icons.Add(new KeyValuePair<string, string>("Task Force", "Task Force"));
            icons.Add(new KeyValuePair<string, string>("Battlegroup", "Battle Group"));
            icons.Add(new KeyValuePair<string, string>("Kampfgruppe", "Kampfgruppe"));
            icons.Add(new KeyValuePair<string, string>("Combat Command A", "Combat Command A"));
            icons.Add(new KeyValuePair<string, string>("Combat Command B", "Combat Command B"));
            icons.Add(new KeyValuePair<string, string>("Combat Command C", "Combat Command C"));
            icons.Add(new KeyValuePair<string, string>("Combat Command R", "Combat Command R"));

            //LIST OF ALL ICONS WITH VARIANTS
            List<string> iconsVariants = new List<string>();
            iconsVariants.Add("Headquarters");
            iconsVariants.Add("Antitank");
            iconsVariants.Add("Motor Antitank");
            iconsVariants.Add("Fighter");
            iconsVariants.Add("Fighter Bomber");
            iconsVariants.Add("Light Bomber");
            iconsVariants.Add("Heavy Bomber");
            iconsVariants.Add("Coastal Artillery");
            iconsVariants.Add("Transport");

            //bool hasVariant;

            //LOOP THROUGH DATA REPEATERS
            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    Control control = row.Controls.Find("cboUnitType", true).First();
                    unitID = label.Text;

                    if (!control.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);

                    //LOOP THROUGH ALL ICONS
                    foreach (KeyValuePair<string, string> icon in icons)
                    {
                        //FOR ICONS W/O VARIANTS SWITCHING TO THOSE WITH VARIANTS
                        if ((icon.Value.ToString() == control.Text) && (unit.Attribute("ICONID") == null) && (iconsVariants.Contains(icon.Key.ToString()) == true))
                        {
                            switch (control.Text)
                            {
                            case "Antitank [v1]":
                                    unit.Attribute("ICON").Value = "Antitank";
                                    unit.Add(new XAttribute("ICONID", "14"));
                                    break;
                            case "Antitank [v2]":
                                    unit.Attribute("ICON").Value = "Antitank";
                                    unit.Add(new XAttribute("ICONID", "15"));
                                    break;
                            case "Antitank (Mot) [v1]":
                                    unit.Attribute("ICON").Value = "Motor Antitank";
                                    unit.Add(new XAttribute("ICONID", "25"));
                                    break;
                            case "Antitank (Mot) [v2]":
                                    unit.Attribute("ICON").Value = "Motor Antitank";
                                    unit.Add(new XAttribute("ICONID", "26"));
                                    break;
                            case "Artillery (Coast) [icon]":
                                    unit.Attribute("ICON").Value = "Coastal Artillery";
                                    unit.Add(new XAttribute("ICONID", "62"));
                                    break;
                            case "Artillery (Coast) [silh]":
                                    unit.Attribute("ICON").Value = "Coastal Artillery";
                                    unit.Add(new XAttribute("ICONID", "63"));
                                    break;
                            case "Bomber (Heavy) [icon]":
                                    unit.Attribute("ICON").Value = "Heavy Bomber";
                                    unit.Add(new XAttribute("ICONID", "69"));
                                    break;
                            case "Bomber (Heavy) [silh]":
                                    unit.Attribute("ICON").Value = "Heavy Bomber";
                                    unit.Add(new XAttribute("ICONID", "45"));
                                    break;
                            case "Bomber (Light) [icon]":
                                    unit.Attribute("ICON").Value = "Light Bomber";
                                    unit.Add(new XAttribute("ICONID", "43"));
                                    break;
                            case "Bomber (Light) [silh]":
                                    unit.Attribute("ICON").Value = "Light Bomber";
                                    unit.Add(new XAttribute("ICONID", "68"));
                                    break;
                            case "Fighter [icon]":
                                    unit.Attribute("ICON").Value = "Fighter";
                                    unit.Add(new XAttribute("ICONID", "41"));
                                    break;
                            case "Fighter [silh]":
                                    unit.Attribute("ICON").Value = "Fighter";
                                    unit.Add(new XAttribute("ICONID", "66"));
                                    break;
                            case "Fighter Bomber [icon]":
                                    unit.Attribute("ICON").Value = "Fighter Bomber";
                                    unit.Add(new XAttribute("ICONID", "42"));
                                    break;
                            case "Fighter Bomber [silh]":
                                    unit.Attribute("ICON").Value = "Fighter Bomber";
                                    unit.Add(new XAttribute("ICONID", "67"));
                                    break;
                            case "Headquarters [v1]":
                                    unit.Attribute("ICON").Value = "Headquarters";
                                    unit.Add(new XAttribute("ICONID", "0"));
                                    break;
                            case "Headquarters [v2]":
                                    unit.Attribute("ICON").Value = "Headquarters";
                                    unit.Add(new XAttribute("ICONID", "1"));
                                    break;
                            case "Transport [icon]":
                                    unit.Attribute("ICON").Value = "Transport";
                                    unit.Add(new XAttribute("ICONID", "82"));
                                    break;
                            case "Transport [silh]":
                                    unit.Attribute("ICON").Value = "Transport";
                                    unit.Add(new XAttribute("ICONID", "94"));
                                    break;
                            }
                        }

                        //FOR ICONS WITH NO VARIANTS
                        else if ((icon.Value.ToString() == control.Text) && (unit.Attribute("ICONID") == null))
                        {
                            unit.Attribute("ICON").Value = icon.Key.ToString();
                        }
                        //FOR ICONS WITH VARIANTS
                        else if ((icon.Value.ToString() == control.Text) && (unit.Attribute("ICONID") != null))                        
                        {
                            switch (unit.Attribute("ICONID").Value.ToString())
                            {
                                case "0":
                                    unit.Attribute("ICON").Value = "Headquarters";
                                    unit.Attribute("ICONID").Value = "1";
                                    break;
                                case "1":
                                    unit.Attribute("ICON").Value = "Headquarters";
                                    unit.Attribute("ICONID").Value = "0";
                                    break;
                                case "14":
                                    unit.Attribute("ICON").Value = "Antitank";
                                    unit.Attribute("ICONID").Value = "15";
                                    break;
                                case "15":
                                    unit.Attribute("ICON").Value = "Antitank";
                                    unit.Attribute("ICONID").Value = "14";
                                    break;
                                case "25":
                                    unit.Attribute("ICON").Value = "Motor Antitank";
                                    unit.Attribute("ICONID").Value = "26";
                                    break;
                                case "26":
                                    unit.Attribute("ICON").Value = "Motor Antitank";
                                    unit.Attribute("ICONID").Value = "25";
                                    break;
                                case "41":
                                    unit.Attribute("ICON").Value = "Fighter";
                                    unit.Attribute("ICONID").Value = "66";
                                    break;
                                case "42":
                                    unit.Attribute("ICON").Value = "Fighter Bomber";
                                    unit.Attribute("ICONID").Value = "67";
                                    break;
                                case "43":
                                    unit.Attribute("ICON").Value = "Light Bomber";
                                    unit.Attribute("ICONID").Value = "68";
                                    break;
                                case "45":
                                    unit.Attribute("ICON").Value = "Heavy Bomber";
                                    unit.Attribute("ICONID").Value = "69";
                                    break;
                                case "62":
                                    unit.Attribute("ICON").Value = "Coastal Artillery";
                                    unit.Attribute("ICONID").Value = "63";
                                    break;
                                case "63":
                                    unit.Attribute("ICON").Value = "Coastal Artillery";
                                    unit.Attribute("ICONID").Value = "62";
                                    break;
                                case "66":
                                    unit.Attribute("ICON").Value = "Fighter";
                                    unit.Attribute("ICONID").Value = "41";
                                    break;
                                case "67":
                                    unit.Attribute("ICON").Value = "Fighter Bomber";
                                    unit.Attribute("ICONID").Value = "42";
                                    break;
                                case "68":
                                    unit.Attribute("ICON").Value = "Light Bomber";
                                    unit.Attribute("ICONID").Value = "43";
                                    break;
                                case "69":
                                    unit.Attribute("ICON").Value = "Heavy Bomber";
                                    unit.Attribute("ICONID").Value = "45";
                                    break;
                                case "82":
                                    unit.Attribute("ICON").Value = "Transport";
                                    unit.Attribute("ICONID").Value = "94";
                                    break;
                                case "94":
                                    unit.Attribute("ICON").Value = "Transport";
                                    unit.Attribute("ICONID").Value = "82";
                                    break;
                                default:
                                    Console.WriteLine("ICON ERROR!");
                                    unit.Attribute("ICONID").Remove();
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private void cboUnitSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    Control size = row.Controls.Find("cboUnitSize", true).First();
                    unitID = label.Text;

                    if (!size.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);

                    if (size.Text != null) unit.Attribute("SIZE").Value = size.Text;

                    break;
                }
            }
        }

        private void txtUnitProf_Enter(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control prof = row.Controls.Find("txtUnitProf", true).First();

                    if (!prof.Focused) return;

                    oldprof = prof.Text;
                }
            }
        }

        private void txtUnitProf_Leave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control prof = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    prof = row.Controls.Find("txtUnitProf", true).First();
                    unitID = label.Text;

                    if (!prof.Focused) return;

                    //VALIDATE AS NUMBER
                    int profnum = 0;
                    bool isNum = int.TryParse(prof.Text, out profnum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        prof.Text = oldprof;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);
                        if (!withinRange) prof.Text = oldprof;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (prof.Text != null) unit.Attribute("PROFICIENCY").Value = prof.Text;
                    break;
                }
            }
        }

        private void txtUnitProf_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control prof = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    prof = row.Controls.Find("txtUnitProf", true).First();
                    unitID = label.Text;

                    if (!prof.Focused) return;

                    //VALIDATE AS NUMBER
                    int profnum = 0;
                    bool isNum = int.TryParse(prof.Text, out profnum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        prof.Text = oldprof;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(profnum, "Proficiency", 1, 100);
                        if (!withinRange) prof.Text = oldprof;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (prof.Text != null) unit.Attribute("PROFICIENCY").Value = prof.Text;
                    break;
                }
            }
        }

        private void txtUnitSupply_Enter(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control supply = row.Controls.Find("txtUnitSupply", true).First();

                    if (!supply.Focused) return;

                    oldsupply = supply.Text;
                }
            }
        }

        private void txtUnitSupply_Leave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control supply = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    supply = row.Controls.Find("txtUnitSupply", true).First();
                    unitID = label.Text;

                    if (!supply.Focused) return;

                    //VALIDATE AS NUMBER
                    int supplynum = 0;
                    bool isNum = int.TryParse(supply.Text, out supplynum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        supply.Text = oldsupply;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);
                        if (!withinRange) supply.Text = oldsupply;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (supply.Text != null) unit.Attribute("SUPPLY").Value = supply.Text;
                    break;
                }
            }
        }

        private void txtUnitSupply_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control supply = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    supply = row.Controls.Find("txtUnitSupply", true).First();
                    unitID = label.Text;

                    if (!supply.Focused) return;

                    //VALIDATE AS NUMBER
                    int supplynum = 0;
                    bool isNum = int.TryParse(supply.Text, out supplynum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        supply.Text = oldsupply;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(supplynum, "Supply", 1, 100);
                        if (!withinRange) supply.Text = oldsupply;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (supply.Text != null) unit.Attribute("SUPPLY").Value = supply.Text;
                    break;
                }
            }
        }

        private void txtUnitReadiness_Enter(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control ready = row.Controls.Find("txtUnitReadiness", true).First();

                    if (!ready.Focused) return;

                    oldready = ready.Text;
                }
            }
        }

        private void txtUnitReadiness_Leave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control ready = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    ready = row.Controls.Find("txtUnitReadiness", true).First();
                    unitID = label.Text;

                    if (!ready.Focused) return;

                    //VALIDATE AS NUMBER
                    int readynum = 0;
                    bool isNum = int.TryParse(ready.Text, out readynum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ready.Text = oldready;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(readynum, "Readiness", 1, 100);
                        if (!withinRange) ready.Text = oldready;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (ready.Text != null) unit.Attribute("READINESS").Value = ready.Text;
                    break;
                }
            }
        }

        private void txtUnitReadiness_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control ready = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    ready = row.Controls.Find("txtUnitReadiness", true).First();
                    unitID = label.Text;

                    if (!ready.Focused) return;

                    //VALIDATE AS NUMBER
                    int readynum = 0;
                    bool isNum = int.TryParse(ready.Text, out readynum);

                    if (isNum == false)
                    {
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        ready.Text = oldready;
                    }
                    else
                    {
                        bool withinRange = IsWithinRange(readynum, "Readiness", 1, 100);
                        if (!withinRange) ready.Text = oldready;
                    }

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (ready.Text != null) unit.Attribute("READINESS").Value = ready.Text;
                    break;
                }
            }
        }

        private void cboExp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    Control exper = row.Controls.Find("cboExp", true).First();
                    unitID = label.Text;

                    if (!exper.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);

                    if (exper.Text != null) unit.Attribute("EXPERIENCE").Value = exper.Text;

                    break;
                }
            }
        }

        private void cboUnitLossTol_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    Control losstol = row.Controls.Find("cboUnitLossTol", true).First();
                    unitID = label.Text;

                    if (!losstol.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);

                    if (losstol.Text != null) unit.Attribute("EMPHASIS").Value = losstol.Text;

                    break;
                }
            }
        }

        private void cboReplace_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    Control replace = row.Controls.Find("cboReplace", true).First();
                    unitID = label.Text;

                    if (!replace.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    string replaceID = GetReplacementPriorityID(replace.Text);

                    if (replace.Text != null) unit.Attribute("REPLACEMENTPRIORITY").Value = replaceID;

                    break;
                }
            }
        }

        private void cboUnitOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    Control orders = row.Controls.Find("cboUnitOrders", true).First();
                    unitID = label.Text;

                    if (!orders.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    string order = SetUnitOrderID(orders.Text);

                    if (orders.Text != null) unit.Attribute("STATUS").Value = order;

                    break;
                }
            }
        }

        private void cboHdrFormSupport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboHdrFormSupport.Focused) return;
                        
            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (cboHdrFormSupport.Text != null) form.Attribute("SUPPORTSCOPE").Value = cboHdrFormSupport.Text;
        }

        private void cboHdrFormOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboHdrFormOrders.Focused) return;

            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (cboHdrFormOrders.Text != null) form.Attribute("ORDERS").Value = cboHdrFormOrders.Text;
        }

        private void cboHdrFormLossTol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboHdrFormLossTol.Focused) return;

            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (cboHdrFormLossTol.Text != null) form.Attribute("EMPHASIS").Value = cboHdrFormLossTol.Text;
        }

        private void txtHdrForceName_Enter(object sender, EventArgs e)
        {
            oldname = txtHdrForceName.Text;

        }

        private void txtHdrForceName_Leave(object sender, EventArgs e)
        {
            string newname = txtHdrForceName.Text;

            IEnumerable<XElement> forcename =
                from f in tacFile.Descendants("OOB").Descendants("FORCE")
                where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                select f;

            if (forcename.Count() > 1)
            {
                MessageBox.Show("Force cannot have same name as other force!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrForceName.Text = oldname;
            }
            else
            {
                string xpath = "OOB/FORCE[@ID=" + forceID + "]";
                var force = tacFile.XPathSelectElement(xpath);
                if (txtHdrForceName.Text != null) force.Attribute("NAME").Value = txtHdrForceName.Text;

                //REVISE TREE NODE
                foreach (TreeNode node in trvUnitTree.Nodes)
                {
                        if (node.Name == "FORCE")
                        {
                            if (node.Tag.ToString() == forceID)
                            {
                                node.Text = txtHdrForceName.Text;
                                trvUnitTree.Refresh();
                                break;
                            }
                        }
                }
            }
        }

        private void txtHdrForceName_MouseLeave(object sender, EventArgs e)
        {
            string newname = txtHdrForceName.Text;

            IEnumerable<XElement> forcename =
                from f in tacFile.Descendants("OOB").Descendants("FORCE")
                where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                select f;

            if (forcename.Count() > 1)
            {
                MessageBox.Show("Force cannot have same name as other force!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrForceName.Text = oldname;
            }
            else
            {
                string xpath = "OOB/FORCE[@ID=" + forceID + "]";
                var force = tacFile.XPathSelectElement(xpath);
                if (txtHdrForceName.Text != null) force.Attribute("NAME").Value = txtHdrForceName.Text;

                //REVISE TREE NODE
                foreach (TreeNode node in trvUnitTree.Nodes)
                {
                    if (node.Name == "FORCE")
                    {
                        if (node.Tag.ToString() == forceID)
                        {
                            node.Text = txtHdrForceName.Text;
                            trvUnitTree.Refresh();
                            break;
                        }
                    }
                } 
            }
        }

        private void txtHdrForceCdr_Leave(object sender, EventArgs e)
        {
                string xpath = "OOB/FORCE[@ID=" + forceID + "]";
                var force = tacFile.XPathSelectElement(xpath);
                if (txtHdrForceCdr.Text != null) force.Attribute("CDR").Value = txtHdrForceCdr.Text;
        }

        private void txtHdrForceCdr_MouseLeave(object sender, EventArgs e)
        {
            string xpath = "OOB/FORCE[@ID=" + forceID + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrForceCdr.Text != null) force.Attribute("CDR").Value = txtHdrForceCdr.Text;
        }

        private void txtHdrForceRank_Leave(object sender, EventArgs e)
        {
            string xpath = "OOB/FORCE[@ID=" + forceID + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrForceRank.Text != null) force.Attribute("RANK").Value = txtHdrForceRank.Text;
        }

        private void txtHdrForceRank_MouseLeave(object sender, EventArgs e)
        {
            string xpath = "OOB/FORCE[@ID=" + forceID + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrForceRank.Text != null) force.Attribute("RANK").Value = txtHdrForceRank.Text;
        }

        private void txtHdrForceRating_Leave(object sender, EventArgs e)
        {
            string xpath = "OOB/FORCE[@ID=" + forceID + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrForceRating.Text != null) force.Attribute("RATING").Value = txtHdrForceRating.Text;
        }

        private void txtHdrForceRating_MouseLeave(object sender, EventArgs e)
        {
            string xpath = "OOB/FORCE[@ID=" + forceID + "]";
            var force = tacFile.XPathSelectElement(xpath);
            if (txtHdrForceRating.Text != null) force.Attribute("RATING").Value = txtHdrForceRating.Text;
        }

        private void txtHdrFormName_Enter(object sender, EventArgs e)
        {
            haschanged = false;
            oldname = txtHdrFormName.Text;
        }

        private void txtHdrFormName_TextChanged(object sender, EventArgs e)
        {
            haschanged = true;
        }

        private void txtHdrFormName_Leave(object sender, EventArgs e)
        {
            string newname = txtHdrFormName.Text;

            IEnumerable<XElement> formname =
                from f in tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION")
                where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                select f;

            if ((haschanged == true) && (formname.Count() >= 1))
            {
                MessageBox.Show("Formation cannot have same name as another formation!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrFormName.Text = oldname;
            }
            else
            {
                string formid = trvUnitTree.SelectedNode.Tag.ToString();
                string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
                var form = tacFile.XPathSelectElement(xpath);
                if (txtHdrFormName.Text != null) form.Attribute("NAME").Value = txtHdrFormName.Text;

                //REVISE TREE NODE
                foreach (TreeNode node in trvUnitTree.Nodes)
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                      //  foreach (TreeNode grandchild in child.Nodes)
                            if (child.Name == "FORMATION")
                            {
                                if (child.Tag.ToString() == formid)
                                {
                                    child.Text = txtHdrFormName.Text;
                                    trvUnitTree.Refresh();
                                    break;
                                }
                            }
                    }
                }

                haschanged = false;
            }
        }

        private void txtHdrFormName_MouseLeave(object sender, EventArgs e)
        {
            string newname = txtHdrFormName.Text;

            IEnumerable<XElement> formname =
                from f in tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION")
                where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                select f;

            if ((haschanged == true) && (formname.Count() >= 1))
            {
                MessageBox.Show("Formation cannot have same name as another formation!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrFormName.Text = oldname;
            }
            else
            {
                string formid = trvUnitTree.SelectedNode.Tag.ToString();
                string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
                var form = tacFile.XPathSelectElement(xpath);
                if (txtHdrFormName.Text != null) form.Attribute("NAME").Value = txtHdrFormName.Text;

                //REVISE TREE NODE
                foreach (TreeNode node in trvUnitTree.Nodes)
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                        //  foreach (TreeNode grandchild in child.Nodes)
                        if (child.Name == "FORMATION")
                        {
                            if (child.Tag.ToString() == formid)
                            {
                                child.Text = txtHdrFormName.Text;
                                trvUnitTree.Refresh();
                                break;
                            }
                        }
                    }
                }

                haschanged = false;
            }
        }

        private void txtHdrFormCdr_Leave(object sender, EventArgs e)
        {
            if (!txtHdrFormCdr.Focused) return;

            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormCdr.Text != null) form.Attribute("CDR").Value = txtHdrFormCdr.Text;
        }

        private void txtHdrFormCdr_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrFormCdr.Focused) return;

            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormCdr.Text != null) form.Attribute("CDR").Value = txtHdrFormCdr.Text;
        }

        private void txtHdrFormRank_Leave(object sender, EventArgs e)
        {
            if (!txtHdrFormRank.Focused) return;

            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormRank.Text != null) form.Attribute("RANK").Value = txtHdrFormRank.Text;
        }

        private void txtHdrFormRank_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrFormRank.Focused) return;

            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormRank.Text != null) form.Attribute("RANK").Value = txtHdrFormRank.Text;
        }

        private void txtHdrFormRating_Leave(object sender, EventArgs e)
        {
            if (!txtHdrFormRating.Focused) return;

            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormRating.Text != null) form.Attribute("RATING").Value = txtHdrFormRating.Text;
        }

        private void txtHdrFormRating_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrFormRating.Focused) return;

            //CHANGE TACFILE XML
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrFormRating.Text != null) form.Attribute("RATING").Value = txtHdrFormRating.Text;
        }

        private void txtHdrUnitName_Enter(object sender, EventArgs e)
        {
            haschanged = false;
            oldname = txtHdrUnitName.Text;
        }

        private void txtHdrUnitName_TextChanged(object sender, EventArgs e)
        {
            haschanged = true;
        }

        private void txtHdrUnitName_Leave(object sender, EventArgs e)
        {
            string newname = txtHdrUnitName.Text;

            IEnumerable<XElement> unitname =
                from f in tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT")
                where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                select f;

            if ((haschanged == true) && (unitname.Count() >= 1))
            {
                MessageBox.Show("Unit cannot have same name as another unit!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrUnitName.Text = oldname;

            }
            else
            {
                string unitid = trvUnitTree.SelectedNode.Tag.ToString();
                string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                var form = tacFile.XPathSelectElement(xpath);
                if (txtHdrUnitName.Text != null) form.Attribute("NAME").Value = txtHdrUnitName.Text;

                foreach (TreeNode node in trvUnitTree.Nodes)
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                        foreach (TreeNode grandchild in child.Nodes)
                            if (grandchild.Name == "UNIT")
                            {
                                if (grandchild.Tag.ToString() == unitid)
                                {
                                    grandchild.Text = txtHdrUnitName.Text;
                                    trvUnitTree.Refresh();
                                    break;
                                }
                            }
                    }
                }

                haschanged = false;
            }
        }

        private void txtHdrUnitName_MouseLeave(object sender, EventArgs e)
        {
            string newname = txtHdrUnitName.Text;

            IEnumerable<XElement> unitname =
                from f in tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT")
                where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                select f;

            if ((haschanged == true) && (unitname.Count() >= 1))
            {
                MessageBox.Show("Unit cannot have same name as another unit!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtHdrUnitName.Text = oldname;
            }
            else
            {
                string unitid = trvUnitTree.SelectedNode.Tag.ToString();
                string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                var form = tacFile.XPathSelectElement(xpath);
                if (txtHdrUnitName.Text != null) form.Attribute("NAME").Value = txtHdrUnitName.Text;

                foreach (TreeNode node in trvUnitTree.Nodes)
                {
                    foreach (TreeNode child in node.Nodes)
                    {
                        foreach (TreeNode grandchild in child.Nodes)
                            if (grandchild.Name == "UNIT")
                            {
                                if (grandchild.Tag.ToString() == unitid)
                                {
                                    grandchild.Text = txtHdrUnitName.Text;
                                    trvUnitTree.Refresh();
                                    break;
                                }
                            }
                    }
                }

                haschanged = false;
            }
        }

        private void txtName_Enter(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    Control name = row.Controls.Find("txtName", true).First();
                    formID = label.Text;
                    oldname = name.Text;
                    haschanged = false;
                    return;
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            haschanged = true;
        }

        private void txtName_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    Control name = row.Controls.Find("txtName", true).First();
                    formID = label.Text;
                    string newname = name.Text;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (name.Text != null) formation.Attribute("NAME").Value = name.Text;

                    IEnumerable<XElement> unitname =
                        from f in tacFile.Elements("OOB").Elements("FORCE").Elements("FORMATION")
                        where f.Attribute("NAME").Value.Equals(newname, StringComparison.OrdinalIgnoreCase)
                        select f;

                    if ((haschanged == true) && (unitname.Count() > 1))
                    {
                        MessageBox.Show("Formation cannot have same name as another formation!", "Invalid Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        name.Text = oldname;
                        haschanged = false;
                    }

                    //REVISE TREE NODE
                    foreach (TreeNode node in trvUnitTree.Nodes)
                    {
                        foreach (TreeNode child in node.Nodes)
                        {
                            if (child.Name == "FORMATION")
                            {
                                if (child.Tag.ToString() == formID)
                                {
                                    child.Text = name.Text;
                                    trvUnitTree.Refresh();
                                    break;
                                }
                            }
                        }
                    }
                    break;
                }
            }
        }

        private void txtUnitName_Enter(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    Control name = row.Controls.Find("txtUnitName", true).First();
                    unitID = label.Text;
                    oldname = name.Text;
                    haschanged = false;
                    return;
                }
            }
        }

        private void txtUnitName_TextChanged(object sender, EventArgs e)
        {
            haschanged = true;
        }

        private void cboHdrUnitSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboHdrUnitSize.Focused) return;

            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (cboHdrUnitSize.Text != null) form.Attribute("SIZE").Value = cboHdrUnitSize.Text;
        }

        private void cboHdrUnitLossTol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboHdrUnitLossTol.Focused) return;

            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (cboHdrUnitLossTol.Text != null) form.Attribute("EMPHASIS").Value = cboHdrUnitLossTol.Text;
        }

        private void cboHdrUnitExp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboHdrUnitExp.Focused) return;

            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (cboHdrUnitExp.Text != null) form.Attribute("EXPERIENCE").Value = cboHdrUnitExp.Text;
        }

        private void cboHdrUnitType_MouseWheel(object sender, MouseEventArgs e)
        {
            if (sender == cboUnitType)
            {
                ((HandledMouseEventArgs)e).Handled = false;
            }
        }

        private void cboHdrUnitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string unitID = "";
            string icon = "";

            //LIST OF ALL ICONS WITH VARIANTS
            List<string> iconsVariants = new List<string>();
            iconsVariants.Add("Headquarters");
            iconsVariants.Add("Antitank");
            iconsVariants.Add("Motor Antitank");
            iconsVariants.Add("Fighter");
            iconsVariants.Add("Fighter Bomber");
            iconsVariants.Add("Light Bomber");
            iconsVariants.Add("Heavy Bomber");
            iconsVariants.Add("Coastal Artillery");
            iconsVariants.Add("Transport");

            if (!cboHdrUnitType.Focused) return;

            //CHANGE TACFILE XML
            unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
            var unit = tacFile.XPathSelectElement(xpath);

            icon = cboHdrUnitType.SelectedValue.ToString();
            bool hasVariant = iconsVariants.Any(v=>v == icon);

            if (!hasVariant)
            {
                unit.Attribute("ICON").Value = icon;
                if (unit.Attribute("ICONID") != null) unit.Attribute("ICONID").Remove();
            }
            else
            {
                switch (cboHdrUnitType.Text)
                {
                    case "Antitank [v1]":
                        unit.Attribute("ICON").Value = "Antitank";
                        if(unit.Attribute("ICONID")==null)
                        {
                            unit.Add(new XAttribute("ICONID", "14"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "14";
                        }
                        break;
                    case "Antitank [v2]":
                        unit.Attribute("ICON").Value = "Antitank";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "15"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "15";
                        }
                        break;
                    case "Antitank (Mot) [v1]":
                        unit.Attribute("ICON").Value = "Motor Antitank";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "25"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "25";
                        }
                        break;
                    case "Antitank (Mot) [v2]":
                        unit.Attribute("ICON").Value = "Motor Antitank";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "26"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "26";
                        }
                        break;
                    case "Artillery (Coast) [icon]":
                        unit.Attribute("ICON").Value = "Coastal Artillery";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "62"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "62";
                        }
                        break;
                    case "Artillery (Coast) [silh]":
                        unit.Attribute("ICON").Value = "Coastal Artillery";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "63"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "63";
                        }
                        break;
                    case "Bomber (Heavy) [icon]":
                        unit.Attribute("ICON").Value = "Heavy Bomber";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "69"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "69";
                        }
                        break;
                    case "Bomber (Heavy) [silh]":
                        unit.Attribute("ICON").Value = "Heavy Bomber";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "45"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "45";
                        }
                        break;
                    case "Bomber (Light) [icon]":
                        unit.Attribute("ICON").Value = "Light Bomber";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "43"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "43";
                        }
                        break;
                    case "Bomber (Light) [silh]":
                        unit.Attribute("ICON").Value = "Light Bomber";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "68"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "68";
                        }
                        break;
                    case "Fighter [icon]":
                        unit.Attribute("ICON").Value = "Fighter";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "41"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "41";
                        }
                        break;
                    case "Fighter [silh]":
                        unit.Attribute("ICON").Value = "Fighter";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "66"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "66";
                        }
                        break;
                    case "Fighter Bomber [icon]":
                        unit.Attribute("ICON").Value = "Fighter Bomber";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "42"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "42";
                        }
                        break;
                    case "Fighter Bomber [silh]":
                        unit.Attribute("ICON").Value = "Fighter Bomber";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "67"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "67";
                        }
                        break;
                    case "Headquarters [v1]":
                        unit.Attribute("ICON").Value = "Headquarters";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "0"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "0";
                        }
                        break;
                    case "Headquarters [v2]":
                        unit.Attribute("ICON").Value = "Headquarters";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "1"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "1";
                        }
                        break;
                    case "Transport [icon]":
                        unit.Attribute("ICON").Value = "Transport";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "82"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "82";
                        }
                        break;
                    case "Transport [silh]":
                        unit.Attribute("ICON").Value = "Transport";
                        if (unit.Attribute("ICONID") == null)
                        {
                            unit.Add(new XAttribute("ICONID", "94"));
                        }
                        else
                        {
                            unit.Attribute("ICONID").Value = "94";
                        }
                        break;
                }
            }
                //    ////LOOP THROUGH ALL ICONS
                //    //foreach (KeyValuePair<string, string> icon in icons)
                //    //{
                //    //    //FOR ICONS W/O VARIANTS SWITCHING TO THOSE WITH VARIANTS
                //    //    if ((icon.Value.ToString() == cboHdrUnitType.Text) && (unit.Attribute("ICONID") == null) && (iconsVariants.Contains(icon.Key.ToString()) == true))
                //    //    {
                //    //        switch (cboHdrUnitType.Text)
                //    //        {
                //    //            case "Antitank [v1]":
                //    //                unit.Attribute("ICON").Value = "Antitank";
                //    //                unit.Add(new XAttribute("ICONID", "14"));
                //    //                break;
                //    //            case "Antitank [v2]":
                //    //                unit.Attribute("ICON").Value = "Antitank";
                //    //                unit.Add(new XAttribute("ICONID", "15"));
                //    //                break;
                //    //            case "Antitank (Mot) [v1]":
                //    //                unit.Attribute("ICON").Value = "Motor Antitank";
                //    //                unit.Add(new XAttribute("ICONID", "25"));
                //    //                break;
                //    //            case "Antitank (Mot) [v2]":
                //    //                unit.Attribute("ICON").Value = "Motor Antitank";
                //    //                unit.Add(new XAttribute("ICONID", "26"));
                //    //                break;
                //    //            case "Artillery (Coast) [icon]":
                //    //                unit.Attribute("ICON").Value = "Coastal Artillery";
                //    //                unit.Add(new XAttribute("ICONID", "62"));
                //    //                break;
                //    //            case "Artillery (Coast) [silh]":
                //    //                unit.Attribute("ICON").Value = "Coastal Artillery";
                //    //                unit.Add(new XAttribute("ICONID", "63"));
                //    //                break;
                //    //            case "Bomber (Heavy) [icon]":
                //    //                unit.Attribute("ICON").Value = "Heavy Bomber";
                //    //                unit.Add(new XAttribute("ICONID", "69"));
                //    //                break;
                //    //            case "Bomber (Heavy) [silh]":
                //    //                unit.Attribute("ICON").Value = "Heavy Bomber";
                //    //                unit.Add(new XAttribute("ICONID", "45"));
                //    //                break;
                //    //            case "Bomber (Light) [icon]":
                //    //                unit.Attribute("ICON").Value = "Light Bomber";
                //    //                unit.Add(new XAttribute("ICONID", "43"));
                //    //                break;
                //    //            case "Bomber (Light) [silh]":
                //    //                unit.Attribute("ICON").Value = "Light Bomber";
                //    //                unit.Add(new XAttribute("ICONID", "68"));
                //    //                break;
                //    //            case "Fighter [icon]":
                //    //                unit.Attribute("ICON").Value = "Fighter";
                //    //                unit.Add(new XAttribute("ICONID", "41"));
                //    //                break;
                //    //            case "Fighter [silh]":
                //    //                unit.Attribute("ICON").Value = "Fighter";
                //    //                unit.Add(new XAttribute("ICONID", "66"));
                //    //                break;
                //    //            case "Fighter Bomber [icon]":
                //    //                unit.Attribute("ICON").Value = "Fighter Bomber";
                //    //                unit.Add(new XAttribute("ICONID", "42"));
                //    //                break;
                //    //            case "Fighter Bomber [silh]":
                //    //                unit.Attribute("ICON").Value = "Fighter Bomber";
                //    //                unit.Add(new XAttribute("ICONID", "67"));
                //    //                break;
                //    //            case "Headquarters [v1]":
                //    //                unit.Attribute("ICON").Value = "Headquarters";
                //    //                unit.Add(new XAttribute("ICONID", "0"));
                //    //                break;
                //    //            case "Headquarters [v2]":
                //    //                unit.Attribute("ICON").Value = "Headquarters";
                //    //                unit.Add(new XAttribute("ICONID", "1"));
                //    //                break;
                //    //            case "Transport [icon]":
                //    //                unit.Attribute("ICON").Value = "Transport";
                //    //                unit.Add(new XAttribute("ICONID", "82"));
                //    //                break;
                //    //            case "Transport [silh]":
                //    //                unit.Attribute("ICON").Value = "Transport";
                //    //                unit.Add(new XAttribute("ICONID", "94"));
                //    //                break;
                //    //        }
                //    //    }

                //    //    //FOR ICONS WITH NO VARIANTS
                //    //    else if ((icon.Value.ToString() == cboHdrUnitType.Text) && (unit.Attribute("ICONID") == null))
                //    //    {
                //    //        unit.Attribute("ICON").Value = icon.Key.ToString();
                //    //    }
                //    //    //FOR ICONS WITH VARIANTS
                //    //    else if ((icon.Value.ToString() == cboHdrUnitType.Text) && (unit.Attribute("ICONID") != null))
                //    //    {
                //    //        switch (unit.Attribute("ICONID").Value.ToString())
                //    //        {
                //    //            case "0":
                //    //                unit.Attribute("ICON").Value = "Headquarters";
                //    //                unit.Attribute("ICONID").Value = "1";
                //    //                break;
                //    //            case "1":
                //    //                unit.Attribute("ICON").Value = "Headquarters";
                //    //                unit.Attribute("ICONID").Value = "0";
                //    //                break;
                //    //            case "14":
                //    //                unit.Attribute("ICON").Value = "Antitank";
                //    //                unit.Attribute("ICONID").Value = "15";
                //    //                break;
                //    //            case "15":
                //    //                unit.Attribute("ICON").Value = "Antitank";
                //    //                unit.Attribute("ICONID").Value = "14";
                //    //                break;
                //    //            case "25":
                //    //                unit.Attribute("ICON").Value = "Motor Antitank";
                //    //                unit.Attribute("ICONID").Value = "26";
                //    //                break;
                //    //            case "26":
                //    //                unit.Attribute("ICON").Value = "Motor Antitank";
                //    //                unit.Attribute("ICONID").Value = "25";
                //    //                break;
                //    //            case "41":
                //    //                unit.Attribute("ICON").Value = "Fighter";
                //    //                unit.Attribute("ICONID").Value = "66";
                //    //                break;
                //    //            case "42":
                //    //                unit.Attribute("ICON").Value = "Fighter Bomber";
                //    //                unit.Attribute("ICONID").Value = "67";
                //    //                break;
                //    //            case "43":
                //    //                unit.Attribute("ICON").Value = "Light Bomber";
                //    //                unit.Attribute("ICONID").Value = "68";
                //    //                break;
                //    //            case "45":
                //    //                unit.Attribute("ICON").Value = "Heavy Bomber";
                //    //                unit.Attribute("ICONID").Value = "69";
                //    //                break;
                //    //            case "62":
                //    //                unit.Attribute("ICON").Value = "Coastal Artillery";
                //    //                unit.Attribute("ICONID").Value = "63";
                //    //                break;
                //    //            case "63":
                //    //                unit.Attribute("ICON").Value = "Coastal Artillery";
                //    //                unit.Attribute("ICONID").Value = "62";
                //    //                break;
                //    //            case "66":
                //    //                unit.Attribute("ICON").Value = "Fighter";
                //    //                unit.Attribute("ICONID").Value = "41";
                //    //                break;
                //    //            case "67":
                //    //                unit.Attribute("ICON").Value = "Fighter Bomber";
                //    //                unit.Attribute("ICONID").Value = "42";
                //    //                break;
                //    //            case "68":
                //    //                unit.Attribute("ICON").Value = "Light Bomber";
                //    //                unit.Attribute("ICONID").Value = "43";
                //    //                break;
                //    //            case "69":
                //    //                unit.Attribute("ICON").Value = "Heavy Bomber";
                //    //                unit.Attribute("ICONID").Value = "45";
                //    //                break;
                //    //            case "82":
                //    //                unit.Attribute("ICON").Value = "Transport";
                //    //                unit.Attribute("ICONID").Value = "94";
                //    //                break;
                //    //            case "94":
                //    //                unit.Attribute("ICON").Value = "Transport";
                //    //                unit.Attribute("ICONID").Value = "82";
                //    //                break;
                //    //            default:
                //    //                Console.WriteLine("ICON ERROR!");
                //    //                unit.Attribute("ICONID").Remove();
                //    //                break;
                //    //        }
                //    //    }
                //    //}
                //    //}
                //    //}
            }

        private void cboHdrUnitOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitid + "]";
            var unit = tacFile.XPathSelectElement(xpath);
            string order = SetUnitOrderID(cboHdrUnitOrders.Text);
            unit.Attribute("STATUS").Value = order;
        }

        private void cboHdrUnitReplace_SelectedIndexChanged(object sender, EventArgs e)
        {
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitid + "]";
            var unit = tacFile.XPathSelectElement(xpath);
            string replaceID = GetReplacementPriorityID(cboHdrUnitReplace.Text);
            unit.Attribute("REPLACEMENTPRIORITY").Value = replaceID;
        }

        private void txtHdrUnitCdr_Leave(object sender, EventArgs e)
        {
            if (!txtHdrUnitCdr.Focused) return;

            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitCdr.Text != null) form.Attribute("CDR").Value = txtHdrUnitCdr.Text;
        }

        private void txtHdrUnitCdr_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrUnitCdr.Focused) return;

            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitCdr.Text != null) form.Attribute("CDR").Value = txtHdrUnitCdr.Text;
        }

        private void txtHdrUnitRank_Leave(object sender, EventArgs e)
        {
            if (!txtHdrUnitRank.Focused) return;

            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitRank.Text != null) form.Attribute("RANK").Value = txtHdrUnitRank.Text;
        }

        private void txtHdrUnitRank_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrUnitRank.Focused) return;

            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitRank.Text != null) form.Attribute("RANK").Value = txtHdrUnitRank.Text;
        }

        private void txtHdrUnitRating_Leave(object sender, EventArgs e)
        {
            if (!txtHdrUnitRating.Focused) return;

            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitRating.Text != null) form.Attribute("RATING").Value = txtHdrUnitRating.Text;
        }

        private void txtHdrUnitRating_MouseLeave(object sender, EventArgs e)
        {
            if (!txtHdrUnitRating.Focused) return;

            //CHANGE TACFILE XML
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var form = tacFile.XPathSelectElement(xpath);
            if (txtHdrUnitRating.Text != null) form.Attribute("RATING").Value = txtHdrUnitRating.Text;
        }

        private void txtCdr_Leave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control cdr = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    cdr = row.Controls.Find("txtCdr", true).First();
                    formID = label.Text;

                    if (!cdr.Focused) return;

                   //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (cdr.Text != null) formation.Attribute("CDR").Value = cdr.Text;
                }
            }
        }

        private void txtCdr_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control cdr = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    cdr = row.Controls.Find("txtCdr", true).First();
                    formID = label.Text;

                    if (!cdr.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (cdr.Text != null) formation.Attribute("CDR").Value = cdr.Text;
                }
            }
        }

        private void txtRank_Leave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control rank = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    rank = row.Controls.Find("txtRank", true).First();
                    formID = label.Text;

                    if (!rank.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (rank.Text != null) formation.Attribute("RANK").Value = rank.Text;
                }
            }
        }

        private void txtRank_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control rank = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    rank = row.Controls.Find("txtRank", true).First();
                    formID = label.Text;

                    if (!rank.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (rank.Text != null) formation.Attribute("RANK").Value = rank.Text;
                }
            }
        }

        private void txtRating_Leave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control rating = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    rating = row.Controls.Find("txtRating", true).First();
                    formID = label.Text;

                    if (!rating.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (rating.Text != null) formation.Attribute("RATING").Value = rating.Text;
                }
            }
        }

        private void txtRating_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drForce.CurrentItemIndex;
            string formID = "";
            Control rating = null;

            foreach (DataRepeaterItem row in drForce.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblFormID", true).First();
                    rating = row.Controls.Find("txtRating", true).First();
                    formID = label.Text;

                    if (!rating.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (rating.Text != null) formation.Attribute("RATING").Value = rating.Text;
                }
            }
        }

        private void txtUnitCdr_Leave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control cdr = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    cdr = row.Controls.Find("txtUnitCdr", true).First();
                    unitID = label.Text;

                    if (!cdr.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (cdr.Text != null) unit.Attribute("CDR").Value = cdr.Text;
                }
            }
        }

        private void txtUnitCdr_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control cdr = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    cdr = row.Controls.Find("txtUnitCdr", true).First();
                    unitID = label.Text;

                    if (!cdr.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (cdr.Text != null) unit.Attribute("CDR").Value = cdr.Text;
                }
            }
        }

        private void txtUnitRank_Leave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control rank = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    rank = row.Controls.Find("txtUnitRank", true).First();
                    unitID = label.Text;

                    if (!rank.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (rank.Text != null) unit.Attribute("RANK").Value = rank.Text;
                }
            }
        }

        private void txtUnitRank_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control rank = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    rank = row.Controls.Find("txtUnitRank", true).First();
                    unitID = label.Text;

                    if (!rank.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (rank.Text != null) unit.Attribute("RANK").Value = rank.Text;
                }
            }
        }

        private void txtUnitRating_Leave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control rating = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    rating = row.Controls.Find("txtUnitRating", true).First();
                    unitID = label.Text;

                    if (!rating.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (rating.Text != null) unit.Attribute("RATING").Value = rating.Text;
                }
            }
        }

        private void txtUnitRating_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drFormation.CurrentItemIndex;
            string unitID = "";
            Control rating = null;

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    rating = row.Controls.Find("txtUnitRating", true).First();
                    unitID = label.Text;

                    if (!rating.Focused) return;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (rating.Text != null) unit.Attribute("RATING").Value = rating.Text;
                }
            }
        }

        private void txtItemCdr_Leave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemcdr = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemcdr = row.Controls.Find("txtItemCdr", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID="+ itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemcdr.Text != null) eqp.Attribute("ITEMCDR").Value = itemcdr.Text;
                }
            }
        }

        private void txtItemCdr_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemcdr = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemcdr = row.Controls.Find("txtItemCdr", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemcdr.Text != null) eqp.Attribute("ITEMCDR").Value = itemcdr.Text;
                }
            }
        }

        private void txtItemExp_Leave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemexp = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemexp = row.Controls.Find("txtItemExp", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemexp.Text != null) eqp.Attribute("ITEMEXP").Value = itemexp.Text;
                }
            }
        }

        private void txtItemExp_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemexp = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemexp = row.Controls.Find("txtItemExp", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemexp.Text != null) eqp.Attribute("ITEMEXP").Value = itemexp.Text;
                }
            }
        }

        private void txtItemKills_Leave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemkills = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemkills = row.Controls.Find("txtItemKills", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemkills.Text != null) eqp.Attribute("ITEMKILLS").Value = itemkills.Text;
                }
            }
        }

        private void txtItemKills_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemkills = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemkills = row.Controls.Find("txtItemKills", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemkills.Text != null) eqp.Attribute("ITEMKILLS").Value = itemkills.Text;
                }
            }
        }

        private void cboCasualty_SelectedIndexChanged(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control casualty = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    casualty = row.Controls.Find("cboCasualty", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (casualty.Text != null) eqp.Attribute("CASUALTY").Value = casualty.Text;
                }
            }
        }

        private void txtEquipDamage_Leave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemdamage = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemdamage = row.Controls.Find("txtItemDamage", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemdamage.Text != null) eqp.Attribute("ITEMDAMAGE").Value = itemdamage.Text;
                }
            }
        }

        private void txtItemDamage_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemdamage = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemdamage = row.Controls.Find("txtItemDamage", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemdamage.Text != null) eqp.Attribute("ITEMDAMAGE").Value = itemdamage.Text;
                }
            }
        }

        private void txtEquipNote_Leave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemnote = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemnote = row.Controls.Find("txtItemNote", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemnote.Text != null) eqp.Attribute("ITEMNOTE").Value = itemnote.Text;
                }
            }
        }

        private void txtItemNote_MouseLeave(object sender, EventArgs e)
        {
            int drIndex = drUnit.CurrentItemIndex;
            string unitID = trvUnitTree.SelectedNode.Tag.ToString();
            string eqpID = "";
            string itemID = "";
            Control itemnote = null;
            Control eqpid = null;
            Control itemid = null;

            foreach (DataRepeaterItem row in drUnit.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    eqpid = row.Controls.Find("lblEquipID", true).First();
                    itemid = row.Controls.Find("txtItemID", true).First();
                    itemnote = row.Controls.Find("txtItemNote", true).First();
                    eqpID = eqpid.Text;
                    itemID = itemid.Text;

                    ////CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]/EQUIPMENT[@ID=" + eqpID + "]/ITEM[@ID=" + itemID + "]";
                    var eqp = tacFile.XPathSelectElement(xpath);
                    if (itemnote.Text != null) eqp.Attribute("ITEMNOTE").Value = itemnote.Text;
                }
            }
        }

        private void  DeleteUnit(XElement gamfile, string forceid, string unitid)
        {
            string xpath = "OOB/FORCE[@ID=" + forceid + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = gamfile.XPathSelectElements(xpath);
            //SHOULD REVISE ANY EVENTS WITH THIS UNIT TO NO EFFECT
            unit.Remove();

            ////////////////////JULY 19
            var unitevents = (from u in gamfile.Elements("EVENTS").Elements("EVENT")  //ALL EVENTS FEATURING UNITS
                              where (string)u.Attribute("TRIGGER") == "Unit destroyed" ||
                              (string)u.Attribute("EFFECT") == "Disband unit" ||
                              (string)u.Attribute("EFFECT") == "Withdraw unit" ||
                              (string)u.Attribute("EFFECT") == "Withdraw army" &&
                              (string)u.Attribute("VALUE") == ((Convert.ToInt32(u.Attribute("VALUE").Value) + 1).ToString())
                              select u).FirstOrDefault();

            unitevents.Attribute("TRIGGER").Value = "No trigger";
            unitevents.Attribute("EFFECT").Value = "No effect";
            unitevents.Attribute("VALUE").Value = "0";
            unitevents.Attribute("NEWS").Value = "";
            ///////////////JULY 19
        }

        private void btnSyncGamTac_Click(object sender, EventArgs e)
        {
            string dateformat = "dd MMM yyyy";
            string datetime = DateTimePicker.Value.ToString(dateformat);
            frmSyncGamTac gamsync = new frmSyncGamTac(datetime);
            gamsync.Show();
        }

        private void btnPostBattle_Click(object sender, EventArgs e)
        {
            //OPEN SELECTED TACFILE
            string TacFileName = txtTacFile.Text;
            string unitname;
            string xpathTac;
            string xpathGam;

            XElement tacFile = XElement.Load(TacFileName);
            XElement gamFile;

            //SELECT GAM FILE TO BE MODIFIED WITH TAC FILE RESULTS
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "*.gam files *.gam|*.gam";
            file.Title = "Select gam file to be modified to reflect tac file results";

            if (file.ShowDialog() == DialogResult.OK)
            {
                TOAWTac.Properties.Settings.Default.TacFilePath = file.FileName;
                //TOAWTac.Properties.Settings.Default.GamFilePath = file.FileName;
                TOAWTac.Properties.Settings.Default.Save();

                TOAWTac.Properties.Settings.Default.FilePath = file.FileName;
                TOAWTac.Properties.Settings.Default.Save();

                txtGamFile.Text = file.FileName;
            }
            else
            {
                TOAWTac.Properties.Settings.Default.TacFilePath = "";
                return;
            }
            ////
            
            //SET FORCE ID
            if (rbForce1.Checked == true)
            {
                forceID = "1";
            }
            if (rbForce2.Checked == true)
            {
                forceID = "2";
            }

            //LOAD GAM FILE
            gamFile = XElement.Load(file.FileName);

            //GET SELECTED UNITS FROM TREE
            var checkedlist = new List<TreeNode>();
            GetCheckedNodes(trvUnitTree.Nodes, checkedlist);

            if (checkedlist.Count == 0)
            {
                MessageBox.Show("No units have been selected.",
                   "No Units Selected",
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button1);
                return;
            }

            foreach (var selectedunit in checkedlist)
            {
                int u = 0; //number of equipment items in unit
                unitname = selectedunit.Text;
                xpathTac = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@NAME ='" + unitname + "']";

                XElement tacunit = tacFile.XPathSelectElement(xpathTac);

                string unitid = tacunit.Attribute("ID").Value;
                int eqpID = 1;

                foreach (XElement equipment in tacunit.Descendants("EQUIPMENT"))
                {
                    float f = 0;  ///number of items in equipment
                   
                    foreach (XElement item in equipment.Descendants("ITEM"))
                    {
                        if (item.Attribute("CASUALTY").Value == "None")
                        {
                            f++;
                            u++;
                        }
                        if (item.Attribute("CASUALTY").Value == "Half")
                        {
                            f = f + 0.5f;
                        }
                    }//item

                    int i = (int)Math.Round(f, MidpointRounding.AwayFromZero);

                    //INPUT REVISED EQP NUMBER INTO GAMFILE
                    string equipID = eqpID.ToString();
                    xpathGam = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@NAME ='" + unitname + "']/EQUIPMENT[@ID =  " + equipID + "]";
                    XElement gamEquip = gamFile.XPathSelectElement(xpathGam);
                    gamEquip.Attribute("NUMBER").Value = i.ToString();
                    eqpID++;
                }//equipment
            }
            gamFile.Save(file.FileName);
        }
    }
}