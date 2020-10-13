using ClearData.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ClearData.Converters
{
    /**
     * Class for determining what text to display on the about page
     */
    public class AboutPageTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals((string)parameter, "intro"))
            {
                switch ((AboutViewModel.TimePeriod)value)
                {
                    case AboutViewModel.TimePeriod.ALL_TIME:
                        return "Since starting with the app you have earned:";
                    case AboutViewModel.TimePeriod.MONTHLY:
                        return "In the last month you have earned:";
                    case AboutViewModel.TimePeriod.WEEKLY:
                        return "In the last week you have earned:";
                }
            } else if (Equals((string)parameter, "breakdown")) 
            {
                switch((AboutViewModel.DisplayType)value)
                {
                    case AboutViewModel.DisplayType.COMPANIES:
                        return "Here's a breakdown of who's been paying for your data";
                    case AboutViewModel.DisplayType.DATATYPES:
                        return "Here's a breakdown of which data is earning you money";
                }
            }
            return "bad argument";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //this conversion shouldn't ever be performed, don't even know what a reverse conversion would be in this context
            return null; 
        }
    }
}