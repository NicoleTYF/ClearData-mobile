using ClearData.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xamarin.Forms;

namespace ClearData.Converters
{
    public class NameToImageUrlConverter : IValueConverter
    {
        private static string clearbitUrl = "https://logo.clearbit.com";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //get the name, remove whitespace and make it all lowercase
            string name = (string)value;
            name.Replace(" ", String.Empty);
            name.ToLower();
            return String.Format("{0}/{1}.com", clearbitUrl, name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //this conversion shouldn't ever be performed, don't even know what a reverse conversion would be in this context
            return null; 
        }
    }
}