using BussinesLayer;
using ExFit.Areas.Member.Models;
using ExFit.Controllers;
using ExFit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]
    public class MyMeazurements : _MemberControllerBase
    {
        public _MyMeazurementsViewModel ViewModel()
        {
            _MyMeazurementsViewModel VM = new _MyMeazurementsViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager().GetMember(id);
            VM.Company = new CompanyManager().GetCompany(VM._Member.Company_ID);
            VM._MemberMeazurements = new MemberManager().GetMemberMeazurements(id);
            VM._MemberWeightArray = new MemberManager().GetMemberWeightsArray(id);
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
