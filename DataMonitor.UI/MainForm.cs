using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMonitor.UI
{
    public partial class MainForm : Office2007Form
    {
        public MainForm()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
            this.EnableGlass = false;
            this.currentTiem.Text = string.Format("{0:F}", DateTime.Now);
            this.Text = System.Configuration.ConfigurationManager.AppSettings["name"];
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            this.currentTiem.Text = string.Format("{0:F}", DateTime.Now);
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {

        }


    }
}
