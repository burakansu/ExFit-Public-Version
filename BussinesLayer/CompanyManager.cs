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
        public List<ObjCompany> GetCompanies()
        {
            using (Context x = new Context())
            {
                return x.Companies.ToList();
            }
        }
        public void SaveCompany(ObjCompany objCompany)
        {
            using (Context x = new Context())
            {
                if (objCompany.Company_ID == 0)
                {
                    objCompany.Logo = "-";
                    objCompany.Registration_Date = DateTime.Now;
                    objCompany.Registration_Time = DateTime.Now.AddYears(45);
                    x.Companies.Add(objCompany);
                    x.SaveChanges();
                }
                else
                    x.Companies.Update(objCompany);
                x.SaveChanges();
            }
        }
    }
}
