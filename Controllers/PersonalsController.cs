using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExFit.Controllers
{
    public class PersonalsController : ExFitControllerBase
    {
        UserManager userManager = new UserManager();
        TaskManager taskManager = new TaskManager();

        public PersonalsViewModel ViewModel(int id = 0)
        {
            PersonalsViewModel personalsViewModel = new PersonalsViewModel();
            personalsViewModel.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            personalsViewModel.Users = userManager.GetUsers();
            personalsViewModel.Tasks = taskManager.GetLastFiveTask();
            personalsViewModel.TodayTasks = taskManager.GetLastFiveTask(1);
            if (id != 0)
            {
                personalsViewModel.SelectedUser = userManager.GetUser(id);
                personalsViewModel.UsersTasks = userManager.GetUserTasks(id);
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
            userManager.DeleteUser(id);
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

            userManager.SaveUser(personalsViewModel.SelectedUser);
            TaskBuilder(4, 0);
            personalsViewModel.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            return RedirectToAction("Index", "Home");
        }
    }
}
