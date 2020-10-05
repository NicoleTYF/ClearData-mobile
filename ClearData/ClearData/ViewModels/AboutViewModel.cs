using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microcharts;
using SkiaSharp;

namespace ClearData.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        public enum TimePeriod { ALL_TIME = 0, MONTHLY = 1, WEEKLY = 2}
        public enum DisplayType { COMPANIES = 0, DATATYPES = 1}

        public AboutViewModel()
        {
            /*
            MyItemsSource = new ObservableCollection<View>()
            {
                new CachedImage() { Source = "c1.jpg", DownsampleToViewSize = true, Aspect = Aspect.AspectFill },
                new CachedImage() { Source = "c2.jpg", DownsampleToViewSize = true, Aspect = Aspect.AspectFill },
                new CachedImage() { Source = "c3.jpg", DownsampleToViewSize = true, Aspect = Aspect.AspectFill }
            };
            */
            Period = (int)TimePeriod.ALL_TIME;
            Display = (int)DisplayType.COMPANIES;
            DisplayPrice = "$10.23";

            var entries = new ChartEntry[]
            {
                new ChartEntry(600) { ValueLabel = "600", Label = "January", Color = SKColor.Parse("#266489") },
                new ChartEntry(600) { ValueLabel = "600", Label = "February", Color = SKColor.Parse("#68B9C0") },
                new ChartEntry(600) { ValueLabel = "600", Label = "March", Color = SKColor.Parse("#90D585") },
                new ChartEntry(600) { ValueLabel = "600", Label = "April", Color = SKColor.Parse("#F3C151")},
                new ChartEntry(600) { ValueLabel = "600", Label = "May", Color = SKColor.Parse("#F37F64")},
                new ChartEntry(600) { ValueLabel = "600", Label = "June", Color = SKColor.Parse("#424856") },
                new ChartEntry(600) { ValueLabel = "600", Label = "July", Color = SKColor.Parse("#8F97A4")},
                new ChartEntry(600) { ValueLabel = "600", Label = "August", Color = SKColor.Parse("#DAC096") },
                new ChartEntry(600) { ValueLabel = "600", Label = "September", Color = SKColor.Parse("#76846E") },
                new ChartEntry(600) { ValueLabel = "600", Label = "October", Color = SKColor.Parse("#A65B69") },
                new ChartEntry(600) { ValueLabel = "600", Label = "November", Color = SKColor.Parse("#DABFAF") },
                new ChartEntry(600) { ValueLabel = "600", Label = "December", Color = SKColor.Parse("#97A69D") },
            };

            DonutChart = new DonutChart() { BackgroundColor=SKColors.Transparent, Entries = entries };
        }

        /*
        ObservableCollection<View> _myItemsSource;
        public ObservableCollection<View> MyItemsSource {
            set {
                _myItemsSource = value;
                OnPropertyChanged("MyItemsSource");
            }
            get {
                return _myItemsSource;
            }
        }
        

        public Command MyCommand { protected set; get; }
        */

        private String displayPrice;
        public String DisplayPrice
        {
            get => displayPrice;
            set => SetProperty(ref displayPrice, value);
        }

        //these two are ints not the enums themselves because of interacting with a picker
        private int period;
        public int Period {
            get => period;
            set => SetProperty(ref period, value);
        }
        private int display;
        public int Display {
            get => display;
            set => SetProperty(ref display, value);
        }
        

        private Chart donutChart;
        public Chart DonutChart
        {
            get => donutChart;
            set => SetProperty(ref donutChart, value);
        }

        /*
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        */
    }
}