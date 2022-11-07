using DatabaseLayer;
using DatabaseLayer.MSSQL_Databases.ExFit_Database;

namespace ObjectLayer
{
    public class ObjCost : TBL_Cost
    {
        //Sanal Tablo Kolonları

        public int Total_Cost
        {
            get
            {
                SQL sQL = new SQL();
                return sQL.Value<int>("SELECT SUM(Rent,Electric,Water,Staff_Salaries,Other) FROM TBL_Cost");
            }
        }
        public int Which_Cost
        {
            get
            {
                return this.Rent + this.Electric + this.Water + this.Staff_Salaries + this.Other;
            }
        }
    }
}
