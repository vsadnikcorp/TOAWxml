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
//using TOAWXML;
//using TOAWTac;
//using TOAWmenu;

namespace TOAWXML
{
    public partial class frmMissingEqpViewFile : Form
    {
        public frmMissingEqpViewFile()
        {
            InitializeComponent();
            string pathstring = "Please load a TOAW *.eqp file." + Environment.NewLine + Environment.NewLine;
            pathstring = pathstring + " Please select *.eqp file.";
            this.label1.Text = pathstring;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
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
                
                this.Close();
            }
            else
            {
                Application.Exit();
                this.Close();
                return;
            }
        }
    }
}
