using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class TaskManager
    {
        public List<ObjTask> GetLastFiveTask(int Company_ID, int all = 0)
        {
            using (Context x = new Context())
            {
                if (all == 1)
                {
                    return x.Tasks.Where(x => x.Company_ID == Company_ID).OrderByDescending(x => x.Task_ID).ToList();
                }
                else
                {
                    return x.Tasks.Where(x => x.Company_ID == Company_ID).OrderByDescending(x => x.Task_ID).Take(5).ToList();
                }
            }
        }
        public void SaveTask(ObjTask objTask)
        {
            using (Context x = new Context())
            {
                x.Tasks.Add(objTask);
                x.SaveChanges();
            }
        }
        public void DeleteTask(int Task_ID, int last = 0)
        {
            using (Context x = new Context())
            {
                x.Tasks.Remove(x.Tasks.Single(x => x.Task_ID == Task_ID));
                x.SaveChanges();
            }
        }
        public int CountTasks(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Tasks.Where(x => x.Company_ID == Company_ID).Count();
            }
        }
        public ObjTask TaskBuilder(int Company_ID, int desctype, int Member_ID, int User_ID)
        {
            using (Context x = new Context())
            {
                ObjTask objTask = new ObjTask();
                switch (desctype)
                {
                    case 0:
                        objTask.Description = "Yeni Üye Kayıt Edildi";
                        break;
                    case 1:
                        objTask.Description = "Üye Güncellendi";
                        break;
                    case 2:
                        objTask.Description = "Üye Kontratı Sonlandı!";
                        break;
                    case 3:
                        objTask.Description = "Üye Kontratı Yenilendi";
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
                    case 8:
                        objTask.Description = "Üye Kaydedilemedi Zaten Kayıtlı Email Veya Telefon";
                        break;
                    case 9:
                        objTask.Description = "Personel Kaydedilemedi Zaten Kayıtlı Email Veya Telefon";
                        break;
                    case 10:
                        objTask.Description = "Personel Güncellendi";
                        break;
                    case 11:
                        objTask.Description = "Personel İşten Çıkarıldı";
                        break;
                }
                objTask.Create_Date = DateTime.Now;
                objTask.User_ID = User_ID;
                objTask.Member_ID = Member_ID;
                objTask.Company_ID = Company_ID;
                return objTask;
            }
        }
    }
}
