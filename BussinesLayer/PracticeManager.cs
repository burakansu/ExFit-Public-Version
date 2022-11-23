using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class PracticeManager
    {
        public List<ObjPractice> GetPractices()
        {
            using (Context x = new Context())
            {
                return x.Practices.OrderBy(x => x.Practice_ID).ToList();
            }
        }
        public void DeletePractice(int id)
        {
            using (Context x = new Context())
            {
                x.Practices.Remove(x.Practices.Single(x => x.Practice_ID == id));
                x.SaveChanges();
            }
        }
        public void AddDatabasePractice(ObjPractice objPractice)
        {
            using (Context x = new Context())
            {
                x.Add(objPractice);
                x.SaveChanges();
            }
        }
    }
}
