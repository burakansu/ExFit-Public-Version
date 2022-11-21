using ExFit.Data;
using Microsoft.AspNetCore.Http;
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
        public ObjTask TaskBuilder(int type, int id, int userid)
        {
            ObjTask objTask = new ObjTask(context);
            switch (type)
            {
                case 0:
                    objTask.Description = "Yeni Üye Kayıt Edildi";
                    break;
                case 1:
                    objTask.Description = "Üye Güncellendi";
                    break;
                case 2:
                    objTask.Description = "Üye Kaydı Pasif Durumda!";
                    break;
                case 3:
                    objTask.Description = "Üye Kaydı Aktifleştirildi";
                    break;
                case 4:
                    objTask.Description = "Yeni Çalışan İşe Alındı";
                    break;
                case 5:
                    objTask.Description = "Yeni Egzersiz Programı";
                    break;
                case 6:
                    objTask.Description = "Yeni Diyet Programı";
                    break;
                case 7:
                    objTask.Description = "Üye Kaydı Silindi";
                    break;
            }
            objTask.User_ID = userid;
            objTask.Member_ID = id;
            return objTask;
        }
    }
}
