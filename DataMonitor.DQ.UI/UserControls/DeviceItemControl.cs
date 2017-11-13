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
using DataMonitor.DQ.UI.Helper;
using DataMonitor.DQ.UI.UIForm;
using DataMonitor.DQ.BusinessLayer;

namespace DataMonitor.DQ.UI.UserControls
{
    public partial class DeviceItemControl : UserControl
    {
        DeviceItem _deviceitem;
        public event Action<DeviceItemControl> EditComplete;
        public event Action<DeviceItemControl> RemodeComplete;
        public DeviceItemControl(DeviceItem deviceitem)
        {
            InitializeComponent();
            _deviceitem = deviceitem;

            labelX1.Text = deviceitem.Device.DeviceName;
            if (deviceitem.Client != null)
            {
                SetClient(deviceitem.Client);
            }

            labelX2.Text = string.Format("{0}℃", "0");
            labelX3.Text = string.Format("{0}%", "0");
            labelX1.MouseClick += labelX1_MouseClick;
            labelX2.MouseClick += labelX1_MouseClick;
            labelX3.MouseClick += labelX1_MouseClick;
            flowLayoutPanel1.MouseClick += labelX1_MouseClick;


            labelX1.MouseMove += labelX1_MouseMove;
            labelX2.MouseMove += labelX1_MouseMove;
            labelX3.MouseMove += labelX1_MouseMove;
            flowLayoutPanel1.MouseMove += labelX1_MouseMove;

        }

        void labelX1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (Singleton.Instance.EditDeviceMode)
            {
                case DataMonitor.DQ.Infrastructure.EditDeviceMode.None:
                    Cursor.Current = Cursors.Default;
                    break;
                case DataMonitor.DQ.Infrastructure.EditDeviceMode.Add:
                case DataMonitor.DQ.Infrastructure.EditDeviceMode.Update:

                    Bitmap a = (Bitmap)Bitmap.FromFile("MyXG.ico");
                    CursorHelper.SetCursor(this, a, new Point(0, 0));
                    break;
                case DataMonitor.DQ.Infrastructure.EditDeviceMode.Delete:
                    Bitmap del = (Bitmap)Bitmap.FromFile("MyDelete.ico");
                    CursorHelper.SetCursor(this, del, new Point(0, 0));
                    break;
                default:
                    Cursor.Current = Cursors.Default;
                    break;
            }
        }

        void labelX1_MouseClick(object sender, MouseEventArgs e)
        {
            switch (Singleton.Instance.EditDeviceMode)
            {
                case DataMonitor.DQ.Infrastructure.EditDeviceMode.Update:

                    var editDeviceForm = new EditDeviceForm(_deviceitem.Device);
                    editDeviceForm.EditComplete += editDeviceForm_EditComplete;
                    editDeviceForm.ShowDialog();

                    break;
                case DataMonitor.DQ.Infrastructure.EditDeviceMode.Delete:
                    if (MessageBox.Show("确定要删除删除当前监测点吗？", "删除确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DeviceService.DeleteDevice(this._deviceitem.Device.Id);
                        if (RemodeComplete != null)
                        {
                            RemodeComplete(this);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        void editDeviceForm_EditComplete(Device device)
        {
            labelX1.Text = device.DeviceName;
            if (EditComplete != null)
            {
                this.DeviceItem.SetDevice(device);
                EditComplete(this);
            }
        }


        public DeviceItem DeviceItem
        {
            get { return _deviceitem; }
            private set { _deviceitem = value; }
        }

        public void SetClient(MyTcpSocketClient client)
        {
            _deviceitem.SetClient(client);
            _deviceitem.Client.Send("25" + _deviceitem.Device.DeviceNum + "01FF0066");
            _deviceitem.Client.ServerDataReceived -= Client_ServerDataReceived;
            _deviceitem.Client.ServerDataReceived += Client_ServerDataReceived;
        }

        void Client_ServerDataReceived(string arg1, string arg2, string arg3)
        {
            if (arg1 == _deviceitem.Device.DeviceNum)
            {
                this.Invoke(new MethodInvoker(() =>
                {
                    labelX2.Text = string.Format("{0}℃", arg2);

                    labelX3.Text = string.Format("{0}%", arg3);
                }));


            }
        }

    }
}
