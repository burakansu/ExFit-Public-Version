using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class LandingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
