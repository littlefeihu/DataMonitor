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
    public partial class GPSControl : UserControl
    {
        public GPSControl()
        {
            InitializeComponent();
            dtstarttime.Value = DateTime.Now.AddDays(-1);
            dtendtime.Value = DateTime.Now;
        }
    }
}
