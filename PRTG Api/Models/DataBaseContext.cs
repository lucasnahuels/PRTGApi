using Microsoft.EntityFrameworkCore;

namespace PRTG_Api.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }
        public DbSet<Contract> Contract { get; set; }
        public DbSet<Email> Email { get; set; }
    }
}