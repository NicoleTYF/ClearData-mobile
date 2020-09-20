using System;
using System.Collections.Generic;
using System.Text;
using ClearData.Models;
using ClearData.Services;


namespace ClearData
{
    public static class UserInfo
    {
        private static PermissionsDataStore permissions;

        public static PermissionsDataStore GetPermissions()
        {
            //if the permissions data store doesn't exist yet, create it, otherwise return it
            if (permissions == null)
            {
                permissions = new PermissionsDataStore();
            }
            return permissions;
        }

        public static string name { get; set; }

        public static DateTime DOB { get; set; }

        public static string birthPlace { get; set; }

    }
}
