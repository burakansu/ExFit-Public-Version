using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class DietManager
    {
        SQL SQL = new SQL();
        public List<ObjDiet> GetDiets(int id = 0, bool Special = false)
        {
            string Command = "SELECT * FROM TBL_Diet WHERE Active = 1";
            if (id != 0 || Special == true) { Command = "SELECT * FROM TBL_Diet WHERE Diet_ID=" + id; };
            return SQL.Get<ObjDiet>(Command);
        }
        public void DeleteDiet(int id, bool Special = false)
        {
            string Command = "UPDATE TBL_Diet SET Active=0 WHERE Diet_ID=" + id;
            if (Special == true) { Command = "UPDATE TBL_Members SET Diet_ID = 0 WHERE Member_ID=" + id; }
            SQL.Execute(Command);
        }
        public void AddDatabaseDiet(ObjDiet objDiet)
        {
            objDiet.Registration_Date = DateTime.Now;
            SQL.Execute("INSERT INTO TBL_Diet (IMG,Diet_Name,Author,Registration_Date) VALUES (@IMG,@Diet_Name,@Author,@Registration_Date)", objDiet);
        }
    }
}
