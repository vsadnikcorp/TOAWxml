using System;
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
        }
        private void frmTacFile_Load(object sender, EventArgs e)
        {
            //SET GAME DATE
            DateTimePicker.Format = DateTimePickerFormat.Custom;
            DateTimePicker.CustomFormat = "dd-MMM-yyyy";

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

            //CREATE DATATABLE FOR FORMATIONS
            dtFormation.Columns.Add("Name", typeof(string));
            dtFormation.Columns.Add("Prof", typeof(string));
            dtFormation.Columns.Add("Supply", typeof(string));
            dtFormation.Columns.Add("Support", typeof(string));
            dtFormation.Columns.Add("Orders", typeof(string));
            dtFormation.Columns.Add("LossTol", typeof(string));
            dtFormation.Columns.Add("FormID", typeof(string));

            //CREATE DATA TABLE FOR UNITS
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

            //CREATE DATATABLE FOR EQUIP
            dtEquip.Columns.Add("EquipName", typeof(string));
            dtEquip.Columns.Add("Qty", typeof(string));
            dtEquip.Columns.Add("Max", typeof(string));
            dtEquip.Columns.Add("Dam", typeof(string));

            txtName.Visible = false;
            txtProf.Visible = false;
            txtSupply.Visible = false;
            cboSupport.Visible = false;
            cboOrders.Visible = false;
            cboLossTol.Visible = false;

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
            frmLoadGamFile loadfileform = new frmLoadGamFile();
            loadfileform.ShowDialog();
            string FilePath = TOAWTac.Properties.Settings.Default.FilePath.ToString();
            string dateformat = "dd MMM yyyy";
            string date = DateTimePicker.Value.ToString(dateformat);

            trvUnitTree.Nodes.Clear();
            dtFormation.Clear();
            dtUnit.Clear();

            rbForce1.Checked = false;
            rbForce2.Checked = false;

            if (FilePath != "" && FilePath != null)  //THERE IS IS NO "ELSE" TO COVER WHAT IF FILEPATH == "" OR NULL!!
            {
                ssTac.Visible = true;

                //CREATE TACFILE
                //QQQ XDocument xdoc = XDocument.Load(FilePath);
                tacFile = XElement.Load(FilePath);
                string TacFilePath = FilePath.Substring(0, FilePath.Length - 3) + "tac";
                txtTacFile.Text = TacFilePath;
                TOAWTac.Properties.Settings.Default.TacFilePath = TacFilePath;
                TOAWTac.Properties.Settings.Default.Save();

                //CREATE TACFILE

                //SSSSSSSSSSSSSSSS
                //XDocument tacFile = new XDocument();
                //tacFile = new XDocument(new XElement("GAME", new XElement("OOB")));
                //SSSSSSSSSSSSSSSS

                //ZZZZZZZZZZZZZZZZZZ
                //XDocument tacFile = xdoc;
                //QQQ tacFile = xdoc;
                //ZZZZZZZZZZZZZZZZZ

                //LOAD COMMANDER NAMES FILE
                string CdrNameDirectory = Path.GetDirectoryName(TacFilePath);
                string CdrNameFilePath = CdrNameDirectory + "\\CDRNAMES.XML";
                XDocument xdocCDR = XDocument.Load(CdrNameFilePath);
                Random rng = new Random();

                //RUN ASYNC TO FILL TACFILE
                await Task.Run(() =>
                {
                    //SSSSSSSSSSSSSSSSSSSSSSSSSSSSS
                    ////ADD HEADER DATA TO TACFILE
                    //var gamenode = tacFile.Descendants("GAME").FirstOrDefault();

                    //XElement header = new XElement("HEADER",
                    //new XAttribute("version", xdoc.Descendants("HEADER").First().Attribute("version").Value),
                    //new XAttribute("fileType", xdoc.Descendants("HEADER").First().Attribute("fileType").Value),
                    //new XAttribute("firstPlayer", xdoc.Descendants("HEADER").First().Attribute("firstPlayer").Value),
                    //new XAttribute("name", xdoc.Descendants("HEADER").First().Attribute("name").Value),
                    //new XAttribute("forceName1", xdoc.Descendants("HEADER").First().Attribute("forceName1").Value),
                    //new XAttribute("forceName2", xdoc.Descendants("HEADER").First().Attribute("forceName2").Value));

                    //gamenode.AddFirst(header);
                    //SSSSSSSSSSSSSSSSSSSSSSS

                    //ADD FORCES TO TACFILE
                    //SSSSSSSSSSSS
                    //foreach (XElement force in xdoc.Descendants("OOB").Descendants("FORCE"))

                    foreach (XElement force in tacFile.Descendants("OOB").Descendants("FORCE"))
                    //SSSSSSSSSSSSSSSSSSSSSSSSSSS
                    {
                        string forceID = force.Attribute("ID").Value;
                        string forcecdrname = AssignCdrName(xdocCDR, forceID, rng);

                        //SSSSSSSSSSSSSSSSSS
                        //tacFile.Descendants("OOB").FirstOrDefault().Add(new XElement("FORCE",
                        //                                            new XAttribute("ID", force.Attribute("ID").Value),
                        //                                            new XAttribute("NAME", force.Attribute("NAME").Value),
                        //                                            new XAttribute("CDR", forcecdrname)));

                        force.Add(new XAttribute("CDR", forcecdrname));
                        //SSSSSSSSSSSSSSSSSS

                        //LIST FORMATIONS
                        foreach (XElement formation in force.Descendants("FORMATION").Where(f => f.Parent.Attribute("ID").Value == forceID))
                        {
                            //ADD FORMATIONS TO TACFILE
                            string formcdrname = AssignCdrName(xdocCDR, forceID, rng);

                            //SSSSSSSSSSSSSSSSSSS
                            //tacFile.Descendants("OOB").Descendants("FORCE")
                            //    .Where(g => g.Attribute("ID").Value == forceID).FirstOrDefault()
                            //    .Add(
                            //        new XElement("FORMATION",
                            //        new XAttribute("ID", formation.Attribute("ID").Value),
                            //        new XAttribute("NAME", formation.Attribute("NAME").Value),
                            //        new XAttribute("CDR", formcdrname),
                            //        new XAttribute("RANK", "CPT"),
                            //        new XAttribute("FORMDATE", date)));

                            formation.Add(
                                new XAttribute("CDR", formcdrname),
                                new XAttribute("RANK", "CPT"),
                                new XAttribute("FORMDATE", date));
                            //SSSSSSSSSSSSSSSSSSSSSSSS

                            string formID = formation.Attribute("ID").Value;

                            //LIST UNITS
                            foreach (XElement unit in formation.Descendants("UNIT")
                                .Where(u => u.Parent.Attribute("ID").Value == formID)
                                .Where(u => u.Parent.Parent.Attribute("ID").Value == forceID))
                            {
                                string unitcdrname = AssignCdrName(xdocCDR, forceID, rng);

                                //ADD UNITS TO TACFILE
                                //SSSSSSSSSSSSSSSSSSSSSSSS
                                //tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION")
                                //    .Where(g => g.Attribute("ID").Value == formID)
                                //    .Where(g => g.Parent.Attribute("ID").Value == forceID).FirstOrDefault()
                                //    .Add(
                                //        new XElement("UNIT",
                                //        new XAttribute("ID", unit.Attribute("ID").Value),
                                //        new XAttribute("NAME", unit.Attribute("NAME").Value),
                                //        new XAttribute("CDR", unitcdrname),
                                //        new XAttribute("RANK", "LT"),
                                //        new XAttribute("FORMDATE", date)));

                                unit.Add(
                                     new XAttribute("CDR", unitcdrname),
                                     new XAttribute("RANK", "LT"),
                                     new XAttribute("FORMDATE", date));
                                //SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS

                                string unitID = unit.Attribute("ID").Value;

                                //EQUIP NOT NECESSARY FOR TAC FILE?
                                //LIST EACH PIECE OF EQUP
                                //foreach (XElement equip in unit.Descendants("EQUIPMENT")
                                //        .Where(q => q.Parent.Attribute("ID").Value == unitID)
                                //        .Where(q => q.Parent.Parent.Parent.Attribute("ID").Value == forceID))
                                //{
                                //    int EqpQty = Convert.ToInt16(equip.Attribute("NUMBER").Value);

                                //    for (int i = 1; i <= EqpQty; i++)
                                //    {
                                //        //ADD EQUIP TO TACFILE
                                //        tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT")
                                //            .Where(g => g.Attribute("ID").Value == unitID)
                                //            .Where(g => g.Parent.Parent.Attribute("ID").Value == forceID).FirstOrDefault()
                                //            .Add(
                                //                new XElement("EQUIPMENT",
                                //                new XAttribute("ITEM", i.ToString()),
                                //                new XAttribute("ID", equip.Attribute("ID").Value),
                                //                new XAttribute("NAME", equip.Attribute("NAME").Value)));
                                //    }
                                //}
                            }
                        }
                    }
                });

                txtTacFile.Text = TacFilePath;

                //ENABLE FORCE RADIO BUTTONS, SET FORCE NAMES
                rbForce1.Enabled = true;
                rbForce2.Enabled = true;
                //SSSSSSSSSSSSSSSSS
                //rbForce1.Text = xdoc.Descendants("HEADER").First().Attribute("forceName1").Value;
                //rbForce2.Text = xdoc.Descendants("HEADER").First().Attribute("forceName2").Value;

                rbForce1.Text = tacFile.Descendants("HEADER").First().Attribute("forceName1").Value;
                rbForce2.Text = tacFile.Descendants("HEADER").First().Attribute("forceName2").Value;
                //SSSSSSSSSSSSSSSSSSSSSSSSS
                tacFile.Save(TacFilePath);
                ssTac.Visible = false;
            }
            //  WHAT IF == "" OR NULL??
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoadTacFile_Click(object sender, EventArgs e)
        {
            frmLoadTacFile loadtacfile = new frmLoadTacFile();
            loadtacfile.ShowDialog();

            //SET FILE PATH STRINGS, SAVE TO SETTINGS
            string TacFilePath = TOAWTac.Properties.Settings.Default.TacFilePath.ToString();

            //LOAD TAC FILE
            if (TacFilePath != "" && TacFilePath != null)
            {
                string FilePath = TacFilePath.Substring(0, TacFilePath.Length - 3) + "gam";
                TOAWTac.Properties.Settings.Default.FilePath = FilePath;
                TOAWTac.Properties.Settings.Default.Save();

                tacFile = XElement.Load(TacFilePath);
                txtTacFile.Text = TacFilePath;

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
            }

            rbForce1.Checked = false;
            rbForce2.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tacFile.Save(txtTacFile.Text);
            trvUnitTree.Focus();
        }

        private string AssignCdrName(XDocument xdoc, string forceID, Random rng)
        {
            var commanders = xdoc.Descendants("CDRNAMES").Descendants("CDRNAME")
                .Where(f => f.Attribute("forceid").Value == forceID);

            int cdrnameCount = commanders.Count();
            string cdrname = commanders.ElementAt(rng.Next(0, cdrnameCount)).Attribute("cdrname").Value;

            return cdrname;
        }

        private void rbForce1_CheckedChanged(object sender, EventArgs e)
        {
            ssTac.Visible = true;
            pnlFormation.Visible = false;
            pnlUnit.Visible = false;
            pnlForce.Visible = true;

            if (rbForce1.Checked == true)
            {
                forceID = "1";
                //LoadTree("1");
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
                //LoadTree("2");
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

                        ////>>>>>>>>>>>>>>>JAN 30 2021
                        //dt.Rows.Add(formation.Attribute("NAME").Value, formation.Attribute("PROFICIENCY").Value, formation.Attribute("SUPPLY").Value,
                        //    formation.Attribute("EMPHASIS").Value, formation.Attribute("SUPPORTSCOPE").Value);
                        ////<<<<<<<<<<<<<

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

            //>>>>>>>>>>>>>>
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
            var list = new List<TreeNode>();
            GetCheckedNodes(trvUnitTree.Nodes, list);
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
                    pnlForce.Size = new Size(944, 46);
                    pnlFormation.Visible = false;
                    pnlUnit.Visible = false;

                    txtName.Visible = true;
                    txtProf.Visible = true;
                    txtSupply.Visible = true;
                    cboSupport.Visible = true;
                    cboOrders.Visible = true;
                    cboLossTol.Visible = true;

                    //CLEAR CONTROL DATABINDINGS
                    txtName.DataBindings.Clear();
                    txtProf.DataBindings.Clear();
                    txtSupply.DataBindings.Clear();
                    cboSupport.DataBindings.Clear();
                    cboOrders.DataBindings.Clear();
                    cboLossTol.DataBindings.Clear();
                    lblFormID.DataBindings.Clear();

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
                    

                    //SET DATA BINDINGS
                    txtName.DataBindings.Add("Text", dtFormation, "Name");
                    txtProf.DataBindings.Add("Text", dtFormation, "Prof");
                    txtSupply.DataBindings.Add("Text", dtFormation, "Supply");
                    cboSupport.DataBindings.Add("Text", dtFormation, "Support");
                    cboOrders.DataBindings.Add("Text", dtFormation, "Orders");
                    cboLossTol.DataBindings.Add("Text", dtFormation, "LossTol");
                    lblFormID.DataBindings.Add("Text", dtFormation, "FormID");

                    foreach (XElement formation in force.Descendants("FORMATION").Where(f => f.Parent.Attribute("ID").Value == forceID))
                    {
                        dtFormation.Rows.Add(formation.Attribute("NAME").Value, formation.Attribute("PROFICIENCY").Value,
                            formation.Attribute("SUPPLY").Value, formation.Attribute("SUPPORTSCOPE").Value,
                            formation.Attribute("ORDERS").Value, formation.Attribute("EMPHASIS").Value, formation.Attribute("ID").Value);
                    }

                    drForce.DataSource = dtFormation;

                    break;

                case 1: //IF FORMATION SELECTED

                    drForce.Visible = false;
                    drFormation.Visible = true;
                    drFormation.Location = new Point(216, 84);
                    drFormation.Size = new Size(951, 500);
                    drUnit.Visible = false;

                    pnlForce.Visible = false;
                    pnlFormation.Visible = true;
                    pnlFormation.Location = new Point(221, 33);
                    pnlFormation.Size = new Size(944, 46);
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

                    xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID =" + formid + "]";
                    var units = tacFile.XPathSelectElements(xpath);

                    //SET HEADER DATA
                    txtHdrFormName.Text = units.First().Attribute("NAME").Value;
                    txtHdrFormProf.Text = units.First().Attribute("PROFICIENCY").Value;
                    txtHdrFormSupply.Text = units.First().Attribute("SUPPLY").Value;
                    cboHdrFormSupport.Text = units.First().Attribute("SUPPORTSCOPE").Value;
                    cboHdrFormOrders.Text = units.First().Attribute("ORDERS").Value;
                    cboHdrFormLossTol.Text = units.First().Attribute("EMPHASIS").Value;

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

                    drFormation.DataSource = dtUnit;

                    foreach (XElement unit in units.Descendants("UNIT"))
                    {
                        string replacePriority = GetReplacementPriorityText(unit.Attribute("REPLACEMENTPRIORITY").Value);

                        string iconID = "";
                        string orderID = unit.Attribute("STATUS").Value;
                        string icon = unit.Attribute("ICON").Value;
                        bool isAirUnit = IsAirUnit(icon);

                        if (unit.Attribute("ICONID") != null) iconID = unit.Attribute("ICONID").Value;
                        string iconDisplay = GetIconText(iconID, icon);

                        string unitorders = SetUnitOrders(isAirUnit, orderID);

                        dtUnit.Rows.Add(unit.Attribute("NAME").Value, unit.Attribute("PROFICIENCY").Value,
                            unit.Attribute("SUPPLY").Value, unitorders, unit.Attribute("EMPHASIS").Value,
                            unit.Attribute("READINESS").Value, iconDisplay, unit.Attribute("SIZE").Value,
                            unit.Attribute("EXPERIENCE").Value, replacePriority, unit.Attribute("ID").Value);
                    }

                    drFormation.DataSource = dtUnit;
                    break;

                case 2:  //IF UNIT SELECTED
                    drForce.Visible = false;
                    drFormation.Visible = false;
                    drUnit.Location = new Point(216, 84);
                    drUnit.Size = new Size(951, 500);
                    drUnit.Visible = true;

                    //SET HEADER PANEL VISIBILITY
                    pnlForce.Visible = false;
                    pnlFormation.Visible = false;
                    pnlUnit.Visible = true;
                    pnlUnit.Location = new Point(221, 33);
                    pnlUnit.Size = new Size(944, 46);

                    dtEquip.Clear();

                    xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                    XElement equipment = tacFile.XPathSelectElement(xpath);

                    //SET HEADER PANEL DATA
                    txtHdrUnitName.Text = equipment.Attribute("NAME").Value;
                    txtHdrUnitProf.Text = equipment.Attribute("PROFICIENCY").Value;
                    txtHdrUnitSupply.Text = equipment.Attribute("SUPPLY").Value;

                    txtEquipName.DataBindings.Clear();

                    txtEquipName.DataBindings.Add("Text", dtEquip, "EquipName");

                    drUnit.DataSource = dtEquip;

                    foreach (XElement equip in equipment.Descendants("EQUIPMENT"))
                    {
                        int qty = Int32.Parse(equip.Attribute("NUMBER").Value);
                        for (int i = 1; i <= qty; i++)
                        {
                            dtEquip.Rows.Add(equip.Attribute("NAME").Value);
                            //dtEquip.Rows.Add(equip.Attribute("NAME").Value, equip.Attribute("NUMBER").Value, 
                            //equip.Attribute("MAX").Value, equip.Attribute("DAMAGE").Value);
                        }
                    }

                    drUnit.DataSource = dtEquip;

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

                    break;

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
                //Console.WriteLine(iconDisplay);
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

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION[@ID= " + formID + "]";
                    var formation = tacFile.XPathSelectElement(xpath);
                    if (name.Text != null) formation.Attribute("NAME").Value = name.Text;

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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show(desc + " must be between " + min + " and " + max + ".", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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

            foreach (DataRepeaterItem row in drFormation.Controls)
            {
                if (row.ItemIndex == drIndex)
                {
                    Control label = row.Controls.Find("lblUnitID", true).First();
                    Control name = row.Controls.Find("txtUnitName", true).First();
                    unitID = label.Text;

                    //CHANGE TACFILE XML
                    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT[@ID= " + unitID + "]";
                    var unit = tacFile.XPathSelectElement(xpath);
                    if (name.Text != null) unit.Attribute("NAME").Value = name.Text;

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
                           //Console.WriteLine("key: " + icon.Key.ToString());

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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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
                        MessageBox.Show("Please enter number between 1-100!", "Entry Error");
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

                    Console.WriteLine("text: " + replace.Text + " id: " + replaceID);
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
    }
}

