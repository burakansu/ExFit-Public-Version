using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class IncomeManager
    {
        private Context context;
        public IncomeManager(Context _context)
        {
            context = _context;
        }
        public List<ObjIncome> GetIncomes()
        {
            return context.Incomes.OrderBy(x => x.Year).ToList();
        }
        public void SaveIncome(ObjIncome objIncome)
        {
            objIncome.Year = DateTime.Now.Year;
            objIncome.WhichMonth = DateTime.Now.Month;

            if (objIncome.Income_ID != 0)
            {
                context.Incomes.Remove(context.Incomes.Single(x => x.Income_ID == objIncome.Income_ID ));
                context.Incomes.Add(objIncome);
                context.SaveChanges();
            }
            else
            {
                context.Incomes.Add(objIncome);
                context.SaveChanges();
            }
        }
    }
}
