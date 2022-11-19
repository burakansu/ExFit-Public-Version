using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class PracticeManager
    {
        private Context context;
        public PracticeManager(Context _context)
        {
            context = _context;
        }
        public List<ObjPractice> GetPractices()
        {
            return context.Practices.OrderBy(x => x.Practice_ID).ToList();
        }
        public void DeletePractice(int id)
        {
            context.Practices.Remove(context.Practices.Single(x => x.Practice_ID == id));
            context.SaveChanges();
        }
        public void AddDatabasePractice(ObjPractice objPractice)
        {
            context.Add(objPractice);
            context.SaveChanges();
        }
    }
}
