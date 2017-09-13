using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMonitor.UI.Controls
{
    public partial class RealtimeControl : UserControl
    {
        public RealtimeControl()
        {
            InitializeComponent();
        }

        private void superTabControlPanel2_Click(object sender, EventArgs e)
        {

        }

        private void buttonX3_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            buttonX3.Checked = !buttonX3.Checked;
        }
    }
}
