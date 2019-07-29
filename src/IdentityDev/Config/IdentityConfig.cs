using IdentityDev.Areas.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDev.Config
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddAIdentityConfig(this IServiceCollection services, IConfiguration Configuration)
        {

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });

            services.AddDbContext<IdentityDevContext>(options =>
                  options.UseOracle(
                      Configuration.GetConnectionString("AspNetIdentityContextConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDevContext>();

            return services;
        }
    }
}
