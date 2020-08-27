using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

using ClearData.Models;

namespace ClearData.Converters
{
    public class PermissionTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals((string)parameter, "info"))
            {
                switch ((Company.RestrictionType)value)
                {
                    case Company.RestrictionType.ALL:
                        return "On this setting, any data you are sharing will be accessible by this company";
                    case Company.RestrictionType.CUSTOM_OPT_IN:
                        return "On this setting, your settings below will apply and any new data you choose to share will not be shared with this company unless you opt in";
                    case Company.RestrictionType.CUSTOM_OPT_OUT:
                        return "On this setting, your settings below will apply and any new data you choose to share will be shared with this company unless you opt out";
                    case Company.RestrictionType.NONE:
                    default:
                        return "On this setting, none of your data will be shared with this company";
                }
            }
            else
            {
                switch ((Company.RestrictionType)value)
                {
                    case Company.RestrictionType.ALL:
                        return "All";
                    case Company.RestrictionType.CUSTOM_OPT_IN:
                        return "Custom Opt In";
                    case Company.RestrictionType.CUSTOM_OPT_OUT:
                        return "Custom Opt Out";
                    case Company.RestrictionType.NONE:
                    default:
                        return "None";
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch((string)value)
            {
                case "All":
                    return Company.RestrictionType.ALL;
                case "Custom Opt In":
                    return Company.RestrictionType.CUSTOM_OPT_IN;
                case "Custom Opt Out":
                    return Company.RestrictionType.CUSTOM_OPT_OUT;
                default:
                    return Company.RestrictionType.NONE;
            }
        }
    }
}