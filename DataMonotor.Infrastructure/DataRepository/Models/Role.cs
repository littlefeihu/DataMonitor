using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure.DataRepository.Models
{
    [Table("Role")]
    public class Role : Entity
    {

        public string RoleName { get; set; }

        public string Remark { get; set; }

        public virtual ICollection<UserInfo> Users { get; set; }

        public virtual ICollection<RoleModule> RoleModules { get; set; }

    }
}
