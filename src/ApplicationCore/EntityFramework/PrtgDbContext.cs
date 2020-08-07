using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
using ApplicationCore.Models.Reports;

namespace ApplicationCore.EntityFramework
{
    public class PrtgDbContext : DbContext
    {
        public PrtgDbContext(DbContextOptions<PrtgDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<DailyContadoresDataDevices> DailyContadores { get; set; }
        public DbSet<DailyTonersDataDevices> DailyToners { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }

        public DbSet<ContractDevice> ContractDevices { get; set; }
        public DbSet<ContractEmployee> ContractEmployees { get; set; }
        public DbSet<ContractUser> ContractUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if(modelBuilder != null)
            {
                modelBuilder.Entity<Device>(d =>
                {
                    d.HasKey(d => d.ObjId);
                    d.ToTable("Device");
                });

                modelBuilder.Entity<User>(d =>
                {
                    d.HasKey(u => u.UserId);
                    d.ToTable("User");
                });

                #region Many to many relations
                // Code to set up many to many relationships

                //Contract Device
                modelBuilder.Entity<ContractDevice>()
                    .HasOne(c => c.Contract)
                    .WithMany(cd => cd.ContractDevices)
                    .HasForeignKey(c => c.ContractId);

                modelBuilder.Entity<ContractDevice>()
                    .HasOne(d => d.Device)
                    .WithMany(cd => cd.ContractDevices)
                    .HasForeignKey(d => d.ObjId);

                //Contract Employee
                modelBuilder.Entity<ContractEmployee>()
                    .HasOne(c => c.Contract)
                    .WithMany(de => de.ContractEmployees)
                    .HasForeignKey(c => c.ContractId);

                modelBuilder.Entity<ContractEmployee>()
                    .HasOne(e => e.Employee)
                    .WithMany(ce => ce.ContractEmployees)
                    .HasForeignKey(e => e.EmployeeId);                

                //Contract User
                modelBuilder.Entity<ContractUser>()
                    .HasOne(c => c.Contract)
                    .WithMany(cu => cu.ContractUsers)
                    .HasForeignKey(c => c.ContractId);

                modelBuilder.Entity<ContractUser>()
                    .HasOne(u => u.User)
                    .WithMany(cu => cu.ContractUsers)
                    .HasForeignKey(u => u.UserId);

                //Keys for many to many relations
                modelBuilder.Entity<ContractEmployee>().HasKey
                    (ce => new { ce.ContractId, ce.EmployeeId });

                modelBuilder.Entity<ContractDevice>().HasKey
                    (cd => new { cd.ContractId, cd.ObjId });

                modelBuilder.Entity<ContractUser>().HasKey
                    (cu => new { cu.ContractId, cu.UserId });

                #endregion
            }
        }
    }
}