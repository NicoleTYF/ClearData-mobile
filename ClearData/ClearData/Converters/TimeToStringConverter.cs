using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ClearData.Converters
{
    /**
     * Used for converting a date time value to a string representing the time between the given
     * value and the current time
     */
    public class TimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime currentTime = DateTime.Now;
            DateTime registeredTime = (DateTime)value;
            //get the number of days different
            int difference = (currentTime.Date - registeredTime.Date).Days;
            //now do the checks, if they have the same day, then display it as a time
            if (difference <= 0)
            {   
                return String.Format("Today {0}:{1:00}", registeredTime.Hour, registeredTime.Minute);
            }
            else if (difference == 1)
            {
                return "Yesterday";
            }
            else if (difference < 7)
            {
                return String.Format("{0} days ago", difference);
            }
            else if (difference < 14)
            {
                return "A week ago";
            }
            else if (difference < 28)
            {
                return String.Format("{0} weeks ago", difference / 7);
            }
            return "Months ago";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //this conversion shouldn't ever be performed, may not be universally correct but shouldn't matter
            return 1;
        }
    }
}