using DatabaseLayer;
using ExFit.Data;
using FunctionLayer.Stats_Manager.Regression;
using ObjectLayer;

namespace BussinesLayer
{
    public class MemberManager
    {
        private Context context;
        public MemberManager(Context _context)
        {
            context = _context;
        }
        SQL sQL = new SQL();
        public int Authorization(int Joined_Member_ID)
        {
            if (Joined_Member_ID != 0) { return 1; }
            else { return 0; }
        }
        public ObjMember CheckMemberEntering(ObjMember member)
        {
            ObjMember _User = context.Members.SingleOrDefault(x => x.Mail == member.Mail && x.Password == member.Password);

            if (_User != null)
            {
                return _User;
            }
            else
            {
                member.Member_ID = 0;
                return member;
            }
        }
        public int CountMembers()
        {
            return context.Members.Count();
        }
        public ObjMember GetMember(int id)
        {
            return context.Members.Single(x => x.Member_ID == id);
        }
        public void DeleteMember(int id, bool Del = false)
        {
            if (Del == true)
            {
                context.Members.Remove(context.Members.Single(x => x.Member_ID == id));
                context.SaveChanges();
            }
            else
            {
                ObjMember objMember = context.Members.Single(x => x.Member_ID == id);
                objMember.Block = 1;
                context.Members.Update(objMember);
                context.SaveChanges();
            }
        }
        public void ActiveMember(int id)
        {
            ObjMember objMember = context.Members.Single(x => x.Member_ID == id);
            objMember.Block = 0;
            context.Members.Update(objMember);
            context.SaveChanges();
        }
        public void SaveMember(ObjMember objMember)
        {
            if (objMember.Member_ID != 0)
            {
                context.Update(objMember);
                context.SaveChanges();
            }
            else
            {
                context.Add(objMember);
                context.SaveChanges();
            }
        }
        public void SaveMemberMeazurements(ObjMemberMeazurement objMemberMeazurement)
        {
            objMemberMeazurement.Which_Month = context.MemberMeazurements.Count(x => x.Member_ID == objMemberMeazurement.Member_ID) + 1;
            context.MemberMeazurements.Update(objMemberMeazurement);
            context.SaveChanges();
        }
        public void DeleteMemberMeazurements(int id)
        {
            context.MemberMeazurements.Remove(context.MemberMeazurements.Single(x => x.Meazurement_ID == id));
            context.SaveChanges();
        }
        public List<ObjMemberMeazurement> GetMemberMeazurements(int id)
        {
            return context.MemberMeazurements.Where(x => x.Member_ID == id).ToList();
        }
        public int GetIncome()
        {
            return context.Members.Sum(x => x.Price);
        }
        public double[] GetMemberWeightsArray(int id)
        {
            int[] Array = context.MemberMeazurements.Where(x => x.Member_ID == id).Select(x => x.Weight).ToArray();

            double[] Weights = new double[Array.Count()];
            double[] WeightsAndCurve = new double[12];
            if (Array.Count() > 0)
            {
                for (int i = 0; i < Array.Count(); i++)
                {
                    Weights[i] = Convert.ToDouble(Array[i]);
                    WeightsAndCurve[i] = Weights[i];
                }
                LinearCurve linearCurve = new LinearCurve(context);
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
        public List<ObjMember> GetMembers(int last = 0, int pasive = 0)
        {
            // Bir Değişiklik Varsa O Ayki Geliri Günceller Veya Yeni Ay a Geçildiyse Kaydı Oluşturur.
            ObjIncome objIncome = new ObjIncome();
            int c = sQL.Value<int>("SELECT Count(*) FROM TBL_Members WHERE Registration_Date BETWEEN '" + DateTime.Now.MonthFirstDay().ToString("yyyy-MM-dd") + "' AND '" + DateTime.Now.MonthLastDay().ToString("yyyy-MM-dd") + "' ");

            if (c > 0)
            {
                objIncome.Value = sQL.Value<int>("SELECT SUM(Price) AS 'summary' FROM TBL_Members WHERE Registration_Date BETWEEN '" + DateTime.Now.MonthFirstDay().ToString("yyyy-MM-dd") + "' AND '" + DateTime.Now.MonthLastDay().ToString("yyyy-MM-dd") + "' ");

                if (context.Incomes.Where(x => x.WhichMonth == DateTime.Now.Month).Count() > 0)
                {
                    objIncome.Income_ID = context.Incomes.Single(x => x.WhichMonth == DateTime.Now.Month).Income_ID;
                }
                new IncomeManager(context).SaveIncome(objIncome);
            }
            // end


            if (last == 1)
            {
                return context.Members.OrderByDescending(x => x.Member_ID).Take(3).ToList();
            }
            else if (pasive == 1)
            {
                return context.Members.Where(x => x.Block == 1).ToList();
            }
            else
            {
                return context.Members.Where(x => x.Block == 0).ToList();
            }
        }
        public int[] GetThisYearRegystry()
        {
            string year = DateTime.Now.ToString("yyyy");
            int[] ints = new int[12];
            for (int i = 1; i <= 12; i++)
            {
                String CommandPart = "'" + year + "-" + i + "-31'";
                int RealDate = sQL.Value<int>("SELECT ISDATE (" + CommandPart + ")");
                if (RealDate == 1)
                {
                    ints[i - 1] = sQL.Value<int>("SELECT COUNT(Name) FROM TBL_Members WHERE Registration_Date BETWEEN '" + year + "-" + i + "-01' AND " + CommandPart + " ");
                }
                else
                {
                    for (int j = 0; j < 6; j++)
                    {
                        int day = 30;
                        CommandPart = "'" + year + "-" + i + "-" + day + "'";
                        RealDate = sQL.Value<int>("SELECT ISDATE (" + CommandPart + ")");
                        if (RealDate == 1)
                        {
                            ints[i - 1] = sQL.Value<int>("SELECT COUNT(Name) FROM TBL_Members WHERE Registration_Date BETWEEN '" + year + "-" + i + "-01' AND " + CommandPart + " ");
                        }
                        day--;
                    }
                }
            }
            return ints;
        }
    }
    public static class ExtensionMethods
    {
        public static DateTime MonthFirstDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime MonthLastDay(this DateTime dt)
        {
            return dt.MonthFirstDay().AddMonths(1).AddDays(-1);
        }
    }
}
