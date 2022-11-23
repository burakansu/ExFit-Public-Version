using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.ExFit_Database
{
    public class TBL_Excersize
    {
        [Key]
        public int Excersize_ID { get; set; }
        public string Excersize_Name { get; set; }
        public string Author { get; set; }
        public int Active { get; set; }
        public DateTime Registration_Date { get; set; }
        public int Company_ID { get; set; }
    }
}
