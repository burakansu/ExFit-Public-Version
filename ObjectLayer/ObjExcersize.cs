using DatabaseLayer.ExFit_Database;
using ExFit.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjExcersize : TBL_Excersize
    {
        private Context context;
        public ObjExcersize(Context _context)
        {
            context = _context;
        }
        //Sanal Tablo Kolonları

        [NotMapped]
        public int Count 
        { 
            get
            {
                return context.Excersizes.Where(x => x.Excersize_ID == this.Excersize_ID).Count();
            }   
        }
    }
}
