using DatabaseLayer;
using DatabaseLayer.MSSQL_Databases.ExFit_Database;

namespace ObjectLayer
{
    public class ObjTask : TBL_Tasks
    {

        //Sanal Tablo Kolonları

        SQL sQL = new SQL();
        public string MemberName
        { 
            get
            {
                string Name = sQL.Value<string>("SELECT Name FROM TBL_Members WHERE Member_ID=" + this.Member_ID);
                string Surname = sQL.Value<string>("SELECT Surname FROM TBL_Members WHERE Member_ID=" + this.Member_ID);
                return Name + " " + Surname;
            }
        }
        public string User_IMG
        {
            get
            {
                return sQL.Value<string>("SELECT IMG FROM TBL_Users WHERE User_ID=" + this.User_ID);
            }
        }
        public string UserName
        {
            get
            {
                string Name = sQL.Value<string>("SELECT Name FROM TBL_Users WHERE User_ID=" + this.User_ID);
                string Surname = sQL.Value<string>("SELECT Surname FROM TBL_Users WHERE User_ID=" + this.User_ID);
                return Name + " " + Surname;
            }
        }
    }
}
