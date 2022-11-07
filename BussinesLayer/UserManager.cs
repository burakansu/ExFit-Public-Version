using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class UserManager
    {
        SQL SQL = new SQL();
        TaskManager taskManager = new TaskManager();
        public int Authorization(int Joined_User_ID)
        {
            if (Joined_User_ID != 0) { return 1; }
            else { return 0; }
        }
        public ObjUser CheckUserEntering(ObjUser User)
        {
            ObjUser _user = SQL.Single<ObjUser>("SELECT * FROM TBL_Users WHERE Mail='" + User.Mail + "' AND Password='" + User.Password + "'");

            if (_user.User_ID != 0)
            {
                User.User_ID = _user.User_ID;
                User.Type = _user.Type;
                return User;
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
                SQL.Run("UPDATE TBL_Users SET Phone=@Phone ,Name=@Name ,Surname=@Surname ,Mail=@Mail ,Password=@Password ,Position=@Position ,IMG=@IMG WHERE User_ID=" + objUser.User_ID, objUser);
                taskManager.DeleteTask(0, 1);
            }
            else
            {
                SQL.Run("INSERT INTO TBL_Users (Name,Surname,Mail,Password,Position,Type,IMG,Phone) VALUES (@Name,@Surname,@Mail,@Password,@Position,@Type,@IMG,@Phone)", objUser);
            }
        }
        public void DeleteUser(int User_ID)
        {
            SQL.Run("DELETE TBL_Users WHERE User_ID=" + User_ID);
        }
        public List<ObjTask> GetUserTasks(int id)
        {
            return SQL.Get<ObjTask>("SELECT * FROM TBL_Tasks WHERE User_ID =" + id);
        }
        public ObjUser GetUser(int ID)
        {
            return SQL.Single<ObjUser>("SELECT * FROM TBL_Users WHERE User_ID=" + ID);
        }
        public List<ObjUser> GetUsers()
        {
            return SQL.Get<ObjUser>("SELECT * FROM TBL_Users");
        }
    }
}
