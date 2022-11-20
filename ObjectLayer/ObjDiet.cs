using DatabaseLayer;
using DatabaseLayer.ExFit_Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjDiet : TBL_Diet
    {
        //Sanal Tablo Kolonları

        [NotMapped]
        public int Count
        {
            get
            {
                return new SQL().Value<int>("SELECT COUNT(*) FROM TBL_Members WHERE Diet_ID=" + this.Diet_ID);
            }
        }
        [NotMapped]
        public int TotalCalorie
        {
            get
            {
                return new SQL().Value<int>("SELECT ISNULL((SELECT SUM(Calorie) FROM TBL_Food WHERE Diet_ID="+ this.Diet_ID +"), 0) AS i");
            }
        }
        [NotMapped]
        public int TotalProtein
        {
            get
            {
                return new SQL().Value<int>("SELECT ISNULL((SELECT SUM(Protein) FROM TBL_Food WHERE Diet_ID=" + this.Diet_ID + "), 0) AS i");
            }
        }
        [NotMapped]
        public int TotalFat
        {
            get
            {
                return new SQL().Value<int>("SELECT ISNULL((SELECT SUM(Fat) FROM TBL_Food WHERE Diet_ID=" + this.Diet_ID + "), 0) AS i");
            }
        }
        [NotMapped]
        public int TotalCarbonhidrat
        {
            get
            {
                return new SQL().Value<int>("SELECT ISNULL((SELECT SUM(Carbonhidrat) FROM TBL_Food WHERE Diet_ID=" + this.Diet_ID + "), 0) AS i");
            }
        }
    }
}
