using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDev.Extensions
{
    public static class RazorExtensions
    {
        public static bool IfClaim(this RazorPage page, string claimName, string cliamValue)
        {
            return CustomAuthorization.ValidaClaimsUsuario(page.Context, claimName, cliamValue);

        }
        public static string IfClaimShow(this RazorPage page, string claimName, string cliamValue)
        {
            return CustomAuthorization.ValidaClaimsUsuario(page.Context, claimName, cliamValue) ?"":"disabled";

        }
        public static IHtmlContent IfClaimShow(this IHtmlContent htmlContent, HttpContext HttpContext, string claimName, string cliamValue)
        {
            return CustomAuthorization.ValidaClaimsUsuario(HttpContext, claimName, cliamValue) ? htmlContent : null ;

        }
    }
}
