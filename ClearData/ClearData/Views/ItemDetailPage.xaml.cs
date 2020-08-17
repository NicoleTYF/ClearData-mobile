using System.ComponentModel;
using Xamarin.Forms;
using ClearData.ViewModels;

namespace ClearData.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}