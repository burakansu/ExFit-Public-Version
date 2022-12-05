using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class IncomeManager
    {
        private DateTime MontFirstDay = DateTime.Now.MonthFirstDay();
        private DateTime MonthLastDay = DateTime.Now.MonthLastDay();

        public List<ObjIncome> GetIncomes(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Incomes.Where(x => x.Company_ID == Company_ID && x.Year == DateTime.Now.Year).OrderBy(x => x.Year).ToList();
            }
        }
        public void UpdateIncomeAuto(int User_ID)
        {
            using (Context x = new Context())
            {
                // Bir Değişiklik Varsa O Ayki Geliri Günceller Veya Yeni Aya Geçildiyse Gelir Kaydı Oluşturur.
                ObjIncome objIncome = new ObjIncome();
                int Company_ID = x.Users.Single(x => x.User_ID== User_ID).Company_ID;
                foreach (var item in x.Members.Where(x => x.Registration_Date >= MontFirstDay && x.Registration_Date <= MonthLastDay && x.Company_ID == Company_ID && x.Block == 0).ToList())
                {
                    objIncome.Value += item.TotalPrice;
                    objIncome.Company_ID = Company_ID;
                    if (x.Incomes.Where(x => x.WhichMonth == DateTime.Now.Month).Count() > 0)
                    {
                        objIncome.Income_ID = x.Incomes.Single(x => x.WhichMonth == DateTime.Now.Month).Income_ID;
                    }
                    new IncomeManager().SaveIncome(objIncome);

                }
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
                    x.Incomes.Add(objIncome);
                x.SaveChanges();
            }
        }
    }
    public static class ExtensionMethods
    {
        public static DateTime MonthFirstDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime MonthLastDay(this DateTime dt)
        {
            return dt.MonthFirstDay().AddMonths(1).AddDays(-1);
        }
    }
}
