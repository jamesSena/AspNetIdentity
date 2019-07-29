using IdentityDev.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDev.Config
{
    public static class AuthorizationConfig
    {
        public static IServiceCollection AddAuthorizationConfig(this IServiceCollection services)
        {
            services.AddAuthorization(options => {
                options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));
                options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer"))); ;
                options.AddPolicy("PodeGravar", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeGravar")));

            });
            return services;
        }
    }
}
