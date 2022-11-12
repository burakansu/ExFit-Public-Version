using BussinesLayer;
using ExFit.Areas.Member.Models;
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
        public _MembersViewModel ViewModel()
        {
            _MembersViewModel VM = new _MembersViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager().GetMember(id);
            VM._MemberMeazurements = new MemberManager().GetMemberMeazurements(id);
            VM._MemberWeightArray = new MemberManager().GetMemberWeightsArray(id);
            VM._MemberDiet = new DietManager().GetDiets(VM._Member.Diet_ID, true)[0];
            VM._MemberExcersize = new ExcersizeManager().GetExcersizes(VM._Member.Excersize_ID, true)[0];
            VM.Foods = new FoodManager().GetFoods();
            VM.Practices = new PracticeManager().GetPractices();
            return VM;
        }
    }
}
