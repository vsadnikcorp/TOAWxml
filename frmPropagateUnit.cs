using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TOAWXML
{
    public partial class frmPropagateUnit : Form
    {
        public frmPropagateUnit()
        {
            InitializeComponent();
        }

        private void frmPropagateUnit_Load(object sender, EventArgs e)
        {
            rbUnitPropForm.Checked = true;
            chkUnitProf.Checked = true;
            chkUnitSupply.Checked = true;
            chkLossTol.Checked = true;
            chkUnitReadiness.Checked = true;
            chkUnitDeploy.Checked = true;
            chkUnitReplace.Checked = true;
            chkExperience.Checked = true;
            chkIcon.Checked = true;

            //CHINESE
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            //END CHINESE
        }

        private void btnCancel4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUnitPropagate_Click(object sender, EventArgs e)
        {
            string strProficiency = Globals.GlobalVariables.PROFIC;
            string strSupply = Globals.GlobalVariables.SUPPLY;
            string strEmphasis = Globals.GlobalVariables.LOSSTOL;
            string strSuppScope = Globals.GlobalVariables.SUPPSCOPE;
            string strOrders = Globals.GlobalVariables.ORDERS;
            string strReadiness = Globals.GlobalVariables.READINESS;
            string strDeploy = Globals.GlobalVariables.DEPLOY;
            string strReplace = Globals.GlobalVariables.REPLACE;
            string strExperience = Globals.GlobalVariables.EXPERIENCE;
            string strIconColor = Globals.GlobalVariables.ICONCOLOR;
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);

            if (rbUnitPropForm.Checked == true)
            {
                var propUnitForm = (from f in xelem.Descendants("UNIT")
                                       where f.Parent.Parent.Attribute("ID").Value.ToString() == Globals.GlobalVariables.FORCE &&
                                       f.Parent.Attribute("ID").Value.ToString() == Globals.GlobalVariables.PARENTID
                                       select f);

                foreach (var fp in propUnitForm)
                {
                    if (chkUnitProf.Checked == true)
                    {
                        fp.Attribute("PROFICIENCY").Value = strProficiency;
                    }

                    if (chkUnitSupply.Checked == true)
                    {
                        fp.Attribute("SUPPLY").Value = strSupply;
                    }

                    if (chkLossTol.Checked == true)
                    {
                        fp.Attribute("EMPHASIS").Value = strEmphasis;
                    }

                    if (chkUnitReadiness.Checked == true)
                    {
                        fp.Attribute("READINESS").Value = strReadiness;
                    }

                    if (chkUnitDeploy.Checked == true)
                    {
                        fp.Attribute("STATUS").Value = strDeploy;
                    }
                    if (chkUnitReplace.Checked == true)
                    {
                        fp.Attribute("REPLACEMENTPRIORITY").Value = strReplace;
                    }
                    if (chkExperience.Checked == true)
                    {
                        fp.Attribute("EXPERIENCE").Value = strExperience;
                    }
                    if (chkIcon.Checked == true)
                    {
                        fp.Attribute("COLOR").Value = strIconColor;
                    }
                }

            }
                if (rbUnitPropAllUnits.Checked == true)
                {
                var propUnitAll = (from f in xelem.Descendants("UNIT")
                                    where f.Parent.Parent.Attribute("ID").Value.ToString() == Globals.GlobalVariables.FORCE 
                                    select f);

                foreach (var ff in propUnitAll)
                    {
                        if (chkUnitProf.Checked == true)
                        {
                            ff.Attribute("PROFICIENCY").Value = strProficiency;
                        }

                        if (chkUnitSupply.Checked == true)
                        {
                            ff.Attribute("SUPPLY").Value = strSupply;
                        }

                        if (chkLossTol.Checked == true)
                        {
                            ff.Attribute("EMPHASIS").Value = strEmphasis;
                        }

                        if (chkUnitReadiness.Checked == true)
                        {
                            ff.Attribute("READINESS").Value = strReadiness;
                        }

                        if (chkUnitDeploy.Checked == true)
                        {
                            ff.Attribute("STATUS").Value = strDeploy;
                        }
                        if (chkUnitReplace.Checked == true)
                        {
                            ff.Attribute("REPLACEMENTPRIORITY").Value = strReplace;
                        }
                        if (chkExperience.Checked == true)
                         {
                             ff.Attribute("EXPERIENCE").Value = strExperience;
                         }
                        if (chkIcon.Checked == true)
                        {
                            ff.Attribute("COLOR").Value = strIconColor;
                        }
                }
                }
                xelem.Save(Globals.GlobalVariables.PATH);
                this.Close();
        }
    }
}
