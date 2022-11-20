using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Tasks
    {
        [Key]
        public int Task_ID { get; set; }
        public string Description { get; set; }
        public DateTime Create_Date { get; set; }
        public int User_ID { get; set; }
        public int Member_ID { get; set; }
    }
}
