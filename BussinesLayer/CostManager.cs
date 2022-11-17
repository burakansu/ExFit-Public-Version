using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class CostManager
    {
        SQL SQL = new SQL();
        public List<ObjCost> GetCosts()
        {
            return SQL.Get<ObjCost>("SELECT * FROM TBL_Cost"); 
        }
        public ObjCost GetCost(int id)
        {
            return SQL.Single<ObjCost>("SELECT * FROM TBL_Cost WHERE Cost_ID=" + id);
        }
        public void DeleteCost(int id)
        {
            SQL.Run("DELETE TBL_Cost WHERE Cost_ID="+ id);
        }
        public void AddDatabaseCost(ObjCost objCost)
        {
            objCost.Year = DateTime.Now;
            objCost.WhichMonth = DateTime.Now.Month;
            SQL.Run("INSERT INTO TBL_Cost (Rent,Electric,Water,Staff_Salaries,Other,WhichMonth,Year) VALUES (@Rent,@Electric,@Water,@Staff_Salaries,@Other,@WhichMonth,@Year)", objCost);
        }
    }
}
