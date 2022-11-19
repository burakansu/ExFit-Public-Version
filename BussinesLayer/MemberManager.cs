using DatabaseLayer;
using ExFit.Data;
using FunctionLayer.Stats_Manager.Regression;
using Microsoft.EntityFrameworkCore;
using ObjectLayer;
using System.Linq;

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

            if (_User.Member_ID != 0)
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
        public List<ObjMember> GetMembers(int last = 0, int pasive = 0)
        {
            // Income ctor
            IncomeManager incomeManager = new IncomeManager(context);
            ObjIncome objIncome = new ObjIncome();
            int c = sQL.Value<int>("SELECT Count(*) FROM TBL_Members WHERE Registration_Date BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-01' AND '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-28' ");
            if (c > 0)
            {
                objIncome.Value = sQL.Value<int>("SELECT SUM(Price) AS 'summary' FROM TBL_Members WHERE Registration_Date BETWEEN '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-01' AND '" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-28' ");
                objIncome.Income_ID = sQL.Value<int>("SELECT Income_ID FROM TBL_Income WHERE WhichMonth=" + DateTime.Now.Month);
                incomeManager.SaveIncome(objIncome);
            }
            // Income ctor end


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
            }
            else 
            {
                context.Add(objMember);
            }
            context.SaveChanges();
        }
        public void SaveMemberMeazurements(ObjMemberMeazurement objMemberMeazurement)
        {
            objMemberMeazurement.Which_Month = context.MemberMeazurements.Count(x => x.Member_ID == objMemberMeazurement.Member_ID) + 1;
            context.MemberMeazurements.Update(objMemberMeazurement);
        }
        public void DeleteMemberMeazurements(int id)
        {
            context.MemberMeazurements.Remove(context.MemberMeazurements.Single(x => x.Meazurement_ID == id));
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
            List<int> Array = sQL.Get<int>("SELECT Weight FROM TBL_Members_Meazurements WHERE Member_ID=" + id);
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
}
