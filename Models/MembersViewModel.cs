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
                ExcersizeManager excersize_Manager = new ExcersizeManager();
                List<ObjExcersize> ObjExcersizeslist = excersize_Manager.GetExcersizes();
                return ObjExcersizeslist;
            }
        }
        public List<ObjDiet> DietArray
        {
            get
            {

                DietManager dietManager = new DietManager();
                List<ObjDiet> objDiets = dietManager.GetDiets();
                return objDiets;
            }
        }
    }
}
