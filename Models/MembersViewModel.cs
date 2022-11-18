using BussinesLayer;
using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Models
{
    public class MembersViewModel
    {
        public ObjUser User { get; set; }
        public ObjMember Member { get; set; }
        public ObjExcersize MemberExcersize { get; set; }
        public ObjDiet MemberDiet { get; set; }
        public List<ObjMember> Members { get; set; }
        public ObjMemberMeazurement MemberMeazurement { get; set; }
        public List<ObjMemberMeazurement> MemberMeazurements { get; set; }
        public double[] MemberWeightArray { get; set; }
        public List<ObjTask> Tasks { get; set; }

        public List<ObjExcersize> ExcersizeArray
        {
            get
            {
                return new ExcersizeManager().GetExcersizes();
            }
        }
        public List<ObjDiet> DietArray
        {
            get
            {
                return new DietManager().GetDiets();
            }
        }
    }
}
