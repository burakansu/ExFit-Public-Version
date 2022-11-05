using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExFit.Controllers
{
    public class ExcersizeRoomController : ExFitControllerBase
    {
        Excersize_Manager excersize_Manager = new Excersize_Manager();
        TaskManager taskManager = new TaskManager();
        UserManager userManager = new UserManager();
        public ExcersizeRoomViewModel ViewModel()
        {
            ExcersizeRoomViewModel viewModelExcersizeRoom = new ExcersizeRoomViewModel();
            viewModelExcersizeRoom.Excersizes = excersize_Manager.GetExcersizes();
            viewModelExcersizeRoom.Tasks = taskManager.GetLastFiveTask();
            viewModelExcersizeRoom.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            return viewModelExcersizeRoom;
        }
        public IActionResult ExcersizeRoom()
        {        
            return View(ViewModel());
        }
        public IActionResult DeleteExcersize(int id)
        {
            excersize_Manager.DeleteExcersize(id);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SaveDatabaseExcersizeAsync(ExcersizeRoomViewModel viewModelExcersizeRoom)
        {
            if (viewModelExcersizeRoom.Excersize.FileExcersizeIMG != null)
            {
                string imageExtension = Path.GetExtension(viewModelExcersizeRoom.Excersize.FileExcersizeIMG.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Excersize/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await viewModelExcersizeRoom.Excersize.FileExcersizeIMG.CopyToAsync(stream);
                viewModelExcersizeRoom.Excersize.IMG = $"/Excersize/{imageName}";
            }
            else if (viewModelExcersizeRoom.Excersize.IMG == null) { viewModelExcersizeRoom.Excersize.IMG = "/Member/ProfilePhotos/AvatarNull.png"; }
            excersize_Manager.AddDatabaseExcersize(viewModelExcersizeRoom.Excersize);
            TaskBuilder(5, 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
