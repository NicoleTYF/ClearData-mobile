using System;
using System.Collections.Generic;
using System.Text;
using ClearData.Models;
using ClearData.Services;


namespace ClearData
{
    public static class UserInfo
    {
        // placeholder value
        // TODO: replace string with DataType
        public static PermissionsDataStore permissions { get; set; }

        public static string name { get; set; }

        public static DateTime DOB { get; set; }

        public static string birthPlace { get; set; }

    }
}
