using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class AnalyzeRoomController : ExFitControllerBase
    {
        public IActionResult Analyze()
        {
            return RedirectToAction("Analyze");
        }
    }
}
