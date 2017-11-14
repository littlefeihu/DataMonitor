using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure.DTO
{
    /// <summary>
    /// 历史记录实体
    /// </summary>
    public class HisRecord
    {
        public decimal Temperature { get; set; }

        public decimal Humidity { get; set; }

        public string DeviceAddressHex { get; set; }

        public string DatetimeStr { get; set; }
    }
}
