using ExFit.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Areas.Member.Controllers
{
    public class MyMeazurements : _MemberControllerBase
    {
        [Area("Member")]

        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
