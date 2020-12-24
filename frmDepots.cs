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


namespace TOAWXML
{
    public partial class frmDepots : Form
    {
        public frmDepots()
        {
            InitializeComponent();
        }

        public class Depots
        {
            //public string ID { get; set; }
            //public string X { get; set; }
            //public string Y { get; set; }
            //public string Supply { get; set; }
        }

        private void frmDepots_Load(object sender, EventArgs e)
        {
            //SUPPLY DEPOT PORTION OF XML
            dgvDepots.AutoGenerateColumns = false;
            dgvDepots.ColumnCount = 4;

            dgvDepots.Columns[0].Name = "ID";
            dgvDepots.Columns["ID"].DataPropertyName = "ID";
            dgvDepots.Columns[1].Name = "X";
            dgvDepots.Columns["X"].DataPropertyName = "x";
            dgvDepots.Columns[2].Name = "Y";
            dgvDepots.Columns["Y"].DataPropertyName = "y";
            dgvDepots.Columns[3].Name = "Supply";
            dgvDepots.Columns["Supply"].DataPropertyName = "supply";

            dgvDepots.Columns[0].ReadOnly = true;

            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("X", typeof(Int32));
            dt.Columns.Add("Y", typeof(Int32));
            dt.Columns.Add("Supply", typeof(Int32));
            XDocument xdoc = XDocument.Load(Globals.GlobalVariables.PATH);
            
            var depots = (from d in xdoc.Descendants("SUPPLIES").Descendants("FORCE").Descendants("NODE")
                     where (string)d.Parent.Attribute("ID") == Globals.GlobalVariables.FORCE
                           select new
                           {
                               ID = d.Attribute("ID").Value.ToString(),
                               X = d.Attribute("x").Value.ToString(),
                               Y = d.Attribute("y").Value.ToString(),
                               Supply = d.Attribute("supply").Value.ToString()
                           });
            depots.ToList().ForEach(i => dt.Rows.Add(i.ID, i.X, i.Y, i.Supply));
            dgvDepots.DataSource = dt;
        }

        private void btnCloseDepot_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDepots_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.CellStyle.ForeColor = Color.Gray;
            }
        }

        private void btnSaveDepot_Click(object sender, EventArgs e)
        {
            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
            string xpathdepot;
            XElement xdepot;

            foreach (DataGridViewRow row in dgvDepots.Rows)
            {
                string strID = row.Cells["ID"].Value.ToString();
                string strX = row.Cells["X"].Value.ToString();
                string strY = row.Cells["Y"].Value.ToString();
                string strSupply = row.Cells["Supply"].Value.ToString();

                xpathdepot = "SUPPLIES/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/NODE[@ID=" + strID + "]";
                xdepot = xelem.XPathSelectElement(xpathdepot);
                xdepot.Attribute("x").Value = strX;
                xdepot.Attribute("y").Value = strY;
                xdepot.Attribute("supply").Value = strSupply;
                xelem.Save(Globals.GlobalVariables.PATH);
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in dgvDepots.SelectedRows)
            {
                string xpathdepot;
                string strID;
                XElement xdepot;
                int rowindex;

                rowindex = dgvDepots.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvDepots.Rows[rowindex];
                strID = selectedRow.Cells["ID"].Value.ToString();

                XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
                xpathdepot = "SUPPLIES/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/NODE[@ID=" + strID + "]";
                xdepot = xelem.XPathSelectElement(xpathdepot);

                dgvDepots.Rows.RemoveAt(item.Index);
                xdepot.Remove();
                xelem.Save(Globals.GlobalVariables.PATH);
            }
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string xpathdepot;
            string strID;
            IEnumerable<XElement> xdepot;
            int rowindex;
            int newMax;
            int depRows = dgvDepots.Rows.Count;

            XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);

            if (depRows > 0)
            {
                rowindex = dgvDepots.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgvDepots.Rows[rowindex];
                strID = selectedRow.Cells["ID"].Value.ToString();
                //XElement xelem = XElement.Load(Globals.GlobalVariables.PATH);
                xpathdepot = "SUPPLIES/FORCE[@ID=" + Globals.GlobalVariables.FORCE + "]/NODE";
                xdepot = xelem.XPathSelectElements(xpathdepot);

                //FIND HIGHEST ID NUMBER, ADD ONE TO NEW DEPOT'S ID
                int oldMax = xdepot.Max(m => (int)m.Attribute("ID"));
                //int newMax;
                newMax = oldMax + 1;
            }
            else
            {
                newMax = 1;
            }

            //ADD NEW DEPOT ROW TO DATAGRIDVIEW
            dgvDepots.AllowUserToAddRows = true;
            dgvDepots.Rows[dgvDepots.RowCount - 1].Cells["ID"].Value = newMax.ToString();
            
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("X", typeof(int));
            dt.Columns.Add("Y", typeof(int));
            dt.Columns.Add("Supply", typeof(int));

            int i = 0;

            foreach (DataGridViewRow row in dgvDepots.Rows)
            {
                dt.Rows.Add();

                if (i == dgvDepots.Rows.Count - 1)
                  {
                    dgvDepots.Rows[i].Cells["X"].Value = "1";
                    dgvDepots.Rows[i].Cells["Y"].Value = "1";
                    dgvDepots.Rows[i].Cells["Supply"].Value = "1";
                  }

                dt.Rows[i][0] = dgvDepots.Rows[i].Cells["ID"].Value;
                dt.Rows[i][1] = dgvDepots.Rows[i].Cells["X"].Value;
                dt.Rows[i][2] = dgvDepots.Rows[i].Cells["Y"].Value;
                dt.Rows[i][3] = dgvDepots.Rows[i].Cells["Supply"].Value;

                i++;
            }

            XElement depotNode = new XElement ("NODE",
                new XAttribute("ID", newMax.ToString()), 
                new XAttribute("node", (newMax - 1).ToString()), 
                new XAttribute("x", dgvDepots.Rows[dgvDepots.RowCount - 1].Cells["X"].Value.ToString()),
                new XAttribute("y", dgvDepots.Rows[dgvDepots.RowCount - 1].Cells["Y"].Value.ToString()), 
                new XAttribute("supply", dgvDepots.Rows[dgvDepots.RowCount - 1].Cells["Supply"].Value.ToString()));

            var depots = (from d in xelem.Elements("SUPPLIES").Elements("FORCE").Elements("NODE")
                          where (string)d.Parent.Attribute("ID") == Globals.GlobalVariables.FORCE
                          select d);

            //IF THERE ARE ALREADY DEPOTS OR NOT
            if (depots.Any())
            {
                depots.Last().AddAfterSelf(depotNode);
            }
            else
            {
                xelem.XPathSelectElement("SUPPLIES/FORCE[@ID = " + Globals.GlobalVariables.FORCE + "]").Add(depotNode);
            }

            xelem.Save(Globals.GlobalVariables.PATH);

            dt.Rows[dgvDepots.Rows.Count-1][0] = newMax.ToString();
            dgvDepots.DataSource = dt;
            dgvDepots.AllowUserToAddRows = false;
        }

        private void dgvDepots_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            dgvDepots.Rows[e.RowIndex].ErrorText = "";
            int depotData;

            if (dgvDepots.Rows[e.RowIndex].IsNewRow) { return; }
            if (!int.TryParse(e.FormattedValue.ToString(),
                out depotData) || depotData < 0)
            {
                MessageBox.Show("The entered value must be a positive whole number!", "Enter Positive Whole Number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnSaveDepot.Enabled = false;
                e.Cancel = true;
            }
            else
            {
                btnSaveDepot.Enabled = true;
            }
        }
    }
}
