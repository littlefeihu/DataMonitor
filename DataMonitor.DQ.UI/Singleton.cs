using DataMonitor.DQ.Infrastructure.DataRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor.DQ.UI
{
    public class Singleton
    {
        public static Singleton Instance = new Singleton();


        public UserInfo CurrentUser { get; set; }


        public List<Module> Modules { get; set; }
    }
}
