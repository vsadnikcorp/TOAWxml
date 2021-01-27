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

        private void frmLoadFile_Load(object sender, EventArgs e)
        {
            string selectstring = "Either no *.gam file has been specified or the \r\n " +
               "specified file is missing. \r\n  \r\n " +
               "Please select new *.gam file.";
            this.lblLoadFile.Text = selectstring;
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
                ///Globals.GlobalVariables.PATH = file.FileName;
                ///System.IO.File.WriteAllText("FilePath.txt", Globals.GlobalVariables.PATH = file.FileName);
                ///Application.Restart();


                //>>>>>>>>>>>>>>>>>>>>
                //string FilePath;
                TOAWXML.Properties.Settings.Default.FilePath = file.FileName;
                TOAWXML.Properties.Settings.Default.Save();
                ////Application.Restart();
                ////xmlform_Load(null, EventArgs.Empty);
                ////trvUnitTree.Nodes.Clear();
                ////LoadTree();
                ///
                //>>>>>>>>>>>>>>>
                Application.OpenForms["xmlform"].Close();
                this.Close();
                xmlform toawxml = new xmlform();
                toawxml.Show();
                toawxml.TopMost = true;
                
                //<<<<<<<<<<<<<<<<<<<<<

            }
            //else
            //{
            //    //Console.WriteLine("superdafuq");
            //    //Application.Exit();
            //    //return;
            //}
            //<<<<<<<<<<<<<<<<<<<<
        }

       
    }
}
