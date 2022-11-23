using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Income
    {
        [Key]
        public int Income_ID { get; set; }
        public int Value { get; set; }
        public int WhichMonth { get; set; }
        public int Year { get; set; }
        public int Company_ID { get; set; }
    }
}
