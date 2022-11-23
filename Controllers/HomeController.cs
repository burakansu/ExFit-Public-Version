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
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public HomeViewModel ViewModel()
        {
            HomeViewModel VM = new HomeViewModel();
            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            VM.Company = new CompanyManager().GetCompany(VM.User.Company_ID);
            VM.ThisYearRegistrys = new MemberManager().GetThisYearRegystry(VM.Company.Company_ID);
            VM.Members = new MemberManager().GetMembers(VM.Company.Company_ID, 0, 0);
            VM.LastMembers = new MemberManager().GetMembers(VM.Company.Company_ID, 1, 0);            
            VM.Users = new UserManager().GetUsers(VM.Company.Company_ID);
            VM.Income = new MemberManager().GetIncome(VM.Company.Company_ID);
            VM.Costs = new CostManager().GetCosts(VM.Company.Company_ID);
            VM.TotalCost = new CostManager().TotalCost(VM.Company.Company_ID);
            VM.Profit = VM.Income - VM.TotalCost;
            VM.Incomes = new IncomeManager().GetIncomes(VM.Company.Company_ID);
            VM.Tasks = new TaskManager().GetLastFiveTask(VM.Company.Company_ID);
            VM.TodayTaskCount = new TaskManager().CountTasks(VM.Company.Company_ID);

            int[] incomes = new int[VM.Incomes.Count];
            int[] costs = new int[VM.Costs.Count];
            int i = 0,j = 0;
            foreach (var item in VM.Incomes)
            {
                incomes[i] = item.Value;
                i++;
            }
            foreach (var item in VM.Costs)
            {
                costs[j] = item.Which_Cost;
                j++;
            }
            VM.IncomeArray = incomes;
            VM.CostArray = costs;
            return VM;
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
