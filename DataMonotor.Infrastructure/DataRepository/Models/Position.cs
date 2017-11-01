using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure.DataRepository.Models
{
    [Table("Position")]
    public class Position : Entity
    {

        public long ParentId { get; set; }


        public string PositionName { get; set; }


        public string Remark { get; set; }

        public string DistributionFilePath { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}
