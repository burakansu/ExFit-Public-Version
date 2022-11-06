using DatabaseLayer;
using DatabaseLayer.ExFit_Database;
using Microsoft.AspNetCore.Http;

namespace ObjectLayer
{
    public class ObjExcersize : TBL_Excersize
    {
        public IFormFile? FileExcersizeIMG { get; set; }

        //Sanal Tablo Kolonları

        public int Count 
        { 
            get
            {
                SQL sQL = new SQL();
                return sQL.Single<int>("SELECT COUNT(*) FROM TBL_Members WHERE Excersize_ID="+ this.Excersize_ID);
            }   
        }
    }
}
