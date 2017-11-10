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
    public partial class MainWarehouseForm : Office2007Form
    {
        public MainWarehouseForm()
        {
            InitializeComponent();
            this.EnableGlass = false;
            LoadData();

        }


        private void LoadData()
        {
            dataGridViewX1.DataSource = WarehouseService.GetAllWarehouses();
        }

        private void tsbtnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认删除吗？", "删除提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                var warehouse = (Warehouse)this.dataGridViewX1.CurrentRow.DataBoundItem;

                WarehouseService.DeleteWarehouse(warehouse.Id);
                LoadData();
            }


        }

        private void tsbtnEdit_Click(object sender, EventArgs e)
        {
            var warehouse = (Warehouse)this.dataGridViewX1.CurrentRow.DataBoundItem;
            if (warehouse == null)
            {
                WarehouseEditForm form = new WarehouseEditForm(warehouse);
                form.ShowDialog();
                LoadData();

            }


        }

        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            WarehouseEditForm form = new WarehouseEditForm();

            form.ShowDialog();
            LoadData();
        }


    }
}
