using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class DietManager
    {
        private Context context;
        public DietManager(Context _context)
        {
            context = _context;
        }
        public List<ObjDiet> GetDiets(int id = 0, bool Special = false)
        {
            if (id != 0 || Special == true) 
            {
                return context.Diets.Where(x => x.Diet_ID == id).ToList();
            }
            else
            {
                return context.Diets.Where(x => x.Active == 1).ToList();
            }
        }
        public void DeleteDiet(int id, bool Special = false)
        {
            if (Special == true) 
            {
                ObjMember objMember = context.Members.Single(x => x.Member_ID == id);
                objMember.Diet_ID = 0;
                context.Members.Update(objMember);
                context.SaveChanges();
            }
            else
            {
                ObjDiet objDiet = context.Diets.Single(x => x.Diet_ID == id);
                objDiet.Active = 0;
                context.Diets.Update(objDiet);
                context.SaveChanges();
            }
        }
        public void AddDatabaseDiet(ObjDiet objDiet)
        {
            objDiet.Registration_Date = DateTime.Now;
            objDiet.IMG = "";
            context.Add(objDiet);
            context.SaveChanges();
        }
        public int[] Counts(int day, int id)
        {
            int[] ints = { 0, 0, 0, 0, 0, 0 };
            List<ObjFood> list = context.Foods.Where(x => x.Day == day && x.Diet_ID == id).ToList();
            foreach (var item in list)
            {
                switch (item.MealType)
                {
                    case 1:
                        ints[0] = 1;
                        break;
                    case 2:
                        ints[1] = 1;
                        break;
                    case 3:
                        ints[2] = 1;
                        break;
                    case 4:
                        ints[3] = 1;
                        break;
                    case 5:
                        ints[4] = 1;
                        break;
                    case 6:
                        ints[5] = 1;
                        break;
                }
            }
            return ints;
        }
    }
}
