namespace WebApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using MediatR;
    using WebApp.Common;
    using WebApp.Models;
    using WebApp.Core;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var optionsBuilder = new DbContextOptionsBuilder<DbContext>()
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString"));

            services.AddScoped(t => new AppDbContext(optionsBuilder.Options));           
            services.AddScoped(t => new KafkaSettings(
                Configuration.GetConnectionString("Zookeeper"),
                Configuration.GetConnectionString("BrokerList")));

            services.AddAutoMapper();
            Mapper.AssertConfigurationIsValid();

            services.AddMediatR(typeof(Startup).Assembly);
            services.AddTransient<IGetRandomMembersTask, GetRandomMembersTask>();
            services.AddTransient<IGetRandomLocationsTask, GetRandomLocationsTask>();
            
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}