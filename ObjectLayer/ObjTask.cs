using DatabaseLayer.MSSQL_Databases.ExFit_Database;
using ExFit.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjTask : TBL_Tasks
    {

        //Sanal Tablo Kolonları

        [NotMapped]
        public string MemberName // İşlem İle İlgili Üyenin Adı
        {
            get
            {
                if (this.Member_ID != 0)
                {
                    using (Context x = new Context())
                    {
                        string Name = x.Members.Single(x => x.Member_ID == this.Member_ID).Name;
                        string Surname = x.Members.Single(x => x.Member_ID == this.Member_ID).Surname;
                        return Name + " " + Surname;
                    }
                }
                return "-";
            }
        }
        [NotMapped]
        public string User_IMG // İşlemi Gerçekleştiren Personelin Resmi
        {
            get
            {
                using (Context x = new Context())
                {
                    return x.Users.Single(x => x.User_ID == this.User_ID).IMG;
                }
            }
        }
        [NotMapped]
        public string UserName // İşlemi Gerçekleştiren Personelin Tam Adı
        {
            get
            {
                using (Context x = new Context())
                {
                    string Name = x.Users.Single(x => x.User_ID == this.User_ID).Name;
                    string Surname = x.Users.Single(x => x.User_ID == this.User_ID).Surname;
                    return Name + " " + Surname;
                }
            }
        }
    }
}
