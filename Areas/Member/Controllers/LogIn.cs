using BussinesLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _MembersViewModel = ExFit.Areas.Member.Models._MembersViewModel;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]
    public class LogIn : Controller
    {
        MemberManager memberManager = new MemberManager();
        public IActionResult SignIn()
        {
            HttpContext.Session.SetInt32("Member_ID", 0);
            return View();
        }
        public IActionResult Entering(_MembersViewModel Model)
        {
            Model._Member = memberManager.CheckMemberEntering(Model._Member);
            if (Model._Member.Member_ID != 0)
            {
                HttpContext.Session.SetInt32("Member_ID", Model._Member.Member_ID);
                return RedirectToAction("Index", "Home", new { ID = Model._Member.Member_ID });
            }
            else
            {
                return RedirectToAction("/MemberHome/Index");
            }
        }
    }
}
