using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOAWXML
{
    public partial class frmMissingEqpFile : Form
    {
        public frmMissingEqpFile()
        {
            InitializeComponent();
            string pathstring = "The indicated *.eqp file (" + Globals.GlobalVariables.EQPPATH;
            pathstring = pathstring + ") is missing." + Environment.NewLine + Environment.NewLine;
            pathstring = pathstring + " Please select new *.eqp file.";
            this.label1.Text = pathstring;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .eqp files *.eqp|*.eqp";
            if (file.ShowDialog() == DialogResult.OK)
            {
                Globals.GlobalVariables.PATH = file.FileName;
                System.IO.File.WriteAllText("EqpFilePath.txt", Globals.GlobalVariables.EQPPATH = file.FileName);

                var f = new frmEquip();
                f.Show();

                this.Close();

                frmEquip obj = (frmEquip)Application.OpenForms["frmEquip"];
                obj.Close();

                f.Activate();
                f.Focus();
                f.TopMost = true;
            }
        }
    }
}
