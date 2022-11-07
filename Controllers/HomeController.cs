using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExFit.Controllers
{
    public class HomeController : MemberControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        MemberManager memberManager = new MemberManager();
        TaskManager taskManager = new TaskManager();
        UserManager userManager = new UserManager();
        CostManager costManager = new CostManager();
        IncomeManager incomeManager = new IncomeManager();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public HomeViewModel ViewModel()
        {
            HomeViewModel Model = new HomeViewModel();
            Model.ThisYearRegistrys = memberManager.GetThisYearRegystry();
            Model.Members = memberManager.GetMembers(0, 0);
            Model.LastMembers = memberManager.GetMembers(1, 0);            
            Model.Users = userManager.GetUsers();
            Model.Income = memberManager.GetIncome();
            Model.Costs = costManager.GetCosts();
            Model.Incomes = incomeManager.GetIncomes();
            Model.Tasks = taskManager.GetLastFiveTask();
            Model.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
            Model.TodayTaskCount = taskManager.CountTasks();
            int[] incomes = new int[Model.Incomes.Count];
            int[] costs = new int[Model.Costs.Count];
            int i = 0,j = 0;
            foreach (var item in Model.Incomes)
            {
                incomes[i] = item.Value;
                i++;
            }
            foreach (var item in Model.Costs)
            {
                costs[j] = item.Which_Cost;
                j++;
            }
            Model.IncomeArray = incomes;
            Model.CostArray = costs;
            return Model;
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
        public IActionResult _Growth()
        {
            return PartialView("Partial/_Growth", ViewModel());
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
