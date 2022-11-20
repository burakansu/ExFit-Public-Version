using BussinesLayer;
using ExFit.Areas.Member.Models;
using ExFit.Controllers;
using ExFit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]

    public class Contact : _MemberControllerBase
    {
        private Context context;
        public Contact(Context _context)
        {
            context = _context;
        }
        public _ContactViewModel ViewModel()
        {
            _ContactViewModel VM = new _ContactViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager(context).GetMember(id);
            VM._MemberWeightArray = new MemberManager(context).GetMemberWeightsArray(id);
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
