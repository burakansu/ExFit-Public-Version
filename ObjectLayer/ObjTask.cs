using DatabaseLayer;
using DatabaseLayer.MSSQL_Databases.ExFit_Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjTask : TBL_Tasks
    {

        //Sanal Tablo Kolonları

        [NotMapped]
        public string MemberName
        { 
            get
            {
                string Name = new SQL().Value<string>("SELECT Name FROM TBL_Members WHERE Member_ID=" + this.Member_ID);
                string Surname = new SQL().Value<string>("SELECT Surname FROM TBL_Members WHERE Member_ID=" + this.Member_ID);
                return Name + " " + Surname;
            }
        }
        [NotMapped]
        public string User_IMG
        {
            get
            {
                return new SQL().Value<string>("SELECT IMG FROM TBL_Users WHERE User_ID=" + this.User_ID);
            }
        }
        [NotMapped]
        public string UserName
        {
            get
            {
                string Name = new SQL().Value<string>("SELECT Name FROM TBL_Users WHERE User_ID=" + this.User_ID);
                string Surname = new SQL().Value<string>("SELECT Surname FROM TBL_Users WHERE User_ID=" + this.User_ID);
                return Name + " " + Surname;
            }
        }
    }
}
