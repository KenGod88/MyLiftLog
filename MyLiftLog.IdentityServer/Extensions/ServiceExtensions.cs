using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyLiftLog.IdentityServer.Data;
using MyLiftLog.IdentityServer.Models;
using Duende.IdentityServer;

namespace MyLiftLog.IdentityServer.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Register DbContext
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            // Register ASP.NET Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure IdentityServer with ASP.NET Identity integration
            builder.Services.AddIdentityServer(options =>
            {
                options.EmitStaticAudienceClaim = true;
            })
            .AddDeveloperSigningCredential()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>();

            // Register Authentication
            builder.Services.AddAuthentication()
                .AddLocalApi("Bearer", options =>
                {
                    options.ExpectedScope = "api1";
                });

            // Register Razor Pages and MVC
            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
        }
    }
}
