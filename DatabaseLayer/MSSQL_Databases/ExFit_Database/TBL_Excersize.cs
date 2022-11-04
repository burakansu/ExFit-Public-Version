using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.ExFit_Database
{
    public class TBL_Excersize
    {
        [Key]
        public int Excersize_ID { get; set; }
        public string IMG { get; set; }
        public string Excersize_Name { get; set; }
        public string Author { get; set; }       
        public DateTime Registration_Date { get; set; }
    }
}
