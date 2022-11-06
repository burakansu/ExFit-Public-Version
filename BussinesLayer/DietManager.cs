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
            SQL.Run("INSERT INTO TBL_Diet (IMG,Diet_Name,Author,Registration_Date) VALUES (@IMG,@Diet_Name,@Author,@Registration_Date)", objDiet);
        }
    }
}
