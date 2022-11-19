using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.ExFit_Database
{
    public class TBL_Diet
    {
        [Key]
        public int Diet_ID { get; set; }
        public string IMG { get; set; }
        public string Diet_Name { get; set; }
        public string Author { get; set; }
        public int Active { get; set; }
        public DateTime Registration_Date { get; set; }
    }

}
