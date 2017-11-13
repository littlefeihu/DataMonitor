using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure.DataRepository.Models
{
    [Table("MonitoringRecord")]
    public class MonitoringRecord
    {
        public long Id { get; set; }

        public long DeviceId { get; set; }

        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public string DeviceNum { get; set; }
        public string DeviceRemark { get; set; }
        public string CreateOnStr { get; set; }
        public string DeviceName { get; set; }
        public DateTime CreateOn { get; set; }

        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }

    }
}
