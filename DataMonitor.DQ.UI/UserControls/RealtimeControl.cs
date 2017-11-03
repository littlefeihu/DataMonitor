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

            LoadAllDevice();
        }

        private void LoadAllDevice()
        {

            var alldevices = DeviceService.GetAllDevices();

            foreach (var device in alldevices)
            {
                var deviceItem = new DeviceItem(device);
                flowLayoutPanel1.Controls.Add(deviceItem);
            }
        }


    }
}
