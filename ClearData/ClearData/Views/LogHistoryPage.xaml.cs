using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearData.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace ClearData.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogHistoryPage : ContentPage
	{
        public ObservableCollection<string> Items { get; set; } 

        public LogHistoryPage()  
		{
            InitializeComponent();
            this.BindingContext = new LogHistoryViewModel(); 

            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };
             
            LogHistory_Today.ItemsSource = Items;
        }
    }
}