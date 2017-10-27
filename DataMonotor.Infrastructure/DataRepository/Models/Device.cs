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
        public decimal X { get; set; }
        public decimal Y { get; set; }

        [ForeignKey("DevicePositionId")]
        public virtual Position Position { get; set; }

    }
}
