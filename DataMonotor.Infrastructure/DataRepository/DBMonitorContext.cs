using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;
using DataMonitor.DQ.Infrastructure.DataRepository.Models;

namespace DataMonitor.DQ.Infrastructure.DataRepository
{
    public class DBMonitorContext : DbContext
    {
        public DBMonitorContext(string databaseName = "PrimaryData")
            : base(databaseName)
        {
        }

        public DbSet<Device> Devices { set; get; }
        public DbSet<Module> Modules { set; get; }
        public DbSet<Position> Positions { set; get; }
        public DbSet<Role> Roles { set; get; }
        public DbSet<RoleModule> RoleModules { set; get; }
        public DbSet<UserInfo> UserInfos { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }

}
