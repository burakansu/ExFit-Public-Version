using ExFit.Data;
using FunctionLayer.Stats_Manager.Regression;
using ObjectLayer;

namespace BussinesLayer
{
    public class MemberManager
    {
        public void SendSmsMember(ObjMember objMember, string SmsText)
        {
            using (Context x = new Context())
            {
                ObjCompany objCompany = x.Companies.Single(x => x.Company_ID == objMember.Company_ID);
                if (objCompany.Package_Type > 0)
                {
                    List<ObjMember> objMembers = new List<ObjMember>();
                    objMembers.Add(objMember);
                    new SmsManager().SmsSender(objCompany.Name, SmsText, objMembers);
                }
            }
        }
        public int Authorization(int Joined_Member_ID)
        {
            if (Joined_Member_ID != 0)
                return 1;
            else
                return 0;
        }
        public ObjMember CheckMemberEntering(ObjMember member)
        {
            using (Context x = new Context())
            {
                ObjMember _User = x.Members.SingleOrDefault(x => x.Mail == member.Mail && x.Password == member.Password);

                if (_User != null)
                    return _User;
                member.Member_ID = 0;
                return member;
            }
        }
        public int CheckEmailAndPhone(string Email, string Phone)
        {
            using (Context x = new Context())
            {
                return x.Members.Where(x => x.Mail == Email || x.Phone == Phone).Count();
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
                    objMember.Package_ID = 0;
                    objMember.Gift = 0;
                    objMember.Price = 0;
                    x.Members.Update(objMember);
                    SendSmsMember(objMember, "Üyeliğiniz " + DateTime.Now.ToShortDateString() + " itibarıyla Pasif Durumdadır. Daha fazla bilgi için Exfit Yönetim Panelinden Üye Girişi Yapabilirsiniz.");
                }
                x.SaveChanges();
            }
        }
        public int SaveMember(ObjMember objMember)
        {
            int Count = 0;
            ObjPackage objPackage = new ObjPackage();
            using (Context x = new Context())
            {
                if (objMember.Package_ID != 0)
                    objPackage = x.Packages.Single(x => x.Package_ID == objMember.Package_ID);
                else
                    objPackage = new ObjPackage { Month = 0, Price = 0 };

                if (objMember.Member_ID != 0)
                {
                    objMember.Registration_Time = objMember.Registration_Date.AddMonths(objPackage.Month + objMember.Gift);
                    if (objMember.Block == 1)
                        objMember.Registration_Date = DateTime.Now;
                    objMember.Block = 0;
                    x.Update(objMember);
                }
                else
                {
                    Count = new MemberManager().CheckEmailAndPhone(objMember.Mail, objMember.Phone);
                    if (Count == 0)
                    {
                        objMember.Registration_Date = DateTime.Now;
                        objMember.Registration_Time = DateTime.Now.AddMonths(objPackage.Month + objMember.Gift);
                        objMember.Password = objMember.Phone.ToString() + objMember.Name;
                        objMember.Block = 1;
                        x.Add(objMember);
                        SendSmsMember(objMember, "Hoşgeldin! Üyeliğiniz " + DateTime.Now.ToShortDateString() + " itibarıyla Başlamıştır. Kalan Gününüz: " + objMember.RemainingDay + " Daha fazla bilgi için Exfit İle Üye Girişi Yapabilirsiniz. Mail:" + objMember.Mail + " Şifre: " + objMember.Password);
                    }
                }
                x.SaveChanges();
            }
            return Count;
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
                return x.Incomes.Where(x => x.Company_ID == Company_ID && x.Year == DateTime.Now.Year).Sum(x => x.Value);
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
                if (last == 1)
                    return x.Members.Where(x => x.Company_ID == Company_ID).OrderByDescending(x => x.Registration_Date).Take(3).ToList();
                else if (pasive == 1)
                    return x.Members.Where(x => x.Block == 1 && x.Company_ID == Company_ID).ToList();
                else
                    return x.Members.Where(x => x.Block == 0 && x.Company_ID == Company_ID).ToList();
            }
        }
        public int[] GetThisYearRegystry(int Company_ID)
        {
            using (Context x = new Context())
            {
                DateTime now = DateTime.Now;
                int[] Months = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                var objMembers = x.Members.Where(x => x.Registration_Date.Year == now.Year && x.Company_ID == Company_ID);
                foreach (var item in objMembers)
                {
                    switch (item.Registration_Date.Month.ToString())
                    {
                        case "1":
                            Months[0] += 1;
                            break;
                        case "2":
                            Months[1] += 1;
                            break;
                        case "3":
                            Months[2] += 1;
                            break;
                        case "4":
                            Months[3] += 1;
                            break;
                        case "5":
                            Months[4] += 1;
                            break;
                        case "6":
                            Months[5] += 1;
                            break;
                        case "7":
                            Months[6] += 1;
                            break;
                        case "8":
                            Months[7] += 1;
                            break;
                        case "9":
                            Months[8] += 1;
                            break;
                        case "10":
                            Months[9] += 1;
                            break;
                        case "11":
                            Months[10] += 1;
                            break;
                        case "12":
                            Months[11] += 1;
                            break;
                    }
                }
                return Months;
            }
        }
        public void PasiveMemberAuto()
        {
            using (Context x = new Context())
            {
                // Kontratı Dolan Üyeleri Pasifleştirir
                List<ObjMember> objMembers = x.Members.Where(x => x.Registration_Time < DateTime.Now).ToList();
                foreach (var item in objMembers)
                {
                    DeleteMember(item.Member_ID);
                }
            }
        }
    }
}
