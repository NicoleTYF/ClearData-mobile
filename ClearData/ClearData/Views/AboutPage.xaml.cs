using ClearData.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CarouselView.FormsPlugin.Abstractions;

namespace ClearData.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = new AboutViewModel();
            var myCarousel = new CarouselViewControl();
            myCarousel.ItemsSource = new ObservableCollection<int> { 1, 2 }; 
            myCarousel.Position = 0; //default
            myCarousel.InterPageSpacing = 10;
            myCarousel.Orientation = CarouselViewOrientation.Horizontal;
        }

        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            Debug.WriteLine("Position " + e.NewValue + " selected.");
        }

        void Handle_Scrolled(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            Debug.WriteLine("Scrolled to " + e.NewValue + " percent.");
            Debug.WriteLine("Direction = " + e.Direction);
        }
    }
}