using Dapper;
using DatabaseLayer;
using ObjectLayer;
using System.Data;
using System.Data.SqlClient;

namespace BussinesLayer
{
    public class UserManager
    {
        TaskManager taskManager = new TaskManager();
        public int Authorization(int Joined_User_ID)
        {
            if (Joined_User_ID != 0) { return 1; }
            else { return 0; }
        }
        public ObjUser CheckUserEntering(ObjUser User)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                ObjUser _user = conn.QuerySingle<ObjUser>("SELECT * FROM TBL_Users WHERE Mail='" + User.Mail + "' AND Password='" + User.Password + "'");

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
        }
        public void SaveUser(ObjUser objUser)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                if (objUser.User_ID != 0)
                {
                    conn.Query<ObjUser>("UPDATE TBL_Users SET Phone=@Phone ,Name=@Name ,Surname=@Surname ,Mail=@Mail ,Password=@Password ,Position=@Position ,IMG=@IMG WHERE User_ID=" + objUser.User_ID, objUser);
                    taskManager.DeleteTask(0, 1);
                }
                else
                {
                    conn.Query<ObjUser>("INSERT INTO TBL_Users (Name,Surname,Mail,Password,Position,Type,IMG,Phone) VALUES (@Name,@Surname,@Mail,@Password,@Position,@Type,@IMG,@Phone)", objUser);
                }
            }
        }
        public void DeleteUser(int User_ID)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                conn.Execute("DELETE TBL_Users WHERE User_ID=" + User_ID);
            }
        }
        public List<ObjTask> GetUserTasks(int id)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                return conn.Query<ObjTask>("SELECT * FROM TBL_Tasks WHERE User_ID =" + id).ToList();
            }
        }
        public ObjUser GetUser(int ID)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                return conn.QuerySingle<ObjUser>("SELECT * FROM TBL_Users WHERE User_ID=" + ID);
            }
        }
        public List<ObjUser> GetUsers()
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                return conn.Query<ObjUser>("SELECT * FROM TBL_Users").ToList();
            }
        }
    }
}
