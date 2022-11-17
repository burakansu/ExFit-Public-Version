using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class FoodManager
    {
        SQL SQL = new SQL();
        public List<ObjFood> GetFoods()
        {
            return SQL.Get<ObjFood>("SELECT * FROM TBL_Food");
        }
        public void DeleteFood(int id)
        {
            SQL.Run("DELETE TBL_Food WHERE Food_ID=" + id);
        }
        public void AddDatabaseFood(ObjFood ObjFood)
        {
            SQL.Run("INSERT INTO TBL_Food (MealType, Name, Calorie, Protein, Fat, Note, Day, Diet_ID) VALUES (@MealType, @Name, @Calorie, @Protein, @Fat, @Note, @Day, @Diet_ID) ", ObjFood);
        }
    }
}
