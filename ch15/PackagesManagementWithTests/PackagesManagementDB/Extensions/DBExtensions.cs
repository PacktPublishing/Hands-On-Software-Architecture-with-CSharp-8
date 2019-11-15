using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PackagesManagementDB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DDD.DomainLayer;

namespace PackagesManagementDB.Extensions
{
    public static class DBExtensions
    {
        public static IServiceCollection AddDbLayer(this IServiceCollection services, 
            string connectionString, string migrationAssembly)
        {
            services.AddDbContext<MainDBContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationAssembly)));
            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>()
                .AddEntityFrameworkStores<MainDBContext>()
                .AddDefaultTokenProviders();
            services.AddAllRepositories(typeof(DBExtensions).Assembly);
            return services;
        }
        public static async void UseDBLayer(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MainDBContext>();
                context.Database.Migrate();
                await Seed(context, serviceScope);

            }
        }
        private static async Task Seed(MainDBContext context, IServiceScope serviceScope)
        {
            
           if (!await context.Roles.AnyAsync())
            {
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole<int>>>();
                var role = new IdentityRole<int> { Name = "Admins"};
                await roleManager.CreateAsync(role);

            }
            if (!await context.Users.AnyAsync())
            {

                var userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser<int>>>();
                string user = "Admin";
                string password = "_Admin_pwd1";
                IdentityUser<int> currUser = null;
                var result = await userManager.CreateAsync(currUser = new IdentityUser<int> { UserName = user, Email = "admin@fakedomain.com", EmailConfirmed = true }, password);

                await userManager.AddToRoleAsync(currUser, "Admins");

            }
            if (!await context.Destinations.AnyAsync())
            {
                var firstDestination = new Destination
                {
                    Name = "Florence",
                    Country = "Italy",
                    Packages = new List<Package>()
                        {
                            new Package
                            {
                                Name = "Summer in Florence",
                                StartValidityDate = new DateTime(2019, 6, 1),
                                EndValidityDate = new DateTime(2019, 10, 1),
                                DuratioInDays=7,
                                Price=1000,
                                EntityVersion=1
                            },
                            new Package
                            {
                                Name = "Winter in Florence",
                                StartValidityDate = new DateTime(2019, 12, 1),
                                EndValidityDate = new DateTime(2020, 2, 1),
                                DuratioInDays=7,
                                Price=500,
                                EntityVersion=1
                            }
                        }
                };
                context.Destinations.Add(firstDestination);
                await context.SaveChangesAsync();
            }
        }
        
    }
}
