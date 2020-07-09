using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Services.Interfaces;
using WebApi.Amazon.DynamoDb.Context;
using WebApi.Models;

namespace WebApi.Services.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddDatabaseContext(this IServiceCollection services)
        {
            services.AddTransient<IDynamoDBContext, DynamoDbContext<Employee>>();
            services.AddTransient<IDynamoDBContext, DynamoDbContext<Email>>();
            services.AddTransient<IDynamoDBContext, DynamoDbContext<Contract>>();
        }

        public static void AddPRTGServices(this IServiceCollection services)
        {
            services.AddTransient<ISensorService, SensorService>();
            services.AddTransient<IContractService, ContractService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
