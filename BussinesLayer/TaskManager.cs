using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class TaskManager
    {
        SQL SQL = new SQL();
        public List<ObjTask> GetLastFiveTask(int all = 0)
        {
            if (all == 1)
            {
                return SQL.Get<ObjTask>("SELECT * FROM TBL_Tasks WHERE Create_Date='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ORDER BY Task_ID desc");
            }
            else
            {
                return SQL.Get<ObjTask>("SELECT TOP(5)* FROM TBL_Tasks ORDER BY Task_ID desc");
            }
        }
        public void SaveTask(ObjTask objTask)
        {
            objTask.Create_Date = DateTime.Now;
            SQL.Execute("INSERT INTO TBL_Tasks (Description,Create_Date,User_ID,Member_ID) VALUES (@Description,@Create_Date,@User_ID,@Member_ID)", objTask);
        }
        public void DeleteTask(int Task_ID, int last = 0)
        {
            //    if (last == 1)
            //    {
            //        int Miss = conn.QuerySingle<int>("SELECT TOP 1 Task_ID FROM TBL_Tasks ORDER BY Task_ID desc");
            //        SQL.Execute("DELETE FROM TBL_Tasks WHERE Task_ID = " + Miss);
            //    }
            //    else
            //    {
            //        SQL.Execute("DELETE FROM TBL_Tasks WHERE Task_ID = " + Task_ID);
            //    }
        }
        public int CountTasks()
        {
            return SQL.GetSingle<int>("SELECT COUNT(*) FROM TBL_Tasks");
        }
    }
}
