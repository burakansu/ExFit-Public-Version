using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class FoodManager
    {
        private Context context;
        public FoodManager(Context _context)
        {
            context = _context;
        }
        public List<ObjFood> GetFoods()
        {
            return context.Foods.OrderBy(x => x.Food_ID).ToList();
        }
        public void DeleteFood(int id)
        {
            context.Foods.Remove(context.Foods.Single(x => x.Food_ID == id));
            context.SaveChanges();
        }
        public void AddDatabaseFood(ObjFood ObjFood)
        {
            context.Add(ObjFood);
            context.SaveChanges();
        }
    }
}
