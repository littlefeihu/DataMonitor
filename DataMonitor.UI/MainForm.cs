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

namespace DataMonitor.UI
{
    public partial class MainForm : Office2007Form
    {
        public MainForm()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();
            this.EnableGlass = false;
            this.currentTiem.Text = string.Format("{0:F}", DateTime.Now);
            this.Text = System.Configuration.ConfigurationManager.AppSettings["name"];
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            this.currentTiem.Text = string.Format("{0:F}", DateTime.Now);
        }

        private void panelEx1_Click(object sender, EventArgs e)
        {

        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {

        }
        private DevComponents.DotNetBar.ButtonItem m_PopupFromCode = null;

        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (m_PopupFromCode == null)
                CreatePopupMenu();
            // Apply style
            DevComponents.DotNetBar.eDotNetBarStyle style = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;

            m_PopupFromCode.Style = style;

            // MUST ALWAYS register popup with DotNetBar Manager if popup does not belong to ContextMenus collection
            dotNetBarManager1.RegisterPopup(m_PopupFromCode);

            // Place the menu just below the button
            Control ctrl = sender as Control;
            Point p = this.PointToScreen(new Point(ctrl.Left, ctrl.Bottom));
            m_PopupFromCode.PopupMenu(p);
        }




        private void CreatePopupMenu()
        {
            DevComponents.DotNetBar.ButtonItem item;

            m_PopupFromCode = new DevComponents.DotNetBar.ButtonItem();

            // Create items
            item = new DevComponents.DotNetBar.ButtonItem("bInsertBreakpoint");
            item.Text = "企业&仓库";
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bNewBreakpoint");
            item.Text = "数据接口";
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bRunToCursor");
            item.Text = "设备管理";
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("bAddTask");
            item.Text = "报警设置";
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);


            item = new DevComponents.DotNetBar.ButtonItem("display");
            item.Text = "显示";
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);


            item = new DevComponents.DotNetBar.ButtonItem("system");
            item.Text = "系统";
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);

            item = new DevComponents.DotNetBar.ButtonItem("advance");
            item.Text = "高级";
            item.BeginGroup = true;
            m_PopupFromCode.SubItems.Add(item);

        }

    }
}
