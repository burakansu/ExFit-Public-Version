using DatabaseLayer.ExFit_Database;
using ExFit.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjDiet : TBL_Diet
    {
        private Context context;
        public ObjDiet(Context _context)
        {
            context = _context;
        }

        // Sanal Tablo Kolonları

        [NotMapped]
        public int Count // Diyetin Üyeler Tarafından Kullanım Sayısı
        {
            get
            {
                return context.Members.Where(x => x.Diet_ID == this.Diet_ID).Count();
            }
        }
        [NotMapped]
        public int TotalCalorie // Diyetteki Toplam Kalori
        {
            get
            {
                return context.Foods.Where(x => x.Diet_ID == this.Diet_ID).Sum(x => x.Calorie);
            }
        }
        [NotMapped]
        public int TotalProtein // Diyetteki Toplam Protein
        {
            get
            {
                return context.Foods.Where(x => x.Diet_ID == this.Diet_ID).Sum(x => x.Protein);
            }
        }
        [NotMapped]
        public int TotalFat //Diyetteki Toplam Yağ
        {
            get
            {
                return context.Foods.Where(x => x.Diet_ID == this.Diet_ID).Sum(x => x.Fat);
            }
        }
        [NotMapped]
        public int TotalCarbonhidrat // Diyetteki Toplam Karbonhidrat
        {
            get
            {
                return context.Foods.Where(x => x.Diet_ID == this.Diet_ID).Sum(x => x.Carbonhidrat);
            }
        }
    }
}
