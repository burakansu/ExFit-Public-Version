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
        public _MyExcersizeViewModel ViewModel()
        {
            _MyExcersizeViewModel VM = new _MyExcersizeViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager().GetMember(id);
            VM._MemberWeightArray = new MemberManager().GetMemberWeightsArray(id);
            VM._MemberMeazurements = new MemberManager().GetMemberMeazurements(id);
            VM._MemberExcersize = new ExcersizeManager().GetExcersizes(VM._Member.Company_ID, VM._Member.Excersize_ID, true)[0];
            VM.Practices = new PracticeManager().GetPractices();
            VM._ExcersizeArray = new ExcersizeManager().GetExcersizes(0);
            VM.C1 = new ExcersizeManager().Counts(1, VM._Member.Excersize_ID);
            VM.C2 = new ExcersizeManager().Counts(2, VM._Member.Excersize_ID);
            VM.C3 = new ExcersizeManager().Counts(3, VM._Member.Excersize_ID);
            VM.C4 = new ExcersizeManager().Counts(4, VM._Member.Excersize_ID);
            VM.C5 = new ExcersizeManager().Counts(5, VM._Member.Excersize_ID);
            VM.C6 = new ExcersizeManager().Counts(6, VM._Member.Excersize_ID);
            VM.C7 = new ExcersizeManager().Counts(7, VM._Member.Excersize_ID);
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
