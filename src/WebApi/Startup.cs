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

namespace WebApi
{
    public class Startup
    {
        private const string PrtgCorsPolicy = "_prtgCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCognitoIdentity();

            var region = Configuration["AWS:Region"];
            var userPoolId = Configuration["AWS:UserPoolId"];
            var appClientId = Configuration["AWS:AppClientId"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Audience = appClientId;
                o.Authority = $"https://cognito-idp.{region}.amazonaws.com/{userPoolId}";
                o.RequireHttpsMetadata = false;
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
            services.AddDefaultAWSOptions(Configuration.GetAWSOptions());
            services.AddDatabaseContext(Configuration.GetConnectionString("prtg"));
            services.AddPRTGServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            loggerFactory.AddLambdaLogger();            
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
        }

        private void SetupCorsPolicyAction(CorsOptions options)
        {
            var domainsAllowed = "http://localhost:3000;https://dev.d115kdf62p0izj.amplifyapp.com/;https://prtg.it-one.com.ar";

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
    }
}
