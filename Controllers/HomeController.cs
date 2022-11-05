using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExFit.Controllers
{
    public class HomeController : ExFitControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        MemberManager memberManager = new MemberManager();
        TaskManager taskManager = new TaskManager();
        UserManager userManager = new UserManager();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public HomeViewModel ViewModel()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.ThisYearRegistrys = memberManager.GetThisYearRegystry();
            homeViewModel.Members = memberManager.GetMembers(0, 0);
            homeViewModel.LastMembers = memberManager.GetMembers(1, 0);            
            homeViewModel.Users = userManager.GetUsers();
            homeViewModel.Income = memberManager.GetIncome();
            homeViewModel.Tasks = taskManager.GetLastFiveTask();
            homeViewModel.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            homeViewModel.TodayTaskCount = taskManager.CountTasks();
            return homeViewModel;
        }
        public IActionResult Index()
        {          
            userManager.Authorization((int)HttpContext.Session.GetInt32("ID"));                       
            return View(ViewModel());
        }
        public IActionResult Delete_Task(int id)
        {
            taskManager.DeleteTask(id);
            return RedirectToAction("Index","Home", ViewModel());
        }
        public IActionResult _Activities(int id)
        {
            return View(ViewModel());
        }
        public IActionResult _LastTasks()
        {
            return PartialView("Partial/_LastTasks", ViewModel());
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
