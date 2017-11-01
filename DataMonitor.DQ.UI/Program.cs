using DataMonitor.DQ.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMonitor.DQ.UI
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitDatabase();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }

        private static void InitDatabase()
        {
            string baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

            var dataDirectory = Path.Combine(baseDirectory, "Data");
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            var db1 = Path.Combine(dataDirectory, "DataMonitorPrimary.db");
            var db2 = Path.Combine(dataDirectory, "DQData.db");

            if (!File.Exists(db1))
            {
                using (FileStream fs = new FileStream(db1, FileMode.Create))
                {
                    fs.Write(Properties.Resources.DataMonitorPrimary, 0, Properties.Resources.DataMonitorPrimary.Length);
                }
            }

            if (!File.Exists(db2))
            {
                using (FileStream fs = new FileStream(db2, FileMode.Create))
                {
                    fs.Write(Properties.Resources.DQData, 0, Properties.Resources.DQData.Length);
                }
            }

        }
    }
}
