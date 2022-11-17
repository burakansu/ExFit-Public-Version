namespace DatabaseLayer.MSSQL_Databases.ExFit_Database
{
    public class TBL_Food
    {
        public int Food_ID { get; set; }
        public int MealType { get; set; }
        public string Name { get; set; }
        public int Calorie { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbonhidrat { get; set; }
        public string Note { get; set; }
        public int Diet_ID { get; set; }
        public int Day { get; set; }
    }
}
