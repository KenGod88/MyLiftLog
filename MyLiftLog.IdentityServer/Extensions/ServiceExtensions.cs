using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLiftLog.IdentityServer.Data;
using MyLiftLog.IdentityServer.Models;

namespace MyLiftLog.IdentityServer.Extensions
{
    public static class ServiceExtensions
    {
        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>();

            builder.Services.AddAuthentication()
                .AddLocalApi("Bearer", options =>
                {
                    options.ExpectedScope = "api1";
                });

            builder.Services.AddControllersWithViews();

            return builder;
        }
    }
}
