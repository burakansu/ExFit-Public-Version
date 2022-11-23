using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class IncomeManager
    {
        public List<ObjIncome> GetIncomes(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Incomes.Where(x => x.Company_ID == Company_ID).OrderBy(x => x.Year).ToList();
            }
        }
        public void SaveIncome(ObjIncome objIncome)
        {
            using (Context x = new Context())
            {
                objIncome.Year = DateTime.Now.Year;
                objIncome.WhichMonth = DateTime.Now.Month;

                if (objIncome.Income_ID != 0)
                {
                    x.Incomes.Remove(x.Incomes.Single(x => x.Income_ID == objIncome.Income_ID));
                    x.Incomes.Add(objIncome);
                }
                else
                {
                    x.Incomes.Add(objIncome);
                }
                x.SaveChanges();
            }
        }
    }
}
