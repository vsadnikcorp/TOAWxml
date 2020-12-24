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
    public partial class frmPropagateForm : Form
    {
        public frmPropagateForm()
        {
            InitializeComponent();
        }

        private void frmPropagateForm_Load(object sender, EventArgs e)
        {
            rbFormPropSubunits.Checked = true;
            chkFormProf.Checked = true;
            chkFormSupply.Checked = true;
            chkFormLossTol.Checked = true;
            chkFormSuppScope.Enabled = false;
            chkFormOrders.Enabled = false;

            //CHINESE
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            //END CHINESE

        }

        private void btnCancel3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbFormPropForms_CheckedChanged(object sender, EventArgs e)
        {

            chkFormSuppScope.Enabled = true;
            chkFormOrders.Enabled = true;

            chkFormProf.Checked = true;
            chkFormSupply.Checked = true;
            chkFormLossTol.Checked = true;
            chkFormSuppScope.Checked = true;
            chkFormOrders.Checked = true;
        }

        private void rbFormPropSubunits_CheckedChanged(object sender, EventArgs e)
        {
            chkFormSuppScope.Enabled = false;
            chkFormOrders.Enabled = false;

            chkFormProf.Checked = true;
            chkFormSupply.Checked = true;
            chkFormLossTol.Checked = true;
            chkFormSuppScope.Checked = false;
            chkFormOrders.Checked = false;
        }

        private void btnFormPropagate_Click(object sender, EventArgs e)
        {
            string strProficiency = Globals.GlobalVariables.PROFIC;
            string strSupply = Globals.GlobalVariables.SUPPLY;
            string strEmphasis = Globals.GlobalVariables.LOSSTOL;
            string strSuppScope = Globals.GlobalVariables.SUPPSCOPE;
            string strOrders = Globals.GlobalVariables.ORDERS;
            string strReadiness = Globals.GlobalVariables.READINESS;
            string strDeploy = Globals.GlobalVariables.DEPLOY;
            string strReplace = Globals.GlobalVariables.REPLACE;

            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);

            if (rbFormPropSubunits.Checked == true)
            {
                var propFormSubunit = (from f in xelem.Descendants("UNIT")
                                       where f.Parent.Parent.Attribute("ID").Value.ToString() == Globals.GlobalVariables.FORCE &&
                                       f.Parent.Attribute("ID").Value.ToString() == Globals.GlobalVariables.ID
                                       select f);

                foreach (var fp in propFormSubunit)
                {
                    if (chkFormProf.Checked == true)
                    {
                        fp.Attribute("PROFICIENCY").Value = strProficiency;
                    }
                    if (chkFormSupply.Checked == true)
                    {
                        fp.Attribute("SUPPLY").Value = strSupply;
                    }
                    if (chkFormLossTol.Checked == true)
                    {
                        fp.Attribute("EMPHASIS").Value = strEmphasis;
                    }
                }
            }
                if (rbFormPropForms.Checked == true)
                {
                    var propFormForm = (from f in xelem.Descendants("FORMATION")
                                        where f.Parent.Attribute("ID").Value.ToString() == Globals.GlobalVariables.FORCE //&&
                                        select f);

                    foreach (var ff in propFormForm)
                    {
                        if (chkFormProf.Checked == true)
                        {
                            ff.Attribute("PROFICIENCY").Value = strProficiency;
                        }

                        if (chkFormSupply.Checked == true)
                        {
                            ff.Attribute("SUPPLY").Value = strSupply;
                        }

                        if (chkFormLossTol.Checked == true)
                        {
                            ff.Attribute("EMPHASIS").Value = strEmphasis;
                        }

                        if(chkFormSuppScope.Checked ==true)
                        {
                        ff.Attribute("SUPPORTSCOPE").Value = strSuppScope;
                        }

                        if(chkFormOrders.Checked == true)
                        {
                            ff.Attribute("ORDERS").Value = strOrders;
                        }
                    }
                }
                xelem.Save(Globals.GlobalVariables.PATH);
                this.Close();
        }
    }
}
