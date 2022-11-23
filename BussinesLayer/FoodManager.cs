using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class FoodManager
    {
        public List<ObjFood> GetFoods()
        {
            using (Context x = new Context())
            {
                return x.Foods.OrderBy(x => x.Food_ID).ToList();
            }
        }
        public void DeleteFood(int id)
        {
            using (Context x = new Context())
            {
                x.Foods.Remove(x.Foods.Single(x => x.Food_ID == id));
                x.SaveChanges();
            }
        }
        public void AddDatabaseFood(ObjFood ObjFood)
        {
            using (Context x = new Context())
            {
                x.Add(ObjFood);
                x.SaveChanges();
            }
        }
    }
}
