using DatabaseLayer.ExFit_Database;
using ExFit.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjExcersize : TBL_Excersize
    {
        //Sanal Tablo Kolonları

        [NotMapped]
        public int Count // Egzersizin Üyeler Tarafından Kullanılma Sayısı
        { 
            get
            {
                using (Context x = new Context())
                {
                    return x.Excersizes.Where(x => x.Excersize_ID == this.Excersize_ID).Count();
                }
            }   
        }
    }
}
