using BussinesLayer;
using ExFit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ObjectLayer;

namespace ExFit.Controllers
{
    public class AnalyzeRoomController : MemberControllerBase
    {
        MemberManager memberManager = new MemberManager();
        TaskManager taskManager = new TaskManager();
        UserManager userManager = new UserManager();
        CostManager costManager = new CostManager();
        IncomeManager incomeManager = new IncomeManager();
        public AnalyzeRoomViewModel ViewModel()
        {
            AnalyzeRoomViewModel Model = new AnalyzeRoomViewModel();
            Model.Income = memberManager.GetIncome();
            Model.Costs = costManager.GetCosts();
            Model.Incomes = incomeManager.GetIncomes();
            Model.Tasks = taskManager.GetLastFiveTask();
            Model.User = userManager.GetUser((int)HttpContext.Session.GetInt32("ID"));
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
            costManager.AddDatabaseCost(objCost);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteCost(int id)
        {
            costManager.DeleteCost(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
