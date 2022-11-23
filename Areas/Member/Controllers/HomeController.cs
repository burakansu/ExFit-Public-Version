using BussinesLayer;
using ExFit.Areas.Member.Models;
using ExFit.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ObjectLayer;

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
        public _HomeViewModel ViewModel()
        {
            _HomeViewModel VM = new _HomeViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager().GetMember(id);
            VM.Company = new CompanyManager().GetCompany(VM._Member.Company_ID);
            VM._MemberWeightArray = new MemberManager().GetMemberWeightsArray(id);
            if (VM._Member.Diet_ID != 0)
            {
                VM._MemberDiet = new DietManager().GetDiets(VM.Company.Company_ID, VM._Member.Diet_ID, true)[0];
            }
            else { VM._MemberDiet = new ObjDiet(); }
            if (VM._Member.Excersize_ID != 0)
            {
                VM._MemberExcersize = new ExcersizeManager().GetExcersizes(VM.Company.Company_ID, VM._Member.Excersize_ID, true)[0];
            }
            else { VM._MemberExcersize = new ObjExcersize(); }
            
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
