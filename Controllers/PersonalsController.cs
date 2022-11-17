using BussinesLayer;
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
            PersonalsViewModel personalsViewModel = new PersonalsViewModel();
            personalsViewModel.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            personalsViewModel.Users = new UserManager().GetUsers();
            personalsViewModel.Tasks = new TaskManager().GetLastFiveTask();
            personalsViewModel.TodayTasks = new TaskManager().GetLastFiveTask(1);
            if (id != 0)
            {
                personalsViewModel.SelectedUser = new UserManager().GetUser(id);
                personalsViewModel.UsersTasks = new UserManager().GetUserTasks(id);
            }
            return personalsViewModel;
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
        public async Task<IActionResult> RegistryingAsync(PersonalsViewModel personalsViewModel)
        {
            if (personalsViewModel.file != null)
            {
                string imageExtension = Path.GetExtension(personalsViewModel.file.FileName);
                string imageName = Guid.NewGuid() + imageExtension;
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/Personal/{imageName}");
                using var stream = new FileStream(path, FileMode.Create);
                await personalsViewModel.file.CopyToAsync(stream);
                personalsViewModel.SelectedUser.IMG = $"/Personal/{imageName}";
            }
            else if (personalsViewModel.SelectedUser.IMG == null) { personalsViewModel.SelectedUser.IMG = $"/Personal/AvatarNull.png"; }

            new UserManager().SaveUser(personalsViewModel.SelectedUser);
            TaskBuilder(4, 0);
            personalsViewModel.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            return RedirectToAction("Index", "Home");
        }
    }
}
