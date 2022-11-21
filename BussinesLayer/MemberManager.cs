using DatabaseLayer;
using DatabaseLayer.ExFit_Database;
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

            DateTime MontFirstDay = DateTime.Now.MonthFirstDay();
            DateTime MonthLastDay = DateTime.Now.MonthLastDay();
            int c = context.Members.Where(x => x.Registration_Date >= MontFirstDay && x.Registration_Date <= MonthLastDay).Count();

            if (c > 0)
            {
                objIncome.Value = context.Members.Where(m => m.Registration_Date >= MontFirstDay && m.Registration_Date <= MonthLastDay).Sum(x => x.Price);

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
        public int CheckDate(String date)
        {
            try
            {
                DateTime dt = DateTime.Parse(date);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        public int[] GetThisYearRegystry()
        {
            DateTime now = DateTime.Now;
            int[] ints = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var objMembers = context.Members.Where(x => x.Registration_Date.Year == now.Year);
            foreach (var item in objMembers)
            {
                switch (item.Registration_Date.Month.ToString())
                {
                    case "1":
                        ints[0] += 1; 
                        break;
                    case "2":
                        ints[1] += 1;
                        break;
                    case "3":
                        ints[2] += 1;
                        break;
                    case "4":
                        ints[3] += 1;
                        break;
                    case "5":
                        ints[4] += 1;
                        break;
                    case "6":
                        ints[5] += 1;
                        break;
                    case "7":
                        ints[6] += 1;
                        break;
                    case "8":
                        ints[7] += 1;
                        break;
                    case "9":
                        ints[8] += 1;
                        break;
                    case "10":
                        ints[9] += 1;
                        break;
                    case "11":
                        ints[10] += 1;
                        break;
                    case "12":
                        ints[11] += 1;
                        break;
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
