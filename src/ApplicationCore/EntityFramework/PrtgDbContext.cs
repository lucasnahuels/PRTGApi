using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
using ApplicationCore.Models.Reports;

namespace ApplicationCore.EntityFramework
{
    public class PrtgDbContext : DbContext
    {
        public PrtgDbContext(DbContextOptions<PrtgDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<DailyContadoresDataDevices> DailyContadores { get; set; }
        public DbSet<DailyTonersDataDevices> DailyToners { get; set; }
    }
}