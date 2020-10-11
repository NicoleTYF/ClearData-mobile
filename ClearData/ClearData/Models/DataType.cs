using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using Plugin.Permissions;

namespace ClearData.Models
{
    public class DataType : DataObject
    {
        public enum DataTypeId { LOCATION = 0, BROWSING = 1, PHONE_USAGE = 2, PAYMENT_HISTORY = 3, PHOTOS = 4, ADVERTISING = 5}
        
        //whether this data type is enabled globally, need to infer this from the company settings
        public bool Enabled {
            get => GetEnabled();
            set => SetEnabled(value);
        } 

        private bool GetEnabled()
        {
            return UserInfo.GetPermissions().IsDataTypeEnabledGlobally(Id);
        }

        private void SetEnabled(bool setting)
        {
            //really annoyingly, this gets run every time we open that page, so we want to check whether or not the current setting is this one by inference
            //and then only update if we are changing. This prevents everything from getting overridden when we load the page (because this counts as
            //pressing the switch and there is no way of distinguishing whether it was pressed or just the page was opened)
            bool currentSetting = GetEnabled(); //read the current value
            bool canToggle = true;
            if (Name == "Location") 
            {
                ToggleLocationCapture(setting);
                canToggle = UserInfo.locationPossible;
            }
            if (setting != currentSetting && canToggle)
            {
                UserInfo.GetPermissions().SetDataTypeGlobalPermission(Id, setting);
            }
        }

        private async void ToggleLocationCapture(bool setting)
        {
            UserInfo.locationEnabled = setting;
            if (setting)
            {
                Device.StartTimer(TimeSpan.FromSeconds(5), () =>
                {
                    Task.Run(async () =>
                    {
                        var permStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>();
                        if (permStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            try
                            {
                                permStatus = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();                      
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                        if (permStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                        {
                            UserInfo.locationPossible = false;
                            UserInfo.locationEnabled = false;
                            return;
                        }
                        else
                        {
                            UserInfo.locationPossible = true;
                        }
                        try
                        {
                            var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                            var location = await Geolocation.GetLocationAsync(request);

                            if (location != null)
                            {
                                Console.WriteLine("LOCATION HERE!");
                                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                            }
                        }
                        catch (PermissionException PEx)
                        {
                            Console.WriteLine("WE HAD A PERMISSION PROBLEM!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("AWW NUTS SOMETHING WENT WRONG!");
                        }
                    });
                    // we return true here if we are wanting to keep the timer going
                    return UserInfo.locationEnabled;
                });
            }
        }
    }
}