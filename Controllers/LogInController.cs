using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult SignIn(string Msg = null)
        {
            // context.Database.EnsureCreated();
            ViewBag.Msg = Msg;
            HttpContext.Session.SetInt32("ID", 0);
            return View();
        }
        public IActionResult Register(string Msg = null)
        {
            HttpContext.Session.SetInt32("ID", 0);
            ViewBag.Msg = Msg;
            return View();
        }
        public IActionResult Registering(LogInViewModel VM)
        {
            int Count = new UserManager().CheckEmail(VM.User.Mail);
            if (Count == 0)
            {
                new CompanyManager().SaveCompany(VM.Company);
                new UserManager().SaveUser(VM.User, 1);
                return RedirectToAction("SignIn", new { Msg = "-" });
            }
            return RedirectToAction("Register", new { Msg = "-" });
        }
        public IActionResult Entering(LogInViewModel VM)
        {
            VM.User = new UserManager().CheckUserEntering(VM.User);
            if (VM.User.User_ID != 0)
            {
                HttpContext.Session.SetInt32("ID", VM.User.User_ID);

                new IncomeManager().UpdateIncomeAuto(VM.User.User_ID);
                new MemberManager().PasiveMemberAuto();

                return RedirectToAction("Index", "Home", new { ID = VM.User.User_ID });
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public IActionResult ForgetPassword()
        {
            HttpContext.Session.SetInt32("ID", 0);
            return View();
        }
    }
}
