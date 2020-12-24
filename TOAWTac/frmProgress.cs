using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOAWTac
{
    public partial class frmProgress : Form
    {
        public frmProgress()
        {
            InitializeComponent();
        }

        private void frmProgress_Load(object sender, EventArgs e)
        {
            //Application.EnableVisualStyles();
            //ShowMarquee();
            //this.Refresh();
            //this.label1.Refresh();
        }

        //private async void ShowMarquee()
        //{
            //Application.EnableVisualStyles();
            //progressBar1.Visible = true;
            //progressBar1.Style = ProgressBarStyle.Marquee;
            //progressBar1.MarqueeAnimationSpeed = 100;
            //await Task.Delay(500);
        //}
    }
}
