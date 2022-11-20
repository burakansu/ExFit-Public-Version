using DatabaseLayer.MSSQL_Databases.ExFit_Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjCost : TBL_Cost
    {
        //Sanal Tablo Kolonları

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
