using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure.DataRepository.Models
{
    [Table("Device")]
    public class Device : Entity
    {
        public string DeviceName { get; set; }
        public string DeviceNum { get; set; }
        public string DataInfo { get; set; }
        public long DevicePositionId { get; set; }
        public string IPAddress { get; set; }
        public string Port { get; set; }
        public string Remark { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Connected { get; set; }

        /// <summary>
        /// 温度报警上限
        /// </summary>
        public decimal? TemperatureAlarmUpperLimit { get; set; }
        /// <summary>
        /// 温度报警下限
        /// </summary>
        public decimal? TemperatureAlarmLowerLimit { get; set; }
        /// <summary>
        /// 温度预警上限
        /// </summary>
        public decimal? TemperatureForewarningUpperLimit { get; set; }
        /// <summary>
        /// 温度预警下限
        /// </summary>
        public decimal? TemperatureForewarningLowerLimit { get; set; }
        /// <summary>
        /// 湿度报警上限
        /// </summary>
        public decimal? HumidityAlarmUpperLimit { get; set; }
        /// <summary>
        /// 湿度报警下限
        /// </summary>
        public decimal? HumidityAlarmLowerLimit { get; set; }
        /// <summary>
        /// 湿度预警上限
        /// </summary>
        public decimal? HumidityForewarningUpperLimit { get; set; }
        /// <summary>
        /// 湿度预警下限
        /// </summary>
        public decimal? HumidityForewarningLowerLimit { get; set; }

        /// <summary>
        /// 湿度限制可使用
        /// </summary>
        public bool HumidityLimitCanUse { get; set; }
        /// <summary>
        /// 温度限制可使用
        /// </summary>
        public bool TemperatureLimitCanUse { get; set; }

        /// <summary>
        /// 记录数据间隔 分钟
        /// </summary>
        public int? SaveRecordInterval { get; set; }
        /// <summary>
        /// 超限数据间隔 分钟
        /// </summary>
        public int? ExceedDataInterval { get; set; }


        [ForeignKey("DevicePositionId")]
        public virtual Position Position { get; set; }

        [NotMapped]
        public string GetwayAddress { get { return IPAddress + "|" + Port; } }

    }
}
