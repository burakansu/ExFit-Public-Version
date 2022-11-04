using Dapper;
using DatabaseLayer;
using FunctionLayer.Stats_Manager.Regression;
using ObjectLayer;
using System.Data;
using System.Data.SqlClient;
using ObjMember = ObjectLayer.ObjMember;

namespace BussinesLayer
{
    public class MemberManager
    {
        SQL sQL = new SQL();
        public int CountMembers()
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql)) 
            {
                return conn.QuerySingle<int>("SELECT COUNT(*) FROM TBL_Members"); 
            }
        }
        public List<ObjMember> GetMembers(int last = 0, int pasive = 0)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                string Query = "SELECT * FROM TBL_Members WHERE Block = 0";
                if (last == 1) { Query = "SELECT top 3 * FROM TBL_Members ORDER BY Member_ID DESC"; }
                else if (pasive == 1) { Query = "SELECT * FROM TBL_Members WHERE Block = 1"; }
                return conn.Query<ObjMember>(Query).ToList();
            }
        }
        public ObjMember GetMember(int id)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql)) 
            {
                return conn.QuerySingle<ObjMember>("SELECT * FROM TBL_Members WHERE Member_ID=" + id); 
            }
        }
        public void DeleteMember(int id, bool Del = false)
        {
            string Command = "UPDATE TBL_Members SET Block=1 WHERE Member_ID=";
            if (Del == true) { Command = "DELETE TBL_Members WHERE Member_ID="; }
            using (IDbConnection conn = new SqlConnection(SQL.Sql)) 
            { 
                conn.Execute(Command + id); 
            }
        }
        public void ActiveMember(int id)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql)) 
            { 
                conn.Execute("UPDATE TBL_Members SET Block=0 WHERE Member_ID=" + id);
            }
        }
        public void SaveMember(ObjMember objMember)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                string command = "INSERT INTO TBL_Members([Name],Surname,[Phone],[Mail],Adress,Registration_Time,[Block],Registration_Date,[IMG],[Price],Excersize_ID,Diet_ID,Health_Report,Identity_Card) VALUES (@Name,@Surname,@Phone,@Mail,@Adress,@Registration_Time,@Block,@Registration_Date,@IMG,@Price,@Excersize_ID,@Diet_ID,@Identity_Card,@Health_Report) SELECT CAST(scope_identity() AS int)";
                if (objMember.Member_ID != 0)
                {
                    command = "UPDATE TBL_Members SET [Name]=@Name,Surname=@Surname,[Phone]=@Phone,[Mail]=@Mail,Adress=@Adress,Registration_Time=@Registration_Time,[Block]=@Block,Registration_Date=@Registration_Date,[IMG]=@IMG,[Price]=@Price,Excersize_ID=@Excersize_ID,Diet_ID=@Diet_ID,Health_Report=@Health_Report,Identity_Card=@Identity_Card WHERE Member_ID=" + objMember.Member_ID;
                }
                conn.Execute(command, objMember);
            }
        }
        public void SaveMemberMeazurements(ObjMemberMeazurement objMemberMeazurement)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql))
            {
                int Count = sQL.Count_Database("SELECT COUNT(*) FROM TBL_Members_Meazurements WHERE Member_ID=" + objMemberMeazurement.Member_ID);
                objMemberMeazurement.Which_Month = Count + 1;
                conn.Execute("INSERT INTO TBL_Members_Meazurements (Member_ID,Shoulder,Chest,Arm,Belly,Leg,Weight,Age,Size,Which_Month,Avarage_Asleep_Time,Avarage_Calorie_Intake) VALUES (@Member_ID,@Shoulder,@Chest,@Arm,@Belly,@Leg,@Weight,@Age,@Size,@Which_Month,@Avarage_Asleep_Time,@Avarage_Calorie_Intake) ", objMemberMeazurement);
            }
        }
        public void DeleteMemberMeazurements(int id)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql)) 
            { 
                conn.Execute("DELETE FROM TBL_Members_Meazurements WHERE Meazurement_ID=" + id); 
            }
        }
        public List<ObjMemberMeazurement> GetMemberMeazurements(int id)
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql)) 
            { 
                return conn.Query<ObjMemberMeazurement>("SELECT * FROM TBL_Members_Meazurements WHERE Member_ID=" + id).ToList();
            }
        }
        public int GetIncome()
        {
            using (IDbConnection conn = new SqlConnection(SQL.Sql)) 
            { 
                return conn.QuerySingle<int>("SELECT SUM(Price) FROM TBL_Members"); 
            }
        }
        public double[] GetMemberWeightsArray(int id)
        {
            DataTable TBL = sQL.GetDataTable("SELECT Weight FROM TBL_Members_Meazurements WHERE Member_ID=" + id);
            double[] Weights = new double[TBL.Rows.Count];
            double[] WeightsAndCurve = new double[12];
            if (TBL.Rows.Count > 0)
            {
                for (int i = 0; i < TBL.Rows.Count; i++)
                {
                    Weights[i] = (Int32)TBL.Rows[i]["Weight"];
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
                int RealDate = sQL.Count_Database("SELECT ISDATE (" + CommandPart + ")");
                if (RealDate == 1)
                {
                    ints[i - 1] = sQL.GetDataTable("SELECT Name FROM TBL_Members WHERE Registration_Date BETWEEN '" + year + "-" + i + "-01' AND " + CommandPart + " ").Rows.Count;
                }
                else
                {
                    for (int j = 0; j < 6; j++)
                    {
                        int day = 30;
                        CommandPart = "'" + year + "-" + i + "-" + day + "'";
                        RealDate = sQL.Count_Database("SELECT ISDATE (" + CommandPart + ")");
                        if (RealDate == 1)
                        {
                            ints[i - 1] = sQL.GetDataTable("SELECT Name FROM TBL_Members WHERE Registration_Date BETWEEN '" + year + "-" + i + "-01' AND " + CommandPart + " ").Rows.Count;
                        }
                        day--;
                    }
                }
            }
            return ints;
        }
    }
}
