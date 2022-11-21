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
        public string FullName // Üyenin Tam İsmi
        {
            get
            {
                return this.Name +" "+ this.Surname;
            }
        }
        [NotMapped]
        public int RemainingDay // Üyenin Kalan Günü
        { 
            get
            {
                DateTime now = DateTime.Now;
                TimeSpan ts = this.Registration_Time - now;
                return Convert.ToInt32(ts.Days);
            }
        }
    }
}
