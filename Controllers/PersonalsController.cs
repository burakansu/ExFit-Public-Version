using BussinesLayer;
using ExFit.Data;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ExFit.Controllers
{
    public class PersonalsController : MemberControllerBase
    {
        public PersonalsViewModel ViewModel(int id = 0)
        {
            PersonalsViewModel VM = new PersonalsViewModel();
            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            VM.Company = new CompanyManager().GetCompany(VM.User.Company_ID);

            VM.Users = new UserManager().GetUsers(VM.Company.Company_ID);
            VM.Tasks = new TaskManager().GetLastFiveTask(VM.Company.Company_ID);
            VM.TodayTasks = new TaskManager().GetLastFiveTask(VM.Company.Company_ID, 1);
            if (id != 0)
            {
                VM.SelectedUser = new UserManager().GetUser(id);
                VM.UsersTasks = new UserManager().GetUserTasks(id);
            }
            return VM;
        }
        public IActionResult Index()
        {
            return View(ViewModel());
        }
        public IActionResult UserSettings(int id)
        {
            return View(ViewModel(id));
        }
        public IActionResult AllActivitiesToday()
        {
            return View(ViewModel());
        }
        public IActionResult AddPersonal()
        {
            return View(ViewModel());
        }
        public IActionResult GetUser(int id)
        {
            return PartialView("Partial/_PersonalsDetails", ViewModel(id));
        }
        public IActionResult Delete(int id)
        {
            new UserManager().DeleteUser(id);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> RegistryingAsync(PersonalsViewModel VM)
        {
            if (VM.file != null)
            {
                string imageExtension = Path.GetExtension(VM.file.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Personal/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await VM.file.CopyToAsync(stream);
                VM.SelectedUser.IMG = $"/Personal/{imageName}";
            }
            else if (VM.SelectedUser.IMG == null) { VM.SelectedUser.IMG = $"/Personal/AvatarNull.png"; }

            VM.SelectedUser.Company_ID = VM.Company.Company_ID;
            new UserManager().SaveUser(VM.SelectedUser);
            new TaskManager().SaveTask(new TaskManager().TaskBuilder(VM.SelectedUser.Company_ID, 4, 0, (int)HttpContext.Session.GetInt32("ID")));

            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            return RedirectToAction("Index", "Home");
        }
    }
}
