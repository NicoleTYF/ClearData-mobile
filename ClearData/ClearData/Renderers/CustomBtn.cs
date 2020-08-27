using System;
using Xamarin.Forms;

namespace ClearData.Controls
{
    public class CustomBtn : Button
    {
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(Button), default(Thickness));

        public Thickness Padding {
            get => (Thickness)this.GetValue(PaddingProperty);
            set => this.SetValue(PaddingProperty, value);
        }
    }
}
