using ClearData.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ClearData.Converters
{
    public class AboutPageTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals((string)parameter, "intro"))
            {
                switch ((AboutViewModel.TimePeriod)value)
                {
                    case AboutViewModel.TimePeriod.ALL_TIME:
                        return "Your All Time Earnings";
                    case AboutViewModel.TimePeriod.MONTHLY:
                        return "Your Monthly Earnings:";
                    case AboutViewModel.TimePeriod.WEEKLY:
                        return "Your Weekly Earnings:";
                }
            } else if (Equals((string)parameter, "breakdown")) 
            {
                switch((AboutViewModel.DisplayType)value)
                {
                    case AboutViewModel.DisplayType.COMPANIES:
                        return "See who's been paying for your data";
                    case AboutViewModel.DisplayType.DATATYPES:
                        return "See which data is earning you money";
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