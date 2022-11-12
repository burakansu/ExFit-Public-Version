using DatabaseLayer;
using DatabaseLayer.ExFit_Database;

namespace ObjectLayer
{
    public class ObjExcersize : TBL_Excersize
    {
        //Sanal Tablo Kolonları

        public int Count 
        { 
            get
            {
                SQL sQL = new SQL();
                return sQL.Value<int>("SELECT COUNT(*) FROM TBL_Members WHERE Excersize_ID="+ this.Excersize_ID);
            }   
        }
    }
}
