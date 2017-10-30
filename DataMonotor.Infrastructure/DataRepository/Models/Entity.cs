using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.Infrastructure.DataRepository.Models
{
    public class Entity
    {
        public int Id { get; set; }

        public string CreateBy { get; set; }


        public string UpdateBy { get; set; }


        public DateTime? CreateOn { get; set; }


        public DateTime? UpdateOn { get; set; }

    }
}
