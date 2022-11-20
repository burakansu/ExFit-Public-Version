using ObjectLayer;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExFit.Models
{
    public class ExcersizeRoomViewModel
    {
        public ObjUser User { get; set; }
        public ObjExcersize Excersize { get; set; }
        public List<ObjExcersize> Excersizes { get; set; }
        public List<ObjTask> Tasks { get; set; }
        public List<ObjPractice> Practices { get; set; }
        public ObjPractice Practice { get; set; }
        public int _Day { get; set; }
        public int[] C1 { get; set; }
        public int[] C2 { get; set; }
        public int[] C3 { get; set; }
        public int[] C4 { get; set; }
        public int[] C5 { get; set; }
        public int[] C6 { get; set; }
        public int[] C7 { get; set; }
    }
}
