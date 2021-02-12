using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TOAWXML;
using TOAWTac;
using TOAWEquipViewer;
using TOAWEditSave;

namespace toawMenu
{
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            //rbTacLayer.Enabled = false;

            rbTOAWxml.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            rbEquipView.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            rbSavedGame.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
            rbTacLayer.CheckedChanged += new EventHandler(radioButtons_CheckedChanged);
        }

        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            btnOpen.Enabled = true;
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (rbTOAWxml.Checked == true)
            {
                
                xmlform toawxml = new xmlform();
                toawxml.Show();
                //this.Close();
            }
            if (rbEquipView.Checked == true)
            {
                frmEquipView viewer = new frmEquipView();
                viewer.Show();
                //this.Close();
            }

            if (rbSavedGame.Checked == true)
            {
                frmEditSave editsaved = new frmEditSave();
                editsaved.Show();
                //this.Close();
            }
            if (rbTacLayer.Checked == true)
            {
                frmTacFile taclayer = new frmTacFile();
                taclayer.Show();
                //this.Close();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        
    }
}
