using ExFit.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]

    public class HomeController : _MemberControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
