using DatabaseLayer;
using ObjectLayer;

namespace BussinesLayer
{
    public class PracticeManager
    {
        SQL SQL = new SQL();
        public List<ObjPractice> GetPractices()
        {
            return SQL.Get<ObjPractice>("SELECT * FROM TBL_Practice");
        }
        public void DeletePractice(int id)
        {
            SQL.Run("DELETE TBL_Practice WHERE Practice_ID="+ id);
        }
        public void AddDatabasePractice(ObjPractice objPractice)
        {
            SQL.Run("INSERT INTO TBL_Practice (BodySection, Name, SetCount, Repeat, CoolDownTime, Note, Day, Excersize_ID) VALUES (@BodySection, @Name, @SetCount, @Repeat, @CoolDownTime, @Note, @Day, @Excersize_ID) ", objPractice);
        }
    }
}
