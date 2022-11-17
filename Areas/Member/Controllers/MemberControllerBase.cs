using BussinesLayer;
using ExFit.Areas.Member.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ExFit.Controllers
{
    public class _MemberControllerBase : Controller
    {
        ExcersizeManager EM = new ExcersizeManager();
        DietManager DM = new DietManager();
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
            VM._MemberDiet = DM.GetDiets(VM._Member.Diet_ID, true)[0];
            VM._MemberExcersize = EM.GetExcersizes(VM._Member.Excersize_ID, true)[0];
            VM.Foods = new FoodManager().GetFoods();
            VM.Practices = new PracticeManager().GetPractices();
            VM.C1 = EM.Counts(1, VM._Member.Excersize_ID);
            VM.C2 = EM.Counts(2, VM._Member.Excersize_ID);
            VM.C3 = EM.Counts(3, VM._Member.Excersize_ID);
            VM.C4 = EM.Counts(4, VM._Member.Excersize_ID);
            VM.C5 = EM.Counts(5, VM._Member.Excersize_ID);
            VM.C6 = EM.Counts(6, VM._Member.Excersize_ID);
            VM.C7 = EM.Counts(7, VM._Member.Excersize_ID);
            VM.D1 = DM.Counts(1, VM._Member.Diet_ID);
            VM.D2 = DM.Counts(2, VM._Member.Diet_ID);
            VM.D3 = DM.Counts(3, VM._Member.Diet_ID);
            VM.D4 = DM.Counts(4, VM._Member.Diet_ID);
            VM.D5 = DM.Counts(5, VM._Member.Diet_ID);
            VM.D6 = DM.Counts(6, VM._Member.Diet_ID);
            VM.D7 = DM.Counts(7, VM._Member.Diet_ID);
            return VM;
        }
    }
}
