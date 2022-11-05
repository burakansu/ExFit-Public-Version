using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class Excersize_Manager
    {
        SQL SQL = new SQL();
        public List<ObjExcersize> GetExcersizes(int id = 0, bool Special = false)
        {
            string Command = "SELECT * FROM TBL_Excersize WHERE Active = 1";
            if (id != 0 || Special == true) { Command = "SELECT * FROM TBL_Excersize WHERE Excersize_ID=" + id; };
            return SQL.Get<ObjExcersize>(Command);
        }
        public void DeleteExcersize(int id, bool Special = false)
        {
            string Command = "UPDATE TBL_Excersize SET Active = 0 WHERE Excersize_ID =" + id;
            if (Special == true) { Command = "UPDATE TBL_Members SET Excersize_ID = 0 WHERE Member_ID=" + id; }
            SQL.Execute(Command);
        }
        public void AddDatabaseExcersize(ObjExcersize objExcersize)
        {
            objExcersize.Registration_Date = DateTime.Now;
            SQL.Execute("INSERT INTO TBL_Excersize(IMG, Excersize_Name, Author, Registration_Date) VALUES (@IMG, @Excersize_Name, @Author, @Registration_Date)", objExcersize);
        }
    }
}