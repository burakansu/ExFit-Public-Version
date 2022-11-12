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
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public HomeViewModel ViewModel()
        {
            HomeViewModel Model = new HomeViewModel();
            Model.ThisYearRegistrys = new MemberManager().GetThisYearRegystry();
            Model.Members = new MemberManager().GetMembers(0, 0);
            Model.LastMembers = new MemberManager().GetMembers(1, 0);            
            Model.Users = new UserManager().GetUsers();
            Model.Income = new MemberManager().GetIncome();
            Model.Costs = new CostManager().GetCosts();
            Model.Incomes = new IncomeManager().GetIncomes();
            Model.Tasks = new TaskManager().GetLastFiveTask();
            Model.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            Model.TodayTaskCount = new TaskManager().CountTasks();
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
            new UserManager().Authorization((int)HttpContext.Session.GetInt32("ID"));                       
            return View(ViewModel());
        }
        public IActionResult Delete_Task(int id)
        {
            new TaskManager().DeleteTask(id);
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
