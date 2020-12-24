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

namespace TOAWXML
{
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
        //$$$$$$$$$$$$$$$$$$$$$$$$

        public frmTacFile()
        {
            InitializeComponent();
        }

        private void frmTacFile_Load(object sender, EventArgs e)
        {
            //SET GAME DATE
            DateTimePicker.Format = DateTimePickerFormat.Custom;
            DateTimePicker.CustomFormat = "dd-MMM-yyyy";

            //DISABLE FORCE RADIO BUTTONS
            rbForce1.Enabled = false;
            rbForce2.Enabled = false;

            //CHECK THAT TAM's GAM FILE IS PRESENT
            if (System.IO.File.Exists("FilePath.txt"))
            {
                string filePath = File.ReadAllText("FilePath.txt");

                //Globals.GlobalVariables.PATH = System.IO.Path.Combine(filePath);
                //txtPath.Text = filePath;

                //if (!System.IO.File.Exists(Globals.GlobalVariables.PATH))
                //{
                //    frmMissingFile loadfileform = new frmMissingFile();
                //    loadfileform.ShowDialog();
                //    return;
                //}
                //FixInvalidXML();
                //FixForce2SubunitBug();

                //XDocument xdoc = XDocument.Load(Globals.GlobalVariables.PATH);

                //GET NAME OF FORCE 1 AND ASSIGN TO radio button text
                //var forcenames = xdoc.Descendants("HEADER");
                //foreach (var f in forcenames)
                //{
                //    string fn1 = f.Attribute("forceName1").Value.ToString();
                //    this.rbForce1.Text = fn1;

                //    string fn2 = f.Attribute("forceName2").Value.ToString();
                //    this.rbForce2.Text = fn2;
                //}
            }
        }

        private async void btnCreateTacFile_Click(object sender, EventArgs e)
        {
            frmLoadGamFile loadfileform = new frmLoadGamFile();
            loadfileform.ShowDialog();
            string FilePath = TOAWTac.Properties.Settings.Default.FilePath.ToString();
            string dateformat = "dd MMM yyyy";
            string date = DateTimePicker.Value.ToString(dateformat);

            if (FilePath != "" && FilePath != null)
            {
                ssTac.Visible = true;

                //CREATE TACFILE
                XDocument xdoc = XDocument.Load(FilePath);
                string TacFilePath = FilePath.Substring(0, FilePath.Length - 3) + "tam";
                txtTacFile.Text = TacFilePath;
                TOAWTac.Properties.Settings.Default.TacFilePath = TacFilePath;
                TOAWTac.Properties.Settings.Default.Save();

                XDocument tacFile = new XDocument();
                tacFile = new XDocument(new XElement("GAME", new XElement("OOB")));

                //LOAD COMMANDER NAMES FILE
                string CdrNameDirectory = Path.GetDirectoryName(TacFilePath);
                string CdrNameFilePath = CdrNameDirectory + "\\CDRNAMES.XML";
                XDocument xdocCDR = XDocument.Load(CdrNameFilePath);
                Random rng = new Random();

                //RUN ASYNC TO CREATE TAC FILE
                await Task.Run(() => 
                {
                    //ADD HEADER DATA TO TACFILE
                    var gamenode = tacFile.Descendants("GAME").FirstOrDefault();

                    XElement header = new XElement("HEADER",
                    new XAttribute("version", xdoc.Descendants("HEADER").First().Attribute("version").Value),
                    new XAttribute("fileType", xdoc.Descendants("HEADER").First().Attribute("fileType").Value),
                    new XAttribute("firstPlayer", xdoc.Descendants("HEADER").First().Attribute("firstPlayer").Value),
                    new XAttribute("name", xdoc.Descendants("HEADER").First().Attribute("name").Value),
                    new XAttribute("forceName1", xdoc.Descendants("HEADER").First().Attribute("forceName1").Value),
                    new XAttribute("forceName2", xdoc.Descendants("HEADER").First().Attribute("forceName2").Value));

                    gamenode.AddFirst(header);

                    //rbForce1.Text = xdoc.Descendants("HEADER").First().Attribute("forceName1").Value;
                    //rbForce2.Text = xdoc.Descendants("HEADER").First().Attribute("forceName2").Value;

                    //ADD FORCES TO TACFILE
                    foreach (XElement force in xdoc.Descendants("OOB").Descendants("FORCE"))
                    {
                        string forceID = force.Attribute("ID").Value;
                        string forcecdrname = AssignCdrName(xdocCDR, forceID, rng);

                        tacFile.Descendants("OOB").FirstOrDefault().Add(new XElement("FORCE",
                                                                    new XAttribute("ID", force.Attribute("ID").Value),
                                                                    new XAttribute("NAME", force.Attribute("NAME").Value),
                                                                    new XAttribute("CDR", forcecdrname)));

                        //LIST FORMATIONS
                        foreach (XElement formation in force.Descendants("FORMATION").Where(f => f.Parent.Attribute("ID").Value == forceID))
                        {
                            //ADD FORMATIONS TO TACFILE
                            string formcdrname = AssignCdrName(xdocCDR, forceID, rng);

                            tacFile.Descendants("OOB").Descendants("FORCE")
                                .Where(g => g.Attribute("ID").Value == forceID).FirstOrDefault()
                                .Add(
                                    new XElement("FORMATION",
                                    new XAttribute("ID", formation.Attribute("ID").Value),
                                    new XAttribute("NAME", formation.Attribute("NAME").Value),
                                    new XAttribute("CDR", formcdrname),
                                    new XAttribute("FORMDATE", date)));

                            string formID = formation.Attribute("ID").Value;

                            //LIST UNITS
                            foreach (XElement unit in formation.Descendants("UNIT")
                                .Where(u => u.Parent.Attribute("ID").Value == formID)
                                .Where(u => u.Parent.Parent.Attribute("ID").Value == forceID))
                            {
                                string unitcdrname = AssignCdrName(xdocCDR, forceID, rng);

                                //ADD UNITS TO TACFILE
                                tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION")
                                    .Where(g => g.Attribute("ID").Value == formID)
                                    .Where(g => g.Parent.Attribute("ID").Value == forceID).FirstOrDefault()
                                    .Add(
                                        new XElement("UNIT",
                                        new XAttribute("ID", unit.Attribute("ID").Value),
                                        new XAttribute("NAME", unit.Attribute("NAME").Value),
                                        new XAttribute("CDR", unitcdrname),
                                        new XAttribute("FORMDATE", date)));

                                string unitID = unit.Attribute("ID").Value;

                                //LIST EACH PIECE OF EQUP
                                foreach (XElement equip in unit.Descendants("EQUIPMENT")
                                        .Where(q => q.Parent.Attribute("ID").Value == unitID)
                                        .Where(q => q.Parent.Parent.Parent.Attribute("ID").Value == forceID))
                                {
                                    int EqpQty = Convert.ToInt16(equip.Attribute("NUMBER").Value);

                                    for (int i = 1; i <= EqpQty; i++)
                                    {
                                        //ADD EQUIP TO TACFILE
                                        tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT")
                                            .Where(g => g.Attribute("ID").Value == unitID)
                                            .Where(g => g.Parent.Parent.Attribute("ID").Value == forceID).FirstOrDefault()
                                            .Add(
                                                new XElement("EQUIPMENT",
                                                new XAttribute("ITEM", i.ToString()),
                                                new XAttribute("ID", equip.Attribute("ID").Value),
                                                new XAttribute("NAME", equip.Attribute("NAME").Value)));
                                    }
                                }
                            }
                        }
                    }
                });
                
                //tacFile.Save(TacFilePath);
                txtTacFile.Text = TacFilePath;

                //ENABLE FORCE RADIO BUTTONS, SET FORCE NAMES
                rbForce1.Enabled = true;
                rbForce2.Enabled = true;
                rbForce1.Text = xdoc.Descendants("HEADER").First().Attribute("forceName1").Value;
                rbForce2.Text = xdoc.Descendants("HEADER").First().Attribute("forceName2").Value;

                //Console.WriteLine(tacFile.ToString());

                tacFile.Save(TacFilePath);
                ssTac.Visible = false;
                rbForce1.Select();
            }
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
            //string FilePath = TacFilePath.Substring(0, TacFilePath.Length - 3) + "gam";
            //TOAWTac.Properties.Settings.Default.FilePath = FilePath;
            //TOAWTac.Properties.Settings.Default.Save();

            //LOAD TAM FILE
            if (TacFilePath != "" && TacFilePath != null)
            {
                string FilePath = TacFilePath.Substring(0, TacFilePath.Length - 3) + "gam";
                TOAWTac.Properties.Settings.Default.FilePath = FilePath;
                TOAWTac.Properties.Settings.Default.Save();

                XDocument tacFile = XDocument.Load(TacFilePath);
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
            rbForce1.Select();
        }

        private string AssignCdrName(XDocument xdoc, string forceID, Random rng)
        {   
            var commanders = xdoc.Descendants("CDRNAMES").Descendants("CDRNAME")
                .Where(f => f.Attribute("forceid").Value ==  forceID);
            
            int cdrnameCount = commanders.Count();
            string cdrname = commanders.ElementAt(rng.Next(0, cdrnameCount)).Attribute("cdrname").Value;

            return cdrname;
        }

        private void rbForce1_CheckedChanged(object sender, EventArgs e)
        {
            ssTac.Visible = true;
            string forceID = "1";
            LoadTree(forceID);
            //await Task.Delay(500);
            ssTac.Visible = false;
        }

        private void rbForce2_CheckedChanged(object sender, EventArgs e)
        {
            ssTac.Visible = true;
            string forceID = "2";
            LoadTree(forceID);
            //await Task.Delay(500);
            ssTac.Visible = false;
        }

        private void LoadTree(string forceID)
        {
            ssTac.Visible = true;
            XElement xelem;

            if (txtTacFile.Text != "")
            { 
                string FilePath = TOAWTac.Properties.Settings.Default.FilePath.ToString();
                xelem = XElement.Load(FilePath);
             }
            else
            {
                return;
            }
            TreeNode forceNode;
            TreeNode formationNode;
            TreeNode unitNode;

            ////MAKE ALL CONTROLS INVISIBLE UNTIL AFTER TREE SELECT
            //lblUnitName.Visible = false;
            //txtUnitName.Visible = false;
            //lblID.Visible = false;
            //txtID.Visible = false;
            //lblType.Visible = false;
            //txtType.Visible = false;
            //lblSize.Visible = false;
            //cboSize.Visible = false;
            //lblProficiency.Visible = false;
            //txtProficiency.Visible = false;
            //lblSupply.Visible = false;
            //txtSupply.Visible = false;
            //lblSupportScope.Visible = false;
            //cboSupportScope.Visible = false;
            //lblOrders.Visible = false;
            //cboOrders.Visible = false;
            //lblEmphasis.Visible = false;
            //cboEmphasis.Visible = false;
            //lblReadiness.Visible = false;
            //txtReadiness.Visible = false;
            //lblReplacements.Visible = false;
            //cboReplacements.Visible = false;
            //lblExperience.Visible = false;
            //cboExperience.Visible = false;
            //cboDeployment.Visible = false;
            //lblDeployment.Visible = false;
            //lblNumber.Visible = false;
            //txtNumber.Visible = false;
            //lblMax.Visible = false;
            //txtMax.Visible = false;
            //lblDamage.Visible = false;
            //txtDamage.Visible = false;
            //txtEntryTurn.Visible = false;
            //lblEntryTurn.Visible = false;
            //tssLabel1.Text = "";
            ////lblEntryDate.Visible = false;

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
          
        }

        // Hides the checkbox for the specified node on TreeView.
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
                    Console.WriteLine("name: " + node.Text + " ID: " + node.Tag);
                }  

                GetCheckedNodes(node.Nodes, list);
            }
        }
    }














































































































































































































































































































































































































































































































}
