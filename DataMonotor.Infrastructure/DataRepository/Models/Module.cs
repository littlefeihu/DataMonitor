using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure.DataRepository.Models
{
    [Table("Module")]
    public class Module : Entity
    {
        public long ParentId { get; set; }
        public string ModuleName { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<RoleModule> RoleModules { get; set; }
    }
}
