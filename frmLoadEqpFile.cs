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
    public partial class frmLoadEqpFile : Form

    {
        public frmLoadEqpFile()
        {
            InitializeComponent();
        }
        private void frmLoadEqpFile_Load(object sender, EventArgs e)
        {
            string selectstring = "Either no *.eqp file has been specified or the \r\n " +
                "specified file is missing. \r\n  \r\n " +
                "Please select new *.eqp file.";
            this.lblLoadEqpFile.Text = selectstring;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "TOAW .eqp files *.eqp|*.eqp";

            
            ////if (TOAWXML.Properties.Settings.Default.EqpFilePath != "")
            ////{
                if (file.ShowDialog() == DialogResult.OK)
                {
                    TOAWXML.Properties.Settings.Default.EqpFilePath = file.FileName;
                    TOAWXML.Properties.Settings.Default.Save();

                    var f = new frmEquip();
                    f.Show();

                    this.Close();

                    frmEquip obj = (frmEquip)Application.OpenForms["frmEquip"];
                    obj.Close();

                    f.Activate();
                    f.Focus();
                    f.TopMost = true;
                }
            else
            {
                this.Close();
                return;
            }
        }

       
    }
}
//}
