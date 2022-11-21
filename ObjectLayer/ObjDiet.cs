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
        public int Count
        {
            get
            {
                return context.Members.Where(x => x.Diet_ID == this.Diet_ID).Count();
            }
        }
        [NotMapped]
        public int TotalCalorie
        {
            get
            {
                return context.Foods.Where(x => x.Diet_ID == this.Diet_ID).Sum(x => x.Calorie);
            }
        }
        [NotMapped]
        public int TotalProtein
        {
            get
            {
                return context.Foods.Where(x => x.Diet_ID == this.Diet_ID).Sum(x => x.Protein);
            }
        }
        [NotMapped]
        public int TotalFat
        {
            get
            {
                return context.Foods.Where(x => x.Diet_ID == this.Diet_ID).Sum(x => x.Fat);
            }
        }
        [NotMapped]
        public int TotalCarbonhidrat
        {
            get
            {
                return context.Foods.Where(x => x.Diet_ID == this.Diet_ID).Sum(x => x.Carbonhidrat);
            }
        }
    }
}
