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