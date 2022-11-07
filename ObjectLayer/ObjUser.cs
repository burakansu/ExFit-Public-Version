using DatabaseLayer.ExFit_Database;

namespace ObjectLayer
{
    public class ObjUser : TBL_Users
    {

        //Sanal Tablo Kolonları

        public string FullName
        {
            get
            {
                return this.Name + " " + this.Surname;
            }
        }
    }
}
