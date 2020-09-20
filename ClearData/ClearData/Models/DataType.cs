using System;

namespace ClearData.Models
{
    public class DataType : DataObject
    {
        public enum DataTypeId { COARSE_LOCATION = 0, FINE_LOCATION = 1, BROWSING = 2, PHONE_USAGE = 3, PAYMENT_HISTORY = 4, PHOTOS = 5, ADVERTISING = 6}
        
        //whether this data type is enabled globally, need to infer this from the company settings
        public bool Enabled {
            get => GetEnabled();
            set => SetEnabled(value);
        } 

        private bool GetEnabled()
        {
            return UserInfo.GetPermissions().IsDataTypeEnabled(Id);
        }

        private void SetEnabled(bool setting)
        {
            //really annoyingly, this gets run every time we open that page, so we want to check whether or not the current setting is this one by inference
            //and then only update if we are changing. This prevents everything from getting overridden when we load the page (because this counts as
            //pressing the switch and there is no way of distinguishing whether it was pressed or just the page was opened)
            bool currentSetting = GetEnabled(); //read the current value
            if (setting != currentSetting)
            {
                UserInfo.GetPermissions().SetDataTypeGlobalPermission(Id, setting);
            }
        }
    }
}