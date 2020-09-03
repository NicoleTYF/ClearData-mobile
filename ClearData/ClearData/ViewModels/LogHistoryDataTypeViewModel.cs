using ClearData.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClearData.ViewModels
{
	public class LogHistoryDataTypeViewModel : LogHistoryViewModel
	{
        public DataType DataType { get; set; }

        public LogHistoryDataTypeViewModel(DataType dataType, ObservableCollection<Log> allLogs) : base(allLogs)
        {
            DataType = dataType;
        }

    }
}