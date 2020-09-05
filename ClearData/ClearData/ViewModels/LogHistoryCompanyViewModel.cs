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
	public class LogHistoryCompanyViewModel : LogHistoryViewModel
	{
        public Company Company { get; set; }

        public LogHistoryCompanyViewModel(Company company, ObservableCollection<Log> allLogs) : base(allLogs)
        {
            Company = company;
        }

    }
}