using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TOAWXML
{
    public partial class frmSelectFormation : Form
    {
        public frmSelectFormation()
        {
            InitializeComponent();
        }

        private void frmSelectFormation_Load(object sender, EventArgs e)
        {
            btnSelect.Enabled = false;

            ////Checks that FilePath.txt exists
            //if (System.IO.File.Exists("FilePath.txt"))
            //{
            //    string filePath = File.ReadAllText("FilePath.txt");

            //    Globals.GlobalVariables.PATH = System.IO.Path.Combine(filePath);

            //    if (!System.IO.File.Exists(Globals.GlobalVariables.PATH))
            //    {
            //        frmMissingFile loadfileform = new frmMissingFile();
            //        loadfileform.ShowDialog();
            //        return;
            //    }

            //    XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);

            //    //GET NAME OF FORCE 1 AND ASSIGN TO radio button text
            //    var forcenames = xelem.Descendants("HEADER");
            //    foreach (var f in forcenames)
            //    {
            //        string fn1 = f.Attribute("forceName1").Value.ToString();
            //        this.rbForce1.Text = fn1;

            //        string fn2 = f.Attribute("forceName2").Value.ToString();
            //        this.rbForce2.Text = fn2;
            //    }
            //}

            //>>>>>>>>>>>>>>>>>>>>>>>>>>
            //CHECK THAT FILEPATH HAS BEEN ASSIGNED
            if (TOAWXML.Properties.Settings.Default.FilePath != "")
            {
                string filePath = TOAWXML.Properties.Settings.Default.FilePath;

                if (!System.IO.File.Exists(filePath))
                {
                    //frmMissingFile loadfileform = new frmMissingFile();
                    //loadfileform.ShowDialog();
                    //return;
                    frmLoadFile loadfileform = new frmLoadFile();
                    loadfileform.ShowDialog();
                    return;
                }

                //XDocument xdoc = XDocument.Load(TOAWXML.Properties.Settings.Default.FilePath);
                XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);

                //GET NAME OF FORCE 1 AND ASSIGN TO radio button text
                var forcenames = xelem.Descendants("HEADER");
                foreach (var f in forcenames)
                {
                    string fn1 = f.Attribute("forceName1").Value.ToString();
                    this.rbForce1.Text = fn1;

                    string fn2 = f.Attribute("forceName2").Value.ToString();
                    this.rbForce2.Text = fn2;
                }
            }
            //<<<<<<<<<<<<<<<<<<<<<<<<<<
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SFLoadTree()
        {
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            TreeNode forceNode;
            TreeNode formationNode;
            //TreeNode unitNode;
            string strForce;

            strForce = "0";

            if (this.rbForce1.Checked == true)
            {
                strForce = "1";
            }
            else if (this.rbForce2.Checked == true)
            {
                strForce = "2";
            }

            trvSFTree.Nodes.Clear();

            foreach (XElement force in xelem.Descendants("FORCE").Where(f => f.Attribute("ID").Value == strForce))
            {
                if (force.Attribute("NAME") != null)
                {
                    forceNode = trvSFTree.Nodes.Add(force.Attribute("NAME").Value);
                    forceNode.Tag = force.Attribute("ID").Value;
                    forceNode.Name = "FORCE";
                }

                forceNode = trvSFTree.TopNode;
                //forceNode = trvSFTree.Nodes[0];

                foreach (XElement formation in force.Descendants("FORMATION"))
                {
                    if (force.Attribute("NAME").Value != null)
                    {
                        formationNode = forceNode.Nodes.Add(formation.Attribute("NAME").Value);
                        formationNode.Tag = formation.Attribute("ID").Value;
                        formationNode.Name = "FORMATION";

                        //###CHANGE TREENODE COLOR IF FORMATION IS IMMOBILE
                        if (formation.Attribute("ORDERS").Value == "Static" || formation.Attribute("ORDERS").Value == "Wait" || formation.Attribute("ORDERS").Value == "Delay" || formation.Attribute("ORDERS").Value == "Hold" || formation.Attribute("ORDERS").Value == "Manual" || formation.Attribute("ORDERS").Value == "Garrison")
                        {
                            formationNode.ForeColor = System.Drawing.Color.IndianRed;
                            Font f = new Font(trvSFTree.Font, FontStyle.Bold);
                            formationNode.NodeFont = f;
                        }
                    }
                    else
                    {
                        formationNode = null;
                    }

                    //foreach (XElement unit in formation.Descendants("UNIT"))
                    //{
                    //    unitNode = formationNode.Nodes.Add(unit.Attribute("NAME").Value);
                    //    unitNode.Tag = unit.Attribute("ID").Value;
                    //    unitNode.Name = "UNIT";

                    //    //+++CHANGE TREENODE FONT COLOR IF UNIT IS DIVIDED
                    //    if (unit.Attribute("STATUS").Value == "24")
                    //    {
                    //        unitNode.ForeColor = System.Drawing.Color.Gray;
                    //    }

                    //    //***CHANGE TREENODE FONT COLOR IF UNIT IS SUBUNIT
                    //    if (unit.Attribute("PARENT") != null)
                    //    {
                    //        unitNode.ForeColor = System.Drawing.Color.CornflowerBlue;
                    //        Font f = new Font(trvSFTree.Font, FontStyle.Bold);
                    //        unitNode.NodeFont = f;
                    //    }

                    //    //^^^CHANGE TREENODE FONT COLOR IF UNIT IS REINFORCEMENT
                    //    if (unit.Attribute("X") == null && unit.Attribute("STATUS").Value != "24")
                    //    {
                    //        unitNode.ForeColor = System.Drawing.Color.ForestGreen;
                    //        Font f = new Font(trvSFTree.Font, FontStyle.Bold);
                    //        unitNode.NodeFont = f;
                    //    }

                    //    foreach (XElement equip in unit.Descendants("EQUIPMENT"))
                    //    {
                    //        unitNode.Tag = unit.Attribute("ID").Value;
                    //        TreeNode equipTnode = unitNode.Nodes.Add(equip.Attribute("NAME").Value + " x" + equip.Attribute("NUMBER").Value + " [" + equip.Attribute("MAX").Value + "]");
                    //        equipTnode.Tag = equip.Attribute("ID").Value;
                    //        equipTnode.Name = "EQUIPMENT";
                    //    }
                    //}
                }
            }
            trvSFTree.Nodes[0].Expand();
        }

        private void trvSFTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strUnitName;
            string strUnitID;

            if (trvSFTree.SelectedNode.Level == 1)
            {
                strUnitName = trvSFTree.SelectedNode.Text;
                strUnitID = trvSFTree.SelectedNode.Tag.ToString();
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
                MessageBox.Show("You must select a Formation!", "Select Formation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void rbForce1_CheckedChanged(object sender, EventArgs e)
        {
            SFLoadTree();
        }

        private void rbForce2_CheckedChanged(object sender, EventArgs e)
        {
            SFLoadTree();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            frmEvents f = Application
          .OpenForms
          .OfType<frmEvents>()
          .FirstOrDefault();

            string strFormationName = trvSFTree.SelectedNode.Text;
            string strFormationID = trvSFTree.SelectedNode.Tag.ToString();
            string strForce;

            if (rbForce1.Checked == true)
            {
                strForce = "1";
            }
            else
            {
                strForce = "2";
            }

            f.SetSelectedUnit(strForce, strFormationName, strFormationID);

            this.Close();
        }
    }
}
