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
    public partial class frmUnitDivide : Form
    {
        xmlform xmlf;

        public frmUnitDivide(xmlform f)
        {
            //FormMain xmlform;
            InitializeComponent();
            this.ActiveControl = btnCancelDiv;
            xmlf = f;

            //^^^^^^^^^^^^^^^^^^^
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string unitid = xmlf.getUnitID();
            
            string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = xelem.XPathSelectElement(xpath);
            string xpathUnit = "copied"; //FILLER CONTENT
            TreeNode copiedNode = xmlf.getSelectedNode();
            Globals.GlobalVariables.COPIEDID = copiedNode.Tag.ToString();
            xpathUnit = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.COPIEDID + "]";
            var unit2 = xelem.XPathSelectElement(xpathUnit);
            string unitSize = unit2.Attribute("SIZE").Value.ToString();
            string textName = unit2.Attribute("NAME").Value.ToString();

            if (unit2.Attribute("PARENT") != null)
            {
                btn2Subs.Enabled = false;
                btn3Subs.Enabled = false;
                lblCannotDivide.Text = "A divided unit's subunit cannot be divided";
                lblCannotDivide.ForeColor = Color.Red;
            }

            if (unitSize == "Section")
            {
                btn2Subs.Enabled = false;
                btn3Subs.Enabled = false;
                lblCannotDivide.Text = "Section-sized units cannot be divided";
                lblCannotDivide.ForeColor = Color.Red;
            }
            //^^^^^^^^^^^^^^^^
            xmlf.SetSelectedTreeNodebyTag(copiedNode);
        }

        private void btnCancelDiv_Click(object sender, EventArgs e)
        {
            this.Close();
            return;
        }

        private void btn2Subs_Click(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string unitid = xmlf.getUnitID();
            string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = xelem.XPathSelectElement(xpath);
            Globals.GlobalVariables.UNITDIVIDER = "2";
            //***********
            string xpathParent = "parent"; //FILLER CONTENT
            string xpathCopied = "copied"; //FILLER CONTENT

            //COPY IN TREEVIEW
            TreeNode copiedNode = xmlf.getSelectedNode();
            TreeNode parentNode = copiedNode.Parent;
            string strParentID = parentNode.Tag.ToString();
            string strcopiedText = copiedNode.Text;
            Globals.GlobalVariables.COPIEDID = copiedNode.Tag.ToString();
            Globals.GlobalVariables.COPIEDPARENTID = strParentID;

            xpathParent = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION";
            xpathCopied = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.COPIEDID + "]";
            var parent = xelem.XPathSelectElement(xpathParent);
            var copied = xelem.XPathSelectElement(xpathCopied);
            var maxid = parent.Parent.Descendants("UNIT").Attributes("ID").ToList();
            int oldMax = parent.Parent.Descendants("UNIT").Max(m => (int)m.Attribute("ID"));

            XElement cloneNode = new XElement(copied);
            parentNode = copiedNode.Parent;
            string copiedSize = copied.Attribute("SIZE").Value.ToString();
            double dblSubProficiency = 0;
            dblSubProficiency = double.Parse(copied.Attribute("PROFICIENCY").Value.ToString()) * 0.8;
            var subProf = Math.Round(dblSubProficiency, 0, MidpointRounding.AwayFromZero);

            //ADD CLONED XMLNODE FOR SUB1
            int newMax = oldMax + 1;
            string subSize = sizeDown(copiedSize);
            cloneNode.Attribute("SIZE").Value = subSize;
            cloneNode.Attribute("ID").Value = newMax.ToString();
            cloneNode.Attribute("NAME").Value = "Sub 1 of " + copied.Attribute("NAME").Value.ToString();
            cloneNode.Attribute("PROFICIENCY").Value = subProf.ToString();
            cloneNode.Add(new XAttribute("PARENT", Globals.GlobalVariables.COPIEDID.ToString()));
            copied.AddAfterSelf(cloneNode);

            //ADD CLONED TREENODE FOR SUB1
            TreeNode copiedClone = (TreeNode)xmlf.getSelectedNode().Clone();
            copiedClone.Text = "Sub 1 of " + strcopiedText;
            copiedClone.Tag = newMax.ToString();
            copiedClone.ForeColor = System.Drawing.Color.CornflowerBlue;
            parentNode.Nodes.Insert(copiedNode.Index + 1, copiedClone);

            //ADD CLONED XML NODE FOR SUB2
            XElement cloneNode2 = new XElement(copied);
            newMax = newMax + 1;
            cloneNode2.Attribute("SIZE").Value = subSize;
            cloneNode2.Attribute("ID").Value = newMax.ToString();
            cloneNode2.Attribute("NAME").Value = "Sub 2 of " + copied.Attribute("NAME").Value.ToString();
            cloneNode2.Attribute("PROFICIENCY").Value = subProf.ToString();
            cloneNode2.Add(new XAttribute("PARENT", Globals.GlobalVariables.COPIEDID.ToString()));
            cloneNode.AddAfterSelf(cloneNode2);

            //ADD CLONED TREENODE FOR SUB2
            TreeNode copiedClone2 = (TreeNode)xmlf.getSelectedNode().Clone();
            copiedClone2.Text = "Sub 2 of " + strcopiedText;
            copiedClone2.Tag = newMax.ToString();
            copiedClone2.ForeColor = System.Drawing.Color.CornflowerBlue; 
            parentNode.Nodes.Insert(copiedNode.Index + 2, copiedClone2);
            //^^^^^^^^^^^^^^^^^^^^^^

            //SET ATTRIBS FOR DIVIDED UNIT
            copied.Attribute("STATUS").Value = "24";
            copied.Add(new XAttribute("GOINGTOX", "--"));
            copied.Add(new XAttribute("GOINGTOY", "--"));
            XAttribute attx = copied.Attribute("X");
            XAttribute atty = copied.Attribute("Y");
            attx.Remove();
            atty.Remove();
            int intDivider = int.Parse(Globals.GlobalVariables.UNITDIVIDER);
            //+++++++++++++++++++++++++

            //ASSIGN SUB1 EQUIP QTY AND MAX QTY IN XML
            foreach (XElement clone1Equip in cloneNode.Descendants("EQUIPMENT"))
                {
                    int intQty = int.Parse(clone1Equip.Attribute("NUMBER").Value.ToString());
                    int intMaxQty = int.Parse(clone1Equip.Attribute("MAX").Value.ToString());
                    int intR = intQty % intDivider;
                    int intSub1 = 0;
                    int intMaxQ1 = 0;
                    if (intR == 0)
                    {
                        intSub1 = intQty / intDivider;
                        intMaxQ1 = intMaxQty / intDivider;
                    }
                    else
                    {
                        intSub1 = (intQty / intDivider) + 1;
                        intMaxQ1 = intMaxQty / intDivider + 1;
                    }
                    clone1Equip.Attribute("NUMBER").Value = intSub1.ToString();
                    clone1Equip.Attribute("MAX").Value = intMaxQ1.ToString();
                }

            //ASSIGN SUB2 EQUIP QTY AND MAX QTY IN XML
            foreach (XElement clone2Equip in cloneNode2.Descendants("EQUIPMENT"))
                {
                    int intQty = int.Parse(clone2Equip.Attribute("NUMBER").Value.ToString());
                    int intMaxQty = int.Parse(clone2Equip.Attribute("MAX").Value.ToString());
                    int intR = intQty % intDivider;
                    int intSub2 = intQty / intDivider;
                    int intMaxQ2 = intMaxQty / intDivider;
                    clone2Equip.Attribute("NUMBER").Value = intSub2.ToString();
                    clone2Equip.Attribute("MAX").Value = intMaxQ2.ToString();
                }

            //ZERO OUT PARENT'S EQUIPMENT IN XML
            foreach (XElement equip in unit.Descendants("EQUIPMENT"))
            {
                equip.Attribute("NUMBER").Value = "0";
            }
            
            //SET CBODEPLOYMENT TO DIVIDED
            xmlf.DivideUnit();

            //DISABLE UNIT ATTRIBUTES FOR DIVIDED UNITS
            xmlf.DivideUnitGUI();

            parentNode.Expand();
            xelem.Save(Globals.GlobalVariables.PATH);
            xmlf.ReloadTree(copiedNode);
            this.Close();
            return;
        }

        private void btn3Subs_Click(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string unitid = xmlf.getUnitID();
            string xpath = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + unitid + "]";
            var unit = xelem.XPathSelectElement(xpath);
            Globals.GlobalVariables.UNITDIVIDER = "3";

            //***********
            string xpathParent = "parent"; //FILLER CONTENT
            string xpathCopied = "copied"; //FILLER CONTENT

            //COPY IN TREEVIEW
            TreeNode copiedNode = xmlf.getSelectedNode();
            TreeNode parentNode = copiedNode.Parent;
            string strParentID = parentNode.Tag.ToString();
            string strcopiedText = copiedNode.Text;
            Globals.GlobalVariables.COPIEDID = copiedNode.Tag.ToString();
            Globals.GlobalVariables.COPIEDPARENTID = strParentID;

            xpathParent = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION";
            xpathCopied = "OOB/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/FORMATION/UNIT[@ID =" + Globals.GlobalVariables.COPIEDID + "]";
            var parent = xelem.XPathSelectElement(xpathParent);
            var copied = xelem.XPathSelectElement(xpathCopied);
            var maxid = parent.Parent.Descendants("UNIT").Attributes("ID").ToList();
            int oldMax = parent.Parent.Descendants("UNIT").Max(m => (int)m.Attribute("ID"));
            XElement cloneNode = new XElement(copied);
            parentNode = copiedNode.Parent;
            string copiedSize = copied.Attribute("SIZE").Value.ToString();
            double dblSubProficiency = 0;
            dblSubProficiency = double.Parse(copied.Attribute("PROFICIENCY").Value.ToString()) * 0.8;
            var subProf = Math.Round(dblSubProficiency, 0, MidpointRounding.AwayFromZero);

            //111111111111111111111111111
            //ADD CLONED XMLNODE FOR SUB1
            int newMax = oldMax + 1;
            string subSize = sizeDown(copiedSize);
            cloneNode.Attribute("SIZE").Value = subSize;
            cloneNode.Attribute("ID").Value = newMax.ToString();
            cloneNode.Attribute("NAME").Value = "Sub 1 of " + copied.Attribute("NAME").Value.ToString();
            cloneNode.Attribute("PROFICIENCY").Value = subProf.ToString();
            cloneNode.Add(new XAttribute("PARENT", Globals.GlobalVariables.COPIEDID.ToString()));
            copied.AddAfterSelf(cloneNode);

            //ADD CLONED TREENODE FOR SUB1
            TreeNode copiedClone = (TreeNode)xmlf.getSelectedNode().Clone();
            copiedClone.Text = "Sub 1 of " + strcopiedText;
            copiedClone.Tag = newMax.ToString();
            copiedClone.ForeColor = System.Drawing.Color.CornflowerBlue;
            parentNode.Nodes.Insert(copiedNode.Index + 1, copiedClone);
            //11111111111111111111111111

            //22222222222222222222222222
            //ADD CLONED XML NODE FOR SUB2
            XElement cloneNode2 = new XElement(copied);
            newMax = newMax + 1;
            cloneNode2.Attribute("SIZE").Value = subSize;
            cloneNode2.Attribute("ID").Value = newMax.ToString();
            cloneNode2.Attribute("NAME").Value = "Sub 2 of " + copied.Attribute("NAME").Value.ToString();
            cloneNode2.Attribute("PROFICIENCY").Value = subProf.ToString();
            cloneNode2.Add(new XAttribute("PARENT", Globals.GlobalVariables.COPIEDID.ToString()));
            cloneNode.AddAfterSelf(cloneNode2);

            //ADD CLONED TREENODE FOR SUB2
            TreeNode copiedClone2 = (TreeNode)xmlf.getSelectedNode().Clone();
            copiedClone2.Text = "Sub 2 of " + strcopiedText;
            copiedClone2.Tag = newMax.ToString();
            copiedClone2.ForeColor = System.Drawing.Color.CornflowerBlue;
            parentNode.Nodes.Insert(copiedNode.Index + 2, copiedClone2);
            //22222222222222222222222222222

            //33333333333333333333333333333
            //ADD CLONED XML NODE FOR SUB3
            XElement cloneNode3 = new XElement(copied);
            newMax = newMax + 1;
            cloneNode3.Attribute("SIZE").Value = subSize;
            cloneNode3.Attribute("ID").Value = newMax.ToString();
            cloneNode3.Attribute("NAME").Value = "Sub 3 of " + copied.Attribute("NAME").Value.ToString();
            cloneNode3.Attribute("PROFICIENCY").Value = subProf.ToString();
            cloneNode3.Add(new XAttribute("PARENT", Globals.GlobalVariables.COPIEDID.ToString()));
            cloneNode2.AddAfterSelf(cloneNode3);

            //ADD CLONED TREENODE FOR SUB3
            TreeNode copiedClone3 = (TreeNode)xmlf.getSelectedNode().Clone();
            copiedClone3.Text = "Sub 3 of " + strcopiedText;
            copiedClone3.Tag = newMax.ToString();
            copiedClone3.ForeColor = System.Drawing.Color.CornflowerBlue;
            parentNode.Nodes.Insert(copiedNode.Index + 3, copiedClone3);
            //33333333333333333333333333333

            //SET ATTRIBS FOR DIVIDED UNIT
            copied.Attribute("STATUS").Value = "24";
            copied.Add(new XAttribute("GOINGTOX", "--"));
            copied.Add(new XAttribute("GOINGTOY", "--"));
            XAttribute attx = copied.Attribute("X");
            XAttribute atty = copied.Attribute("Y");
            attx.Remove();
            atty.Remove();
            //+++++++++++++++++++++++++

            int intDivider = int.Parse(Globals.GlobalVariables.UNITDIVIDER);

            //ASSIGN SUB1 EQUIP QTY AND MAX QTY IN XML
            //11111111111111111111111111
            foreach (XElement clone1Equip in cloneNode.Descendants("EQUIPMENT"))
            {
                int intQty = int.Parse(clone1Equip.Attribute("NUMBER").Value.ToString());
                int intMaxQty = int.Parse(clone1Equip.Attribute("MAX").Value.ToString());
                int intR = intQty % intDivider;
                int intSub1 = 0;
                int intMaxQ1 = 0;
                if (intR > 0)
                {
                    intSub1 = (intQty / intDivider) + 1;
                    intMaxQ1 = intMaxQty / intDivider + 1;
                }
                else
                {
                    intSub1 = intQty / intDivider;
                    intMaxQ1 = intMaxQty / intDivider;
                }
                clone1Equip.Attribute("NUMBER").Value = intSub1.ToString();
                clone1Equip.Attribute("MAX").Value = intMaxQ1.ToString();
            }
            //1111111111111111111111111111

            //2222222222222222222222
            //ASSIGN SUB2 EQUIP QTY AND MAX QTY IN XML
            foreach (XElement clone2Equip in cloneNode2.Descendants("EQUIPMENT"))
            {
                int intQty = int.Parse(clone2Equip.Attribute("NUMBER").Value.ToString());
                int intMaxQty = int.Parse(clone2Equip.Attribute("MAX").Value.ToString());
                int intR = intQty % intDivider;
                int intSub2 = intQty / intDivider;
                int intMaxQ2 = intMaxQty / intDivider;

                //IF THERE IS A REMAINDER...
                if (intR == 2)
                {
                    intSub2 = intQty / intDivider + 1;
                    intMaxQ2 = intMaxQty / intDivider + 1;
                }
                else
                {
                    intSub2 = (intQty / intDivider);
                    intMaxQ2 = intMaxQty / intDivider;
                }

                clone2Equip.Attribute("NUMBER").Value = intSub2.ToString();
                clone2Equip.Attribute("MAX").Value = intMaxQ2.ToString();
            }
            //222222222222222222222

            //3333333333333333333
            foreach (XElement clone3Equip in cloneNode3.Descendants("EQUIPMENT"))
            {
                int intQty = int.Parse(clone3Equip.Attribute("NUMBER").Value.ToString());
                int intMaxQty = int.Parse(clone3Equip.Attribute("MAX").Value.ToString());
                int intR = intQty % intDivider;
                int intSub3 = intQty / intDivider;
                int intMaxQ3 = intMaxQty / intDivider;
                intSub3 = (intQty / intDivider);
                intMaxQ3 = intMaxQty / intDivider;

                clone3Equip.Attribute("NUMBER").Value = intSub3.ToString();
                clone3Equip.Attribute("MAX").Value = intMaxQ3.ToString();
            }
            //3333333333333333333

            //ZERO OUT PARENT'S EQUIPMENT IN XML
            foreach (XElement equip in unit.Descendants("EQUIPMENT"))
            {
                equip.Attribute("NUMBER").Value = "0";
            }

            //SET CBODEPLOYMENT TO DIVIDED
            xmlf.DivideUnit();

            //DISABLE UNIT ATTRIBUTES FOR DIVIDED UNITS
            xmlf.DivideUnitGUI();

            parentNode.Expand();
            xelem.Save(Globals.GlobalVariables.PATH);
            xmlf.ReloadTree(copiedNode);
            this.Close();
            return;
        }

        private string sizeDown(string copiedSize)
        {
            string subSize = "size";
            switch (copiedSize)
            {
                case "Platoon":
                    subSize = "Section";
                    break;
                case "Company":
                    subSize = "Platoon";
                    break;
                case "Battalion":
                    subSize = "Company";
                    break;
                case "Regiment":
                    subSize = "Battalion";
                    break;
                case "Brigade":
                    subSize = "Regiment";
                    break;
                case "Division":
                    subSize = "Brigade";
                    break;
                case "Corps":
                    subSize = "Division";
                    break;
                case "Army":
                    subSize = "Corps";
                    break;
                case "Army Group":
                    subSize = "Army";
                    break;
                case "Theater":
                    subSize = "Army Group";
                    break;
                case "Supreme Command":
                    subSize = "Theater";
                    break;
            }
            return subSize;
        }
      
    }
}
