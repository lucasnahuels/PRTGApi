using Microsoft.EntityFrameworkCore;
using PRTG_Api.Models;

namespace PRTG_Api.EntityFramework
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Email> Emails { get; set; }
    }
}