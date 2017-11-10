using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure.DataRepository.Models
{
    [Table("Warehouse")]
    public class Warehouse : Entity
    {
        [DisplayName("库房属性")]
        public string Name { get; set; }
        /// <summary>
        /// 温度报警上限
        /// </summary>
        [DisplayName("温度报警上限")]
        public decimal? TemperatureAlarmUpperLimit { get; set; }
        /// <summary>
        /// 温度报警下限
        /// </summary>
         [DisplayName("温度报警下限")]
        public decimal? TemperatureAlarmLowerLimit { get; set; }
        /// <summary>
        /// 温度预警上限
        /// </summary>
        [DisplayName("温度预警上限")]
         public decimal? TemperatureForewarningUpperLimit { get; set; }
        /// <summary>
        /// 温度预警下限
        /// </summary>
        [DisplayName("温度预警下限")]
        public decimal? TemperatureForewarningLowerLimit { get; set; }
        /// <summary>
        /// 湿度报警上限
        /// </summary>
        [DisplayName("湿度报警上限")]
        public decimal? HumidityAlarmUpperLimit { get; set; }
        /// <summary>
        /// 湿度报警下限
        /// </summary>
         [DisplayName("湿度报警下限")]
        public decimal? HumidityAlarmLowerLimit { get; set; }
        /// <summary>
        /// 湿度预警上限
        /// </summary>
         [DisplayName("湿度预警上限")]
         public decimal? HumidityForewarningUpperLimit { get; set; }
        /// <summary>
        /// 湿度预警下限
        /// </summary>
        [DisplayName("湿度预警下限")]
         public decimal? HumidityForewarningLowerLimit { get; set; }
    }
}
