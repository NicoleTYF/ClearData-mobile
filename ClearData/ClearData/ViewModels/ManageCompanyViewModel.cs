using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ClearData.Models;
using Xamarin.Forms;

namespace ClearData.ViewModels
{
    public class ManageCompanyViewModel : BaseViewModel
    {
        
        public Company Company { get; set; }

        public ManageCompanyViewModel(Company company)
        {
            Title = String.Format("Manage {0} permissions", company.Name);
            Company = company;
        }

    }
}
