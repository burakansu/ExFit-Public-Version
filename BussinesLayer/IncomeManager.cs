using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class IncomeManager
    {
        SQL SQL = new SQL();
        public List<ObjIncome> GetIncomes()
        {
            return SQL.Get<ObjIncome>("SELECT * FROM TBL_Income");
        }
        public void SaveIncome(ObjIncome objIncome)
        {
            if (objIncome.Income_ID != 0)
            {
                objIncome.Year = DateTime.Now;
                objIncome.WhichMonth = DateTime.Now.Month;
                SQL.Run("INSERT INTO TBL_Income (Value,WhichMonth,Year) VALUES (@Value,@WhichMonth,@Year)", objIncome);
            }
            else
            {
                objIncome.Year = DateTime.Now;
                objIncome.WhichMonth = DateTime.Now.Month;
                SQL.Run("UPDATE TBL_Income SET Value=@Value,WhichMonth=@WhichMonth,Year=@Year WHERE WhichMonth=" + objIncome.WhichMonth, objIncome);
            }
        }
    }
}
