using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.Infrastructure.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public static void AddDevice(Device device)
        {
            using (var db = DataAccess.Db)
            {
                db.Devices.Add(device);
                db.SaveChanges();
            }

        }
        public static void DeleteDevice(long Id)
        {
            using (var db = DataAccess.Db)
            {
                db.Devices.Remove(db.Devices.FirstOrDefault(o => o.Id == Id));

                db.SaveChanges();
            }


        }
        public static void UpdateDevice(Device device)
        {
            using (var db = DataAccess.Db)
            {
                var item = db.Devices.FirstOrDefault(o => o.Id == device.Id);
                if (item != null)
                {
                    item.DeviceName = device.DeviceName;
                    item.DeviceNum = device.DeviceNum;
                    item.DevicePositionId = device.DevicePositionId;
                    item.IPAddress = device.IPAddress;
                    item.Port = device.Port;
                    item.X = device.X;
                    item.Y = device.Y;
                    item.TemperatureAlarmLowerLimit = device.TemperatureAlarmLowerLimit;
                    item.TemperatureAlarmUpperLimit = device.TemperatureAlarmUpperLimit;
                    item.TemperatureForewarningLowerLimit = device.TemperatureForewarningLowerLimit;
                    item.TemperatureForewarningUpperLimit = device.TemperatureForewarningUpperLimit;
                    item.TemperatureLimitCanUse = device.TemperatureLimitCanUse;
                    item.HumidityAlarmLowerLimit = device.HumidityAlarmLowerLimit;
                    item.HumidityAlarmUpperLimit = device.HumidityAlarmUpperLimit;
                    item.HumidityForewarningLowerLimit = device.HumidityForewarningLowerLimit;
                    item.HumidityForewarningUpperLimit = device.HumidityForewarningUpperLimit;
                    item.HumidityLimitCanUse = device.HumidityLimitCanUse;
                    item.SaveRecordInterval = device.SaveRecordInterval;
                    item.ExceedDataInterval = device.ExceedDataInterval;

                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }

    }
}
