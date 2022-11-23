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
            new CompanyManager().SaveCompany(VM.Company, VM.User);
            return RedirectToAction("SignIn");
        }
        public IActionResult Entering(LogInViewModel logInViewModel)
        {
            logInViewModel.User = new UserManager().CheckUserEntering(logInViewModel.User);
            if (logInViewModel.User.User_ID != 0)
            {
                HttpContext.Session.SetInt32("ID", logInViewModel.User.User_ID);
                switch (logInViewModel.User.Type)
                {
                    case 1:
                        return RedirectToAction("Index", "Home", new { ID = logInViewModel.User.User_ID });
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
    }
}
