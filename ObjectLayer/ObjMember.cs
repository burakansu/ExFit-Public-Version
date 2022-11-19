using DatabaseLayer;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjMember : DatabaseLayer.ExFit_Database.TBL_Members
    {
        [NotMapped]
        public IFormFile? FileAvatarIMG { get; set; }
        [NotMapped]
        public IFormFile? FileHealthReport { get; set; }
        [NotMapped]
        public IFormFile? FileIdentityCard { get; set; }

        //Sanal Tablo Kolonları

        [NotMapped]
        public string FullName
        {
            get
            {
                return this.Name +" "+ this.Surname;
            }
        }
        [NotMapped]
        public int RemainingDay 
        { 
            get
            {
                return new SQL().Value<int>("SELECT DATEDIFF(DAY,'" + DateTime.Now.ToString("yyyyMMdd") + "','"+ this.Registration_Time.ToString("yyyyMMdd") + "')");
            }
        }
    }
}
