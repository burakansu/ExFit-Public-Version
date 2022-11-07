using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public DietRoomViewModel ViewModel()
        {
            DietRoomViewModel viewModelDietRoom = new DietRoomViewModel();
            viewModelDietRoom.Diets = dietManager.GetDiets();
            viewModelDietRoom.Tasks = taskManager.GetLastFiveTask();
            viewModelDietRoom.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            return viewModelDietRoom;
        }
        public IActionResult DietRoom()
        {                       
            return View(ViewModel());
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
