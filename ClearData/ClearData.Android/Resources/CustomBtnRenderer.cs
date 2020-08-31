using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using ClearData.Controls;
using ClearData.Android;
using Android.Content;

[assembly: ExportRenderer(typeof(CustomBtn), typeof(CustomBtnRenderer))]
namespace ClearData.Android
{
    public class CustomBtnRenderer : ButtonRenderer
    {
        public CustomBtnRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            this.Control.SetPadding(
                (int)((CustomBtn)this.Element).Padding.Left,
                (int)((CustomBtn)this.Element).Padding.Top,
                (int)((CustomBtn)this.Element).Padding.Right, 
                (int)((CustomBtn)this.Element).Padding.Bottom);
        }
    }
}