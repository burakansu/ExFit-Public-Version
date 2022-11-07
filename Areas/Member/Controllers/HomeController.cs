using BussinesLayer;
using ExFit.Areas.Member.Models;
using ExFit.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]
    public class HomeController : _MemberControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        MemberManager memberManager = new MemberManager();
        DietManager dietManager = new DietManager();
        ExcersizeManager excersizeManager = new ExcersizeManager();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(int id)
        {
            _MembersViewModel Model = new _MembersViewModel();
            Model._Member = memberManager.GetMember(id);
            Model._MemberMeazurements = memberManager.GetMemberMeazurements(id);
            Model._MemberWeightArray = memberManager.GetMemberWeightsArray(id);
            Model._MemberDiet = dietManager.GetDiets(Model._Member.Diet_ID, true)[0];
            Model._MemberExcersize = excersizeManager.GetExcersizes(Model._Member.Excersize_ID, true)[0];
            return View(Model);
        }
    }
}
