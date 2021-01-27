using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
//CHINESE
using System.Globalization;
using System.Threading;
//END CHINESE

namespace TOAWXML
{
    public partial class frmScenSettings : Form
    {
        public frmScenSettings()
        {
            InitializeComponent();
        }

        private void frmScenSettings_Load(object sender, EventArgs e)
        {
            //XPATH FOR SCENARIO VARIABLES PORTION OF XML
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpathvariables = "VARIABLES";
            var variables = xelem.XPathSelectElement(xpathvariables);

            //ASSIGN DATA TO TEXT BOXES 
            txtEngineVariable.Text = variables.Attribute("eventEngineVariable").Value.ToString();
            txtAttritionDivider.Text = variables.Attribute("attritionDivider").Value.ToString();
            txtNavAttritionDivider.Text = variables.Attribute("navalAttritionDivider").Value.ToString();
            txtMRPB.Text = variables.Attribute("maxRoundsPerBattle").Value.ToString();
            txtAALethalityRate.Text = variables.Attribute("AAALethalityRate").Value.ToString();
            txtCombDensRate.Text = variables.Attribute("combatDensityRate").Value.ToString();
            txtScenOver.Text = variables.Attribute("scenarioIsOver").Value.ToString();
            txtCeasefire.Text = variables.Attribute("ceaseFire").Value.ToString();
            txtEngRate.Text = variables.Attribute("engineeringRate").Value.ToString();
            txtEntrenchRate.Text = variables.Attribute("entrenchmentRate").Value.ToString();
            txtRoadCost.Text = variables.Attribute("roadCost").Value.ToString();
            txtHexConvRate.Text = variables.Attribute("hexConversionRate").Value.ToString();
            txtSuppMoveRate.Text = variables.Attribute("supplyMovementRate").Value.ToString();
            txtSuppReadRate.Text = variables.Attribute("supplyReadinessRate").Value.ToString();
            txtNewMudRules.Text = variables.Attribute("newMudRulesScalar").Value.ToString();
            txtRiversHexside.Text = variables.Attribute("riversAlongEdges").Value.ToString();
            btnCloseScenSettings.Select();
            //CHINESE
            var culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            foreach (Control c in this.Controls)
            {
                ComponentResourceManager resources = new ComponentResourceManager(typeof(frmScenSettings));
                resources.ApplyResources(c, c.Name, new CultureInfo("en-US"));
            }
            //END CHINESE
        }

        private void btnCloseScenSettings_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSaveScenSettings_Click(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(TOAWXML.Properties.Settings.Default.FilePath);
            string xpathvariables = "VARIABLES";
            var variables = xelem.XPathSelectElement(xpathvariables);

            variables.Attribute("eventEngineVariable").Value = txtEngineVariable.Text;
            variables.Attribute("attritionDivider").Value = txtAttritionDivider.Text;
            variables.Attribute("navalAttritionDivider").Value = txtNavAttritionDivider.Text;
            variables.Attribute("maxRoundsPerBattle").Value = txtMRPB.Text;
            variables.Attribute("AAALethalityRate").Value = txtAALethalityRate.Text;
            variables.Attribute("combatDensityRate").Value = txtCombDensRate.Text;
            variables.Attribute("scenarioIsOver").Value = txtScenOver.Text;
            variables.Attribute("ceaseFire").Value = txtCeasefire.Text;
            variables.Attribute("engineeringRate").Value = txtEngRate.Text;
            variables.Attribute("entrenchmentRate").Value = txtEntrenchRate.Text;
            variables.Attribute("roadCost").Value = txtRoadCost.Text;
            variables.Attribute("hexConversionRate").Value = txtHexConvRate.Text;
            variables.Attribute("supplyMovementRate").Value = txtSuppMoveRate.Text;
            variables.Attribute("supplyReadinessRate").Value = txtSuppReadRate.Text;
            variables.Attribute("newMudRulesScalar").Value = txtNewMudRules.Text;
            variables.Attribute("riversAlongEdges").Value = txtRiversHexside.Text;

            xelem.Save(TOAWXML.Properties.Settings.Default.FilePath);

            btnCloseScenSettings.Select();
        }
      
        private void txtEngineVariable_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtEngineVariable.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtEngineVariable.Text, out var intValue))
            {
                errorProvider2.SetError(txtEngineVariable, "Please enter non-negative whole number between 0 and 999.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 999))
                {
                    errorProvider2.SetError(txtEngineVariable, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtEngineVariable, "Please enter non-negative whole number between 0 and 999.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtAALethalityRate_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtAALethalityRate.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtAALethalityRate.Text, out var intValue))
            {
                errorProvider2.SetError(txtAALethalityRate, "Please enter non-negative whole number between 0 and 999.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 999))
                {
                    errorProvider2.SetError(txtAALethalityRate, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtAALethalityRate, "Please enter non-negative whole number between 0 and 999.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtCombDensRate_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtCombDensRate.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtCombDensRate.Text, out var intValue))
            {
                errorProvider2.SetError(txtCombDensRate, "Please enter non-negative whole number between 0 and 999.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 999))
                {
                    errorProvider2.SetError(txtCombDensRate, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtCombDensRate, "Please enter non-negative whole number between 0 and 999.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtEngRate_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtEngRate.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtEngRate.Text, out var intValue))
            {
                errorProvider2.SetError(txtEngRate, "Please enter non-negative whole number between 0 and 999.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 999))
                {
                    errorProvider2.SetError(txtEngRate, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtEngRate, "Please enter non-negative whole number between 0 and 999.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtEntrenchRate_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtEntrenchRate.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtEntrenchRate.Text, out var intValue))
            {
                errorProvider2.SetError(txtEntrenchRate, "Please enter non-negative whole number between 0 and 999.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 999))
                {
                    errorProvider2.SetError(txtEntrenchRate, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtEntrenchRate, "Please enter non-negative whole number between 0 and 999.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtHexConvRate_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtHexConvRate.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtHexConvRate.Text, out var intValue))
            {
                errorProvider2.SetError(txtHexConvRate, "Please enter non-negative whole number between 0 and 999.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 999))
                {
                    errorProvider2.SetError(txtHexConvRate, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtHexConvRate, "Please enter non-negative whole number between 0 and 999.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtSuppMoveRate_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtSuppMoveRate.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtSuppMoveRate.Text, out var intValue))
            {
                errorProvider2.SetError(txtSuppMoveRate, "Please enter non-negative whole number between 0 and 999.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 999))
                {
                    errorProvider2.SetError(txtSuppMoveRate, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtSuppMoveRate, "Please enter non-negative whole number between 0 and 999.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtSuppReadRate_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtSuppReadRate.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtSuppReadRate.Text, out var intValue))
            {
                errorProvider2.SetError(txtSuppReadRate, "Please enter non-negative whole number between 0 and 999.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 0 && (intValue <= 999))
                {
                    errorProvider2.SetError(txtSuppReadRate, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtSuppReadRate, "Please enter non-negative whole number between 0 and 999.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtNavAttritionDivider_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtNavAttritionDivider.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtNavAttritionDivider.Text, out var intValue))
            {
                errorProvider2.SetError(txtNavAttritionDivider, "Please enter whole number between 1 and 100.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1 && (intValue <= 100))
                {
                    errorProvider2.SetError(txtNavAttritionDivider, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtNavAttritionDivider, "Please enter whole number between 1 and 100.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtAttritionDivider_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtAttritionDivider.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtAttritionDivider.Text, out var intValue))
            {
                errorProvider2.SetError(txtAttritionDivider, "Please enter whole number between 1 and 100.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1 && (intValue <= 100))
                {
                    errorProvider2.SetError(txtAttritionDivider, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtAttritionDivider, "Please enter whole number between 1 and 100.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtMRPB_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtMRPB.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtMRPB.Text, out var intValue))
            {
                errorProvider2.SetError(txtMRPB, "Please enter whole number between 1 and 99.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1 && (intValue <= 99))
                {
                    errorProvider2.SetError(txtMRPB, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtMRPB, "Please enter whole number between 1 and 99.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }

        private void txtRoadCost_TextChanged(object sender, EventArgs e)
        {
            //EMPTY TEXTBOX
            if (string.IsNullOrEmpty(txtRoadCost.Text))
            {
                btnSaveScenSettings.Enabled = false;
                return;
            }
            //NON-NUMERIC INPUT
            if (!int.TryParse(txtRoadCost.Text, out var intValue))
            {
                errorProvider2.SetError(txtRoadCost, "Please enter whole number between 1 and 10.");
                btnSaveScenSettings.Enabled = false;
                return;
            }
            else  //NUMERIC INPUT, CHECKS INPUT RANGE
            {
                //INPUT RANGE DEPENDS ON SPECIFIC TRIGGERS
                if (intValue >= 1 && (intValue <= 10))
                {
                    errorProvider2.SetError(txtRoadCost, "");
                    btnSaveScenSettings.Enabled = true;
                }
                else
                {
                    errorProvider2.SetError(txtRoadCost, "Please enter whole number between 1 and 10.");
                    btnSaveScenSettings.Enabled = false;
                }
            }
        }
    }
}
