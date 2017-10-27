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
            System.Data.SQLite.SQLiteConnectionStringBuilder connstr = new System.Data.SQLite.SQLiteConnectionStringBuilder(this.Database.Connection.ConnectionString);
            string path = AppDomain.CurrentDomain.BaseDirectory + connstr.DataSource;
            System.IO.FileInfo fi = new System.IO.FileInfo(path);
            if (System.IO.File.Exists(fi.FullName) == false)
            {
                if (System.IO.Directory.Exists(fi.DirectoryName) == false)
                {
                    System.IO.Directory.CreateDirectory(fi.DirectoryName);
                }
                SQLiteConnection.CreateFile(fi.FullName);

                connstr.DataSource = path;
                //connstr.Password = "admin";//设置密码，SQLite ADO.NET实现了数据库密码保护
                using (SQLiteConnection conn = new SQLiteConnection(connstr.ConnectionString))
                {
                    string sql = @" CREATE TABLE User (
   Id INTEGER PRIMARY KEY AUTOINCREMENT,
   Name varchar (20),
   Time timestamp,
   Data blob,
   Val real,
   TestE int);";
                    conn.Open();
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
