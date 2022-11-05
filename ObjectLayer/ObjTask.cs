using DatabaseLayer;
using DatabaseLayer.MSSQL_Databases.ExFit_Database;
using System.Data;

namespace ObjectLayer
{
    public class ObjTask : TBL_Tasks
    {

        //Sanal Tablo Kolonları

        public string MemberName
        { 
            get
            {
                SQL sQL = new SQL();
                string Name = sQL.GetSingle<string>("SELECT Name FROM TBL_Members WHERE Member_ID=" + this.Member_ID);
                string Surname = sQL.GetSingle<string>("SELECT Surname FROM TBL_Members WHERE Member_ID=" + this.Member_ID);
                return Name + " " + Surname;
            }
        }
        public string User_IMG
        {
            get
            {
                SQL sQL = new SQL();
                return sQL.GetSingle<string>("SELECT IMG FROM TBL_Users WHERE User_ID=" + this.User_ID);
            }
        }
        public string UserName
        {
            get
            {
                SQL sQL = new SQL();
                string Name = sQL.GetSingle<string>("SELECT Name FROM TBL_Users WHERE User_ID=" + this.User_ID);
                string Surname = sQL.GetSingle<string>("SELECT Surname FROM TBL_Users WHERE User_ID=" + this.User_ID);
                return Name + " " + Surname;
            }
        }
    }
}
