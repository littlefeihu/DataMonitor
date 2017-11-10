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
    public partial class WarehouseEditForm : Office2007Form
    {

        Warehouse _warehouse = new Warehouse();

        public WarehouseEditForm()
        {
            InitializeComponent();
            this.EnableGlass = false;
            bindingSource1.DataSource = _warehouse;
        }
        public WarehouseEditForm(Warehouse warehouse)
        {
            InitializeComponent();
            this.EnableGlass = false;
            _warehouse = warehouse;
            bindingSource1.DataSource = _warehouse;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (_warehouse.Id == 0)
            {
                WarehouseService.AddWarehouse(_warehouse);
            }
            else
            {
                WarehouseService.UpdateWarehouse(_warehouse);
            }
            this.Hide();
        }
    }
}
