using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class ExcersizeManager
    {
        private Context context;
        public ExcersizeManager(Context _context)
        {
            context = _context;
        }
        public List<ObjExcersize> GetExcersizes(int id = 0, bool Special = false)
        {
            if (id != 0 || Special == true)
            {
                return context.Excersizes.Where(x => x.Excersize_ID == id).ToList();
            }
            else
            {
                return context.Excersizes.Where(x => x.Active == 1).ToList();
            }
        }
        public void DeleteExcersize(int id, bool Special = false)
        {
            if (Special == true)
            {
                ObjMember objMember = context.Members.Single(x => x.Member_ID == id);
                objMember.Excersize_ID = 0;
                context.Members.Update(objMember);
                context.SaveChanges();
            }
            else
            {
                ObjExcersize objExcersize = context.Excersizes.Single(x => x.Excersize_ID == id);
                objExcersize.Active = 0;
                context.Excersizes.Update(objExcersize);
                context.SaveChanges();
            }
        }
        public void AddDatabaseExcersize(ObjExcersize objExcersize)
        {
            objExcersize.Registration_Date = DateTime.Now;
            objExcersize.IMG = "";
            context.Add(objExcersize);
            context.SaveChanges();
        }
        public int[] Counts(int day, int id)
        {
            int[] ints = { 0, 0, 0, 0, 0, 0 };
            List<ObjPractice> list = context.Practices.Where(x => x.Day == day && x.Excersize_ID == id).ToList();
            foreach (var item in list)
            {
                switch (item.BodySection)
                {
                    case 1:
                        ints[0] = 1;
                        break;
                    case 2:
                        ints[1] = 1;
                        break;
                    case 3:
                        ints[2] = 1;
                        break;
                    case 4:
                        ints[3] = 1;
                        break;
                    case 5:
                        ints[4] = 1;
                        break;
                    case 6:
                        ints[5] = 1;
                        break;
                }
            }
            return ints;
        }
    }
}