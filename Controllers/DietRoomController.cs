using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectLayer;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExFit.Controllers
{
    public class DietRoomController : MemberControllerBase
    {
        DietManager dietManager = new DietManager();
        TaskManager taskManager = new TaskManager();
        UserManager userManager = new UserManager();
        FoodManager foodManager = new FoodManager();

        public DietRoomViewModel ViewModel(int id = 0)
        {
            DietRoomViewModel VM = new DietRoomViewModel();
            VM.Diets = dietManager.GetDiets();
            VM.Tasks = taskManager.GetLastFiveTask();
            VM.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            if (id != 0)
            {
                VM.Diet = dietManager.GetDiets(id, true)[0];
                VM.Foods = foodManager.GetFoods();
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
            foodManager.AddDatabaseFood(Model.Food);
            return RedirectToAction("EditDiet", "DietRoom", new { id = Model.Food.Diet_ID });
        }
        public IActionResult DeleteFood(int id = 0, int Diet_ID = 0)
        {
            foodManager.DeleteFood(id);
            return RedirectToAction("EditDiet", "DietRoom", new { id = Diet_ID });
        }
        public IActionResult DeleteDiet(int id)
        {
            dietManager.DeleteDiet(id);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SaveDatabaseDietAsync(DietRoomViewModel viewModelDietRoom)
        {
            if (viewModelDietRoom.Diet.FileDietIMG != null)
            {
                string imageExtension = Path.GetExtension(viewModelDietRoom.Diet.FileDietIMG.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Diet/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await viewModelDietRoom.Diet.FileDietIMG.CopyToAsync(stream);
                viewModelDietRoom.Diet.IMG = $"/Diet/{imageName}";
            }
            else if (viewModelDietRoom.Diet.IMG == null) { viewModelDietRoom.Diet.IMG = "/Member/ProfilePhotos/AvatarNull.png"; }
            dietManager.AddDatabaseDiet(viewModelDietRoom.Diet);
            TaskBuilder(6, 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
