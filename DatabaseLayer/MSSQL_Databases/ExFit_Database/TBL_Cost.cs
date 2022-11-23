using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Cost
    {
        [Key]
        public int Cost_ID { get; set; }
        [DefaultValue(0)]
        public int Rent { get; set; }
        [DefaultValue(0)]
        public int Electric { get; set; }
        [DefaultValue(0)]
        public int Water { get; set; }
        [DefaultValue(0)]
        public int Staff_Salaries { get; set; }
        [DefaultValue(0)]
        public int Other { get; set; }
        [DefaultValue(1)]
        public int WhichMonth { get; set; }
        public DateTime Year { get; set; }
        public int Company_ID { get; set; }
    }
}
