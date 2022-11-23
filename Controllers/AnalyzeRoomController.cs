using BussinesLayer;
using ExFit.Data;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectLayer;

namespace ExFit.Controllers
{
    public class AnalyzeRoomController : MemberControllerBase
    {
        public AnalyzeRoomViewModel ViewModel()
        {
            AnalyzeRoomViewModel VM = new AnalyzeRoomViewModel();
            VM.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            VM.Company = new CompanyManager().GetCompany(VM.User.Company_ID);
            VM.Income = new MemberManager().GetIncome(VM.Company.Company_ID);
            VM.Costs = new CostManager().GetCosts(VM.Company.Company_ID);
            VM.TotalCost = new CostManager().TotalCost(VM.Company.Company_ID);
            VM.Profit = VM.Income - VM.TotalCost;
            VM.Incomes = new IncomeManager().GetIncomes(VM.Company.Company_ID);
            VM.Tasks = new TaskManager().GetLastFiveTask(VM.Company.Company_ID);
            

            int[] incomes = new int[VM.Incomes.Count];
            int[] costs = new int[VM.Costs.Count];
            int i = 0, j = 0;
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
        public IActionResult Economy()
        {
            return View(ViewModel());
        }
        public IActionResult SaveCost(AnalyzeRoomViewModel VM)
        {
            VM.Cost.Company_ID= VM.User.Company_ID;
            new CostManager().AddDatabaseCost(VM.Cost);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteCost(int id)
        {
            new CostManager().DeleteCost(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
