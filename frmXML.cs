using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using TOAWEquipViewer;

namespace TOAWXML

{

    public partial class xmlform : Form
    {
        public xmlform()
        {
            InitializeComponent();
        }

        private void xmlform_Load(object sender, EventArgs e)
        {
            //Checks that FilePath.txt exists
            //if (System.IO.File.Exists("FilePath.txt"))
            // {
            //string filePath = File.ReadAllText("FilePath.txt");

            //    Globals.GlobalVariables.PATH = System.IO.Path.Combine(filePath);
            //    txtPath.Text = filePath;

            //    if (!System.IO.File.Exists(Globals.GlobalVariables.PATH))
            //    {
            //        frmMissingFile loadfileform = new frmMissingFile();
            //        loadfileform.ShowDialog();
            //        return;
            //    }
            //    FixInvalidXML();
            //    FixForce2SubunitBug();

            //    XDocument xdoc = XDocument.Load(Globals.GlobalVariables.PATH);

            //    //GET NAME OF FORCE 1 AND ASSIGN TO radio button text
            //    var forcenames = xdoc.Descendants("HEADER");
            //    foreach (var f in forcenames)
            //    {
            //        string fn1 = f.Attribute("forceName1").Value.ToString();
            //        this.rbForce1.Text = fn1;

            //        string fn2 = f.Attribute("forceName2").Value.ToString();
            //        this.rbForce2.Text = fn2;
            //    }

            //    //HIDE TAB HEADERS ON TAB CONTROL
            //    tabUnits.Appearance = TabAppearance.FlatButtons;
            //    tabUnits.ItemSize = new Size(0, 1);
            //    tabUnits.SizeMode = TabSizeMode.Fixed;

            //    tabUnits.SelectedIndex = 4;
            //}
            //else
            //{
            //    frmLoadFile loadfileform = new frmLoadFile();
            //    loadfileform.ShowDialog();
            //    return;
            //}

            //TOAWXML.Properties.Settings.Default.FilePath = "";
            //TOAWXML.Properties.Settings.Default.Save();

            ///>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            if (TOAWXML.Properties.Settings.Default.FilePath != "")
            {
                string filePath = TOAWXML.Properties.Settings.Default.FilePath;
                txtPath.Text = filePath;
                if (!System.IO.File.Exists(filePath))
                {
                    frmLoadFile loadfileform = new frmLoadFile();
                    loadfileform.ShowDialog();
                    return;
                }
                FixInvalidXML();
                FixForce2SubunitBug();

                XDocument xdoc = XDocument.Load(TOAWXML.Properties.Settings.Default.FilePath);

                //GET NAME OF FORCE 1 AND ASSIGN TO radio button text
                var forcenames = xdoc.Descendants("HEADER");
                foreach (var f in forcenames)
                {
                    string fn1 = f.Attribute("forceName1").Value.ToString();
                    this.rbForce1.Text = fn1;

                    string fn2 = f.Attribute("forceName2").Value.ToString();
                    this.rbForce2.Text = fn2;
                }

                //HIDE TAB HEADERS ON TAB CONTROL
                tabUnits.Appearance = TabAppearance.FlatButtons;
                tabUnits.ItemSize = new Size(0, 1);
                tabUnits.SizeMode = TabSizeMode.Fixed;

                tabUnits.SelectedIndex = 4;
            }
            else
            {
                frmLoadFile loadfileform = new frmLoadFile();
                loadfileform.ShowDialog();
                return;
            }

            //<<<<<<<<<<<<<<<<<<<<<<<<
            //POPULATES DEPLOYMENT COMBO BOX
            var deployment = new BindingList<KeyValuePair<string, string>>();
            deployment.Add(new KeyValuePair<string, string>("1", "Reinforce (Turn)"));
            deployment.Add(new KeyValuePair<string, string>("2", "Reinforce (Event)"));
            deployment.Add(new KeyValuePair<string, string>("3", "Defend/Dig In"));
            deployment.Add(new KeyValuePair<string, string>("4", "Entrenched"));
            deployment.Add(new KeyValuePair<string, string>("5", "Fortified"));
            deployment.Add(new KeyValuePair<string, string>("6", "Tactical Reserve"));
            deployment.Add(new KeyValuePair<string, string>("7", "Local Reserve"));
            deployment.Add(new KeyValuePair<string, string>("8", "Mobile"));
            deployment.Add(new KeyValuePair<string, string>("9", "Moving"));
            deployment.Add(new KeyValuePair<string, string>("10", "Attacking"));
            deployment.Add(new KeyValuePair<string, string>("11", "Supporting"));
            deployment.Add(new KeyValuePair<string, string>("12", "Retreated"));
            deployment.Add(new KeyValuePair<string, string>("13", "Routed"));
            deployment.Add(new KeyValuePair<string, string>("14", "Advancing"));
            deployment.Add(new KeyValuePair<string, string>("15", "Withdrawn"));
            deployment.Add(new KeyValuePair<string, string>("16", "Exited"));
            deployment.Add(new KeyValuePair<string, string>("17", "Embarked"));
            deployment.Add(new KeyValuePair<string, string>("18", "Disbanded"));
            deployment.Add(new KeyValuePair<string, string>("19", "Tact React"));
            deployment.Add(new KeyValuePair<string, string>("20", "Local React"));
            deployment.Add(new KeyValuePair<string, string>("21", "Entrained"));
            deployment.Add(new KeyValuePair<string, string>("22", "Airborne"));
            deployment.Add(new KeyValuePair<string, string>("23", "Seaborne"));
            deployment.Add(new KeyValuePair<string, string>("24", "Divided"));
            deployment.Add(new KeyValuePair<string, string>("25", "Nuclear"));
            deployment.Add(new KeyValuePair<string, string>("26", "Airmobile"));
            deployment.Add(new KeyValuePair<string, string>("27", "Bridge Attack"));
            deployment.Add(new KeyValuePair<string, string>("28", "Airfield Attack"));
            deployment.Add(new KeyValuePair<string, string>("29", "Reorganizing"));
            deployment.Add(new KeyValuePair<string, string>("30", "Port Attack"));

            cboDeployment.DataSource = deployment;
            cboDeployment.ValueMember = "Key";
            cboDeployment.DisplayMember = "Value";

            //POPULATES REPLACEMENT PRIORITY COMBO BOX
            var replacements = new BindingList<KeyValuePair<string, string>>();
            replacements.Add(new KeyValuePair<string, string>("3", "None"));
            replacements.Add(new KeyValuePair<string, string>("4", "Very Low"));
            replacements.Add(new KeyValuePair<string, string>("5", "Low"));
            replacements.Add(new KeyValuePair<string, string>("0", "Normal"));
            replacements.Add(new KeyValuePair<string, string>("1", "High"));
            replacements.Add(new KeyValuePair<string, string>("2", "Very High"));
            cboReplacements.DataSource = replacements;
            cboReplacements.ValueMember = "Key";
            cboReplacements.DisplayMember = "Value";

            //POPULATES MICROICON COMBO BOX
            var microicon = new BindingList<KeyValuePair<string, string>>();
            microicon.Add(new KeyValuePair<string, string>("0", "Blue"));
            microicon.Add(new KeyValuePair<string, string>("1", "Orange"));
            microicon.Add(new KeyValuePair<string, string>("2", "White"));
            microicon.Add(new KeyValuePair<string, string>("3", "Green"));
            microicon.Add(new KeyValuePair<string, string>("4", "Red"));
            cboMicroIcon.DataSource = microicon;
            cboMicroIcon.ValueMember = "Key";
            cboMicroIcon.DisplayMember = "Value";

            //POPULATES STRATEGIC BIAS COMBO BOX
            var stratbias = new BindingList<KeyValuePair<string, string>>();
            stratbias.Add(new KeyValuePair<string, string>("0", "Very Cautious"));
            stratbias.Add(new KeyValuePair<string, string>("1", "Cautious"));
            stratbias.Add(new KeyValuePair<string, string>("2", "Neutral"));
            stratbias.Add(new KeyValuePair<string, string>("3", "Aggressive"));
            stratbias.Add(new KeyValuePair<string, string>("4", "Beserk"));

            cboStratBias.DataSource = stratbias;
            cboStratBias.ValueMember = "Key";
            cboStratBias.DisplayMember = "Value";

            //POPULATES OBJECTIVES TRACK COMBO BOX
            var objtrack = new BindingList<KeyValuePair<string, string>>();
            objtrack.Add(new KeyValuePair<string, string>("1", "Track 1"));
            objtrack.Add(new KeyValuePair<string, string>("2", "Track 2"));
            objtrack.Add(new KeyValuePair<string, string>("3", "Track 3"));
            objtrack.Add(new KeyValuePair<string, string>("4", "Track 4"));
            objtrack.Add(new KeyValuePair<string, string>("5", "Track 5"));

            cboTrack.DataSource = objtrack;
            cboTrack.ValueMember = "Key";
            cboTrack.DisplayMember = "Value";
            dgvObjectives.AutoGenerateColumns = false;
            dgvObjectives.ColumnCount = 4;

            //POPULATES ICON COMBO BOX
            var icon = new BindingList<KeyValuePair<string, string>>();
            icon.Add(new KeyValuePair<string, string>("Air", "Air"));
            icon.Add(new KeyValuePair<string, string>("Anti Aircraft", "AA"));
            icon.Add(new KeyValuePair<string, string>("Airmobile Anti Air", "AA (Airmob)"));
            icon.Add(new KeyValuePair<string, string>("Motor Anti Air", "AA (Mot)"));
            icon.Add(new KeyValuePair<string, string>("Parachute Anti Air", "AA (Para)"));
            icon.Add(new KeyValuePair<string, string>("Airmobile", "Airmobile"));
            icon.Add(new KeyValuePair<string, string>("Amphibious", "Amphibious"));
            icon.Add(new KeyValuePair<string, string>("Antitank", "Antitank [v1]"));
            icon.Add(new KeyValuePair<string, string>("Antitank", "Antitank [v2]"));
            icon.Add(new KeyValuePair<string, string>("Armored Antitank", "Antitank (Armored)"));
            icon.Add(new KeyValuePair<string, string>("Airmobile Antitank", "Antitank (Airmob)"));
            icon.Add(new KeyValuePair<string, string>("Glider Antitank", "Antitank (Glider)"));
            icon.Add(new KeyValuePair<string, string>("Hvy Antitank", "Antitank (Heavy)"));
            icon.Add(new KeyValuePair<string, string>("Motor Antitank", "Antitank (Mot) [v1]"));
            icon.Add(new KeyValuePair<string, string>("Motor Antitank", "Antitank (Mot) [v2]"));
            icon.Add(new KeyValuePair<string, string>("Parachute Antitank", "Antitank (Para)"));
            icon.Add(new KeyValuePair<string, string>("Tank", "Armor"));
            icon.Add(new KeyValuePair<string, string>("Amphibious Armor", "Armor (Amphib)"));
            icon.Add(new KeyValuePair<string, string>("Assault Gun", "Armor (Asslt Gun)"));
            icon.Add(new KeyValuePair<string, string>("Glider Tank", "Armor (Glider)"));
            icon.Add(new KeyValuePair<string, string>("Hvy Armor", "Armor (Heavy)"));
            icon.Add(new KeyValuePair<string, string>("Armored Train", "Armored Train"));
            icon.Add(new KeyValuePair<string, string>("Artillery", "Artillery"));
            icon.Add(new KeyValuePair<string, string>("Airborne Artillery", "Artillery (Abn)"));
            icon.Add(new KeyValuePair<string, string>("Airmobile Arty", "Artillery (Airmob)"));
            icon.Add(new KeyValuePair<string, string>("Armored Artillery", "Artillery (Armored)"));
            icon.Add(new KeyValuePair<string, string>("Armored Hvy Arty", "Artillery (Arm, Hvy)"));
            icon.Add(new KeyValuePair<string, string>("Chemical Artillery", "Artillery (Chem)"));
            icon.Add(new KeyValuePair<string, string>("Coastal Artillery", "Artillery (Coast) [icon]"));
            icon.Add(new KeyValuePair<string, string>("Coastal Artillery", "Artillery (Coast) [silh]"));
            icon.Add(new KeyValuePair<string, string>("Fixed Artillery", "Artillery (Fixed)"));
            icon.Add(new KeyValuePair<string, string>("Glider Artillery", "Artillery (Glider)"));
            icon.Add(new KeyValuePair<string, string>("Hvy Artillery", "Artillery (Heavy)"));
            icon.Add(new KeyValuePair<string, string>("Horse Artillery", "Artillery (Horse)"));
            icon.Add(new KeyValuePair<string, string>("Inf Artillery", "Artillery (Infantry)"));
            icon.Add(new KeyValuePair<string, string>("Missile Artillery", "Artillery (Missile)"));
            icon.Add(new KeyValuePair<string, string>("Motor Artillery", "Artillery (Mot)"));
            icon.Add(new KeyValuePair<string, string>("Rail Artillery", "Artillery (Rail)"));
            icon.Add(new KeyValuePair<string, string>("Rocket Artillery", "Artillery (Rocket)"));
            icon.Add(new KeyValuePair<string, string>("Motor Rocket", "Artillery (Rocket, Mot)"));
            icon.Add(new KeyValuePair<string, string>("Bicycle", "Bicycle"));
            icon.Add(new KeyValuePair<string, string>("Heavy Bomber", "Bomber (Heavy) [icon]"));
            icon.Add(new KeyValuePair<string, string>("Heavy Bomber", "Bomber (Heavy) [silh]"));
            icon.Add(new KeyValuePair<string, string>("Jet Bomber", "Bomber (Jet)"));
            icon.Add(new KeyValuePair<string, string>("Jet Heavy Bomber", "Bomber (Jet, Heavy)"));
            icon.Add(new KeyValuePair<string, string>("Light Bomber", "Bomber (Light) [icon]"));
            icon.Add(new KeyValuePair<string, string>("Light Bomber", "Bomber (Light) [silh]"));
            icon.Add(new KeyValuePair<string, string>("Medium Bomber", "Bomber (Medium)"));
            icon.Add(new KeyValuePair<string, string>("Naval Bomber", "Bomber (Naval)"));
            icon.Add(new KeyValuePair<string, string>("Border", "Border"));
            icon.Add(new KeyValuePair<string, string>("Cavalry", "Cavalry"));
            icon.Add(new KeyValuePair<string, string>("Airmobile Cavalry", "Cavalry (Airmob)"));
            icon.Add(new KeyValuePair<string, string>("Armored Cavalry", "Cavalry (Armored)"));
            icon.Add(new KeyValuePair<string, string>("Motor Cavalry", "Cavalry (Mot)"));
            icon.Add(new KeyValuePair<string, string>("Mountain Cavalry", "Cavalry (Mtn)"));
            icon.Add(new KeyValuePair<string, string>("Civilian", "Civilian"));
            icon.Add(new KeyValuePair<string, string>("Embarked Air", "Embarked Air"));
            icon.Add(new KeyValuePair<string, string>("Embarked Heli", "Embarked Heli"));
            icon.Add(new KeyValuePair<string, string>("Embarked Naval", "Embarked Naval"));
            icon.Add(new KeyValuePair<string, string>("Embarked Rail", "Embarked Rail"));
            icon.Add(new KeyValuePair<string, string>("Engineer", "Engineer"));
            icon.Add(new KeyValuePair<string, string>("Airborne Engineer", "Engineer (Abn)"));
            icon.Add(new KeyValuePair<string, string>("Airmobile Engineer", "Engineer (Airmob)"));
            icon.Add(new KeyValuePair<string, string>("Armored Engineer", "Engineer (Armored)"));
            icon.Add(new KeyValuePair<string, string>("Ferry Engineer", "Engineer (Ferry)"));
            icon.Add(new KeyValuePair<string, string>("Motor Engineer", "Engineer (Mot)"));
            icon.Add(new KeyValuePair<string, string>("Fighter", "Fighter [icon]"));
            icon.Add(new KeyValuePair<string, string>("Fighter", "Fighter [silh]"));
            icon.Add(new KeyValuePair<string, string>("Jet Fighter", "Fighter (Jet)"));
            icon.Add(new KeyValuePair<string, string>("Naval Fighter", "Fighter (Naval)"));
            icon.Add(new KeyValuePair<string, string>("Fighter Bomber", "Fighter Bomber [icon]"));
            icon.Add(new KeyValuePair<string, string>("Fighter Bomber", "Fighter Bomber [silh]"));
            icon.Add(new KeyValuePair<string, string>("Garrison", "Garrison"));
            icon.Add(new KeyValuePair<string, string>("Guerilla", "Guerilla"));
            icon.Add(new KeyValuePair<string, string>("Headquarters", "Headquarters [v1]"));
            icon.Add(new KeyValuePair<string, string>("Headquarters", "Headquarters [v2]"));
            icon.Add(new KeyValuePair<string, string>("Airmobile Hvy Wpns", "Heavy Wpns (Airmob)"));
            icon.Add(new KeyValuePair<string, string>("Mountain Cav Hvy Wpns", "Heavy Wpns (Mtn Cav)"));
            icon.Add(new KeyValuePair<string, string>("Glider Hvy Wpns", "Heavy Wpns (Glider)"));
            icon.Add(new KeyValuePair<string, string>("Infantry Hvy Wpns", "Heavy Wpns (Infantry)"));
            icon.Add(new KeyValuePair<string, string>("Motor Hvy Wpns", "Heavy Wpns (Mot)"));
            icon.Add(new KeyValuePair<string, string>("Mountain Hvy Wpns", "Heavy Wpns (Mtn)"));
            icon.Add(new KeyValuePair<string, string>("Parachute Hvy Wpns", "Heavy Wpns (Para)"));
            icon.Add(new KeyValuePair<string, string>("Attack Helicopter", "Helicopter (Attack)"));
            icon.Add(new KeyValuePair<string, string>("Recon Helicopter", "Helicopter (Recon)"));
            icon.Add(new KeyValuePair<string, string>("Trans Helicopter", "Helicopter (Transport)"));
            icon.Add(new KeyValuePair<string, string>("Infantry", "Infantry"));
            icon.Add(new KeyValuePair<string, string>("Airmobile Infantry", "Infantry (Airmob)"));
            icon.Add(new KeyValuePair<string, string>("Glider Infantry", "Infantry (Glider)"));
            icon.Add(new KeyValuePair<string, string>("Marine Infantry", "Infantry (Marine)"));
            icon.Add(new KeyValuePair<string, string>("Mechanized", "Infantry (Mech)"));
            icon.Add(new KeyValuePair<string, string>("Motor Infantry", "Infantry (Mot)"));
            icon.Add(new KeyValuePair<string, string>("Mountain Infantry", "Infantry (Mtn)"));
            icon.Add(new KeyValuePair<string, string>("Parachute Infantry", "Infantry (Para)"));
            icon.Add(new KeyValuePair<string, string>("Irregular", "Irregular"));
            icon.Add(new KeyValuePair<string, string>("Machine Gun", "Machine Gun"));
            icon.Add(new KeyValuePair<string, string>("Motor Machinegun", "Machine Gun (Mot)"));
            icon.Add(new KeyValuePair<string, string>("Military Police", "Military Police"));
            icon.Add(new KeyValuePair<string, string>("Mortar", "Mortar"));
            icon.Add(new KeyValuePair<string, string>("Hvy Mortar", "Mortar (Heavy)"));
            icon.Add(new KeyValuePair<string, string>("Carrier Naval", "Naval (Carrier)"));
            icon.Add(new KeyValuePair<string, string>("Heavy Naval", "Naval (Heavy)"));
            icon.Add(new KeyValuePair<string, string>("Light Naval", "Naval (Light)"));
            icon.Add(new KeyValuePair<string, string>("Medium Naval", "Naval (Medium)"));
            icon.Add(new KeyValuePair<string, string>("Riverine", "Naval (Riverine)"));
            icon.Add(new KeyValuePair<string, string>("Naval Task Force", "Naval (Task Force)"));
            icon.Add(new KeyValuePair<string, string>("Naval Attack", "Naval Attack Aircraft"));
            icon.Add(new KeyValuePair<string, string>("Parachute", "Parachute"));
            icon.Add(new KeyValuePair<string, string>("Railroad Repair", "Railroad Repair"));
            icon.Add(new KeyValuePair<string, string>("Airborne Recon", "Recon (Airborne)"));
            icon.Add(new KeyValuePair<string, string>("Armored Recon", "Recon (Armored)"));
            icon.Add(new KeyValuePair<string, string>("Glider Recon", "Recon (Glider)"));
            icon.Add(new KeyValuePair<string, string>("Reserve", "Reserve"));
            icon.Add(new KeyValuePair<string, string>("Security", "Security"));
            icon.Add(new KeyValuePair<string, string>("Ski", "Ski"));
            icon.Add(new KeyValuePair<string, string>("Special Forces", "Special Forces"));
            icon.Add(new KeyValuePair<string, string>("Supply", "Supply"));
            icon.Add(new KeyValuePair<string, string>("Transport", "Transport [icon]"));
            icon.Add(new KeyValuePair<string, string>("Transport", "Transport [silh]"));
            icon.Add(new KeyValuePair<string, string>("Amphib Transport", "Transport (Amphib)"));
            icon.Add(new KeyValuePair<string, string>("Task Force", "Task Force"));
            icon.Add(new KeyValuePair<string, string>("Battlegroup", "Battle Group"));
            icon.Add(new KeyValuePair<string, string>("Kampfgruppe", "Kampfgruppe"));
            icon.Add(new KeyValuePair<string, string>("Combat Command A", "Combat Command A"));
            icon.Add(new KeyValuePair<string, string>("Combat Command B", "Combat Command B"));
            icon.Add(new KeyValuePair<string, string>("Combat Command C", "Combat Command C"));
            icon.Add(new KeyValuePair<string, string>("Combat Command R", "Combat Command R"));

            cboIcon.DataSource = icon;
            cboIcon.ValueMember = "Key";
            cboIcon.DisplayMember = "Value";

            //MAKE ALL CONTROLS INVISIBLE UNTIL AFTER TREE SELECT
            lblUnitName.Visible = false;
            txtUnitName.Visible = false;
            lblID.Visible = false;
            txtID.Visible = false;
            lblType.Visible = false;
            txtType.Visible = false;
            lblIcon.Visible = false;
            cboIcon.Visible = false;
            lblColor.Visible = false;
            cboColor.Visible = false;
            lblSize.Visible = false;
            cboSize.Visible = false;
            lblProficiency.Visible = false;
            txtProficiency.Visible = false;
            lblSupply.Visible = false;
            txtSupply.Visible = false;
            lblSupportScope.Visible = false;
            cboSupportScope.Visible = false;
            lblOrders.Visible = false;
            cboOrders.Visible = false;
            lblEmphasis.Visible = false;
            cboEmphasis.Visible = false;
            lblReadiness.Visible = false;
            txtReadiness.Visible = false;
            lblReplacements.Visible = false;
            cboReplacements.Visible = false;
            lblExperience.Visible = false;
            cboExperience.Visible = false;
            cboDeployment.Visible = false;
            lblDeployment.Visible = false;
            lblNumber.Visible = false;
            txtNumber.Visible = false;
            lblMax.Visible = false;
            txtMax.Visible = false;
            lblDamage.Visible = false;
            txtDamage.Visible = false;
            btnSave.Enabled = false;
            lblReinforce.Visible = false;
            lblReinforce2.Visible = false;
            txtReinforceTrigger.Visible = false;
            ssMainProgress.Visible = false;
            txtEntryTurn.Visible = false;
            lblEntryTurn.Visible = false;
            tssLabel1.Text = "";

            //CHINESE
            //var culture = new CultureInfo("en-US");
            //CultureInfo.DefaultThreadCurrentCulture = culture;
            //CultureInfo.DefaultThreadCurrentUICulture = culture;
            //END CHINESE
        }
        private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode)
        {
            XmlNode xNode;
            TreeNode tNode;
            XmlNodeList nodeList;
            int i;

            // Loop through the XML nodes until the leaf is reached.
            // Add the nodes to the TreeView during the looping process.
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= nodeList.Count - 1; i++)
                {
                    xNode = inXmlNode.ChildNodes[i];
                    inTreeNode.Nodes.Add(new TreeNode(xNode.Name));
                    tNode = inTreeNode.Nodes[i];
                    AddNode(xNode, tNode);
                }
            }
            else
            {
                string text = GetAttributeText(inXmlNode, "NAME");
                if (string.IsNullOrEmpty(text))
                    text = (inXmlNode.OuterXml).Trim();
                TreeNode newNode = inTreeNode.Nodes.Add(text);
            }
        }

        static string GetAttributeText(XmlNode inXmlNode, string name)
        {
            XmlAttribute attr = (inXmlNode.Attributes == null ? null : inXmlNode.Attributes[name]);
            return attr == null ? null : attr.Value;
        }

        private void trvUnitTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.txtType.Text = trvUnitTree.SelectedNode.Name.ToString();
            string type = this.txtType.Text;
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string equipid = trvUnitTree.SelectedNode.Tag.ToString();

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            trvUnitTree.Update();

            switch (type)
            {
                case "FORCE":
                    //XPATH FOR OOB PORTION OF XML
                    string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]";
                    var unit = xelem.XPathSelectElement(xpath);

                    //XPATH FOR FORCE VARIABLES PORTION OF XML
                    string xpathforcevariables = "FORCEVARIABLES/FORCE[@ID =" + Globals.GlobalVariables.FORCE + "]";
                    var forcevariables = xelem.XPathSelectElement(xpathforcevariables);

                    //SET CONTROL VISIBILITY
                    tabUnits.SelectedIndex = 0;
                    lblUnitName.Visible = true;
                    txtUnitName.Visible = true;
                    txtUnitName.Enabled = true;
                    txtUnitName.Text = trvUnitTree.SelectedNode.Text;
                    lblID.Visible = true;
                    txtID.Visible = true;
                    txtID.Text = trvUnitTree.SelectedNode.Tag.ToString();
                    lblType.Visible = true;
                    txtType.Visible = true;
                    lblSize.Visible = false;
                    cboSize.Visible = false;
                    lblIcon.Visible = false;
                    cboIcon.Visible = false;
                    lblColor.Visible = false;
                    cboColor.Visible = false;
                    lblProficiency.Visible = true;
                    txtProficiency.Visible = true;
                    txtProficiency.Text = unit.Attribute("proficiency").Value.ToString();
                    lblSupply.Visible = true;
                    txtSupply.Visible = true;
                    txtSupply.Text = unit.Attribute("supply").Value.ToString();
                    lblReinforce.Visible = false;
                    lblReinforce2.Visible = false;
                    txtReinforceTrigger.Visible = false;
                    lblDivided.Visible = false;

                    //LOAD FORCE VARIABLES
                    txtRecon.Text = forcevariables.Attribute("globalRecce").Value.ToString();
                    txtGuerilla.Text = forcevariables.Attribute("globalGuerillas").Value.ToString();
                    txtInterdiction.Text = forcevariables.Attribute("interdiction").Value.ToString();
                    txtRailRepair.Text = forcevariables.Attribute("globalRailRepair").Value.ToString();
                    txtRailDestr.Text = forcevariables.Attribute("globalRailDestruction").Value.ToString();
                    txtZOCCost.Text = forcevariables.Attribute("ZOCCost").Value.ToString();
                    txtRoadSupply.Text = forcevariables.Attribute("roadSupplyRadius").Value.ToString();
                    txtExtSupply.Text = forcevariables.Attribute("extendedSupply").Value.ToString();
                    txtNightProf.Text = forcevariables.Attribute("forceNightProficiency").Value.ToString();
                    txtPestilence.Text = forcevariables.Attribute("forcePestilence").Value.ToString();
                    txtCommunications.Text = forcevariables.Attribute("forceCommunication").Value.ToString();
                    txtLossToler.Text = forcevariables.Attribute("forceLossIntolerance").Value.ToString();
                    txtReconPtX.Text = forcevariables.Attribute("reconstitutionPointX").Value.ToString();
                    txtReconPtY.Text = forcevariables.Attribute("reconstitutionPointY").Value.ToString();
                    txtInitRailCap.Text = forcevariables.Attribute("globalRailcapInitial").Value.ToString();
                    txtCurrRailCap.Text = forcevariables.Attribute("globalRailcapCurrent").Value.ToString();
                    txtInitSeaCap.Text = forcevariables.Attribute("globalSeacapInitial").Value.ToString();
                    txtCurrSeaCap.Text = forcevariables.Attribute("globalSeacapCurrent").Value.ToString();
                    txtInitAirCap.Text = forcevariables.Attribute("globalAircapInitial").Value.ToString();
                    txtCurrAirCap.Text = forcevariables.Attribute("globalAircapCurrent").Value.ToString();
                    txtCurrTrack.Text = forcevariables.Attribute("currentTrack").Value.ToString();
                    cboMicroIcon.SelectedValue = forcevariables.Attribute("microUnitIcon").Value.ToString();
                    Globals.GlobalVariables.MICROICON = forcevariables.Attribute("microUnitIcon").Value.ToString();
                    cboStratBias.SelectedValue = forcevariables.Attribute("externalPOBias").Value.ToString();
                    txtPGWMult.Text = forcevariables.Attribute("forcePGWMultiplier").Value.ToString();

                    //SEEMS TO BE ERROR IN XML EXPORT FOR FORCEREFUEL--DEFAULT IS 0, WHICH IS INVALID VALUE
                    if (forcevariables.Attribute("forceAirRefuel").Value == "0")
                    {
                        txtAirRefuel.Text = "1";
                    }
                    else
                    {
                        txtAirRefuel.Text = forcevariables.Attribute("forceAirRefuel").Value.ToString();
                    }
                    txtRFCScal.Text = forcevariables.Attribute("RFCScalar").Value.ToString();
                    txtNavCritScal.Text = forcevariables.Attribute("navalCriticalScalar").Value.ToString();
                    txtElectSupp.Text = forcevariables.Attribute("forceElectronicSupport").Value.ToString();
                    txtReinforcements.Text = forcevariables.Attribute("newReinforcements").Value.ToString();
                    txtChemAvail.Text = forcevariables.Attribute("chemicalsAvailable").Value.ToString();
                    txtChemUsed.Text = forcevariables.Attribute("chemicalsUsed").Value.ToString();
                    txtNukeAvailInit.Text = forcevariables.Attribute("nukesAvailableInitial").Value.ToString();
                    txtNukesAvailCurr.Text = forcevariables.Attribute("nukesAvailableCurrent").Value.ToString();
                    txtNukesUsed.Text = forcevariables.Attribute("nukesUsed").Value.ToString();
                    txtNBCReadiness.Text = forcevariables.Attribute("forceNBCReadiness").Value.ToString();
                    txtVictoryMod.Text = forcevariables.Attribute("victoryModifications").Value.ToString();
                    txtHandicap.Text = forcevariables.Attribute("globalHandicap").Value.ToString();
                    txtMoveBias.Text = forcevariables.Attribute("forceMoveBias").Value.ToString();
                    txtAirHandicap.Text = forcevariables.Attribute("globalAirHandicap").Value.ToString();
                    txtReconPtValue.Text = forcevariables.Attribute("reconstitutionPointValue").Value.ToString();
                    break;

                case "FORMATION":
                    tabUnits.SelectedIndex = 1;
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + formid + "]";
                    unit = xelem.XPathSelectElement(xpath);
                    lblUnitName.Visible = true;
                    txtUnitName.Visible = true;
                    txtUnitName.Enabled = true;
                    txtUnitName.Text = unit.Attribute("NAME").Value;
                    lblID.Visible = true;
                    txtID.Visible = true;
                    lblType.Visible = true;
                    txtType.Visible = true;
                    txtID.Text = trvUnitTree.SelectedNode.Tag.ToString();
                    lblSize.Visible = false;
                    cboSize.Visible = false;
                    lblIcon.Visible = false;
                    lblColor.Visible = false;
                    cboColor.Visible = false;
                    cboIcon.Visible = false;
                    lblProficiency.Visible = true;
                    txtProficiency.Visible = true;
                    txtProficiency.Text = unit.Attribute("PROFICIENCY").Value;
                    lblSupply.Visible = true;
                    txtSupply.Visible = true;
                    txtSupply.Text = unit.Attribute("SUPPLY").Value;
                    lblSupportScope.Visible = true;
                    cboSupportScope.Visible = true;
                    cboSupportScope.Text = unit.Attribute("SUPPORTSCOPE").Value;
                    lblOrders.Visible = true;
                    cboOrders.Visible = true;
                    cboOrders.Text = unit.Attribute("ORDERS").Value;
                    lblEmphasis.Visible = true;
                    cboEmphasis.Visible = true;
                    cboEmphasis.Text = unit.Attribute("EMPHASIS").Value;
                    lblReadiness.Visible = false;
                    txtReadiness.Visible = false;
                    lblReplacements.Visible = false;
                    cboReplacements.Visible = false;
                    lblDeployment.Visible = false;
                    cboDeployment.Visible = false;
                    lblExperience.Visible = false;
                    cboExperience.Visible = false;
                    lblNumber.Visible = false;
                    txtNumber.Visible = false;
                    lblMax.Visible = false;
                    txtMax.Visible = false;
                    lblDamage.Visible = false;
                    txtDamage.Visible = false;
                    lblReinforce.Visible = false;
                    lblReinforce2.Visible = false;
                    txtReinforceTrigger.Visible = false;
                    txtReinforceTrigger.Text = "--";

                    if (cboOrders.Text == "Static")
                    {
                        txtEntryTurn.Visible = true;
                        lblEntryTurn.Visible = true;
                        txtEntryTurn.Text = unit.Attribute("ENTRYTURN").Value;
                        string date = txtEntryTurn.Text;
                        tssLabel1.Text = GameTime.getReleaseDate(date);

                        trvUnitTree.SelectedNode.ForeColor = System.Drawing.Color.IndianRed;
                        Font font = new Font(trvUnitTree.Font, FontStyle.Bold);
                        trvUnitTree.SelectedNode.NodeFont = font;
                    }
                    else
                    {
                        txtEntryTurn.Visible = false;
                        lblEntryTurn.Visible = false;
                        tssLabel1.Text = "";

                        trvUnitTree.SelectedNode.ForeColor = System.Drawing.Color.Black;
                        Font font = new Font(trvUnitTree.Font, FontStyle.Regular);
                        trvUnitTree.SelectedNode.NodeFont = font;
                    }

                    //SET UP OBJECTIVES DATAGRIDVIEW
                    string strTrack = cboTrack.SelectedValue.ToString();

                    dgvObjectives.Columns[0].Name = "ID";
                    dgvObjectives.Columns["ID"].DataPropertyName = "ID";
                    dgvObjectives.Columns[1].Name = "DESCRIPTION";
                    dgvObjectives.Columns["DESCRIPTION"].DataPropertyName = "DESCRIPTION";
                    dgvObjectives.Columns[2].Name = "X";
                    dgvObjectives.Columns["X"].DataPropertyName = "X";
                    dgvObjectives.Columns[3].Name = "Y";
                    dgvObjectives.Columns["Y"].DataPropertyName = "Y";

                    dgvObjectives.Columns[0].ReadOnly = true;

                    dgvObjectives.Columns[0].Width = 25;
                    dgvObjectives.Columns[1].Width = 165;
                    dgvObjectives.Columns[2].Width = 30;
                    dgvObjectives.Columns[3].Width = 30;
                    dgvObjectives.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvObjectives.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvObjectives.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgvObjectives.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvObjectives.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    DataTable dt = new DataTable();

                    dt.Columns.Add("ID", typeof(Int32));
                    dt.Columns.Add("DESCRIPTION", typeof(string));
                    dt.Columns.Add("X", typeof(Int32));
                    dt.Columns.Add("Y", typeof(Int32));
                    XDocument xdoc = XDocument.Load(TOAWXML.Properties.Settings.Default.FilePath);

                    var objectives = (from d in xdoc.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("OBJECTIVES").Descendants("OBJECTIVE")
                                      where (string)d.Parent.Parent.Parent.Attribute("ID") == Globals.GlobalVariables.FORCE && (string)d.Parent.Parent.Attribute("ID") == formid && (string)d.Parent.Attribute("TRACK") == strTrack
                                      select new
                                      {
                                          ID = d.Attribute("ID").Value.ToString(),
                                          DESCRIPTION = d.Attribute("DESCRIPTION").Value,
                                          X = d.Attribute("X").Value.ToString(),
                                          Y = d.Attribute("Y").Value.ToString()
                                      });
                    objectives.ToList().ForEach(i => dt.Rows.Add(i.ID, i.DESCRIPTION, i.X, i.Y));
                    dgvObjectives.DataSource = dt;
                    ///END OF DGVOBJECTIVES BLOCK
                    break;

                case "UNIT":
                    tabUnits.SelectedIndex = 2;
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                    unit = xelem.XPathSelectElement(xpath);

                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    //SET DEPLOYMENT COMBO BOX FOR LAND OR AIR UNITS
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

                    //SET DEPLOY COMBOBOX FOR AIR OR LAND UNITS
                    if (airunits.Contains(unit.Attribute("ICON").Value.ToString()))  //IF UNIT HAS AIR UNIT ICON TYPE, SET DEPLOY COMBOBOX TO AIR MISSIONS
                    {
                        var deployment = new BindingList<KeyValuePair<string, string>>();
                        deployment.Add(new KeyValuePair<string, string>("3", "Interdiction"));
                        deployment.Add(new KeyValuePair<string, string>("4", "Air Superiority"));
                        deployment.Add(new KeyValuePair<string, string>("5", "Combat Support"));
                        deployment.Add(new KeyValuePair<string, string>("8", "Rest"));
                        deployment.Add(new KeyValuePair<string, string>("23", "Sea Interdiction"));

                        cboDeployment.DataSource = deployment;
                        cboDeployment.ValueMember = "Key";
                        cboDeployment.DisplayMember = "Value";
                    }
                    else // OTHERWISE SET TO LAND DEPLOYMENTS
                    {
                        var deployment = new BindingList<KeyValuePair<string, string>>();
                        deployment.Add(new KeyValuePair<string, string>("1", "Reinforce (Turn)"));
                        deployment.Add(new KeyValuePair<string, string>("2", "Reinforce (Event)"));
                        deployment.Add(new KeyValuePair<string, string>("3", "Defend/Dig In"));
                        deployment.Add(new KeyValuePair<string, string>("4", "Entrenched"));
                        deployment.Add(new KeyValuePair<string, string>("5", "Fortified"));
                        deployment.Add(new KeyValuePair<string, string>("6", "Tactical Reserve"));
                        deployment.Add(new KeyValuePair<string, string>("7", "Local Reserve"));
                        deployment.Add(new KeyValuePair<string, string>("8", "Mobile"));
                        deployment.Add(new KeyValuePair<string, string>("9", "Moving"));
                        deployment.Add(new KeyValuePair<string, string>("10", "Attacking"));
                        deployment.Add(new KeyValuePair<string, string>("11", "Supporting"));
                        deployment.Add(new KeyValuePair<string, string>("12", "Retreated"));
                        deployment.Add(new KeyValuePair<string, string>("13", "Routed"));
                        deployment.Add(new KeyValuePair<string, string>("14", "Advancing"));
                        deployment.Add(new KeyValuePair<string, string>("15", "Withdrawn"));
                        deployment.Add(new KeyValuePair<string, string>("16", "Exited"));
                        deployment.Add(new KeyValuePair<string, string>("17", "Embarked"));
                        deployment.Add(new KeyValuePair<string, string>("18", "Disbanded"));
                        deployment.Add(new KeyValuePair<string, string>("19", "Tact React"));
                        deployment.Add(new KeyValuePair<string, string>("20", "Local React"));
                        deployment.Add(new KeyValuePair<string, string>("21", "Entrained"));
                        deployment.Add(new KeyValuePair<string, string>("22", "Airborne"));
                        deployment.Add(new KeyValuePair<string, string>("23", "Seaborne"));
                        deployment.Add(new KeyValuePair<string, string>("24", "Divided"));
                        deployment.Add(new KeyValuePair<string, string>("25", "Nuclear"));
                        deployment.Add(new KeyValuePair<string, string>("26", "Airmobile"));
                        deployment.Add(new KeyValuePair<string, string>("27", "Bridge Attack"));
                        deployment.Add(new KeyValuePair<string, string>("28", "Airfield Attack"));
                        deployment.Add(new KeyValuePair<string, string>("29", "Reorganizing"));
                        deployment.Add(new KeyValuePair<string, string>("30", "Port Attack"));
                        cboDeployment.DataSource = deployment;
                        cboDeployment.ValueMember = "Key";
                        cboDeployment.DisplayMember = "Value";
                    }

                    //SET CONTROL VISIBILITY
                    lblUnitName.Visible = true;
                    txtUnitName.Visible = true;
                    txtUnitName.Enabled = true;
                    txtUnitName.Text = unit.Attribute("NAME").Value.ToString();
                    lblID.Visible = true;
                    txtID.Visible = true;
                    txtID.Text = trvUnitTree.SelectedNode.Tag.ToString();
                    lblType.Visible = true;
                    txtType.Visible = true;
                    lblSize.Visible = true;
                    cboSize.Visible = true;
                    lblIcon.Visible = true;
                    cboIcon.Visible = true;
                    lblColor.Visible = true;
                    cboColor.Visible = true;
                    cboColor.Text = unit.Attribute("COLOR").Value.ToString();
                    cboSize.Text = unit.Attribute("SIZE").Value.ToString();
                    lblProficiency.Visible = true;
                    txtProficiency.Visible = true;
                    txtProficiency.Text = unit.Attribute("PROFICIENCY").Value.ToString();
                    lblSupply.Visible = true;
                    txtSupply.Visible = true;
                    txtSupply.Text = unit.Attribute("SUPPLY").Value.ToString();
                    lblSupportScope.Visible = false;
                    cboSupportScope.Visible = false;
                    lblOrders.Visible = false;
                    cboOrders.Visible = false;
                    lblReadiness.Visible = true;
                    txtReadiness.Visible = true;
                    txtReadiness.Text = unit.Attribute("READINESS").Value.ToString();
                    lblExperience.Visible = true;
                    cboExperience.Visible = true;
                    cboExperience.Text = unit.Attribute("EXPERIENCE").Value.ToString();
                    lblDeployment.Visible = true;
                    cboDeployment.Visible = true;
                    cboDeployment.SelectedValue = unit.Attribute("STATUS").Value.ToString();
                    cboDeployment.Update();
                    lblEmphasis.Visible = true;
                    cboEmphasis.Visible = true;
                    cboEmphasis.Text = unit.Attribute("EMPHASIS").Value.ToString();
                    lblReplacements.Visible = true;
                    cboReplacements.Visible = true;
                    cboReplacements.SelectedValue = unit.Attribute("REPLACEMENTPRIORITY").Value.ToString();
                    cboDeployment.Update();
                    lblDivided.Visible = false;
                    txtEntryTurn.Visible = false;
                    lblEntryTurn.Visible = false;
                    tssLabel1.Text = "";
                    //lblEntryDate.Visible = false;

                    //SET ICON COMBOBOX
                    if (unit.Attribute("ICONID") == null)
                    {
                        cboIcon.SelectedValue = unit.Attribute("ICON").Value.ToString();
                    }
                    else //IF ALTERNATE ICONS EXIST
                    {
                        switch (unit.Attribute("ICONID").Value.ToString())
                        {
                            case "0":
                                cboIcon.Text = "Headquarters [v1]";
                                break;
                            case "1":
                                cboIcon.Text = "Headquarters [v2]";
                                break;
                            case "14":
                                cboIcon.Text = "Antitank [v1]";
                                break;
                            case "15":
                                cboIcon.Text = "Antitank [v2]";
                                break;
                            case "25":
                                cboIcon.Text = "Antitank (Mot) [v1]";
                                break;
                            case "26":
                                cboIcon.Text = "Antitank (Mot) [v2]";
                                break;
                            case "41":
                                cboIcon.Text = "Fighter [icon]";
                                break;
                            case "42":
                                cboIcon.Text = "Fighter Bomber [icon]";
                                break;
                            case "43":
                                cboIcon.Text = "Bomber (Light) [icon]";
                                break;
                            case "45":
                                cboIcon.Text = "Bomber (Heavy) [icon]";
                                break;
                            case "62":
                                cboIcon.Text = "Artillery (Coast) [icon]";
                                break;
                            case "63":
                                cboIcon.Text = "Artillery (Coast) [silh]";
                                break;
                            case "66":
                                cboIcon.Text = "Fighter [silh]";
                                break;
                            case "67":
                                cboIcon.Text = "Fighter Bomber [silh]";
                                break;
                            case "68":
                                cboIcon.Text = "Bomber (Light) [silh]";
                                break;
                            case "69":
                                cboIcon.Text = "Bomber (Heavy) [silh]";
                                break;
                            case "82":
                                cboIcon.Text = "Transport [icon]";
                                break;
                            case "94":
                                cboIcon.Text = "Transport [silh]";
                                break;
                        }
                    }

                    //SET DEPLOY COMBOBOX
                    string deploy;

                    //IF NO DEPLOYMENT HAS BEEN SET PREVIOUSLY
                    if (cboDeployment.SelectedValue != null)
                    {
                        deploy = cboDeployment.SelectedValue.ToString();
                    }
                    else
                    {
                        cboDeployment.SelectedValue = "8";
                        deploy = cboDeployment.SelectedValue.ToString();
                    }

                    if (cboDeployment.SelectedValue.ToString() == "1" || cboDeployment.SelectedValue.ToString() == "2")
                    {
                        txtReinforceTrigger.Enabled = true;
                        //lblReinfDate.Visible = true;

                    }
                    else
                    {
                        //lblReinfDate.Visible = false;
                        tssLabel1.Text = "";
                    }

                    switch (deploy)
                    {
                        case "1":  //SET ENTRY LOCATION FOR UNITS WHICH ARE REINFORCEMENTS BY TURN
                            lblReinforce.Visible = true;
                            lblReinforce2.Visible = true;
                            txtReinforceTrigger.Visible = true;
                            lblReinforce.Text = "Turn";

                            //SET X AND Y, AFTER CHECKING FOR NULL
                            if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                            {
                                unit.Add(new XAttribute("GOINGTOX", "1"));
                                unit.Add(new XAttribute("GOINGTOY", "1"));
                            }

                            if (unit.Attribute("GOINGTOX") != null)
                            {
                                txtX.Text = unit.Attribute("GOINGTOX").Value.ToString();
                            }
                            else
                            {
                                txtX.Text = "1";
                            }

                            if (unit.Attribute("GOINGTOY") != null)
                            {
                                txtY.Text = unit.Attribute("GOINGTOY").Value.ToString();
                            }
                            else
                            {
                                txtY.Text = "1";
                            }

                            //SET ENTRY TURN AFTER CHECKING FOR NULL
                            if (unit.Attribute("ENTRY") != null)
                            {
                                //lblReinfDate.Visible = true;
                                txtReinforceTrigger.Text = unit.Attribute("ENTRY").Value.ToString();
                                tssLabel1.Text = GameTime.getReleaseDate(txtReinforceTrigger.Text);
                            }
                            else
                            {
                                //lblReinfDate.Visible = false;
                                tssLabel1.Text = "";
                                txtReinforceTrigger.Text = "998";
                            }

                            //ENABLE UNIT ATTRIBUTES FOR NON-DIVIDED UNITS
                            NonDivideUnitGUI();

                            ////DISABLE "DIVIDE UNIT" MENU ITEM
                            //divideUnitToolStripMenuItem1.Enabled = false;

                            break;

                        case "2": //SET ENTRY LOCATION FOR UNITS WHICH ARE REINFORCEMENTS BY EVENT
                            lblReinforce.Visible = true;
                            lblReinforce2.Visible = true;
                            txtReinforceTrigger.Visible = true;
                            txtReinforceTrigger.Enabled = true;
                            lblReinforce.Text = "Event";

                            //SET X AND Y, AFTER CHECKING FOR NULL
                            if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                            {
                                unit.Add(new XAttribute("GOINGTOX", "1"));
                                unit.Add(new XAttribute("GOINGTOY", "1"));
                            }

                            if (unit.Attribute("GOINGTOX") != null)
                            {
                                txtX.Text = unit.Attribute("GOINGTOX").Value.ToString();
                            }
                            else
                            {
                                txtX.Text = "1";
                            }

                            if (unit.Attribute("GOINGTOY") != null)
                            {
                                txtY.Text = unit.Attribute("GOINGTOY").Value.ToString();
                            }
                            else
                            {
                                txtY.Text = "1";
                            }

                            //SET ENTRY EVENT AFTER CHECKING FOR NULL
                            if (unit.Attribute("ENTRY") != null)
                            {
                                txtReinforceTrigger.Text = unit.Attribute("ENTRY").Value.ToString();
                            }
                            else
                            {
                                txtReinforceTrigger.Text = "999";
                            }
                            //ENABLE UNIT ATTRIBUTES FOR NON-DIVIDED UNITS
                            NonDivideUnitGUI();

                            break;

                        case "24": //SET NO LOCATION FOR DIVIDED UNITS
                            txtX.Text = "--";
                            txtY.Text = "--";

                            //DISABLE UNIT ATTRIBUTES FOR DIVIDED UNITS
                            DivideUnitGUI();

                            break;

                        default: //SET LOCATION FOR ON-MAP UNITS
                            lblReinforce.Visible = false;
                            lblReinforce2.Visible = false;
                            txtReinforceTrigger.Visible = false;

                            if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                            {
                                unit.Add(new XAttribute("X", "--"));
                                unit.Add(new XAttribute("Y", "--"));
                            }

                            if (unit.Attribute("X") != null)
                            {
                                txtX.Text = unit.Attribute("X").Value.ToString();
                            }
                            else
                            {
                                txtX.Text = "--";
                            }

                            if (unit.Attribute("Y") != null)
                            {
                                txtY.Text = unit.Attribute("Y").Value.ToString();
                            }
                            else
                            {
                                txtY.Text = "--";
                            }

                            //ENABLE UNIT ATTRIBUTES FOR NON-DIVIDED UNITS
                            NonDivideUnitGUI();

                            break;
                    }

                    lblNumber.Visible = false;
                    txtNumber.Visible = false;
                    lblMax.Visible = false;
                    txtMax.Visible = false;
                    lblDamage.Visible = false;
                    txtDamage.Visible = false;

                    //SHOW LABEL FOR DIVIDED SUBUNITS
                    if (unit.Attribute("PARENT") != null)
                    {
                        lblDivided.Text = "DIVIDED SUBUNIT";
                        lblDivided.Visible = true;
                    }

                    //IF UNIT IS NOT DIVIDED SUBUNIT, CAN DIVIDE & ADD EQUIP, CANNOT REUNITE
                    if (unit.Attribute("PARENT") == null)
                    {
                        divideUnitToolStripMenuItem1.Enabled = true;
                        reuniteUnitToolStripMenuItem1.Enabled = false;
                        addeqpNewEquipUnitStripMenuItem.Enabled = true;
                    }
                    //
                    //IF UNIT IS DIVIDED PARENT, CANNOT DIVIDE OR ADD EQUIP, CAN REUNITE
                    if ((unit.Attribute("PARENT") == null) && (unit.Attribute("STATUS").Value.ToString() == "24"))
                    {
                        divideUnitToolStripMenuItem1.Enabled = false;
                        reuniteUnitToolStripMenuItem1.Enabled = true;
                        addeqpNewEquipUnitStripMenuItem.Enabled = false;
                    }

                    //IF UNIT IS DIVIDED SUBUNIT, CANNOT DIVIDE, REUNITE, OR ADD EQUIP
                    if ((unit.Attribute("PARENT") != null) && (unit.Attribute("STATUS").Value.ToString() != "24"))
                    {
                        divideUnitToolStripMenuItem1.Enabled = false;
                        reuniteUnitToolStripMenuItem1.Enabled = false;
                        addeqpNewEquipUnitStripMenuItem.Enabled = false;
                    }

                    //IF UNIT IF REINFORCMENT, CANNOT DIVIDE
                    if (deploy == "1" || deploy == "2")
                    {
                        divideUnitToolStripMenuItem1.Enabled = false;
                    }

                    //IF UNIT IS SECTION-SIZED, CANNOT DIVIDE OR REUNITE, BUT CAN ADD EQUIP
                    if (unit.Attribute("SIZE").Value.ToString() == "Section")
                    {
                        divideUnitToolStripMenuItem1.Enabled = false;
                        reuniteUnitToolStripMenuItem1.Enabled = false;
                        addeqpNewEquipUnitStripMenuItem.Enabled = true;
                    }

                    break;

                case "EQUIPMENT":
                    tabUnits.SelectedIndex = 3;
                    string parentunitid = trvUnitTree.SelectedNode.Parent.Tag.ToString();
                    if (Globals.GlobalVariables.TREEVIEWCHANGED == true)
                    {
                        parentunitid = Globals.GlobalVariables.DRAGGEDPARENTID;
                    }
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + parentunitid + "]/EQUIPMENT[@ID =" + equipid + "]";
                    unit = xelem.XPathSelectElement(xpath);

                    if (unit != null)
                    {
                        lblUnitName.Visible = true;
                        txtUnitName.Visible = true;
                        txtUnitName.Enabled = false;
                        txtUnitName.Text = unit.Attribute("NAME").Value.ToString();
                        lblID.Visible = true;
                        txtID.Visible = true;
                        txtID.Text = trvUnitTree.SelectedNode.Tag.ToString();
                        lblType.Visible = true;
                        txtType.Visible = true;
                        lblSize.Visible = false;
                        cboSize.Visible = false;
                        lblIcon.Visible = false;
                        cboIcon.Visible = false;
                        lblColor.Visible = false;
                        cboColor.Visible = false;
                        lblProficiency.Visible = false;
                        txtProficiency.Visible = false;
                        lblSupply.Visible = false;
                        txtSupply.Visible = false;
                        lblSupportScope.Visible = false;
                        cboSupportScope.Visible = false;
                        lblOrders.Visible = false;
                        cboOrders.Visible = false;
                        lblEmphasis.Visible = false;
                        cboEmphasis.Visible = false;
                        lblReadiness.Visible = false;
                        txtReadiness.Visible = false;
                        lblExperience.Visible = false;
                        cboExperience.Visible = false;
                        lblDeployment.Visible = false;
                        cboDeployment.Visible = false;
                        lblReplacements.Visible = false;
                        cboReplacements.Visible = false;
                        lblNumber.Visible = true;
                        txtNumber.Visible = true;
                        txtNumber.Text = unit.Attribute("NUMBER").Value.ToString();
                        lblMax.Visible = true;
                        txtMax.Visible = true;
                        lblMax.Visible = true;
                        txtMax.Text = unit.Attribute("MAX").Value.ToString();
                        lblDamage.Visible = false;
                        txtDamage.Visible = false;
                        txtDamage.Text = unit.Attribute("DAMAGE").Value.ToString();
                        txtEntryTurn.Visible = false;
                        lblEntryTurn.Visible = false;
                        tssLabel1.Text = "";

                        if ((unit.Parent.Attribute("STATUS").Value.ToString() == "24") || unit.Parent.Attribute("PARENT") != null) //IF UNIT DIVIDED PARENT OR SUBUNIT
                        {
                            deleteToolStripMenuItem2.Enabled = false;
                            copyToolStripMenuItem2.Enabled = false;
                        }
                        else
                        {
                            deleteToolStripMenuItem2.Enabled = true;
                            copyToolStripMenuItem2.Enabled = true;
                        }
                    }
                    break;
            }

            btnSave.Enabled = true;
        }

        private async void rbForce1_CheckedChanged(object sender, EventArgs e)
        {
            ssMainProgress.Visible = true;
            LoadTree();
            tabUnits.SelectedIndex = 4;
            Globals.GlobalVariables.FORCE = "1";
            await Task.Delay(500);
            ssMainProgress.Visible = false;
        }

        private async void rbForce2_CheckedChanged(object sender, EventArgs e)
        {
            ssMainProgress.Visible = true;
            LoadTree();
            tabUnits.SelectedIndex = 4;
            Globals.GlobalVariables.FORCE = "2";
            await Task.Delay(500);
            ssMainProgress.Visible = false;
        }

        private void LoadTree()
        {
            ///XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            ////XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            TreeNode forceNode;
            TreeNode formationNode;
            TreeNode unitNode;
            string strForce;

            //MAKE ALL CONTROLS INVISIBLE UNTIL AFTER TREE SELECT
            lblUnitName.Visible = false;
            txtUnitName.Visible = false;
            lblID.Visible = false;
            txtID.Visible = false;
            lblType.Visible = false;
            txtType.Visible = false;
            lblSize.Visible = false;
            cboSize.Visible = false;
            lblProficiency.Visible = false;
            txtProficiency.Visible = false;
            lblSupply.Visible = false;
            txtSupply.Visible = false;
            lblSupportScope.Visible = false;
            cboSupportScope.Visible = false;
            lblOrders.Visible = false;
            cboOrders.Visible = false;
            lblEmphasis.Visible = false;
            cboEmphasis.Visible = false;
            lblReadiness.Visible = false;
            txtReadiness.Visible = false;
            lblReplacements.Visible = false;
            cboReplacements.Visible = false;
            lblExperience.Visible = false;
            cboExperience.Visible = false;
            cboDeployment.Visible = false;
            lblDeployment.Visible = false;
            lblNumber.Visible = false;
            txtNumber.Visible = false;
            lblMax.Visible = false;
            txtMax.Visible = false;
            lblDamage.Visible = false;
            txtDamage.Visible = false;
            txtEntryTurn.Visible = false;
            lblEntryTurn.Visible = false;
            tssLabel1.Text = "";
            //lblEntryDate.Visible = false;

            ///XElement xdoc = XElement.Load(Globals.GlobalVariables.PATH);
            XElement xdoc = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            //Console.WriteLine(TOAWXML.Properties.Settings.Default.FilePath);
            //Console.WriteLine(xdoc.ToString());

            strForce = "0";

            if (this.rbForce1.Checked == true)
            {
                strForce = "1";
            }
            else if (this.rbForce2.Checked == true)
            {
                strForce = "2";
            }

            trvUnitTree.Nodes.Clear();

            foreach (XElement force in xdoc.Descendants("FORCE").Where(f => f.Attribute("ID").Value == strForce))
            {
                if (force.Attribute("NAME") != null)
                {
                    forceNode = trvUnitTree.Nodes.Add(force.Attribute("NAME").Value);
                    forceNode.Tag = force.Attribute("ID").Value;
                    forceNode.Name = "FORCE";
                }

                forceNode = trvUnitTree.TopNode;
                //forceNode = trvUnitTree.Nodes[0];

                foreach (XElement formation in force.Descendants("FORMATION"))
                {
                    if (force.Attribute("NAME").Value != null)
                    {
                        formationNode = forceNode.Nodes.Add(formation.Attribute("NAME").Value);
                        formationNode.Tag = formation.Attribute("ID").Value;
                        formationNode.Name = "FORMATION";

                        //CHANGE TREENODE COLOR IF FORMATION IS STATIC
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
                        //^^^

                        foreach (XElement equip in unit.Descendants("EQUIPMENT"))
                        {
                            unitNode.Tag = unit.Attribute("ID").Value;
                            TreeNode equipTnode = unitNode.Nodes.Add(equip.Attribute("NAME").Value + " x" + equip.Attribute("NUMBER").Value + " [" + equip.Attribute("MAX").Value + "]");
                            equipTnode.Tag = equip.Attribute("ID").Value;
                            equipTnode.Name = "EQUIPMENT";
                        }
                    }
                }
            }
            ////trvUnitTree.Nodes[0].Expand();
            if (trvUnitTree.TopNode != null)
            {
                trvUnitTree.TopNode.Expand();
            }
            //trvUnitTree.Refresh();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            ssMainProgress.Visible = true;

            this.txtType.Text = trvUnitTree.SelectedNode.Name.ToString();
            string type = this.txtType.Text;
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string equipid = trvUnitTree.SelectedNode.Tag.ToString();

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            switch (type)
            {
                case "FORCE":
                    //PATH FOR OOB PORTION OF XML FILE
                    string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]";
                    var unit = xelem.XPathSelectElement(xpath);

                    //PATH FOR HEADER PORTION OF XML FILE
                    string xpathheader = "HEADER";
                    var header = xelem.XPathSelectElement(xpathheader);

                    //PATH FOR FORCE VARIABLES PORTION OF XML FILE
                    string xpathforcevariables = "FORCEVARIABLES/FORCE[@ID =" + Globals.GlobalVariables.FORCE + "]";
                    var forcevariables = xelem.XPathSelectElement(xpathforcevariables);

                    //COPY TEXT BOX VALUES TO XML
                    //FOR OOB PORTION
                    unit.Attribute("NAME").Value = this.txtUnitName.Text;
                    unit.Attribute("proficiency").Value = this.txtProficiency.Text;
                    unit.Attribute("supply").Value = this.txtSupply.Text;

                    //FOR FORCE VARIABLES PORTION
                    forcevariables.Attribute("globalRecce").Value = txtRecon.Text;
                    forcevariables.Attribute("globalGuerillas").Value = txtGuerilla.Text;
                    forcevariables.Attribute("interdiction").Value = txtInterdiction.Text;
                    forcevariables.Attribute("globalRailRepair").Value = txtRailRepair.Text;
                    forcevariables.Attribute("globalRailDestruction").Value = txtRailDestr.Text;
                    forcevariables.Attribute("ZOCCost").Value = txtZOCCost.Text;
                    forcevariables.Attribute("roadSupplyRadius").Value = txtRoadSupply.Text;
                    forcevariables.Attribute("extendedSupply").Value = txtExtSupply.Text;
                    forcevariables.Attribute("forceNightProficiency").Value = txtNightProf.Text;
                    forcevariables.Attribute("forcePestilence").Value = txtPestilence.Text;
                    forcevariables.Attribute("forceCommunication").Value = txtCommunications.Text;
                    forcevariables.Attribute("forceLossIntolerance").Value = txtLossToler.Text;
                    forcevariables.Attribute("reconstitutionPointX").Value = txtReconPtX.Text;
                    forcevariables.Attribute("reconstitutionPointY").Value = txtReconPtY.Text;
                    forcevariables.Attribute("globalRailcapInitial").Value = txtInitRailCap.Text;
                    forcevariables.Attribute("globalRailcapCurrent").Value = txtCurrRailCap.Text;
                    forcevariables.Attribute("globalSeacapInitial").Value = txtInitSeaCap.Text;
                    forcevariables.Attribute("globalSeacapCurrent").Value = txtCurrSeaCap.Text;
                    forcevariables.Attribute("globalAircapInitial").Value = txtInitAirCap.Text;
                    forcevariables.Attribute("globalAircapCurrent").Value = txtCurrAirCap.Text;
                    forcevariables.Attribute("currentTrack").Value = txtCurrTrack.Text;
                    forcevariables.Attribute("microUnitIcon").Value = cboMicroIcon.SelectedValue.ToString();
                    forcevariables.Attribute("externalPOBias").Value = cboStratBias.SelectedValue.ToString();
                    forcevariables.Attribute("forcePGWMultiplier").Value = txtPGWMult.Text;
                    forcevariables.Attribute("forceAirRefuel").Value = txtAirRefuel.Text;
                    forcevariables.Attribute("RFCScalar").Value = txtRFCScal.Text;
                    forcevariables.Attribute("navalCriticalScalar").Value = txtNavCritScal.Text;
                    forcevariables.Attribute("forceElectronicSupport").Value = txtElectSupp.Text;
                    forcevariables.Attribute("newReinforcements").Value = txtReinforcements.Text;
                    forcevariables.Attribute("newReinforcements").Value = txtReinforcements.Text;
                    forcevariables.Attribute("chemicalsAvailable").Value = txtChemAvail.Text;
                    forcevariables.Attribute("chemicalsUsed").Value = txtChemUsed.Text;
                    forcevariables.Attribute("nukesAvailableInitial").Value = txtNukeAvailInit.Text;
                    forcevariables.Attribute("nukesAvailableCurrent").Value = txtNukesAvailCurr.Text;
                    forcevariables.Attribute("nukesUsed").Value = txtNukesUsed.Text;
                    forcevariables.Attribute("forceNBCReadiness").Value = txtNBCReadiness.Text;
                    forcevariables.Attribute("victoryModifications").Value = txtVictoryMod.Text;
                    forcevariables.Attribute("globalHandicap").Value = txtVictoryMod.Text;
                    forcevariables.Attribute("forceMoveBias").Value = txtMoveBias.Text;
                    forcevariables.Attribute("globalAirHandicap").Value = txtAirHandicap.Text;
                    forcevariables.Attribute("reconstitutionPointValue").Value = txtReconPtValue.Text;

                    //CHECK THAT MICROICON COLOR IS NOT SAME AS OTHER FORCE
                    trvUnitTree.SelectedNode.Text = this.txtUnitName.Text;
                    if (Globals.GlobalVariables.FORCE == "1")
                    {
                        rbForce1.Text = this.txtUnitName.Text;
                        header.Attribute("forceName1").Value = this.txtUnitName.Text;
                    }
                    else
                    {
                        rbForce2.Text = this.txtUnitName.Text;
                        header.Attribute("forceName2").Value = this.txtUnitName.Text;
                    }

                    string microIcon;
                    if (Globals.GlobalVariables.FORCE == "1")
                    {
                        microIcon = "2";
                    }
                    else
                    {
                        microIcon = "1";
                    }
                    string xpathmicroicon = "FORCEVARIABLES/FORCE[@ID =" + microIcon + "]";
                    var microicons = xelem.XPathSelectElement(xpathmicroicon);

                    //TO CHECK THAT BOTH MICROICON COLORS ARE NOT THE SAME
                    if (forcevariables.Attribute("microUnitIcon").ToString() == microicons.Attribute("microUnitIcon").ToString())
                    {
                        MessageBox.Show("Force MicroIcon color cannot be same color as other force's MicroIcon color.  Please select new MicroIcon color.", "Select New Color", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        cboMicroIcon.SelectedValue = Globals.GlobalVariables.MICROICON;
                        forcevariables.Attribute("microUnitIcon").Value = cboMicroIcon.SelectedValue.ToString();
                    }

                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);

                    trvUnitTree.Update();
                    trvUnitTree.Focus();

                    break;

                case "FORMATION":
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + formid + "]";
                    unit = xelem.XPathSelectElement(xpath);

                    unit.Attribute("NAME").Value = this.txtUnitName.Text;
                    unit.Attribute("PROFICIENCY").Value = this.txtProficiency.Text;
                    unit.Attribute("SUPPLY").Value = this.txtSupply.Text;
                    unit.Attribute("SUPPORTSCOPE").Value = cboSupportScope.Text;
                    unit.Attribute("ORDERS").Value = this.cboOrders.Text;
                    unit.Attribute("EMPHASIS").Value = this.cboEmphasis.Text;

                    if (cboOrders.Text == "Static")
                    {
                        if (unit.Attribute("ENTRYTURN") != null)
                        {
                            unit.Attribute("ENTRYTURN").Value = this.txtEntryTurn.Text;
                        }
                        else
                        {
                            unit.Add(new XAttribute("ENTRYTURN", this.txtEntryTurn.Text));
                        }
                    }
                    else
                    {
                        if (unit.Attribute("ENTRYTURN") != null)
                        {
                            XAttribute attET = unit.Attribute("ENTRYTURN");
                            attET.Remove();
                        }
                    }

                    trvUnitTree.SelectedNode.Text = this.txtUnitName.Text;

                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvUnitTree.Focus();
                    break;

                case "UNIT":
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                    unit = xelem.XPathSelectElement(xpath);

                    unit.Attribute("NAME").Value = this.txtUnitName.Text;
                    unit.Attribute("PROFICIENCY").Value = this.txtProficiency.Text;
                    unit.Attribute("SUPPLY").Value = this.txtSupply.Text;
                    unit.Attribute("READINESS").Value = this.txtReadiness.Text;
                    unit.Attribute("EXPERIENCE").Value = this.cboExperience.Text;
                    unit.Attribute("SIZE").Value = this.cboSize.Text;
                    unit.Attribute("EMPHASIS").Value = this.cboEmphasis.Text;
                    unit.Attribute("STATUS").Value = this.cboDeployment.SelectedValue.ToString();
                    unit.Attribute("REPLACEMENTPRIORITY").Value = this.cboReplacements.SelectedValue.ToString();

                    string deploy = cboDeployment.SelectedValue.ToString();
                    string XCoord = "1"; //PLACEHOLDER VALUE
                    string YCoord = "1"; //PLACEHOLDER VALUE

                    switch (deploy)
                    {
                        case "1":  //SAVE ENTRY LOCATION FOR UNITS WHICH ARE REINFORCEMENTS BY TURN
                                   //SAVE X AND Y, AFTER CHECKING FOR NULL

                            //THIS CODE DEALS WITH FACT THAT ON-MAP UNITS USE "X/Y" ATTRIBUTES, WHILE OFF-MAP UNITS USE "GOINGTOX/Y" ATTRIBUTES.  ALSO CHECKS THAT USE OF DEFAULT VALUES IS INTENDED
                            if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                            {
                                if (txtX.Text == "1")
                                {
                                    DialogResult dialog = MessageBox.Show("X Coordinate is the default X value (1).  Is this correct?", "X Coordinate Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        XCoord = txtX.Text;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    XCoord = txtX.Text;
                                }

                                if (txtY.Text == "1")
                                {
                                    DialogResult dialog = MessageBox.Show("Y Coordinate is the default Y value (1).  Is this correct?", "Y Coordinate Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        YCoord = txtY.Text;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    YCoord = txtY.Text;
                                }

                                unit.Add(new XAttribute("GOINGTOX", XCoord));
                                unit.Add(new XAttribute("GOINGTOY", YCoord));
                                unit.Add(new XAttribute("ENTRY", "998"));
                            }

                            if (unit.Attribute("GOINGTOX") == null)
                            {
                                if (txtX.Text == "1")
                                {
                                    DialogResult dialog = MessageBox.Show("X Coordinate is the default X value (1).  Is this correct?", "X Coordinate Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        XCoord = txtX.Text;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    XCoord = txtX.Text;
                                }

                                if (txtY.Text == "1")
                                {
                                    DialogResult dialog = MessageBox.Show("Y Coordinate is the default Y value (1).  Is this correct?", "Y Coordinate Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        YCoord = txtY.Text;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                else
                                {
                                    YCoord = txtY.Text;
                                }

                                unit.Add(new XAttribute("GOINGTOX", XCoord));
                                unit.Add(new XAttribute("GOINGTOY", YCoord));
                                unit.Add(new XAttribute("ENTRY", "998"));
                                XAttribute attx = unit.Attribute("X");
                                XAttribute atty = unit.Attribute("Y");
                                attx.Remove();
                                atty.Remove();
                            }
                            else
                            {
                                unit.Attribute("GOINGTOX").Value = txtX.Text;
                                unit.Attribute("GOINGTOY").Value = txtY.Text;
                            }

                            //SET ENTRY TURN
                            if (txtReinforceTrigger.Text == "998")
                            {
                                DialogResult dialog = MessageBox.Show("Reinforcement entry turn is the default turn value (998).  Is this correct?", "Reinforcement Entry Turn Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                if (dialog == DialogResult.Yes)
                                {
                                    unit.Attribute("ENTRY").Value = txtReinforceTrigger.Text;
                                }
                                else
                                {
                                    //break;
                                    return;
                                }
                            }
                            else
                            {
                                unit.Attribute("ENTRY").Value = txtReinforceTrigger.Text;
                            }
                            break;

                        case "2": //SAVE ENTRY LOCATION FOR UNITS WHICH ARE REINFORCEMENTS BY EVENT
                                  //SET X AND Y, AFTER CHECKING FOR NULL

                            //THIS CODE DEALS WITH FACT THAT ON-MAP UNITS USE "X/Y" ATTRIBUTES, WHILE OFF-MAP UNITS USE "GOINGTOX/Y" ATTRIBUTES.  ALSO CHECKS THAT USE OF DEFAULT VALUES IS INTENDED
                            if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                            {
                                if (txtX.Text == "1")
                                {
                                    DialogResult dialog = MessageBox.Show("X Coordinate is the default X value (1).  Is this correct?", "X Coordinate Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        XCoord = txtX.Text;
                                    }
                                    else
                                    {
                                        //break;
                                        return;
                                    }
                                }
                                else
                                {
                                    XCoord = txtX.Text;
                                }

                                if (txtY.Text == "1")
                                {
                                    DialogResult dialog = MessageBox.Show("Y Coordinate is the default Y value (1).  Is this correct?", "Y Coordinate Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        YCoord = txtY.Text;
                                    }
                                    else
                                    {
                                        //break;
                                        return;
                                    }
                                }
                                else
                                {
                                    YCoord = txtY.Text;
                                }

                                unit.Add(new XAttribute("GOINGTOX", XCoord));
                                unit.Add(new XAttribute("GOINGTOY", YCoord));
                                unit.Add(new XAttribute("ENTRY", "999"));
                            }

                            if (unit.Attribute("GOINGTOX") == null)
                            {
                                if (txtX.Text == "1")
                                {
                                    DialogResult dialog = MessageBox.Show("X Coordinate is the default X value (1).  Is this correct?", "X Coordinate Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        XCoord = txtX.Text;
                                    }
                                    else
                                    {
                                        //break;
                                        return;
                                    }
                                }
                                else
                                {
                                    XCoord = txtX.Text;
                                }

                                if (txtY.Text == "1")
                                {
                                    DialogResult dialog = MessageBox.Show("Y Coordinate is the default Y value (1).  Is this correct?", "Y Coordinate Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                    if (dialog == DialogResult.Yes)
                                    {
                                        YCoord = txtY.Text;
                                    }
                                    else
                                    {
                                        //break;
                                        return;
                                    }
                                }
                                else
                                {
                                    YCoord = txtY.Text;
                                }

                                unit.Add(new XAttribute("GOINGTOX", XCoord));
                                unit.Add(new XAttribute("GOINGTOY", YCoord));
                                unit.Add(new XAttribute("ENTRY", "999"));
                                XAttribute attx = unit.Attribute("X");
                                XAttribute atty = unit.Attribute("Y");
                                attx.Remove();
                                atty.Remove();
                            }
                            else
                            {
                                unit.Attribute("GOINGTOX").Value = txtX.Text;
                                unit.Attribute("GOINGTOY").Value = txtY.Text;
                            }

                            if (txtReinforceTrigger.Text == "999")
                            {
                                DialogResult dialog = MessageBox.Show("Reinforcement entry event is the default event value (999).  Is this correct?", "Reinforcement Entry Event Correct?", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
                                if (dialog == DialogResult.Yes)
                                {
                                    unit.Attribute("ENTRY").Value = txtReinforceTrigger.Text;
                                }
                                else
                                {
                                    //break;
                                    return;
                                }
                            }
                            else
                            {
                                unit.Attribute("ENTRY").Value = txtReinforceTrigger.Text;
                            }

                            break;

                        case "24": //SET NO LOCATION FOR DIVIDED UNITS
                            txtX.Text = "--";
                            txtY.Text = "--";
                            break;

                        default: //SET LOCATION FOR ON-MAP UNITS
                            lblReinforce.Visible = false;
                            lblReinforce2.Visible = false;
                            txtReinforceTrigger.Visible = false;

                            if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                            {
                                unit.Add(new XAttribute("X", "--"));
                                unit.Add(new XAttribute("Y", "--"));
                            }

                            if (unit.Attribute("X") == null)
                            {
                                unit.Add(new XAttribute("X", "--"));
                                unit.Add(new XAttribute("Y", "--"));
                                XAttribute attgotox = unit.Attribute("GOINGTOX");
                                XAttribute attgotoy = unit.Attribute("GOINGTOY");
                                XAttribute entry = unit.Attribute("ENTRY");
                                attgotox.Remove();
                                attgotoy.Remove();
                                if (entry != null)
                                {
                                    entry.Remove();
                                }
                            }

                            if (txtX.Text != null)
                            {
                                unit.Attribute("X").Value = txtX.Text;
                            }

                            if (txtY.Text != null)
                            {
                                unit.Attribute("Y").Value = txtY.Text;
                            }
                            break;
                    }

                    trvUnitTree.SelectedNode.Text = this.txtUnitName.Text;

                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvUnitTree.Focus();
                    break;

                case "EQUIPMENT":
                    string parentunitid = trvUnitTree.SelectedNode.Parent.Tag.ToString();
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + parentunitid + "]/EQUIPMENT[@ID =" + equipid + "]";
                    unit = xelem.XPathSelectElement(xpath);

                    unit.Attribute("NUMBER").Value = this.txtNumber.Text;
                    unit.Attribute("MAX").Value = this.txtMax.Text;
                    unit.Attribute("DAMAGE").Value = this.txtDamage.Text;

                    trvUnitTree.Refresh();
                    trvUnitTree.SelectedNode.Text = this.txtUnitName.Text + " x" + txtNumber.Text + " [" + txtMax.Text + "]";
                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvUnitTree.Focus();
                    break;
            }

            //SAVE CHANGES FOR TRANSFERRED TREE NODES
            if (Globals.GlobalVariables.TREEVIEWCHANGED == true)
            {
                string xpathFormation = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + Globals.GlobalVariables.TARGETTAG + "]";
                string xpathUnit = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.DRAGGEDTAG + "]";

                var transferred = xelem.XPathSelectElement(xpathUnit);
                var target = xelem.XPathSelectElement(xpathFormation);

                transferred.Remove();
                target.Add(transferred);
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                btnSave.Enabled = false;
            }
            await Task.Delay(500);
            ssMainProgress.Visible = false;
        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .gam files *.gam|*.gam";
            if (file.ShowDialog() == DialogResult.OK)
            {
                //TOAWXML.Properties.Settings.Default.FilePath = file.FileName;
                //System.IO.File.WriteAllText("FilePath.txt", Globals.GlobalVariables.PATH);
                TOAWXML.Properties.Settings.Default.FilePath = file.FileName;
                TOAWXML.Properties.Settings.Default.Save();
                FixInvalidXML();

                xmlform_Load(null, EventArgs.Empty);
                trvUnitTree.Nodes.Clear();
                LoadTree();
            }
        }

        private void btnForceNext_Click(object sender, EventArgs e)
        {
            tabUnits.SelectedIndex = 5;
            btnForcePrev.Focus();
        }

        private void btnForcePrev_Click(object sender, EventArgs e)
        {
            tabUnits.SelectedIndex = 0;
            btnForceNext.Focus();
        }

        private void btnScenSettings_Click(object sender, EventArgs e)
        {
            frmScenSettings scenariosettingform = new frmScenSettings();
            scenariosettingform.Show();
        }

        private void trvUnitTree_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        private void trvUnitTree_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void trvUnitTree_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            int draggedLevel;
            int targetLevel = 0;

            // Retrieve the client coordinates of the drop location.
            Point targetPoint = trvUnitTree.PointToClient(new Point(e.X, e.Y));

            // Retrieve the node at the drop location, get its TreeNode Level and Tag
            TreeNode targetNode = trvUnitTree.GetNodeAt(targetPoint);

            if (targetNode != null)
            {
                targetLevel = targetNode.Level;
                Globals.GlobalVariables.TARGETTAG = targetNode.Tag.ToString();
            }

            // Retrieve the node that was dragged, get its TreeNode Level and Tag
            TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
            draggedLevel = draggedNode.Level;
            TreeNode draggedParentNode = draggedNode.Parent;
            string strdraggedParentID = draggedParentNode.Tag.ToString();
            Globals.GlobalVariables.DRAGGEDTAG = draggedNode.Tag.ToString();

            // Sanity check
            if (draggedNode == null)
            {
                return;
            }

            // Did the user drop on a valid target node?
            if (targetNode == null)
            {
                //// The user dropped the node on the treeview control instead
                //// of another node so lets place the node at the bottom of the tree.
                //draggedNode.Remove();
                //trvUnitTree.Nodes.Add(draggedNode);
                //draggedNode.Expand();
            }
            else
            {
                TreeNode parentNode = targetNode;
                bool blnparentAsTarget = (targetNode == draggedParentNode);
                bool blnsameLevels = (draggedLevel == (targetLevel + 1));

                // Confirm that the node at the drop location is (i) not 
                // the dragged node; (ii) that target node isn't null
                //(for example if you drag outside the control); and 
                //(iii) unit "levels" are not being changed (ie, a unit 
                //can only be dragged to another formation.

                if (!draggedNode.Equals(targetNode) && targetNode != null && blnsameLevels == true && blnparentAsTarget == false)
                {
                    bool canDrop = true;

                    // Crawl our way up from the node we dropped on to find out if
                    // if the target node is our parent. 
                    while (canDrop && (parentNode != null))
                    {
                        canDrop = !Object.ReferenceEquals(draggedNode, parentNode);
                        parentNode = parentNode.Parent;
                    }

                    // Is this a valid drop location?
                    if (canDrop)
                    {
                        // Yes. Move the node, expand it, and select it.
                        draggedNode.Remove();
                        targetNode.Nodes.Add(draggedNode);
                        targetNode.Expand();
                        Globals.GlobalVariables.TREEVIEWCHANGED = true;

                        string xpathFormation;
                        string xpathUnit;

                        if (draggedLevel == 2)  //UNIT BEING TRANSFERRED TO DIFFERENT FORMATION
                        {
                            xpathFormation = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + Globals.GlobalVariables.TARGETTAG + "]";
                            xpathUnit = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.DRAGGEDTAG + "]";

                        }
                        else //EQUIPMENT BEING TRANSFERRED TO DIFFERENT UNIT (draggedLevel == 3)
                        {
                            xpathFormation = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.TARGETTAG + "]";
                            xpathUnit = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + strdraggedParentID + "]/EQUIPMENT[@ID =" + Globals.GlobalVariables.DRAGGEDTAG + "]";
                        }
                        XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                        var target = xelem.XPathSelectElement(xpathFormation);
                        var transferred = xelem.XPathSelectElement(xpathUnit);

                        //IF EQUIP IS BEING TRANSFERRED, GET HIGHEST ID OF EXISTING EQUIPMENT ELEMENTS, ADD 1 FOR NEW  EQUIP ELEMENT, CHANGE ID IN XML
                        if (draggedLevel == 3)
                        {
                            Globals.GlobalVariables.DRAGGEDPARENTID = strdraggedParentID;
                            var maxid = target.Descendants("EQUIPMENT").Attributes("ID").ToList();
                            int oldMax = target.Descendants("EQUIPMENT").Max(m => (int)m.Attribute("ID"));
                            int newMax;
                            newMax = oldMax + 1;
                            transferred.Attribute("ID").Value = newMax.ToString();
                            draggedNode.Tag = newMax.ToString();
                        }
                        transferred.Remove();
                        target.Add(transferred);
                        xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                        Globals.GlobalVariables.TREEVIEWCHANGED = false;
                        trvUnitTree.Refresh();
                    }
                }
            }
            //Select the dropped node and navigate (however you do it)
            trvUnitTree.SelectedNode = draggedNode;
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node;
            node = trvUnitTree.SelectedNode;
            TreeNode tparent = node.Parent;

            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string parentid = trvUnitTree.SelectedNode.Parent.Tag.ToString();
            int unitLevel = node.Level;
            string xpath = "";

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            switch (unitLevel)
            {
                case 1:
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + unitid + "]";
                    break;
                case 2:
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                    break;
                case 3:
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + parentid + "]/EQUIPMENT[@ID =" + unitid + "]";
                    break;
            }

            var unit = xelem.XPathSelectElement(xpath);

            if (tparent != null)
            {
                int index = tparent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    tparent.Nodes.RemoveAt(index);
                    tparent.Nodes.Insert(index - 1, node);
                    // bw : add this line to restore the originally selected node as selected
                    node.TreeView.SelectedNode = node;

                    var xparent = unit.Parent;
                    var previousNode = unit.PreviousNode;
                    previousNode.AddBeforeSelf(unit);
                    unit.Remove();  // see note below
                }
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            }
        }

        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode node;
            node = trvUnitTree.SelectedNode;
            TreeNode tparent = node.Parent;
            int unitLevel = node.Level;

            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string parentid = trvUnitTree.SelectedNode.Parent.Tag.ToString();
            string xpath = "";

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            switch (unitLevel)
            {
                case 1:
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + unitid + "]";
                    break;
                case 2:
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                    break;
                case 3:
                    xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + parentid + "]/EQUIPMENT[@ID =" + unitid + "]";
                    break;
            }
            var unit = xelem.XPathSelectElement(xpath);

            if (tparent != null)
            {
                int index = tparent.Nodes.IndexOf(node);
                if (index < tparent.Nodes.Count - 1)
                {
                    tparent.Nodes.RemoveAt(index);
                    tparent.Nodes.Insert(index + 1, node);
                    // bw : add this line to restore the originally selected node as selected
                    node.TreeView.SelectedNode = node;

                    var xparent = unit.Parent;
                    var nextNode = unit.NextNode;
                    nextNode.AddAfterSelf(unit);
                    unit.Remove();
                }
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you SURE you want to delete this formation?" + Environment.NewLine + Environment.NewLine + "This will also delete all of its subordinate units.",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                TreeNode tnode;
                tnode = trvUnitTree.SelectedNode;
                int unitLevel = tnode.Level;
                string formationid = tnode.Tag.ToString();
                string parentid = tnode.Parent.Tag.ToString();
                XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

                string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + formationid + "]";
                string xpath2 = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION";
                string xpath3 = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT";
                string xpath4 = "EVENTS/EVENT[@EFFECT=\"Form'n orders\"]";
                string xpath5 = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + formationid + "]/UNIT";

                //RENUMBER FORMATIONS/UNITS IF ONE IS REMOVED
                var units = xelem.XPathSelectElements(xpath2);  //ALL FORMATIONS
                var allunits = xelem.XPathSelectElements(xpath3); //ALL UNITS
                var formations = xelem.XPathSelectElements(xpath4); //ALL EVENTS FEATURING FORMATIONS
                var subunits = xelem.XPathSelectElements(xpath5); //ALL FORMATION'S SUBUNITS
                var unitevents = (from u in xelem.Elements("EVENTS").Elements("EVENT")  //ALL EVENTS FEATURING UNITS
                                  where (string)u.Attribute("TRIGGER") == "Unit destroyed" ||
                                  (string)u.Attribute("EFFECT") == "Disband unit" ||
                                  (string)u.Attribute("EFFECT") == "Withdraw unit" ||
                                  (string)u.Attribute("EFFECT") == "Withdraw army"
                                  select u);

                string forceid = "";

                if (rbForce1.Checked == true)
                {
                    forceid = "1";
                }

                if (rbForce2.Checked == true)
                {
                    forceid = "2";
                }

                //AFTER CONFIRMATION, RENUMBER FORMATIONS/UNITS USED IN EVENTS
                bool proceed = Renumbering.RenumberEventFormations(formations, formationid, forceid); //RENUMBER FORMATIONS USED IN EVENTS
                bool proceed2 = Renumbering.RenumberEventFormationUnits(subunits, unitevents, formationid, forceid);  //RENUMBER UNITS USED IN EVENTS AFTER FORMATION DELETED

                if (proceed == false || proceed2 == false)
                {
                    return;
                };

                //RENUMBER PARENT ATTRIBUTE OF DIVIDED SUBUNITS
                var unitparents = from p in xelem.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT")
                                  where (string)p.Parent.Parent.Attribute("ID") == "1" && p.Attribute("PARENT") != null
                                  select p;

                Renumbering.RenumberDividedFormation(subunits, unitparents);

                //REMOVE TREE AND XML NODES FOR DELETED FORMATION/UNIT
                var unit = xelem.XPathSelectElement(xpath);
                trvUnitTree.BeginUpdate();
                tnode.Remove();
                unit.Remove();

                Renumbering.RenumberAll(units);  //RENUMBER ALL FORMATIONS
                Renumbering.RenumberAll(allunits);  //RENUMBER ALL UNITS AFTER FORMATION DELETED

                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                trvUnitTree.EndUpdate();
                LoadTree();
                trvUnitTree.Refresh();
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int copiedLevel;
            int oldMax = 1;
            int oldchildMax = 1;
            int newMax = 1;
            int newchildMax = 1;
            //int next = 1;
            string xpathParent = "parent"; //FILLER CONTENT
            string xpathCopied = "copied"; //FILLER CONTENT
            string strunitname = "unitname";

            //COPY IN TREEVIEW
            TreeNode copiedNode = trvUnitTree.SelectedNode;
            copiedLevel = copiedNode.Level;

            TreeNode parentNode = copiedNode.Parent;
            string strParentID = parentNode.Tag.ToString();
            string strcopiedText = copiedNode.Text;
            Globals.GlobalVariables.COPIEDID = copiedNode.Tag.ToString();
            Globals.GlobalVariables.COPIEDPARENTID = strParentID;
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            switch (copiedLevel)
            {
                case 0:
                    // FORCE CANNOT BE COPIED, EXITS
                    break;

                case 1: //FORMATION BEING COPIED
                    xpathParent = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]";
                    xpathCopied = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + Globals.GlobalVariables.COPIEDID + "]";
                    var parent = xelem.XPathSelectElement(xpathParent);
                    var copied = xelem.XPathSelectElement(xpathCopied);
                    var maxid = parent.Descendants("FORMATION").Attributes("ID").ToList();
                    var maxchildid = parent.Descendants("FORMATION").Descendants("UNIT").Attributes("ID").ToList();
                    parentNode = copiedNode.Parent;

                    oldMax = parent.Descendants("FORMATION").Max(m => (int)m.Attribute("ID"));
                    oldchildMax = parent.Descendants("UNIT").Max(c => (int)c.Attribute("ID"));

                    ////ADD CLONED XMLNODE 
                    XElement cloneNode = new XElement(copied);
                    newMax = oldMax + 1;
                    newchildMax = oldchildMax + 1;
                    cloneNode.Attribute("ID").Value = newMax.ToString();
                    cloneNode.Attribute("NAME").Value = "Copy of " + copied.Attribute("NAME").Value.ToString();
                    copied.AddAfterSelf(cloneNode);

                    //INSERT UNIQUE IDs FOR UNITS IN CLONED FORMATION IN XML
                    foreach (XElement xe in cloneNode.Elements("UNIT"))
                    {
                        strunitname = xe.Attribute("NAME").Value.ToString();
                        xe.Attribute("NAME").Value = "Copy of " + strunitname;
                        xe.Attribute("ID").Value = newchildMax.ToString();
                        newchildMax = newchildMax + 1;
                    }

                    //SHOW CLONED FORMATION IN TREEVIEW
                    TreeNode copiedClone = (TreeNode)trvUnitTree.SelectedNode.Clone();
                    copiedClone.Text = "Copy of " + strcopiedText;
                    copiedClone.Tag = newMax.ToString();
                    parentNode.Nodes.Insert(copiedNode.Index + 1, copiedClone);

                    newchildMax = oldchildMax + 1;

                    //INSERT UNIQUE IDs FOR UNITS IN CLONED FORMATION IN TREEVIEW
                    foreach (TreeNode childNode in copiedClone.Nodes)
                    {
                        strunitname = childNode.Text;
                        childNode.Text = "Copy of " + strunitname;
                        childNode.Tag = newchildMax.ToString();
                        newchildMax = newchildMax + 1;
                    }

                    //DELETE NEXT ATTRIBUTES FOR ALL UNITS IN COPIED FORMATIONS
                    string xpathCopyForm = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[ID=" + cloneNode.Attribute("ID").Value.ToString() + "]/UNIT";
                    var cloneform = (from f in xelem.Descendants("UNIT")
                                     where f.Parent.Parent.Attribute("ID").Value.ToString() == Globals.GlobalVariables.FORCE &&
                                     f.Parent.Attribute("ID").Value.ToString() == cloneNode.Attribute("ID").Value.ToString()
                                     select f);

                    foreach (var u in cloneform)
                    {
                        if (u.Attribute("NEXT") != null)
                        {
                            XAttribute attNext = u.Attribute("NEXT");
                            attNext.Remove();
                        }
                    }

                    break;

                case 2: //UNIT BEING COPIED 
                    xpathParent = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION";
                    xpathCopied = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.COPIEDID + "]";
                    parent = xelem.XPathSelectElement(xpathParent);
                    copied = xelem.XPathSelectElement(xpathCopied);
                    maxid = parent.Parent.Descendants("UNIT").Attributes("ID").ToList();
                    oldMax = parent.Parent.Descendants("UNIT").Max(m => (int)m.Attribute("ID"));

                    //ADD CLONED XMLNODE 
                    parentNode = copiedNode.Parent;
                    cloneNode = new XElement(copied);
                    newMax = oldMax + 1;
                    cloneNode.Attribute("ID").Value = newMax.ToString();
                    cloneNode.Attribute("NAME").Value = "Copy of " + copied.Attribute("NAME").Value.ToString();
                    copied.AddAfterSelf(cloneNode);

                    copiedClone = (TreeNode)trvUnitTree.SelectedNode.Clone();
                    copiedClone.Text = "Copy of " + strcopiedText;
                    copiedClone.Tag = newMax.ToString();
                    parentNode.Nodes.Insert(copiedNode.Index + 1, copiedClone);

                    //DELETE "NEXT" ATTRIBUTES
                    string xpathForceUnits = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT";
                    var forceunits = xelem.XPathSelectElements(xpathForceUnits);

                    foreach (XElement fu in forceunits)
                        if (fu.Attribute("NEXT") != null)
                        {
                            XAttribute attNext = fu.Attribute("NEXT");
                            attNext.Remove();
                        }

                    break;

                case 3: //EQUIPMENT BEING COPIED 
                    xpathParent = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.COPIEDPARENTID + "]";
                    xpathCopied = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID= " + Globals.GlobalVariables.COPIEDPARENTID + "]/EQUIPMENT[@ID =" + Globals.GlobalVariables.COPIEDID + "]";
                    parent = xelem.XPathSelectElement(xpathParent);
                    copied = xelem.XPathSelectElement(xpathCopied);
                    maxid = parent.Descendants("EQUIPMENT").Attributes("ID").ToList();
                    oldMax = parent.Descendants("EQUIPMENT").Max(m => (int)m.Attribute("ID"));

                    //ADD CLONED XMLNODE 
                    parentNode = copiedNode.Parent;
                    cloneNode = new XElement(copied);
                    newMax = oldMax + 1;
                    cloneNode.Attribute("ID").Value = newMax.ToString();
                    cloneNode.Attribute("NAME").Value = "Copy of " + copied.Attribute("NAME").Value.ToString();
                    copied.AddAfterSelf(cloneNode);

                    copiedClone = (TreeNode)trvUnitTree.SelectedNode.Clone();
                    copiedClone.Text = "Copy of " + strcopiedText;
                    copiedClone.Tag = newMax.ToString();
                    parentNode.Nodes.Insert(copiedNode.Index + 1, copiedClone);

                    break;
            }

            parentNode.Expand();
            Globals.GlobalVariables.TREEVIEWCHANGED = true;

            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            Globals.GlobalVariables.TREEVIEWCHANGED = false;
            trvUnitTree.Refresh();

            //Select the dropped node and navigate (however you do it)
            trvUnitTree.SelectedNode = copiedNode;

        }

        private void trvUnitTree_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void formationPropagationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Globals.GlobalVariables.PROFIC = txtProficiency.Text;
            Globals.GlobalVariables.SUPPLY = txtSupply.Text;
            Globals.GlobalVariables.LOSSTOL = cboEmphasis.Text;
            Globals.GlobalVariables.SUPPSCOPE = cboSupportScope.Text;
            Globals.GlobalVariables.ORDERS = cboOrders.Text;
            Globals.GlobalVariables.ID = trvUnitTree.SelectedNode.Tag.ToString();

            frmPropagateForm propagateformationform = new frmPropagateForm();
            propagateformationform.ShowDialog();
        }

        private void unitPropagationToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void cboDeployment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string deploy = cboDeployment.SelectedValue.ToString();
            TreeNode selectedTNode = trvUnitTree.SelectedNode;
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = xelem.XPathSelectElement(xpath);

            switch (deploy)
            {
                case "1":  //SET CONTROL VISIBILITY IF DEPLOY IS REINFORCEMENTS BY TURN
                    lblReinforce.Visible = true;
                    lblReinforce2.Visible = true;
                    txtReinforceTrigger.Visible = true;
                    txtReinforceTrigger.Enabled = true;
                    lblReinforce.Text = "Turn";
                    trvUnitTree.SelectedNode.ForeColor = System.Drawing.Color.ForestGreen;
                    Font font = new Font(trvUnitTree.Font, FontStyle.Bold);
                    trvUnitTree.SelectedNode.NodeFont = font;

                    selectedTNode.Text = selectedTNode.Text;
                    trvUnitTree.Focus();

                    txtReinforceTrigger.Text = "998";
                    //lblReinfDate.Visible = true;

                    //SET XML ATTRIBUTES FOR REINFORCEMENTS (X & Y COORDS)
                    if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                    {
                        unit.Add(new XAttribute("GOINGTOX", "1"));
                        unit.Add(new XAttribute("GOINGTOY", "1"));
                    }

                    if (unit.Attribute("GOINGTOX") == null)
                    {
                        unit.Add(new XAttribute("GOINGTOX", "1"));
                        unit.Add(new XAttribute("GOINGTOY", "1"));
                        XAttribute attx2 = unit.Attribute("X");
                        XAttribute atty2 = unit.Attribute("Y");
                        attx2.Remove();
                        atty2.Remove();
                    }

                    break;

                case "2": //SET CONTROL VISIBILITY IF DEPLOY IS REINFORCEMENTS BY EVENT
                    lblReinforce.Visible = true;
                    lblReinforce2.Visible = true;
                    txtReinforceTrigger.Visible = true;
                    txtReinforceTrigger.Enabled = true;
                    lblReinforce.Text = "Event";

                    trvUnitTree.SelectedNode.ForeColor = System.Drawing.Color.ForestGreen;
                    Font font2 = new Font(trvUnitTree.Font, FontStyle.Bold);
                    trvUnitTree.SelectedNode.NodeFont = font2;
                    selectedTNode.Text = selectedTNode.Text;
                    trvUnitTree.Focus();
                    //lblReinfDate.Visible = false;

                    //SET XML ATTRIBUTES FOR REINFORCEMENTS BY EVENT (X & Y COORDS)
                    if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                    {
                        unit.Add(new XAttribute("GOINGTOX", "1"));
                        unit.Add(new XAttribute("GOINGTOY", "1"));
                    }

                    if (unit.Attribute("GOINGTOX") == null)
                    {
                        unit.Add(new XAttribute("GOINGTOX", "1"));
                        unit.Add(new XAttribute("GOINGTOY", "1"));
                        XAttribute attx2 = unit.Attribute("X");
                        XAttribute atty2 = unit.Attribute("Y");
                        attx2.Remove();
                        atty2.Remove();
                    }
                    break;

                case "24": //SET CONTROL VISIBILITY FOR DIVIDED UNIT
                    lblReinforce.Visible = false;
                    lblReinforce2.Visible = false;
                    txtReinforceTrigger.Visible = false;
                    cboDeployment.SelectedIndex = Globals.GlobalVariables.PREVCBODEPLOYINDEX;
                    cboDeployment.Refresh();
                    tssLabel1.Text = "";
                    //lblReinfDate.Visible = false;

                    var f = new frmUnitDivide(this);
                    f.Show();

                    break;

                default: //SET CONTROL VISIBILITY FOR ON-MAP UNITS
                    lblReinforce.Visible = false;
                    lblReinforce2.Visible = false;
                    txtReinforceTrigger.Visible = false;
                    tssLabel1.Text = "";
                    //lblReinfDate.Visible = false;

                    trvUnitTree.SelectedNode.ForeColor = System.Drawing.Color.Black;
                    Font font4 = new Font(trvUnitTree.Font, FontStyle.Regular);
                    trvUnitTree.SelectedNode.NodeFont = font4;
                    selectedTNode.Text = selectedTNode.Text;
                    trvUnitTree.Focus();

                    //SET XML ATTRIBUTES FOR ON-MAP UNITS
                    if (unit.Attribute("GOINGTOX") == null && unit.Attribute("X") == null)
                    {
                        unit.Add(new XAttribute("X", "--"));
                        unit.Add(new XAttribute("Y", "--"));
                    }

                    if (unit.Attribute("X") == null)
                    {
                        unit.Add(new XAttribute("X", "--"));
                        unit.Add(new XAttribute("Y", "--"));
                        XAttribute attgotox = unit.Attribute("GOINGTOX");
                        XAttribute attgotoy = unit.Attribute("GOINGTOY");
                        attgotox.Remove();
                        attgotoy.Remove();
                    }
                    break;
            }
        }

        private void btnDepots_Click(object sender, EventArgs e)
        {
            frmDepots depotform = new frmDepots();
            depotform.ShowDialog();
        }

        private void cboTrack_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string formid = trvUnitTree.SelectedNode.Tag.ToString();
            string strTrack = cboTrack.SelectedValue.ToString();

            dgvObjectives.Columns[0].Name = "ID";
            dgvObjectives.Columns["ID"].DataPropertyName = "ID";
            dgvObjectives.Columns[1].Name = "DESCRIPTION";
            dgvObjectives.Columns["DESCRIPTION"].DataPropertyName = "DESCRIPTION";
            dgvObjectives.Columns[2].Name = "X";
            dgvObjectives.Columns["X"].DataPropertyName = "X";
            dgvObjectives.Columns[3].Name = "Y";
            dgvObjectives.Columns["Y"].DataPropertyName = "Y";

            dgvObjectives.Columns[0].ReadOnly = true;

            dgvObjectives.Columns[0].Width = 25;
            dgvObjectives.Columns[1].Width = 165;
            dgvObjectives.Columns[2].Width = 30;
            dgvObjectives.Columns[3].Width = 30;
            dgvObjectives.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvObjectives.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvObjectives.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvObjectives.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvObjectives.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("X", typeof(Int32));
            dt.Columns.Add("Y", typeof(Int32));
            XDocument xdoc = XDocument.Load(TOAWXML.Properties.Settings.Default.FilePath);

            var objectives = (from d in xdoc.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("OBJECTIVES").Descendants("OBJECTIVE")
                              where (string)d.Parent.Parent.Parent.Attribute("ID") == Globals.GlobalVariables.FORCE && (string)d.Parent.Parent.Attribute("ID") == formid && (string)d.Parent.Attribute("TRACK") == strTrack
                              select new
                              {
                                  ID = d.Attribute("ID").Value.ToString(),
                                  DESCRIPTION = d.Attribute("DESCRIPTION").Value,
                                  X = d.Attribute("X").Value.ToString(),
                                  Y = d.Attribute("Y").Value.ToString()
                              });
            objectives.ToList().ForEach(i => dt.Rows.Add(i.ID, i.DESCRIPTION, i.X, i.Y));
            dgvObjectives.DataSource = dt;
            ///END OF DGVOBJECTIVES BLOCK
        }

        private void btnSaveObj_Click(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpathObjectives;
            string strFormID = "1";
            string strTrack = "1";
            XElement xObjectives;
            strFormID = trvUnitTree.SelectedNode.Tag.ToString();
            strTrack = cboTrack.SelectedValue.ToString();

            foreach (DataGridViewRow row in dgvObjectives.Rows)
            {
                string strObjID = row.Cells["ID"].Value.ToString();
                string strDesc = row.Cells["DESCRIPTION"].Value.ToString();
                string strX = row.Cells["X"].Value.ToString();
                string strY = row.Cells["Y"].Value.ToString();

                xpathObjectives = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID=" + strFormID + "]/OBJECTIVES[@TRACK=" + strTrack + "]/OBJECTIVE[@ID=" + strObjID + "]";
                xObjectives = xelem.XPathSelectElement(xpathObjectives);
                xObjectives.Attribute("ID").Value = strObjID;
                xObjectives.Attribute("DESCRIPTION").Value = strDesc;
                xObjectives.Attribute("X").Value = strX;
                xObjectives.Attribute("Y").Value = strY;
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            }
        }

        private void dgvObjectives_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.CellStyle.ForeColor = Color.Gray;
            }
        }

        private void toolStripMenuObjDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvObjectives.SelectedRows)
            {
                string xpathObj;
                string strObjID;
                XElement xObjective;
                int rowindex;

                string strFormID = "1";
                string strTrack = "1";
                strFormID = trvUnitTree.SelectedNode.Tag.ToString();
                strTrack = cboTrack.SelectedValue.ToString();

                rowindex = dgvObjectives.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvObjectives.Rows[rowindex];
                strObjID = selectedRow.Cells["ID"].Value.ToString();

                XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                xpathObj = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID=" + strFormID + "]/OBJECTIVES[@TRACK=" + strTrack + "]/OBJECTIVE[@ID=" + strObjID + "]";
                xObjective = xelem.XPathSelectElement(xpathObj);

                dgvObjectives.Rows.RemoveAt(item.Index);
                xObjective.Remove();
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            }
        }

        private void addToolStripMenuObjAdd_Click(object sender, EventArgs e)
        {
            string xpathObj;
            string strObjID;
            IEnumerable<XElement> xObj;
            int rowindex;

            string strFormID = "1";
            string strTrack = "1";
            strFormID = trvUnitTree.SelectedNode.Tag.ToString();
            strTrack = cboTrack.SelectedValue.ToString();

            int objRows = dgvObjectives.Rows.Count;
            int newMax;

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            if (objRows > 0)
            {
                rowindex = dgvObjectives.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvObjectives.Rows[rowindex];
                strObjID = selectedRow.Cells["ID"].Value.ToString();
                xpathObj = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID=" + strFormID + "]/OBJECTIVES[@TRACK=" + strTrack + "]/OBJECTIVE[@ID=" + strObjID + "]";
                xObj = xelem.XPathSelectElements(xpathObj);

                //FIND HIGHEST ID NUMBER, ADD ONE TO NEW DEPOT'S ID
                int oldMax = xObj.Max(m => (int)m.Attribute("ID"));
                newMax = oldMax + 1;
            }
            else
            {
                newMax = 1;
            }

            //ADD NEW OBJ ROW TO DATAGRIDVIEW
            dgvObjectives.AllowUserToAddRows = true;
            dgvObjectives.Rows[dgvObjectives.RowCount - 1].Cells["ID"].Value = newMax.ToString();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("DESCRIPTION", typeof(string));
            dt.Columns.Add("X", typeof(int));
            dt.Columns.Add("Y", typeof(int));

            int i = 0;

            foreach (DataGridViewRow row in dgvObjectives.Rows)
            {
                dt.Rows.Add();

                if (i == dgvObjectives.Rows.Count - 1)
                {
                    dgvObjectives.Rows[i].Cells["DESCRIPTION"].Value = "Objective";
                    dgvObjectives.Rows[i].Cells["X"].Value = "1";
                    dgvObjectives.Rows[i].Cells["Y"].Value = "1";
                }

                dt.Rows[i][0] = dgvObjectives.Rows[i].Cells["ID"].Value;
                dt.Rows[i][1] = dgvObjectives.Rows[i].Cells["DESCRIPTION"].Value;
                dt.Rows[i][2] = dgvObjectives.Rows[i].Cells["X"].Value;
                dt.Rows[i][3] = dgvObjectives.Rows[i].Cells["Y"].Value;

                i++;
            }

            XElement objectiveNode = new XElement("OBJECTIVE",
                new XAttribute("ID", newMax.ToString()),
                new XAttribute("DESCRIPTION", dgvObjectives.Rows[dgvObjectives.RowCount - 1].Cells["DESCRIPTION"].Value.ToString()),
                new XAttribute("X", dgvObjectives.Rows[dgvObjectives.RowCount - 1].Cells["X"].Value.ToString()),
                new XAttribute("Y", dgvObjectives.Rows[dgvObjectives.RowCount - 1].Cells["Y"].Value.ToString()));

            var objectives = (from d in xelem.Elements("OOB").Elements("FORCE").Elements("FORMATION").Elements("OBJECTIVES").Elements("OBJECTIVE")
                              where (string)d.Parent.Parent.Parent.Attribute("ID") == Globals.GlobalVariables.FORCE && (string)d.Parent.Parent.Attribute("ID") == strFormID && (string)d.Parent.Attribute("TRACK") == strTrack
                              select d);

            //IF THERE ARE ALREADY OBJECTIVES IN THIS TRACK OR NOT
            if (objectives.Any())
            {
                objectives.Last().AddAfterSelf(objectiveNode);
            }
            else
            {
                xelem.XPathSelectElement("OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID=" + strFormID + "]/OBJECTIVES[@TRACK=" + strTrack + "]").Add(objectiveNode);
            }
            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);

            dt.Rows[dgvObjectives.Rows.Count - 1][0] = newMax.ToString();
            dgvObjectives.DataSource = dt;
            dgvObjectives.AllowUserToAddRows = false;
        }

        private void btnCloseMain_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        private void dgvObjectives_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            dgvObjectives.Rows[e.RowIndex].ErrorText = "";
            int depotData;

            if (dgvObjectives.Rows[e.RowIndex].IsNewRow) { return; }
            if (e.ColumnIndex != 1)
            {
                if (!int.TryParse(e.FormattedValue.ToString(),
                    out depotData) || depotData < 0)
                {
                    MessageBox.Show("The entered value must be a positive whole number!", "Enter Positive Whole Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    btnSaveObj.Enabled = false;
                    e.Cancel = true;
                }
                else
                {
                    btnSaveObj.Enabled = true;
                }

            }

        }

        private void btnEnviron_Click(object sender, EventArgs e)
        {
            frmEnviron environform = new frmEnviron();
            environform.Show();
        }

        public void DivideUnit()
        {
            cboDeployment.SelectedValue = "24";
            trvUnitTree.SelectedNode.ForeColor = System.Drawing.Color.Gray;
            Font f = new Font(trvUnitTree.Font, FontStyle.Bold);
            trvUnitTree.SelectedNode.NodeFont = f;
        }

        public void DivideUnitGUI()
        {
            //DISABLE UNIT ATTRIBUTES FOR DIVIDED UNITS
            txtProficiency.Enabled = false;
            txtSupply.Enabled = false;
            cboEmphasis.Enabled = false;
            txtReadiness.Enabled = false;
            cboExperience.Enabled = false;
            cboDeployment.Enabled = false;
            cboReplacements.Enabled = false;
            txtX.Enabled = false;
            txtY.Enabled = false;
            lblDivided.Text = "DIVIDED UNIT";
            lblDivided.Visible = true;

            //DISABLE EQUIPMENT ATTRIBUTES FOR DIVIDED UNITS
            txtNumber.Enabled = false;
            txtMax.Enabled = false;
            txtDamage.Enabled = false;
        }

        public void NonDivideUnitGUI()
        {
            //ENABLE UNIT ATTRIBUTES FOR NON-DIVIDED UNITS
            txtProficiency.Enabled = true;
            txtSupply.Enabled = true;
            cboEmphasis.Enabled = true;
            txtReadiness.Enabled = true;
            cboExperience.Enabled = true;
            cboDeployment.Enabled = true;
            cboReplacements.Enabled = true;
            txtX.Enabled = true;
            txtY.Enabled = true;
            lblDivided.Visible = false;

            //ENABLE EQUIPMENT ATTRIBUTES FOR NON-DIVIDED UNITS
            txtNumber.Enabled = true;
            txtMax.Enabled = true;
            txtDamage.Enabled = true;
        }

        public string getUnitID()
        {
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            return unitid;
        }

        public TreeNode getSelectedNode()
        {
            TreeNode copiedNode = trvUnitTree.SelectedNode;
            return copiedNode;
        }

        public void refreshTreeView()
        {
            Globals.GlobalVariables.TREEVIEWCHANGED = true;
            trvUnitTree.Refresh();
            Globals.GlobalVariables.TREEVIEWCHANGED = false;
        }

        public void ReloadTree(TreeNode copiedNode)
        {
            LoadTree();
            string textName = copiedNode.Text.ToString();
            string ID = copiedNode.Tag.ToString();
            TreeNode selectTNode = null;
            foreach (TreeNode node in trvUnitTree.Nodes)
            {
                selectTNode = SelectTreeNodeByTag(ID, textName, node);
                if (selectTNode != null) break;
            }
            trvUnitTree.SelectedNode = selectTNode;
            trvUnitTree.Focus();
            trvUnitTree.SelectedNode.EnsureVisible();
        }

        public TreeNode SelectTreeNodeByTag(string ID, string textName, TreeNode copiedNode)
        {
            foreach (TreeNode node in copiedNode.Nodes)
            {
                if (node.Tag.Equals(ID) && node.Text.Equals(textName)) return node;
                TreeNode next = SelectTreeNodeByTag(ID, textName, node);
                if (next != null) return next;
            }
            return null;
        }

        public List<TreeNode> SelectTreeNodeByTagSubUnits(string ID, string textName, TreeNodeCollection subunitNodes)
        {
            List<TreeNode> subNodes = new List<TreeNode>();
            foreach (TreeNode node in subunitNodes)
            {
                if (node.Tag.Equals(ID) && node.Text.Equals(textName))
                {
                    subNodes.Add(node);
                }
            }
            return subNodes;
        }

        public void SetSelectedTreeNodebyTag(TreeNode copiedTNode)
        {
            trvUnitTree.SelectedNode = copiedTNode;
            trvUnitTree.Focus();
            trvUnitTree.SelectedNode.EnsureVisible();
        }

        public void ExpandTreeNode(TreeNode parentTNode)
        {
            trvUnitTree.SelectedNode = parentTNode;
            trvUnitTree.SelectedNode.Expand();
        }

        public void cboDeployment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.GlobalVariables.PREVCBODEPLOYINDEX = cboDeployment.SelectedIndex;
        }

        public void FixInvalidXML()
        {
            //FIX INVALID XML**********************
            ///StreamReader streamReader = new StreamReader(Globals.GlobalVariables.PATH);
            StreamReader streamReader = new StreamReader(TOAWXML.Properties.Settings.Default.FilePath);

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
                goodXMLdoc.Save(TOAWXML.Properties.Settings.Default.FilePath);

                MessageBox.Show("TOAWxml has detected invalid XML characters.  The invalid characters have been replaced with '$'.",
                    "Invalid XML Characters",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button1);
            }
            //++++++++++++++
        }

        public void FixForce2SubunitBug()
        //WHEN XML EXPORTED FROM TOAW, IT ASSIGNS A PARENT ATTRIBUTE TO *ALL* UNITS (OTHER THAN EXISTING, "REAL" SUBUNITS) WITH A VALUE EQUAL TO UNIT ID.
        //THIS METHOD REMOVES THE ERRONEOUS PARENT ATTRIBUTES
        {
            ///XElement xdoc = XElement.Load(Globals.GlobalVariables.PATH);
            XElement xdoc = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            foreach (XElement force in xdoc.Descendants("FORCE").Where(f => f.Attribute("ID").Value.ToString() == "2"))
            {
                foreach (XElement formation in force.Descendants("FORMATION"))
                {
                    foreach (XElement unit in formation.Descendants("UNIT"))
                    {
                        if (unit.Attribute("PARENT") != null && (unit.Attribute("ID").Value.ToString() == unit.Attribute("PARENT").Value.ToString()))
                        {
                            XAttribute parent = unit.Attribute("PARENT");
                            parent.Remove();
                        }
                    }
                }

            }
            ///xdoc.Save(Globals.GlobalVariables.PATH);
            xdoc.Save(TOAWXML.Properties.Settings.Default.FilePath);
        }

        private void trvUnitTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                trvUnitTree.SelectedNode = e.Node;
            }

            switch (e.Node.Level)
            {
                case 0:
                    e.Node.ContextMenuStrip = cmsForce;
                    break;
                case 1:
                    e.Node.ContextMenuStrip = contextMenuStrip1;
                    break;
                case 2:
                    e.Node.ContextMenuStrip = cmsUnit;
                    break;
                case 3:
                    e.Node.ContextMenuStrip = cmsEquip;
                    break;
            }
        }

        private void divideUnitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var f = new frmUnitDivide(this);
            f.Show();
        }

        private void reuniteUnitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Reunification will merge all subunits into parent." + Environment.NewLine + Environment.NewLine + "Are you SURE you want to continue?",
                    "Confirm Reunification",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                TreeNode tnode = trvUnitTree.SelectedNode;
                tnode.ForeColor = System.Drawing.Color.Black;
                string unitid = tnode.Tag.ToString();
                string parentid = tnode.Parent.Tag.ToString();
                XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                var unit = xelem.XPathSelectElement(xpath);
                var subunits = xelem.XPathSelectElements("OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@PARENT =" + unitid + "]").ToArray();
                int SubQty = subunits.Count();

                List<int> eqpQtyList = new List<int>();
                List<int> eqpQtyList0 = new List<int>();
                List<int> eqpQtyList1 = new List<int>();
                List<int> eqpQtyList2 = new List<int>();
                List<int> eqpMaxList = new List<int>();
                List<int> eqpMaxList0 = new List<int>();
                List<int> eqpMaxList1 = new List<int>();
                List<int> eqpMaxList2 = new List<int>();

                trvUnitTree.BeginUpdate();

                double dblSubProf1 = 0; // = double.Parse(subunit.Attribute("PROFICIENCY").Value.ToString()) * 1.25;
                double dblSubProf2 = 0; // = double.Parse(subunit.Attribute("PROFICIENCY").Value.ToString()) * 1.25;
                double dblSubProf3 = 0; // = double.Parse(subunit.Attribute("PROFICIENCY").Value.ToString()) * 1.25;

                int u = 0;

                ////BEGIN SUBUNITS
                foreach (XElement subunit in subunits)
                {
                    string subunitid = subunit.Attribute("ID").Value.ToString();
                    string subunitname = subunit.Attribute("NAME").Value.ToString();
                    int eqpQty = subunit.Descendants("EQUIPMENT").Count();
                    int q = 0;
                    int[] eqpQtyArray = new int[eqpQty];
                    int[] eqpMaxArray = new int[eqpQty];

                    //PUT EACH EQUIPMENT'S NUMBER AND MAX INTO LIST
                    foreach (XElement equip in subunit.Descendants("EQUIPMENT"))
                    {
                        eqpQtyArray[q] = Convert.ToInt32(equip.Attribute("NUMBER").Value);
                        eqpMaxArray[q] = Convert.ToInt32(equip.Attribute("MAX").Value);
                        eqpQtyList.Add(Convert.ToInt32(equip.Attribute("NUMBER").Value));
                        eqpMaxList.Add(Convert.ToInt32(equip.Attribute("MAX").Value));
                        q = q + 1;
                    }

                    //REMOVE THE SUB TREENODES
                    foreach (TreeNode subNode in SelectTreeNodeByTagSubUnits(subunitid, subunitname, tnode.Parent.Nodes))
                    {
                        tnode.Parent.Nodes.Remove(subNode);
                    }

                    //ADD EACH OF THE ARRAYS OF EQUIPMENT QUANTITIES
                    switch (u)
                    {
                        case 0:
                            eqpQtyList0 = eqpQtyArray.ToList();
                            eqpMaxList0 = eqpMaxArray.ToList();
                            unit.Add(new XAttribute("X", subunit.Attribute("X").Value.ToString()));
                            unit.Add(new XAttribute("Y", subunit.Attribute("Y").Value.ToString()));
                            XAttribute attx = unit.Attribute("GOINGTOX");
                            XAttribute atty = unit.Attribute("GOINGTOY");
                            attx.Remove();
                            atty.Remove();
                            unit.Attribute("STATUS").Value = subunit.Attribute("STATUS").Value.ToString();
                            unit.Attribute("X").Value = subunit.Attribute("X").Value.ToString();
                            unit.Attribute("Y").Value = subunit.Attribute("Y").Value.ToString();
                            //dblSubProf1 = double.Parse(subunit.Attribute("PROFICIENCY").Value.ToString());
                            dblSubProf1 = Convert.ToDouble(subunit.Attribute("PROFICIENCY").Value.ToString());
                            break;
                        case 1:
                            eqpQtyList1 = eqpQtyArray.ToList();
                            eqpMaxList1 = eqpMaxArray.ToList();
                            var newList = eqpQtyList0.Zip(eqpQtyList1, (x, y) => (x + y));
                            var newMax = eqpMaxList0.Zip(eqpMaxList1, (x, y) => (x + y));

                            //ASSIGN NUMBERS AND MAX TO UNIT'S EQUIPMENT
                            int z = 0;
                            foreach (XElement equip in unit.Descendants("EQUIPMENT"))
                            {
                                equip.Attribute("NUMBER").Value = newList.ElementAt(z).ToString();
                                equip.Attribute("MAX").Value = newMax.ElementAt(z).ToString();
                                z = z + 1;
                            }

                            dblSubProf2 = (dblSubProf1 + (Convert.ToDouble(subunit.Attribute("PROFICIENCY").Value.ToString()))) / 2;
                            var parProf = Math.Round((dblSubProf2 * 1.25), 0, MidpointRounding.AwayFromZero);
                            unit.Attribute("PROFICIENCY").Value = parProf.ToString();
                            break;
                        case 2:
                            eqpQtyList2 = eqpQtyArray.ToList();
                            eqpMaxList2 = eqpMaxArray.ToList();
                            newList = eqpQtyList0.Zip(eqpQtyList1, (x, y) => (x + y)).Zip(eqpQtyList2, (x, y) => x + y);
                            newMax = eqpMaxList0.Zip(eqpMaxList1, (x, y) => (x + y)).Zip(eqpMaxList2, (x, y) => x + y);

                            //ASSIGN NUMBERS AND MAX TO UNIT'S EQUIPMENT
                            z = 0;
                            foreach (XElement equip in unit.Descendants("EQUIPMENT"))
                            {
                                equip.Attribute("NUMBER").Value = newList.ElementAt(z).ToString();
                                equip.Attribute("MAX").Value = newMax.ElementAt(z).ToString();
                                z = z + 1;
                            }
                            dblSubProf3 = ((dblSubProf2 * 2) + (Convert.ToDouble(subunit.Attribute("PROFICIENCY").Value))) / 3;
                            parProf = Math.Round((dblSubProf3 * 1.25), 0, MidpointRounding.AwayFromZero);
                            unit.Attribute("PROFICIENCY").Value = parProf.ToString();
                            break;
                    }
                    subunit.Remove();
                    u = u + 1;
                }
                //////END SUBUNITS

                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                trvUnitTree.EndUpdate();
                trvUnitTree.Refresh();
                ReloadTree(tnode);
            }
        }

        private void unitPropagationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Globals.GlobalVariables.PROFIC = txtProficiency.Text;
            Globals.GlobalVariables.SUPPLY = txtSupply.Text;
            Globals.GlobalVariables.LOSSTOL = cboEmphasis.Text;
            Globals.GlobalVariables.READINESS = txtReadiness.Text;
            Globals.GlobalVariables.DEPLOY = cboDeployment.SelectedValue.ToString();
            Globals.GlobalVariables.REPLACE = cboReplacements.SelectedValue.ToString();
            Globals.GlobalVariables.EXPERIENCE = cboExperience.Text;
            Globals.GlobalVariables.ID = trvUnitTree.SelectedNode.Tag.ToString();
            Globals.GlobalVariables.PARENTID = trvUnitTree.SelectedNode.Parent.Tag.ToString();
            Globals.GlobalVariables.ICONCOLOR = cboColor.Text;

            frmPropagateUnit propagateunitform = new frmPropagateUnit();
            propagateunitform.ShowDialog();
        }

        private void moveUpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node;
            node = trvUnitTree.SelectedNode;
            TreeNode tparent = node.Parent;

            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string parentid = trvUnitTree.SelectedNode.Parent.Tag.ToString();
            int unitLevel = node.Level;
            string xpath = "";

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";

            var unit = xelem.XPathSelectElement(xpath);

            if (tparent != null)
            {
                int index = tparent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    tparent.Nodes.RemoveAt(index);
                    tparent.Nodes.Insert(index - 1, node);
                    // bw : add this line to restore the originally selected node as selected
                    node.TreeView.SelectedNode = node;

                    var xparent = unit.Parent;
                    var previousNode = unit.PreviousNode;
                    previousNode.AddBeforeSelf(unit);
                    unit.Remove();  // see note below
                }
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            }
        }

        private void moveDownToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node;
            node = trvUnitTree.SelectedNode;
            TreeNode tparent = node.Parent;
            int unitLevel = node.Level;

            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string parentid = trvUnitTree.SelectedNode.Parent.Tag.ToString();
            string xpath = "";

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";

            var unit = xelem.XPathSelectElement(xpath);

            if (tparent != null)
            {
                int index = tparent.Nodes.IndexOf(node);
                if (index < tparent.Nodes.Count - 1)
                {
                    tparent.Nodes.RemoveAt(index);
                    tparent.Nodes.Insert(index + 1, node);
                    // bw : add this line to restore the originally selected node as selected
                    node.TreeView.SelectedNode = node;

                    var xparent = unit.Parent;
                    var nextNode = unit.NextNode;
                    nextNode.AddAfterSelf(unit);
                    unit.Remove();
                }
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            }
        }

        private void deleteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (cboDeployment.SelectedValue.ToString() != "24") //IF UNIT IS NOT DIVIDED
            {
                if (MessageBox.Show("Are you SURE you want to delete this unit?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    TreeNode tnode;
                    tnode = trvUnitTree.SelectedNode;
                    int unitLevel = tnode.Level;
                    string unitid = tnode.Tag.ToString();
                    string parentid = tnode.Parent.Tag.ToString();
                    XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

                    string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
                    string xpath2 = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT";

                    var unit = xelem.XPathSelectElement(xpath);
                    var units = xelem.XPathSelectElements(xpath2);
                    var unitevents = (from u in xelem.Elements("EVENTS").Elements("EVENT")  //ALL EVENTS FEATURING UNITS
                                      where (string)u.Attribute("TRIGGER") == "Unit destroyed" ||
                                      (string)u.Attribute("EFFECT") == "Disband unit" ||
                                      (string)u.Attribute("EFFECT") == "Withdraw unit" ||
                                      (string)u.Attribute("EFFECT") == "Withdraw army"
                                      select u);

                    string forceid = "";

                    //RENUMBER ANY FORMATIONS USED IN EVENTS
                    if (rbForce1.Checked == true)
                    {
                        forceid = "1";
                    }

                    if (rbForce2.Checked == true)
                    {
                        forceid = "2";
                    }

                    bool proceed = Renumbering.RenumberEventUnits(unitevents, unitid, forceid);

                    if (proceed == false)
                    {
                        return;
                    }

                    trvUnitTree.BeginUpdate();
                    tnode.Remove();
                    unit.Remove();

                    //RENUMBER ALL DIVIDED UNIT PARENT ATTRIBUTES
                    var unitparents = from p in xelem.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT")
                                      where (string)p.Parent.Parent.Attribute("ID") == "1" && p.Attribute("PARENT") != null
                                      select p;

                    Renumbering.RenumberDividedUnit(unitparents, unitid);

                    //RENUMBER ALL UNITS
                    Renumbering.RenumberAll(units);

                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvUnitTree.EndUpdate();
                    LoadTree();
                    trvUnitTree.Refresh();
                }
            }
            else //IF UNIT IS DIVIDED
            {
                if (MessageBox.Show("Deletion of divided unit also deletes all of its divided sub-units." + Environment.NewLine + Environment.NewLine + "Are you SURE you want to continue?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    TreeNode tnode;
                    tnode = trvUnitTree.SelectedNode;
                    int unitLevel = tnode.Level;
                    string unitid = tnode.Tag.ToString();
                    string parentid = tnode.Parent.Tag.ToString();
                    XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
                    string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";

                    IEnumerable<XElement> subunits = xelem.XPathSelectElements("OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@PARENT =" + unitid + "]").ToArray();

                    var unit = xelem.XPathSelectElement(xpath);
                    trvUnitTree.BeginUpdate();

                    //REMOVE DIVIDED SUBUNITS FROM TREE AND XML
                    foreach (XElement subunit in subunits)
                    {
                        string subunitid = subunit.Attribute("ID").Value.ToString();
                        string subunitname = subunit.Attribute("NAME").Value.ToString();
                        foreach (TreeNode subNode in SelectTreeNodeByTagSubUnits(subunitid, subunitname, tnode.Parent.Nodes))
                        {
                            tnode.Parent.Nodes.Remove(subNode);
                        }
                        subunit.Remove();
                    }

                    //REMOVE DIVIDED PARENT UNIT FROM TREE AND XML
                    tnode.Remove();
                    unit.Remove();

                    xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                    trvUnitTree.EndUpdate();
                    trvUnitTree.Refresh();
                }
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int copiedLevel;
            int oldMax = 1;
            int newMax = 1;
            string xpathParent = "parent"; //FILLER CONTENT
            string xpathCopied = "copied"; //FILLER CONTENT

            //COPY IN TREEVIEW
            TreeNode copiedNode = trvUnitTree.SelectedNode;
            copiedLevel = copiedNode.Level;

            TreeNode parentNode = copiedNode.Parent;
            string strParentID = parentNode.Tag.ToString();
            string strcopiedText = copiedNode.Text;
            Globals.GlobalVariables.COPIEDID = copiedNode.Tag.ToString();
            Globals.GlobalVariables.COPIEDPARENTID = strParentID;
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            xpathParent = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION";
            xpathCopied = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.COPIEDID + "]";
            var parent = xelem.XPathSelectElement(xpathParent);
            var copied = xelem.XPathSelectElement(xpathCopied);
            var maxid = parent.Parent.Descendants("UNIT").Attributes("ID").ToList();
            oldMax = parent.Parent.Descendants("UNIT").Max(m => (int)m.Attribute("ID"));
            XElement cloneNode = new XElement(copied);
            TreeNode copiedClone = (TreeNode)trvUnitTree.SelectedNode.Clone();

            //ADD CLONED XMLNODE 
            parentNode = copiedNode.Parent;
            cloneNode = new XElement(copied);
            newMax = oldMax + 1;
            cloneNode.Attribute("ID").Value = newMax.ToString();
            cloneNode.Attribute("NAME").Value = "Copy of " + copied.Attribute("NAME").Value.ToString();
            copied.AddAfterSelf(cloneNode);

            copiedClone = (TreeNode)trvUnitTree.SelectedNode.Clone();
            copiedClone.Text = "Copy of " + strcopiedText;
            copiedClone.Tag = newMax.ToString();
            parentNode.Nodes.Insert(copiedNode.Index + 1, copiedClone);

            //DELETE "NEXT" ATTRIBUTES
            string xpathForceUnits = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT";
            var forceunits = xelem.XPathSelectElements(xpathForceUnits);

            foreach (XElement fu in forceunits)
                if (fu.Attribute("NEXT") != null)
                {
                    XAttribute attNext = fu.Attribute("NEXT");
                    attNext.Remove();
                }

            parentNode.Expand();
            Globals.GlobalVariables.TREEVIEWCHANGED = true;

            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            Globals.GlobalVariables.TREEVIEWCHANGED = false;
            trvUnitTree.Refresh();

            //Select the dropped node and navigate (however you do it)
            trvUnitTree.SelectedNode = copiedNode;
        }

        private void moveUpToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TreeNode node;
            node = trvUnitTree.SelectedNode;
            TreeNode tparent = node.Parent;

            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string parentid = trvUnitTree.SelectedNode.Parent.Tag.ToString();
            int unitLevel = node.Level;
            string xpath = "";

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + parentid + "]/EQUIPMENT[@ID =" + unitid + "]";

            var unit = xelem.XPathSelectElement(xpath);

            if (tparent != null)
            {
                int index = tparent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    tparent.Nodes.RemoveAt(index);
                    tparent.Nodes.Insert(index - 1, node);
                    // bw : add this line to restore the originally selected node as selected
                    node.TreeView.SelectedNode = node;

                    var xparent = unit.Parent;
                    var previousNode = unit.PreviousNode;
                    previousNode.AddBeforeSelf(unit);
                    unit.Remove();  // see note below
                }
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            }
        }

        private void moveDownToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TreeNode node;
            node = trvUnitTree.SelectedNode;
            TreeNode tparent = node.Parent;
            int unitLevel = node.Level;

            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            string parentid = trvUnitTree.SelectedNode.Parent.Tag.ToString();
            string xpath = "";

            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + parentid + "]/EQUIPMENT[@ID =" + unitid + "]";
            var unit = xelem.XPathSelectElement(xpath);

            if (tparent != null)
            {
                int index = tparent.Nodes.IndexOf(node);
                if (index < tparent.Nodes.Count - 1)
                {
                    tparent.Nodes.RemoveAt(index);
                    tparent.Nodes.Insert(index + 1, node);
                    // bw : add this line to restore the originally selected node as selected
                    node.TreeView.SelectedNode = node;

                    var xparent = unit.Parent;
                    var nextNode = unit.NextNode;
                    nextNode.AddAfterSelf(unit);
                    unit.Remove();
                }
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            }
        }

        private void deleteToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TreeNode tnode = trvUnitTree.SelectedNode;
            int unitLevel = tnode.Level;
            string unitid = tnode.Tag.ToString();
            string parentid = tnode.Parent.Tag.ToString();
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + parentid + "]/EQUIPMENT[@ID =" + unitid + "]";
            var equip = xelem.XPathSelectElement(xpath);
            //if ((equip.Parent.Attribute("STATUS").Value.ToString() != "24") && equip.Parent.Attribute("PARENT") == null) //IF UNIT IS NOT DIVIDED
            //{
            if (MessageBox.Show("Are you SURE you want to delete this equipment?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                trvUnitTree.BeginUpdate();
                tnode.Remove();
                equip.Remove();
                xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
                trvUnitTree.EndUpdate();
                trvUnitTree.Refresh();
            }
            //}
            //else //IF UNIT IS DIVIDED
            //{
            //    MessageBox.Show("Equipment line items cannot be deleted from divided units or subunits!  The divided unit must be reunited before deleting any equipment line items from the divided parent or subunits.",
            //        "Equipment Line Item Cannot Be Deleted",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Exclamation);
            //}
        }

        private void copyToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int copiedLevel;
            int oldMax = 1;
            int newMax = 1;
            string xpathParent = "parent"; //FILLER CONTENT
            string xpathCopied = "copied"; //FILLER CONTENT

            //COPY IN TREEVIEW
            TreeNode copiedNode = trvUnitTree.SelectedNode;
            copiedLevel = copiedNode.Level;

            TreeNode parentNode = copiedNode.Parent;
            string strParentID = parentNode.Tag.ToString();
            string strcopiedText = copiedNode.Text;
            Globals.GlobalVariables.COPIEDID = copiedNode.Tag.ToString();
            Globals.GlobalVariables.COPIEDPARENTID = strParentID;
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

            xpathParent = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.COPIEDPARENTID + "]";
            xpathCopied = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID= " + Globals.GlobalVariables.COPIEDPARENTID + "]/EQUIPMENT[@ID =" + Globals.GlobalVariables.COPIEDID + "]";
            var parent = xelem.XPathSelectElement(xpathParent);
            var copied = xelem.XPathSelectElement(xpathCopied);
            var maxid = parent.Descendants("EQUIPMENT").Attributes("ID").ToList();
            oldMax = parent.Descendants("EQUIPMENT").Max(m => (int)m.Attribute("ID"));

            //ADD CLONED XMLNODE 
            parentNode = copiedNode.Parent;
            XElement cloneNode = new XElement(copied);
            newMax = oldMax + 1;
            cloneNode.Attribute("ID").Value = newMax.ToString();
            cloneNode.Attribute("NAME").Value = "Copy of " + copied.Attribute("NAME").Value.ToString();
            copied.AddAfterSelf(cloneNode);

            TreeNode copiedClone = (TreeNode)trvUnitTree.SelectedNode.Clone();
            copiedClone.Text = "Copy of " + strcopiedText;
            copiedClone.Tag = newMax.ToString();
            parentNode.Nodes.Insert(copiedNode.Index + 1, copiedClone);

            parentNode.Expand();
            Globals.GlobalVariables.TREEVIEWCHANGED = true;

            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
            Globals.GlobalVariables.TREEVIEWCHANGED = false;
            trvUnitTree.Refresh();

            //Select the dropped node and navigate (however you do it)
            trvUnitTree.SelectedNode = copiedNode;
        }

        private void addFormationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int oldMax = 1;
            int newMax = 1;
            string xpathForce = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]";
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            var force = xelem.XPathSelectElement(xpathForce);
            oldMax = force.Descendants("FORMATION").Max(m => (int)m.Attribute("ID"));
            newMax = oldMax + 1;
            var newXnode = new XElement("FORMATION",
                    new XAttribute("ID", newMax.ToString()),
                    new XAttribute("NAME", "New Formation"),
                    new XAttribute("PROFICIENCY", "1"),
                    new XAttribute("SUPPLY", "1"),
                    new XAttribute("SUPPORTSCOPE", "Force Support"),
                    new XAttribute("ORDERS", "Attack"),
                    new XAttribute("EMPHASIS", "Limit Losses"),
                    new XElement("OBJECTIVES", new XAttribute("TRACK", "1")),
                    new XElement("OBJECTIVES", new XAttribute("TRACK", "2")),
                    new XElement("OBJECTIVES", new XAttribute("TRACK", "3")),
                    new XElement("OBJECTIVES", new XAttribute("TRACK", "4")),
                    new XElement("OBJECTIVES", new XAttribute("TRACK", "5"))
                    );
            force.Add(newXnode);

            //INSERT NEW FORMATION IN TREEVIEW
            TreeNode newTnode = new TreeNode();
            //TreeNode rootTnode = trvUnitTree.TopNode;
            TreeNode rootTnode = trvUnitTree.Nodes[0];
            newTnode.Text = "New Formation";
            newTnode.Tag = newMax.ToString();
            newTnode.Name = "FORMATION";
            //rootTnode.Nodes.Insert(trvUnitTree.TopNode.LastNode.Index+1, newTnode);
            rootTnode.Nodes.Insert(trvUnitTree.Nodes[0].LastNode.Index + 1, newTnode);
            Globals.GlobalVariables.TREEVIEWCHANGED = true;
            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);

            trvUnitTree.SelectedNode = newTnode;
            trvUnitTree.SelectedNode.EnsureVisible();
            Globals.GlobalVariables.TREEVIEWCHANGED = false;
            trvUnitTree.Focus();

        }

        private void addNewUnitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int oldMax = 1;
            int newMax = 1;
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpathTarget = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION[@ID =" + trvUnitTree.SelectedNode.Tag.ToString() + "]";
            string xpathParent = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]";
            var target = xelem.XPathSelectElement(xpathTarget);
            var parent = xelem.XPathSelectElement(xpathParent);

            //GET FORMATION DATA TO USE FOR UNIT
            string parentProf = target.Attribute("PROFICIENCY").Value.ToString();
            string parentSupply = target.Attribute("SUPPLY").Value.ToString();

            oldMax = parent.Descendants("FORMATION").Descendants("UNIT").Max(c => (int)c.Attribute("ID"));
            newMax = oldMax + 1;
            var newXnode = new XElement("UNIT",
                    new XAttribute("ID", newMax.ToString()),
                    new XAttribute("NAME", "New Unit"),
                    new XAttribute("ICON", "Infantry"),
                    new XAttribute("COLOR", "1"),
                    new XAttribute("SIZE", "Regiment"),
                    new XAttribute("EXPERIENCE", "untried"),
                    new XAttribute("CHARACTERISTICS", "1"),
                    new XAttribute("PROFICIENCY", parentProf),
                    new XAttribute("READINESS", "100"),
                    new XAttribute("SUPPLY", parentSupply),
                    new XAttribute("X", "1"),
                    new XAttribute("Y", "1"),
                    new XAttribute("EMPHASIS", "Limit Losses"),
                    new XAttribute("STATUS", "8"),
                    new XAttribute("REPLACEMENTPRIORITY", "0")
                    );
            target.Add(newXnode);

            //INSERT NEW UNIT IN TREEVIEW
            TreeNode newTnode = new TreeNode();
            TreeNode targetTnode = trvUnitTree.SelectedNode;
            newTnode.Text = "New Unit";
            newTnode.Tag = newMax.ToString();
            newTnode.Name = "UNIT";
            //targetTnode.Nodes.Insert(trvUnitTree.SelectedNode.Index + 1, newTnode);
            targetTnode.Nodes.Insert(trvUnitTree.SelectedNode.LastNode.Index + 1, newTnode);
            Globals.GlobalVariables.TREEVIEWCHANGED = true;
            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);

            trvUnitTree.SelectedNode = newTnode;
            trvUnitTree.SelectedNode.EnsureVisible();
            Globals.GlobalVariables.TREEVIEWCHANGED = false;
            trvUnitTree.Focus();
        }

        private void cboIcon_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = xelem.XPathSelectElement(xpath);


            string strOldIcon = unit.Attribute("ICON").Value.ToString();
            string strNewIconDisplay = cboIcon.Text;
            string strNewIconValue = cboIcon.SelectedValue.ToString();
            string strNewIconID = ""; //PLACEHOLDER VALUE
            bool oldHasIconID = false;
            bool newHasIconID = false;

            if (unit.Attribute("ICONID") != null)
            {
                oldHasIconID = true;
                string strOldIconID = unit.Attribute("ICONID").Value.ToString();
            }
            switch (strNewIconDisplay)
            {
                case "Headquarters [v1]":
                    newHasIconID = true;
                    strNewIconID = "0";
                    break;
                case "Headquarters [v2]":
                    newHasIconID = true;
                    strNewIconID = "1";
                    break;
                case "Antitank [v1]":

                    newHasIconID = true;
                    strNewIconID = "14";
                    break;
                case "Antitank [v2]":
                    newHasIconID = true;
                    strNewIconID = "15";
                    break;
                case "Antitank (Mot) [v1]":
                    newHasIconID = true;
                    strNewIconID = "25";
                    break;
                case "Antitank (Mot) [v2]":
                    newHasIconID = true;
                    strNewIconID = "26";
                    break;
                case "Fighter [icon]":
                    newHasIconID = true;
                    strNewIconID = "41";
                    break;
                case "Fighter Bomber [icon]":
                    newHasIconID = true;
                    strNewIconID = "42";
                    break;
                case "Bomber (Light) [icon]":
                    newHasIconID = true;
                    strNewIconID = "43";
                    break;
                case "Bomber (Heavy) [icon]":
                    newHasIconID = true;
                    strNewIconID = "45";
                    break;
                case "Artillery (Coast) [icon]":
                    newHasIconID = true;
                    strNewIconID = "62";
                    break;
                case "Artillery (Coast) [silh]":
                    newHasIconID = true;
                    strNewIconID = "63";
                    break;
                case "Fighter [silh]":
                    newHasIconID = true;
                    strNewIconID = "66";
                    break;
                case "Fighter Bomber [silh]":
                    newHasIconID = true;
                    strNewIconID = "67";
                    break;
                case "Bomber (Light) [silh]":
                    newHasIconID = true;
                    strNewIconID = "68";
                    break;
                case "Bomber (Heavy) [silh]":
                    newHasIconID = true;
                    strNewIconID = "69";
                    break;
                case "Transport [icon]":
                    newHasIconID = true;
                    strNewIconID = "82";
                    break;
                case "Transport [silh]":
                    newHasIconID = true;
                    strNewIconID = "94";
                    break;
            }

            //SET ICON VALUES DEPENDING ON WHETHER ALTERNATE ICONS ARE INVOLVED 
            if (oldHasIconID == true && newHasIconID == true)
            {
                unit.Attribute("ICONID").Value = strNewIconID;
            }

            if (oldHasIconID == false && newHasIconID == true)
            {
                unit.Add(new XAttribute("ICONID", strNewIconID));
            }

            if (oldHasIconID == true && newHasIconID == false)
            {
                XAttribute attID = unit.Attribute("ICONID");
                attID.Remove();
            }

            unit.Attribute("ICON").Value = strNewIconValue;
            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
        }

        private void cboColor_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string unitid = trvUnitTree.SelectedNode.Tag.ToString();
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = xelem.XPathSelectElement(xpath);

            unit.Attribute("COLOR").Value = cboColor.Text;

            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);
        }

        private void addNewUnitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new frmEquip();
            f.Show();
        }

        private void btnEvents_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            var f = new frmEvents();
            f.Show();
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtNumber.Text))
            {
                btnSave.Enabled = false;
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
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtNumber, "Please enter positive whole number between 0 and 999.");
                    btnSave.Enabled = false;
                }
            }
            //{
            //    validatePosInt();
            //}
            //bool validatePosInt()
            //{
            //    bool bPosInt = true;
            //    if (txtNumber.Text.All(Char.IsDigit) == false)
            //    {
            //        errorProvider1.SetError(txtNumber, "Please enter whole number between 0 and 999");
            //        bPosInt = false;
            //        btnSave.Enabled = false;
            //    }
            //    else
            //    {
            //        string strNumber = txtNumber.Text;
            //        int x = int.Parse(strNumber);

            //        if (x >= 0 && x <= 999)
            //        {
            //            errorProvider1.SetError(txtNumber, "");
            //            btnSave.Enabled = true;
            //        }
            //        else
            //        {
            //            errorProvider1.SetError(txtNumber, "Please enter whole number between 0 and 999");
            //            btnSave.Enabled = false;
            //        }
            //    }
            //    return bPosInt;
            //}
        }

        private void txtMax_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtMax.Text))
            {
                btnSave.Enabled = false;
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
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtMax, "Please enter positive whole number between 1 and 999.");
                    btnSave.Enabled = false;
                }
            }
            //{
            //    validatePosInt();
            //}
            //bool validatePosInt()
            //{
            //    bool bPosInt = true;
            //    if (txtMax.Text.All(Char.IsDigit) == false)
            //    {
            //        errorProvider1.SetError(txtMax, "Please enter whole number between 1 and 999");
            //        bPosInt = false;
            //        btnSave.Enabled = false;
            //    }
            //    else
            //    {
            //        string strMax = txtMax.Text;
            //        int x = int.Parse(strMax);

            //        if (x >= 1 && x <= 999)
            //        {
            //            errorProvider1.SetError(txtMax, "");
            //            btnSave.Enabled = true;
            //        }
            //        else
            //        {
            //            errorProvider1.SetError(txtMax, "Please enter whole number between 1 and 999");
            //            btnSave.Enabled = false;
            //        }
            //    }
            //    return bPosInt;
            //}
        }

        private void txtProficiency_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtProficiency.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtProficiency.Text, out var intValue))
            {
                errorProvider1.SetError(txtProficiency, "Please enter whole number between 1 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtProficiency, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtProficiency, "Please enter whole number between 1 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtSupply_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtSupply.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtSupply.Text, out var intValue))
            {
                if (tabUnits.SelectedIndex == 2)
                {
                    errorProvider1.SetError(txtSupply, "Please enter whole number between 1 and 150.");
                    btnSave.Enabled = false;
                    return;
                }
                else
                {
                    errorProvider1.SetError(txtSupply, "Please enter whole number between 1 and 100.");
                    btnSave.Enabled = false;
                    return;
                }
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //**
                if (tabUnits.SelectedIndex == 2)
                {
                    //**
                    //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                    if (intValue >= 1 && (intValue <= 150))
                    {
                        errorProvider1.SetError(txtSupply, "");
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        errorProvider1.SetError(txtSupply, "Please enter whole number between 1 and 150.");
                        btnSave.Enabled = false;
                    }
                    //**
                }
                else
                {
                    //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                    if (intValue >= 1 && (intValue <= 100))
                    {
                        errorProvider1.SetError(txtSupply, "");
                        btnSave.Enabled = true;
                    }
                    else
                    {
                        errorProvider1.SetError(txtSupply, "Please enter whole number between 1 and 100.");
                        btnSave.Enabled = false;
                    }
                }

                //**
            }
        }

        private void txtReadiness_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtReadiness.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtReadiness.Text, out var intValue))
            {
                errorProvider1.SetError(txtReadiness, "Please enter whole number between 1 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtReadiness, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtReadiness, "Please enter whole number between 1 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtReinforceTrigger_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtReinforceTrigger.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtReinforceTrigger.Text, out var intValue))
            {
                errorProvider1.SetError(txtReinforceTrigger, "Please enter positive whole number.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1)
                {
                    errorProvider1.SetError(txtReinforceTrigger, "");
                    btnSave.Enabled = true;

                    if (cboDeployment.SelectedValue.ToString() == "1")
                    {
                        //DateTime gameDateTime = GameTime.getCurrentGameDate();
                        int turnLength = GameTime.getTurnLength();

                        if (turnLength > 0)
                        {
                            //lblReinfDate.Visible = true;
                            //int turnSpan = ((Convert.ToInt32(txtReinforceTrigger.Text)-1) * turnLength);
                            //TimeSpan timeSpan = TimeSpan.FromHours(turnSpan);
                            //DateTime reinfDateTime = gameDateTime + timeSpan;
                            //lblReinfDate.Text = reinfDateTime.ToString("d MMM yyyy" + Environment.NewLine + " @ " + "HH:mm");
                            //lblReinfDate.Text = GameTime.getReleaseDate(txtReinforceTrigger.Text); ;
                            tssLabel1.Text = GameTime.getReleaseDate(txtReinforceTrigger.Text);
                        }
                        else
                        {
                            //lblReinfDate.Visible = false;
                            //lblReinfDate.Text = "N/A";
                            tssLabel1.Text = "";
                        }
                    }
                }
                else
                {
                    errorProvider1.SetError(txtReinforceTrigger, "Please enter positive whole number.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtX_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtX.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtX.Text, out var intValue) && txtX.Text != "--")
            {
                errorProvider1.SetError(txtX, "Please enter valid X location.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0)
                {
                    errorProvider1.SetError(txtX, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtX, "Please enter valid X location.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtY_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtY.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtY.Text, out var intValue) && txtY.Text != "--")
            {
                errorProvider1.SetError(txtY, "Please enter valid Y location.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0)
                {
                    errorProvider1.SetError(txtY, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtY, "Please enter valid Y location.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtRecon_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtRecon.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtRecon.Text, out var intValue))
            {
                errorProvider1.SetError(txtRecon, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtRecon, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtRecon, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtGuerilla_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtGuerilla.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtGuerilla.Text, out var intValue))
            {
                errorProvider1.SetError(txtGuerilla, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtGuerilla, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtGuerilla, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtRailRepair_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtRailRepair.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtRailRepair.Text, out var intValue))
            {
                errorProvider1.SetError(txtRailRepair, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtRailRepair, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtRailRepair, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtRailDestr_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtRailDestr.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtRailDestr.Text, out var intValue))
            {
                errorProvider1.SetError(txtRailDestr, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtRailDestr, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtRailDestr, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtRoadSupply_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtRoadSupply.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtRoadSupply.Text, out var intValue))
            {
                errorProvider1.SetError(txtRoadSupply, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtRoadSupply, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtRoadSupply, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtExtSupply_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtExtSupply.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtExtSupply.Text, out var intValue))
            {
                errorProvider1.SetError(txtExtSupply, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtExtSupply, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtExtSupply, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtNightProf_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtNightProf.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtNightProf.Text, out var intValue))
            {
                errorProvider1.SetError(txtNightProf, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtNightProf, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtNightProf, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtPestilence_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtPestilence.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtPestilence.Text, out var intValue))
            {
                errorProvider1.SetError(txtPestilence, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtPestilence, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtPestilence, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtZOCCost_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtZOCCost.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtZOCCost.Text, out var intValue))
            {
                errorProvider1.SetError(txtZOCCost, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtZOCCost, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtZOCCost, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtReconPtX_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtReconPtX.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtReconPtX.Text, out var intValue))
            {
                errorProvider1.SetError(txtReconPtX, "Please enter number equal to or greater than 0.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0)
                {
                    errorProvider1.SetError(txtReconPtX, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtReconPtX, "Please enter number equal to or greater than 0.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtReconPtY_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtReconPtY.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtReconPtY.Text, out var intValue))
            {
                errorProvider1.SetError(txtReconPtY, "Please enter number equal to or greater than 0.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0)
                {
                    errorProvider1.SetError(txtReconPtY, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtReconPtY, "Please enter number equal to or greater than 0.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtVictoryMod_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtVictoryMod.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtVictoryMod.Text, out var intValue))
            {
                errorProvider1.SetError(txtVictoryMod, "Please enter number equal to or greater than 0.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0)
                {
                    errorProvider1.SetError(txtVictoryMod, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtVictoryMod, "Please enter number equal to or greater than 0.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtNBCReadiness_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtNBCReadiness.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtNBCReadiness.Text, out var intValue))
            {
                errorProvider1.SetError(txtNBCReadiness, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtNBCReadiness, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtNBCReadiness, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtElectSupp_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtElectSupp.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtElectSupp.Text, out var intValue))
            {
                errorProvider1.SetError(txtElectSupp, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtElectSupp, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtElectSupp, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtNavCritScal_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtNavCritScal.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtNavCritScal.Text, out var intValue))
            {
                errorProvider1.SetError(txtNavCritScal, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtNavCritScal, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtNavCritScal, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtRFCScal_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtRFCScal.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtRFCScal.Text, out var intValue))
            {
                errorProvider1.SetError(txtRFCScal, "Please enter number between 0 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtRFCScal, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtRFCScal, "Please enter number between 0 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtCommunications_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtCommunications.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtCommunications.Text, out var intValue))
            {
                errorProvider1.SetError(txtCommunications, "Please enter number between 25 and 100.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 25 && (intValue <= 100))
                {
                    errorProvider1.SetError(txtCommunications, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtCommunications, "Please enter number between 25 and 100.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtPGWMult_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtPGWMult.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtPGWMult.Text, out var intValue))
            {
                errorProvider1.SetError(txtPGWMult, "Please enter number between 0 and 999.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 999))
                {
                    errorProvider1.SetError(txtPGWMult, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtPGWMult, "Please enter number between 0 and 999.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtAirRefuel_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtAirRefuel.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtAirRefuel.Text, out var intValue))
            {
                errorProvider1.SetError(txtAirRefuel, "Please enter number between 1 and 10.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1 && (intValue <= 10))
                {
                    errorProvider1.SetError(txtAirRefuel, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtAirRefuel, "Please enter number between 1 and 10.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtMoveBias_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtMoveBias.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtMoveBias.Text, out var intValue))
            {
                errorProvider1.SetError(txtMoveBias, "Please enter number between 14 and 455.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 14 && (intValue <= 455))
                {
                    errorProvider1.SetError(txtMoveBias, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtMoveBias, "Please enter number between 14 and 455.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void txtLossToler_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtLossToler.Text))
            {
                btnSave.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtLossToler.Text, out var intValue))
            {
                errorProvider1.SetError(txtLossToler, "Please enter number between 10 and 999.");
                btnSave.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 10 && (intValue <= 999))
                {
                    errorProvider1.SetError(txtLossToler, "");
                    btnSave.Enabled = true;
                }
                else
                {
                    errorProvider1.SetError(txtLossToler, "Please enter number between 10 and 999.");
                    btnSave.Enabled = false;
                }
            }
        }

        private void cboOrders_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboOrders.Text == "Static")
            {
                txtEntryTurn.Visible = true;
                lblEntryTurn.Visible = true;
                //lblEntryDate.Visible = true;
                txtEntryTurn.Text = "1";
                string date = txtEntryTurn.Text;
                //lblEntryDate.Text = GameTime.GetReleaseDate(date);
                tssLabel1.Text = GameTime.getReleaseDate(date);

                trvUnitTree.SelectedNode.ForeColor = System.Drawing.Color.IndianRed;
                Font f = new Font(trvUnitTree.Font, FontStyle.Bold);
                trvUnitTree.SelectedNode.NodeFont = f;
                trvUnitTree.Refresh();
            }
            else
            {
                txtEntryTurn.Visible = false;
                lblEntryTurn.Visible = false;
                //lblEntryDate.Visible = false;
                tssLabel1.Text = "";
                trvUnitTree.SelectedNode.ForeColor = System.Drawing.Color.Black;
                Font font = new Font(trvUnitTree.Font, FontStyle.Regular);
                trvUnitTree.SelectedNode.NodeFont = font;
                trvUnitTree.Refresh();
            }
        }

        //private void btnStar_Click(object sender, EventArgs e)
        //{
        //    frmTacFile tacfileform = new frmTacFile();
        //    tacfileform.ShowDialog();
        //}

        private void btnEquipView_Click(object sender, EventArgs e)
        {
            frmEquipView equipviewform = new frmEquipView();
            equipviewform.ShowDialog();

        }
    }
}