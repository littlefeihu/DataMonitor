using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure.DataRepository.Models
{
    [Table("RoleModule")]
    public class RoleModule : Entity
    {
        public long RoleId { get; set; }

        public long ModuleId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [ForeignKey("ModuleId")]
        public virtual Module Module { get; set; }



    }
}
