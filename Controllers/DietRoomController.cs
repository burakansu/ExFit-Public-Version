using BussinesLayer;
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
            VM.Diets = new DietManager().GetDiets();
            VM.Tasks = new TaskManager().GetLastFiveTask();
            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            if (id != 0)
            {
                VM.Diet = new DietManager().GetDiets(id, true)[0];
                VM.Foods = new FoodManager().GetFoods();
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
        public IActionResult SaveFood(DietRoomViewModel Model)
        {
            Model.Food.Diet_ID = Model.Diet.Diet_ID;
            Model.Food.Day = Model._Day;
            new FoodManager().AddDatabaseFood(Model.Food);
            return RedirectToAction("EditDiet", "DietRoom", new { id = Model.Food.Diet_ID });
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
        public IActionResult SaveDatabaseDiet(DietRoomViewModel viewModelDietRoom)
        {
           new DietManager().AddDatabaseDiet(viewModelDietRoom.Diet);
            TaskBuilder(6, 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
