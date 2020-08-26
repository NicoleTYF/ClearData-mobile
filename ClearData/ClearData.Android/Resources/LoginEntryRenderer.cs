using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using ClearData.Controls;
using ClearData.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(LoginEntry), typeof(LoginEntryRenderer))]
namespace ClearData.Android
{
    public class LoginEntryRenderer : EntryRenderer
    {
        public LoginEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                Control.SetTextSize(global::Android.Util.ComplexUnitType.Dip, 20);
                Control.SetHighlightColor(global::Android.Graphics.Color.ParseColor("#FFE88F")); // amber color
                Control.SetWidth(230);
                Control.SetHeight(50);
                Control.SetTextColor(global::Android.Graphics.Color.White);
            }
        }
    }
}