using DatabaseLayer.MSSQL_Databases.ExFit_Database;
using ExFit.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjTask : TBL_Tasks
    {
        private Context context;
        public ObjTask(Context _context)
        {
            context = _context;
        }

        //Sanal Tablo Kolonları

        [NotMapped]
        public string MemberName // İşlem İle İlgili Üyenin Adı
        { 
            get
            {
                if (this.Member_ID != 0)
                {
                    string Name = context.Members.Single(x => x.Member_ID == this.Member_ID).Name;
                    string Surname = context.Members.Single(x => x.Member_ID == this.Member_ID).Surname;
                    return Name + " " + Surname;
                }
                return "-";
            }
        }
        [NotMapped]
        public string User_IMG // İşlemi Gerçekleştiren Personelin Resmi
        {
            get
            {
                return context.Users.Single(x => x.User_ID == this.User_ID).IMG;
            }
        }
        [NotMapped]
        public string UserName // İşlemi Gerçekleştiren Personelin Tam Adı
        {
            get
            {
                string Name = context.Users.Single(x => x.User_ID == this.User_ID).Name;
                string Surname = context.Users.Single(x => x.User_ID == this.User_ID).Surname;
                return Name + " " + Surname;
            }
        }
    }
}
