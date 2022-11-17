using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectLayer;

namespace ExFit.Controllers
{
    public class ExcersizeRoomController : MemberControllerBase
    {
        public ExcersizeRoomViewModel ViewModel(int id = 0)
        {
            ExcersizeRoomViewModel VM = new ExcersizeRoomViewModel();
            VM.Excersizes = new ExcersizeManager().GetExcersizes();
            VM.Tasks = new TaskManager().GetLastFiveTask();
            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            VM._Day = 0;
            if (id != 0) 
            { 
                VM.Excersize = new ExcersizeManager().GetExcersizes(id, true)[0];
                VM.Practices = new PracticeManager().GetPractices();
                VM.C1 = new ExcersizeManager().Counts(1, id);
                VM.C2 = new ExcersizeManager().Counts(2, id);
                VM.C3 = new ExcersizeManager().Counts(3, id);
                VM.C4 = new ExcersizeManager().Counts(4, id);
                VM.C5 = new ExcersizeManager().Counts(5, id);
                VM.C6 = new ExcersizeManager().Counts(6, id);
                VM.C7 = new ExcersizeManager().Counts(7, id);
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
            new PracticeManager().AddDatabasePractice(Model.Practice);
            return RedirectToAction("EditExcersize", "ExcersizeRoom", new { id = Model.Practice.Excersize_ID });
        }
        public IActionResult DeletePractice(int id = 0,int Excersize_ID = 0)
        {
            new PracticeManager().DeletePractice(id);
            return RedirectToAction("EditExcersize", "ExcersizeRoom", new { id = Excersize_ID });
        }
        public IActionResult DeleteExcersize(int id)
        {
            new ExcersizeManager().DeleteExcersize(id);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SaveDatabaseExcersize(ExcersizeRoomViewModel Model)
        {
            new ExcersizeManager().AddDatabaseExcersize(Model.Excersize);
            TaskBuilder(5, 0);
            return RedirectToAction("Index", "Home");
        }
    }
}
