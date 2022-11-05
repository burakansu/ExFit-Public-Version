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
                DataTable MembersTable = sQL.GetTBL("SELECT Name,Surname FROM TBL_Members WHERE Member_ID=" + this.Member_ID);
                if (MembersTable.Rows.Count > 0)
                {
                    String FullName = (String)MembersTable.Rows[0]["Name"];
                    FullName += " " + (String)MembersTable.Rows[0]["Surname"];
                    return FullName;
                }
                return "-";
            }
        }
        public string User_IMG
        {
            get
            {
                SQL sQL = new SQL();
                DataTable Tbl_User = sQL.GetTBL("SELECT IMG FROM TBL_Users WHERE User_ID=" + this.User_ID);
                if (Tbl_User.Rows.Count > 0)
                {
                    return (String)Tbl_User.Rows[0]["IMG"];
                }
                return "-";
            }
        }
        public string UserName
        {
            get
            {
                SQL sQL = new SQL();
                DataTable Tbl_User = sQL.GetTBL("SELECT Name,Surname FROM TBL_Users WHERE User_ID=" + this.User_ID);
                if (Tbl_User.Rows.Count > 0)
                {
                    String FullName = (String)Tbl_User.Rows[0]["Name"];
                    FullName += " " + (String)Tbl_User.Rows[0]["Surname"];
                    return FullName;
                }
                return "-";
            }
        }
    }
}
