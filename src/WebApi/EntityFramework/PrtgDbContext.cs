using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.Reports;

namespace WebApi.EntityFramework
{
    public class PrtgDbContext : DbContext
    {
        public PrtgDbContext(DbContextOptions<PrtgDbContext> options) : base(options) { }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<WeeklyPrinter> WeeklyPrinters { get; set; }
    }
}