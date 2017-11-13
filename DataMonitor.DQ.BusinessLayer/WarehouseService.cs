using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.Infrastructure.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.BusinessLayer
{
    public class WarehouseService
    {
        public static List<Warehouse> GetAllWarehouses()
        {
            var warehouses = DataAccess.Db.Warehouses.ToList();
            warehouses.Insert(0, new Warehouse { Id = 0, Name = "请选择" });
            return warehouses;
        }
        public static void AddWarehouse(Warehouse warehouse)
        {
            using (var db = DataAccess.Db)
            {
                db.Warehouses.Add(warehouse);
                db.SaveChanges();
            }
        }
        public static void UpdateWarehouse(Warehouse warehouse)
        {
            using (var db = DataAccess.Db)
            {
                var item = db.Warehouses.FirstOrDefault(o => o.Id == warehouse.Id);
                if (item != null)
                {
                    item.Name = warehouse.Name;
                    item.TemperatureAlarmUpperLimit = warehouse.TemperatureAlarmUpperLimit;
                    item.TemperatureAlarmLowerLimit = warehouse.TemperatureAlarmLowerLimit;
                    item.TemperatureForewarningLowerLimit = warehouse.TemperatureForewarningLowerLimit;
                    item.TemperatureForewarningUpperLimit = warehouse.TemperatureForewarningUpperLimit;
                    item.HumidityAlarmLowerLimit = warehouse.HumidityAlarmLowerLimit;
                    item.HumidityAlarmUpperLimit = warehouse.HumidityAlarmUpperLimit;
                    item.HumidityForewarningLowerLimit = warehouse.HumidityForewarningLowerLimit;
                    item.HumidityForewarningUpperLimit = warehouse.HumidityForewarningUpperLimit;
                    db.SaveChanges();
                }
            }


        }

        public static void DeleteWarehouse(long Id)
        {
            using (var db = DataAccess.Db)
            {
                db.Warehouses.Remove(db.Warehouses.FirstOrDefault(o => o.Id == Id));

                db.SaveChanges();
            }


        }


    }
}
