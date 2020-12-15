using Blog.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace Blog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var scope = host.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userContext = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleContext = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var adminRole = new IdentityRole("Admin");

                if (!roleContext.Roles.Any())
                {
                    roleContext.CreateAsync(adminRole).GetAwaiter().GetResult();
                }

                if (!userContext.Users.Any(user => user.UserName == "admin"))
                {
                    var adminUser = new IdentityUser
                    {
                        UserName = "****",
                        Email = "****"
                    };
                    userContext.CreateAsync(adminUser, "***password***").GetAwaiter().GetResult();
                    userContext.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
