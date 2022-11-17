using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class DietManager
    {
        SQL SQL = new SQL();
        public List<ObjDiet> GetDiets(int id = 0, bool Special = false)
        {
            string Query = "SELECT * FROM TBL_Diet WHERE Active = 1";
            if (id != 0 || Special == true) { Query = "SELECT * FROM TBL_Diet WHERE Diet_ID=" + id; };
            return SQL.Get<ObjDiet>(Query);
        }
        public void DeleteDiet(int id, bool Special = false)
        {
            string Query = "UPDATE TBL_Diet SET Active=0 WHERE Diet_ID=" + id;
            if (Special == true) { Query = "UPDATE TBL_Members SET Diet_ID = 0 WHERE Member_ID=" + id; }
            SQL.Run(Query);
        }
        public void AddDatabaseDiet(ObjDiet objDiet)
        {
            objDiet.Registration_Date = DateTime.Now;
            objDiet.IMG = "";
            SQL.Run("INSERT INTO TBL_Diet (Diet_Name,Author,Registration_Date) VALUES (@Diet_Name,@Author,@Registration_Date)", objDiet);
        }
        public int[] Counts(int day, int id)
        {
            int[] ints = { 0, 0, 0, 0, 0, 0 };
            List<ObjFood> list = SQL.Get<ObjFood>("SELECT * FROM TBL_Food WHERE Day=" + day + " AND Diet_ID=" + id);
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
