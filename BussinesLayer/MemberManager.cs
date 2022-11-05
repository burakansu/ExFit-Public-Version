using DatabaseLayer;
using FunctionLayer.Stats_Manager.Regression;
using ObjectLayer;
using System.Data;
using ObjMember = ObjectLayer.ObjMember;

namespace BussinesLayer
{
    public class MemberManager
    {
        SQL sQL = new SQL();
        public int CountMembers()
        {
            return sQL.GetSingle<int>("SELECT COUNT(*) FROM TBL_Members");
        }
        public List<ObjMember> GetMembers(int last = 0, int pasive = 0)
        {
            string Command = "SELECT * FROM TBL_Members WHERE Block = 0";
            if (last == 1) { Command = "SELECT top 3 * FROM TBL_Members ORDER BY Member_ID DESC"; }
            else if (pasive == 1) { Command = "SELECT * FROM TBL_Members WHERE Block = 1"; }
            return sQL.Get<ObjMember>(Command);
        }
        public ObjMember GetMember(int id)
        {
                return sQL.GetSingle<ObjMember>("SELECT * FROM TBL_Members WHERE Member_ID=" + id);
        }
        public void DeleteMember(int id, bool Del = false)
        {
            string Command = "UPDATE TBL_Members SET Block=1 WHERE Member_ID=";
            if (Del == true) { Command = "DELETE TBL_Members WHERE Member_ID="; }
            sQL.Execute(Command + id);
        }
        public void ActiveMember(int id)
        {
            sQL.Execute("UPDATE TBL_Members SET Block=0 WHERE Member_ID=" + id);
        }
        public void SaveMember(ObjMember objMember)
        {
            string Command = "INSERT INTO TBL_Members([Name],Surname,[Phone],[Mail],Adress,Registration_Time,[Block],Registration_Date,[IMG],[Price],Excersize_ID,Diet_ID,Health_Report,Identity_Card) VALUES (@Name,@Surname,@Phone,@Mail,@Adress,@Registration_Time,@Block,@Registration_Date,@IMG,@Price,@Excersize_ID,@Diet_ID,@Identity_Card,@Health_Report) SELECT CAST(scope_identity() AS int)";
            if (objMember.Member_ID != 0)
            {
                Command = "UPDATE TBL_Members SET [Name]=@Name,Surname=@Surname,[Phone]=@Phone,[Mail]=@Mail,Adress=@Adress,Registration_Time=@Registration_Time,[Block]=@Block,Registration_Date=@Registration_Date,[IMG]=@IMG,[Price]=@Price,Excersize_ID=@Excersize_ID,Diet_ID=@Diet_ID,Health_Report=@Health_Report,Identity_Card=@Identity_Card WHERE Member_ID=" + objMember.Member_ID;
            }
            sQL.Execute(Command, objMember);
        }
        public void SaveMemberMeazurements(ObjMemberMeazurement objMemberMeazurement)
        {
            int Count = sQL.GetSingle<int>("SELECT COUNT(*) FROM TBL_Members_Meazurements WHERE Member_ID=" + objMemberMeazurement.Member_ID);
            objMemberMeazurement.Which_Month = Count + 1;
            sQL.Execute("INSERT INTO TBL_Members_Meazurements (Member_ID,Shoulder,Chest,Arm,Belly,Leg,Weight,Age,Size,Which_Month,Avarage_Asleep_Time,Avarage_Calorie_Intake) VALUES (@Member_ID,@Shoulder,@Chest,@Arm,@Belly,@Leg,@Weight,@Age,@Size,@Which_Month,@Avarage_Asleep_Time,@Avarage_Calorie_Intake) ", objMemberMeazurement);
        }
        public void DeleteMemberMeazurements(int id)
        {
            sQL.Execute("DELETE FROM TBL_Members_Meazurements WHERE Meazurement_ID=" + id);
        }
        public List<ObjMemberMeazurement> GetMemberMeazurements(int id)
        {
            return sQL.Get<ObjMemberMeazurement>("SELECT * FROM TBL_Members_Meazurements WHERE Member_ID=" + id);
        }
        public int GetIncome()
        {
            return sQL.GetSingle<int>("SELECT SUM(Price) FROM TBL_Members");
        }
        public double[] GetMemberWeightsArray(int id)
        {
            var Array = sQL.Get<int[]>("SELECT Weight FROM TBL_Members_Meazurements WHERE Member_ID=" + id);
            double[] Weights = new double[Array.Count()];
            double[] WeightsAndCurve = new double[12];
            if (Array.Count() > 0)
            {
                for (int i = 0; i < Array.Count(); i++)
                {
                    Weights[i] = Convert.ToDouble(Array[i]);
                    WeightsAndCurve[i] = Weights[i];
                }
                LinearCurve linearCurve = new LinearCurve();
                if (Weights.Count() > 3)
                {
                    double[] Lcurve = linearCurve.Curve(id, 12 - Weights.Count());

                    int counter = 0;
                    int a = (Weights.Count() + Lcurve.Count());
                    for (int i = Weights.Count(); i < a; i++)
                    {
                        WeightsAndCurve[i] = Lcurve[counter];
                        counter++;
                    }
                }
            }
            return WeightsAndCurve;
        }
        public int[] GetThisYearRegystry()
        {
            string year = DateTime.Now.ToString("yyyy");
            int[] ints = new int[12];
            for (int i = 1; i <= 12; i++)
            {
                String CommandPart = "'" + year + "-" + i + "-31'";
                int RealDate = sQL.GetSingle<int>("SELECT ISDATE (" + CommandPart + ")");
                if (RealDate == 1)
                {
                    ints[i - 1] = sQL.GetSingle<int>("SELECT COUNT(Name) FROM TBL_Members WHERE Registration_Date BETWEEN '" + year + "-" + i + "-01' AND " + CommandPart + " ");
                }
                else
                {
                    for (int j = 0; j < 6; j++)
                    {
                        int day = 30;
                        CommandPart = "'" + year + "-" + i + "-" + day + "'";
                        RealDate = sQL.GetSingle<int>("SELECT ISDATE (" + CommandPart + ")");
                        if (RealDate == 1)
                        {
                            ints[i - 1] = sQL.GetSingle<int>("SELECT COUNT(Name) FROM TBL_Members WHERE Registration_Date BETWEEN '" + year + "-" + i + "-01' AND " + CommandPart + " ");
                        }
                        day--;
                    }
                }
            }
            return ints;
        }
    }
}
