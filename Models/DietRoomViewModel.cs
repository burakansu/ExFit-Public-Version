using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Models
{
    public class DietRoomViewModel
    {
        public ObjUser User { get; set; }
        public ObjDiet Diet { get; set; }
        public List<ObjDiet> Diets { get; set; }
        public List<ObjTask> Tasks { get; set; }
        public List<ObjFood> Foods { get; set; }
        public ObjFood Food { get; set; }
        public int _Day { get; set; }
    }
}
