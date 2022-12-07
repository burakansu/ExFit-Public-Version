using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Company
    {
        [Key]
        public int Company_ID { get; set; }
        public string Name { get; set; }
        public int Package_Type { get; set; }
        public string Logo { get; set; }
        public DateTime Registration_Date { get; set; }
        public DateTime Registration_Time { get; set; }
    }
}
