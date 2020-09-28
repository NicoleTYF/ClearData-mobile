using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearData.ViewModels;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;


namespace ClearData.Views
{
    public partial class AboutPage : ContentPage
    {

        AboutViewModel _viewModel;

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new AboutViewModel();
            OnAppearing();

            /*
            var myCarousel = new CarouselViewControl();
            myCarousel.ItemsSource = new ObservableCollection<int> { 1, 2 }; 
            myCarousel.Position = 0; //default
            myCarousel.InterPageSpacing = 10;
            myCarousel.Orientation = CarouselViewOrientation.Horizontal;
            */

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // ... our chart data and chart type here ...

            chartViewLine.Chart = new LineChart { Entries = _viewModel.entries, LineMode = LineMode.Straight };
        }
    }
}