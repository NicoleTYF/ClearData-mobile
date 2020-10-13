using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ClearData.Converters
{
    /**
     * Class for determining the colour of the buttons to be display dependent on whether they are being displayed or not
     */
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals((string)parameter, "primary"))
            {
                return ((bool)value == true) ? Color.White : Color.FromHex("#416369"); // change hardcoded colours  
            } else //secondary
            {
                return ((bool)value == true) ? Color.FromHex("#26A2C9") : Color.FromHex("#7D7D7D");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //this conversion shouldn't ever be performed, may not be universally correct but shouldn't matter
            return ((Color)value == Color.White); 
        }
    }
}