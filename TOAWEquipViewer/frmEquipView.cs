using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
//using TOAWXML;
//using TOAWTac;
//using TOAWmenu;

namespace TOAWXML

{
    public partial class frmEquipView : Form
    {
        public frmEquipView()
        {
            InitializeComponent();
        }
        public class EquipView
        {
            public string EquipPath { get; set; }
        }

        private void frmEquipView_Load(object sender, EventArgs e)
        {
           CallOnLoad();
        }

        private void CallOnLoad()
        {
            string FilePath;

            FilePath = TOAWEquipViewer.Properties.Settings.Default.FilePath.ToString();
            dgvEquipView.DataSource = null;

            if (FilePath == "")
            {
                frmMissingEqpViewFile loadfileform = new frmMissingEqpViewFile();
                loadfileform.ShowDialog();
                if (TOAWEquipViewer.Properties.Settings.Default.FilePath.ToString() == "")
                    {
                    return;
                }
            }

            FilePath = TOAWEquipViewer.Properties.Settings.Default.FilePath.ToString();
            //dafuq with git
            if (File.Exists(FilePath))
                {
                txtEquipFile.Text = FilePath;

                //SET UP DATATABLE FOR DGV
                DataTable dt = new DataTable();

                dt.Columns.Add("NAME", typeof(string));
                dt.Columns.Add("Country", typeof(string));
                dt.Columns.Add("AT", typeof(Int16));
                dt.Columns.Add("AP", typeof(Int16));
                dt.Columns.Add("Range", typeof(Int16));
                dt.Columns.Add("AA", typeof(Int16));
                dt.Columns.Add("AARNG", typeof(Int16));
                dt.Columns.Add("DF", typeof(Int16));
                dt.Columns.Add("Armor", typeof(Int16));
                dt.Columns.Add("Vol", typeof(Int16));
                dt.Columns.Add("Wght", typeof(Int32));
                dt.Columns.Add("ShllWgt", typeof(Int16));
                dt.Columns.Add("flag0", typeof(Int16));
                dt.Columns.Add("flag1", typeof(Int16));
                dt.Columns.Add("flag2", typeof(Int16));
                dt.Columns.Add("flag3", typeof(Int16));
                dt.Columns.Add("flag4", typeof(Int16));
                dt.Columns.Add("flag5", typeof(Int16));
                dt.Columns.Add("flag6", typeof(Int16));
                dt.Columns.Add("flag7", typeof(Int16));

                ///SETUP DGVEQUIPVIEW CHARACTERISTICS
                dgvEquipView.AutoGenerateColumns = false;
                dgvEquipView.ColumnCount = 20;
                dgvEquipView.ReadOnly = false;

                dgvEquipView.Columns[0].Name = "Name";
                dgvEquipView.Columns["Name"].DataPropertyName = "Name";
                dgvEquipView.Columns[1].Name = "Country";
                dgvEquipView.Columns["Country"].DataPropertyName = "Country";
                dgvEquipView.Columns[2].Name = "AT";
                dgvEquipView.Columns["AT"].DataPropertyName = "AT";
                dgvEquipView.Columns[3].Name = "AP";
                dgvEquipView.Columns["AP"].DataPropertyName = "AP";
                dgvEquipView.Columns[4].Name = "RNG";
                dgvEquipView.Columns["RNG"].DataPropertyName = "RANGE";
                dgvEquipView.Columns[5].Name = "AA";
                dgvEquipView.Columns["AA"].DataPropertyName = "AA";
                dgvEquipView.Columns[6].Name = "AARNG";
                dgvEquipView.Columns["AARNG"].DataPropertyName = "AARNG";
                dgvEquipView.Columns[7].Name = "DF";
                dgvEquipView.Columns["DF"].DataPropertyName = "DF";
                dgvEquipView.Columns[8].Name = "ARM";
                dgvEquipView.Columns["ARM"].DataPropertyName = "ARMOR";
                dgvEquipView.Columns[9].Name = "VOL";
                dgvEquipView.Columns["VOL"].DataPropertyName = "VOL";
                dgvEquipView.Columns[10].Name = "WGHT";
                dgvEquipView.Columns["WGHT"].DataPropertyName = "WGHT";
                dgvEquipView.Columns[11].Name = "SHWGT";
                dgvEquipView.Columns["SHWGT"].DataPropertyName = "SHLLWGT";
                dgvEquipView.Columns[12].Name = "FLAG0";
                dgvEquipView.Columns["FLAG0"].DataPropertyName = "flag0";
                dgvEquipView.Columns[13].Name = "FLAG1";
                dgvEquipView.Columns["FLAG1"].DataPropertyName = "flag1";
                dgvEquipView.Columns[14].Name = "FLAG2";
                dgvEquipView.Columns["FLAG2"].DataPropertyName = "flag2";
                dgvEquipView.Columns[15].Name = "FLAG3";
                dgvEquipView.Columns["FLAG3"].DataPropertyName = "flag3";
                dgvEquipView.Columns[16].Name = "FLAG4";
                dgvEquipView.Columns["FLAG4"].DataPropertyName = "flag4";
                dgvEquipView.Columns[17].Name = "FLAG5";
                dgvEquipView.Columns["FLAG5"].DataPropertyName = "flag5";
                dgvEquipView.Columns[18].Name = "FLAG6";
                dgvEquipView.Columns["FLAG6"].DataPropertyName = "flag6";
                dgvEquipView.Columns[19].Name = "FLAG7";
                dgvEquipView.Columns["FLAG7"].DataPropertyName = "flag7";

                dgvEquipView.Columns["Name"].ReadOnly = true;

                //CENTER-JUSTIFY DGV HEADERS
                dgvEquipView.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[9].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[10].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[11].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[12].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[13].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[14].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[15].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[16].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[17].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[18].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[19].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                //CENTER-JUSTIFY DGV CONTENT
                dgvEquipView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvEquipView.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                ///DGV COLUMN WIDTH
                dgvEquipView.Columns[0].Width = 150;
                dgvEquipView.Columns[1].Width = 115;
                dgvEquipView.Columns[2].Width = 50;
                dgvEquipView.Columns[3].Width = 50;
                dgvEquipView.Columns[4].Width = 50;
                dgvEquipView.Columns[5].Width = 50;
                dgvEquipView.Columns[6].Width = 50;
                dgvEquipView.Columns[7].Width = 50;
                dgvEquipView.Columns[8].Width = 50;
                dgvEquipView.Columns[9].Width = 50;
                dgvEquipView.Columns[10].Width = 50;
                dgvEquipView.Columns[11].Width = 50;
                dgvEquipView.Columns[12].Width = 50;
                dgvEquipView.Columns[13].Width = 50;
                dgvEquipView.Columns[14].Width = 50;
                dgvEquipView.Columns[15].Width = 50;
                dgvEquipView.Columns[16].Width = 50;
                dgvEquipView.Columns[17].Width = 50;
                dgvEquipView.Columns[18].Width = 50;
                dgvEquipView.Columns[19].Width = 50;

                //SETS UP XML
                XElement xelem = XElement.Load(FilePath);
                var xequip = xelem.Descendants("UNITS_DATABASE");

                var eqp = xelem.Descendants("COUNTRY");
                var eqpd = eqp.GroupBy(x => x.ToString().Split(new[] { '-' }, 2)[0]).Select(y => y.First()).Where(x => x.Value.ToString() != "Do Not Use").Distinct().OrderBy(z => z.ToString()); //SORTS DISTINCT COUNTRY NAMES

                List<string> equipmentList = new List<string>();

                IEnumerable<XElement> xcat = (from c in xequip.Descendants()
                                              where c.Element("NAME") != null && c.Element("NAME").Value.ToString() != "EMPTY"
                                              select c);

                foreach (XElement cc in xcat)
                {

                    if (cc.Element("NAME").Value.ToString() != "EMPTY" && cc.Element("NAME").Value.ToString() != "Empty")
                    {
                        dt.Rows.Add(cc.Element("NAME").Value, cc.Element("COUNTRY").Value, cc.Element("AT").Value, cc.Element("AP").Value, cc.Element("ARTY_RNG").Value,
                            cc.Element("AA").Value, cc.Element("SAM_RNG").Value, cc.Element("DF").Value, cc.Element("ARMOR").Value,
                            cc.Element("VOL").Value, cc.Element("WEIGHT").Value, cc.Element("SHELL_W").Value,
                            cc.Element("FLAG0").Value, cc.Element("FLAG1").Value, cc.Element("FLAG2").Value, cc.Element("FLAG3").Value, cc.Element("FLAG4").Value,
                            cc.Element("FLAG5").Value, cc.Element("FLAG6").Value, cc.Element("FLAG7").Value);
                    }
                    dgvEquipView.DataSource = dt;
                }
            }
            else
            {
                MessageBox.Show("The specified .eqp file (" + FilePath + ") is missing.  Please load another file.", "Specified .eqp File Missing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            //Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //SET UP XML
            string strEqpFilePath =  txtEquipFile.Text;
            XElement xelem = XElement.Load(strEqpFilePath);
            var xequip = xelem.Descendants("UNITS_DATABASE");
            string xpatheqpview =  txtEquipFile.Text;

            IEnumerable<XElement> xview = (from c in xequip.Descendants()
                  where c.Element("NAME") != null && c.Element("NAME").Value.ToString() != "EMPTY"
                  select c);

            //WRITE DT DATA TO XML
            foreach (DataGridViewRow row in dgvEquipView.Rows)
            {
                //ASSIGN DT VALUES TO STRINGS
                //string strName = row.Cells["Name"].Value.ToString();
                string strName = Convert.ToString(row.Cells["Name"].Value);
                string strCountry = Convert.ToString(row.Cells["Country"].Value);
                string strAT = Convert.ToString(row.Cells["AT"].Value);
                string strAP = Convert.ToString(row.Cells["AP"].Value);
                string strRNG = Convert.ToString(row.Cells["RNG"].Value);
                string strAA = Convert.ToString(row.Cells["AA"].Value);
                string strAARng = Convert.ToString(row.Cells["AARNG"].Value);
                string strDF = Convert.ToString(row.Cells["DF"].Value);
                string strArmor = Convert.ToString(row.Cells["ARM"].Value);
                string strVol = Convert.ToString(row.Cells["VOL"].Value);
                string strWght = Convert.ToString(row.Cells["WGHT"].Value);
                string strShWght = Convert.ToString(row.Cells["SHWGT"].Value);
                string strF0 = Convert.ToString(row.Cells["FLAG0"].Value);
                string strF1 = Convert.ToString(row.Cells["FLAG1"].Value);
                string strF2 = Convert.ToString(row.Cells["FLAG2"].Value);
                string strF3 = Convert.ToString(row.Cells["FLAG3"].Value);
                string strF4 = Convert.ToString(row.Cells["FLAG4"].Value);
                string strF5 = Convert.ToString(row.Cells["FLAG5"].Value);
                string strF6 = Convert.ToString(row.Cells["FLAG6"].Value);
                string strF7 = Convert.ToString(row.Cells["FLAG7"].Value);

                //SETUP XML
                if (strName != "")
                {
                    var xeqpview = (from v in xview
                                    where v.Element("NAME").Value == strName
                                    select v).FirstOrDefault();

                    //ASSIGN STRING VALUES TO XML
                    xeqpview.Element("NAME").Value = strName;
                    xeqpview.Element("COUNTRY").Value = strCountry;
                    xeqpview.Element("AT").Value = strAT;
                    xeqpview.Element("AP").Value = strAP;
                    xeqpview.Element("AA").Value = strAA;
                    xeqpview.Element("DF").Value = strDF;
                    xeqpview.Element("ARTY_RNG").Value = strRNG;
                    xeqpview.Element("SAM_RNG").Value = strAARng;
                    xeqpview.Element("VOL").Value = strVol;
                    xeqpview.Element("WEIGHT").Value = strWght;
                    xeqpview.Element("SHELL_W").Value = strShWght;
                    xeqpview.Element("FLAG0").Value = strF0;
                    xeqpview.Element("FLAG1").Value = strF1;
                    xeqpview.Element("FLAG2").Value = strF2;
                    xeqpview.Element("FLAG3").Value = strF3;
                    xeqpview.Element("FLAG4").Value = strF4;
                    xeqpview.Element("FLAG5").Value = strF5;
                    xeqpview.Element("FLAG6").Value = strF6;
                    xeqpview.Element("FLAG7").Value = strF7;
                }
            }
            xelem.Save(xpatheqpview);
        }

        private void dgvEquipView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.CellStyle.ForeColor = Color.DarkSlateGray;
            }
        }

        private void btnEqpFile_Click(object sender, EventArgs e)
        {
            string FilePath;
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .eqp files *.eqp|*.eqp";
            if (file.ShowDialog() == DialogResult.OK)
            {
                FilePath = TOAWEquipViewer.Properties.Settings.Default.FilePath.ToString();
                TOAWEquipViewer.Properties.Settings.Default.FilePath = file.FileName;
                TOAWEquipViewer.Properties.Settings.Default.Save();
                CallOnLoad();
            }
        }

        public void CloseApp()
        {
            Application.Exit();
        }
    }
}
