using ClearData.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Xamarin.Forms;

namespace ClearData.Converters
{
    public class IsEmptyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //the goal here is to return true if the supplied list is empty
            ObservableCollection<Log> logs = (ObservableCollection<Log>)value;
            return logs.Count == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //this conversion shouldn't ever be performed, may not be universally correct but shouldn't matter
            return new IndexedLogCollection();
        }
    }
}