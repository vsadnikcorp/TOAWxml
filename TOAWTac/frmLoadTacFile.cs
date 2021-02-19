using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOAWTac
{
    public partial class frmLoadTacFile : Form
    {
        public frmLoadTacFile()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            TOAWTac.Properties.Settings.Default.TacFilePath = "";
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "*.tac files *.tac|*.tac";

            if (file.ShowDialog() == DialogResult.OK)
            {
                TOAWTac.Properties.Settings.Default.TacFilePath = file.FileName;
                TOAWTac.Properties.Settings.Default.Save();

                this.Close();
            }
            else
            {
                this.Close();
                TOAWTac.Properties.Settings.Default.TacFilePath = "";
            }
        }
    }
}
