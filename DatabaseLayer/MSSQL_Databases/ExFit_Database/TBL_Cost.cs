using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Cost
    {
        [Key]
        public int Cost_ID { get; set; }
        public int Rent { get; set; }
        public int Electric { get; set; }
        public int Water { get; set; }
        public int Staff_Salaries { get; set; }
        public int Other { get; set; }
        public int WhichMonth { get; set; }
        public DateTime Year { get; set; }
        public int Company_ID { get; set; }
    }
}
