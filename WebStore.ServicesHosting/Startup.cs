﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebStore.DAL.Context;
using Microsoft.EntityFrameworkCore;
using WebStore.Interfaces.Services;
using WebStore.Services.InMemory;
using WebStore.Services.Sql;
using Microsoft.AspNetCore.Identity;
using WebStore.DomainNew.Entities;

namespace WebStore.ServicesHosting
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Добавляем EF Core
            services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection")));

            // Добавляем разрешение зависимостей
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            services.AddTransient<IProductData, SqlProductData>();
            services.AddTransient<IOrdersService, SqlOrdersService>();
            // Настройка Identity
            services.AddIdentity<User, IdentityRole>()
            .AddEntityFrameworkStores<WebStoreContext>()
            .AddDefaultTokenProviders();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
