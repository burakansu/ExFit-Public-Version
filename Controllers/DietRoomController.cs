using BussinesLayer;
using ExFit.Data;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectLayer;

namespace ExFit.Controllers
{
    public class DietRoomController : MemberControllerBase
    {
        public DietRoomViewModel ViewModel(int id = 0)
        {
            DietRoomViewModel VM = new DietRoomViewModel();
            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            VM.Company = new CompanyManager().GetCompany(VM.User.Company_ID);
            VM.Diets = new DietManager().GetDiets(VM.Company.Company_ID);
            VM.Tasks = new TaskManager().GetLastFiveTask(VM.Company.Company_ID);

            if (id != 0)
            {
                VM.Diet = new DietManager().GetDiets(VM.Company.Company_ID,id, true)[0];
                VM.Foods = new FoodManager().GetFoods();
                VM.C1 = new DietManager().Counts(1, id);
                VM.C2 = new DietManager().Counts(2, id);
                VM.C3 = new DietManager().Counts(3, id);
                VM.C4 = new DietManager().Counts(4, id);
                VM.C5 = new DietManager().Counts(5, id);
                VM.C6 = new DietManager().Counts(6, id);
                VM.C7 = new DietManager().Counts(7, id);
            }
            else { VM.Diet = new ObjDiet(); }

            return VM;
        }
        public IActionResult DietRoom()
        {
            return View(ViewModel());
        }
        public IActionResult EditDiet(int id = 0)
        {
            return View(ViewModel(id));
        }
        public IActionResult FoodRoom(int id = 0)
        {
            return View(ViewModel(id));
        }
        public IActionResult SaveFood(DietRoomViewModel VM)
        {
            VM.Food.Diet_ID = VM.Diet.Diet_ID;
            VM.Food.Day = VM._Day;
            if (VM.Food.Note == null)
                VM.Food.Note = ".";
            new FoodManager().AddDatabaseFood(VM.Food);
            return RedirectToAction("EditDiet", "DietRoom", new { id = VM.Food.Diet_ID });
        }
        public IActionResult DeleteFood(int id = 0, int Diet_ID = 0)
        {
            new FoodManager().DeleteFood(id);
            return RedirectToAction("EditDiet", "DietRoom", new { id = Diet_ID });
        }
        public IActionResult DeleteDiet(int id)
        {
            new DietManager().DeleteDiet(id);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SaveDatabaseDiet(DietRoomViewModel VM)
        {
            new DietManager().AddDatabaseDiet(VM.Diet);
            new TaskManager().SaveTask(new TaskManager().TaskBuilder(VM.Diet.Company_ID,6, 0, VM.User.User_ID));
            return RedirectToAction("Index", "Home");
        }
    }
}
