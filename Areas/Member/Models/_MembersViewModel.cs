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

        // ViewModel in içine gömülü sanal property ler.

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
    }
}
