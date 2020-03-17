using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRTG_Api.Models;

namespace PRTG_Api.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }
        public DbSet<PRTG_Api.Models.Contract> Contract { get; set; }
    }
}