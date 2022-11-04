using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class LogInController : Controller
    {
        UserManager userManager = new UserManager();
        public IActionResult SignIn()
        {
            HttpContext.Session.SetInt32("ID", 0);
            return View();
        }
        public IActionResult Entering(LogInViewModel logInViewModel)
        {
            logInViewModel.User = userManager.CheckUserEntering(logInViewModel.User);
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
