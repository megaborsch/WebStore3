using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using WebStore.DAL.Context;
using WebStore.DomainNew.Entities;
//using Microsoft.EntityFrameworkCore;
using WebStore.Clients.Services.Employees;
using WebStore.Clients.Services.Products;
using WebStore.Clients.Services.Orders;
using WebStore.Clients.Services.Users;
//using WebStore.Clients.Services.Products;
//using WebStore.Services.Sql;
using WebStore.Interfaces.Services;
using WebStore.Interfaces.Api;
using WebStore.Infrastructure.Implementations;
//using WebStore.DomainNew.Entities;
using WebStore.Logger;
using WebStore.Services;
using WebStore.Services.Middleware;

namespace WebStore
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // Добавляем разрешение зависимости
            services.AddTransient<IEmployeesData, EmployeesClient>();
            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddTransient<IProductData, ProductsClient>();
            services.AddTransient<IOrdersService, OrdersClient>();
            services.AddTransient<IUsersClient, UsersClient>();

            services.AddTransient<IUserStore<User>, UsersClient>();
            services.AddTransient<IUserRoleStore<User>, UsersClient>();
            services.AddTransient<IUserClaimStore<User>, UsersClient>();
            services.AddTransient<IUserPasswordStore<User>, UsersClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UsersClient>();
            services.AddTransient<IUserEmailStore<User>, UsersClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UsersClient>();
            services.AddTransient<IUserLoginStore<User>, UsersClient>();
            services.AddTransient<IUserLockoutStore<User>, UsersClient>();
            services.AddTransient<IRoleStore<IdentityRole>, RolesClient>();

            //services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<User, IdentityRole>()
                //.AddEntityFrameworkStores<WebStoreContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequiredLength = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                //options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath =
                    "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath =
                    "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath =
                    "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });
            //Настройки для корзины
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartStore, CookiesCartStore>();






        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
            app.UseWelcomePage("/welcome");
            app.UseAuthentication();
            app.UseStatusCodePagesWithRedirects("~/home/errorstatus/{0}");

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            //var hello = Configuration["CustomHelloWorld"];
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync(hello);
            //});
            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                       name: "areas",
                       template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                     );
                    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                }
                );
        }
    }
}
