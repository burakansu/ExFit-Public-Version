using DatabaseLayer;
using DatabaseLayer.MSSQL_Databases.ExFit_Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjCost : TBL_Cost
    {
        //Sanal Tablo Kolonları

        [NotMapped]
        public int Total_Cost
        {
            get
            {
                return new SQL().Value<int>("SELECT SUM(Rent,Electric,Water,Staff_Salaries,Other) FROM TBL_Cost");
            }
        }
        [NotMapped]
        public int Which_Cost
        {
            get
            {
                return this.Rent + this.Electric + this.Water + this.Staff_Salaries + this.Other;
            }
        }
    }
}
