using DatabaseLayer;
using DatabaseLayer.ExFit_Database;

namespace ObjectLayer
{
    public class ObjDiet : TBL_Diet
    {
        //Sanal Tablo Kolonları

        public int Count
        {
            get
            {
                SQL sQL = new SQL();
                return sQL.Value<int>("SELECT COUNT(*) FROM TBL_Members WHERE Diet_ID=" + this.Diet_ID);
            }
        }
    }
}
