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
            //OnAppearing();

            /*
            var myCarousel = new CarouselViewControl();
            myCarousel.ItemsSource = new ObservableCollection<int> { 1, 2 }; 
            myCarousel.Position = 0; //default
            myCarousel.InterPageSpacing = 10;
<<<<<<< HEAD
            myCarousel.Orientation = CarouselViewOrientation.Horizontal;
            */

        }

        public void Picker(object sender, System.EventArgs e)
        {
            //this is honestly the weirdest stuff, the about view model somehow gets instantiated and called before
            //the actual page appears, so on app start up, and this gets run, even though the binding context hasn't been
            //set yet, so it crashes here on startup because _viewModel is null. Needed to add this just to stop it
            //from crashing at startup
            if (_viewModel != null)
            {
                _viewModel.UpdateDonutChart();
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            //this is so janky and I hate doing this, but the whole view model gets created early
            //this is all patching up the fact that this gets run on start up before we could have ever
            //established almost anything
            _viewModel.UpdateDonutChart();

            // ... our chart data and chart type here ...

            //chartViewLine.Chart = new LineChart { Entries = _viewModel.entries, LineMode = LineMode.Straight };
=======
            myCarousel.Orientation = CarouselViewOrientation.Horizontal;  
>>>>>>> master
        }
        
    }
}