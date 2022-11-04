using Dapper;
using DatabaseLayer;
using ObjectLayer;
using System.Data;
using System.Data.SqlClient;

namespace BussinesLayer
{
    public class DietManager
    {
        public List<ObjDiet> GetDiets(int id = 0, bool Special = false)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                string Command = "SELECT * FROM TBL_Diet WHERE Active = 1";
                if (id != 0 || Special == true) { Command = "SELECT * FROM TBL_Diet WHERE Diet_ID=" + id; };
                return conn.Query<ObjDiet>(Command).ToList();
            }
        }
        public void DeleteDiet(int id, bool Special = false)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                string Command = "UPDATE TBL_Diet SET Active=0 WHERE Diet_ID=" + id;
                if (Special == true) { Command = "UPDATE TBL_Members SET Diet_ID = 0 WHERE Member_ID=" + id; }
                conn.Execute(Command);
            }
        }
        public void AddDatabaseDiet(ObjDiet objDiet)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                conn.Execute("insert into TBL_Diet (IMG,Diet_Name,Author,Registration_Date) Values (@IMG,@Diet_Name,@Author,@Registration_Date)", objDiet);
            }
        }
    }
}
