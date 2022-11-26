using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Models
{
    public class PackageViewModel
    {
        public ObjPackage Package { get; set; }
        public List<ObjPackage> Packages { get; set; }
        public ObjUser User { get; set; }
        public ObjCompany Company { get; set; }
    }
}
