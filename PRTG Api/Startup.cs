using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using PRTG_Api.Models;
using PRTG_Api.Services;
using System.Net.Http;
using System.Net;
using AutoMapper;

namespace PRTG_Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapperProfiles));

            services.AddDatabaseContext(Configuration.GetConnectionString("PRTGApi"));

            //services.AddDbContext<DataBaseContext>(opt =>
            //{
            //    opt.UseInMemoryDatabase("TodoList");
            //});
            services.AddControllers();
            services.AddHttpClient("prtg", c =>
            {
                c.BaseAddress = new Uri(Configuration["LosTerosUrl"]);

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });
            services.AddTransient<ISensorService, SensorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/error");
            }

            app.UseCors((builder) => builder.WithOrigins("https://localhost:44323").AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
