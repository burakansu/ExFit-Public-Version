using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Models
{
    public class MembersViewModel
    {
        public ObjUser User { get; set; }
        public ObjCompany Company { get; set; }
        public ObjMember Member { get; set; }
        public int SelectedPackageID { get; set; }
        public int ExtraMonth { get; set; }
        public ObjExcersize MemberExcersize { get; set; }
        public ObjDiet MemberDiet { get; set; }
        public List<ObjMember> Members { get; set; }
        public ObjMemberMeazurement MemberMeazurement { get; set; }
        public List<ObjMemberMeazurement> MemberMeazurements { get; set; }
        public double[] MemberWeightArray { get; set; }
        public List<ObjTask> Tasks { get; set; }
        public List<ObjExcersize> ExcersizeArray { get; set; }
        public List<ObjDiet> DietArray { get; set; }
        public List<ObjPackage> PackageArray { get; set; }
    }
}
