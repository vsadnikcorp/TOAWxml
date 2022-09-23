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
using TOAWXML;

namespace TOAWTac
{
    public partial class frmSyncGamTac : Form
    {
        public string dateTime;

        public frmSyncGamTac(String datetime)
        {
            InitializeComponent();
            dateTime = datetime;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectGam_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Title = "Open gam file to sync";
            file.Filter = "*.gam files *.gam|*.gam";

            if (file.ShowDialog() == DialogResult.OK)
            {
                txtSelectedGam.Text = file.FileName;
                btnSelectTac.Enabled = true;
            }
        }

        private void btnSelectTac_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Title = "Select tac file to modify";

            file.Filter = "*.tac files *.tac|*.tac";

            if (file.ShowDialog() == DialogResult.OK)
            {
                txtSelectedTac.Text = file.FileName;
                btnSync.Enabled = true;
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            string date = dateTime;

            string TacFileName = txtSelectedTac.Text;
            string GamFileName = txtSelectedGam.Text;

            XElement tacFile = XElement.Load(TacFileName);
            XElement gamFile = XElement.Load(GamFileName);

            foreach (XElement force in gamFile.Descendants("OOB").Descendants("FORCE"))
            {
                string forceID = force.Attribute("ID").Value;
                string forceName = force.Attribute("NAME").Value;
                string proficiency = force.Attribute("proficiency").Value;
                string supply = force.Attribute("supply").Value;
                                
                var tacForce = tacFile.Descendants("OOB").Descendants("FORCE")
                    .Where(f => f.Attribute("ID").Value == forceID)
                      .FirstOrDefault();

                if (tacForce == null)
                {
                    continue;
                }
                else
                {
                    tacForce.Attribute("proficiency").Value = proficiency;
                    tacForce.Attribute("supply").Value = supply;
                }

                foreach (XElement formation in force.Descendants("FORMATION"))
                {
                    string formID = formation.Attribute("ID").Value;
                    string formName = formation.Attribute("NAME").Value;
                    string formProf = formation.Attribute("PROFICIENCY").Value;
                    string formSupply = formation.Attribute("SUPPLY").Value;
                    string supportScope = formation.Attribute("SUPPORTSCOPE").Value;
                    string orders = formation.Attribute("ORDERS").Value;
                    string emphasis = formation.Attribute("EMPHASIS").Value;

                    var tacForm = tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION")
                        .Where(f => f.Parent.Attribute("ID").Value == forceID)
                        .Where(f=> f.Attribute("ID").Value == formID)
                        .FirstOrDefault(); 

                    if (tacForm == null)
                    {
                        //WHAT IF FORMATION MISSING FROM TAC FILE?  ADD?
                        continue;
                    }
                    else
                    {
                        tacForm.Attribute("PROFICIENCY").Value = formProf;
                        tacForm.Attribute("SUPPLY").Value = formSupply;
                        tacForm.Attribute("SUPPORTSCOPE").Value = supportScope;
                        tacForm.Attribute("ORDERS").Value = orders;
                        tacForm.Attribute("EMPHASIS").Value = emphasis;
                    }

                    foreach (XElement unit in formation.Descendants("UNIT"))
                    {
                        string unitID = unit.Attribute("ID").Value;
                        string unitIcon = unit.Attribute("ICON").Value;
                        string unitIconID = unit.Attribute("ICONID")?.Value;
                        string unitColor = unit.Attribute("COLOR").Value;
                        string unitName = unit.Attribute("NAME").Value;
                        string unitSize = unit.Attribute("SIZE").Value;
                        string unitExperience = unit.Attribute("EXPERIENCE").Value;
                        string unitChar = unit.Attribute("CHARACTERISTICS")?.Value;
                        string unitProf = unit.Attribute("PROFICIENCY").Value;
                        string unitReadiness = unit.Attribute("READINESS").Value;
                        string unitSupply = unit.Attribute("SUPPLY").Value;
                        string unitX = unit.Attribute("X")?.Value;
                        string unitY = unit.Attribute("Y")?.Value;
                        string unitEmphasis = unit.Attribute("EMPHASIS").Value;
                        string unitNext = unit.Attribute("NEXT")?.Value;
                        string unitStatus = unit.Attribute("STATUS").Value;
                        string unitReplacePriority = unit.Attribute("REPLACEMENTPRIORITY").Value;

                        if (unitChar == null) unitChar = "--";

                        var tacUnit = tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT")
                            .Where(f => f.Parent.Parent.Attribute("ID").Value == forceID)
                            .Where(f => f.Attribute("NAME").Value == unitName)
                            .FirstOrDefault();

                        if (tacUnit == null)
                        {
                            //ADD UNIT TO TAC FILE IF PRESENT IN GAM BUT NOT TAC
                            XElement newunit = Utils.AddUnitToTac(unit, date, unitName, formID, forceID);
                            tacForm.Add(newunit); 
                        }
                        else  //TRANSFER GAM UNIT VALUES TO TACFILE
                        {     
                            tacUnit.Attribute("ID").Value = unitID;
                            tacUnit.Attribute("ICON").Value = unitIcon;
                            if (unitIconID != null && tacUnit.Attribute("ICONID") != null)
                                { tacUnit.Attribute("ICONID").Value = unitIconID; }
                                else if (unitIconID != null && tacUnit.Attribute("ICONID") == null)
                                    { tacUnit.Add(new XAttribute("ICONID", unitIconID)); }
                            tacUnit.Attribute("COLOR").Value = unitColor;
                            tacUnit.Attribute("NAME").Value = unitName;
                            tacUnit.Attribute("SIZE").Value = unitSize;
                            tacUnit.Attribute("EXPERIENCE").Value = unitExperience;
                            if (tacUnit.Attribute("CHARACTERISTICS") != null) tacUnit.Attribute("CHARACTERISTICS").Value = unitChar;
                            tacUnit.Attribute("PROFICIENCY").Value = unitProf;
                            tacUnit.Attribute("READINESS").Value = unitReadiness;
                            tacUnit.Attribute("SUPPLY").Value = unitSupply;
                            if (unitX != null && tacUnit.Attribute("X") != null)
                                { tacUnit.Attribute("X").Value = unitX; }
                                else if (unitX != null && tacUnit.Attribute("X") == null)
                                    { tacUnit.Add(new XAttribute("X", unitX)); }
                            if (unitY != null && tacUnit.Attribute("Y") != null)
                                { tacUnit.Attribute("Y").Value = unitY; }
                                else if (unitY != null && tacUnit.Attribute("Y") == null)
                                { tacUnit.Add(new XAttribute("Y", unitY)); }
                            tacUnit.Attribute("EMPHASIS").Value = unitEmphasis;
                            if (unitNext != null && tacUnit.Attribute("NEXT") != null)
                                { tacUnit.Attribute("NEXT").Value = unitNext; }
                                else if (unitNext != null && tacUnit.Attribute("NEXT") == null)
                                    { tacUnit.Add(new XAttribute("NEXT", unitNext)); }
                            tacUnit.Attribute("STATUS").Value = unitStatus;
                            tacUnit.Attribute("REPLACEMENTPRIORITY").Value = unitReplacePriority;
                        }

                        //>>>>>>>>>>>>>  MUST COUNT AND ADD ITEMS
                        foreach (XElement equipment in unit.Descendants("EQUIPMENT"))
                        {
                            string eqpID = equipment.Attribute("ID").Value;
                            string eqpName = equipment.Attribute("NAME").Value;
                            string eqpNumber = equipment.Attribute("NUMBER").Value;
                            string eqpMax = equipment.Attribute("MAX").Value;
                            string eqpDamage = equipment.Attribute("DAMAGE").Value;

                            var tacEquip = tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT").Descendants("EQUIPMENT")
                            .Where(E => E.Parent.Parent.Parent.Attribute("ID").Value == forceID)
                            .Where(E => E.Parent.Attribute("NAME").Value == unitName)
                            .Where(E => E.Attribute("NAME").Value == eqpName)
                            .FirstOrDefault();

                            if (tacEquip == null)  //GAM EQUIP NOT IN TAC
                            {
                                XElement newequip = Utils.AddEquipToTac(unit, date, unitName, formID, forceID);
                                tacUnit.Add(newequip);
                            }
                            else //GAME EQUIP IN TAC
                            {
                                tacEquip.Attribute("ID").Value = eqpID;
                                tacEquip.Attribute("NAME").Value = eqpName;
                                tacEquip.Attribute("NUMBER").Value = eqpNumber;
                                tacEquip.Attribute("MAX").Value = eqpMax;
                                tacEquip.Attribute("DAMAGE").Value = eqpDamage;
                            }
                            
                            int tacItemCount = tacEquip.Descendants("ITEM").Count();
                            int gamQty = Int32.Parse(equipment.Attribute("NUMBER").Value);

                            if(tacItemCount ==gamQty)  //TAC FILES ITEMS MATCH GAM NUMBER
                            {
                                continue;
                            }
                            else if (tacItemCount < gamQty)  //TAC FILE HAS FEWER ITEMS THAN GAM NUMBER
                            {
                                int addToTac = gamQty - tacItemCount;

                                //ADD LOOP HERE
                                for (int j = 1; j <= addToTac; j++)
                                {
                                    int tacItemID = tacItemCount + 1;

                                    XElement copiedItem = tacEquip.Descendants("ITEM").FirstOrDefault();
                                    copiedItem.Attribute("ID").Value = tacItemID.ToString();
                                    copiedItem.Attribute("ITEMCDR").Value = "--";
                                    copiedItem.Attribute("ITEMEXP").Value = "40";
                                    copiedItem.Attribute("ITEMKILLS").Value = "0";
                                    copiedItem.Attribute("CASUALTY").Value = "None";
                                    copiedItem.Attribute("ITEMDAMAGE").Value = "0";
                                    copiedItem.Attribute("ITEMFORMDATE").Value = date;
                                    copiedItem.Attribute("ITEMNOTE").Value = "--";

                                    tacEquip.Add(copiedItem);
                                }
                            }
                            else if (tacItemCount > gamQty)  //TAC FILE HAS MORE ITEMS THAN GAM NUMBER
                            {
                                int removeFromTac = tacItemCount - gamQty;
                                for (int t = tacItemCount; t > gamQty; t--)
                                {
                                    string itemID = t.ToString();  
                                    tacEquip.Descendants("ITEM")
                                        .Attributes("ID")
                                        .Where(z => z.Value == itemID)
                                        .Remove();
                                }
                            }

                        }//equipment
                    }//unit-5.0
                }//formation
            }//force

            tacFile.Save(TacFileName);
            this.Close();
        }
    }
}
