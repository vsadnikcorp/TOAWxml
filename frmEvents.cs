using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace TOAWXML
{
    public partial class frmEvents : Form
    {
        public frmEvents()
        {
            InitializeComponent();
        }
 
        public List<String> shorteffectslist = new List<String>()
            {
                "No effect",
                "Cease Fire",
                "Cool front",
                "End normal",
                "End victory 1",
                "End victory 2",
                "News only",
                "Open Fire",
                "Remove zone 1",
                "Remove zone 2",
                "Storms",
                "Use chemicals 1",
                "Use chemicals 2",
                "Warm front"
            };

        public List<String> longeffectslist = new List<String>()
            {
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
                "Form'n orders",
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
                "ZOC Cost 2"
            };

        private void frmEvents_Load(object sender, EventArgs e)
        {
            //POPULATES BIAS COMBO BOX
            var bias = new BindingList<KeyValuePair<string, string>>();
            bias.Add(new KeyValuePair<string, string>("0", "Very cautious"));
            bias.Add(new KeyValuePair<string, string>("1", "Cautious"));
            bias.Add(new KeyValuePair<string, string>("2", "Neutral"));
            bias.Add(new KeyValuePair<string, string>("3", "Aggressive"));
            bias.Add(new KeyValuePair<string, string>("4", "Beserk"));
            cboBias.DataSource = bias;
            cboBias.ValueMember = "Key";
            cboBias.DisplayMember = "Value";

            //POPULATES ORDERS COMBO BOX
            var orders = new BindingList<KeyValuePair<string, string>>();
            orders.Add(new KeyValuePair<string, string>("0", "Defend"));
            orders.Add(new KeyValuePair<string, string>("1", "Attack"));
            orders.Add(new KeyValuePair<string, string>("2", "Secure"));
            orders.Add(new KeyValuePair<string, string>("3", "Screen"));
            orders.Add(new KeyValuePair<string, string>("4", "Static"));
            orders.Add(new KeyValuePair<string, string>("5", "Wait"));
            orders.Add(new KeyValuePair<string, string>("6", "Hold"));
            orders.Add(new KeyValuePair<string, string>("7", "Delay"));
            orders.Add(new KeyValuePair<string, string>("8", "Independent"));
            orders.Add(new KeyValuePair<string, string>("9", "Manual"));
            orders.Add(new KeyValuePair<string, string>("10", "Advance"));
            orders.Add(new KeyValuePair<string, string>("11", "Garrison"));
            cboOrders.DataSource = orders;
            cboOrders.ValueMember = "Key";
            cboOrders.DisplayMember = "Value";

            //POPULATES EMPHASIS COMBO BOX
            var emphasis = new BindingList<KeyValuePair<string, string>>();
            emphasis.Add(new KeyValuePair<string, string>("0", "Minimize Losses"));
            emphasis.Add(new KeyValuePair<string, string>("1", "Limit Losses"));
            emphasis.Add(new KeyValuePair<string, string>("2", "Ignore Losses"));
            cboEmphasis.DataSource = emphasis;
            cboEmphasis.ValueMember = "Key";
            cboEmphasis.DisplayMember = "Value";

            //SET GUI
            cboTrigger.Enabled = false;
            cboEffect.Enabled = false;
            cboFiltTrigger.Enabled = true;
            cboFiltEffect.Enabled = true;
            cboFiltCausal.Enabled = false;
            txtCausalChain.Enabled = false;
            txtTriggerValue.Enabled = false;
            txtValue.Enabled = false;
            txtX.Enabled = false;
            txtY.Enabled = false;
            txtNews.Enabled = false;
            nudProb.Enabled = false;
            nudDelay.Enabled = false;
            nudTurnRange.Enabled = false;
            nupRadius.Enabled = false;
            btnSave.Enabled = false;
            cboTrigger.SelectedIndex = 0;
            List<string> listEmpty = new List<string> { "No effect" };
            cboEffect.DataSource = listEmpty;
            cboFiltTrigger.SelectedIndex = 0;
            cboFiltEffect.SelectedIndex = 0;
            cboFiltCausal.SelectedIndex = 0;
            txtCausalChain.Visible = false;
            btnUnitTrigger.Visible = false;
            btnUnitEffect.Visible = false;
            cboBias.Visible = false;
            gbFormation.Visible = false;
            ssEventsProgress.Visible = false;
            tssLabel1.Text = "";

            //TO BE ACTIVATED?
            gbCausal.Visible = false;
            cboFiltCausal.Visible = false;
            txtCausalChain.Visible = false;
            label18.Visible = false;

            //LOAD TREEVIEW
            trvEvents.BeginUpdate();
            trvEvents.Nodes.Clear();
            LoadEventTree();
            trvEvents.Focus();
            //trvEvents.TopNode.Expand();
            trvEvents.Nodes[0].Expand();
            //trvEvents.SelectedNode = trvEvents.TopNode;
            trvEvents.SelectedNode = trvEvents.Nodes[0];
            Globals.GlobalVariables.EVENTID = "0";
            trvEvents.EndUpdate();
        }

        private void rbExpand_Click(object sender, EventArgs e)
        {
            trvEvents.Focus();
            trvEvents.ExpandAll();

            string strEventID = Globals.GlobalVariables.EVENTID;

            foreach (TreeNode node in trvEvents.Nodes)
            {
                node.Expand();
                if (node.Tag.Equals(strEventID))
                {
                    trvEvents.SelectedNode = node;
                }
            }
            trvEvents.SelectedNode.EnsureVisible();
        }

        private void rbCollapse_Click(object sender, EventArgs e)
        {
            trvEvents.Focus();
            string strEventID = Globals.GlobalVariables.EVENTID;
            trvEvents.CollapseAll();
            trvEvents.Nodes[0].Expand();

            foreach (TreeNode node in trvEvents.Nodes[0].Nodes)
            {
                node.Collapse();
                if (node.Tag.Equals(strEventID))
                {
                    trvEvents.SelectedNode = node;
                }
            }

            trvEvents.SelectedNode.EnsureVisible();
        }

        private void trvEvents_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (trvEvents.SelectedNode.Level == 1)
            {
                cboTrigger.Enabled = true;
                cboEffect.Enabled = true;
                txtTriggerValue.Enabled = true;
                txtX.Enabled = true;
                txtY.Enabled = true;
                txtValue.Enabled = true;
                txtNews.Enabled = true;
                nudProb.Enabled = true;
                nudDelay.Enabled = true;
                nudTurnRange.Enabled = true;
                nupRadius.Enabled = true;
                tssLabel1.Text = "";

                Globals.GlobalVariables.EVENTID = trvEvents.SelectedNode.Tag.ToString();
                string strEventID = trvEvents.SelectedNode.Tag.ToString();
                if (trvEvents.Focused == true)
                {
                    txtID.Text = strEventID;

                    XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                    string xpath = "EVENTS/EVENT[@ID=" + Globals.GlobalVariables.EVENTID + "]";
                    var eventz = xelem.XPathSelectElement(xpath);
                    SetFullAttributes(eventz);

                    cboTrigger.Text = eventz.Attribute("TRIGGER").Value;
                    cboEffect.Text = eventz.Attribute("EFFECT").Value;

                    //PPPPPPPPPPPPP
                    //bool turnrange = true;

                    //if (eventz.Attribute("VARIABLE") == null) nudTurnRange.Value = 0;
                    
                    //if (eventz.Attribute("VARIABLE") == null)
                    //if (eventz.Attribute("VARIABLE") == null)
                    //{
                    //    turnrange = false;
                    //}
                    //Console.WriteLine(eventz.Attribute("VARIABLE").Value);
                    //Console.WriteLine(turnrange);

                    //XmlNode node = objTmplt.Attributes["name"];
                    //if (node != null)
                    //    styleName = node.Value;

                    IEnumerable<XAttribute> attList = eventz.Attributes("VARIABLE");
                    foreach (XAttribute att in attList)
                        Console.WriteLine(att);
                    //PPPPPPPPPPPPPPPPPPP

                    //SET CONTROLS BASED ON SELECTED TRIGGER
                    SetTriggerGroup(eventz);

                    //SET EFFECT GUI
                    SetEffectsGUI(cboEffect.Text, eventz);

                    if (eventz.Attribute("NEWS") != null)
                    {
                        txtNews.Text = eventz.Attribute("NEWS").Value;
                    }
                    else
                    {
                        txtNews.Text = "";
                    }
                }
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        public async void LoadEventTree()
        {
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            TreeNode eventsTNode;
            TreeNode eventTNode;
            TreeNode attribTNode;
            ssEventsProgress.Visible = true;

            foreach (XElement eventz1 in xelem.Descendants("EVENTS"))
            {
                eventsTNode = trvEvents.Nodes.Add("EVENTS");
                //eventsTNode = trvEvents.TopNode;
                eventsTNode = trvEvents.Nodes[0];
                eventsTNode.Tag = "0";

                foreach (XElement eventz2 in eventz1.Descendants("EVENT"))
                {
                    eventTNode = eventsTNode.Nodes.Add("Event " + eventz2.Attribute("ID").Value);
                    eventTNode.Tag = eventz2.Attribute("ID").Value;
                    eventTNode.Name = "EVENT";

                    foreach (XAttribute a in eventz2.Attributes())
                    {
                        attribTNode = eventTNode.Nodes.Add(a.Name.LocalName + ": " + a.Value);
                        Font f = new Font(trvEvents.Font, FontStyle.Regular);
                        attribTNode.NodeFont = f;
                    }
                }
            }

            await Task.Delay(500);
            ssEventsProgress.Visible = false;
        }

        public void SetTriggerBase(XElement eventz)
        {
            //SET PROBABILITY 
            int chance = Convert.ToInt32((eventz.Attribute("CHANCE").Value));
            int turnrange = Convert.ToInt32(eventz.Attribute("VARIABLE").Value);
            int delay = Convert.ToInt32(eventz.Attribute("TURN").Value);

            if (eventz.Attribute("CHANCE") != null && (chance >= nudProb.Minimum && chance <= nudProb.Maximum))
            {
                //nudProb.Value = int.Parse(eventz.Attribute("CHANCE").Value.ToString());
                nudProb.Value = Convert.ToInt32(eventz.Attribute("CHANCE").Value);
            }
            else nudProb.Value = 100;

            //SET TURN RANGE
            if (eventz.Attribute("VARIABLE") != null && (turnrange >= nudTurnRange.Minimum && turnrange <= nudTurnRange.Maximum))
            {
                //nudTurnRange.Value = (int.Parse(eventz.Attribute("VARIABLE").Value) + 1);
                nudTurnRange.Value = Convert.ToInt32(eventz.Attribute("VARIABLE").Value) + 1;
            }
            else nudTurnRange.Value = 1;

            //SET DELAY
            if (eventz.Attribute("TURN") != null && (delay >= nudDelay.Minimum && delay <= nudDelay.Maximum))
            {
                nudDelay.Value = Convert.ToInt32(eventz.Attribute("TURN").Value);
            }
            else nudDelay.Value = 0;
        }

        public void SetTriggerGroup(XElement eventz)
        {
            switch (cboTrigger.Text)
            {
                case "1 attacks":
                    SetTriggers1(eventz);
                    btnUnitTrigger.Visible = false;
                    lblTriggerValue.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "2 attacks":
                    SetTriggers1(eventz);
                    btnUnitTrigger.Visible = false;
                    lblTriggerValue.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "1 occupies":
                    SetTriggers1(eventz);
                    btnUnitTrigger.Visible = false;
                    lblTriggerValue.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "2 occupies":
                    SetTriggers1(eventz);
                    btnUnitTrigger.Visible = false;
                    lblTriggerValue.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "1 uses chemical":
                    SetTriggers4(eventz);
                    btnUnitTrigger.Visible = false;
                    lblTriggerValue.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "2 uses chemical":
                    SetTriggers4(eventz);
                    btnUnitTrigger.Visible = false;
                    lblTriggerValue.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "1 uses nuclear":
                    SetTriggers4(eventz);
                    btnUnitTrigger.Visible = false;
                    lblTriggerValue.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "2 uses nuclear":
                    SetTriggers4(eventz);
                    btnUnitTrigger.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "Event activated":
                    SetTriggers3(eventz);
                    lblTriggerValue.Text = "Event: ";
                    btnUnitTrigger.Visible = false;
                    lblTriggerValue.Visible = true;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "Event cancelled":
                    SetTriggers3(eventz);
                    lblTriggerValue.Text = "Event: ";
                    lblTriggerValue.Visible = true;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "Force 1 winning":
                    SetTriggers2(eventz);
                    lblTriggerValue.Text = "Value:";
                    lblTriggerValue.Visible = true;
                    btnUnitTrigger.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "Force 2 winning":
                    SetTriggers2(eventz);
                    lblTriggerValue.Text = "Value:";
                    lblTriggerValue.Visible = true;
                    btnUnitTrigger.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "Turn":
                    SetTriggers5(eventz);
                    lblTriggerValue.Text = "Turn:";
                    lblTriggerValue.Visible = true;
                    lblDelay.Visible = false;
                    nudDelay.Visible = false;
                    btnUnitTrigger.Visible = false;
                    //lblTriggerDate.Visible = true;
                    break;

                case "Unit destroyed":
                    SetTriggers2(eventz);
                    lblTriggerValue.Visible = false;
                    txtTriggerValue.Visible = false;
                    btnUnitTrigger.Visible = true;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    if (eventz.Attribute("TRIGGER").Value != "Unit destroyed")
                    {
                        btnUnitTrigger.Text = "Select unit";
                        //btnSave.Enabled = false;
                    }
                    break;

                case "Variable value":
                    SetTriggers2(eventz);
                    lblTriggerValue.Visible = true;
                    lblTriggerValue.Text = "Value:";
                    txtTriggerValue.Visible = true;
                    btnUnitTrigger.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    break;

                case "No trigger":
                    txtTriggerValue.Visible = false;
                    lblTriggerValue.Visible = false;
                    //lblTriggerDate.Visible = false;
                    tssLabel1.Text = "";
                    btnUnitTrigger.Visible = false;
                    break;
            }
        }

        public void SetTriggers1(XElement eventz)
        {
            gbLocation.Visible = true;
            gbTiming.Top = 252;
            gbLocation.Top = 181;
            gbEffect.Top = 318;
            gbCausal.Visible = false;

            lblTriggerValue.Visible = false;
            txtTriggerValue.Visible = false;
            lblValue.Visible = false;
            txtValue.Visible = false;
            lblDelay.Visible = true;
            nudDelay.Visible = true;
            nupRadius.Visible = true;
            label10.Visible = true;
            gbFormation.Visible = false;
            cboBias.Visible = false;
            btnUnitEffect.Visible = false;
            btnUnitTrigger.Visible = false;

            SetFullAttributes(eventz);
            SetTriggerBase(eventz);

            //SET X, Y
            if (eventz.Attribute("X") != null)
            {
                txtX.Text = eventz.Attribute("X").Value.ToString();
                txtY.Text = eventz.Attribute("Y").Value.ToString();
            }
            else
            {
                txtX.Text = "0";
                txtY.Text = "0";
            }
            //SET RADIUS
            int rvalue = Convert.ToInt32(eventz.Attribute("VALUE").Value);
            //if (eventz.Attribute("VALUE") != null)
            if (eventz.Attribute("VALUE") != null && (rvalue >= nupRadius.Minimum && rvalue <= nupRadius.Maximum))
            {
                //nupRadius.Value = int.Parse(eventz.Attribute("VALUE").Value.ToString());
                nupRadius.Value = Convert.ToInt32(eventz.Attribute("VALUE").Value);
            }
            else nupRadius.Value = 0;

            //SET EFFECTS CBO
            cboEffect.DataSource = shorteffectslist;
            cboEffect.Text = eventz.Attribute("EFFECT").Value.ToString();
        }

        public void SetTriggers2(XElement eventz)
        {
            gbCausal.Visible = false;
            gbLocation.Visible = false;
            gbTiming.Top = 181;
            gbLocation.Top = 318;
            gbEffect.Top = 252;
            gbCausal.Visible = false;

            lblValue.Visible = false;
            txtValue.Visible = false;
            lblTriggerValue.Visible = true;
            txtTriggerValue.Visible = true;
            lblDelay.Visible = true;
            nudDelay.Visible = true;
            gbFormation.Visible = false;
            cboBias.Visible = false;
            btnUnitEffect.Visible = false;

            SetFullAttributes(eventz);
            SetTriggerBase(eventz);

            if (cboTrigger.Text == "Unit destroyed")
            {
                int unitLength = eventz.Attribute("VALUE").Value.Length;
                string strForceID = "none";
                string strUnitID = "";
                string strNoPrefix = "";
                string strNoZero = "";

                if (unitLength < 5)
                {
                    strForceID = "1";
                    strUnitID = (Convert.ToInt32(eventz.Attribute("VALUE").Value) + 1).ToString();
                }
                else
                {
                    strForceID = "2";
                    strNoPrefix = eventz.Attribute("VALUE").Value.Substring(eventz.Attribute("VALUE").Value.Length - 4).ToString();
                    strNoZero = strNoPrefix.TrimStart('0');

                    bool isvalidunitid = Int32.TryParse(strNoZero, out int unitid);
                    if (isvalidunitid)
                    {
                        strUnitID = (Convert.ToInt32(strNoZero) + 1).ToString();
                    }
                    else
                    {
                        strUnitID = "1";
                    }
                }

                XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                string xpath = "OOB/FORCE[@ID=" + strForceID + "]/FORMATION/UNIT[@ID =" + strUnitID + "]";
                var unit = xelem.XPathSelectElement(xpath);
                if (unit != null)
                {
                    btnUnitTrigger.Text = unit.Attribute("NAME").Value;
                    btnUnitTrigger.Tag = strUnitID;
                    txtTriggerValue.Tag = strForceID;
                }
                else
                {
                    MessageBox.Show("There is no unit associated with this Event!  Select a new unit.", "Select New Unit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                txtTriggerValue.Text = eventz.Attribute("VALUE").Value.ToString();
            }

            cboEffect.DataSource = shorteffectslist;
            cboEffect.Text = eventz.Attribute("EFFECT").Value.ToString();
        }

        public void SetTriggers3(XElement eventz)
        {
            gbCausal.Visible = false;
            lblValue.Visible = true;
            txtValue.Visible = true;
            lblDelay.Visible = true;
            nudDelay.Visible = true;
            lblTriggerValue.Visible = true;
            txtTriggerValue.Visible = true;
            gbLocation.Visible = false;
            gbTiming.Top = 181;
            gbLocation.Top = 318;
            gbEffect.Top = 252;

            SetFullAttributes(eventz);
            SetTriggerBase(eventz);

            txtTriggerValue.Text = eventz.Attribute("CONTINGENCY").Value;

            if (eventz.Attribute("X") != null)
            {
                gbLocation.Visible = true;
                txtValue.Visible = true;
                lblValue.Visible = true;

                txtX.Text = eventz.Attribute("X").Value;
                txtY.Text = eventz.Attribute("Y").Value;

                if (eventz.Attribute("VALUE") != null)
                {
                    txtValue.Text = eventz.Attribute("VALUE").Value;
                }
                else txtValue.Text = "0";
            }
            else
            {
                if (eventz.Attribute("VALUE") != null)
                {
                    txtValue.Text = eventz.Attribute("VALUE").Value;
                }
                else nupRadius.Value = 0;
            }

            cboEffect.DataSource = longeffectslist;
            cboEffect.Text = eventz.Attribute("EFFECT").Value;
        }

        public void SetTriggers4(XElement eventz)
        {
            lblTriggerValue.Visible = false;
            txtTriggerValue.Visible = false;
            gbCausal.Visible = false;
            lblValue.Visible = true;
            txtValue.Visible = true;
            lblDelay.Visible = true;
            nudDelay.Visible = true;
            gbLocation.Visible = false;
            gbTiming.Top = 181;
            gbLocation.Top = 318;
            gbEffect.Top = 252;

            SetFullAttributes(eventz);
            SetTriggerBase(eventz);

            if (eventz.Attribute("X") != null)
            {
                gbLocation.Visible = true;
                txtValue.Visible = true;
                lblValue.Visible = true;

                txtX.Text = eventz.Attribute("X").Value;
                txtY.Text = eventz.Attribute("Y").Value;

                if (eventz.Attribute("VALUE") != null)
                {
                    txtValue.Text = eventz.Attribute("VALUE").Value;
                }
                else txtValue.Text = "0";
            }
            else
            {
                if (eventz.Attribute("VALUE") != null)
                {
                    //**txtTriggerValue.Visible = true;
                    txtValue.Text = eventz.Attribute("VALUE").Value;
                }
                else nupRadius.Value = 0;
            }

            cboEffect.DataSource = longeffectslist;
            cboEffect.Text = eventz.Attribute("EFFECT").Value;
        }

        public void SetTriggers5(XElement eventz)
        {
            lblTriggerValue.Visible = true;
            lblTrigger.Text = "Turn:";
            txtTriggerValue.Visible = true;
            gbCausal.Visible = false;
            lblValue.Visible = true;
            txtValue.Visible = true;
            lblDelay.Visible = true;
            nudDelay.Visible = true;
            gbLocation.Visible = false;
            gbTiming.Top = 181;
            gbLocation.Top = 318;
            gbEffect.Top = 252;

            int chance = Convert.ToInt32((eventz.Attribute("CHANCE").Value));
            int turnrange = Convert.ToInt32(eventz.Attribute("VARIABLE").Value);
            int delay = Convert.ToInt32(eventz.Attribute("TURN").Value);

            SetFullAttributes(eventz);

            //SET PROBABILITY 
            //if (eventz.Attribute("CHANCE") != null)
            if (eventz.Attribute("CHANCE") != null && (chance >= nudProb.Minimum && chance <= nudProb.Maximum))
            {
                nudProb.Value = Convert.ToInt32(eventz.Attribute("CHANCE").Value);
            }
            else nudProb.Value = 100;

            //SET TURN RANGE
            if (eventz.Attribute("VARIABLE") != null && (turnrange > nudTurnRange.Minimum && turnrange <= nudTurnRange.Maximum))
            {
                nudTurnRange.Value = (Convert.ToInt32(eventz.Attribute("VARIABLE").Value) + 1);
            }
            else nudTurnRange.Value = 1;

            //SET TURN
            if (eventz.Attribute("TURN") != null)
            {
                txtTriggerValue.Text = (Convert.ToInt32(eventz.Attribute("TURN").Value) + 1).ToString();
            }
            else txtTriggerValue.Text = "1";

            tssLabel1.Text = GameTime.getReleaseDate(txtTriggerValue.Text);
            cboEffect.DataSource = longeffectslist;
            cboEffect.Text = eventz.Attribute("EFFECT").Value.ToString();

        }

        private void SetEffectsGUI(string strEffect, XElement eventz)
        {
            SetFullAttributes(eventz);
            gbFormation.Visible = false;
            //EFFECT GROUP 1
            if (strEffect == "Activate event" || strEffect == "Cancel event" || strEffect == "Enable event" ||  //EVENT:
                strEffect == "Air Shock 1" || strEffect == "Air Shock 2" ||                                     //VALUE
                strEffect == "Air transport 1" || strEffect == "Air transport 2" ||                             //VALUE
                strEffect == "Disband unit" || strEffect == "Withdraw unit" || strEffect == "Withdraw army" ||  //UNIT ID:
                strEffect == "Force 1 track" || strEffect == "Force 2 track" ||                                 //TRACK:
                strEffect == "Guerrillas 1" || strEffect == "Guerrillas 2" ||                                   //VALUE:
                strEffect == "Move Bias 1" || strEffect == "Move Bias 2" ||                                     //VALUE:
                strEffect == "Nuclear OK 1" || strEffect == "Nuclear OK 2" ||                                   //VALUE:
                strEffect == "Pestilence 1" || strEffect == "Pestilence 2" ||                                   //VALUE:
                strEffect == "PO 1 Activate" || strEffect == "PO 2 activate" ||                                 //VALUE:
                strEffect == "Rail damage 1" || strEffect == "Rail damage 2" ||                                 //VALUE:
                strEffect == "Rail repair 1" || strEffect == "Rail repair 2" ||                                 //VALUE:
                strEffect == "Rail transport 1" || strEffect == "Rail transport 2" ||                           //VALUE:
                strEffect == "Replacements 1*" || strEffect == "Replacements 2*" ||                             //VALUE:
                strEffect == "Sea transport 1" || strEffect == "Sea transport 2" ||                             //VALUE:
                strEffect == "Shock 1" || strEffect == "Shock 2" ||                                             //VALUE:
                strEffect == "Strategic bias 1" || strEffect == "Strategic bias 2" ||                           //BIAS:
                strEffect == "Supply 1+" || strEffect == "Supply 2+" ||                                         //VALUE:
                strEffect == "Supply 1-" || strEffect == "Supply 2-" ||                                         //VALUE:
                strEffect == "Supply radius 1" || strEffect == "Supply radius 2" ||                             //VALUE:
                strEffect == "Theater Option 1" || strEffect == "Theater Option 2" ||                           //VALUE:
                strEffect == "Theater recon 1" || strEffect == "Theater recon 2" ||                             //VALUE:
                strEffect == "Variable +" || strEffect == "Variable -" ||                                       //VALUE:
                strEffect == "Victory 1+" || strEffect == "Victory 2+" ||                                       //VALUE:
                strEffect == "ZOC Cost 1" || strEffect == "ZOC Cost 2")                                         //VALUE:
            {
                lblValue.Visible = true;
                txtValue.Visible = true;
                txtValue.Text = eventz.Attribute("VALUE").Value;
                //txtValue.Text = "1";

                //DO NOT HIDE LOCATION GB IF IT IS FOR TRIGGER
                if (gbLocation.Top != 181)
                {
                    gbLocation.Visible = false;
                }

                //SET GUI FOR SPECIFIC EFFECTS:
                if (strEffect == "Activate event" || strEffect == "Cancel event" || strEffect == "Enable event")   //EVENT EFFECTS
                {
                    lblValue.Text = "Event:";
                    btnUnitEffect.Visible = false;
                    cboBias.Visible = false;
                }

                else if (strEffect == "Disband unit" || strEffect == "Withdraw unit" || strEffect == "Withdraw army") //UNIT EFFECTS
                {
                    btnUnitEffect.Visible = true;
                    cboBias.Visible = false;

                    if (eventz.Attribute("EFFECT").Value != "Disband unit" && eventz.Attribute("EFFECT").Value != "Withdraw unit" && eventz.Attribute("EFFECT").Value != "Withdraw army")
                    {
                        btnUnitEffect.Text = "Select unit";
                    }
                    else
                    {
                        //btnSave.Enabled = true;
                        int unitLength = eventz.Attribute("VALUE").Value.Length;
                        string strForceID = "none";
                        string strUnitID = "";
                        string strNoPrefix = "";
                        string strNoZero = "";

                        if (unitLength < 5)
                        {
                            strForceID = "1";
                            strUnitID = (Convert.ToInt32(eventz.Attribute("VALUE").Value) + 1).ToString();
                        }
                        else
                        {
                            strForceID = "2";
                            strNoPrefix = eventz.Attribute("VALUE").Value.Substring(eventz.Attribute("VALUE").Value.Length - 4).ToString();
                            strNoZero = strNoPrefix.TrimStart('0');

                            bool isvalidunitid = Int32.TryParse(strNoZero, out int unitid);
                            if (isvalidunitid)
                            {
                                strUnitID = (Convert.ToInt32(strNoZero) + 1).ToString();
                            }
                            else
                            {
                                strUnitID = "1";
                            }
                        }

                        XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                        string xpath = "OOB/FORCE[@ID=" + strForceID + "]/FORMATION/UNIT[@ID =" + strUnitID + "]";
                        var unit = xelem.XPathSelectElement(xpath);
                        if (unit != null)
                        {
                            btnUnitEffect.Text = unit.Attribute("NAME").Value;
                            btnUnitEffect.Tag = strUnitID;
                            txtValue.Tag = strForceID;
                            //btnSave.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("There is no unit associated with this Event!  Select a new unit.", "Select New Unit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            btnUnitEffect.Text = "Select unit";
                        }
                    }
                }

                else if (strEffect == "Force 1 track" || strEffect == "Force 2 track") //TRACK EFFECTS
                {
                    lblValue.Text = "Track:";
                    btnUnitEffect.Visible = false;
                    cboBias.Visible = false;
                }

                else if (strEffect == "Strategic bias 1" || strEffect == "Strategic bias 2") //BIAS EFFECTS
                {
                    cboBias.Visible = true;
                    btnUnitEffect.Visible = false;

                    if (eventz.Attribute("EFFECT").Value == "Strategic bias 1" || eventz.Attribute("EFFECT").Value == "Strategic bias 2")
                    {
                        cboBias.SelectedValue = eventz.Attribute("VALUE").Value.ToString();
                    }
                    else
                    {
                        cboBias.SelectedValue = "2";
                    }
                }

                else if (strEffect == "Theater Option 1" || strEffect == "Theater Option 2") //THEATER OPTION EFFECTS
                {
                    btnUnitEffect.Visible = false;
                    cboBias.Visible = false;
                    txtValue.Text = (Convert.ToInt32(eventz.Attribute("VALUE").Value) + 1).ToString();
                }
                    else
                    {
                        lblValue.Text = "Value:";
                        btnUnitEffect.Visible = false;
                        cboBias.Visible = false;
                    }

            }

            //EFFECTS WITHOUT VALUES //EFFECT GROUP 2
            if (strEffect == "Cool front" || strEffect == "Warm front" || strEffect == "Storms" ||
                            strEffect == "Cease Fire" || strEffect == "Open Fire" ||
                            strEffect == "End normal" || strEffect == "End victory 1" || strEffect == "End victory 2" ||
                            strEffect == "News only" || strEffect == "No effect" ||
                            strEffect == "Remove zone 1" || strEffect == "Remove zone 2" ||
                            strEffect == "Use chemicals 1" || strEffect == "Use chemicals 2")
            {
                lblValue.Visible = false;
                txtValue.Visible = false;
                btnUnitEffect.Visible = false;
                cboBias.Visible = false;

                //DO NOT HIDE LOCATION GB IF IT IS FOR TRIGGER
                if (gbLocation.Top != 181)
                {
                    gbLocation.Visible = false;
                }
            }

            //EFFECTS WITH LOCATION AND VALUE  // EFFECT GROUP 3
            if (strEffect == "Nuclear Attack" || strEffect == "Supply Point 1" || strEffect == "Supply Point 2")
            {
                gbLocation.Visible = true;
                lblValue.Visible = true;
                txtValue.Visible = true;
                label10.Visible = false;
                nupRadius.Visible = false;
                btnUnitEffect.Visible = false;
                cboBias.Visible = false;
                lblValue.Text = "Value";
                txtValue.Text = eventz.Attribute("VALUE").Value;
                //txtValue.Text = "1";
                txtX.Text = eventz.Attribute("X").Value;
                txtY.Text = eventz.Attribute("Y").Value;
            }

            //EFFECTS WITH LOCATION AND NO VALUE OR RADIUS//EFFECT GROUP 4
            if (strEffect == "Set ownership 1" || strEffect == "Set ownership 2")
            {
                gbLocation.Visible = true;
                nupRadius.Visible = false;
                label10.Visible = false;
                lblValue.Visible = false;
                txtValue.Visible = false;
                btnUnitEffect.Visible = false;
                cboBias.Visible = false;
                txtX.Text = eventz.Attribute("X").Value;
                txtY.Text = eventz.Attribute("Y").Value;
            }

            //EFFECTS WITH LOCATION AND RADIUS // EFFECT GROUP 5
            if (strEffect == "Refugees 1" || strEffect == "Refugees 2")
            {
                gbLocation.Visible = true;
                lblValue.Visible = false;
                txtValue.Visible = false;
                label10.Visible = true;
                nupRadius.Visible = true;
                btnUnitEffect.Visible = false;
                cboBias.Visible = false;
                txtX.Text = eventz.Attribute("X").Value;
                txtY.Text = eventz.Attribute("Y").Value;
                nupRadius.Value = Convert.ToInt32(eventz.Attribute("VALUE").Value);
            }

            //FORMATION ORDERS // EFFECT GROUP 6
            if (strEffect == "Form'n orders")
            {
                gbLocation.Visible = false;
                lblValue.Visible = true;
                txtValue.Visible = true;
                lblValue.Text = "Form ID:";
                btnUnitEffect.Visible = true;
                cboBias.Visible = false;
                gbFormation.Visible = true;
                cboOrders.SelectedValue = eventz.Attribute("X").Value.ToString();
                cboEmphasis.SelectedValue = eventz.Attribute("Y").Value.ToString();
                if (eventz.Attribute("EFFECT").Value != "Form'n orders")
                {
                    btnUnitEffect.Text = "Select Formation";
                }
                else
                {
                    //CODE FOR ID'ing FORMATION
                    int formLength = eventz.Attribute("VALUE").Value.Length;
                    string strForceID = "none";
                    string strFormID = "";
                    string strNoPrefix = "";
                    string strNoZero = "";

                    if (formLength < 4)
                    {
                        strForceID = "1";
                        strFormID = (Convert.ToInt32(eventz.Attribute("VALUE").Value) + 1).ToString();
                    }
                    else
                    {
                        strForceID = "2";
                        strNoPrefix = eventz.Attribute("VALUE").Value.Substring(eventz.Attribute("VALUE").Value.Length - 3).ToString();
                        strNoZero = strNoPrefix.TrimStart('0');

                        bool isvalidformid = Int32.TryParse(strNoZero, out int formid);
                        if (isvalidformid)
                        {
                            strFormID = (Convert.ToInt32(strNoZero) + 1).ToString();
                        }
                        else
                        {
                            strFormID = "1";
                        }
                    }

                    XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                    string xpath = "OOB/FORCE[@ID=" + strForceID + "]/FORMATION[@ID =" + strFormID + "]";
                    var unit = xelem.XPathSelectElement(xpath);
                    btnUnitEffect.Text = unit.Attribute("NAME").Value.ToString();
                    btnUnitEffect.Tag = strFormID;
                    txtValue.Tag = strForceID;
                }
            }
        }

        private void cboTrigger_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (trvEvents.SelectedNode.Level == 1)
            {
                Globals.GlobalVariables.EVENTID = trvEvents.SelectedNode.Tag.ToString();
                string strEventID = trvEvents.SelectedNode.Tag.ToString();
                txtID.Text = strEventID;

                XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                string xpath = "EVENTS/EVENT[@ID=" + Globals.GlobalVariables.EVENTID + "]";
                var eventz = xelem.XPathSelectElement(xpath);

                if (eventz.Attribute("CONTINGENCY") == null) eventz.Add(new XAttribute("CONTINGENCY", "1"));
                if (eventz.Attribute("X") == null) eventz.Add(new XAttribute("X", "1"));
                if (eventz.Attribute("Y") == null) eventz.Add(new XAttribute("Y", "1"));
                if (eventz.Attribute("TURN") == null) eventz.Add(new XAttribute("TURN", "0"));
                if (eventz.Attribute("VARIABLE") == null) eventz.Add(new XAttribute("VARIABLE", "1"));
                if (eventz.Attribute("CHANCE") == null) eventz.Add(new XAttribute("CHANCE", "100"));
                if (eventz.Attribute("VALUE") == null) eventz.Add(new XAttribute("VALUE", "1"));
                if (eventz.Attribute("NEWS") == null) eventz.Add(new XAttribute("NEWS", ""));

                //SET CONTROLS BASED ON SELECTED TRIGGER
                SetTriggerGroup(eventz);
                SetFullAttributes(eventz);

                //SET EFFECT GUI
                eventz.Attribute("CONTINGENCY").Value = "1";
                eventz.Attribute("X").Value = "1";
                eventz.Attribute("Y").Value = "1";
                eventz.Attribute("TURN").Value = "1";
                eventz.Attribute("VARIABLE").Value = "1";
                eventz.Attribute("CHANCE").Value = "100";
                eventz.Attribute("VALUE").Value = "1";
                txtNews.Text = "";
                SetEffectsGUI(cboEffect.Text, eventz);

                //NEWS TEXT
                if (eventz.Attribute("NEWS") != null)
                {
                    txtNews.Text = eventz.Attribute("NEWS").Value;
                }
                else
                {
                    txtNews.Text = "";
                }
            }
            else  //MUST SELECT EVENT!!
            {
                MessageBox.Show("You must select an Event!", "Select Event", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void cboEffect_SelectionChangeCommitted(object sender, EventArgs e)
        {

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpath = "EVENTS/EVENT[@ID=" + Globals.GlobalVariables.EVENTID + "]";
            var eventz = xelem.XPathSelectElement(xpath);

            //SET EFFECT GUI
            SetFullAttributes(eventz);
            eventz.Attribute("CONTINGENCY").Value = "1";
            eventz.Attribute("X").Value = "1";
            eventz.Attribute("Y").Value = "1";
            eventz.Attribute("TURN").Value = "1";
            eventz.Attribute("VARIABLE").Value = "1";
            eventz.Attribute("CHANCE").Value = "100";
            eventz.Attribute("VALUE").Value = "1";
            txtNews.Text = "";
            SetEffectsGUI(cboEffect.Text, eventz);

        }

        private void btnUnitTrigger_Click(object sender, EventArgs e)
        {
            var f = new frmSelectUnit();
            f.Show();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void SetSelectedUnit(string strForce, string strUnitName, string strUnitID)
        {
            if (btnUnitTrigger.Visible == true)
            {
                btnUnitTrigger.Text = strUnitName;
                btnUnitTrigger.Tag = strUnitID;
                txtTriggerValue.Tag = strForce;
            }

            if (btnUnitEffect.Visible == true)
            {
                btnUnitEffect.Text = strUnitName;
                btnUnitEffect.Tag = strUnitID;
                txtValue.Tag = strForce;
            }

            //if (cboEffect.Text != "No effect") btnSave.Enabled = true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            bool hasTriggerAndEffect = (cboTrigger.Text != "No trigger" && cboEffect.Text != "No effect");
            bool noUnitSelected = (btnUnitTrigger.Visible == true && btnUnitTrigger.Text == "Select unit") 
                   || (btnUnitEffect.Visible == true && (btnUnitEffect.Text == "Select unit" || btnUnitEffect.Text == "Select Formation"));

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpath = "EVENTS/EVENT[@ID=" + Globals.GlobalVariables.EVENTID + "]";

            var eventz = xelem.XPathSelectElement(xpath);
            if (hasTriggerAndEffect == true)
            {
                ssEventsProgress.Visible = true;
                //XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                //string xpath = "EVENTS/EVENT[@ID=" + Globals.GlobalVariables.EVENTID + "]";
                //var eventz = xelem.XPathSelectElement(xpath);
                int lastturn = Convert.ToInt32(eventz.Parent.Parent.Element("CALENDAR").Attribute("finalTurn").Value) + 1;

                if (noUnitSelected == true)
                {
                    MessageBox.Show("No unit/formation has been selected for this Event's trigger and/or effect!", "No Unit/Formation Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (cboTrigger.Text == "Turn" && Convert.ToInt32(txtTriggerValue.Text) > lastturn)
                {
                    MessageBox.Show("Please enter a valid turn number!", "Invalid Turn Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                SetFullAttributes(eventz);

                eventz.Attribute("ID").Value = trvEvents.SelectedNode.Tag.ToString();
                eventz.Attribute("TRIGGER").Value = cboTrigger.Text;
                eventz.Attribute("EFFECT").Value = cboEffect.Text;
                eventz.Attribute("NEWS").Value = txtNews.Text;

                SaveTriggerGroup(eventz);

                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                await Task.Delay(500);
                ssEventsProgress.Visible = false;
            }
            else
            {
                if(MessageBox.Show("Your Event is missing a Trigger or an Effect so will not work properly."  + Environment.NewLine + Environment.NewLine + "Proceed anyway?", 
                    "Trigger and/or Effect Missing", 
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                    SetFullAttributes(eventz);
                    eventz.Attribute("ID").Value = trvEvents.SelectedNode.Tag.ToString();
                    eventz.Attribute("TRIGGER").Value = cboTrigger.Text;
                    eventz.Attribute("EFFECT").Value = cboEffect.Text;
                    eventz.Attribute("NEWS").Value = txtNews.Text;
                    //SaveTriggerGroup(eventz);
                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    await Task.Delay(500);
                    ssEventsProgress.Visible = false;
                }
            }
        }

        public void SaveTriggerGroup(XElement eventz)
        {
            switch (cboTrigger.Text)
            {
                case "1 attacks":
                    SaveTriggers1(eventz);
                    break;

                case "2 attacks":
                    SaveTriggers1(eventz);
                    break;

                case "1 occupies":
                    SaveTriggers1(eventz);
                    break;

                case "2 occupies":
                    SaveTriggers1(eventz);
                    break;

                case "1 uses chemical":
                    SaveTriggers4(eventz);
                    break;

                case "2 uses chemical":
                    SaveTriggers4(eventz);
                    break;

                case "1 uses nuclear":
                    SaveTriggers4(eventz);
                    btnUnitTrigger.Visible = false;
                    break;

                case "2 uses nuclear":
                    SaveTriggers4(eventz);
                    break;

                case "Event activated":
                    SaveTriggers3(eventz);
                    break;

                case "Event cancelled":
                    SaveTriggers3(eventz);
                    break;

                case "Force 1 winning":
                    SaveTriggers2(eventz);
                    break;

                case "Force 2 winning":
                    SaveTriggers2(eventz);
                    break;

                case "Turn":
                    SaveTriggers5(eventz);
                    break;

                case "Unit destroyed":
                    SaveTriggers2(eventz);
                    break;

                case "Variable value":
                    SaveTriggers2(eventz);
                    break;
            }
        }

        public void SaveTriggers1(XElement eventz)
        {
            eventz.Attribute("X").Value = txtX.Text;
            eventz.Attribute("Y").Value = txtY.Text;
            eventz.Attribute("TURN").Value = nudDelay.Value.ToString();
            eventz.Attribute("VARIABLE").Value = (nudTurnRange.Value - 1).ToString();
            eventz.Attribute("CHANCE").Value = nudProb.Value.ToString();
            eventz.Attribute("VALUE").Value = nupRadius.Value.ToString();
        }

        public void SaveTriggers2(XElement eventz)
        {
            eventz.Attribute("TURN").Value = nudDelay.Value.ToString();
            eventz.Attribute("VARIABLE").Value = (nudTurnRange.Value - 1).ToString();
            eventz.Attribute("CHANCE").Value = nudProb.Value.ToString();

            eventz.Attribute("X").Remove();
            eventz.Attribute("Y").Remove();
            eventz.Attribute("CONTINGENCY").Remove();

            //********UNIT DESTROYED TRIGGER
            if (cboTrigger.Text == "Unit destroyed")
            {
                int unitLength = eventz.Attribute("VALUE").Value.Length;
                string strForceID = "";
                string strUnitID = "";
                strForceID = txtTriggerValue.Tag.ToString();

                if (strForceID == "1")
                {
                    strUnitID = (Convert.ToInt32(btnUnitTrigger.Tag) - 1).ToString();
                }
                else
                {
                    strUnitID = (10000 + (Convert.ToInt32(btnUnitTrigger.Tag) - 1)).ToString();
                }
                eventz.Attribute("VALUE").Value = strUnitID;
            }
            else
            {
                eventz.Attribute("VALUE").Value = txtTriggerValue.Text;
            }
        }

        public void SaveTriggers3(XElement eventz)
        {
            eventz.Attribute("TURN").Value = nudDelay.Value.ToString();
            eventz.Attribute("VARIABLE").Value = (nudTurnRange.Value - 1).ToString();
            eventz.Attribute("CHANCE").Value = nudProb.Value.ToString();
            eventz.Attribute("CONTINGENCY").Value = txtTriggerValue.Text;

            int maxevent = eventz.Parent.Descendants("EVENT").Max(m => (int)m.Attribute("ID"));

            if (Convert.ToInt32(txtTriggerValue.Text) > maxevent)
            {
                    MessageBox.Show("Please enter a valid, existing event!", "Enter Valid Event", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
            }

            string strEffectGroup = SaveEffectGroup(eventz);

            switch (strEffectGroup)
            {
                case "effectgroup1":
                    SaveEffectGroup1(eventz);
                    break;

                case "effectgroup2":
                    SaveEffectGroup2(eventz);
                    break;

                case "effectgroup3":
                    SaveEffectGroup3(eventz);
                    break;

                case "effectgroup4":
                    SaveEffectGroup4(eventz);
                    break;

                case "effectgroup5":
                    SaveEffectGroup5(eventz);
                    break;

                case "effectgroup6":
                    SaveEffectGroup6(eventz);
                    break;
            }
        }

        public void SaveTriggers4(XElement eventz)
        {
            eventz.Attribute("TURN").Value = nudDelay.Value.ToString();
            eventz.Attribute("VARIABLE").Value = (nudTurnRange.Value - 1).ToString();
            eventz.Attribute("CHANCE").Value = nudProb.Value.ToString();
            eventz.Attribute("CONTINGENCY").Remove();
            int maxevent = eventz.Parent.Descendants("EVENT").Max(m => (int)m.Attribute("ID"));
            //string strForceID = "";
            //string strUnitID = "";

            string strEffectGroup = SaveEffectGroup(eventz);

            switch (strEffectGroup)
            {
                case "effectgroup1":
                    SaveEffectGroup1(eventz);
                    break;
                case "effectgroup2":
                    SaveEffectGroup2(eventz);
                    break;
                case "effectgroup3":
                    SaveEffectGroup3(eventz);
                    break;
                case "effectgroup4":
                    SaveEffectGroup4(eventz);
                    break;
                case "effectgroup5":
                    SaveEffectGroup5(eventz);
                    break;
                case "effectgroup6":
                    SaveEffectGroup6(eventz);
                    break;
            }
        }

        public void SaveTriggers5(XElement eventz)
        {
            eventz.Attribute("TURN").Value = (Convert.ToInt32(txtTriggerValue.Text) - 1).ToString();
            eventz.Attribute("VARIABLE").Value = (nudTurnRange.Value - 1).ToString();
            eventz.Attribute("CHANCE").Value = nudProb.Value.ToString();
            eventz.Attribute("CONTINGENCY").Remove();

            int maxevent = eventz.Parent.Descendants("EVENT").Max(m => (int)m.Attribute("ID"));
                //string strForceID = "";
                //string strUnitID = "";

            string strEffectGroup = SaveEffectGroup(eventz);

                switch (strEffectGroup)
                {
                    case "effectgroup1":
                        SaveEffectGroup1(eventz);
                        break;
                    case "effectgroup2":
                        SaveEffectGroup2(eventz);
                        break;
                    case "effectgroup3":
                        SaveEffectGroup3(eventz);
                        break;
                    case "effectgroup4":
                        SaveEffectGroup4(eventz);
                        break;
                    case "effectgroup5":
                        SaveEffectGroup5(eventz);
                        break;
                    case "effectgroup6":
                        SaveEffectGroup6(eventz);
                        break;
                }
        }

        public string SaveEffectGroup(XElement eventz)
        {
            string strEffect = cboEffect.Text;
            string strEffectGroup = "placeholder";

            //EFFECT GROUP 1
            if (strEffect == "Activate event" || strEffect == "Cancel event" || strEffect == "Enable event" ||  //EVENT:
                strEffect == "Air Shock 1" || strEffect == "Air Shock 2" ||                                     //VALUE
                strEffect == "Air transport 1" || strEffect == "Air transport 2" ||                             //VALUE
                strEffect == "Disband unit" || strEffect == "Withdraw unit" || strEffect == "Withdraw army" ||  //UNIT ID:
                strEffect == "Force 1 track" || strEffect == "Force 2 track" ||                                 //TRACK:
                strEffect == "Guerrillas 1" || strEffect == "Guerrillas 2" ||                                   //VALUE:
                strEffect == "Move Bias 1" || strEffect == "Move Bias 2" ||                                     //VALUE:
                strEffect == "Nuclear OK 1" || strEffect == "Nuclear OK 2" ||                                   //VALUE:
                strEffect == "Pestilence 1" || strEffect == "Pestilence 2" ||                                   //VALUE:
                strEffect == "PO 1 Activate" || strEffect == "PO 2 activate" ||                                 //VALUE:
                strEffect == "Rail damage 1" || strEffect == "Rail damage 2" ||                                 //VALUE:
                strEffect == "Rail repair 1" || strEffect == "Rail repair 2" ||                                 //VALUE:
                strEffect == "Rail transport 1" || strEffect == "Rail transport 2" ||                           //VALUE:
                strEffect == "Replacements 1*" || strEffect == "Replacements 2*" ||                             //VALUE:
                strEffect == "Sea transport 1" || strEffect == "Sea transport 2" ||                             //VALUE:
                strEffect == "Shock 1" || strEffect == "Shock 2" ||                                             //VALUE:
                strEffect == "Strategic bias 1" || strEffect == "Strategic bias 2" ||                           //BIAS:
                strEffect == "Supply 1+" || strEffect == "Supply 2+" ||                                         //VALUE:
                strEffect == "Supply 1-" || strEffect == "Supply 2-" ||                                         //VALUE:
                strEffect == "Supply radius 1" || strEffect == "Supply radius 2" ||                             //VALUE:
                strEffect == "Theater Option 1" || strEffect == "Theater Option 2" ||                           //VALUE:
                strEffect == "Theater recon 1" || strEffect == "Theater recon 2" ||                             //VALUE:
                strEffect == "Variable +" || strEffect == "Variable -" ||                                       //VALUE:
                strEffect == "Victory 1+" || strEffect == "Victory 2+" ||                                       //VALUE:
                strEffect == "ZOC Cost 1" || strEffect == "ZOC Cost 2")                                         //VALUE:
            {
                strEffectGroup = "effectgroup1";
            }

            //EFFECTS WITHOUT VALUES //EFFECT GROUP 2
            if (strEffect == "Cool front" || strEffect == "Warm front" || strEffect == "Storms" ||
                            strEffect == "Cease Fire" || strEffect == "Open Fire" ||
                            strEffect == "End normal" || strEffect == "End victory 1" || strEffect == "End victory 2" ||
                            strEffect == "News only" ||
                            strEffect == "Remove zone 1" || strEffect == "Remove zone 2" ||
                            strEffect == "Use chemicals 1" || strEffect == "Use chemicals 2")
            {
                strEffectGroup = "effectgroup2";
            }

            //EFFECTS WITH LOCATION AND VALUE  // EFFECT GROUP 3
            if (strEffect == "Nuclear Attack" || strEffect == "Supply Point 1" || strEffect == "Supply Point 2")
            {
                strEffectGroup = "effectgroup3";
            }

            //EFFECTS WITH LOCATION AND NO VALUE OR RADIUS//EFFECT GROUP 4
            if (strEffect == "Set ownership 1" || strEffect == "Set ownership 2")
            {
                strEffectGroup = "effectgroup4";
            }

            //EFFECTS WITH LOCATION AND RADIUS // EFFECT GROUP 5
            if (strEffect == "Refugees 1" || strEffect == "Refugees 2")
            {
                strEffectGroup = "effectgroup5";
            }

            //FORMATION ORDERS // EFFECT GROUP 6
            if (strEffect == "Form'n orders")
            {
                strEffectGroup = "effectgroup6";
            }

            return strEffectGroup;
        }

        public void SaveEffectGroup1 (XElement eventz)
        {
            eventz.Attribute("X").Remove();
            eventz.Attribute("Y").Remove();

            string strForceID = "";
            string strUnitID = "";
            int maxevent = eventz.Parent.Descendants("EVENT").Max(m => (int)m.Attribute("ID"));

            if (cboEffect.Text == "Disband unit" || cboEffect.Text == "Withdraw unit" || cboEffect.Text == "Withdraw army")
            {
                int unitLength = eventz.Attribute("VALUE").Value.Length;
                strForceID = txtValue.Tag.ToString();

                if (strForceID == "1")
                {
                    strUnitID = (Convert.ToInt32(btnUnitEffect.Tag) - 1).ToString();
                }
                else
                {
                    strUnitID = (10000 + (Convert.ToInt32(btnUnitEffect.Tag) - 1)).ToString();
                }
                eventz.Attribute("VALUE").Value = strUnitID;
            }

            else if (cboEffect.Text == "Strategic bias 1" || cboEffect.Text == "Strategic bias 2")
            {
                eventz.Attribute("VALUE").Value = cboBias.SelectedValue.ToString();
            }

            else if (cboEffect.Text == "Enable event" || cboEffect.Text == "Activate event" || cboEffect.Text == "Cancel event")
            {
                if (Convert.ToInt32(txtValue.Text) > maxevent)
                {
                    {
                        MessageBox.Show("Please enter a valid, existing event!", "Enter Valid Event", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    eventz.Attribute("VALUE").Value = txtValue.Text;
                }
            }

            else if (cboEffect.Text == "Theater Option 1" || cboEffect.Text == "Theater Option 2") //THEATER OPTION EFFECTS
            {
                if (Convert.ToInt32(txtValue.Text) > maxevent)
                {
                    MessageBox.Show("Please enter a valid, existing event!", "Enter Valid Event", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                eventz.Attribute("VALUE").Value = (Convert.ToInt32(txtValue.Text) - 1).ToString();
            }
            else
            {
                eventz.Attribute("VALUE").Value = txtValue.Text;
            }
        }

        public void SaveEffectGroup2 (XElement eventz)
        {
            eventz.Attribute("X").Remove();
            eventz.Attribute("Y").Remove();
            eventz.Attribute("VALUE").Remove();
        }

        public void SaveEffectGroup3(XElement eventz)
        {
            eventz.Attribute("X").Value = txtX.Text;
            eventz.Attribute("Y").Value = txtY.Text;
            eventz.Attribute("VALUE").Value = txtValue.Text;
        }

        public void SaveEffectGroup4 (XElement eventz)
        {
            eventz.Attribute("X").Value = txtX.Text;
            eventz.Attribute("Y").Value = txtY.Text;
            eventz.Attribute("VALUE").Remove();
        }
        
        public void SaveEffectGroup5 (XElement eventz)
        {
            eventz.Attribute("X").Value = txtX.Text;
            eventz.Attribute("Y").Value = txtY.Text;
            eventz.Attribute("VALUE").Value = nupRadius.Value.ToString();
        }

        public void SaveEffectGroup6 (XElement eventz)
        {
            eventz.Attribute("X").Value = cboOrders.SelectedValue.ToString();
            eventz.Attribute("Y").Value = cboEmphasis.SelectedValue.ToString();

            string strForceID = "";
            string strUnitID = "";
            strForceID = txtValue.Tag.ToString();
                       
            if (strForceID == "1")
                {
                    strUnitID = (Convert.ToInt32(btnUnitEffect.Tag) - 1).ToString();
                }
            else
                {
                    strUnitID = (2000 + (Convert.ToInt32(btnUnitEffect.Tag) - 1)).ToString();
                }
                eventz.Attribute("VALUE").Value = strUnitID;
        }

        public void SetFullAttributes(XElement eventz)
        {
            if (eventz.Attribute("CONTINGENCY") == null) eventz.Add(new XAttribute("CONTINGENCY", "1"));
            if (eventz.Attribute("X") == null) eventz.Add(new XAttribute("X", "1"));
            if (eventz.Attribute("Y") == null) eventz.Add(new XAttribute("Y", "1"));
            if (eventz.Attribute("TURN") == null) eventz.Add(new XAttribute("TURN", "0"));
            if (eventz.Attribute("VARIABLE") == null) eventz.Add(new XAttribute("VARIABLE", "0"));
            if (eventz.Attribute("CHANCE") == null) eventz.Add(new XAttribute("CHANCE", "100"));
            if (eventz.Attribute("VALUE") == null) eventz.Add(new XAttribute("VALUE", "1"));
            if (eventz.Attribute("NEWS") == null) eventz.Add(new XAttribute("NEWS", ""));
        }

        private void btnUnitEffect_Click(object sender, EventArgs e)
        {
            if (cboEffect.Text == "Disband unit" || cboEffect.Text == "Withdraw unit" || cboEffect.Text == "Withdraw army")
            {
                var f = new frmSelectUnit();
                f.Show();
            }
            else
            {
                var f = new frmSelectFormation();
                f.Show();
            }
        }

        private void cboFiltTrigger_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string strTrigger = cboFiltTrigger.Text;
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            TreeNode eventsTNode;
            TreeNode eventTNode;
            TreeNode attribTNode;
            trvEvents.Nodes.Clear();
            string strEffect = "";
            
                foreach (XElement eventz1 in xelem.Descendants("EVENTS"))
                {
                    eventsTNode = trvEvents.Nodes.Add("EVENTS");
                    eventsTNode = trvEvents.Nodes[0];
                    eventsTNode.Tag = "0";

                    if (cboFiltTrigger.Text != "--ALL--")
                    {
                        foreach (XElement eventz2 in eventz1.Descendants("EVENT").Where(f => f.Attribute("TRIGGER").Value == strTrigger))
                        {
                            strEffect = eventz2.Attribute("EFFECT").Value.ToString();
                            eventTNode = eventsTNode.Nodes.Add("Event " + eventz2.Attribute("ID").Value + ": " + strEffect);
                            eventTNode.Tag = eventz2.Attribute("ID").Value;
                            eventTNode.Name = "EVENT";

                            foreach (XAttribute a in eventz2.Attributes())
                            {
                                attribTNode = eventTNode.Nodes.Add(a.Name.LocalName + ": " + a.Value);
                                Font f = new Font(trvEvents.Font, FontStyle.Regular);
                                attribTNode.NodeFont = f;
                            }
                        }
                    }
                    else
                    {
                    foreach (XElement eventz2 in eventz1.Descendants("EVENT"))
                    {
                        strEffect = eventz2.Attribute("EFFECT").Value.ToString();
                        eventTNode = eventsTNode.Nodes.Add("Event " + eventz2.Attribute("ID").Value + ": " + strEffect);
                        eventTNode.Tag = eventz2.Attribute("ID").Value;
                        eventTNode.Name = "EVENT";

                        foreach (XAttribute a in eventz2.Attributes())
                        {
                            attribTNode = eventTNode.Nodes.Add(a.Name.LocalName + ": " + a.Value);
                            Font f = new Font(trvEvents.Font, FontStyle.Regular);
                            attribTNode.NodeFont = f;
                        }
                    }
                }
           }
            trvEvents.Nodes[0].Expand();
            cboFiltEffect.SelectedIndex = 0;
        }

        private void cboFiltEffect_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string strEffect = cboFiltEffect.Text;
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            TreeNode eventsTNode;
            TreeNode eventTNode;
            TreeNode attribTNode;
            trvEvents.Nodes.Clear();
            string strTrigger = "";

            foreach (XElement eventz1 in xelem.Descendants("EVENTS"))
            {
                eventsTNode = trvEvents.Nodes.Add("EVENTS");
                eventsTNode = trvEvents.Nodes[0];
                eventsTNode.Tag = "0";
                if (cboFiltEffect.Text != "--ALL--")
                {
                    foreach (XElement eventz2 in eventz1.Descendants("EVENT").Where(f => f.Attribute("EFFECT").Value == strEffect))
                    {
                        strTrigger = eventz2.Attribute("TRIGGER").Value.ToString();
                        eventTNode = eventsTNode.Nodes.Add("Event " + eventz2.Attribute("ID").Value + ": " + strTrigger);
                        eventTNode.Tag = eventz2.Attribute("ID").Value;
                        eventTNode.Name = "EVENT";

                        foreach (XAttribute a in eventz2.Attributes())
                        {
                            attribTNode = eventTNode.Nodes.Add(a.Name.LocalName + ": " + a.Value);
                            Font f = new Font(trvEvents.Font, FontStyle.Regular);
                            attribTNode.NodeFont = f;
                        }
                    }
                }
                else
                {
                    foreach (XElement eventz2 in eventz1.Descendants("EVENT"))
                    {
                        strTrigger = eventz2.Attribute("TRIGGER").Value.ToString();
                        eventTNode = eventsTNode.Nodes.Add("Event " + eventz2.Attribute("ID").Value + ": " + strTrigger);
                        eventTNode.Tag = eventz2.Attribute("ID").Value;
                        eventTNode.Name = "EVENT";

                        foreach (XAttribute a in eventz2.Attributes())
                        {
                            attribTNode = eventTNode.Nodes.Add(a.Name.LocalName + ": " + a.Value);
                            Font f = new Font(trvEvents.Font, FontStyle.Regular);
                            attribTNode.NodeFont = f;
                        }
                    }
                }
            }

            trvEvents.Nodes[0].Expand();
            cboFiltTrigger.SelectedIndex = 0;
        }

        private void deleteEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.GlobalVariables.EVENTID = trvEvents.SelectedNode.Tag.ToString();
            TreeNode tnode;
            tnode = trvEvents.SelectedNode;
            string eventid = tnode.Tag.ToString();
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpath = "EVENTS/EVENT[@ID=" + eventid + "]";
            var eventz = xelem.XPathSelectElement(xpath);

            if (eventz.Attribute("TRIGGER").Value == "Event activated")
            {
                string strTriggerEvent = eventz.Attribute("CONTINGENCY").Value;

                if (MessageBox.Show("Neutralizing this Event means that Event " + strTriggerEvent + Environment.NewLine +
                    "might have no effect.  Continue?",
                   "Confirm Deletion",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    trvEvents.BeginUpdate();
                    //tnode.Remove();
                    //eventz.Remove();
                    cboTrigger.Text = "No trigger";
                    cboEffect.Text = "No effect";
                    eventz.Attribute("TRIGGER").Value = "No trigger";
                    eventz.Attribute("EFFECT").Value = "No effect";
                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvEvents.EndUpdate();
                    trvEvents.Refresh();
                }
            }

            else if (eventz.Attribute("TRIGGER").Value == "Event cancelled")
            {
                string strTriggerEvent = eventz.Attribute("CONTINGENCY").Value;

                if (MessageBox.Show("Neutralizing this Event means that cancellation of Event " + strTriggerEvent + Environment.NewLine +
                    "might have no effect.  Continue?",
                   "Confirm Deletion",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    trvEvents.BeginUpdate();
                    //tnode.Remove();
                    //eventz.Remove();
                    cboTrigger.Text = "No trigger";
                    cboEffect.Text = "No effect";
                    eventz.Attribute("TRIGGER").Value = "No trigger";
                    eventz.Attribute("EFFECT").Value = "No effect";
                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvEvents.EndUpdate();
                    trvEvents.Refresh();
                }
            }

            else if (eventz.Attribute("EFFECT").Value == "Activate event")
            {
                string strEffectEvent = eventz.Attribute("VALUE").Value;

                if (MessageBox.Show("Neutralizing this Event means that Event " + strEffectEvent + Environment.NewLine +
                    "will not be activated.  Continue?",
                   "Confirm Deletion",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    trvEvents.BeginUpdate();
                    //tnode.Remove();
                    //eventz.Remove();
                    cboTrigger.Text = "No trigger";
                    cboEffect.Text = "No effect";
                    eventz.Attribute("TRIGGER").Value = "No trigger";
                    eventz.Attribute("EFFECT").Value = "No effect";
                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvEvents.EndUpdate();
                    trvEvents.Refresh();
                }
            }

            else if (eventz.Attribute("EFFECT").Value == "Enable event")
            {
                string strEffectEvent = eventz.Attribute("VALUE").Value;

                if (MessageBox.Show("Neutralizing this Event means that Event " + strEffectEvent + Environment.NewLine +
                    "will not be enabled.  Continue?",
                   "Confirm Deletion",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    trvEvents.BeginUpdate();
                    //tnode.Remove();
                    //eventz.Remove();
                    cboTrigger.Text = "No trigger";
                    cboEffect.Text = "No effect";
                    eventz.Attribute("TRIGGER").Value = "No trigger";
                    eventz.Attribute("EFFECT").Value = "No effect";
                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvEvents.EndUpdate();
                    trvEvents.Refresh();
                }
            }

            else if (eventz.Attribute("EFFECT").Value == "Cancel event")
            {
                string strEffectEvent = eventz.Attribute("VALUE").Value;

                if (MessageBox.Show("Neutralizing this Event means that Event " + strEffectEvent + Environment.NewLine +
                    "will not be cancel.  Continue?",
                   "Confirm Deletion",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Exclamation,
                   MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    trvEvents.BeginUpdate();
                    //tnode.Remove();
                    //eventz.Remove();
                    cboTrigger.Text = "No trigger";
                    cboEffect.Text = "No effect";
                    eventz.Attribute("TRIGGER").Value = "No trigger";
                    eventz.Attribute("EFFECT").Value = "No effect";
                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvEvents.EndUpdate();
                    trvEvents.Refresh();
                }
            }

            else
            {
                trvEvents.BeginUpdate();
                //tnode.Remove();
                //eventz.Remove();
                cboTrigger.Text = "No trigger";
                cboEffect.Text = "No effect";
                eventz.Attribute("TRIGGER").Value = "No trigger";
                eventz.Attribute("EFFECT").Value = "No effect";
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                trvEvents.EndUpdate();
                trvEvents.Refresh();
            }
        }

        private void addEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int oldMax = 1;
            int newMax = 1;

            List<string> listEmpty = new List<string> { "No effect" };
            cboEffect.DataSource = listEmpty;

            //INSERT NEW EVENT IN XML
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpath = "EVENTS";
            var eventz = xelem.XPathSelectElement(xpath);
            oldMax = eventz.Descendants("EVENT").Max(m => (int)m.Attribute("ID"));
            newMax = oldMax + 1;
            var newXnode = new XElement("EVENT",
                    new XAttribute("ID", newMax.ToString()),
                    new XAttribute("TRIGGER", "No trigger"),
                    new XAttribute("EFFECT", "No effect"),
                    new XAttribute("X", "1"),
                    new XAttribute("Y", "1"),
                    new XAttribute("VALUE", "0"),
                    new XAttribute("CONTINGENCY", "0"),
                    new XAttribute("TURN", "0"),
                    new XAttribute("VARIABLE", "1"),
                    new XAttribute("CHANCE", "100"),
                    new XAttribute("NEWS", "")
                    );
            eventz.Add(newXnode);

            //INSERT NEW EVENT IN TREEVIEW
            TreeNode newTnode = new TreeNode();
            TreeNode attribTNode = new TreeNode();
            TreeNode rootTnode = trvEvents.Nodes[0];
            newTnode.Text = "Event " + newMax.ToString();
            newTnode.Tag = newMax.ToString();
            newTnode.Name = "EVENT";
            rootTnode.Nodes.Insert(rootTnode.LastNode.Index + 1, newTnode);

            var attributes = newXnode.Attributes();

            foreach (var att in attributes)
            {
                attribTNode = newTnode.Nodes.Add(att.Name.LocalName + ": " + att.Value);
                Font f = new Font(trvEvents.Font, FontStyle.Regular);
                attribTNode.NodeFont = f;
            }

            Globals.GlobalVariables.TREEVIEWCHANGED = true;
            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);

            trvEvents.SelectedNode = newTnode;
            trvEvents.SelectedNode.EnsureVisible();
            Globals.GlobalVariables.TREEVIEWCHANGED = false;
            trvEvents.Focus();
        }

        private void lblTriggerValue_MouseHover(object sender, EventArgs e)
        {
            if (cboTrigger.Text == "Force 1 winning" || cboTrigger.Text == "Force 2 winning")
            {
                ssEventsLabel.Text = "Enter victory level differential to serve as trigger (1-100)";
            }
            else if (cboTrigger.Text == "Variable value")
            {
                ssEventsLabel.Text = "Enter engine variable value to serve as trigger (1-100)";
            }
            else
            {
                ssEventsLabel.Text = "";
            }
        }

        private void lblTriggerValue_MouseLeave(object sender, EventArgs e)
        {
            ssEventsLabel.Text = "";
        }

        private void lblChance_MouseHover(object sender, EventArgs e)
        {
            ssEventsLabel.Text = "Enter % chance of activation (1-100)";
        }

        private void lblChance_MouseLeave(object sender, EventArgs e)
        {
            ssEventsLabel.Text = "";
        }

        private void lblTurnRange_MouseHover(object sender, EventArgs e)
        {
            ssEventsLabel.Text = "Enter turn range for event activation (1-99)";
        }

        private void lblTurnRange_MouseLeave(object sender, EventArgs e)
        {
            ssEventsLabel.Text = "";
        }

        private void lblDelay_MouseHover(object sender, EventArgs e)
        {
            ssEventsLabel.Text = "Enter number of turns delay before event activation (0-99)";
        }

        private void lblDelay_MouseLeave(object sender, EventArgs e)
        {
            ssEventsLabel.Text = "";
        }

        private void lblValue_MouseHover(object sender, EventArgs e)
        {
            switch (cboEffect.Text)
            {
                case "Air Shock 1":
                    ssEventsLabel.Text = "Enter force air shock level % (1-999)";
                    break;
                case "Air Shock 2":
                    ssEventsLabel.Text = "Enter force air shock level % (1-999)";
                    break;
                case "Air transport 1":
                    ssEventsLabel.Text = "Enter force air transport capacity (0-100,000)";
                    break;
                case "Air transport 2":
                    ssEventsLabel.Text = "Enter force air transport capacity (0-100,000)";
                    break;
                case "Force 1 track":
                    ssEventsLabel.Text = "Enter force objective track (1-3)";
                    break;
                case "Force 2 track":
                    ssEventsLabel.Text = "Enter force objective track (1-3)";
                    break;
                case "Guerrillas 1":
                    ssEventsLabel.Text = "Enter force guerilla value % (chance that hexes will change control) (0-100)";
                    break;
                case "Guerrillas 2":
                    ssEventsLabel.Text = "Enter force guerilla value % (chance that hexes will change control) (0-100)";
                    break;
                case "Move Bias 1":
                    ssEventsLabel.Text = "Enter force Movement  Bias (10-1000)";
                    break;
                case "Move Bias 2":
                    ssEventsLabel.Text = "Enter force Movement  Bias (10-1000)";
                    break;
                case "Nuclear Attack":
                    ssEventsLabel.Text = "Nuclear attack strength in kT.  Enter numeric value (1-10,000,000)";
                    break;
                case "Nuclear OK 1":
                    ssEventsLabel.Text = "Enter number of nuclear attacks per turn (0-99)";
                    break;
                case "Nuclear OK 2":
                    ssEventsLabel.Text = "Enter number of nuclear attacks per turn (0-99)";
                    break;
                case "Pestilence 1":
                    ssEventsLabel.Text = "Enter force pestilence level (0-50)";
                    break;
                case "Pestilence 2":
                    ssEventsLabel.Text = "Enter force pestilence level (0-50)";
                    break;
                case "PO 1 Activate":
                    ssEventsLabel.Text = "Enter the event to be activated (1-9,999)";
                    break;
                case "PO 2 Activate":
                    ssEventsLabel.Text = "Enter the event to be activated (1-9,999)";
                    break;
                case "Rail damage 1":
                    ssEventsLabel.Text = "Enter chance that units will damage rail in occupied hexes (0-100)";
                    break;
                case "Rail damage 2":
                    ssEventsLabel.Text = "Enter chance that units will damage rail in occupied hexes (0-100)";
                    break;
                case "Rail repair 1":
                    ssEventsLabel.Text = "Enter force automatic rail repair capability (hexes/turn) (0-999)";
                    break;
                case "Rail repair 2":
                    ssEventsLabel.Text = "Enter force automatic rail repair capability (hexes/turn) (0-999)";
                    break;
                case "Rail transport 1":
                    ssEventsLabel.Text = "Enter force rail transport capability (0-100,000)";
                    break;
                case "Rail transport 2":
                    ssEventsLabel.Text = "Enter force rail transport capability (0-100,000)";
                    break;
                case "Replacements 1*":
                    ssEventsLabel.Text = "Enter force replacement rate multiplier % (0-999)";
                    break;
                case "Replacements 2*":
                    ssEventsLabel.Text = "Enter force replacement rate multiplier % (0-999)";
                    break;
                case "Sea transport 1":
                    ssEventsLabel.Text = "Enter force sea transport capacity (0-100,000)";
                    break;
                case "Sea transport 2":
                    ssEventsLabel.Text = "Enter force sea transport capacity (0-100,000)";
                    break;
                case "Shock 1":
                    ssEventsLabel.Text = "Enter force shock level % (1-999)";
                    break;
                case "Shock 2":
                    ssEventsLabel.Text = "Enter force shock level % (1-999)";
                    break;
                case "Supply 1+":
                    ssEventsLabel.Text = "Enter number to add to force supply level (1-100)";
                    break;
                case "Supply 2+":
                    ssEventsLabel.Text = "Enter number to add to force supply level (1-100)";
                    break;
                case "Supply 1-":
                    ssEventsLabel.Text = "Enter number to subtract from force supply level (1-100)";
                    break;
                case "Supply 2-":
                    ssEventsLabel.Text = "Enter number to subtract from force supply level (1-100)";
                    break;
                case "Supply Point 1":
                    ssEventsLabel.Text = "Enter number to add to supply point (1-100)";
                    break;
                case "Supply Point 2":
                    ssEventsLabel.Text = "Enter number to add to supply point (1-100)";
                    break;
                case "Supply radius 1":
                    ssEventsLabel.Text = "Enter supply radius (hex distance to which roads extend full supply from supply point )(0-100)";
                    break;
                case "Supply radius 2":
                    ssEventsLabel.Text = "Enter supply radius (hex distance to which roads extend full supply from supply point )(0-100)";
                    break;
                case "Theater Option 1":
                    ssEventsLabel.Text = "Enter event to be activated (1-9,999)";
                    break;
                case "Theater Option 2":
                    ssEventsLabel.Text = "Enter event to be activated (1-9,999)";
                    break;
                case "Theater recon 1":
                    ssEventsLabel.Text = "Enter theater recon capability (0-100)";
                    break;
                case "Theater recon 2":
                    ssEventsLabel.Text = "Enter theater recon capability (0-100)";
                    break;
                case "Variable +":
                    ssEventsLabel.Text = "Enter amount to add to event variable (0-100)";
                    break;
                case "Variable -":
                    ssEventsLabel.Text = "Enter amount to subtract from event variable (0-100)";
                    break;
                case "Victory 1+":
                    ssEventsLabel.Text = "Enter amount to add to force victory level";
                    break;
                case "Victory 2+":
                    ssEventsLabel.Text = "Enter amount to add to force victory level";
                    break;
                case "ZOC Cost 1":
                    ssEventsLabel.Text = "Enter force ZOC cost (0-1000)";
                    break;
                case "ZOC Cost 2":
                    ssEventsLabel.Text = "Enter force ZOC cost (0-1000)";
                    break;
            }
        }

        private void lblValue_MouseLeave(object sender, EventArgs e)
        {
            ssEventsLabel.Text = "";
        }

        private void txtTriggerValue_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtTriggerValue.Text))
            {
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtTriggerValue.Text, out var intValue))
            {
                epEvents.SetError(txtTriggerValue, "Please enter valid number.");
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (cboTrigger.Text == "Force 1 winning" || cboTrigger.Text == "Force 2 winning" || cboTrigger.Text == "Variable value")
                {
                    if (intValue >= 1 && (intValue <= 100)) //
                    {
                        epEvents.SetError(txtTriggerValue, "");
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        epEvents.SetError(txtTriggerValue, "Please enter positive whole number between 1 and 100.");
                        btnSave.Enabled = false;
                    }
                }
                else if (cboTrigger.Text == "Event activated" || cboTrigger.Text == "Event cancelled")
                {
                    if (intValue >= 1 && (intValue <= 9999))
                    {
                        epEvents.SetError(txtTriggerValue, "");
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        epEvents.SetError(txtTriggerValue, "Please enter valid event number.");
                        btnSave.Enabled = false;
                    }
                }
                else if (cboTrigger.Text == "Turn")
                {
                    if (intValue >= 1 && (intValue <= 999))
                    {
                        epEvents.SetError(txtTriggerValue, "");
                        btnSave.Enabled = true;

                        //lblTriggerDate.Visible = true;
                        //DateTime gameDateTime = GameTime.getCurrentGameDate();
                        int turnLength = GameTime.getTurnLength();
                        

                        if (turnLength > 0)
                        {
                            //int turnSpan = ((Convert.ToInt32(txtTriggerValue.Text) - 1) * turnLength);
                            //TimeSpan timeSpan = TimeSpan.FromHours(turnSpan);
                            //DateTime reinfDateTime = gameDateTime + timeSpan;
                            //lblTriggerDate.Text = reinfDateTime.ToString("d MMM yyyy" + Environment.NewLine + " @ " + "HH:mm");
                            tssLabel1.Text = GameTime.getReleaseDate(txtTriggerValue.Text);
                        }
                        else
                        {
                            //lblTriggerDate.Text = "N/A";

                            tssLabel1.Text = "";
                        }
                    }
                    else
                    {
                        epEvents.SetError(txtTriggerValue, "Please enter valid turn number.");
                        btnSave.Enabled = false;
                    }
                }
                //}
            }
        }
    }
}


