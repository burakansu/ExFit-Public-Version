using DatabaseLayer;
using DatabaseLayer.ExFit_Database;
using Microsoft.AspNetCore.Http;

namespace ObjectLayer
{
    public class ObjDiet : TBL_Diet
    {
        public IFormFile? FileDietIMG { get; set; }

        //Sanal Tablo Kolonları

        public int Count
        {
            get
            {
                SQL sQL = new SQL();
                return sQL.Single<int>("SELECT COUNT(*) FROM TBL_Members WHERE Diet_ID=" + this.Diet_ID);
            }
        }
    }
}
