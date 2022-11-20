using BussinesLayer;
using ExFit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using _MembersViewModel = ExFit.Areas.Member.Models._MembersViewModel;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]
    public class LogIn : Controller
    {
        private Context context;
        public LogIn(Context _context)
        {
            context = _context;
        }
        public IActionResult SignIn()
        {
            HttpContext.Session.SetInt32("Member_ID", 0);
            return View();
        }
        public IActionResult Entering(_MembersViewModel Model)
        {
            Model._Member = new MemberManager(context).CheckMemberEntering(Model._Member);
            if (Model._Member.Member_ID != 0)
            {
                HttpContext.Session.SetInt32("Member_ID", Model._Member.Member_ID);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("SignIn", "LogIn");
            }
        }
    }
}
