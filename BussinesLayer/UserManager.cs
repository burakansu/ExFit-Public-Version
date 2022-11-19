using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class UserManager
    {
        private Context context;
        public UserManager(Context _context)
        {
            context = _context;
        }
        public int Authorization(int Joined_User_ID)
        {
            if (Joined_User_ID != 0) { return 1; }
            else { return 0; }
        }
        public ObjUser CheckUserEntering(ObjUser User)
        {
            ObjUser _User = context.Users.SingleOrDefault(x => x.Mail == User.Mail && x.Password == User.Password);

            if (_User.User_ID != 0)
            {
                return _User;
            }
            else
            {
                User.User_ID = 0;
                return User;
            }
        }
        public void SaveUser(ObjUser objUser)
        {
            if (objUser.User_ID != 0)
            {
                context.Update(objUser);
                context.SaveChanges();
                new TaskManager(context).DeleteTask(0, 1);
            }
            else
            {
                context.Add(objUser);
                context.SaveChanges();
            }
        }
        public void DeleteUser(int id)
        {
            context.Users.Remove(context.Users.Single(x => x.User_ID == id));
            context.SaveChanges();
        }
        public List<ObjTask> GetUserTasks(int id)
        {
            return context.Tasks.Where(x => x.User_ID == id).ToList();
        }
        public ObjUser GetUser(int id)
        {
            return context.Users.Single(x => x.User_ID == id);
        }
        public List<ObjUser> GetUsers()
        {
            return context.Users.OrderBy(x => x.User_ID).ToList();
        }
    }
}
