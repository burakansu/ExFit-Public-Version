using DatabaseLayer;
using DatabaseLayer.ExFit_Database;
using Microsoft.AspNetCore.Http;

namespace ObjectLayer
{
    public class ObjMember : DatabaseLayer.ExFit_Database.ObjMember
    {
        public IFormFile? FileAvatarIMG { get; set; }
        public IFormFile? FileHealthReport { get; set; }
        public IFormFile? FileIdentityCard { get; set; }
        public string FullName
        {
            get
            {
                return this.Name +" "+ this.Surname;
            }
        }
        public int RemainingDay 
        { 
            get
            {
                SQL sQL = new SQL();
                return sQL.Count_Database("SELECT DATEDIFF(DAY,'" + DateTime.Now.ToString("yyyyMMdd") + "','"+ this.Registration_Time.ToString("yyyyMMdd") +"')");

            }
        }
    }
}
