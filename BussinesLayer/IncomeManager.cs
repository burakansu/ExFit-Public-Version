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
            return context.Incomes.OrderBy(x => x.Income_ID).ToList();
        }
        public void SaveIncome(ObjIncome objIncome)
        {
            //if (objIncome.Income_ID != 0)
            //{
            //    objIncome.Year = DateTime.Now;
            //    objIncome.WhichMonth = DateTime.Now.Month;
            //    context.Add(objIncome);
            //    context.SaveChanges();
            //    //SQL.Run("INSERT INTO TBL_Income (Value,WhichMonth,Year) VALUES (@Value,@WhichMonth,@Year)", objIncome);
            //}
            //else
            //{
            //    objIncome.Year = DateTime.Now;
            //    objIncome.WhichMonth = DateTime.Now.Month;
            //    context.Update(objIncome);
            //    context.SaveChanges();
            //    //SQL.Run("UPDATE TBL_Income SET Value=@Value,WhichMonth=@WhichMonth,Year=@Year WHERE WhichMonth=" + objIncome.WhichMonth, objIncome);
            //}
        }
    }
}
