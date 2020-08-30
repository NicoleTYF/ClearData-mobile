using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ClearData.Converters
{
    public class StringCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).ToLower();
        }
    }
}
