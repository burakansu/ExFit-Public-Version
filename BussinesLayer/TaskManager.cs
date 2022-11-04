using Dapper;
using DatabaseLayer;
using ObjectLayer;
using System.Data;
using System.Data.SqlClient;

namespace BussinesLayer
{
    public class TaskManager
    {
        public List<ObjTask> GetLastFiveTask(int all = 0)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                if (all == 1)
                {
                    return conn.Query<ObjTask>("SELECT * FROM TBL_Tasks WHERE Create_Date='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY Task_ID desc").ToList();
                }
                else
                {
                    return conn.Query<ObjTask>("SELECT TOP(5)* FROM TBL_Tasks ORDER BY Task_ID desc").ToList();
                }
            }
        }
        public void SaveTask(ObjTask objTask)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                conn.Execute("INSERT INTO TBL_Tasks (Description,Create_Date,User_ID,Member_ID) VALUES (@Description,@Create_Date,@User_ID,@Member_ID)", objTask);
            }
        }
        public void DeleteTask(int Task_ID, int last = 0)
        {
            //using (IDbConnection conn = new SqlConnection(SQL.SqlConnectionString))
            //{
            //    if (last == 1)
            //    {
            //        int Miss = conn.QuerySingle<int>("SELECT TOP 1 Task_ID FROM TBL_Tasks ORDER BY Task_ID desc");
            //        conn.Execute("DELETE FROM TBL_Tasks WHERE Task_ID = " + Miss);
            //    }
            //    else
            //    {
            //        conn.Execute("DELETE FROM TBL_Tasks WHERE Task_ID = " + Task_ID);
            //    }
            //}
        }
        public int CountTasks()
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                return conn.QuerySingle<int>("SELECT COUNT(*) FROM TBL_Tasks");
            }
        }
    }
}
