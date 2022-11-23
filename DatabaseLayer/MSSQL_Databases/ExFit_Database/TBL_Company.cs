using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Company
    {
        [Key]
        public int Company_ID { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
    }
}
