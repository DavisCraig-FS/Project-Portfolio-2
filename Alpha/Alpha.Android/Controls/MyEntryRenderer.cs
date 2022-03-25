using System;
using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Graphics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(Alpha.Droid.Controls.MyEntryRenderer))]
namespace Alpha.Droid.Controls
{
    
    public class MyEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(color: Android.Graphics.Color.White);
            
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.White, PorterDuff.Mode.Src);
        }
    }
}
