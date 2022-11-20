using BussinesLayer;
using ExFit.Areas.Member.Models;
using ExFit.Controllers;
using ExFit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]
    public class MyExcersize : _MemberControllerBase
    {
        private Context context;
        public MyExcersize(Context _context)
        {
            context = _context;
        }
        public _MyExcersizeViewModel ViewModel()
        {
            _MyExcersizeViewModel VM = new _MyExcersizeViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager(context).GetMember(id);
            VM._MemberWeightArray = new MemberManager(context).GetMemberWeightsArray(id);
            VM._MemberMeazurements = new MemberManager(context).GetMemberMeazurements(id);
            VM._MemberExcersize = new ExcersizeManager(context).GetExcersizes(VM._Member.Excersize_ID, true)[0];
            VM.Practices = new PracticeManager(context).GetPractices();
            VM._ExcersizeArray = new ExcersizeManager(context).GetExcersizes();
            VM.C1 = new ExcersizeManager(context).Counts(1, VM._Member.Excersize_ID);
            VM.C2 = new ExcersizeManager(context).Counts(2, VM._Member.Excersize_ID);
            VM.C3 = new ExcersizeManager(context).Counts(3, VM._Member.Excersize_ID);
            VM.C4 = new ExcersizeManager(context).Counts(4, VM._Member.Excersize_ID);
            VM.C5 = new ExcersizeManager(context).Counts(5, VM._Member.Excersize_ID);
            VM.C6 = new ExcersizeManager(context).Counts(6, VM._Member.Excersize_ID);
            VM.C7 = new ExcersizeManager(context).Counts(7, VM._Member.Excersize_ID);
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
