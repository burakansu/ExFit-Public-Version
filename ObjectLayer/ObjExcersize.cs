using DatabaseLayer;
using DatabaseLayer.ExFit_Database;
using Microsoft.AspNetCore.Http;

namespace ObjectLayer
{
    public class ObjExcersize : TBL_Excersize
    {
        public IFormFile? FileExcersizeIMG { get; set; }
        public int Count 
        { 
            get
            {
                SQL sQL = new SQL();
                return sQL.Count_Database("SELECT COUNT(*) FROM TBL_Members WHERE Excersize_ID="+ this.Excersize_ID);
            }   
        }
    }
}
