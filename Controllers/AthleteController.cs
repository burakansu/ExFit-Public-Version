using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class AthleteController : Controller
    {
        MemberManager memberManager = new MemberManager();
        DietManager dietManager = new DietManager();
        Excersize_Manager excersizeManager = new Excersize_Manager();
        MembersViewModel Model = new MembersViewModel();
        public IActionResult Index()
        {
            return View();
        }
    }
}
