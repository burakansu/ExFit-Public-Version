using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class CostManager
    {
        private Context context;
        public CostManager(Context _context)
        {
            context = _context;
        }
        public List<ObjCost> GetCosts()
        {
            return context.Costs.ToList();
        }
        public ObjCost GetCost(int id)
        {
            return context.Costs.Single(x => x.Cost_ID == id);
        }
        public void DeleteCost(int id)
        {
            context.Costs.Remove(context.Costs.Single(x => x.Cost_ID == id));
            context.SaveChanges();
        }
        public void AddDatabaseCost(ObjCost objCost)
        {
            objCost.Year = DateTime.Now;
            objCost.WhichMonth = DateTime.Now.Month;
            context.Add(objCost);
            context.SaveChanges();
        }
        public int TotalCost()
        {
            return context.Costs.Sum(x => x.Rent) + context.Costs.Sum(x => x.Electric) + context.Costs.Sum(x => x.Water) + context.Costs.Sum(x => x.Staff_Salaries) + context.Costs.Sum(x => x.Other);
        }
    }
}
