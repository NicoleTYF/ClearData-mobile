using System;
using System.Collections.Generic;
using System.Text;

namespace ClearData.Models
{
    public class UserDataJson
    {
        public Auth auth { get; set; }
        public Profile profile { get; set; }
    }

    public class Auth
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class Profile
    {
        public string date_of_birth { get; set; }
        public string birthplace { get; set; }
    }

    public class DatabaseInfo
    {
        public int user_id { get; set; }
        public string token { get; set; }
    }
}
