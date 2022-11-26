﻿using ExFit.Data;
using ObjectLayer;

namespace BussinesLayer
{
    public class PackageManager
    {
        public List<ObjPackage> GetPackages(int Company_ID)
        {
            using (Context x = new Context())
            {
                return x.Packages.Where(x => x.Company_ID == Company_ID).ToList();
            }
        }
        public ObjPackage GetPackage(int id)
        {
            using (Context x = new Context())
            {
                return x.Packages.Single(x => x.Package_ID == id);
            }
        }
        public void DeletePackage(int id)
        {
            using (Context x = new Context())
            {
                x.Packages.Remove(x.Packages.Single(x => x.Package_ID == id));
                x.SaveChanges();
            }
        }
        public void AddDatabasePackage(ObjPackage objPackage)
        {
            using (Context x = new Context())
            {
                x.Packages.Add(objPackage);
                x.SaveChanges();
            }
        }
    }
}
