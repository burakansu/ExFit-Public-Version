using BussinesLayer;
using ExFit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ObjectLayer;

namespace ExFit.Controllers
{
    public class MemberControllerBase : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ControllerName = context.RouteData.Values["controller"].ToString();
            var ActionName = context.RouteData.Values["action"].ToString();

            if (HttpContext.Session.GetInt32("ID") != null)
            {
                if ((int)HttpContext.Session.GetInt32("ID") == 0)
                {
                    context.Result = LocalRedirect("/LogIn/SignIn");
                }
                else
                {
                    base.OnActionExecuting(context);
                }
            }
            else
            {
                context.Result = LocalRedirect("/LogIn/SignIn");
            }
        }
    }
}
