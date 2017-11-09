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

        [ForeignKey("DevicePositionId")]
        public virtual Position Position { get; set; }

        [NotMapped]
        public string GetwayAddress { get { return IPAddress + "|" + Port; } }

    }
}
