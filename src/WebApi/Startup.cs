using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using WebApi.Authorization;
using WebApi.Services.Extensions;

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
            //services.AddAuthorization(
            //    options =>
            //    {
            //        options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme).Build();
            //        //options.AddPolicy("", policy => policy.Requirements.Add(new CognitoGroupAuthorizationRequirement("")));
            //    });

            services.AddSingleton<IAuthorizationHandler, CognitoGroupAuthorizationHandler>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Audience = "";
                o.Authority = "";
                o.RequireHttpsMetadata = false;
            });
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
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
            services.AddCors(SetupCorsPolicyAction);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UpdateDatabase();
        }

        private void SetupCorsPolicyAction(CorsOptions options)
        {
            var domainsAllowed = "http://localhost:3000;https://dev.d115kdf62p0izj.amplifyapp.com/";

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
