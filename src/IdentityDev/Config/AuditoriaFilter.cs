using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityDev.Config
{
    public class AuditoriaFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine(context.ModelState);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine(context.ModelState);
        }
    }
}
