using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Areas.Member.Models
{
    public class _ContactViewModel
    {
        public ObjMember _Member { get; set; }
        public int[] _MemberMeazurementsArray { get; set; }
        public double[] _MemberWeightArray { get; set; }
    }
}
