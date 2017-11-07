using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataMonitor.DQ.BusinessLayer;

namespace DataMonitor.DQ.UI.UserControls
{
    public partial class RealtimeControl : UserControl
    {
        public RealtimeControl()
        {
            InitializeComponent();
            AppStartUp.Start();
            LoadAllDevice();
        }

        private void LoadAllDevice()
        {
            foreach (var device in AppStartUp.DeviceItems)
            {
                var deviceItem = new DeviceItemControl(device);
                flowLayoutPanel1.Controls.Add(deviceItem);
            }
        }


    }
}
