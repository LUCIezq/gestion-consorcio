using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PracticaParcial.Filters
{
    public class RedirectIfAuthenticatedFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity?.IsAuthenticated == true)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}