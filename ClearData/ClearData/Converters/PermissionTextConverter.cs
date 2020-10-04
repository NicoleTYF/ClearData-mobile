using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

using ClearData.Models;
using System.Threading;

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
                        return "On this setting (All), any data you are sharing will be accessible by this company";
                    case Company.RestrictionType.CUSTOM:
                        return "On this setting (Custom), you can toggle access to any data types";
                    case Company.RestrictionType.NONE:
                    default:
                        return "On this setting (None), none of your data will be shared with this company";
                }
            }
            else
            {
                switch ((Company.RestrictionType)value)
                {
                    case Company.RestrictionType.ALL:
                        return "All";
                    case Company.RestrictionType.CUSTOM:
                        return "Custom";
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
                case "Custom":
                    return Company.RestrictionType.CUSTOM;
                default:
                    return Company.RestrictionType.NONE;
            }
        }
    }
}