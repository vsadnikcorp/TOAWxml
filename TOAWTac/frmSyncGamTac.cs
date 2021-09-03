using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TOAWTac
{
    public partial class frmSyncGamTac : Form
    {
        public frmSyncGamTac()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectGam_Click(object sender, EventArgs e)
        {

            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "*.gam files *.gam|*.gam";

            if (file.ShowDialog() == DialogResult.OK)
            {
                txtSelectedGam.Text = file.FileName;
            }
        }

        private void btnSelectTac_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = false;
            file.Filter = "*.tac files *.tac|*.tac";

            if (file.ShowDialog() == DialogResult.OK)
            {
                txtSelectedTac.Text = file.FileName;
            }
        }
    }
}
