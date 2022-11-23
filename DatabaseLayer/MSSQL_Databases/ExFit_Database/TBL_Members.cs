using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.ExFit_Database
{
    public class TBL_Members

    {
        [Key]
        public int Member_ID { get; set; }
        public string IMG { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Adress { get; set; }
        public DateTime Registration_Date { get; set; }
        public DateTime Registration_Time { get; set; }
        public int Price { get; set; }
        public int Block { get; set; }
        public string Health_Report { get; set; }
        public string Identity_Card { get; set; }
        public string Password { get; set; }
        public int Excersize_ID { get; set; }
        public int Diet_ID { get; set; }
        public int Company_ID { get; set; }
    }
}
