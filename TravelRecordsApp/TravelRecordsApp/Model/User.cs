using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordsApp.Model
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250)]
        public string EmailUser { get; set; }

        [MaxLength(250)]
        public string PasswordUser { get; set; }

        [MaxLength(250)]
        public string NameUser { get; set; }


    }
}
