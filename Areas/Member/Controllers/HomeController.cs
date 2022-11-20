using BussinesLayer;
using ExFit.Areas.Member.Models;
using ExFit.Controllers;
using ExFit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]
    public class HomeController : _MemberControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private Context context;
        public HomeController(ILogger<HomeController> logger, Context _context)
        {
            _logger = logger;
            context = _context;
        }
        public _HomeViewModel ViewModel()
        {
            _HomeViewModel VM = new _HomeViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager(context).GetMember(id);
            VM._MemberWeightArray = new MemberManager(context).GetMemberWeightsArray(id);
            VM._MemberDiet = new DietManager(context).GetDiets(VM._Member.Diet_ID, true)[0];
            VM._MemberExcersize = new ExcersizeManager(context).GetExcersizes(VM._Member.Excersize_ID, true)[0];
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
