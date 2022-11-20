using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.ExFit_Database
{
    public class TBL_Members_Meazurements
    {
        [Key]
        public int Meazurement_ID { get; set; }
        public int Member_ID { get; set; }
        public int Shoulder { get; set; }
        public int Chest { get; set; }
        public int Arm { get; set; }
        public int Leg { get; set; }
        public int Belly { get; set; }
        public int Weight { get; set; }
        public int Size { get; set; }
        public int Age { get; set; }
        public int Avarage_Asleep_Time { get; set; }
        public int Avarage_Calorie_Intake { get; set; }
        public int Which_Month { get; set; }
    }
}
