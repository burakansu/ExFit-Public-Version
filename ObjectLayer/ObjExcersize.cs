using DatabaseLayer;
using DatabaseLayer.ExFit_Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjExcersize : TBL_Excersize
    {
        //Sanal Tablo Kolonları

        [NotMapped]
        public int Count 
        { 
            get
            {
                return new SQL().Value<int>("SELECT COUNT(*) FROM TBL_Members WHERE Excersize_ID="+ this.Excersize_ID);
            }   
        }
    }
}
