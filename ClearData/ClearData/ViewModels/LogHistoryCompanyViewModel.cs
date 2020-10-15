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
    /**
     * Class for the log history company page, extends from the general log history page, with a few minor additions
     */
	public class LogHistoryCompanyViewModel : LogHistoryViewModel
	{
        public Company Company { get; set; }

        public LogHistoryCompanyViewModel(Company company, ObservableCollection<Log> allLogs) : base(allLogs)
        {
            Company = company;
        }

    }
}