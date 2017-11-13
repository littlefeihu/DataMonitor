using DataMonitor.DQ.Infrastructure;
using DataMonitor.DQ.Infrastructure.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.BusinessLayer
{
    public class MonitoringRecordService
    {
        /// <summary>
        /// 添加单条记录
        /// </summary>
        /// <param name="record"></param>
        public static void SaveRecord(MonitoringRecord record)
        {
            using (var db = DataAccess.Db_DataRecord)
            {
                db.MonitoringRecords.Add(record);
                db.SaveChanges();
            }
        }
        /// <summary>
        /// 批量添加记录数据
        /// </summary>
        /// <param name="records"></param>
        public static void SaveRecords(IEnumerable<MonitoringRecord> records)
        {
            using (var db = DataAccess.Db_DataRecord)
            {
                foreach (var record in records)
                {
                    db.MonitoringRecords.Add(record);
                }
                db.SaveChanges();
            }
        }



    }
}
