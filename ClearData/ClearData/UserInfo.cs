using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ClearData.Models;
using ClearData.Services;


namespace ClearData
{
    public static class UserInfo
    {
        private static PermissionsDataStore permissions;

        public static async Task LoadPermissionsDataStore()
        {
            permissions = new PermissionsDataStore();
            await permissions.LoadDataStore();
        }

        public static PermissionsDataStore GetPermissions()
        {
            return permissions;
        }

        public static string name { get; set; }

        public static DateTime DOB { get; set; }

        public static string birthPlace { get; set; }

        public static DatabaseInfo DatabaseInfo {get; set;}

    }
}
