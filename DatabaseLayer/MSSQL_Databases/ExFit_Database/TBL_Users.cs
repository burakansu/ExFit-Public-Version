using System.ComponentModel.DataAnnotations;

namespace DatabaseLayer.ExFit_Database
{
    public class TBL_Users
    {
        [Key]
        public int User_ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public int Type { get; set; }
        public string IMG { get; set; }
        public string Phone { get; set; }
    }
}
