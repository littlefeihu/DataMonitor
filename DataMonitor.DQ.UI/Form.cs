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

namespace DataMonitor.DQ.UI
{
    public partial class Form : Office2007RibbonForm
    {
        public Form()
        {
            InitializeComponent();
            this.EnableGlass = false;
            this.Text = System.Configuration.ConfigurationManager.AppSettings["name"];
        }
    }
}
