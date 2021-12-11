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

            //List<string> toRemove = new System.Collections.Generic.List<string>();
                   
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
                        string unitChar = unit.Attribute("CHARACTERISTICS").Value;
                        string unitProf = unit.Attribute("PROFICIENCY").Value;
                        string unitReadiness = unit.Attribute("READINESS").Value;
                        string unitSupply = unit.Attribute("SUPPLY").Value;
                        string unitX = unit.Attribute("X")?.Value;
                        string unitY = unit.Attribute("Y")?.Value;
                        string unitEmphasis = unit.Attribute("EMPHASIS").Value;
                        string unitNext = unit.Attribute("NEXT")?.Value;
                        string unitStatus = unit.Attribute("STATUS").Value;
                        string unitReplacePriority = unit.Attribute("REPLACEMENTPRIORITY").Value;

                        var tacUnit = tacFile.Descendants("OOB").Descendants("FORCE").Descendants("FORMATION").Descendants("UNIT")
                        .Where(f => f.Parent.Parent.Attribute("ID").Value == forceID)
                        .Where(f => f.Attribute("NAME").Value == unitName)
                        .FirstOrDefault();

                        if (tacUnit == null)
                        {
                            //ADD UNIT TO TAC FILE IF PRESENT IN GAM BUT NOT TAC

                            string unitcdrname;
                            string iconid;
                            string x;
                            string y;
                            string next;

                            unitcdrname = Utils.AssignCdrName(forceID);

                            if (unit.Attribute("ICONID")?.Value != null)
                            {
                                iconid = unit.Attribute("ICONID").Value;
                            }
                            else
                            {
                                iconid = "";
                            }

                            if (unit.Attribute("X")?.Value != null)
                            {
                                x = unit.Attribute("X").Value;
                            }
                            else
                            {
                                x = "";
                            }

                            if (unit.Attribute("Y")?.Value != null)
                            {
                                y = unit.Attribute("Y").Value;
                            }
                            else
                            {
                                y = "";
                            }

                            if (unit.Attribute("NEXT")?.Value != null)
                            {
                                next = unit.Attribute("NEXT").Value;
                            }
                            else
                            {
                                next = "";
                            }
                            //}

                            //ADD MISSING UNIT TO TACFILE
                            XElement newunit =
                                new XElement("UNIT",
                                new XAttribute("ID", unit.Attribute("ID").Value),
                                new XAttribute("NAME", unit.Attribute("NAME").Value),
                                new XAttribute("ICON", unit.Attribute("ICON").Value),
                                new XAttribute("ICONID", iconid),
                                new XAttribute("COLOR", unit.Attribute("COLOR").Value),
                                new XAttribute("SIZE", unit.Attribute("SIZE").Value),
                                new XAttribute("EXPERIENCE", unit.Attribute("EXPERIENCE").Value),
                                new XAttribute("CHARACTERISTICS", unit.Attribute("CHARACTERISTICS").Value), 
                                new XAttribute("PROFICIENCY", unit.Attribute("PROFICIENCY").Value), 
                                new XAttribute("READINESS", unit.Attribute("READINESS").Value),
                                new XAttribute("SUPPLY", unit.Attribute("SUPPLY").Value),
                                new XAttribute("X", x), 
                                new XAttribute("Y", y), 
                                new XAttribute("EMPHASIS", unit.Attribute("EMPHASIS").Value),
                                new XAttribute("NEXT", next),
                                new XAttribute("STATUS", unit.Attribute("STATUS").Value),
                                new XAttribute("REPLACEMENTPRIORITY", unit.Attribute("REPLACEMENTPRIORITY").Value), 
                                new XAttribute("CDR", unitcdrname),
                                new XAttribute("RANK", "LT"),
                                new XAttribute("RATING", "--"),
                                new XAttribute("FORMDATE", date));

                                string equipcdrname;
                                bool isFirstEqp = true;
                                int n = 0;

                            //EQUIPMENT
                            foreach (XElement equip in unit.Descendants("EQUIPMENT")
                                    .Where(z => z.Parent.Attribute("NAME").Value == unitName)
                                    .Where(z => z.Parent.Parent.Attribute("ID").Value == formID)
                                    .Where(z => z.Parent.Parent.Parent.Attribute("ID").Value == forceID))
                            {
                                    int qty = Int32.Parse(equip.Attribute("NUMBER").Value);
                                    string eqpID = equip.Attribute("ID").Value;
                                    string eqpName = equip.Attribute("NAME").Value;
                                    string eqpNumber = equip.Attribute("NUMBER").Value;
                                    string eqpMax = equip.Attribute("MAX").Value;
                                    string eqpDamage = equip.Attribute("DAMAGE").Value;

                                newunit.Add(
                                       new XElement("EQUIPMENT",
                                       new XAttribute("ID", eqpID),
                                       new XAttribute("NAME", eqpName),
                                       new XAttribute("NUMBER", eqpNumber),
                                       new XAttribute("MAX", eqpMax),
                                       new XAttribute("DAMAGE", eqpDamage)));

                                        for (int i = 1; i <= qty; i++) //ITEM
                                        {
                                                if (isFirstEqp == true)
                                                {
                                                    equipcdrname = newunit.Attribute("CDR").Value;
                                                }
                                                else if ((isFirstEqp != true) &&
                                                (newunit.Attribute("ICON").Value == "Air" ||
                                                newunit.Attribute("ICON").Value == "Fighter Bomber" ||
                                                newunit.Attribute("ICON").Value == "Light Bomber" ||
                                                newunit.Attribute("ICON").Value == "Medium Bomber" ||
                                                newunit.Attribute("ICON").Value == "Naval Bomber" ||
                                                newunit.Attribute("ICON").Value == "Heavy Bomber" ||
                                                newunit.Attribute("ICON").Value == "Jet Bomber" ||
                                                newunit.Attribute("ICON").Value == "Heavy Jet Bomber" ||
                                                newunit.Attribute("ICON").Value == "Fighter" ||
                                                newunit.Attribute("ICON").Value == "Jet Fighter" ||
                                                newunit.Attribute("ICON").Value == "Naval Fighter" ||
                                                newunit.Attribute("ICON").Value == "Riverine" ||
                                                newunit.Attribute("ICON").Value == "Light Naval" ||
                                                newunit.Attribute("ICON").Value == "Medium Naval" ||
                                                newunit.Attribute("ICON").Value == "Naval Task Force" ||
                                                newunit.Attribute("ICON").Value == "Naval Attack"))
                                                {
                                                    equipcdrname = Utils.AssignCdrName(forceID);
                                                }
                                                else
                                                {
                                                    equipcdrname = "--";
                                                }
                                                n++;

                                            newunit.Elements("EQUIPMENT").Last().Add(
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
                                                continue;
                                        } //item
                            }  //^^END ADD OF MISSING UNIT TO TACFILE^^

                            tacForm.Add(newunit); //RIGHT SPOT?
                        }
                        else  //TRANSFER GAM UNIT VALUES TO TACFILE
                        {     
                            tacUnit.Attribute("ID").Value = unitID;
                            tacUnit.Attribute("ICON").Value = unitIcon;
                            if (unitIconID != null && tacUnit.Attribute("ICONID") !=null) tacUnit.Attribute("ICONID").Value = unitIconID;
                            tacUnit.Attribute("COLOR").Value = unitColor;
                            tacUnit.Attribute("NAME").Value = unitName;
                            tacUnit.Attribute("SIZE").Value = unitSize;
                            tacUnit.Attribute("EXPERIENCE").Value = unitExperience;
                            tacUnit.Attribute("CHARACTERISTICS").Value = unitChar;
                            tacUnit.Attribute("PROFICIENCY").Value = unitProf;
                            tacUnit.Attribute("READINESS").Value = unitReadiness;
                            tacUnit.Attribute("SUPPLY").Value = unitSupply;
                            if (unitX != null && tacUnit.Attribute("X") != null) tacUnit.Attribute("X").Value = unitX;
                            if (unitY != null && tacUnit.Attribute("Y") != null) tacUnit.Attribute("Y").Value = unitY;
                            tacUnit.Attribute("EMPHASIS").Value = unitEmphasis;
                            if (unitNext != null && tacUnit.Attribute("NEXT") != null) tacUnit.Attribute("NEXT").Value = unitNext;
                            tacUnit.Attribute("STATUS").Value = unitStatus;
                            tacUnit.Attribute("REPLACEMENTPRIORITY").Value = unitReplacePriority;
                        }

                        int u = 0; //number of equipment items in unit

                        foreach (XElement equipment in unit.Descendants("EQUIPMENT"))
                        {
                            //float f = 0;  ///number of items in equipment

                            //foreach (XElement item in equipment.Descendants("ITEM"))
                            //{
                            //    if (item.Attribute("CASUALTY").Value == "None")
                            //    {
                            //        f++;
                            //        u++;
                            //    }
                            //    if (item.Attribute("CASUALTY").Value == "Half")
                            //    {
                            //        f = f + 0.5f;
                            //    }
                            //}//item

                            //int i = (int)Math.Round(f, MidpointRounding.AwayFromZero);

                            //equipment.Attribute("NUMBER").Value = i.ToString();
                            //equipment.Descendants("ITEM").Remove();
                        }//equipment

                        //if (u == 0) toRemove.Add(unitID);
                    }//unit-5.0
                }//formation

                //if (toRemove.Count > 0) //IF AT LEAST ONE UNIT HAS NO EQP REMAINING
                //{
                //    //REMOVE UNITS WITH NO ITEMS
                //    foreach (string uid in toRemove)
                //    {
                //        DeleteUnit(gamFile, forceID, uid);
                //    }

                //    ////[RENUMBER UNIT IDs IF AT LEAST ONE UNIT IS DELETED]
                //    string xpath = "OOB/FORCE[@ID=" + forceID + "]/FORMATION/UNIT";
                //    var allunits = gamFile.XPathSelectElements(xpath);
                //    Renumbering.RenumberAll(allunits);

                //}
                //toRemove.Clear();

            }//force

            ////string tacFileName = txtTacFile.Text;
            //string GamFileName = TacFileName.Substring(0, TacFileName.Length - 4) + " " + date + ".gam";
            //txtGamFile.Text = GamFileName;
            gamFile.Save(GamFileName);
            tacFile.Save(TacFileName);
            this.Close();
        }
    }
}
