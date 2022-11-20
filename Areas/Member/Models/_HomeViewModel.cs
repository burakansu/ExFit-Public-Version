using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Areas.Member.Models
{
    public class _HomeViewModel
    {
        public ObjMember _Member { get; set; }
        public ObjExcersize _MemberExcersize { get; set; }
        public ObjDiet _MemberDiet { get; set; }
        public int[] _MemberMeazurementsArray { get; set; }
        public double[] _MemberWeightArray { get; set; }
    }
}
