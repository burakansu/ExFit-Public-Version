using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Package
    {
        [Key]
        public int Package_ID { get; set; }
        public string Name { get; set; }
        public int Month { get; set; }
        public int Price { get; set; }
        public int Company_ID { get; set; }
    }
}
