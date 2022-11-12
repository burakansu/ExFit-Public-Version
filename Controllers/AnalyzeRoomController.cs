using BussinesLayer;
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
            AnalyzeRoomViewModel Model = new AnalyzeRoomViewModel();
            Model.Income = new MemberManager().GetIncome();
            Model.Costs = new CostManager().GetCosts();
            Model.Incomes = new IncomeManager().GetIncomes();
            Model.Tasks = new TaskManager().GetLastFiveTask();
            Model.User = new UserManager().GetUser((int)HttpContext.Session.GetInt32("ID"));
            int[] incomes = new int[Model.Incomes.Count];
            int[] costs = new int[Model.Costs.Count];
            int i = 0, j = 0;
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
        public IActionResult Economy()
        {
            return View(ViewModel());
        }
        public IActionResult SaveCost(ObjCost objCost)
        {
            new CostManager().AddDatabaseCost(objCost);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteCost(int id)
        {
            new CostManager().DeleteCost(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
