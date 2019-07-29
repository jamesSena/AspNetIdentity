using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IdentityDev.Extensions
{

    public class CustomAuthorization
    {
        public static bool ValidaClaimsUsuario(HttpContext context, string claimName, string cliamValue)
        {
            return context.User.Identity.IsAuthenticated && //Verifica se o usuario está logado.
                context.User.Claims.Any(c => c.Type.Equals(claimName) && c.Value.Contains(cliamValue));
        }
    }
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimsName, string claimsValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimsName, claimsValue) };
        }
    }
    public class RequisitoClaimFilter : IAuthorizationFilter
    {
         readonly Claim _claim;

        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
            }
            if (!CustomAuthorization.ValidaClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
