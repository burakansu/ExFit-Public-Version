using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Models
{
    public class HomeViewModel
    {
        public ObjUser User { get; set; }
        public List<ObjUser> Users { get; set; }
        public List<ObjTask> Tasks { get; set; }
        public ObjMember Member { get; set; }
        public List<ObjMember> Members { get; set; }
        public List<ObjMember> LastMembers { get; set; }
        public ObjMemberMeazurement MemberMeazurement { get; set; }
        public List<ObjMemberMeazurement> MemberMeazurements { get; set; }
        public int[] MemberMeazurementsArray { get; set; }
        public int Income { get; set; }
        public int ActiveMembersCount { get { return Members.Count; }}
        public int ActivePersonalCount { get { return Users.Count; }}
        public int TaskCount { get { return Tasks.Count; } }
        public int TodayTaskCount { get; set; }
        public int[] ThisYearRegistrys { get; set; }
        public int MemberCapasity 
        { 
            get
            {
                return (this.ActiveMembersCount * 100) / 250;
            }
        }
        public int PersonalCapasity
        {
            get
            {
                return (this.ActivePersonalCount * 100) / 20;
            }
        }
    }
}
