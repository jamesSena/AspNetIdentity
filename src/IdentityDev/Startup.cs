using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityDev.Areas.Identity.Data;
using IdentityDev.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityDev
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<IdentityDevContext>(options =>
                  options.UseOracle(
                      Configuration.GetConnectionString("AspNetIdentityContextConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDevContext>();


            //Utilizando lambda
           services.AddAuthorization(options => {
               options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));
               options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer"))); ;
               options.AddPolicy("PodeGravar", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeGravar")));

           });

            //Maneira tradicional
            //services.AddAuthorization(AuthorizationOptions);

            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        
        private void AuthorizationOptions(AuthorizationOptions options)
        {
            options.AddPolicy("PodeExcluir", PodeExcluir);
        }

        private void PodeExcluir(AuthorizationPolicyBuilder policy)
        {
            policy.RequireClaim("PodeExcluir");
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
