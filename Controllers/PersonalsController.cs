using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectLayer;
using System;
using System.Collections.Generic;
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
            ObjUser User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            new TaskManager().SaveTask(new TaskManager().TaskBuilder(User.Company_ID, 11, 0, User.User_ID));
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> RegistryingAsync(PersonalsViewModel VM)
        {
            ObjUser User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
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
            int Type = new UserManager().SaveUser(VM.SelectedUser);
            if (Type == 0)
            {
                new TaskManager().SaveTask(new TaskManager().TaskBuilder(VM.SelectedUser.Company_ID, 4, 0, User.User_ID));
                List<ObjUser> objUsers = new List<ObjUser>();
                objUsers.Add(VM.SelectedUser);
                if (VM.Company.Package_Type > 0)
                    new SmsManager().SmsSender(VM.Company.Name, "Hoşgeldin! " + VM.SelectedUser.Name + " " + DateTime.Now.ToShortDateString() + " itibarıyla ExFit Yönetim Paneline Girerek Çalışmaya Başlayabilirsin Mail:" + VM.SelectedUser.Mail + "Şifre: " + VM.SelectedUser.Password, null, objUsers);
            }
            else if (Type == 1)
                new TaskManager().SaveTask(new TaskManager().TaskBuilder(VM.SelectedUser.Company_ID, 9, 0, User.User_ID));
            else if (VM.SelectedUser.User_ID != User.User_ID)
                new TaskManager().SaveTask(new TaskManager().TaskBuilder(VM.SelectedUser.Company_ID, 10, 0, User.User_ID));

            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            return RedirectToAction("Index", "Home");
        }
    }
}
