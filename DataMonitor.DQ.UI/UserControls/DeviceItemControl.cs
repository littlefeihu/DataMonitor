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
using System.Threading;

namespace DataMonitor.DQ.UI.UserControls
{
    public partial class DeviceItemControl : UserControl
    {
        DeviceItem _deviceitem;
        public DeviceItemControl(DeviceItem deviceitem)
        {
            InitializeComponent();
            labelX1.Text = deviceitem.Device.DeviceName + ",设备编号：" + deviceitem.Device.DeviceNum;
            deviceitem.Client.ServerDataReceived += Client_ServerDataReceived;
            labelX2.Text = string.Format("温度:{0}", "");
            labelX3.Text = string.Format("湿度:{0}", "");
            labelX4.Text = string.Format("经度:{0}", "");
            labelX5.Text = string.Format("纬度:{0}", "");
            _deviceitem = deviceitem;
        }

        void Client_ServerDataReceived(string arg1, string arg2, string arg3)
        {
            if (arg1 == _deviceitem.Device.DeviceNum)
            {
                this.Invoke(new MethodInvoker(() =>
                {

                    labelX2.Text = string.Format("温度:{0}", arg2);
                    labelX3.Text = string.Format("湿度:{0}", arg3);

                    labelX4.Text = string.Format("经度:{0}", "");
                    labelX5.Text = string.Format("纬度:{0}", "");

                }));


            }
        }





    }
}
