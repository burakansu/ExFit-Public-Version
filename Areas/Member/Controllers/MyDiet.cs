using ExFit.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]

    public class MyDiet : _MemberControllerBase
    {
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
