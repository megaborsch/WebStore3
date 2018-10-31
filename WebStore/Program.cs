using System.IO;
using System.Reflection;

using System.Xml;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;


namespace WebStore
{
    public class Program
    {
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(Program));

        public static void Main(string[] args)
        {
            //var log4NetConfig = new XmlDocument();
            //log4NetConfig.Load(File.OpenRead("log4net.config"));
            //var repo = log4net.LogManager.CreateRepository(
            //Assembly.GetEntryAssembly(),
            //typeof(log4net.Repository.Hierarchy.Hierarchy));
            //log4net.Config.XmlConfigurator.Configure(repo,
            //log4NetConfig["log4net"]);
            //Log.Info("Application - Main is invoked");

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        var host = BuildWebHost(args);

    //        using (var scope = host.Services.CreateScope())
    //        {
    //            var services = scope.ServiceProvider;
    //            try
    //            {
    //                var context = services.GetRequiredService<WebStoreContext>();
    //                DbInitializer.Initialize(context);
    //                var roleStore = new RoleStore<IdentityRole>(context);
    //                var roleManager = new RoleManager<IdentityRole>(roleStore,
    //                new IRoleValidator<IdentityRole>[] { },
    //                new UpperInvariantLookupNormalizer(),
    //                new IdentityErrorDescriber(), null);
    //                if (!roleManager.RoleExistsAsync(Constants.Roles.User).Result)
    //                {
    //                var role = new IdentityRole(Constants.Roles.User);
    //                var result = roleManager.CreateAsync(role).Result;
    //                }
    //                if (!roleManager.RoleExistsAsync(Constants.Roles.Administrator).Result)
    //                {
    //                var role = new IdentityRole(Constants.Roles.Administrator);
    //                var result = roleManager.CreateAsync(role).Result;
    //                }
    //                var userStore = new UserStore<User>(context);
    //                var userManager = new UserManager<User>(userStore, new
    //                OptionsManager<IdentityOptions>(
    //                    new OptionsFactory<IdentityOptions>(
    //                        new IConfigureOptions<IdentityOptions>[] { },
    //                new IPostConfigureOptions<IdentityOptions>[] { })),
    //                new PasswordHasher<User>(), 
    //                new IUserValidator<User>[] { }, 
    //                new IPasswordValidator<User>[] { },
    //                new UpperInvariantLookupNormalizer(), new IdentityErrorDescriber(), null, null);
    //                if (userStore.FindByEmailAsync("admin@mail.com", CancellationToken.None).Result == null)
    //                {
    //                var user = new User() { UserName = "Admin", Email = "admin@mail.com" };
    //                var result = userManager.CreateAsync(user, "admin").Result;
    //                if (result == IdentityResult.Success)
    //                {
    //                var roleResult = userManager.AddToRoleAsync(user, "Administrator").Result;
    //                }
    //                }

    //            }
    //            catch (Exception ex)
    //            {
    //                var logger = services.GetRequiredService<ILogger<Program>>();
    //                logger.LogError(ex, "An error occurred while seeding the database.");
    //            }
    //        }

    //        host.Run();
    //        //CreateWebHostBuilder(args).Build().Run();
    //    }

    //    public static IWebHost BuildWebHost(string[] args) =>
    //        WebHost.CreateDefaultBuilder(args)
    //            .UseStartup<Startup>()
    //            .Build();

    //    //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    //    //    WebHost.CreateDefaultBuilder(args)
    //    //        .UseStartup<Startup>();
    //}
}
