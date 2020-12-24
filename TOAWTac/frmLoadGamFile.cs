using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOAWXML
{
    public partial class frmLoadGamFile : Form
    {
        public frmLoadGamFile()
        {
            InitializeComponent();
        }

        //private void frmMissingGamFile_Load(object sender, EventArgs e)
        //{
        //    //string pathstring = "Please select *.gam file from which to create Tac File.";
        //    //this.label1.Text = pathstring;
        //}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            TOAWTac.Properties.Settings.Default.FilePath = "";
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            //string FilePath; 
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .gam files *.gam|*.gam";

            if (file.ShowDialog() == DialogResult.OK)
            {
                TOAWTac.Properties.Settings.Default.FilePath = file.FileName;
                TOAWTac.Properties.Settings.Default.Save();

                this.Close();
            }
            else
            {
                this.Close();
                TOAWTac.Properties.Settings.Default.FilePath = "";
            }
        }

    }
}
