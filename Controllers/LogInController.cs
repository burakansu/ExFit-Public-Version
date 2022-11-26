using BussinesLayer;
using ExFit.Data;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class LogInController : Controller
    {
        public IActionResult SignIn()
        {
           // context.Database.EnsureCreated();
            HttpContext.Session.SetInt32("ID", 0);
            return View();
        }
        public IActionResult Register()
        {
            HttpContext.Session.SetInt32("ID", 0);
            return View();
        }
        public IActionResult Registering(LogInViewModel VM)
        {
            int Count = new UserManager().CheckEmail(VM.User.Mail);
            if (Count == 0)
            {
                new CompanyManager().SaveCompany(VM.Company);
                new UserManager().SaveUser(VM.User, 1);
                return RedirectToAction("SignIn");
            }
            return RedirectToAction("Register");
        }
        public IActionResult Entering(LogInViewModel VM)
        {
            VM.User = new UserManager().CheckUserEntering(VM.User);
            if (VM.User.User_ID != 0)
            {
                HttpContext.Session.SetInt32("ID", VM.User.User_ID);
                switch (VM.User.Type)
                {
                    case 1:
                        return RedirectToAction("Index", "Home", new { ID = VM.User.User_ID });
                    case 2:
                        return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("SignIn");
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
