using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ClearData.Converters
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals((string)parameter, "primary"))
            {
                return ((bool)value == true) ? Color.White : Color.DarkGray; //hardcoded colours, may need to change
            } else //secondary
            {
                return ((bool)value == true) ? Color.CornflowerBlue : Color.LightGray;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //this conversion shouldn't ever be performed, may not be universally correct but shouldn't matter
            return ((Color)value == Color.White); 
        }
    }
}