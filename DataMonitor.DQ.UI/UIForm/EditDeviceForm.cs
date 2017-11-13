using DataMonitor.DQ.BusinessLayer;
using DataMonitor.DQ.Infrastructure.DataRepository.Models;
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

namespace DataMonitor.DQ.UI.UIForm
{
    public partial class EditDeviceForm : Office2007Form
    {
        Device _device;
        public event Action<Device> EditComplete;
        public EditDeviceForm(Device device)
        {
            InitializeComponent();
            this.EnableGlass = false;

            comboBoxEx1.DisplayMember = "Name";
            comboBoxEx1.ValueMember = "Id";
            comboBoxEx1.DataSource = WarehouseService.GetAllWarehouses();


            _device = device;
            bindingSource1.DataSource = _device;
        }
        public EditDeviceForm(int x, int y)
            : this(new Device() { X = x, Y = y })
        {

        }

        /// <summary>
        /// 编辑或创建设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this._device.DeviceName))
            {
                MessageBox.Show("测点名称不能为空");
                return;
            }

            if (string.IsNullOrEmpty(this._device.DeviceNum))
            {
                MessageBox.Show("测点编号不能为空");
                return;
            }

            if (string.IsNullOrEmpty(this._device.IPAddress) || string.IsNullOrEmpty(this._device.Port))
            {
                MessageBox.Show("IP或端口号不能为空");
                return;
            }

            try
            {
                if (_device.Id == 0)
                {
                    DeviceService.AddDevice(this._device);
                }
                else
                {
                    DeviceService.UpdateDevice(this._device);
                }
                if (EditComplete != null)
                {
                    EditComplete(this._device);
                }

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBoxEx1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Warehouse warehouse = comboBoxEx1.SelectedItem as Warehouse;

            if (warehouse != null && warehouse.Id != 0)
            {
                txtTemperatureAlarmUpperLimit.Text = warehouse.TemperatureAlarmUpperLimit.Value.ToString();
                txtTemperatureAlarmLowerLimit.Text = warehouse.TemperatureAlarmLowerLimit.Value.ToString();
                txtTemperatureForewarningUpperLimit.Text = warehouse.TemperatureForewarningUpperLimit.Value.ToString();
                txtTemperatureForewarningLowerLimit.Text = warehouse.TemperatureForewarningLowerLimit.Value.ToString();
                txtHumidityAlarmUpperLimit.Text = warehouse.HumidityAlarmUpperLimit.Value.ToString();
                txtHumidityAlarmLowerLimit.Text = warehouse.HumidityAlarmLowerLimit.Value.ToString();
                txtHumidityForewarningUpperLimit.Text = warehouse.HumidityForewarningUpperLimit.Value.ToString();
                txtHumidityForewarningLowerLimit.Text = warehouse.HumidityForewarningLowerLimit.Value.ToString();

                this.bindingSource1.EndEdit();
            }
        }
    }
}
