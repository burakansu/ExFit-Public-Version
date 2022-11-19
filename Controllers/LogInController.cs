using BussinesLayer;
using DatabaseLayer.ExFit_Database;
using ExFit.Data;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ObjectLayer;
using System.Linq;

namespace ExFit.Controllers
{
    public class LogInController : Controller
    {
        private Context context;
        public LogInController(Context _context)
        {
            context = _context;
        }
        public IActionResult SignIn()
        {
           // context.Database.EnsureCreated();
            HttpContext.Session.SetInt32("ID", 0);
            return View();
        }
        public IActionResult Entering(LogInViewModel logInViewModel)
        {
            logInViewModel.User = new UserManager(context).CheckUserEntering(logInViewModel.User);
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
