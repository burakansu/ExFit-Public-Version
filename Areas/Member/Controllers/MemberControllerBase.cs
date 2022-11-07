using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExFit.Controllers
{
    public class _MemberControllerBase : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ControllerName = context.RouteData.Values["controller"].ToString();
            var ActionName = context.RouteData.Values["action"].ToString();

            if ((int)HttpContext.Session.GetInt32("Member_ID") == 0)
            {
                context.Result = LocalRedirect("/MemberHome/MemberLogIn/SignIn");
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
