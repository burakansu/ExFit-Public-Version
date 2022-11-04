using DatabaseLayer.ExFit_Database;

namespace ObjectLayer
{
    public class ObjUser : TBL_Users
    {
        public string FullName
        {
            get
            {
                return this.Name + " " + this.Surname;
            }
        }
    }
}
