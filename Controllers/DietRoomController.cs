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
        private Context context;
        public DietRoomController(Context _context)
        {
            context = _context;
        }
        public DietRoomViewModel ViewModel(int id = 0)
        {
            DietRoomViewModel VM = new DietRoomViewModel();
            VM.Diets = new DietManager(context).GetDiets();
            VM.Tasks = new TaskManager(context).GetLastFiveTask();
            VM.User = new UserManager(context).GetUser((int)HttpContext.Session.GetInt32("ID"));
            if (id != 0)
            {
                VM.Diet = new DietManager(context).GetDiets(id, true)[0];
                VM.Foods = new FoodManager(context).GetFoods();
                VM.C1 = new DietManager(context).Counts(1, id);
                VM.C2 = new DietManager(context).Counts(2, id);
                VM.C3 = new DietManager(context).Counts(3, id);
                VM.C4 = new DietManager(context).Counts(4, id);
                VM.C5 = new DietManager(context).Counts(5, id);
                VM.C6 = new DietManager(context).Counts(6, id);
                VM.C7 = new DietManager(context).Counts(7, id);
            }
            else { VM.Diet = new ObjDiet(context); }

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
            new FoodManager(context).AddDatabaseFood(Model.Food);
            return RedirectToAction("EditDiet", "DietRoom", new { id = Model.Food.Diet_ID });
        }
        public IActionResult DeleteFood(int id = 0, int Diet_ID = 0)
        {
            new FoodManager(context).DeleteFood(id);
            return RedirectToAction("EditDiet", "DietRoom", new { id = Diet_ID });
        }
        public IActionResult DeleteDiet(int id)
        {
            new DietManager(context).DeleteDiet(id);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SaveDatabaseDiet(DietRoomViewModel viewModelDietRoom)
        {
            new DietManager(context).AddDatabaseDiet(viewModelDietRoom.Diet);
            new TaskManager(context).SaveTask(new TaskManager(context).TaskBuilder(6, 0, (int)HttpContext.Session.GetInt32("ID")));
            return RedirectToAction("Index", "Home");
        }
    }
}
