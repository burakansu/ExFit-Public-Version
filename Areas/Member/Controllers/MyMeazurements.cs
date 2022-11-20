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
        private Context context;
        public MyMeazurements(Context _context)
        {
            context = _context;
        }
        public _MyMeazurementsViewModel ViewModel()
        {
            _MyMeazurementsViewModel VM = new _MyMeazurementsViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager(context).GetMember(id);
            VM._MemberMeazurements = new MemberManager(context).GetMemberMeazurements(id);
            VM._MemberWeightArray = new MemberManager(context).GetMemberWeightsArray(id);
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
