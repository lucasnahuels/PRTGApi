using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
using ApplicationCore.Models.Reports;

namespace ApplicationCore.EntityFramework
{
    public class PrtgDbContext : DbContext
    {
        public PrtgDbContext(DbContextOptions<PrtgDbContext> options) : base(options) { }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<DailyContadoresDataDevices> DailyPrinters { get; set; }
    }
}