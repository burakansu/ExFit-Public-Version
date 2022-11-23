using BussinesLayer;
using ExFit.Areas.Member.Models;
using ExFit.Controllers;
using ExFit.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Areas.Member.Controllers
{
    [Area("Member")]
    public class MyDiet : _MemberControllerBase
    {
        public _MyDietViewModel ViewModel()
        {
            _MyDietViewModel VM = new _MyDietViewModel();
            int id = (int)HttpContext.Session.GetInt32("Member_ID");
            VM._Member = new MemberManager().GetMember(id);
            VM.Company = new CompanyManager().GetCompany(VM._Member.Company_ID);
            VM._MemberWeightArray = new MemberManager().GetMemberWeightsArray(id);
            VM._MemberDiet = new DietManager().GetDiets(VM._Member.Company_ID, VM._Member.Diet_ID, true)[0];
            VM.Foods = new FoodManager().GetFoods();
            VM._DietArray = new DietManager().GetDiets(VM._Member.Company_ID);
            VM.D1 = new DietManager().Counts(1, VM._Member.Diet_ID);
            VM.D2 = new DietManager().Counts(2, VM._Member.Diet_ID);
            VM.D3 = new DietManager().Counts(3, VM._Member.Diet_ID);
            VM.D4 = new DietManager().Counts(4, VM._Member.Diet_ID);
            VM.D5 = new DietManager().Counts(5, VM._Member.Diet_ID);
            VM.D6 = new DietManager().Counts(6, VM._Member.Diet_ID);
            VM.D7 = new DietManager().Counts(7, VM._Member.Diet_ID);
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
    }
}
