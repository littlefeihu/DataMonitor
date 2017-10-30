using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure
{
    public class ManifestResourceHelper
    {
        public static void WriteManifestResource(string resourceName, string targetName)
        {
            using (Stream sm = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                byte[] bs = new byte[sm.Length];
                sm.Read(bs, 0, (int)sm.Length);

                using (FileStream fs = new FileStream(targetName, FileMode.Create))
                {
                    fs.Write(bs, 0, bs.Length);
                }
            }
        }
    }
}
