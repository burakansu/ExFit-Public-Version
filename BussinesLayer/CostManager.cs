using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class CostManager
    {
        public List<ObjCost> GetCosts(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Costs.Where(x => x.Company_ID == Company_ID).ToList();
            }
        }
        public ObjCost GetCost(int id)
        {
            using (Context x = new Context())
            {
                return x.Costs.Single(x => x.Cost_ID == id);
            }
        }
        public void DeleteCost(int id)
        {
            using (Context x = new Context())
            {
                x.Costs.Remove(x.Costs.Single(x => x.Cost_ID == id));
                x.SaveChanges();
            }
        }
        public void SaveCost(ObjCost objCost)
        {
            using (Context x = new Context())
            {
                if (objCost.Cost_ID != 0)
                    x.Costs.Update(objCost);
                else
                {
                    objCost.Year = DateTime.Now;
                    objCost.WhichMonth = DateTime.Now.Month;
                    int count = x.Costs.Where(x => x.WhichMonth == objCost.WhichMonth && x.Company_ID == objCost.Company_ID).Count();
                    if (count == 0)
                        x.Costs.Add(objCost);
                }
                x.SaveChanges();
            }
        }
        public int TotalCost(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Costs.Where(x => x.Company_ID == Company_ID).Sum(x => x.Rent) + x.Costs.Where(x => x.Company_ID == Company_ID).Sum(x => x.Electric) + x.Costs.Where(x => x.Company_ID == Company_ID).Sum(x => x.Water) + x.Costs.Where(x => x.Company_ID == Company_ID).Sum(x => x.Staff_Salaries) + x.Costs.Where(x => x.Company_ID == Company_ID).Sum(x => x.Other);
            }
        }
    }
}
