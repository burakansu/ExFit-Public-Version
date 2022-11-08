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
    public class ExcersizeRoomController : MemberControllerBase
    {
        ExcersizeManager excersize_Manager = new ExcersizeManager();
        TaskManager taskManager = new TaskManager();
        UserManager userManager = new UserManager();
        PracticeManager practiceManager = new PracticeManager();
        public ExcersizeRoomViewModel ViewModel(int id = 0)
        {
            ExcersizeRoomViewModel VM = new ExcersizeRoomViewModel();
            VM.Excersizes = excersize_Manager.GetExcersizes();
            VM.Tasks = taskManager.GetLastFiveTask();
            VM.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            VM._Day = 0;
            if (id != 0) 
            { 
                VM.Excersize = excersize_Manager.GetExcersizes(id, true)[0];
                VM.Practices = practiceManager.GetPractices();
            }
            else { VM.Excersize = new ObjExcersize(); }

            return VM;
        }
        public IActionResult ExcersizeRoom()
        {
            return View(ViewModel());
        }
        public IActionResult EditExcersize(int id = 0)
        {
            return View(ViewModel(id));
        }
        public IActionResult ParticleRoom(int id = 0)
        {
            return View(ViewModel(id));
        }
        public IActionResult SavePractice(ExcersizeRoomViewModel Model)
        {
            Model.Practice.Excersize_ID = Model.Excersize.Excersize_ID;
            Model.Practice.Day = Model._Day;
            practiceManager.AddDatabasePractice(Model.Practice);
            return RedirectToAction("EditExcersize", "ExcersizeRoom", new { id = Model.Practice.Excersize_ID });
        }
        public IActionResult DeletePractice(int id = 0,int Excersize_ID = 0)
        {
            practiceManager.DeletePractice(id);
            return RedirectToAction("EditExcersize", "ExcersizeRoom", new { id = Excersize_ID });
        }
        public IActionResult DeleteExcersize(int id)
        {
            excersize_Manager.DeleteExcersize(id);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SaveDatabaseExcersizeAsync(ExcersizeRoomViewModel Model)
        {
            if (Model.Excersize.FileExcersizeIMG != null)
            {
                string imageExtension = Path.GetExtension(Model.Excersize.FileExcersizeIMG.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Excersize/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await Model.Excersize.FileExcersizeIMG.CopyToAsync(stream);
                Model.Excersize.IMG = $"/Excersize/{imageName}";
            }
            else if (Model.Excersize.IMG == null) { Model.Excersize.IMG = "/Member/ProfilePhotos/AvatarNull.png"; }
            excersize_Manager.AddDatabaseExcersize(Model.Excersize);
            TaskBuilder(5, 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
