using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class UserManager
    {
        public int Authorization(int Joined_User_ID)
        {
            if (Joined_User_ID != 0) { return 1; }
            else { return 0; }
        }
        public ObjUser CheckUserEntering(ObjUser User)
        {
            using (Context x = new Context())
            {
                ObjUser _User = x.Users.SingleOrDefault(x => x.Mail == User.Mail && x.Password == User.Password);

                if (_User != null)
                {
                    return _User;
                }
                else
                {
                    User.User_ID = 0;
                    return User;
                }
            }
        }
        public void SaveUser(ObjUser objUser)
        {
            using (Context x = new Context())
            {
                if (objUser.User_ID != 0)
                {
                    x.Update(objUser);
                    new TaskManager().DeleteTask(0, 1);
                }
                else
                {
                    x.Add(objUser);
                }
                x.SaveChanges();
            }
        }
        public void DeleteUser(int id)
        {
            using (Context x = new Context())
            {
                x.Users.Remove(x.Users.Single(x => x.User_ID == id));
                x.SaveChanges();
            }
        }
        public List<ObjTask> GetUserTasks(int id)
        {
            using (Context x = new Context())
            {
                return x.Tasks.Where(x => x.User_ID == id).ToList();
            }
        }
        public ObjUser GetUser(int id)
        {
            using (Context x = new Context())
            {
                return x.Users.Single(x => x.User_ID == id);
            }
        }
        public List<ObjUser> GetUsers(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Users.Where(x => x.Company_ID == Company_ID).OrderBy(x => x.User_ID).ToList();
            }
        }
    }
}
