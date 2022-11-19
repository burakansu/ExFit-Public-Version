﻿using DatabaseLayer.ExFit_Database;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjectLayer
{
    public class ObjUser : TBL_Users
    {

        //Sanal Tablo Kolonları

        [NotMapped]
        public string FullName
        {
            get
            {
                return this.Name + " " + this.Surname;
            }
        }
    }
}
