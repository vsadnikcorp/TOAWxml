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
    public partial class frmMissingFile : Form
    {
        public frmMissingFile()
        {
            InitializeComponent();
            string pathstring = "The indicated *.gam file (" + Globals.GlobalVariables.PATH;
            pathstring = pathstring + ") is missing." + Environment.NewLine + Environment.NewLine;
            pathstring = pathstring + " Please select new *.gam file.";


            this.label1.Text = pathstring;
        }

        private void btnCancel2_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.OpenForms["xmlform"].Close();
        }

        private void btnFile3_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .gam files *.gam|*.gam";
            if (file.ShowDialog() == DialogResult.OK)
            {
                Globals.GlobalVariables.PATH = file.FileName;
                System.IO.File.WriteAllText("FilePath.txt", Globals.GlobalVariables.PATH);
                Application.Restart();
            }
        }
    }
}
