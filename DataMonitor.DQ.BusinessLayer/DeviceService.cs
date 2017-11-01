using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.Infrastructure.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.BusinessLayer
{
    public class DeviceService
    {
        public static List<Device> GetAllDevices()
        {
            return DataAccess.Db.Devices.ToList();
        }

        public static List<Device> GetOnlineDevices()
        {
            return DataAccess.Db.Devices.Where(o => o.Connected).ToList();
        }

        public static List<Device> GetOfflineDevices()
        {
            return DataAccess.Db.Devices.Where(o => !o.Connected).ToList();
        }
    }
}
