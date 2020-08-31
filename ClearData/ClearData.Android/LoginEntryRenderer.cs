using Xamarin.Forms;
using XamarinBorderlessEntry.Controls;
using XamarinBorderlessEntry.Droid.ControlHelpers;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;

[assembly:ExportRenderer(typeof(LoginEntry), typeof(LoginEntryRenderer))]

namespace LoginEntry.Android
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
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.Transparent);
                this.Control.SetBackgroundDrawable(gd);
                this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.White));
            }
        }
    }
}