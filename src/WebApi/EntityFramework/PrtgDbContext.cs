using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.EntityFramework
{
    public class PrtgDbContext : DbContext
    {
        public PrtgDbContext(DbContextOptions<PrtgDbContext> options) : base(options) { }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}