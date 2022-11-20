using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class TaskManager
    {
        private Context context;
        public TaskManager(Context _context)
        {
            context = _context;
        }
        public List<ObjTask> GetLastFiveTask(int all = 0)
        {
            if (all == 1)
            {
                return context.Tasks.OrderByDescending(x => x.Create_Date == DateTime.Now).Take(5).ToList();
            }
            else
            {
                return context.Tasks.OrderByDescending(x => x.Task_ID).Take(5).ToList();
            }
        }
        public void SaveTask(ObjTask objTask)
        {
            objTask.Create_Date = DateTime.Now;
            context.Add(objTask);
            context.SaveChanges();
        }
        public void DeleteTask(int Task_ID, int last = 0)
        {
            //context.Tasks.Remove(context.Tasks.Single(x => x.Task_ID == Task_ID));
            //context.SaveChanges();
        }
        public int CountTasks()
        {
            return context.Tasks.Count();
        }
    }
}
