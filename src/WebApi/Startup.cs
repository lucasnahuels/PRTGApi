using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using ApplicationCore.Services.Extensions;
using Coravel;
using ApplicationCore.Services;
using ApplicationCore.Configuration;
using ApplicationCore.Models.Auth0;

namespace WebApi
{
    public class Startup
    {
        private const string PrtgCorsPolicy = "_prtgCorsPolicy";

        public Startup(IWebHostEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                  .AddEnvironmentVariables()
                  .Build();
            Configuration = configurationBuilder;
        }

        public static IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
                builder
                    .AddDebug()
                    .AddConsole()
                    .AddConfiguration(Configuration.GetSection("Logging"))
                    .SetMinimumLevel(LogLevel.Information)
            );

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration["Auth0:Authority"];
                options.Audience = Configuration["Auth0:Audience"];
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddCors(SetupCorsPolicyAction);
            services.AddHttpClient("prtg", c =>
            {
                c.BaseAddress = new Uri(Configuration["LosTerosUrl"]);

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });
            services.AddHttpClient("auth0", c =>
            {
                c.BaseAddress = new Uri(Configuration["Auth0Url"]);
            });
            services.AddDatabaseContext(string.Format(Configuration.GetConnectionString("prtg"), Configuration.GetConnectionString("prtgHost")));
            services.AddPRTGServices();

            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<TokenRequest>(Configuration.GetSection("Auth0Management"));

            services.AddScheduler();
            services.AddCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
            app.UseExceptionHandler("/Error");
            //TODO: when configuring a new Logger service
            //app.ConfigureExceptionHandler(logger);

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(PrtgCorsPolicy);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UpdateDatabase();

            var provider = app.ApplicationServices;
            provider.UseScheduler(scheduler =>
            {
                scheduler.Schedule<DailyRecordsScheduleService>()
                .DailyAt(14, 30)
                .Zoned(GetBuenosAiresTimeZoneInfo());
                scheduler.Schedule<DailyRecordsScheduleService>()
                .DailyAt(10, 30)
                .Zoned(GetBuenosAiresTimeZoneInfo()); 

                scheduler.Schedule<RefilledTonersRecordsScheduleService>()
                .DailyAt(10, 00)
                .Zoned(GetBuenosAiresTimeZoneInfo());
                scheduler.Schedule<RefilledTonersRecordsScheduleService>()
                .DailyAt(12, 00)
                .Zoned(GetBuenosAiresTimeZoneInfo());
                scheduler.Schedule<RefilledTonersRecordsScheduleService>()
                .DailyAt(15, 00)
                .Zoned(GetBuenosAiresTimeZoneInfo());
                scheduler.Schedule<RefilledTonersRecordsScheduleService>()
                .DailyAt(17, 00)
                .Zoned(GetBuenosAiresTimeZoneInfo());
            });
        }

        private void SetupCorsPolicyAction(CorsOptions options)
        {
            var domainsAllowed = "http://localhost:3000;https://prtg.it-one.com.ar;https://localhost:3000";

            if (!string.IsNullOrEmpty(domainsAllowed))
            {
                options.AddPolicy(
                    PrtgCorsPolicy,
                    builder =>
                    {
                        builder.WithOrigins(domainsAllowed.Split(';'))
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            }
        }

        private TimeZoneInfo GetBuenosAiresTimeZoneInfo()
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Argentina Standard Time");
        }
    }
}
