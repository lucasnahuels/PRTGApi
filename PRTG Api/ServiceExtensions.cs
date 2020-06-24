using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PRTG_Api.Models;

namespace PRTG_Api
{
    public static class ServiceExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
