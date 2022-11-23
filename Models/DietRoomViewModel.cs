using ObjectLayer;
using System.Collections.Generic;

namespace ExFit.Models
{
    public class DietRoomViewModel
    {
        public ObjUser User { get; set; }
        public ObjCompany Company { get; set; }
        public ObjDiet Diet { get; set; }
        public List<ObjDiet> Diets { get; set; }
        public List<ObjTask> Tasks { get; set; }
        public List<ObjFood> Foods { get; set; }
        public ObjFood Food { get; set; }
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
