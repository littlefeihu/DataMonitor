using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataMonitor.DQ.Infrastructure.DataRepository.Models;

namespace DataMonitor.DQ.UI.UserControls
{
    public partial class DeviceItem : UserControl
    {
        public DeviceItem(Device device)
        {
            InitializeComponent();
            labelX1.Text = device.DeviceName + ",设备编号：" + device.DeviceNum;

            //labelX2.Text = string.Format("温度:{0}", temperature);
            //labelX3.Text = string.Format("湿度:{0}", humidity);
            //labelX4.Text = string.Format("经度:{0}", latitude);
            //labelX5.Text = string.Format("纬度:{0}", longitude);

        }




    }
}
