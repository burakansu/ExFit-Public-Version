using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Areas.Member.Models
{
    public class _MyDietViewModel
    {
        public ObjMember _Member { get; set; }
        public int[] _MemberMeazurementsArray { get; set; }
        public double[] _MemberWeightArray { get; set; }
        public ObjDiet _MemberDiet { get; set; }
        public List<ObjFood> Foods { get; set; }
        public ObjFood Food { get; set; }
        public int _DDay { get; set; }
        public List<ObjDiet> _DietArray { get; set; }
        public int[] D1 { get; set; }
        public int[] D2 { get; set; }
        public int[] D3 { get; set; }
        public int[] D4 { get; set; }
        public int[] D5 { get; set; }
        public int[] D6 { get; set; }
        public int[] D7 { get; set; }
    }
}
