using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebStore.Data;
using WebStore.DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DomainNew.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebStore.DomainNew.Models;


namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<WebStoreContext>();
                    DbInitializer.Initialize(context);
                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore,
                    new IRoleValidator<IdentityRole>[] { },
                    new UpperInvariantLookupNormalizer(),
                    new IdentityErrorDescriber(), null);
                    if (!roleManager.RoleExistsAsync(Constants.Roles.User).Result)
                    {
                    var role = new IdentityRole(Constants.Roles.User);
                    var result = roleManager.CreateAsync(role).Result;
                    }
                    if (!roleManager.RoleExistsAsync(Constants.Roles.Administrator).Result)
                    {
                    var role = new IdentityRole(Constants.Roles.Administrator);
                    var result = roleManager.CreateAsync(role).Result;
                    }
                    var userStore = new UserStore<User>(context);
                    var userManager = new UserManager<User>(userStore, new
                    OptionsManager<IdentityOptions>(
                        new OptionsFactory<IdentityOptions>(
                            new IConfigureOptions<IdentityOptions>[] { },
                    new IPostConfigureOptions<IdentityOptions>[] { })),
                    new PasswordHasher<User>(), 
                    new IUserValidator<User>[] { }, 
                    new IPasswordValidator<User>[] { },
                    new UpperInvariantLookupNormalizer(), new IdentityErrorDescriber(), null, null);
                    if (userStore.FindByEmailAsync("admin@mail.com", CancellationToken.None).Result == null)
                    {
                    var user = new User() { UserName = "Admin", Email = "admin@mail.com" };
                    var result = userManager.CreateAsync(user, "admin").Result;
                    if (result == IdentityResult.Success)
                    {
                    var roleResult = userManager.AddToRoleAsync(user, "Administrator").Result;
                    }
                    }

                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
            //CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();
    }
}
