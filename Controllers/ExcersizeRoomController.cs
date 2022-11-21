using BussinesLayer;
using ExFit.Data;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectLayer;

namespace ExFit.Controllers
{
    public class ExcersizeRoomController : MemberControllerBase
    {
        private Context context;
        public ExcersizeRoomController(Context _context)
        {
            context = _context;
        }
        public ExcersizeRoomViewModel ViewModel(int id = 0)
        {
            ExcersizeRoomViewModel VM = new ExcersizeRoomViewModel();
            VM.Excersizes = new ExcersizeManager(context).GetExcersizes();
            VM.Tasks = new TaskManager(context).GetLastFiveTask();
            VM.User = new UserManager(context).GetUser((int)HttpContext.Session.GetInt32("ID"));
            VM._Day = 0;
            if (id != 0) 
            { 
                VM.Excersize = new ExcersizeManager(context).GetExcersizes(id, true)[0];
                VM.Practices = new PracticeManager(context).GetPractices();
                VM.C1 = new ExcersizeManager(context).Counts(1, id);
                VM.C2 = new ExcersizeManager(context).Counts(2, id);
                VM.C3 = new ExcersizeManager(context).Counts(3, id);
                VM.C4 = new ExcersizeManager(context).Counts(4, id);
                VM.C5 = new ExcersizeManager(context).Counts(5, id);
                VM.C6 = new ExcersizeManager(context).Counts(6, id);
                VM.C7 = new ExcersizeManager(context).Counts(7, id);
            }
            else { VM.Excersize = new ObjExcersize(context); }

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
            new PracticeManager(context).AddDatabasePractice(Model.Practice);
            return RedirectToAction("EditExcersize", "ExcersizeRoom", new { id = Model.Practice.Excersize_ID });
        }
        public IActionResult DeletePractice(int id = 0,int Excersize_ID = 0)
        {
            new PracticeManager(context).DeletePractice(id);
            return RedirectToAction("EditExcersize", "ExcersizeRoom", new { id = Excersize_ID });
        }
        public IActionResult DeleteExcersize(int id)
        {
            new ExcersizeManager(context).DeleteExcersize(id);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult SaveDatabaseExcersize(ExcersizeRoomViewModel Model)
        {
            new ExcersizeManager(context).AddDatabaseExcersize(Model.Excersize);
            new TaskManager(context).SaveTask(new TaskManager(context).TaskBuilder(5, 0,Model.User.User_ID));
            return RedirectToAction("Index", "Home");
        }
    }
}
