using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace pawnShop.Validated
{
    public class ValidarSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var httpContext = filterContext.HttpContext;

            if (httpContext.Session.GetString("userId") == null)
            {
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
 

            base.OnActionExecuting(filterContext);
        }
    }
}
