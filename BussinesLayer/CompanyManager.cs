using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class CompanyManager
    {
        public ObjCompany GetCompany(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Companies.Single(x => x.Company_ID == Company_ID);
            }
        }
        public void SaveCompany(ObjCompany objCompany, ObjUser objUser)
        {
            using (Context x = new Context())
            {
                if (objCompany.Company_ID == 0)
                {
                    objCompany.Logo = "-";
                    x.Companies.Add(objCompany);
                    x.SaveChanges();
                    int id = x.Companies.Max(x => x.Company_ID);
                    objUser.Company_ID = id;
                    objUser.Position = "Yönetici";
                    objUser.Type = 1;
                    objUser.IMG = "/Personal/AvatarNull.png";
                    x.Users.Add(objUser);
                }
                else
                    x.Companies.Update(objCompany);
                x.SaveChanges();
            }
        }
    }
}
