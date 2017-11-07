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
        public MainForm()
        {
            InitializeComponent();
            this.EnableGlass = false;
            this.Text = System.Configuration.ConfigurationManager.AppSettings["name"];
            ribbonControl1.Items[0].ShowSubItems = true;
            InitModules();

            
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
            Application.Exit();
        }

        private void switchButtonItem1_ValueChanged(object sender, EventArgs e)
        {
            ribbonControl1.Expanded = !ribbonControl1.Expanded;
        }
    }
}
