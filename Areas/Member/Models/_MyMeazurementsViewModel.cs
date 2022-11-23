using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Areas.Member.Models
{
    public class _MyMeazurementsViewModel
    {
        public ObjMember _Member { get; set; }
        public ObjCompany Company { get; set; }
        public List<ObjMemberMeazurement> _MemberMeazurements { get; set; }
        public int[] _MemberMeazurementsArray { get; set; }
        public double[] _MemberWeightArray { get; set; }
    }
}
