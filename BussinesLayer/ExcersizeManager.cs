using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class ExcersizeManager
    {
        SQL SQL = new SQL();
        public List<ObjExcersize> GetExcersizes(int id = 0, bool Special = false)
        {
            string Query = "SELECT * FROM TBL_Excersize WHERE Active = 1";
            if (id != 0 || Special == true) { Query = "SELECT * FROM TBL_Excersize WHERE Excersize_ID=" + id; };
            return SQL.Get<ObjExcersize>(Query);
        }
        public void DeleteExcersize(int id, bool Special = false)
        {
            string Query = "UPDATE TBL_Excersize SET Active = 0 WHERE Excersize_ID =" + id;
            if (Special == true) { Query = "UPDATE TBL_Members SET Excersize_ID = 0 WHERE Member_ID=" + id; }
            SQL.Run(Query);
        }
        public void AddDatabaseExcersize(ObjExcersize objExcersize)
        {
            objExcersize.Registration_Date = DateTime.Now;
            SQL.Run("INSERT INTO TBL_Excersize(IMG, Excersize_Name, Author, Registration_Date) VALUES (@IMG, @Excersize_Name, @Author, @Registration_Date)", objExcersize);
        }
    }
}