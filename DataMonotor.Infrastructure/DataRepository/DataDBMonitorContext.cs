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
    public class DataDBMonitorContext : DbContext
    {
        public DataDBMonitorContext(string databaseName = "Data")
            : base(databaseName)
        {
        }

        public DbSet<MonitoringRecord> MonitoringRecords { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }

}
