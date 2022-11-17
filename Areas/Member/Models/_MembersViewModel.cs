using BussinesLayer;
using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Areas.Member.Models
{
    public class _MembersViewModel
    {
        public ObjMember _Member { get; set; }
        public ObjExcersize _MemberExcersize { get; set; }
        public ObjDiet _MemberDiet { get; set; }
        public List<ObjMemberMeazurement> _MemberMeazurements { get; set; }
        public int[] _MemberMeazurementsArray { get; set; }
        public double[] _MemberWeightArray { get; set; }
        public List<ObjFood> Foods { get; set; }
        public ObjFood Food { get; set; }
        public int _DDay { get; set; }
        public List<ObjPractice> Practices { get; set; }
        public ObjPractice Practice { get; set; }
        public int _EDay { get; set; }
        public List<ObjExcersize> _ExcersizeArray
        {
            get
            {
                ExcersizeManager excersize_Manager = new ExcersizeManager();
                List<ObjExcersize> ObjExcersizeslist = excersize_Manager.GetExcersizes();
                return ObjExcersizeslist;
            }
        }
        public List<ObjDiet> _DietArray
        {
            get
            {

                DietManager dietManager = new DietManager();
                List<ObjDiet> objDiets = dietManager.GetDiets();
                return objDiets;
            }
        }
        public int[] C1 { get; set; }
        public int[] C2 { get; set; }
        public int[] C3 { get; set; }
        public int[] C4 { get; set; }
        public int[] C5 { get; set; }
        public int[] C6 { get; set; }
        public int[] C7 { get; set; }
        public int[] D1 { get; set; }
        public int[] D2 { get; set; }
        public int[] D3 { get; set; }
        public int[] D4 { get; set; }
        public int[] D5 { get; set; }
        public int[] D6 { get; set; }
        public int[] D7 { get; set; }

    }
}
