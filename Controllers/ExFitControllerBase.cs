using BussinesLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ObjectLayer;

namespace ExFit.Controllers
{
    public class MemberControllerBase : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ControllerName = context.RouteData.Values["controller"].ToString();
            var ActionName = context.RouteData.Values["action"].ToString();

            if ((int)HttpContext.Session.GetInt32("ID") == 0)
            {
                context.Result = LocalRedirect("/LogIn/SignIn");
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
        public void TaskBuilder(int type, int id)
        {
            ObjTask objTask = new ObjTask();
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
            objTask.User_ID = (int)HttpContext.Session.GetInt32("ID");
            objTask.Member_ID = id;
            TaskManager taskManager = new TaskManager();
            taskManager.SaveTask(objTask);
        }
    }
}
