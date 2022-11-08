using ObjectLayer;
using System.Collections.Generic;

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
    }
}
