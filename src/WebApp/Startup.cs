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
    using DotNetCore.CAP;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddRazorPagesOptions(o=> 
                {
                    o.Conventions.AddFolderApplicationModelConvention("/", e=> e.Filters.Add(new DbContextTransactionFilter()));
                });

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

            services.AddCap(t=>
            {
                t.UseEntityFramework<AppDbContext>();
                t.UseKafka(Configuration.GetConnectionString("BrokerList"));
            });
            
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
            app.UseCap();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}