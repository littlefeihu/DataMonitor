using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.UI.UIForm;
using DataMonitor.DQ.UI.UserControls;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMonitor.DQ.UI
{
    public partial class MainForm : Office2007RibbonForm
    {

        RealtimeControl control;
        public MainForm()
        {
            InitializeComponent();
            this.EnableGlass = false;
            this.Text = System.Configuration.ConfigurationManager.AppSettings["name"];
            ribbonControl1.Items[0].ShowSubItems = true;
            InitModules();
            control = new RealtimeControl();
            control.Height = 1004;
            control.Dock = DockStyle.Fill;
            control.MouseMoveEvent += control_MouseMove;
            panel1.Controls.Add(control);
            panel1.Height = 1005;

        }

        void control_MouseMove(MouseEventArgs obj)
        {
            labelItem1.Text = string.Format("X:{0},Y:{1}", obj.X, obj.Y);
        }

        private void InitModules()
        {
            List<ButtonItem> btnItems = new List<ButtonItem>();

            foreach (var tabItem in ribbonControl1.Items)
            {
                RibbonTabItem ribbonTabItem = tabItem as RibbonTabItem;
                if (ribbonTabItem == null)
                    continue;
                foreach (RibbonBar ribbonBar in ribbonTabItem.Panel.Controls)
                {
                    foreach (var item in ribbonBar.Items)
                    {
                        ButtonItem btnItem = item as ButtonItem;
                        if (btnItem != null)
                        {
                            btnItems.Add(btnItem);
                        }
                        else
                        {
                            ItemContainer itemContainer = item as ItemContainer;

                            foreach (var subitem in itemContainer.SubItems)
                            {
                                ButtonItem btnItem1 = subitem as ButtonItem;
                                btnItems.Add(btnItem1);
                            }
                        }
                    }
                }
            }

            foreach (var btnitem in btnItems)
            {
                btnitem.Enabled = Singleton.Instance.Modules.Exists(o => o.ModuleName == btnitem.Text);
            }

        }


        private void ribbonTabItem3_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {

            AppStartUp.ShutDown();
            Application.Exit();
        }

        private void switchButtonItem1_ValueChanged(object sender, EventArgs e)
        {
            ribbonControl1.Expanded = !ribbonControl1.Expanded;
        }
        /// <summary>
        /// 增加测点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            ButtonItem item = sender as ButtonItem;

            Singleton.Instance.EditDeviceMode = (EditDeviceMode)Enum.Parse(typeof(EditDeviceMode), item.Tag.ToString());


        }
        /// <summary>
        /// 库房属性列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonItem21_Click(object sender, EventArgs e)
        {
            MainWarehouseForm form = new MainWarehouseForm();
            form.StartPosition = FormStartPosition.CenterParent;
            form.ShowDialog();
        }
    }
}
