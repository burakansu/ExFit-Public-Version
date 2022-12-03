using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class DietManager
    {
        public List<ObjDiet> GetDiets(int Company_ID, int id = 0, bool Special = false)
        {
            using (Context x = new Context())
            {
                if (id != 0 || Special == true)
                    return x.Diets.Where(x => x.Diet_ID == id).ToList();
                else
                    return x.Diets.Where(x => x.Active == 1 && x.Company_ID == Company_ID).ToList();
            }
        }
        public void DeleteDiet(int id, bool Special = false)
        {
            using (Context x = new Context())
            {
                if (Special == true)
                {
                    ObjMember objMember = x.Members.Single(x => x.Member_ID == id);
                    objMember.Diet_ID = 0;
                    x.Members.Update(objMember);
                }
                else
                {
                    ObjDiet objDiet = x.Diets.Single(x => x.Diet_ID == id);
                    objDiet.Active = 0;
                    x.Diets.Update(objDiet);
                }
                x.SaveChanges();
            }
        }
        public void AddDatabaseDiet(ObjDiet objDiet)
        {
            using (Context x = new Context())
            {
                objDiet.Registration_Date = DateTime.Now;
                objDiet.Active = 1;
                x.Add(objDiet);
                x.SaveChanges();
            }
        }
        public int[] Counts(int day, int id)
        {
            using (Context x = new Context())
            {
                int[] ints = { 0, 0, 0, 0, 0, 0 };
                List<ObjFood> list = x.Foods.Where(x => x.Day == day && x.Diet_ID == id).ToList();
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
}
