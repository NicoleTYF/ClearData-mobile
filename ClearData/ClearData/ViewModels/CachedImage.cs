using Xamarin.Forms;

namespace ClearData.ViewModels
{
    internal class CachedImage : View
    {
        public string Source { get; set; }
        public bool DownsampleToViewSize { get; set; }
        public Aspect Aspect { get; set; }
    }
}