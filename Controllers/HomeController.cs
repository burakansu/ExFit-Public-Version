using BussinesLayer;
using DatabaseLayer;
using ExFit.Data;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExFit.Controllers
{
    public class HomeController : MemberControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private Context context;
        public HomeController(ILogger<HomeController> logger, Context _context)
        {
            _logger = logger;
            context = _context;
        }
        public HomeViewModel ViewModel()
        {
            HomeViewModel Model = new HomeViewModel();
            Model.ThisYearRegistrys = new MemberManager(context).GetThisYearRegystry();
            Model.Members = new MemberManager(context).GetMembers(0, 0);
            Model.LastMembers = new MemberManager(context).GetMembers(1, 0);            
            Model.Users = new UserManager(context).GetUsers();
            Model.Income = new MemberManager(context).GetIncome();
            Model.Costs = new CostManager(context).GetCosts();
            Model.TotalCost = new CostManager(context).TotalCost();
            Model.Profit = Model.Income - Model.TotalCost;
            Model.Incomes = new IncomeManager(context).GetIncomes();
            Model.Tasks = new TaskManager(context).GetLastFiveTask();
            Model.User = new UserManager(context).GetUser((int)HttpContext.Session.GetInt32("ID"));
            Model.TodayTaskCount = new TaskManager(context).CountTasks();
            
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
            new UserManager(context).Authorization((int)HttpContext.Session.GetInt32("ID"));                       
            return View(ViewModel());
        }
        public IActionResult Delete_Task(int id)
        {
            new TaskManager(context).DeleteTask(id);
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
