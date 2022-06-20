using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom1.Models
{
    public class ServiceStationContext : DbContext
    {
        public ServiceStationContext() : base("ServiceStationContext")
        {

        }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> Persons { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Worker>().HasMany(c => c.Jobs).WithMany(s => s.Workers).Map(t => t.MapLeftKey("WorkersId").MapRightKey("JobsId").ToTable("WorkersJobs"));
            modelBuilder.Entity<Worker>().HasMany(c => c.Days).WithMany(s => s.Workers).Map(t => t.MapLeftKey("WorkersId").MapRightKey("DaysId").ToTable("WorkersDays"));
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
