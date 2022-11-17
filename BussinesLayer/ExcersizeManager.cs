using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class ExcersizeManager
    {
        SQL SQL = new SQL();
        public List<ObjExcersize> GetExcersizes(int id = 0, bool Special = false)
        {
            string Query = "SELECT * FROM TBL_Excersize WHERE Active = 1";
            if (id != 0 || Special == true) { Query = "SELECT * FROM TBL_Excersize WHERE Excersize_ID=" + id; };
            return SQL.Get<ObjExcersize>(Query);
        }
        public void DeleteExcersize(int id, bool Special = false)
        {
            string Query = "UPDATE TBL_Excersize SET Active = 0 WHERE Excersize_ID =" + id;
            if (Special == true) { Query = "UPDATE TBL_Members SET Excersize_ID = 0 WHERE Member_ID=" + id; }
            SQL.Run(Query);
        }
        public void AddDatabaseExcersize(ObjExcersize objExcersize)
        {
            objExcersize.Registration_Date = DateTime.Now;
            objExcersize.IMG = "";
            SQL.Run("INSERT INTO TBL_Excersize(IMG, Excersize_Name, Author, Registration_Date) VALUES (@IMG, @Excersize_Name, @Author, @Registration_Date)", objExcersize);
        }
        public int[] Counts (int day, int id)
        {
            int[] ints = { 0, 0, 0, 0, 0, 0 };
            List<ObjPractice> list = SQL.Get<ObjPractice>("SELECT * FROM TBL_Practice WHERE Day=" + day + " AND Excersize_ID=" + id);
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