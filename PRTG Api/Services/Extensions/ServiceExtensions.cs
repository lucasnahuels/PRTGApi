using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PRTG_Api.EntityFramework;
using PRTG_Api.Services.Interfaces;

namespace PRTG_Api.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataBaseContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddPRTGServices(this IServiceCollection services)
        {
            services.AddTransient<ISensorService, SensorService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
