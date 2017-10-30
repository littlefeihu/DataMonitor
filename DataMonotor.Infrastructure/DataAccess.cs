using DataMonitor.DQ.Infrastructure.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure
{
    public class DataAccess
    {
        public static DBMonitorContext Db
        {
            get
            {
                return new DBMonitorContext();
            }
        }

    }
}
