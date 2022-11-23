using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class ExcersizeManager
    {
        public List<ObjExcersize> GetExcersizes(int Company_ID, int id = 0, bool Special = false)
        {
            using (Context x = new Context())
            {
                if (id != 0 || Special == true)
                {
                    return x.Excersizes.Where(x => x.Excersize_ID == id).ToList();
                }
                else
                {
                    return x.Excersizes.Where(x => x.Active == 1 && x.Company_ID == Company_ID).ToList();
                }
            }
        }
        public void DeleteExcersize(int id, bool Special = false)
        {
            using (Context x = new Context())
            {
                if (Special == true)
                {
                    ObjMember objMember = x.Members.Single(x => x.Member_ID == id);
                    objMember.Excersize_ID = 0;
                    x.Members.Update(objMember);
                    x.SaveChanges();
                }
                else
                {
                    ObjExcersize objExcersize = x.Excersizes.Single(x => x.Excersize_ID == id);
                    objExcersize.Active = 0;
                    x.Excersizes.Update(objExcersize);
                    x.SaveChanges();
                }
            }
        }
        public void AddDatabaseExcersize(ObjExcersize objExcersize)
        {
            using (Context x = new Context())
            {
                objExcersize.Registration_Date = DateTime.Now;
                objExcersize.Active = 1;
                x.Add(objExcersize);
                x.SaveChanges();
            }
        }
        public int[] Counts(int day, int id)
        {
            using (Context x = new Context())
            {
                int[] ints = { 0, 0, 0, 0, 0, 0 };
                List<ObjPractice> list = x.Practices.Where(x => x.Day == day && x.Excersize_ID == id).ToList();
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
}