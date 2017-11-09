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
using System.Threading;
using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.UI.Helper;
namespace DataMonitor.DQ.UI.UserControls
{
    public partial class RealtimeControl : UserControl
    {



        public event Action<MouseEventArgs> MouseMoveEvent;
        public RealtimeControl()
        {
            InitializeComponent();

            new Thread(() =>
            {
                AppStartUp.StartCompleted += AppStartUp_StartCompleted;
                AppStartUp.Start();

            }).Start();


            LoadAllDevice();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲

            panel1.Height = 1000;
            panel1.Width = 1290;
            tableLayoutPanel1.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 1290);
            tableLayoutPanel1.RowStyles[0] = new RowStyle(SizeType.Absolute, 1005);

            tableLayoutPanel1.AutoScroll = true;
            this.panel1.MouseMove += panel1_MouseMove;
              this.panel1.MouseClick+=panel1_MouseClick;
        }

        void AppStartUp_StartCompleted(List<MyTcpSocketClient> clients)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                foreach (DeviceItemControl control in panel1.Controls)
                {
                    var targetClient = clients.FirstOrDefault(o => o.Ip == control.DeviceItem.Device.IPAddress && o.Port == control.DeviceItem.Device.Port);

                    control.SetClient(targetClient);
                }
            }));
        }

        private void LoadAllDevice()
        {
            var alldevices = DeviceService.GetAllDevices();
            foreach (var device in alldevices)
            {
                var deviceItem = new DeviceItem(device, null);
                var deviceItemControl = new DeviceItemControl(deviceItem);
                deviceItemControl.Top = device.Y;
                deviceItemControl.Left = device.X;
                panel1.Controls.Add(deviceItemControl);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            switch (Singleton.Instance.EditDeviceMode)
            {
                case DataMonitor.DQ.Infrastructure.EditDeviceMode.Add:

                    Bitmap a = (Bitmap)Bitmap.FromFile("MyAdd.ico");
                    CursorHelper.SetCursor(panel1, a, new Point(0, 0));
                    break;
                default:
                    Cursor.Current = Cursors.Default;
                    break;
            }

            if (MouseMoveEvent != null)
                MouseMoveEvent(e);
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Singleton.Instance.EditDeviceMode = EditDeviceMode.None;
        }



    }
}
