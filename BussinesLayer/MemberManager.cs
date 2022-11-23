using ExFit.Data;
using FunctionLayer.Stats_Manager.Regression;
using ObjectLayer;

namespace BussinesLayer
{
    public class MemberManager
    {
        private DateTime MontFirstDay = DateTime.Now.MonthFirstDay();
        private DateTime MonthLastDay = DateTime.Now.MonthLastDay();
        public int Authorization(int Joined_Member_ID)
        {
            if (Joined_Member_ID != 0) { return 1; }
            else { return 0; }
        }
        public ObjMember CheckMemberEntering(ObjMember member)
        {
            using (Context x = new Context())
            {
                ObjMember _User = x.Members.SingleOrDefault(x => x.Mail == member.Mail && x.Password == member.Password);

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
        }
        public int CountMembers(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Members.Where(x => x.Company_ID == Company_ID).Count();
            }
        }
        public ObjMember GetMember(int id)
        {
            using (Context x = new Context())
            {
                return x.Members.Single(x => x.Member_ID == id);
            }
        }
        public void DeleteMember(int id, bool Del = false)
        {
            using (Context x = new Context())
            {
                if (Del == true)
                {
                    x.Tasks.RemoveRange(x.Tasks.Where(x => x.Member_ID == id).ToList());
                    x.Members.Remove(x.Members.Single(x => x.Member_ID == id));
                }
                else
                {
                    ObjMember objMember = x.Members.Single(x => x.Member_ID == id);
                    objMember.Block = 1;
                    x.Members.Update(objMember);
                }
                x.SaveChanges();
            }
        }
        public void ActiveMember(int id)
        {
            using (Context x = new Context())
            {
                ObjMember objMember = x.Members.Single(x => x.Member_ID == id);
                objMember.Block = 0;
                x.Members.Update(objMember);
                x.SaveChanges();
            }
        }
        public void SaveMember(ObjMember objMember)
        {
            using (Context x = new Context())
            {
                if (objMember.Member_ID != 0)
                    x.Update(objMember);
                else
                {
                    if (objMember.Registration_Date == DateTime.MinValue)
                        objMember.Registration_Date = DateTime.Now;
                    if (objMember.Registration_Time == DateTime.MinValue)
                        objMember.Registration_Time = DateTime.Now.AddMonths(1);

                    objMember.Password = objMember.Phone.ToString() + objMember.Name;
                    x.Add(objMember);
                }
                x.SaveChanges();
            }
        }
        public void SaveMemberMeazurements(ObjMemberMeazurement objMemberMeazurement)
        {
            using (Context x = new Context())
            {
                objMemberMeazurement.Which_Month = x.MemberMeazurements.Count(x => x.Member_ID == objMemberMeazurement.Member_ID) + 1;
                x.MemberMeazurements.Update(objMemberMeazurement);
                x.SaveChanges();
            }
        }
        public void DeleteMemberMeazurements(int id)
        {
            using (Context x = new Context())
            {
                x.MemberMeazurements.Remove(x.MemberMeazurements.Single(x => x.Meazurement_ID == id));
                x.SaveChanges();
            }
        }
        public List<ObjMemberMeazurement> GetMemberMeazurements(int id)
        {
            using (Context x = new Context())
            {
                return x.MemberMeazurements.Where(x => x.Member_ID == id).ToList();
            }
        }
        public int GetIncome(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Members.Where(x => x.Company_ID == Company_ID).Sum(x => x.Price);
            }
        }
        public double[] GetMemberWeightsArray(int id)
        {
            using (Context x = new Context())
            {
                int[] Array = x.MemberMeazurements.Where(x => x.Member_ID == id).Select(x => x.Weight).ToArray();

                double[] Weights = new double[Array.Count()];
                double[] WeightsAndCurve = new double[12];
                if (Array.Count() > 0)
                {
                    for (int i = 0; i < Array.Count(); i++)
                    {
                        Weights[i] = Convert.ToDouble(Array[i]);
                        WeightsAndCurve[i] = Weights[i];
                    }
                    LinearCurve linearCurve = new LinearCurve(x);
                    if (Weights.Count() > 3)
                    {
                        double[] Lcurve = linearCurve.Curve(id, 12 - Weights.Count());

                        int counter = 0;
                        int Total = (Weights.Count() + Lcurve.Count());
                        for (int i = Weights.Count(); i < Total; i++)
                        {
                            WeightsAndCurve[i] = Lcurve[counter];
                            counter++;
                        }
                    }
                }
                return WeightsAndCurve;
            }
        }
        public List<ObjMember> GetMembers(int Company_ID, int last = 0, int pasive = 0)
        {
            using (Context x = new Context())
            {

                // Bir Değişiklik Varsa O Ayki Geliri Günceller Veya Yeni Ay a Geçildiyse Kaydı Oluşturur.
                ObjIncome objIncome = new ObjIncome();

                int c = x.Members.Where(x => x.Registration_Date >= MontFirstDay && x.Registration_Date <= MonthLastDay).Count();

                if (c > 0)
                {
                    objIncome.Value = x.Members.Where(m => m.Registration_Date >= MontFirstDay && m.Registration_Date <= MonthLastDay).Sum(x => x.Price);
                    objIncome.Company_ID = Company_ID;
                    if (x.Incomes.Where(x => x.WhichMonth == DateTime.Now.Month).Count() > 0)
                    {
                        objIncome.Income_ID = x.Incomes.Single(x => x.WhichMonth == DateTime.Now.Month).Income_ID;
                    }
                    new IncomeManager().SaveIncome(objIncome);
                }
                // end

                if (last == 1)
                {
                    return x.Members.Where(x => x.Company_ID == Company_ID).OrderByDescending(x => x.Member_ID).Take(3).ToList();
                }
                else if (pasive == 1)
                {
                    return x.Members.Where(x => x.Block == 1 && x.Company_ID == Company_ID).ToList();
                }
                else
                {
                    return x.Members.Where(x => x.Block == 0 && x.Company_ID == Company_ID).ToList();
                }
            }
        }
        public int[] GetThisYearRegystry(int Company_ID)
        {
            using (Context x = new Context())
            {
                DateTime now = DateTime.Now;
                int[] ints = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                var objMembers = x.Members.Where(x => x.Registration_Date.Year == now.Year && x.Company_ID == Company_ID);
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
