using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApplicationCore.EntityFramework;
using ApplicationCore.Services.Interfaces;
using ApplicationCore.Services.Interfaces.Reports;

namespace ApplicationCore.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<PrtgDbContext>(options => options.UseNpgsql(connectionString));
        }

        public static void UpdateDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<PrtgDbContext>();
            context.Database.Migrate();
        }

        public static void AddPRTGServices(this IServiceCollection services)
        {
            services.AddTransient<ISensorService, SensorService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IDailyPrinterService, DailyPrinterService>();
        }
    }
}
