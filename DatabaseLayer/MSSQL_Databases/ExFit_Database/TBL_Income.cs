namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Income
    {
        public int Income_ID { get; set; }
        public int Value { get; set; }
        public int WhichMonth { get; set; }
        public DateTime Year { get; set; }
    }
}
