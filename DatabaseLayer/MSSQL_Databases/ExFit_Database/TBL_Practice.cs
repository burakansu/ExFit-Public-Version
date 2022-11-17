namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Practice
    {
        public int Practice_ID { get; set; }
        public int Day { get; set; }
        public int BodySection { get; set; }
        public string Name { get; set; }
        public int SetCount { get; set; }
        public int Repeat { get; set; }
        public int CoolDownTime { get; set; }
        public string Note { get; set; }
        public int Excersize_ID { get; set; }
    }
}
