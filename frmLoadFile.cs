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
    public partial class frmLoadFile : Form
    {
        public frmLoadFile()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.OpenForms["xmlform"].Close();

        }

        private void btnFile2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .gam files *.gam|*.gam";
            if (file.ShowDialog() == DialogResult.OK)
            {
                Globals.GlobalVariables.PATH = file.FileName;
                System.IO.File.WriteAllText("FilePath.txt", Globals.GlobalVariables.PATH = file.FileName);
                Application.Restart();

            }
        }
    }
}
