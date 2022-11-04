using Dapper;
using DatabaseLayer;
using ObjectLayer;
using System.Data;
using System.Data.SqlClient;

namespace BussinesLayer
{
    public class Excersize_Manager
    {
        public List<ObjExcersize> GetExcersizes(int id = 0, bool Special = false)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                string Command = "SELECT * FROM TBL_Excersize WHERE Active = 1";
                if (id != 0 || Special == true) { Command = "SELECT * FROM TBL_Excersize WHERE Excersize_ID=" + id; };
                return conn.Query<ObjExcersize>(Command).ToList();
            }
        }
        public void DeleteExcersize(int id, bool Special = false)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                string Command = "UPDATE TBL_Excersize SET Active = 0 WHERE Excersize_ID =" + id;
                if (Special == true) { Command = "UPDATE TBL_Members SET Excersize_ID = 0 WHERE Member_ID=" + id; }
                conn.Execute(Command);
            }
        }
        public void AddDatabaseExcersize(ObjExcersize objExcersize)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                conn.Execute("INSERT INTO TBL_Excersize(IMG, Excersize_Name, Author, Registration_Date) VALUES(@IMG, @Excersize_Name, @Author, @Registration_Date)", objExcersize);
            }        
        }
    }
}