using DatabaseLayer.ExFit_Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjUser : TBL_Users
    {

        //Sanal Tablo Kolonları

        [NotMapped]
        public string FullName // Personelin Tam Adı
        {
            get
            {
                return this.Name + " " + this.Surname;
            }
        }
        [NotMapped]
        public string Position 
        { 
            get
            {
                string position = "-";
                switch (this.Type)
                {
                    case 1:
                        position = "Yönetici";
                        break;
                    case 2:
                        position = "Antrenör";
                        break;
                    case 3:
                        position = "Diyetisyen";
                        break;
                    case 4:
                        position = "Satış Temsilcisi";
                        break;
                    case 5:
                        position = "Sosyal Medya Sorumlusu";
                        break;
                    case 6:
                        position = "Danışman";
                        break;
                    case 7:
                        position = "Genel";
                        break;
                }
                return position;
            }
        }
    }
}
