using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Areas.Member.Models
{
    public class _MyExcersizeViewModel
    {
        public ObjMember _Member { get; set; }
        public int[] _MemberMeazurementsArray { get; set; }
        public double[] _MemberWeightArray { get; set; }
        public List<ObjMemberMeazurement> _MemberMeazurements { get; set; }
        public ObjExcersize _MemberExcersize { get; set; }
        public List<ObjPractice> Practices { get; set; }
        public ObjPractice Practice { get; set; }
        public int _EDay { get; set; }
        public List<ObjExcersize> _ExcersizeArray { get; set; }

        public int[] C1 { get; set; }
        public int[] C2 { get; set; }
        public int[] C3 { get; set; }
        public int[] C4 { get; set; }
        public int[] C5 { get; set; }
        public int[] C6 { get; set; }
        public int[] C7 { get; set; }
    }
}
